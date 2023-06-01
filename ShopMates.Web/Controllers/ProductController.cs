using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopMates.Web.Controllers
{
	public class ProductController : Controller
	{
        [Authorize]
        public IActionResult Index()
		{
			return View();
		}
	}
}
