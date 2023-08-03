using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopMates.Data.Entities;
using ShopMates.Utilities.Constants;
using ShopMates.ViewModels.Common;
using ShopMates.ViewModels.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Integration
{
    public class LanguageApiClient : BaseApiClient, ILanguageApiClient
    {
        private readonly IConfiguration _configuration;
        public LanguageApiClient(IHttpClientFactory httpClientFactory,IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _configuration = configuration;
        }

        public async Task<APIResult<List<LanguageViewModel>>> GetAll()
        {
            return await GetAsync<APIResult<List<LanguageViewModel>>>($"{_configuration[SystemConstants.AppSettings.BaseAddress]}/api/languages");
        }
    }
}
