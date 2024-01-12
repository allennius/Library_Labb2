using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Library_Labb2.Models;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> opt) : base(opt) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Rating> Rating { get; set; }
    public DbSet<LibraryCard> libraryCards { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Property(b => b.ReleaseDate).HasColumnType("date");
        modelBuilder.Entity<Order>().Property(o => o.OrderDate).HasDefaultValue(DateTime.Now);
    }
}
