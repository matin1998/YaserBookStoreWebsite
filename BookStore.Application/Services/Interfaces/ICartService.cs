using BookStore.Application.DTOs.Cart;
using BookStore.Application.DTOs.Coupon;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Interfaces;

public interface ICartService
{
    Task AddToCartAsync(long userId, long productId);

    Task<CartDTO?> GetUserCartAsync(long userId);

    Task<CartOperationResultDTO> IncreaseCountAsync(long userId, long productId);

    Task<CartOperationResultDTO> DecreaseCountAsync(long userId, long productId);

    Task<CartOperationResultDTO> RemoveItemAsync(long userId, long productId);

    Task<UpdateCartResultDTO> UpdateCartAsync(
    long userId,
    UpdateCartDTO model);

    Task<CartSummaryDTO> GetCartSummaryAsync(
    long userId);
    Task<CouponValidationResultDTO> ApplyCouponAsync(
    long userId,
    string code);
}
