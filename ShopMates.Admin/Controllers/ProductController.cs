using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMates.Data.Entities;
using ShopMates.Integration;
using ShopMates.Utilities.Constants;
using ShopMates.ViewModels.Catalog.Products;
using ShopMates.ViewModels.Common;
using ShopMates.ViewModels.System.Users;

namespace ShopMates.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> ListProducts(int pageIndex = 1, int pageSize = 15)
        {
            var language = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var session = HttpContext.Session.GetString("Token");
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var request = new GetManageProductPagingRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = language
            };
            var data = await _productApiClient.GetProductsPagaing(request);
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Add Product Successfully";
                return RedirectToAction("ListProducts");
            }

            ModelState.AddModelError("", "Add Product UnSuccessfilly");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiClient.GetById(id);
            var updateVm = new ProductUpdateRequest()
            {
                Id = product.Id,
                Description = product.Description,
                Details = product.Details,
                Name = product.Name,
                SeoAlias = product.SeoAlias,
                SeoDescription = product.SeoDescription,
                SeoTitle = product.SeoTitle
            };
            return View(updateVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.UpdateProduct(request);
            if (result)
            {
                TempData["result"] = "Update Product Successfully";
                return RedirectToAction("ListUser");
            }

            ModelState.AddModelError("", "Update Product UnSuccessfilly");
            return View(request);
        }
    }
}
