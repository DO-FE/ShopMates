using ShopMates.ViewModels.Catalog.Categories;
using ShopMates.ViewModels.Catalog.Products;
using ShopMates.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Application.Catalog.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll(string languageId);
    }
}
