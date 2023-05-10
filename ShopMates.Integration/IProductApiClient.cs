﻿using ShopMates.ViewModels.Catalog.Products;
using ShopMates.ViewModels.Common;
using ShopMates.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Integration
{
    public interface IProductApiClient
    {
        Task<APIResult<PagedResult<ProductViewModel>>> GetProductsPagaing(GetManageProductPagingRequest request);
    }
}
