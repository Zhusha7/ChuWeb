using ChuWeb.API.Data;
using ChuWeb.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChuWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<ContactController> _logger;

        public ContactController(BlogDbContext context, ILogger<ContactController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Contact (Admin only in a real app with auth)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactMessage>>> GetContactMessages()
        {
            return await _context.ContactMessages
                .Where(m => !m.IsDeleted)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        // GET: api/Contact/5 (Admin only in a real app with auth)
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactMessage>> GetContactMessage(int id)
        {
            var contactMessage = await _context.ContactMessages
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);

            if (contactMessage == null)
            {
                return NotFound();
            }

            return contactMessage;
        }

        // GET: api/Contact/unread (Admin only in a real app with auth)
        [HttpGet("unread")]
        public async Task<ActionResult<IEnumerable<ContactMessage>>> GetUnreadContactMessages()
        {
            return await _context.ContactMessages
                .Where(m => !m.IsRead && !m.IsDeleted)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        // POST: api/Contact
        [HttpPost]
        public async Task<ActionResult<ContactMessage>> CreateContactMessage(ContactMessage contactMessage)
        {
            // Set default values
            contactMessage.CreatedAt = DateTime.UtcNow;
            contactMessage.IsRead = false;
            
            _context.ContactMessages.Add(contactMessage);
            await _context.SaveChangesAsync();

            // For security, we return only confirmation rather than the full message details
            return Ok(new { 
                message = "Thank you for your message. We will get back to you soon.",
                success = true,
                messageId = contactMessage.Id 
            });
        }

        // PUT: api/Contact/5/read (Admin only in a real app with auth)
        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var contactMessage = await _context.ContactMessages.FindAsync(id);
            
            if (contactMessage == null || contactMessage.IsDeleted)
            {
                return NotFound();
            }

            contactMessage.IsRead = true;
            contactMessage.ReadAt = DateTime.UtcNow;
            contactMessage.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Contact/5/unread (Admin only in a real app with auth)
        [HttpPut("{id}/unread")]
        public async Task<IActionResult> MarkAsUnread(int id)
        {
            var contactMessage = await _context.ContactMessages.FindAsync(id);
            
            if (contactMessage == null || contactMessage.IsDeleted)
            {
                return NotFound();
            }

            contactMessage.IsRead = false;
            contactMessage.ReadAt = null;
            contactMessage.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Contact/5 (Admin only in a real app with auth)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactMessage(int id)
        {
            var contactMessage = await _context.ContactMessages.FindAsync(id);
            if (contactMessage == null)
            {
                return NotFound();
            }

            // Soft delete
            contactMessage.IsDeleted = true;
            contactMessage.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
} 