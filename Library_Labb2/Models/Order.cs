namespace Library_Labb2.Models;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public ICollection<Loan> Loans { get; set; }
    public LibraryCard LibraryCard { get; set; }
}
