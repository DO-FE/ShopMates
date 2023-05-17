using Microsoft.EntityFrameworkCore;
using ShopMates.Application.Common;
using ShopMates.Data.EF;
using ShopMates.Data.Entities;
using ShopMates.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ShopMatesDbContext _context;
        public CategoryService(ShopMatesDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };

            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.ct.Name
            }).ToListAsync();
        }
    }
}
