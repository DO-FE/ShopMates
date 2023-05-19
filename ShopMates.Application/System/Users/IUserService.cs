using ShopMates.ViewModels.System.Users;
using ShopMates.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Application.System.Users
{
    public interface IUserService
    {
        Task<APIResult<string>> Authenticate(LoginRequest request);

        Task<APIResult<bool>> Register(RegisterRequest request);

		Task<APIResult<bool>> Update(Guid id, UserUpdateRequest request);

		Task<APIResult<PagedResult<UserViewModels>>> GetUsersPaging(PagingRequestBase request);

		Task<APIResult<UserViewModels>> GetByID(Guid Id);
        Task<APIResult<UserViewModels>> GetByUS(string username);

        Task<APIResult<bool>> Delete(Guid Id);

        Task<APIResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
