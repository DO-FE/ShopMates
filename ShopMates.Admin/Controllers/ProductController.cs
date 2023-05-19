using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly ILanguageApiClient _languageApiClient;
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;
        private readonly ICategoryApiClient _categoryApiClient;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration, ICategoryApiClient categoryApiClient, ILanguageApiClient languageApiClient)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
            _categoryApiClient = categoryApiClient;
            _languageApiClient = languageApiClient;
        }
        public async Task<IActionResult> ListProducts(int? categoryId, int pageIndex = 1, int pageSize = 15)
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
                LanguageId = language,
                CategoryId = categoryId
            };
            var data = await _productApiClient.GetProductsPagaing(request);
            var categories = await _categoryApiClient.GetAll(language);
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = categoryId.HasValue && categoryId.Value == x.Id
            });
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var language = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var categories = await _categoryApiClient.GetAll(language);
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            var session = HttpContext.Session.GetString("Token");
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            var image = await _productApiClient.ViewProductImages(id);
            var imageUrl = GetFileUrl(image.ImagePath);
            var listlanguageResult = await _languageApiClient.GetAll();
            var listlanguage = listlanguageResult.ResultObj.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            var language = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var listcategories = await _categoryApiClient.GetAll(language);
            var category = await _categoryApiClient.GetById(id);
            ViewBag.Categories = listcategories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.ImageUrl = imageUrl;
            ViewBag.Language = listlanguage;
            var updateVm = new ProductUpdateRequest()
            {
                Id = product.Id,
                Description = product.Description,
                Details = product.Details,
                Name = product.Name,
                Price = product.Price,
                SeoAlias = product.SeoAlias,
                SeoDescription = product.SeoDescription,
                SeoTitle = product.SeoTitle,
                Stock = product.Stock,
                IsFeatured = product.IsFeatured,
                CategoryId = category.Id,
                LanguageId = product.LanguageId
            };

            return View(updateVm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _productApiClient.GetById(id);
            var image = await _productApiClient.ViewProductImages(id);
            var imageUrl = GetFileUrl(image.ImagePath);
            ViewBag.ImageUrl = imageUrl;
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.UpdateProduct(request);
            if (result)
            {
                TempData["result"] = "Update Product Successfully";
                return RedirectToAction("ListProducts");
            }

            ModelState.AddModelError("", "Product update failed because nothing changed or something went wrong");
            return View(request);
        }



        [HttpGet]
        public IActionResult Delete(int id, string name)
        {
            return View(new ProductDeleteRequest()
            {
                Id = id,
                Name = name
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.DeleteProduct(request);
            if (result)
            {
                TempData["result"] = "Delete Product Successfully";
                return RedirectToAction("ListProducts");
            }

            ModelState.AddModelError("", "Delete Product UnSuccessfilly");
            return View(request);
        }

        private string GetFileUrl(string imagePath)
        {
            return $"/AdminShopMates/{USER_CONTENT_FOLDER_NAME}/{imagePath}";
        }
    }
}
