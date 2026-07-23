using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Areas.CustomerPanel.Controllers
{
    public class OrdersController : CustomerBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
