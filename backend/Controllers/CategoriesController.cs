using ChuWeb.API.Data;
using ChuWeb.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChuWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(BlogDbContext context, ILogger<CategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // GET: api/Categories/slug/aspnet-core
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<Category>> GetCategoryBySlug(string slug)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Slug == slug && !c.IsDeleted);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }
        
        // GET: api/Categories/withPostCount
        [HttpGet("withPostCount")]
        public async Task<ActionResult<IEnumerable<object>>> GetCategoriesWithPostCount()
        {
            var categories = await _context.Categories
                .Where(c => !c.IsDeleted)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Slug,
                    c.Description,
                    PostCount = c.Posts.Count(p => p.IsPublished && !p.IsDeleted)
                })
                .OrderBy(c => c.Name)
                .ToListAsync();

            return Ok(categories);
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            // Check if a category with the same slug already exists
            if (await _context.Categories.AnyAsync(c => c.Slug == category.Slug && !c.IsDeleted))
            {
                return BadRequest("A category with this slug already exists.");
            }
            
            category.CreatedAt = DateTime.UtcNow;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            // Check if another category with the updated slug already exists
            if (await _context.Categories.AnyAsync(c => c.Slug == category.Slug && c.Id != id && !c.IsDeleted))
            {
                return BadRequest("Another category with this slug already exists.");
            }

            category.UpdatedAt = DateTime.UtcNow;
            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Soft delete
            category.IsDeleted = true;
            category.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
} 