using Microsoft.AspNetCore.Mvc;
using TorneoGalactico.Api.Services.BattleServices;
using static TorneoGalactico.Api.DTOs.Dtos;

namespace TorneoGalactico.Api.Controllers
{
    [ApiController]
    [Route("api/battles")]
    public class BattleController(IBattleServices service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<BattleResponse>>> GetAll()
        {
            var combats = await service.GetAllCombatsAsync();
            return Ok(combats);
        }

        [HttpPost]
        public async Task<ActionResult<CreateBattleRequest>> Create(CreateBattleRequest request)
        {
            var created = await service.CombatAsync(request.SpeciesAId, request.SpeciesBId);
            return Created(string.Empty, created);
        }

        [HttpPost("random")]
        public async Task<ActionResult<BattleResponse>> CombatRandom()
        {
            var randomCombat = await service.CombatRandomAsync();
            return Ok(randomCombat);
        }
    }
}
