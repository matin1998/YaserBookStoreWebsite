using BookStore.Application.DTOs.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Interfaces;

public interface IShippingService
{
    Task<List<ShippingMethodDTO>> GetShippingMethodsAsync();
}
