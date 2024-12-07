using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;
using PortfolioService.DTOs.Post;
using PortfolioService.Filters;
using PortfolioService.Models;

namespace PortfolioService.Controllers;

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

    #region GET

    [HttpGet("getAllPosts")]
    public async Task<IActionResult> GetAllPosts()
    {
        bool pinned = Request.Query.ContainsKey("pinned");
        bool filterVisible = Request.Query.ContainsKey("filter_visible");

        try
        {
            var posts = await _context.Posts
                .Where(post => !(post.IsHidden && !post.IsDraft))
                .Where(post => pinned ? post.IsPinned : true)
                .Where(post => filterVisible ? !post.IsHidden : true)
                .ToListAsync();

            var postDtos = _mapper.Map<IEnumerable<PostListingDto>>(posts);

            return Ok(new { posts = postDtos, count = postDtos.Count(), message = "Posts fetched successfully." });
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

    [HttpGet("getPostById")]
    public async Task<IActionResult> GetPostById([FromQuery(Name = "post_id")] string postId)
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

            _ = Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromMinutes(10));
                post.Views += 1;
                await _context.SaveChangesAsync();
            });

            var postDto = _mapper.Map<PostDto>(post);
            return Ok(new { post = postDto, message = "Post fetched successfully." });
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

    [HttpGet("getPostByUrlString")]
    public async Task<IActionResult> GetPostByUrlString([FromQuery(Name = "url_string")] string urlString)
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

            _ = Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromMinutes(10));
                post.Views += 1;
                await _context.SaveChangesAsync();
            });

            var postDto = _mapper.Map<PostDto>(post);
            return Ok(new { post = postDto, message = "Post fetched successfully." });
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

    #endregion

    #region POST

    [LocalOnly]
    [HttpPost("createPost")]
    public async Task<IActionResult> CreatePost([FromBody] PostCreateDto postCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { message = "Invalid model." });
        }

        try
        {
            var post = _mapper.Map<Post>(postCreateDto);
            post.PostId = Guid.NewGuid().ToString();
            post.CreatedAt = DateTime.Now;

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Post created successfully." });
        }
        catch (Exception e)
        {
            dynamic response = new
            {
                message = "An error occurred while creating the post.",
            };

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                response = new { response.message, error = e.Message };
            }

            return StatusCode(500, response);
        }
    }

    #endregion

    #region PUT

    [LocalOnly]
    [HttpPut("updatePost")]
    public async Task<IActionResult> UpdatePost([FromBody] PostUpdateDto postUpdateDto, [FromQuery(Name = "post_id")] string postId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { message = "Invalid model." });
        }

        try
        {
            var post = await _context.Posts
                .Where(post => post.PostId == postId)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                return NotFound(new { message = "Post not found." });
            }

            post = _mapper.Map(postUpdateDto, post);

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Post updated successfully." });
        }
        catch (Exception e)
        {
            dynamic response = new
            {
                message = "An error occurred while updating the post.",
            };

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                response = new { response.message, error = e.Message };
            }

            return StatusCode(500, response);
        }
    }

    #endregion

    #region DELETE

    [LocalOnly]
    [HttpDelete("deletePost")]
    public async Task<IActionResult> DeletePost([FromQuery(Name = "post_id")] string postId)
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

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Post deleted successfully." });
        }
        catch (Exception e)
        {
            dynamic response = new
            {
                message = "An error occurred while deleting the post.",
            };

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                response = new { response.message, error = e.Message };
            }

            return StatusCode(500, response);
        }
    }

    #endregion
}