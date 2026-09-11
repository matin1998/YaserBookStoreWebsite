using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities;

public class Cart : BaseEntity
{
    public long UserId { get; set; }

    public ApplicationUser User { get; set; }

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
