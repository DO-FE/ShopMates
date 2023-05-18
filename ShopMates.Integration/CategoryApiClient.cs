using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopMates.Data.Entities;
using ShopMates.ViewModels.Catalog.Categories;
using ShopMates.ViewModels.Common;
using ShopMates.ViewModels.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Integration
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {

        }
        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            return await GetListAsync<CategoryViewModel>($"/api/categories/{languageId}");
        }

        public async Task<CategoryViewModel> GetById(int productId)
        {
            return await GetAsync<CategoryViewModel>($"/api/categories/get-by-id/{productId}");
        }
    }
}
