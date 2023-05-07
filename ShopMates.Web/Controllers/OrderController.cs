using Microsoft.AspNetCore.Mvc;

namespace ShopMates.Web.Controllers
{
	public class OrderController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
