using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.Cart;

public class CartOperationResultDTO
{
    public bool Success { get; set; }

    public long ProductId { get; set; }

    public int Count { get; set; }

    public int Inventory { get; set; }

    public decimal ItemTotalPrice { get; set; }

    public decimal CartTotalPrice { get; set; }

    public bool Removed { get; set; }
}
