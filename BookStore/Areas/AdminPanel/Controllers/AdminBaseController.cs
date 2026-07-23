using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Areas.AdminPanel.Controllers;

[Area("Admin")]
[Authorize/*(Roles = "Admin")*/]
public abstract class AdminBaseController : Controller
{
    
}

