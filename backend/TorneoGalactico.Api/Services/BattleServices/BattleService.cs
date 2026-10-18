using Dapper;
using TorneoGalactico.Api.Data.Conection;
using TorneoGalactico.Api.DTOs;
using TorneoGalactico.Api.Rules;
using TorneoGalactico.Api.Services.SpeciesServices;
using static TorneoGalactico.Api.DTOs.Dtos;
using static TorneoGalactico.Api.Exceptions.AppExceptions;

namespace TorneoGalactico.Api.Services.BattleServices
{
    public record BattleRow
    {
        public int Id { get; set; }
        public int SpeciesAId { get; set; }
        public string SpeciesAName { get; set; } = "";
        public int SpeciesBId { get; set; }
        public string SpeciesBName { get; set; } = "";
        public int WinnerId { get; set; }
        public string WinnerName { get; set; } = "";
        public string FoughtAt { get; set; } = "";
    }

    public class BattleService(ISqliteConnection conn, ISpecieService specieService) : IBattleServices
    {
        public async Task<BattleResponse> CombatAsync(int speciesAId, int speciesBId)
        {
            if(speciesAId == speciesBId)
            {
                throw new BadRequestCombatException("La misma especie no puede tener un combate contra si misma");
            }

            using var db = conn.onCreate();

            var specieA = await specieService.GetByIdAsync(speciesAId) ?? throw new NotFoundCombatException($"No existe la especie con el Id {speciesAId}");
            var specieB = await specieService.GetByIdAsync(speciesBId) ?? throw new NotFoundCombatException($"No existe la especie con el Id {speciesBId}");

            var winner = BattleRules.DetermineWinner(specieA, specieB);
            var combatAt = DateTime.UtcNow;

            var id = await db.ExecuteScalarAsync<long>("""
                INSERT INTO Battles (SpeciesAId, SpeciesBId, WinnerId, FoughtAt)
                VALUES (@SpeciesAId, @SpeciesBId, @WinnerId, @FoughtAt);
                SELECT last_insert_rowid();
                """,
                new { SpeciesAId = specieA.Id, SpeciesBId = specieB.Id, WinnerId = winner.Id, FoughtAt = combatAt.ToString("O") 
            });

            return new BattleResponse(
                (int)id,
                specieA.Id,
                specieA.Name,
                specieB.Id,
                specieB.Name,
                winner.Id,
                winner.Name,
                combatAt
            );
        }

        public async Task<BattleResponse> CombatRandomAsync()
        {
            var ids = await specieService.GetBySpeciesIdListAsync();

            if (ids.Count < 2) 
                throw new BadRequestCombatException("No se puede crear una batalla...se necesitan al menos 2 especies registradas");

            var pairs = ids.OrderBy(_ => Random.Shared.Next()).Take(2).ToList();
            var combat = await CombatAsync(pairs[0], pairs[1]);
            return combat;
        }

        public async Task<List<BattleResponse>> GetAllCombatsAsync()
        {
            using var db = conn.onCreate();

            var rows = await db.QueryAsync<BattleRow>("""
                SELECT b.Id,
                       b.SpeciesAId, sa.Name AS SpeciesAName,
                       b.SpeciesBId, sb.Name AS SpeciesBName,
                       b.WinnerId,   sw.Name AS WinnerName,
                       b.FoughtAt
                FROM Battles b
                JOIN Species sa ON sa.Id = b.SpeciesAId
                JOIN Species sb ON sb.Id = b.SpeciesBId
                JOIN Species sw ON sw.Id = b.WinnerId
                ORDER BY b.Id DESC
                """
            );

            return rows.Select(r => new BattleResponse(
                r.Id, r.SpeciesAId, r.SpeciesAName, r.SpeciesBId, r.SpeciesBName,
                r.WinnerId, r.WinnerName,
                DateTime.Parse(r.FoughtAt, null, System.Globalization.DateTimeStyles.RoundtripKind))).ToList();
        }
    }
}