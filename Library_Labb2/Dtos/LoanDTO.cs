using Library_Labb2.Models;

namespace Library_Labb2.Dtos;

public static class LoanDTOExtensions
{
    public static LoanDTO ToDTO(this Loan loan)
    {
        return new LoanDTO
        {
            LoanId = loan.LoanId,
            BookId = loan.Book.BookID,
            OrderId = loan.Order.OrderId
        };
    }
}

public class LoanDTO
{
    public int LoanId { get; set; }
    public int BookId { get; set; }
    public int OrderId { get; set; }
}
