using Library_Labb2.Controllers;
using Library_Labb2.Dtos;
using Library_Labb2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLabb2.Tests;
public class CustomerTests
{

    [Fact]
    public async Task Add_Customer_And_Validate()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "libraryDb")
            .Options;

        //Act
        using (var context = new LibraryDbContext(options))
        {
            CustomersController controller = new CustomersController(context);

            var customerCount = context.Customers.Count(); 

            var actionResult = await controller.PostCustomer(new CustomerDTO { CustomerId = 1, FirstName = "Holge", LastName = "Persson" });


            //Assert
            var result = actionResult.Result as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.True(context.Customers.Any());
            Assert.True(context.Customers.Last().LastName == "Persson");
            Assert.True(context.Customers.Last().FirstName == "Holge");
            Assert.Equal(customerCount + 1, context.Customers.Count());

        }
    }

    [Fact]
    public async Task Delete_Customer_And_Validate()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "LibraryDb")
            .Options;

        //Act
        using(var context = new LibraryDbContext(options))
        {
            CustomersController Controller = new CustomersController(context);

            if (!context.Customers.Any()) 
            {
                context.Add(new Customer { FirstName = "holge", LastName = "Persson" });
                context.SaveChanges();
            }

            var customerCount = context.Customers.Count();
            var customerId = context.Customers.First().CustomerId;

            await Controller.DeleteCustomer(customerId);

            //Assert
            Assert.True(!context.Customers.Any(c => c.CustomerId == customerId));
            Assert.Equal(customerCount - 1, context.Customers.Count());
        }
    }
}
