using static TorneoGalactico.Api.DTOs.Dtos;

namespace TorneoGalactico.Api.Services.RankingServices
{
    public interface IRankingService
    {
        Task<List<RankingEntryResponse>> GetRankingAsync();
    }
}
