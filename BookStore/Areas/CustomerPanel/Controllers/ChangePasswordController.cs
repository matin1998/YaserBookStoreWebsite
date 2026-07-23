using BookStore.Application.DTOs.Account;
using BookStore.Application.Services.Interfaces;
using BookStore.Presentation.Common;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Areas.CustomerPanel.Controllers;

public class ChangePasswordController : CustomerBaseController
{
    private readonly IAccountService _accountService;

    public ChangePasswordController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ChangePasswordDTO model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _accountService.ChangePasswordAsync(CurrentUserId, model);

        if (!result.Succeeded)
        {
            AddIdentityErrors(result);

            return View(model);
        }

        SuccessMessage("رمز عبور با موفقیت تغییر کرد");

        return RedirectToAction(nameof(Index));
    }
}
