using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.Cart;

public class UpdateCartItemDTO
{
    public long ProductId { get; set; }

    public int Count { get; set; }
}
