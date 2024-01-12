using Library_Labb2.Controllers;
using Library_Labb2.Dtos;
using Library_Labb2.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLabb2.Tests;
public class AuthorControllerTests
{

    [Fact]
    public async Task AddAuthor_And_Validate()
    {
        //Arrange - Create in memory database
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "LibraryDb")
            .Options;

        //Act - Use a  context instance with data to run test
        using (var context = new LibraryDbContext(options))
        {
            AuthorsController controller = new AuthorsController(context);

            var actionResult = await controller.PostAuthor(new Author { FirstName = "Holge", LastName = "Persson" });

            var result = actionResult.Result as CreatedAtActionResult;

            var authorId = context.Authors.Last().AuthorID;

            Assert.True(context.Authors.Any(a => a.AuthorID == authorId));
            Assert.NotNull(result);
            Assert.NotEmpty(context.Authors);                 
        }

    }

    [Fact]
    public async Task GetAllAuthors_SingleAutorById_And_Validate()
    {

        //Arrange - Create in memory database
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "LibraryDb")
            .Options;

        //Create mocked context by seeding data
        int authorCount = 3;

        using (var context = new LibraryDbContext(options))
        {
            if (!context.Authors.Any())
            {
                for (int i = 0; i < authorCount; i++)
                {
                    context.Authors.Add(new Author { FirstName = $"F{Convert.ToChar(i + 65)}", LastName = $"L{Convert.ToChar(i + 65)}" });
                }
                context.SaveChanges();
            }
        }

        //Act - Use a context instance with data to run test
        using (var context = new LibraryDbContext(options))
        {
            authorCount = context.Authors.Count();
            AuthorsController controller = new AuthorsController(context);
            var actionResult = await controller.GetAuthors();


            //Assert - Authors
            var result = actionResult.Result as OkObjectResult;
            var authors = result.Value as List<AuthorDTO>;
            Assert.Equal(authorCount, authors.Count());

            var controlAuthor = context.Authors.First();
            var singleActionResult = await controller.GetAuthor(authors.First().AuthorID);
            var singleResult = singleActionResult.Result as OkObjectResult;
            var author = singleResult.Value as Author;

            Assert.NotNull(author);
            Assert.Equal(author.AuthorID, authors.First().AuthorID);
            Assert.Equal(controlAuthor.FirstName, author.FirstName);
            Assert.Equal(controlAuthor.LastName, author.LastName);
        }

    }

    [Fact]
    public async Task DeleteAuthor_And_Validate()
    {
        //Arrange - Create in memory database
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "LibraryDb")
            .Options;

        //Create mocked context by seeding data
        using (var context = new LibraryDbContext(options))
        {
            if (!context.Authors.Any())
            {
                context.Authors.Add(new Author { FirstName = "Holge", LastName = "Persson" });
                context.SaveChanges();
                Assert.True(context.Authors.Any());
            }
        }

        //Act
        using (var context = new LibraryDbContext(options))
        {
            AuthorsController controller = new AuthorsController(context);
            var authorId = context.Authors.First().AuthorID;
            var actionResult = await controller.DeleteAuthor(authorId);

            var result = actionResult as NoContentResult;

            //Assert
            Assert.True(!context.Authors.Any(a => a.AuthorID == authorId));
            Assert.Equal(204, result.StatusCode);

        }

    }
}
