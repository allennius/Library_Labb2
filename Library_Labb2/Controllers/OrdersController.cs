using Library_Labb2.Dtos;
using Library_Labb2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace Library_Labb2.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{

    private readonly LibraryDbContext _context;

    public OrdersController(LibraryDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<Order>> GetAllOrders()
    {
        return Ok(await _context.Orders.Include(o => o.Loans).ThenInclude(l => l.Book).Include(o => o.LibraryCard).Select(o => o.ToDto()).AsNoTracking().ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> PostOrder(OrderDto orderDto)
    {

        if (orderDto.Loans.Count() == 0) return BadRequest();

        var libCard = await _context.libraryCards.FindAsync(orderDto.LibCard.LibraryCardId);
        if (libCard == null) return BadRequest();

        string message = "";
        Order newOrder = new Order { OrderDate = DateTime.Now, LibraryCard = libCard, Loans = new List<Loan>() };
        newOrder.Loans = new List<Loan>();

        foreach (var loan in orderDto.Loans)
        {
            var book = await _context.Books.FindAsync(loan.BookId);
            if (book == null || !book.Available)
            {
                message = "One or more books was not available";
                continue;
            }
            _context.Loans.Add(new Loan { Book = book, Order = newOrder });
            book.Available = false;
        }

        if (newOrder.Loans.Count == 0) return NotFound(message);

        _context.Orders.Add(newOrder);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Ok(newOrder.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ReturnOrder(int id)
    {
        var orderDto = _context.Orders.Include(o => o.Loans).ThenInclude(o => o.Book).FirstAsync(o => o.OrderId == id).Result.ToDto();
        var order = await _context.Orders.FirstAsync(o => o.OrderId == id);

        if (order == null) return BadRequest();
        if (order.Loans.Count == 0)
        {
            order.ReturnDate = DateTime.Now;
            return NoContent();
        }
        if (order.ReturnDate == null)
            order.ReturnDate = DateTime.Now;

        var bookIds = orderDto.Loans.Select(l => l.BookId).ToList();

        foreach (var bookId in bookIds) 
        {
            var book = _context.Books.Find(bookId);
            if (book == null) continue;
            book.Available = true;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }
}
