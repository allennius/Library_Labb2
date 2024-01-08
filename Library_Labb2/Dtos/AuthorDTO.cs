using Library_Labb2.Models;

namespace Library_Labb2.Dtos;

public static class AuthorDTOExtenstions
{
    public static AuthorDTO ToDTO(this Author source)
    {
        return new AuthorDTO
        {
            AuthorID = source.AuthorID,
            FirstName = source.FirstName,
            LastName = source.LastName
        };
    }
}
public class AuthorDTO
{
    public int AuthorID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
