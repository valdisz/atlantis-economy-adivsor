namespace advisor.Features
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using advisor.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public record DeleteGame(long GameId) : IRequest<List<DbGame>> {

    }

    public class DeleteGameHandler : IRequestHandler<DeleteGame, List<DbGame>> {
        public DeleteGameHandler(Database db) {
            this.db = db;
        }

        private readonly Database db;

        public async Task<List<DbGame>> Handle(DeleteGame request, CancellationToken cancellationToken) {
            var gameId = request.GameId;
            var game = await db.Games.FindAsync(gameId);

            if (game != null)  {
                var turns = await db.Turns
                    .AsNoTracking()
                    .Include(x => x.Player)
                    .Where(x => x.Player.GameId == gameId)
                    .Select(x => x.Id)
                    .ToListAsync();

                foreach (var turnId in turns) {
                    await DeleteFromGameAsync<DbStudyPlan>(turnId);
                    await DeleteFromGameAsync<DbEvent>(turnId);
                    await DeleteFromGameAsync<DbUnit>(turnId);
                    await DeleteFromGameAsync<DbStructure>(turnId);
                    await DeleteFromGameAsync<DbRegion>(turnId);
                    await DeleteFromGameAsync<DbFaction>(turnId);
                    await DeleteFromGameAsync<DbReport>(turnId);
                }

                db.Remove(game);
                await db.SaveChangesAsync();
            }

            var games = await db.Games.ToListAsync();
            return games;
        }

        private Task DeleteFromGameAsync<T>(long turnId) {
            var targetTable = db.Model.FindEntityType(typeof(T)).GetTableName();

            return db.Database.ExecuteSqlRawAsync($@"delete from {targetTable} where TurnId = {turnId}");
        }
    }
}