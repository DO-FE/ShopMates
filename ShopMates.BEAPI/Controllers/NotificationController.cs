using Microsoft.AspNetCore.Mvc;

namespace ShopMates.BEAPI.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
