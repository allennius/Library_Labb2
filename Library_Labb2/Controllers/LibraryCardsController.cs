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
    public class LibraryCardsController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public LibraryCardsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/LibraryCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryCard>>> GetlibraryCards()
        {
            return await _context.libraryCards.ToListAsync();
        }

        // POST: api/LibraryCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LibraryCardDTO>> PostLibraryCard(LibraryCardDTO libraryCard)
        {
            if (!_context.Customers.Any(c => c.CustomerId == libraryCard.CustomerId))
                return BadRequest();

            var oldLibraryCard = await _context.libraryCards.FirstOrDefaultAsync(l => l.CustomerId == libraryCard.CustomerId);

            if (oldLibraryCard != null) 
                _context.libraryCards.Remove(oldLibraryCard);

            await _context.libraryCards.AddAsync(new LibraryCard { CustomerId = libraryCard.CustomerId });

            await _context.SaveChangesAsync();

            return Ok(libraryCard);
        }

        // DELETE: api/LibraryCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibraryCard(int id)
        {
            var libraryCard = await _context.libraryCards.FindAsync(id);
            if (libraryCard == null)
            {
                return NotFound();
            }

            _context.libraryCards.Remove(libraryCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
