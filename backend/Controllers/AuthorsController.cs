using ChuWeb.API.Data;
using ChuWeb.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChuWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(BlogDbContext context, ILogger<AuthorsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // GET: api/Authors/username/johndoe
        [HttpGet("username/{username}")]
        public async Task<ActionResult<Author>> GetAuthorByUsername(string username)
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(a => a.Username == username && !a.IsDeleted);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // GET: api/Authors/5/posts
        [HttpGet("{id}/posts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAuthorPosts(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            
            if (author == null || author.IsDeleted)
            {
                return NotFound();
            }
            
            var posts = await _context.Posts
                .Where(p => p.AuthorId == id && p.IsPublished && !p.IsDeleted)
                .OrderByDescending(p => p.PublishedAt)
                .ToListAsync();
                
            return posts;
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            // Check if an author with the same email or username already exists
            if (await _context.Authors.AnyAsync(a => a.Email == author.Email && !a.IsDeleted))
            {
                return BadRequest("An author with this email already exists.");
            }
            
            if (!string.IsNullOrEmpty(author.Username) && await _context.Authors.AnyAsync(a => a.Username == author.Username && !a.IsDeleted))
            {
                return BadRequest("An author with this username already exists.");
            }
            
            author.CreatedAt = DateTime.UtcNow;
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            // Check if another author with the updated email or username already exists
            if (await _context.Authors.AnyAsync(a => a.Email == author.Email && a.Id != id && !a.IsDeleted))
            {
                return BadRequest("Another author with this email already exists.");
            }
            
            if (!string.IsNullOrEmpty(author.Username) && await _context.Authors.AnyAsync(a => a.Username == author.Username && a.Id != id && !a.IsDeleted))
            {
                return BadRequest("Another author with this username already exists.");
            }

            author.UpdatedAt = DateTime.UtcNow;
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            // Soft delete
            author.IsDeleted = true;
            author.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
} 