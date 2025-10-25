using BookStore.Domain.Entities.Book;
using BookStore.Domain.Entities.Stationary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.infrastructure.YaserBookStoreDbContext;

public class BookStoreDbContext: DbContext
{
    #region Ctor

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-9EJGF76;Initial Catalog=BookStore;Integrated Security=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        base.OnConfiguring(optionsBuilder);
    }
    #endregion

    #region Db Sets

    public DbSet<Book> Books { get; set; }

    public DbSet<Stationary> Stationaries { get; set; }

    #endregion
}
