using BookStore.Application.DTOs.AdminSide.Users;
using BookStore.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Areas.AdminPanel.Controllers;

public class UserController : AdminBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _userService.GetUsersAsync();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditRole(long userId)
    {
        var model = await _userService.GetUserRoleAsync(userId);

        if (model == null)
            return NotFound();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(EditUserRoleDTO model)
    {
        if (!ModelState.IsValid)
        {
            model = await _userService.GetUserRoleAsync(model.UserId);

            return View(model);
        }

        var result = await _userService.ChangeUserRoleAsync(model);

        if (!result)
        {
            ModelState.AddModelError("", "خطا در تغییر نقش.");

            model = await _userService.GetUserRoleAsync(model.UserId);

            return View(model);
        }

        TempData["SuccessMessage"] = "نقش کاربر با موفقیت تغییر کرد.";

        return RedirectToAction(nameof(Index));
    }
}
