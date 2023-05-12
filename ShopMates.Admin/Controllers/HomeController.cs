using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMates.Admin.Models;
using ShopMates.Integration;
using ShopMates.Utilities.Constants;
using System.Diagnostics;

namespace ShopMates.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserApiClient _userApiClient;

        public HomeController(ILogger<HomeController> logger, IUserApiClient userApiClient)
        {
            _logger = logger;
            _userApiClient = userApiClient;
        }

        public IActionResult Index()
        {
            var user = User.Identity.Name;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Message()
        {
            return View();
        }

        public async Task<IActionResult> Profile(Guid id)
        {
            var result = await _userApiClient.GetByID(id);
            return View(result.ResultObj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Language(NavigationViewModel viewModel)
        {
            HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId,
                viewModel.CurrentLanguageId);

            return Redirect("/AdminShopMates/Home/Index");
        }
    }
}