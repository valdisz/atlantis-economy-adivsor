namespace advisor.Features {
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using advisor.Model;
    using advisor.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public record ImportMap(long PlayerId, string Source) : IRequest;

    public class ImportMapHandler : IRequestHandler<ImportMap> {
        public ImportMapHandler(Database db, IMediator mediator) {
            this.db = db;
            this.mediator = mediator;
        }

        private readonly Database db;
        private readonly IMediator mediator;

        public async Task<MediatR.Unit> Handle(ImportMap request, CancellationToken cancellationToken) {
            DbPlayer player = await db.Players
                .Include(x => x.Game)
                .SingleOrDefaultAsync(x => x.Id == request.PlayerId);

            if (player == null) return Unit.Value;

            using var textReader = new StringReader(request.Source);
            using var atlantisReader = new AtlantisReportJsonConverter(textReader,
                new RegionsSection()
            );
            var json = await atlantisReader.ReadAsJsonAsync();
            var report  = json.ToObject<JReport>();

            int earliestTurn = player.LastTurnNumber;

            await mediator.Send(new ParseReports(player.Id, earliestTurn, report));

            return Unit.Value;
        }
    }
}
