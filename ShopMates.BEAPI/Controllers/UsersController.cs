using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopMates.Application.System.Users;
using ShopMates.ViewModels.Catalog.Products;
using ShopMates.ViewModels.Common;
using ShopMates.ViewModels.System.Users;

namespace ShopMates.BEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Users : ControllerBase
    {
        private readonly IUserService _userService;

        public Users(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultToken = await _userService.Authenticate(request);
            if (string.IsNullOrEmpty(resultToken.ResultObj))
            {
                return BadRequest(resultToken);
            }
            return Ok(resultToken);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id,[FromBody] UserUpdateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _userService.Update(id, request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

        [HttpPut("{id}/roles")]
        public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RoleAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] PagingRequestBase request)
        {
            var users = await _userService.GetUsersPaging(request);
            return Ok(users);
        }

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByID(Guid Id)
		{
			var users = await _userService.GetByID(Id);
			return Ok(users);
		}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _userService.Delete(Id);
            return Ok(result);
        }
    }
}