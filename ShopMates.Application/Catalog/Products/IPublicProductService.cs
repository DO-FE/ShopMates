using ShopMates.Application.Catalog.Products.DTOS;
using ShopMates.Application.Catalog.Products.DTOS.Public;
using ShopMates.Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Application.Catalog.Products
{
    public interface IPublicProductService
    {

        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(DTOS.Public.GetProductPagingRequest request);
    }
}
