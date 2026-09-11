using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.RepositoryInterfaces;

public interface ICartRepository : IBaseRepository<Cart>
{
    Task<Cart?> GetUserCartAsync(long userId);
}