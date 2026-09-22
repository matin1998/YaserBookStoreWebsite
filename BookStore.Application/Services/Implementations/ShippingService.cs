using BookStore.Application.DTOs.Shipping;
using BookStore.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Implementations;

public class ShippingService : IShippingService
{
    public Task<List<ShippingMethodDTO>> GetShippingMethodsAsync()
    {
        var methods = new List<ShippingMethodDTO>
        {
            new()
            {
                Id = 1,
                Title = "ارسال رایگان",
                Price = 0
            },
            new()
            {
                Id = 2,
                Title = "پست سفارشی",
                Price = 10000
            },
            new()
            {
                Id = 3,
                Title = "پست پیشتاز",
                Price = 20000
            }
        };

        return Task.FromResult(methods);
    }
}