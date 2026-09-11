using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.Cart;

public class CartDTO
{
    public List<CartItemDTO> Items { get; set; } = new();

    public decimal TotalPrice { get; set; }
}
