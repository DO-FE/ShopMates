using ShopMates.Application.Catalog.Products;
using ShopMates.ViewModels.Catalog.Products;
using ShopMates.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopMates.ViewModels.Catalog.ProductImages;

namespace ShopMates.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId, int addQuantity);

        Task AddViewCount(int productId);

        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        Task<int> AddImages(int productId, ProductImageCreateRequest request);

        Task<int> RemoveImages(int imageId);

        Task<int> UpdateImages(int imageId, ProductImageUpdateRequest request);

        Task<List<ProductImageViewModel>> GetListImage(int productId);

        Task<ProductViewModel> GetById(int productId, string languageId);

        Task<ProductImageViewModel> GetImageById(int imageId);

    }
}
