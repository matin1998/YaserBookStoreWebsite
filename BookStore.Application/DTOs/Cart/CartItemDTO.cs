using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.Cart;

public class CartItemDTO
{
    public long ProductId { get; set; }

    public string ProductTitle { get; set; }

    public int Price { get; set; }

    public int Count { get; set; }

    public int Inventory { get; set; }

    public string? ImageName { get; set; }

    public int TotalPrice => Price * Count;
}
