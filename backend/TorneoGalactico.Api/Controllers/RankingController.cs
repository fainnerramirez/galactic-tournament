using Microsoft.AspNetCore.Mvc;
using TorneoGalactico.Api.Services.RankingServices;
using static TorneoGalactico.Api.DTOs.Dtos;

namespace TorneoGalactico.Api.Controllers
{
    [ApiController]
    [Route("api/ranking")]
    public class RankingController(IRankingService service): ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<RankingEntryResponse>> Get()
        {
            var ranking = await service.GetRankingAsync();
            return Ok(ranking);
        }
    }
}