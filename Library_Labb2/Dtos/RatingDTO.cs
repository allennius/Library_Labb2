using Library_Labb2.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace Library_Labb2.Dtos;

public static class RatingDTOExtensions
{
    public static RatingDTO ToDTO(this Rating source)
    {
        return new RatingDTO
        {
            RatingId = source.RatingId,
            Grade = source.Grade,
            Comment = source.Comment,
            BookId = source.Book.BookID,
            CustomerId = source.Customer?.CustomerId
        };
    }
}

public class RatingDTO
{
    public int RatingId { get; set; }

    [Range(0, 10)]
    public int Grade { get; set; }
    public string? Comment { get; set; }

    public int BookId { get; set; }
    public int? CustomerId { get; set; }
}
