using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopMates.Data.Entities;
using ShopMates.Utilities.Exceptions;
using ShopMates.ViewModels.Catalog.Products;
using ShopMates.ViewModels.Common;
using ShopMates.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ShopMates.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager,IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<APIResult<string>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new APISuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<APIResult<bool>> Delete(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null)
            {
                return new APIErrorResult<bool>("UserName không tồn tại nhé");
            }
            var result = await _userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                return new APISuccessResult<bool>();
            }
            return new APIErrorResult<bool>("Xóa không thành công rồi em ới ~");
        }

        public async Task<APIResult<UserViewModels>> GetByID(Guid Id)
		{
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null)
            {
                return new APIErrorResult<UserViewModels>("UserName không tồn tại nhé");
            }
            var userVM = new UserViewModels()
            {
				PhoneNumber = user.PhoneNumber,
				UserName = user.UserName,
                Dob = user.Dob,
				FirstName = user.FirstName,
                Email = user.Email,
				LastName = user.LastName,
                Id = Id
			};
            return new APISuccessResult<UserViewModels>(userVM);
		}

		public async Task<APIResult<PagedResult<UserViewModels>>> GetUsersPaging(PagingRequestBase request)
        {
            var query = _userManager.Users;
            /*if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword));
            }*/

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(x => new UserViewModels()
            {
                Id = x.Id,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName,
                FirstName = x.FirstName,
                LastName = x.LastName

            }).ToListAsync();

            var pagedResult = new PagedResult<UserViewModels>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return new APISuccessResult<PagedResult<UserViewModels>>(pagedResult);
        }

        public async Task<APIResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new APIErrorResult<bool>("UserName này có rồi lấy cái khác đi");
            }
			if (await _userManager.FindByEmailAsync(request.Email) != null)
			{
				return new APIErrorResult<bool>("Email đã có rồi, lấy cái khác đi");
			}

			user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new APISuccessResult<bool>();
            }
            return new APIErrorResult<bool>("Đăng ký không thành công");
        }

		public async Task<APIResult<bool>> Update(Guid id, UserUpdateRequest request)
		{
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.Dob = request.Dob;
            user.FirstName = request.FirstName;
			user.LastName = request.LastName;
			user.PhoneNumber = request.PhoneNumber;

			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return new APISuccessResult<bool>();
			}
			return new APIErrorResult<bool>("Cập nhật không thành công");
		}
	}
}
