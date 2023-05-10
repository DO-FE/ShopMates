using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopMates.ViewModels.Catalog.Products;
using ShopMates.ViewModels.Common;
using ShopMates.ViewModels.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Integration
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        public ProductApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }
        public async Task<APIResult<PagedResult<ProductViewModel>>> GetProductsPagaing(GetManageProductPagingRequest request)
        {
            var data = await base.GetAsync<APIResult<PagedResult<ProductViewModel>>>($"/api/product/paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}&languageId={request.LanguageId}");
            return data;
        }
    }
}
