using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using ShopMates.Integration;
using ShopMates.ViewModels.System.Users;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using ShopMates.ViewModels.Common;

namespace ShopMates.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        public UserController(IUserApiClient userApiClient, IConfiguration configuration) 
        { 
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> ListUser(int pageIndex = 1, int pageSize = 15)
        {
            var session = HttpContext.Session.GetString("Token");
            var request = new PagingRequestBase()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetUsersPagaing(request);
            return View(data.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid) 
                return View();

            var result = await _userApiClient.RegisterUser(request);
            if(result.IsSuccessed) return RedirectToAction("ListUser");

			ModelState.AddModelError("", result.Message);
			return View(request);
        }

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
            var result = await _userApiClient.GetByID(id);
            if(result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UserUpdateRequest()
                {
                    Dob = user.Dob,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Id = id
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Update(UserUpdateRequest request)
		{
			if (!ModelState.IsValid)
				return View();

			var result = await _userApiClient.UpdateUser(request.Id, request);
			if (result.IsSuccessed) return RedirectToAction("ListUser");

			ModelState.AddModelError("", result.Message);
			return View(request);
		}


		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
