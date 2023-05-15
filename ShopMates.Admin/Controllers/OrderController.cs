using Microsoft.AspNetCore.Mvc;

namespace ShopMates.Admin.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult ListOrder()
        {
            return View();
        }
    }
}
