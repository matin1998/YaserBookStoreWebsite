#region Usings

using BookStore.Presentation.Areas.AdminPanel.Controllers;
using Microsoft.AspNetCore.Mvc;
namespace Resume.Presenation.Areas.AdminPanel.Controllers;

#endregion


public class HomeController : AdminBaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
