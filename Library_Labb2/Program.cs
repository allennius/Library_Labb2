using Library_Labb2.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    //AzureSqlServer
    //var connectionString = builder.Configuration.GetConnectionString("LibraryAzureDB");
    //var connBuilder = new SqlConnectionStringBuilder(connectionString)
    //{
    //    Password = builder.Configuration["DbPassword"]
    //};
    //connectionString = connBuilder.ConnectionString;
    //options.UseSqlServer(connectionString);

    //SqlServer Local
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibrarySqlServer"));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//TODO

// Create Order so you can handle multiple book loans in one call
// .. Change loan to bookLoan and have A list of bookloans in one order?
// .. Move loanCard To order from loan?
// Add Ebook bool to book model?

//-Skapa en författare  DONE
//- Skapa en bok        DONE
//- Skapa en ny låntagare  DONE
//- Lista alla böcker    DONE
//- Hämta information om en specifik bok   DONE
//- Hyra en bok           DONE
//- Lämna tillbaka en bok DONE
//- Ta bort låntagare     DONE
//- Ta bort böcker        DONE
//- Ta bort författare    DONE

// Clean up API, Name methods and remove unnecessary functionality
// rename endpoints?

//- Skapa en författare
//- Ta bort författare
//- Skapa en bok
//- Lista alla böcker
//- Hämta information om en specifik bok
//- Ta bort böcker
//- Skapa en ny låntagare
//- Ta bort låntagare
//- Hyra en bok
//- Lämna tillbaka en bok