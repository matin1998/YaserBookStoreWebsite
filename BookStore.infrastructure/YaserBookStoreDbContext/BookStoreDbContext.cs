using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.infrastructure.YaserBookStoreDbContext;

public class BookStoreDbContext: IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
{
    #region Ctor
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-9EJGF76;Initial Catalog=BookStore;Integrated Security=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        base.OnConfiguring(optionsBuilder);
    }
    #endregion

    #region Db Sets

    public DbSet<Book> Books { get; set; }

    public DbSet<Stationary> Stationaries { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Address> Addresses { get; set; }

    #endregion
}
