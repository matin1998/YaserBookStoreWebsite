using BookStore.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Interfaces;

public interface IAddressService
{
    Task<List<AddressDTO>> GetUserAddressesAsync(long userId);

    Task<AddressDTO?> GetByIdAsync(long id, long userId);

    Task AddAsync(AddressDTO model, long userId);

    Task UpdateAsync(AddressDTO model, long userId);

    Task DeleteAsync(long id, long userId);
}