using ShopMates.Application.Catalog.Products;
using ShopMates.ViewModels.Catalog.Products.Public;
using ShopMates.ViewModels.Catalog.Products;
using ShopMates.ViewModels.Catalog;
using ShopMates.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Application.Catalog.Products
{
    public interface IPublicProductService
    {

        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
    }
}
