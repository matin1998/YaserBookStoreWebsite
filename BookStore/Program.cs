using BookStore.Application.Services.Implementations;
using BookStore.Domain.RepositoryInterfaces;
using BookStore.infrastructure.Repositories;
using BookStore.Application.Services.Interfaces;
using BookStore.infrastructure.YaserBookStoreDbContext;
using Microsoft.OpenApi.Models;
using BookStore.infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using BookStore.Domain.Entities;
using BookStore.infrastructure.Identity;
using BookStore.Infrastructure.Identity;
using BookStore.Domain.UnitOfWork;
using BookStore.infrastructure.UnitOfWork;
namespace BookStore.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        /*BookStoreDbContext bookStoreDbContext = new BookStoreDbContext();
        IBookRepository bookRepository = new BookRepository(bookStoreDbContext);
        ICategoryService bookService = new BookService(bookRepository);
        Book book = new Book();
        book.BookTitle = "matin";
        book.BookPrice = 200000;
        book.BookDescription = "sdvcfdv";
        bookService.AddCategoryToDataBase(book);*/
        var builder = WebApplication.CreateBuilder(args);
        // Configure Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole<long>>(
            options =>
            {
                // Configure identity options here if needed
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            }

            ).AddEntityFrameworkStores<BookStoreDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
        CustomClaimsPrincipalFactory>();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";

            options.AccessDeniedPath = "/Account/AccessDenied";
        });
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IImageRepository, ImageRepository>();
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<IImageService, ImageService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IAddressRepository, AddressRepository>();
        builder.Services.AddScoped<IAddressService, AddressService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddDbContext<BookStoreDbContext>();
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var roleManager =
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();

            IdentitySeeder.SeedRolesAsync(roleManager).GetAwaiter().GetResult();
        }
        /*builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; 
            });
        }*/
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
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapControllerRoute(
            name: "area",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
