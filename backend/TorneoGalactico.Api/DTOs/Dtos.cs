namespace TorneoGalactico.Api.DTOs
{
    public class Dtos
    {
        public record CreateSpeciesRequest(string Name, int PowerLevel, string SpecialAbility);
        public record SpeciesResponse(int Id, string Name, int PowerLevel, string SpecialAbility);
        public record CreateBattleRequest(int SpeciesAId, int SpeciesBId);
        public record BattleResponse(
            int Id,
            int SpeciesAId, string SpeciesAName,
            int SpeciesBId, string SpeciesBName,
            int WinnerId, string WinnerName,
            DateTime FoughtAt
        );
        public record RankingEntryResponse(int Position, int SpeciesId, string Name, int PowerLevel, int Wins, int BattlesPlayed);
    }
}
