using Microsoft.AspNetCore.Mvc;

namespace PortfolioService.Controllers;

[ApiController]
public class CatchallController : ControllerBase
{
    public CatchallController() : base() { }

    [Route("{*url}")]
    public IActionResult Catchall(string url)
    {
        return NotFound(new { message = $"No endpoint found for \"{url}\"" });
    }
}