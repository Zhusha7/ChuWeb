using ChuWeb.API.Data;
using ChuWeb.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChuWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<PostsController> _logger;

        public PostsController(BlogDbContext context, ILogger<PostsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return await _context.Posts
                .Include(p => p.Author)
                .Where(p => p.IsPublished && !p.IsDeleted)
                .OrderByDescending(p => p.PublishedAt)
                .ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments.Where(c => c.IsApproved && !c.IsDeleted))
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // GET: api/Posts/slug/getting-started-with-aspnet-core-mvc
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<Post>> GetPostBySlug(string slug)
        {
            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments.Where(c => c.IsApproved && !c.IsDeleted))
                .FirstOrDefaultAsync(p => p.Slug == slug && p.IsPublished && !p.IsDeleted);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            post.CreatedAt = DateTime.UtcNow;
            
            if (post.IsPublished)
            {
                post.PublishedAt = DateTime.UtcNow;
            }
            
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            // If post is being published now
            if (post.IsPublished && !post.PublishedAt.HasValue)
            {
                post.PublishedAt = DateTime.UtcNow;
            }
            
            post.UpdatedAt = DateTime.UtcNow;
            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            // Soft delete
            post.IsDeleted = true;
            post.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        // GET: api/Posts/category/aspnet-core
        [HttpGet("category/{slug}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByCategory(string slug)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Slug == slug && !c.IsDeleted);
                
            if (category == null)
            {
                return NotFound();
            }
            
            var posts = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Categories)
                .Where(p => p.Categories.Any(c => c.Slug == slug) && p.IsPublished && !p.IsDeleted)
                .OrderByDescending(p => p.PublishedAt)
                .ToListAsync();
                
            return posts;
        }
        
        // GET: api/Posts/tag/react
        [HttpGet("tag/{slug}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByTag(string slug)
        {
            var tag = await _context.Tags
                .FirstOrDefaultAsync(t => t.Slug == slug && !t.IsDeleted);
                
            if (tag == null)
            {
                return NotFound();
            }
            
            var posts = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .Where(p => p.Tags.Any(t => t.Slug == slug) && p.IsPublished && !p.IsDeleted)
                .OrderByDescending(p => p.PublishedAt)
                .ToListAsync();
                
            return posts;
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
} 