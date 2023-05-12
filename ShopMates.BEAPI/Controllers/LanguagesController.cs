using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMates.Application.System.Languages;

namespace ShopMates.BEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguagesController(
            ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var language = await _languageService.GetAll();
            return Ok(language);
        }
    }
}
