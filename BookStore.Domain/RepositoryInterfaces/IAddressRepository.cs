using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.RepositoryInterfaces;

public interface IAddressRepository : IBaseRepository<Address>
{
    Task<List<Address>> GetUserAddressesAsync(long userId);

    Task<Address?> GetDefaultAddressAsync(long userId);
    Task<Address?> GetByIdAsync(long id, long userId);
    Task<List<Address>> GetUserAddressesForUpdateAsync(long userId);
}