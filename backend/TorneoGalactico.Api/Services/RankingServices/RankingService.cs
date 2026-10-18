using Dapper;
using TorneoGalactico.Api.Data.Conection;
using TorneoGalactico.Api.DTOs;
using static TorneoGalactico.Api.DTOs.Dtos;

namespace TorneoGalactico.Api.Services.RankingServices
{
    public class RankingService(ISqliteConnection conn) : IRankingService
    {
        private record RankingRow
        {
            public int SpeciesId { get; set; }
            public string Name { get; set; } = "";
            public int PowerLevel { get; set; }
            public int Wins { get; set; }
            public int BattlesPlayed { get; set; }
        }
    
        public async Task<List<RankingEntryResponse>> GetRankingAsync()
        {
            using var db = conn.onCreate();

            // Victorias y combates se calculan desde Battles: no hay contadores duplicados.
            var rows = await db.QueryAsync<RankingRow>("""
            SELECT s.Id AS SpeciesId, s.Name, s.PowerLevel,
                   (SELECT COUNT(*) FROM Battles b WHERE b.WinnerId = s.Id) AS Wins,
                   (SELECT COUNT(*) FROM Battles b
                     WHERE b.SpeciesAId = s.Id OR b.SpeciesBId = s.Id) AS BattlesPlayed
            FROM Species s
            ORDER BY Wins DESC, s.Name COLLATE NOCASE ASC
            """);

            return rows
                .Select((r, i) => 
                    new RankingEntryResponse(i + 1, r.SpeciesId, r.Name, r.PowerLevel, r.Wins, r.BattlesPlayed))
                .ToList();
        }
    }
}
