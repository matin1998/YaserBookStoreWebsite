using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.Coupon;

public class CouponValidationResultDTO
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public int DiscountPercent { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal FinalPrice { get; set; }
}
