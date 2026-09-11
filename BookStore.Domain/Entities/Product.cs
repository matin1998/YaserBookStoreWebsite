using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities;

public abstract class Product : BaseEntity
{
    public string Title { get; set; }

    public string? Description { get; set; }

    public int Price { get; set; }

    public int Inventory { get; set; }

    public ICollection<Image> Images { get; set; } = new List<Image>();

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
