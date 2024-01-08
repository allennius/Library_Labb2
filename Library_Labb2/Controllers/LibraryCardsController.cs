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

        // GET: api/LibraryCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryCard>> GetLibraryCard(int id)
        {
            var libraryCard = await _context.libraryCards.FindAsync(id);

            if (libraryCard == null)
            {
                return NotFound();
            }

            return libraryCard;
        }

        // PUT: api/LibraryCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibraryCard(int id, LibraryCardDTO libraryCardDto)
        {
            if (id != libraryCardDto.LibraryCardId)
            {
                return BadRequest();
            }

            _context.Entry(libraryCardDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryCardExists(id))
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

        // POST: api/LibraryCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LibraryCard>> PostLibraryCard(LibraryCardDTO libraryCard)
        {
            await _context.libraryCards.AddAsync(new LibraryCard { CustomerId = 1});
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibraryCard", new { id = libraryCard.LibraryCardId }, libraryCard);
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

        private bool LibraryCardExists(int id)
        {
            return _context.libraryCards.Any(e => e.LibraryCardId == id);
        }
    }
}
