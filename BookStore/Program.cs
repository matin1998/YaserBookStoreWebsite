using BookStore.Application.Services.Implementations;
using BookStore.Domain.RepositoryInterfaces;
using BookStore.infrastructure.Repositories;
using BookStore.Application.Services.Interfaces;
using BookStore.infrastructure.YaserBookStoreDbContext;
using BookStore.Domain.Entities.Book;
using Microsoft.OpenApi.Models;
namespace BookStore.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        /*BookStoreDbContext bookStoreDbContext = new BookStoreDbContext();
        IBookRepository bookRepository = new BookRepository(bookStoreDbContext);
        IBookService bookService = new BookService(bookRepository);
        Book book = new Book();
        book.BookTitle = "matin";
        book.BookPrice = 200000;
        book.BookDescription = "sdvcfdv";
        bookService.AddCategoryToDataBase(book);*/
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        // Add services to the container.
        //builder.Services.AddControllersWithViews();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; 
            });
        }
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();
        /*app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");*/

        app.Run();
    }
}
