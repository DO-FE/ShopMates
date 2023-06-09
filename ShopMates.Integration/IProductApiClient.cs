﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopMates.ViewModels.Catalog.ProductImages;
using ShopMates.ViewModels.Catalog.Products;
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
        Task<PagedResult<ProductViewModel>> GetProductsPagaing(GetManageProductPagingRequest request);

        Task<bool> CreateProduct(ProductCreateRequest request);

        Task<ProductViewModel> GetById(int id);

        Task<bool> UpdateProduct(ProductUpdateRequest request);

        Task<ProductImageViewModel> ViewProductImages(int productID);
        Task<bool> DeleteProduct(ProductDeleteRequest request);
    }
}
