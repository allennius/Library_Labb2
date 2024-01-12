using Library_Labb2.Controllers;
using Library_Labb2.Models;
using Microsoft.CodeAnalysis.CSharp;

namespace Library_Labb2.Dtos;


public static class BookDTOExtensions
{
    public static BookDTO ToDTO(this Book source)
    {
        var book = new BookDTO
        {
            BookID = source.BookID,
            Isbn = source.Isbn,
            Title = source.Title,
            ReleaseDate = source.ReleaseDate,
            Available = source.Available,
            Authors = new List<AuthorDTO>(),
            AvgRating = source.Ratings?.Average(r => r?.Grade)
        };
        if(source.Authors != null)
            book.Authors.AddRange(source.Authors.Select(x => x.ToDTO()));

        return book;
    }

    public static Book ToModel(this BookDTO source)
{
        var book = new Book
        {
            BookID = source.BookID,
            Isbn = source.Isbn,
            Title = source.Title,
            ReleaseDate = source.ReleaseDate,
            Available = source.Available
        };

        return book;
    }
}
public class BookDTO
{
    public int BookID { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public bool Available { get; set; } = true;
    public List<AuthorDTO>? Authors { get; set; }
    public double? AvgRating { get; set; }
}

