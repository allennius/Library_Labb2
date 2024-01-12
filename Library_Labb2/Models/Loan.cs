namespace Library_Labb2.Models;

public class Loan
{
    public int LoanId { get; set; }
    public Book Book { get; set; }
    public Order Order { get; set; }
}