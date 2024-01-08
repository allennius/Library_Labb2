using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library_Labb2.Models;
using Library_Labb2.Dtos;

namespace Library_Labb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public RatingsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDTO>>> GetRating()
        {
            return await _context.Rating.Include(r => r.Book).Include(r => r.Customer).Select(r => r.ToDTO()).ToListAsync();
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RatingDTO>> GetRating(int id)
        {
            var rating = await _context.Rating.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return rating.ToDTO();
        }

        // PUT: api/Ratings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, RatingDTO ratingDto)
        {
            if (id != ratingDto.RatingId)
            {
                return BadRequest();
            }

            var rating = new Rating { RatingId = id, Grade = ratingDto.Grade, Comment = ratingDto.Comment };
            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(RatingDTO rating)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == rating.BookId);
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == rating.CustomerId);

            if (book == null) { return NotFound(); }

            var newRating = new Rating { Grade = rating.Grade, Comment = rating.Comment, Book = book, Customer = customer };
            await _context.Rating.AddAsync(newRating);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRating", new { id = rating.RatingId }, rating);
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingExists(int id)
        {
            return _context.Rating.Any(e => e.RatingId == id);
        }
    }
}
