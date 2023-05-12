using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMates.Admin.Models;
using ShopMates.Integration;
using ShopMates.Utilities.Constants;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMates.Admin.Controllers.Compoments
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ILanguageApiClient _languageApiClient;

        public NavigationViewComponent(ILanguageApiClient languageApiClient)
        {
            _languageApiClient = languageApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _languageApiClient.GetAll();
            var navigationVm = new NavigationViewModel()
            {
                CurrentLanguageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId),
                Languages = languages.ResultObj
            };

            return View("Default", navigationVm);
        }
    }
}
