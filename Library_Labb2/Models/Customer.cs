namespace Library_Labb2.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public LibraryCard? LibCard { get; set; }
}
