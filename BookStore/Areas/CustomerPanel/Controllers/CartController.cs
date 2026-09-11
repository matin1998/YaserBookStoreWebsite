using BookStore.Application.DTOs.Cart;
using BookStore.Application.DTOs.Coupon;
using BookStore.Application.Services.Implementations;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Areas.CustomerPanel.Controllers;

public class CartController : CustomerBaseController
{
    private readonly ICartService _cartService;
    private readonly ICouponService _couponService;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartController(
        ICartService cartService,
        UserManager<ApplicationUser> userManager)
    {
        _cartService = cartService;
        _userManager = userManager;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(long productId)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Challenge();

        await _cartService.AddToCartAsync(user.Id, productId);

        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Challenge();

        var cart = await _cartService.GetUserCartAsync(user.Id);

        return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Increase(long productId)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Unauthorized();

        var result = await _cartService.IncreaseCountAsync(user.Id, productId);

        return Json(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Decrease(long productId)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Unauthorized();

        var result = await _cartService.DecreaseCountAsync(user.Id, productId);


        return Json(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCart(
    UpdateCartDTO model)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Unauthorized();

        var result = await _cartService.UpdateCartAsync(
            user.Id,
            model);

        return Json(result);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(long productId)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Unauthorized();

        var result = await _cartService.RemoveItemAsync(user.Id, productId);


        return Json(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApplyCoupon(ApplyCouponDTO model)
    {
        var cart = await _cartService.GetUserCartAsync(
            _userManager.GetUserAsync(User).Id);

        if (cart == null || !cart.Items.Any())
        {
            return Json(new CouponValidationResultDTO
            {
                Success = false,
                Message = "سبد خرید شما خالی است."
            });
        }

        var result = await _couponService.ValidateCouponAsync(
            model.Code,
            cart.TotalPrice);

        return Json(result);
    }
}
