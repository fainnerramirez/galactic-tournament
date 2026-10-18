using TorneoGalactico.Api.Models;
using static TorneoGalactico.Api.DTOs.Dtos;

namespace TorneoGalactico.Api.Services.SpeciesServices
{
    public interface ISpecieService
    {
        Task<SpeciesResponse> CreateAsync(CreateSpeciesRequest request);
        Task<List<SpeciesResponse>> GetAllAsync();
        Task<Species?> GetByIdAsync(int Specie);
        Task<List<int>> GetBySpeciesIdListAsync();
    }
}