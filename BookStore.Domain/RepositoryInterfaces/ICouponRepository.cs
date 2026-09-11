using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.RepositoryInterfaces;

public interface ICouponRepository : IBaseRepository<Coupon>
{
    Task<Coupon?> GetByCodeAsync(string code);
}
