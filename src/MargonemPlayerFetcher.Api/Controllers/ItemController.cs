using MargoFetcher.Application.Items.Queries;
using MargoFetcher.Application.Items.Commands;
using MargoFetcher.Domain.DTO;
using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MargoFetcherApi.Controllers
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
        public async Task<IActionResult> GetItem([FromRoute] string hid)
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
