using Library_Labb2.Models;

namespace Library_Labb2.Dtos;

public static class CustomerDTOExtensions
{
    public static CustomerDTO ToDTO(this Customer source)
    {

        var customer = new CustomerDTO
        {
            CustomerId = source.CustomerId,
            FirstName = source.FirstName,
            LastName = source.LastName,
            LibraryCard = source.LibCard?.ToDTO()
        };

        return customer;
    }
}

public class CustomerDTO
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public LibraryCardDTO? LibraryCard { get; set; }
}
