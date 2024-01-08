using Library_Labb2.Controllers;
using Library_Labb2.Dtos;
using Library_Labb2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLabb2.Tests;
public class BookControllerTests
{

    [Fact]
    public async Task CreateBook_And_Check_If_Created()
    {
        //Arrange - Create in memory database
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "LibraryDb")
            .Options;

        //Act - Use a  context instance with data to run test
        using (var context = new LibraryDbContext(options))
        {
            BooksController controller = new BooksController(context);
            
            var actionResult = await controller.PostBook(new BookDTO { Isbn = "2123-ads", Title = "BookOne", ReleaseDate = new DateTime(1990, 04, 02), 
                Authors = new List<AuthorDTO> { new AuthorDTO { FirstName = "Holge", LastName = "Persson" } } });


            var result = actionResult.Result as CreatedAtActionResult;

            var bookId = context.Books.Last().BookID;

            Assert.True(context.Books.Any(a => a.BookID == bookId));
            Assert.NotNull(result);
        }
    }

    [Fact]
    public async Task GetAllBooks_And_Check_Correct_Count_GetSingleBook_And_Verify()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "libraryDb")
            .Options;

        //Act
        using (var context = new LibraryDbContext(options))
        {
            BooksController controller = new BooksController(context);

            int bookCount = 3;
            if (context.Books.Count() < 3)
            {
                for (int i = 0; i < bookCount; i++)
                {
                    context.Books.Add(new Book { Isbn = $"Isbn{Convert.ToChar(i+65)}", ReleaseDate = new DateTime(1990,02,02), 
                        Title = $"Title{Convert.ToChar(i+65)}", Authors = new List<Author> { new Author { FirstName = "He", LastName = "Be"} }
                    });
                }
                await context.SaveChangesAsync();

                bookCount = context.Books.ToList().DistinctBy(b => b.Isbn).Count();
                var controlBook = context.Books.Last();
          
                var actionResult = await controller.GetBooks();
                var result = actionResult.Result as OkObjectResult;
                var books = result?.Value as List<BookDTO>;

                var singleActionResult = await controller.GetBook(controlBook.BookID);
                var singleResult = singleActionResult.Result as OkObjectResult;
                var book = singleResult?.Value as BookDTO;

                //Assert
                //Books
                Assert.True(context.Books.Any());
                Assert.Equal(bookCount, books?.Count());
                //Book
                Assert.Equal(controlBook.BookID, book.BookID);
                Assert.Equal(controlBook.Isbn, book.Isbn);
            }
        }
    }

    [Fact]
    public async Task DeleteBook_And_Validate_Removal()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "libraryDb")
            .Options;

        //Act
        using (var context = new LibraryDbContext(options)) 
        {
            BooksController controller = new BooksController(context);
            if (!context.Books.Any())
            {
                context.Books.Add(new Book { Isbn = "123-asd", ReleaseDate = new DateTime(1990, 02, 02), Title = "Bokkee", Authors = new List<Author> { new Author { FirstName = "He", LastName = "Be" } } });
                context.SaveChanges();               
            }

            var bookId = context.Books.First().BookID;
            var bookCount = context.Books.Count();

            await controller.DeleteBook(bookId);
            context.SaveChanges();


            //Assert
            Assert.True(!context.Books.Any(b => b.BookID == bookId));
            Assert.True(bookCount - 1 == context.Books.Count());
        }

    }
}
