using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
