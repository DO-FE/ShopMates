using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMates.Application.Catalog.Categories;
using ShopMates.ViewModels.Catalog.Products;

namespace ShopMates.BEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Categories : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public Categories(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAllPaging(string languageId)
        {
            var categories = await _categoryService.GetAll(languageId);
            return Ok(categories);
        }

        [HttpGet("get-by-id/{productId}")]
        public async Task<IActionResult> GetCatId(int productId)
        {
            var categories = await _categoryService.GetById(productId);
            return Ok(categories);
        }
    }
}
