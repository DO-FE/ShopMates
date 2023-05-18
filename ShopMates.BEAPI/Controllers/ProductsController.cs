using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMates.Application.Catalog.Products;
using ShopMates.Data.Entities;
using ShopMates.ViewModels.Catalog.ProductImages;
using ShopMates.ViewModels.Catalog.Products;

namespace ShopMates.BEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Products : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;

        public Products(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _manageProductService.GetAllPaging(request);
            return Ok(products);
        }

        [HttpGet("get-all/{languageId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string languageId)
        {
            var products = await _publicProductService.GetAll(languageId);
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var product = await _manageProductService.GetById(productId);
            if (product == null)
                return BadRequest("Không tìm thấy sản phẩm");
            return Ok(product);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _manageProductService.Create(request);
            if (productId == 0)
                return BadRequest("Bị lỗi nào đó khi tạo mới sản phẩm");

            var product = await _manageProductService.GetById(productId);
            return CreatedAtAction(nameof(GetByProductId), new { id = productId }, product);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = productId;
            var affectedResult = await _manageProductService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _manageProductService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest("Bị lỗi rồi, không thể xóa sản phẩm");
            return Ok();
        }

        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var isSuccessful = await _manageProductService.UpdatePrice(productId, newPrice);
            if (isSuccessful)
                return Ok();

            return BadRequest("Bị lỗi rồi không thể cập nhật giá");
        }

        //Image

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _manageProductService.AddImages(productId, request);
            if (imageId == 0)
                return BadRequest("Bị lỗi nào đó khi tạo mới sản phẩm");

            var image = await _manageProductService.GetImageById(productId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manageProductService.UpdateImages(imageId, request);
            if (result == 0)
                return BadRequest("Bị lỗi nào đó khi tạo mới hình ảnh");

            return Ok();
        }

        [HttpGet("{productId}/images")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImageById(int productId)
        {
            var image = await _manageProductService.GetImageById(productId);
            if (image == null)
                return BadRequest("Không tìm thấy hình ảnh");
            return Ok(image);
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manageProductService.RemoveImages(imageId);
            if (result == 0)
                return BadRequest("Bị lỗi nào đó khi tạo mới sản phẩm");

            return Ok();
        }
    }
}