using ShopMates.ViewModels.Common;
using ShopMates.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Integration
{
    public interface IUserApiClient
    {
        Task<APIResult<string>> Authenticate(LoginRequest request);

        Task<APIResult<PagedResult<UserViewModels>>> GetUsersPagaing(PagingRequestBase request);

        Task<APIResult<bool>> RegisterUser(RegisterRequest request);

		Task<APIResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);

		Task<APIResult<UserViewModels>> GetByID(Guid id);
	}
}
