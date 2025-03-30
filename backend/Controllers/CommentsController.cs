using ChuWeb.API.Data;
using ChuWeb.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChuWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(BlogDbContext context, ILogger<CommentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments
                .Where(c => c.IsApproved && !c.IsDeleted)
                .ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments
                .Include(c => c.Replies.Where(r => r.IsApproved && !r.IsDeleted))
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // GET: api/Comments/post/5
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByPost(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            
            if (post == null)
            {
                return NotFound();
            }
            
            return await _context.Comments
                .Where(c => c.PostId == postId && c.ParentCommentId == null && c.IsApproved && !c.IsDeleted)
                .Include(c => c.Replies.Where(r => r.IsApproved && !r.IsDeleted))
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment(Comment comment)
        {
            // Validate the post exists
            var post = await _context.Posts.FindAsync(comment.PostId);
            if (post == null)
            {
                return BadRequest("Invalid Post ID.");
            }

            // If it's a reply, validate the parent comment exists
            if (comment.ParentCommentId.HasValue)
            {
                var parentComment = await _context.Comments.FindAsync(comment.ParentCommentId.Value);
                if (parentComment == null)
                {
                    return BadRequest("Invalid Parent Comment ID.");
                }
            }

            comment.CreatedAt = DateTime.UtcNow;
            // By default, comments might need approval (can be auto-approved in a real app with auth)
            comment.IsApproved = false;
            
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            comment.UpdatedAt = DateTime.UtcNow;
            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            // Soft delete
            comment.IsDeleted = true;
            comment.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Comments/5/approve
        [HttpPatch("{id}/approve")]
        public async Task<IActionResult> ApproveComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            
            if (comment == null)
            {
                return NotFound();
            }

            comment.IsApproved = true;
            comment.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
} 