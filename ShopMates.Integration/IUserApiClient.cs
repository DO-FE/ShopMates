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
        Task<string> Authenticate(LoginRequest request);

        Task<PagedResult<UserViewModels>> GetUsersPagaing(PagingRequestBase request);

        Task<bool> RegisterUser(RegisterRequest request);
    }
}
