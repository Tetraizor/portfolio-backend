using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;
using PortfolioService.DTOs.Featured;

namespace PortfolioService.Controllers;

[Route("api/[controller]")]
public class FeaturedController : Controller
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public FeaturedController(BlogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("getAllFeaturedItems")]
    public async Task<IActionResult> GetFeaturedItems()
    {
        var featuredItems = await _context.FeaturedItems.ToListAsync();
        var featuredItemDtos = _mapper.Map<IEnumerable<FeaturedItemDto>>(featuredItems);

        return Ok(new { featuredItems = featuredItemDtos, count = featuredItems.Count, message = "Featured items fetched successfully." });
    }
}