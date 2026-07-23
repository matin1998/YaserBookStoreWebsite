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

public class AddressRepository : BaseRepository<Address>, IAddressRepository
{
    public AddressRepository(BookStoreDbContext context)
        : base(context)
    {
    }

    public async Task<List<Address>> GetUserAddressesAsync(long userId)
    {
        return await _dbSet
            .Where(a => a.UserId == userId)
            .AsNoTracking()
            .OrderByDescending(a => a.IsDefault)
            .ToListAsync();
    }

    public async Task<List<Address>> GetUserAddressesForUpdateAsync(long userId)
    {
        return await _dbSet
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<Address?> GetDefaultAddressAsync(long userId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(a => a.UserId == userId && a.IsDefault);
    }

    public async Task<Address?> GetByIdAsync(long id, long userId)
    {
        return await _dbSet.FirstOrDefaultAsync(a =>
            a.Id == id &&
            a.UserId == userId);
    }
}

