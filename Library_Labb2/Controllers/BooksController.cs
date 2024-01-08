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
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _context.Books.Include(b => b.Ratings).Include(b => b.Authors).Select(b => b.ToDTO()).AsNoTracking().ToListAsync();
            return Ok(books.DistinctBy(b => b.Isbn).ToList());
            //return await _context.Books.AsNoTracking().Select(b => b.ToDTO()).AsEnumerable().DistinctBy(b => b.Isbn).Include(b => b.Authors).ToListAsync();

            //return _context.Books.AsNoTracking().Include(b => b.Authors).Select(b => b.ToDTO()).ToList();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _context.Books.Include(b => b.Ratings).Include(b => b.Authors).AsNoTracking().FirstOrDefaultAsync(b => b.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            var bookDto = book.ToDTO();

            return Ok(bookDto);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDTO book)
        {
            if (book.BookID != id)
            {
                return BadRequest();
            }

            var newBook = book.ToModel();
            List<Book> books = await _context.Books.Include(b => b.Authors).Where(b => b.Isbn == book.Isbn).ToListAsync();
            //var oldBook = _context.Books.Include(b => b.Authors).First(b => b.BookID == id);
            var oldBook = books.FirstOrDefault();

            newBook.Authors = new List<Author>();

            //adding all authors  to list of authors
            foreach (var author in book.Authors)
            {
                var newAuthor = await _context.Authors.FindAsync(author.AuthorID);
                if (newAuthor != null)
                        newBook.Authors.Add(newAuthor);
            }

            books.ForEach(b =>
            {
                b.Authors = newBook.Authors;
                b.Isbn = newBook.Isbn;
                b.Title = newBook.Title;
                b.ReleaseDate = newBook.ReleaseDate;
                b.Available = newBook.Available;
                b.Authors = newBook.Authors;

                _context.Entry(b).State = EntityState.Modified;
            });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookDTO book)
        {
            Book newBook = new Book{ Isbn = book.Isbn, Title = book.Title, ReleaseDate = book.ReleaseDate.Date, Available = book.Available };
            newBook.Authors = new List<Author>();
            foreach (var authorId in book.Authors)
            {
                var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorID == authorId.AuthorID);
                if(author != null)
                    newBook.Authors.Add(author);
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookID }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}
