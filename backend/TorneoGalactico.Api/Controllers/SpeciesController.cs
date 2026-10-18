using Microsoft.AspNetCore.Mvc;
using TorneoGalactico.Api.Services.SpeciesServices;
using static TorneoGalactico.Api.DTOs.Dtos;

namespace TorneoGalactico.Api.Controllers
{
    [ApiController]
    [Route("api/species")]
    public class SpeciesController(ISpecieService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<SpeciesResponse>>> GetAll()
        {
            var response = await service.GetAllAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<SpeciesResponse>> Create(CreateSpeciesRequest request)
        {
            var created = await service.CreateAsync(request);
            return Created(string.Empty, created);
        }
    }
}