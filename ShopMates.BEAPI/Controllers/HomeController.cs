using Microsoft.AspNetCore.Mvc;
using ShopMates.BEAPI.Models;
using System.Diagnostics;

namespace ShopMates.BEAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Ok();
        }
    }
}