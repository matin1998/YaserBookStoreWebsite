using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities;

public class Coupon : BaseEntity
{
    public string Code { get; set; } = null!;

    public int DiscountPercent { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime ExpireDate { get; set; }

    public bool IsActive { get; set; }

    public int? MaxUsage { get; set; }

    public int UsedCount { get; set; }
}
