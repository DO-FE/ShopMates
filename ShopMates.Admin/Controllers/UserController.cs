﻿using Microsoft.AspNetCore.Authentication;
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
        private readonly IRoleApiClient _roleApiClient;

        public UserController(IUserApiClient userApiClient, IConfiguration configuration, IRoleApiClient roleApiClient) 
        { 
            _userApiClient = userApiClient;
            _configuration = configuration;
            _roleApiClient = roleApiClient;
        }
        public async Task<IActionResult> ListUser(int pageIndex = 1, int pageSize = 15)
        {
            var session = HttpContext.Session.GetString("Token");
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var request = new PagingRequestBase()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetUsersPagaing(request);
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var session = HttpContext.Session.GetString("Token");
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid) 
                return View();

            var result = await _userApiClient.RegisterUser(request);
            if (result.IsSuccessed) 
            {
                TempData["result"] = "Create User Successfully";
                return RedirectToAction("ListUser");
            }

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
			if (result.IsSuccessed)
            {
                TempData["result"] = "Update User Successfully";
                return RedirectToAction("ListUser");
            }

            ModelState.AddModelError("", result.Message);
			return View(request);
		}

        [HttpGet]
        public IActionResult Delete(Guid id, string username)
        {
            return View(new UserDeleteRequest()
            {
                Id = id,
                UserName = username
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.Delete(request.Id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Delete User Successfully";
                return RedirectToAction("ListUser");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
            var result = await _userApiClient.GetByID(id);
			return View(result.ResultObj);
		}

        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id);
            return View(roleAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RoleAssign(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Assign Role for User Successfully";
                return RedirectToAction("ListUser");
            }

            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetRoleAssignRequest(request.Id);
            return View(roleAssignRequest);
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

        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userObj = await _userApiClient.GetByID(id);
            var roleObj = await _roleApiClient.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            foreach (var role in roleObj.ResultObj)
            {
                roleAssignRequest.Roles.Add(new SelectItem()
                {
                    ID = role.Id.ToString(),
                    Name = role.Name,
                    Selected = userObj.ResultObj.Roles.Contains(role.Name)
                });
            }
            return roleAssignRequest;
        }
    }
}
