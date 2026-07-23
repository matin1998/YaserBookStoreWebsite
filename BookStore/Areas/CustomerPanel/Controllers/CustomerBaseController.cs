using BookStore.Presentation.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Presentation.Areas.CustomerPanel.Controllers;

[Area("CustomerPanel")]
[Authorize]
public abstract class CustomerBaseController : Controller
{
    protected long CurrentUserId
        => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    protected string CurrentUserFullName
        => User.FindFirst("FullName")?.Value ?? "";

    protected string CurrentUserEmail
        => User.FindFirstValue(ClaimTypes.Email) ?? "";

    protected void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);
    }

    protected void SuccessMessage(string message)
    {
        TempData[TempDataKeys.Success] = message;
    }

    protected void ErrorMessage(string message)
    {
        TempData[TempDataKeys.Error] = message;
    }

    protected void WarningMessage(string message)
    {
        TempData[TempDataKeys.Warning] = message;
    }

    protected void InfoMessage(string message)
    {
        TempData[TempDataKeys.Info] = message;
    }
}

