using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;
using PortfolioService.DTOs.Post;

namespace PortfolioService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public BlogController(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var service = Request.Query["service"].ToString();
            if (string.IsNullOrEmpty(service))
            {
                return BadRequest(new { message = "Service parameter is required." });
            }

            switch (service)
            {
                case "getAllVisiblePostListings":
                    return await GetAllVisiblePostListings();
                case "getPostById":
                    var postId = Request.Query["post_id"].ToString();

                    if (string.IsNullOrEmpty(postId))
                    {
                        return BadRequest(new { message = "Post ID is required." });
                    }

                    return await GetPostById(postId);
                case "getPostByUrlString":
                    var urlString = Request.Query["url_string"].ToString();

                    if (string.IsNullOrEmpty(urlString))
                    {
                        return BadRequest(new { message = "URL string is required." });
                    }

                    return await GetPostByUrlString(urlString);
                default:
                    return BadRequest(new { message = "Invalid service parameter." });
            }
        }

        private async Task<IActionResult> GetAllVisiblePostListings()
        {
            try
            {
                var posts = await _context.Posts
                    .Where(post => !(post.IsHidden && !post.IsDraft))
                    .ToListAsync();

                var postDtos = _mapper.Map<IEnumerable<PostListingDto>>(posts);

                return Ok(postDtos);
            }
            catch (Exception e)
            {
                dynamic response = new
                {
                    message = "An error occurred while fetching posts.",
                };

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    response = new { response.message, error = e.Message };
                }

                return StatusCode(500, response);
            }
        }

        private async Task<IActionResult> GetPostById(string postId)
        {
            try
            {
                var post = await _context.Posts
                    .Where(post => post.PostId == postId)
                    .FirstOrDefaultAsync();

                if (post == null)
                {
                    return NotFound(new { message = "Post not found." });
                }

                var postDto = _mapper.Map<PostDto>(post);

                return Ok(postDto);
            }
            catch (Exception e)
            {
                dynamic response = new
                {
                    message = "An error occurred while fetching the post.",
                };

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    response = new { response.message, error = e.Message };
                }

                return StatusCode(500, response);
            }
        }

        private async Task<IActionResult> GetPostByUrlString(string urlString)
        {
            try
            {
                var post = await _context.Posts
                    .Where(post => post.UrlString == urlString)
                    .FirstOrDefaultAsync();

                if (post == null)
                {
                    return NotFound(new { message = "Post not found." });
                }

                var postDto = _mapper.Map<PostDto>(post);

                return Ok(postDto);
            }
            catch (Exception e)
            {
                dynamic response = new
                {
                    message = "An error occurred while fetching the post.",
                };

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    response = new { response.message, error = e.Message };
                }

                return StatusCode(500, response);
            }
        }
    }
}