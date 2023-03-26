using MargoFetcher.Application.Players.Queries;
using MargonemPlayerFetcher.Application.Players.Commands;
using MargonemPlayerFetcher.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MargonemPlayerFetcherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IMediator _mediator;

        public PlayerController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> InsertPlayer([FromBody] PlayerDTO players)
        {
            var command = new InsertPlayerCommand(players);

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetAllPlayers()
        {
            var query = new GetPlayersQuery();

            return Ok(await _mediator.Send(query));
        }
    }
}
