using Library_Labb2.Models;

namespace Library_Labb2.Dtos;


public static class LibraryCardDTOExtensions
{
    public static LibraryCardDTO ToDTO(this LibraryCard libraryCard)
    {
        return new LibraryCardDTO
        {
            LibraryCardId = libraryCard.LibraryCardId,
            CustomerId = libraryCard.CustomerId
        };
    }
}

public class LibraryCardDTO
{
    public int LibraryCardId { get; set; }
    public int CustomerId { get; set; }
}
