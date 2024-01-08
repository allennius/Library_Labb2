namespace Library_Labb2.Models;

public class LibraryCard
{
    public int LibraryCardId { get; set; }
    public int CustomerId { get; set; }
    public ICollection<Loan>? Loans { get; set; }
}
