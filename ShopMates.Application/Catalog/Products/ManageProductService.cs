﻿using Microsoft.EntityFrameworkCore;
using ShopMates.ViewModels.Catalog.Products;
using ShopMates.ViewModels.Common;
using ShopMates.Data.EF;
using ShopMates.Data.Entities;
using ShopMates.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO.Enumeration;
using System.Net.Http.Headers;
using System.IO;
using System.Data;
using ShopMates.Application.Common;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Data.SqlClient;
using ShopMates.ViewModels.Catalog.ProductImages;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace ShopMates.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly ShopMatesDbContext _context;
        private readonly IStorageService _storageService;

        public ManageProductService(ShopMatesDbContext context, IStorageService storageService) 
        {
            _context = context;
            _storageService = storageService;
        }


        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                IsFeatured = request.IsFeatured,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                },
                ProductInCategories = new List<ProductInCategory>()
                {
                    new ProductInCategory()
                    {
                        CategoryId = request.CategoryId
                    }
                }
            };

            //Save Image
            if(request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail Image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ShopMatesException($"Không tìm thấy sản phẩm: {productId}");

            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach(var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            // 1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        join pi in _context.ProductImages on p.Id equals pi.ProductId
                        where pt.LanguageId == request.LanguageId && ct.LanguageId == request.LanguageId
                        select new { p, pt, pic, ct , pi };
            //2. filter
            //if (!string.IsNullOrEmpty(request.Keyword))
            //    query = query.Where(x => x.pt.Name.Contains(request.Keyword));

            if (request.CategoryId != null && request.CategoryId != 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                Name = x.pt.Name,
                DateCreated = x.p.DateCreated,
                Description = x.pt.Description,
                Details = x.pt.Details,
                LanguageId = x.pt.LanguageId,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
                SeoAlias = x.pt.SeoAlias,
                SeoDescription = x.pt.SeoDescription,
                SeoTitle = x.pt.SeoTitle,
                Stock = x.p.Stock,
                ViewCount = x.p.ViewCount,
                CategoryName = x.ct.Name,
                ImageUrl = _storageService.GetFileUrl(x.pi.ImagePath)
            }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;

        }

        public async Task<ProductViewModel> GetById(int productId)
        {
            var product = await _context.Products.Include(p => p.ProductTranslations).Include(p => p.ProductInCategories).ThenInclude(pic => pic.Category).ThenInclude(c => c.CategoryTranslations).Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == productId);

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Price = product.Price,
                OriginalPrice = product.OriginalPrice,
                Stock = product.Stock,
                ViewCount = product.ViewCount,
                DateCreated = product.DateCreated,
                Name = product.ProductTranslations.FirstOrDefault()?.Name,
                Description = product.ProductTranslations.FirstOrDefault()?.Description,
                Details = product.ProductTranslations.FirstOrDefault()?.Details,
                SeoDescription = product.ProductTranslations.FirstOrDefault()?.SeoDescription,
                SeoTitle = product.ProductTranslations.FirstOrDefault()?.SeoTitle,
                SeoAlias = product.ProductTranslations.FirstOrDefault()?.SeoAlias,
                LanguageId = product.ProductTranslations.FirstOrDefault()?.LanguageId,
                IsFeatured = product.IsFeatured,
                CategoryName = product.ProductInCategories.FirstOrDefault()?.Category?.CategoryTranslations.FirstOrDefault()?.Name
            };  
            return productViewModel;
        }

        public async Task<int> AddImages(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder,
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }

            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<ProductImageViewModel> GetImageById(int productId)
        {
            var image = await _context.ProductImages.FirstOrDefaultAsync(x => x.ProductId == productId);
            if(image == null) 
            {
                throw new ShopMatesException($"Không tìm thấy hình ảnh với productID {productId}");
            }
            var viewModel = new ProductImageViewModel()
            {
                Caption = image.Caption,
                DateCreate = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }

        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId).Select(i => new ProductImageViewModel()
            {
                Caption = i.Caption,
                DateCreate = i.DateCreated,
                FileSize = i.FileSize,
                Id = i.Id,
                ImagePath = i.ImagePath,
                IsDefault = i.IsDefault,
                ProductId = i.ProductId,
                SortOrder = i.SortOrder,
            }).ToListAsync();
        }

        public async Task<int> RemoveImages(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
            {
                throw new ShopMatesException($"Không tìm thấy hình ảnh với imageID {imageId}");
            }
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.Include(p => p.ProductTranslations).Include(p => p.ProductInCategories).ThenInclude(pic => pic.Category).ThenInclude(c => c.CategoryTranslations).Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == request.Id);

            if (product == null)
            {
                throw new ShopMatesException($"Không tìm thấy sản phẩm: {request.Id}");
            }

            var productTranslations = product.ProductTranslations.FirstOrDefault();
            var productInCategory = product.ProductInCategories.FirstOrDefault();

            if (productTranslations == null || productInCategory == null)
            {
                throw new ShopMatesException($"Không tìm thấy thông tin sản phẩm: {request.Id}");
            }

            productTranslations.Name = request.Name;
            productTranslations.SeoAlias = request.SeoAlias;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Description = request.Description;
            productTranslations.Details = request.Details;
            productTranslations.LanguageId = request.LanguageId;
            product.Stock = request.Stock;
            product.IsFeatured = request.IsFeatured;
            product.Price = request.Price;
            productInCategory.CategoryId = request.CategoryId;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImages(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if(productImage == null)
            {
                throw new ShopMatesException($"Không tìm thấy hình ảnh với imageID {imageId}");
            }

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }

            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ShopMatesException($"Không tìm thấy sản phẩm: {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ShopMatesException($"Không tìm thấy sản phẩm: {productId}");
            product.Stock += addQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var filename = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), filename);
            return filename;
        }

    }
}
