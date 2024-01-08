using System.ComponentModel.DataAnnotations;

namespace Library_Labb2.Models;

public class Book
{
    public int BookID { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public bool Available { get; set; } = true;

    public ICollection<Author>? Authors { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    public ICollection<Loan>? Loans { get; set; }

}
