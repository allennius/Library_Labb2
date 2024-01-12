namespace Library_Labb2.Models;

public class LibraryCard
{
    public int LibraryCardId { get; set; }
    public int CustomerId { get; set; }
    public ICollection<Order>? Orders { get; set; }
}
