using Library_Labb2.Models;

namespace Library_Labb2.Dtos;

public static class LoanDTOExtensions
{
    public static LoanDTO ToDTO(this Loan loan)
    {
        return new LoanDTO
        {
            LoanId = loan.LoanId,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate,
            BookId = loan.Book.BookID,
            LibCardId = loan.LibCard.LibraryCardId
        };
    }
}

public class LoanDTO
{
    public int LoanId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public int BookId { get; set; }
    public int LibCardId { get; set; }
}
