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

public class CartItemRepository
    : BaseRepository<CartItem>, ICartItemRepository
{
    public CartItemRepository(BookStoreDbContext context)
        : base(context)
    {
    }

    public async Task<CartItem?> GetByCartAndProductAsync(
        long cartId,
        long productId)
    {
        return await _dbSet
    .Include(x => x.Product)
    .FirstOrDefaultAsync(x =>
        x.CartId == cartId &&
        x.ProductId == productId);
    }

    public async Task<List<CartItem>> GetCartItemsAsync(long cartId)
    {
        return await _dbSet
            .Where(x => x.CartId == cartId)
            .Include(x => x.Product)
                .ThenInclude(x => x.Images)
            .ToListAsync();
    }

    public async Task<List<CartItem>> GetCartItemsWithProductAsync(long cartId)
    {
        return await _dbSet
            .Include(x => x.Product)
            .Where(x => x.CartId == cartId)
            .ToListAsync();
    }
}
