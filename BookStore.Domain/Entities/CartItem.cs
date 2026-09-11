using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities;

public class CartItem : BaseEntity
{
    public long CartId { get; set; }

    public Cart Cart { get; set; }

    public long ProductId { get; set; }

    public Product Product { get; set; }

    public int Count { get; set; }
}