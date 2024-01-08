using System.ComponentModel.DataAnnotations;

namespace Library_Labb2.Models;

public class Rating
{
    public int RatingId { get; set; }

    [Range(0, 10)]
    public int Grade { get; set; }
    public string? Comment { get; set; }

    public Book Book { get; set; }
    public Customer? Customer { get; set; }
}
