using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library_Labb2.Models;
using Library_Labb2.Dtos;
using NuGet.Protocol;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Library_Labb2.Controllers
{
    [Route("api/books/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public LoansController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanDTO>>> GetLoans()
        {
            return await _context.Loans.Include(l => l.Book).Include(l => l.LibCard).Select(l => l.ToDTO()).ToListAsync();
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDTO>> GetLoan(int id)
        {
            var loan = await _context.Loans.Include(l => l.Book).Include(l => l.LibCard).FirstOrDefaultAsync(l => l.LoanId == id);

            if (loan == null)
            {
                return NotFound();
            }

            var loanDto = loan.ToDTO();

            return loanDto;
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("return/{id}")]
        public async Task<IActionResult> ReturnLoan(int id)
        {
            var loan = await _context.Loans.Include(l => l.Book).FirstOrDefaultAsync(l => l.LoanId == id);
            if (loan == null) return NotFound();
            

            loan.ReturnDate = loan.ReturnDate ?? DateTime.Now;
            loan.Book.Available = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(id))
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

        // POST: api/Loans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(LoanDTO loanDto)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == loanDto.BookId);
            if (book == null) return NotFound();
            if (book.Available == false) return BadRequest();

            var libCard = await _context.libraryCards.FirstOrDefaultAsync(l => l.LibraryCardId == loanDto.LibCardId);
            if (libCard == null) return BadRequest();

            var newLoan = new Loan { LoanDate = DateTime.Now, Book = book, LibCard = libCard };

            book.Available = false;
            //_context.Entry(book).State = EntityState.Modified;
            await _context.Loans.AddAsync(newLoan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoan", new { id = loanDto.LoanId }, loanDto);
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.LoanId == id);
        }
    }
}
