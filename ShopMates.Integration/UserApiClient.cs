using ShopMates.ViewModels.Common;
using ShopMates.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ShopMates.Utilities.Constants;

namespace ShopMates.Integration
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

		public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) 
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<APIResult<string>> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            var response = await client.PostAsync("/api/users/authenticate", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<APISuccessResult<string>>(await response.Content.ReadAsStringAsync());
            }

            return JsonConvert.DeserializeObject<APISuccessResult<string>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<APIResult<bool>> Delete(Guid id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"/api/Users/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<APISuccessResult<bool>>(body);
            }
            return JsonConvert.DeserializeObject<APIErrorResult<bool>>(body);
        }

        public async Task<APIResult<UserViewModels>> GetByID(Guid id)
		{
			var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
			var client = _httpClientFactory.CreateClient();

			client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
			var response = await client.GetAsync($"/api/Users/{id}");
			var body = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<APISuccessResult<UserViewModels>>(body);
			}
			return JsonConvert.DeserializeObject<APIErrorResult<UserViewModels>>(body);
		}

		public async Task<APIResult<PagedResult<UserViewModels>>> GetUsersPagaing(PagingRequestBase request)
        {
            var client = _httpClientFactory.CreateClient();

			var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
			client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/Users/paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<APISuccessResult<PagedResult<UserViewModels>>>(body);
            return users;
        }

        public async Task<APIResult<bool>> RegisterUser(RegisterRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            var response = await client.PostAsync($"/api/Users/register", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<APISuccessResult<bool>>(result);

			}
			return JsonConvert.DeserializeObject<APIErrorResult<bool>>(result);
		}

        public async Task<APIResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/users/{id}/roles", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<APISuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<APIErrorResult<bool>>(result);
        }

        public async Task<APIResult<bool>> UpdateUser(Guid id, UserUpdateRequest request)
		{
			var client = _httpClientFactory.CreateClient();
			var json = JsonConvert.SerializeObject(request);
			var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
			var response = await client.PutAsync($"/api/Users/{id}", httpContent);
			var result = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<APISuccessResult<bool>>(result);

			}
			return JsonConvert.DeserializeObject<APIErrorResult<bool>>(result);
		}
	}
}
