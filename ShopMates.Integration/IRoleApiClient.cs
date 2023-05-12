using ShopMates.ViewModels.Common;
using ShopMates.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Integration
{
    public interface IRoleApiClient
    {
        Task<APIResult<List<RoleViewModel>>> GetAll();
    }
}
