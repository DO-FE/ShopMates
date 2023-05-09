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
    public interface ILanguageService
    {
        Task<APIResult<List<LanguageViewModel>>> GetAll();
    }
}
