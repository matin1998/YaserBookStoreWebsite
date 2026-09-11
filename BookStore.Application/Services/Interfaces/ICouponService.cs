using BookStore.Application.DTOs.Coupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Interfaces;

public interface ICouponService
{
    Task<CouponValidationResultDTO> ValidateCouponAsync(
    string code,
    decimal cartTotal);
}
