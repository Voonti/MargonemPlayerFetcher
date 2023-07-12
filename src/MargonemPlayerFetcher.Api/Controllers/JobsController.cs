using Hangfire;
using MargoFetcher.Application.Jobs;
using MargoFetcher.Application.Jobs.Commands;
using MargoFetcher.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MargoFetcher.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMediator _mediator;

        public JobsController(
            IBackgroundJobClient backgroundJobClient,
            IMediator mediator)
        {
            _backgroundJobClient = backgroundJobClient;
            _mediator = mediator;

        }

        [HttpPost("EqSync")]
        public async Task<IActionResult> ExecuteEqSync()
        {
            await  _mediator.Send(new SyncEqCommand());
            return Ok();
        }

        [HttpPost("PlayerSync")]
        public async Task<IActionResult> ExecutePlayerSync()
        {
            await _mediator.Send(new SyncPlayerCommand());
            return Ok();
        }
    }
}
