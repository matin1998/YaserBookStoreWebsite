using BookStore.Domain.Entities;
using BookStore.Domain.RepositoryInterfaces;
using BookStore.infrastructure.YaserBookStoreDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.infrastructure.Repositories;

public class ProductRepository: BaseRepository<Product>,IProductRepository
{
    #region Ctor
    public ProductRepository(BookStoreDbContext context)
        : base(context)
    {

    }
    #endregion

}
