using ChuWeb.API.Data;
using ChuWeb.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChuWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<TagsController> _logger;

        public TagsController(BlogDbContext context, ILogger<TagsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            return await _context.Tags
                .Where(t => !t.IsDeleted)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var tag = await _context.Tags
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        // GET: api/Tags/slug/react
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<Tag>> GetTagBySlug(string slug)
        {
            var tag = await _context.Tags
                .FirstOrDefaultAsync(t => t.Slug == slug && !t.IsDeleted);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        // GET: api/Tags/withPostCount
        [HttpGet("withPostCount")]
        public async Task<ActionResult<IEnumerable<object>>> GetTagsWithPostCount()
        {
            var tags = await _context.Tags
                .Where(t => !t.IsDeleted)
                .Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.Slug,
                    PostCount = t.Posts.Count(p => p.IsPublished && !p.IsDeleted)
                })
                .OrderBy(t => t.Name)
                .ToListAsync();

            return Ok(tags);
        }

        // POST: api/Tags
        [HttpPost]
        public async Task<ActionResult<Tag>> CreateTag(Tag tag)
        {
            // Check if a tag with the same slug already exists
            if (await _context.Tags.AnyAsync(t => t.Slug == tag.Slug && !t.IsDeleted))
            {
                return BadRequest("A tag with this slug already exists.");
            }
            
            tag.CreatedAt = DateTime.UtcNow;
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
        }

        // PUT: api/Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, Tag tag)
        {
            if (id != tag.Id)
            {
                return BadRequest();
            }

            // Check if another tag with the updated slug already exists
            if (await _context.Tags.AnyAsync(t => t.Slug == tag.Slug && t.Id != id && !t.IsDeleted))
            {
                return BadRequest("Another tag with this slug already exists.");
            }

            tag.UpdatedAt = DateTime.UtcNow;
            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            // Soft delete
            tag.IsDeleted = true;
            tag.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
} 