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

public class CouponRepository : BaseRepository<Coupon>, ICouponRepository
{
    public CouponRepository(BookStoreDbContext context)
        : base(context)
    {
    }

    public async Task<Coupon?> GetByCodeAsync(string code)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.Code == code);
    }
}
