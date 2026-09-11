using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.Cart;

public class UpdateCartDTO
{
    public List<UpdateCartItemDTO> Items { get; set; } = new();
}
