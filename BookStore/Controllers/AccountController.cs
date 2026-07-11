using BookStore.Application.DTOs.Account;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "اطلاعات وارد شده معتبر نیست."
                });
            }

            var result = await _accountService.RegisterAsync(model);

            if (!result.Succeeded)
            {
                return Json(new
                {
                    success = false,
                    message = string.Join("<br/>",
                        result.Errors.Select(x => x.Description))
                });
            }

            // ورود خودکار بعد از ثبت نام

            var loginResult = await _accountService.LoginAsync(
                new LoginDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    RememberMe = false
                });

            return Json(new
            {
                success = loginResult.Succeeded,
                message = "ثبت نام با موفقیت انجام شد."
            });
        }

        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "اطلاعات وارد شده صحیح نیست."
                });
            }

            var result = await _accountService.LoginAsync(model);

            if (!result.Succeeded)
            {
                return Json(new
                {
                    success = false,
                    message = "ایمیل یا رمز عبور اشتباه است."
                });
            }

            return Json(new
            {
                success = true,
                message = "ورود با موفقیت انجام شد."
            });
        }

        #endregion

        #region Logout

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();

            return Json(new
            {
                success = true
            });
        }

        #endregion
    }
}
