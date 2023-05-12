using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMates.Data.Entities;
using ShopMates.ViewModels.System.Languages;

namespace ShopMates.Admin.Models
{
    public class NavigationViewModel
    {
        public List<LanguageViewModel> Languages { get; set; }

        public string CurrentLanguageId { get; set; }
    }
}
