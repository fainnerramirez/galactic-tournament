using TorneoGalactico.Api.Models;
using static TorneoGalactico.Api.DTOs.Dtos;

namespace TorneoGalactico.Api.Services.BattleServices
{
    public interface IBattleServices
    {
        Task<BattleResponse> CombatAsync(int speciesAId, int speciesBId);
        Task<BattleResponse> CombatRandomAsync();
        Task<List<BattleResponse>> GetAllCombatsAsync();
    }
}