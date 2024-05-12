using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Entities;
using Project.Infrastructure;
using Serilog;
using System.Reflection;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=MSSQLLocalDB;Initial Catalog=Housinddb;Integrated Security=True;")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    InitData.Initialize(services);
}

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

partial class Program
{
    //static void Main(string[] args)
    //{

    //    // Configuration du service pour DbContext
    //    var serviceProvider = new ServiceCollection()
    //        .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Data Source=MSSQLLocalDB;Initial Catalog=Housinddb;Integrated Security=True;"))
    //        .BuildServiceProvider();

    //    using (var dbContext = serviceProvider.GetService<ApplicationDbContext>())
    //    {
    //        // Vérifier si la base de données existe et la créer si nécessaire
    //        dbContext.Database.EnsureCreated();

    //        // Exemple : ajouter une entité à la base de données
    //        dbContext.Users.Add(
    //            new User
    //            {
    //                FirstName = "Marie",
    //                LastName = "Vouriot",
    //                UserName = "marie_v",
    //                Password = "mv123",
    //                IsOwner = true,
    //            }
    //        );
    //        dbContext.SaveChanges();

    //        Console.WriteLine("Base de données créée et initialisée.");
    //    }

    //    Console.ReadLine();
    //}
}