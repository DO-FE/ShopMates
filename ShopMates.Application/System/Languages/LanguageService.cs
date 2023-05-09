using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopMates.Data.EF;
using ShopMates.Data.Entities;
using ShopMates.ViewModels.Common;
using ShopMates.ViewModels.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Application.System.Languages
{
    public class LanguageService : ILanguageService
    {
        private readonly IConfiguration _config;
        private readonly ShopMatesDbContext _context;

        public LanguageService(ShopMatesDbContext context,
            IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        public async Task<APIResult<List<LanguageViewModel>>> GetAll()
        {
            var languages = await _context.Languages.Select(x => new LanguageViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                IsDefault = x.IsDefault
            }).ToListAsync();
            return new APISuccessResult<List<LanguageViewModel>>(languages);
        }
    }
}
