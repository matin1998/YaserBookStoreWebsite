using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Areas.CustomerPanel.Controllers
{
    public class DashboardController : CustomerBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
