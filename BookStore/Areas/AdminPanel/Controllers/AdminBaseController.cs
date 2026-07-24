using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
[Authorize/*(Roles = "Admin")*/]
public abstract class AdminBaseController : Controller
{
    
}

