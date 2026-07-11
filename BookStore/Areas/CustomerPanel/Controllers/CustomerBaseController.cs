using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Areas.CustomerPanel.Controllers;

[Area("Customer")]
[Authorize]
 public abstract class CustomerBaseController : Controller
{
}

