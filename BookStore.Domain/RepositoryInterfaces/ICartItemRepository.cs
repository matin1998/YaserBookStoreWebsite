using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.RepositoryInterfaces;

public interface ICartItemRepository : IBaseRepository<CartItem>
{
    Task<CartItem?> GetByCartAndProductAsync(long cartId, long productId);

    Task<List<CartItem>> GetCartItemsAsync(long cartId);

    Task<List<CartItem>> GetCartItemsWithProductAsync(long cartId);
}
