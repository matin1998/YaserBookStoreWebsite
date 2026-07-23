using BookStore.Application.DTOs.Account;
using BookStore.Application.Services.Interfaces;
using BookStore.Presentation.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Presentation.Areas.CustomerPanel.Controllers
{
    public class ProfileController : CustomerBaseController
    {
        private readonly IAccountService _accountService;

        public ProfileController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _accountService.GetProfileAsync(CurrentUserId);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(EditProfileDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var result = await _accountService.UpdateProfileAsync(CurrentUserId, model);

            if (!result.Succeeded)
            {
                AddIdentityErrors(result);

                return View(model);
            }

            SuccessMessage("اطلاعات حساب کاربری بروزرسانی شد");


            return RedirectToAction(nameof(Index));
        }
    }
}
