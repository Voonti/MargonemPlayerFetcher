using MargonemPlayerFetcher.Application.Items.Queries;
using MargonemPlayerFetcher.Application.Items.Commands;
using MargonemPlayerFetcher.Domain.DTO;
using MargonemPlayerFetcher.Domain.Entities;
using MargonemPlayerFetcher.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MargonemPlayerFetcherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IMediator _mediator;

        public ItemController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{hid}")]
        public async Task<IActionResult> GetItems([FromRoute] string hid)
        {
            var query = new GetItemQuery(hid);

            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> InsertItems([FromBody] IEnumerable<ItemDTO> items)
        {
            var command = new InsertItemCommand(items);

            await _mediator.Send(command);

            return Ok();
        }
    }
}
