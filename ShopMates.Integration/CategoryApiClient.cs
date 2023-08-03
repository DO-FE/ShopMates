using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopMates.Data.Entities;
using ShopMates.Utilities.Constants;
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
        private readonly IConfiguration _configuration;
        public CategoryApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            return await GetListAsync<CategoryViewModel>($"{_configuration[SystemConstants.AppSettings.BaseAddress]}/api/categories/{languageId}");
        }

        public async Task<CategoryViewModel> GetById(int productId)
        {
            return await GetAsync<CategoryViewModel>($"{_configuration[SystemConstants.AppSettings.BaseAddress]}/api/categories/get-by-id/{productId}");
        }
    }
}
