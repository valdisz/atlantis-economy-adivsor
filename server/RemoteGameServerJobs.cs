namespace advisor {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using advisor.Features;
    using advisor.Persistence;
    using AngleSharp;
    using Hangfire;
    using Hangfire.Console;
    using Hangfire.RecurringJobExtensions;
    using Hangfire.Server;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class RemoteGameServerJobs {
        public RemoteGameServerJobs(Database db, IMediator mediator, IHttpClientFactory httpClientFactory,
            IBackgroundJobClient backgroundJobs,
            ILogger<RemoteGameServerJobs> logger) {
            this.db = db;
            this.mediator = mediator;
            this.httpClientFactory = httpClientFactory;
            this.backgroundJobs = backgroundJobs;
            this.logger = logger;
        }

        private readonly Database db;
        private readonly IMediator mediator;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IBackgroundJobClient backgroundJobs;
        private readonly ILogger logger;

        [RecurringJob("0 12 * * 2,5", TimeZone = "America/Los_Angeles")]
        public async Task NewOrigins3(PerformContext context) {
            var localTurnNumber = await db.Players
                .Select(x => x.LastTurnNumber)
                .MaxAsync();

            logger.LogInformation($"Last known turn number is {localTurnNumber}");

            var started = DateTime.UtcNow;
            int remoteTurnNumber;
            do {
                remoteTurnNumber = await GetRemoteTurnNumberAsync();
                logger.LogInformation($"Remote turn number is {remoteTurnNumber}");

                if (localTurnNumber >= remoteTurnNumber) {
                    logger.LogInformation($"Sleep 1 minute");
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
            }
            while (localTurnNumber >= remoteTurnNumber && (TimeSpan.FromMinutes(30) > (DateTime.UtcNow - started)));

            if (remoteTurnNumber > localTurnNumber) {
                await DownloadReportsAsync();
            }
            else {
                logger.LogError($"New turn was not generated or was not possible to reach server");
            }
        }

        private async Task<int> GetRemoteTurnNumberAsync() {
            logger.LogInformation("Scraping remote game state");

            var config = Configuration.Default.WithDefaultLoader();

            var browsingContext = BrowsingContext.New(config);
            var document = await browsingContext.OpenAsync("http://atlantis-pbem.com/");

            var allHeadings = document.QuerySelectorAll("h3");
            foreach (var h in allHeadings) {
                if (h.TextContent.StartsWith("Turn Number:")) {
                    var turnNumber = int.Parse(h.QuerySelector("span").TextContent);
                    return turnNumber;
                }
            }

            return -1;
        }

        private async Task<string> DownloadReportForFactionAsync(int factionNumber, string password) {
            using var http = httpClientFactory.CreateClient();

            var fields = new Dictionary<string, string>();
            fields.Add("factionId", factionNumber.ToString());
            fields.Add("password", password);

            var request = new HttpRequestMessage(HttpMethod.Post, "http://atlantis-pbem.com/game/download-report") {
                Content = new FormUrlEncodedContent(fields)
            };

            var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var report = await response.Content.ReadAsStringAsync();
            return report;
        }

        public async Task DownloadReportsAsync() {
            logger.LogInformation($"Queuing downloading new turns from server");

            var factions = await db.Players
                .Where(x => x.FactionNumber != null && x.Password != null)
                .Select(x => new { x.Id, x.FactionName, FactionNumber = x.FactionNumber.Value, x.Password })
                .ToListAsync();

            if (factions.Count == 0) {
                logger.LogInformation($"No factions with known password, report downloading completed.");
                return;
            }

            foreach (var f in factions) {
                backgroundJobs.Enqueue<RemoteGameServerJobs>(x => x.DownloadReportsForFactionAsync(
                    f.FactionName, f.FactionNumber, f.Password, f.Id
                ));
            }

            logger.LogInformation($"Report download for all factions is queued");
        }

        public async Task DownloadReportsForFactionAsync(string factionName, int factionNumber, string password, long playerId) {
            logger.LogInformation($"Downloading report for {factionName} ({factionNumber})");
            var report = await DownloadReportForFactionAsync(factionNumber, password);

            logger.LogInformation($"Saving report to database");
            var turn = await mediator.Send(new UploadReports(playerId, new[] { report }));

            await mediator.Send(new ProcessTurn(playerId, turn));

            logger.LogInformation($"Report is downloaded and processed");
        }
    }
}
