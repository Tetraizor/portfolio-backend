using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;

namespace PortfolioService.Controllers;

[Route("api/[controller]")]
public class FeaturedController : Controller
{
    private readonly BlogDbContext _context;

    public FeaturedController(BlogDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetFeaturedItems()
    {
        var posts = await _context.FeaturedItems.ToListAsync();

        return Ok(posts);
    }
}