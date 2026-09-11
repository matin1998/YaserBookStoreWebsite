using BookStore.Domain.Entities;
using BookStore.Domain.RepositoryInterfaces;
using BookStore.infrastructure.YaserBookStoreDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.infrastructure.Repositories;

public class CartRepository : BaseRepository<Cart>, ICartRepository
{
    public CartRepository(BookStoreDbContext context)
        : base(context)
    {
    }

    public async Task<Cart?> GetUserCartAsync(long userId)
    {
        return await _dbSet
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<CartItem?> GetCartItemAsync(long cartId, long productId)
    {
        return await _context.CartItems
            .FirstOrDefaultAsync(ci =>
                ci.CartId == cartId &&
                ci.ProductId == productId);
    }
}
