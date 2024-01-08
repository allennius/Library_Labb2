//using FakeItEasy;
////using FakeItEasy.Sdk;
//using Library_Labb2.Controllers;
//using Library_Labb2.Data;
//using Library_Labb2.Dtos;
//using Library_Labb2.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
//using Moq;

//namespace LibraryLabb2.Tests;

//public class AuthorsControllerTests
//{
//    [Fact]
//    public async void GetAllAuthors_And_AuthorByID()
//    {

//        //Arrange
//        int authorCount = 5;
//        var fakeAuthors = A.CollectionOfDummy<AuthorDTO>(authorCount).AsEnumerable();
//        var authorID = fakeAuthors.First().AuthorID;
//        var dataStore = A.Fake<ILibraryDataStore>();
//        A.CallTo(() => dataStore.GetAllAuthors()).Returns(Task.FromResult(fakeAuthors));
//        var controller = new AuthorsController(dataStore);

//        //Act
//        var actionResult = await controller.GetAuthors();
//        var singleActionResult = await controller.GetAuthor(authorID);

//        //Assert
//        var result = actionResult.Result as OkObjectResult;
//        var returnAuthors = result?.Value as IEnumerable<AuthorDTO>;
//        Assert.Equal(authorCount, returnAuthors?.Count());

//        var singleResult = singleActionResult.Result as OkObjectResult;
//        var returnAuthor = singleResult?.Value as AuthorDTO;
//        Assert.Equal(returnAuthor?.AuthorID, authorID);
//    }

//    [Fact]
//    public async void GetAllAuthors()
//    {
//        //Arrange
//        var mockedDbContext = Mock.MockedDbContextFor<LibraryDbContext>();
//    }
//}