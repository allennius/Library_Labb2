using Library_Labb2.Controllers;
using Library_Labb2.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Packaging;

namespace Library_Labb2.Dtos;


public static class OrderDtoExtensions
{
    public static OrderDto? ToDto(this Order source)
    {
        var orderDto =  new OrderDto
        {
            OrderId = source.OrderId,
            OrderDate = source.OrderDate,
            ReturnDate = source.ReturnDate,
            LibCard = source.LibraryCard?.ToDTO(),
            Loans = new List<LoanDTO>()
        };

        orderDto.Loans.AddRange(source.Loans.Select(l => l.ToDTO()));

        return orderDto;     
    }
}

public class OrderDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? ReturnDate { get; set; }
    public ICollection<LoanDTO> Loans { get; set; }
    public LibraryCardDTO? LibCard { get; set; }
}
