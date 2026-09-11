using BookStore.Application.DTOs.Coupon;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Implementations;

public class CouponService : ICouponService
{
    private readonly ICouponRepository _couponRepository;

    public CouponService(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<CouponValidationResultDTO> ValidateCouponAsync(
        string code,
        decimal cartTotal)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return new CouponValidationResultDTO
            {
                Success = false,
                Message = "کد تخفیف را وارد کنید."
            };
        }

        if (cartTotal <= 0)
        {
            return new CouponValidationResultDTO
            {
                Success = false,
                Message = "سبد خرید شما خالی است."
            };
        }

        code = code.Trim();

        var coupon = await _couponRepository.GetByCodeAsync(code);

        if (coupon == null)
        {
            return new CouponValidationResultDTO
            {
                Success = false,
                Message = "کد تخفیف معتبر نیست."
            };
        }

        var now = DateTime.Now;

        if (!coupon.IsActive)
        {
            return new CouponValidationResultDTO
            {
                Success = false,
                Message = "این کد تخفیف فعال نیست."
            };
        }

        if (now < coupon.StartDate)
        {
            return new CouponValidationResultDTO
            {
                Success = false,
                Message = "زمان استفاده از این کد تخفیف هنوز فرا نرسیده است."
            };
        }

        if (now > coupon.ExpireDate)
        {
            return new CouponValidationResultDTO
            {
                Success = false,
                Message = "این کد تخفیف منقضی شده است."
            };
        }

        if (coupon.MaxUsage.HasValue &&
            coupon.UsedCount >= coupon.MaxUsage.Value)
        {
            return new CouponValidationResultDTO
            {
                Success = false,
                Message = "ظرفیت استفاده از این کد تخفیف تکمیل شده است."
            };
        }

        if (coupon.DiscountPercent <= 0 ||
            coupon.DiscountPercent > 100)
        {
            return new CouponValidationResultDTO
            {
                Success = false,
                Message = "درصد تخفیف این کد معتبر نیست."
            };
        }

        var discountAmount =
            cartTotal * coupon.DiscountPercent / 100m;

        var finalPrice =
            cartTotal - discountAmount;

        return new CouponValidationResultDTO
        {
            Success = true,
            Message = "کد تخفیف با موفقیت اعمال شد.",
            DiscountPercent = coupon.DiscountPercent,
            DiscountAmount = discountAmount,
            FinalPrice = finalPrice
        };
    }
}
