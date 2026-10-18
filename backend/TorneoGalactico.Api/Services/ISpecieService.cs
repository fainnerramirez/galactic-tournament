using static TorneoGalactico.Api.DTOs.Dtos;

namespace TorneoGalactico.Api.Services
{
    public interface ISpecieService
    {
        Task<SpeciesResponse> CreateAsync(CreateSpeciesRequest request);
        Task<List<SpeciesResponse>> GetAllAsync();
    }
}
