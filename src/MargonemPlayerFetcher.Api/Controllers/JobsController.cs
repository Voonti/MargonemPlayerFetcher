using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MargoFetcher.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ExecuteSync()
        {
            return Ok();
        }

    }
}
