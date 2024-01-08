namespace Library_Labb2.Models;

public class Loan
{
    public int LoanId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public Book Book { get; set; }
    public LibraryCard LibCard { get; set; }
}
