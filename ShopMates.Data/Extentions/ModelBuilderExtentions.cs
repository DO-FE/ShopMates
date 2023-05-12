using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopMates.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Data.Extentions
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(new AppConfig() { Key = "HomeTitle", Value = "This is home page of ShopMates" },
                new AppConfig() { Key = "HomeKeyword", Value = "This is Keyword of ShopMates" },
                new AppConfig() { Key = "HomeDescription", Value = "This is Description of ShopMates" });

            modelBuilder.Entity<Language>().HasData(new Language() { Id = "vi-VN", Name = "Tiếng Việt", IsDefault = true },
                                                    new Language() { Id = "en-US", Name = "English", IsDefault = false }
                                                    );

            modelBuilder.Entity<Category>().HasData(
                new Category() 
                {
                    Id = 1, 
                    IsShowOnHome = true, 
                    ParentId = null, 
                    SortOrder = 1, 
                    Status = Enums.Status.Active
                },
                new Category()
                {
                    Id = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 2,
                    Status = Enums.Status.Active,
 
                });

            modelBuilder.Entity<CategoryTranslation>().HasData(
                new CategoryTranslation() { Id = 1, CategoryId = 1, Name = "Áo Nam", LanguageId = "vi-VN", SeoAlias = "ao-nam", SeoDescription = "Sản phẩm áo thời trang Nam", SeoTitle = "Sản phẩm áo thời trang Nam" },
                new CategoryTranslation() { Id = 2, CategoryId = 1, Name = "Men Shirt", LanguageId = "en-US", SeoAlias = "men-shirt", SeoDescription = "This Shirt product for man", SeoTitle = "This Shirt product for man" },
                new CategoryTranslation() { Id = 3, CategoryId = 2, Name = "Quần Nam", LanguageId = "vi-VN", SeoAlias = "quan-nam", SeoDescription = "Sản phẩm Quần thời trang Nam", SeoTitle = "Sản phẩm Quần thời trang Nam" },
                new CategoryTranslation() { Id = 4, CategoryId = 2, Name = "Men Trousers", LanguageId = "en-US", SeoAlias = "men-trousers", SeoDescription = "This Trousers product for man", SeoTitle = "This Trousers product for man" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product() 
                {
                    Id = 1,
                    DateCreated = DateTime.Now, 
                    OriginalPrice = 150000, 
                    Price = 2000000, 
                    Stock = 15, 
                    ViewCount = 0
                },
                new Product()
                {
                    Id = 2,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 150000,
                    Price = 2000000,
                    Stock = 15,
                    ViewCount = 0
                });

            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation() { Id = 1, ProductId = 1, Name = "Áo Sơ Mi Nam Đen", LanguageId = "vi-VN", SeoAlias = "ao-so-mi-nam-den", SeoDescription = "Áo Sơ Mi Nam Đen", SeoTitle = "Áo Sơ Mi Nam Đen", Details = "error-error-Tiến Đẹp Trai-Error-Error", Description = "Áo Sơ Mi Nam Màu Đen" },
                new ProductTranslation() { Id = 2, ProductId = 1, Name = "Black Men T-Shirt", LanguageId = "en-US", SeoAlias = "black-men-t-shirt", SeoDescription = "Black Men T-Shirt", SeoTitle = "Black Men T-Shirt", Details = "error-error-Tiến Đẹp Trai-Error-Error", Description = "Black Men T-Shirt" },
                new ProductTranslation() { Id = 3, ProductId = 2, Name = "Quần jean Nam Đen", LanguageId = "vi-VN", SeoAlias = "quan-jean-nam-den", SeoDescription = "Quần jean Nam Đen", SeoTitle = "Quần jean Nam Đen", Details = "error-error-Tiến Đẹp Trai-Error-Error", Description = "Quần jean Nam Màu Đen" },
                new ProductTranslation() { Id = 4, ProductId = 2, Name = "Black Men Jeans", LanguageId = "en-US", SeoAlias = "black-men-jeans", SeoDescription = "Black Men Jeans", SeoTitle = "Black Men Jeans", Details = "error-error-Tiến Đẹp Trai-Error-Error", Description = "Black Men Jeans" }
            );

            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId = 1, CategoryId = 1 },
                new ProductInCategory() { ProductId = 2 , CategoryId = 2 }
            );

            // any guid
            var ADMIN_ID = new Guid("8F81B6C3-9468-4B62-B178-18AE55DAAD62");
            var ROLE_ID = new Guid("410CD1A8-43A2-42F2-88BE-BCD3727D0F27");
            var SALE_ID = new Guid("D64B4652-CD9A-48EC-8977-D04606F2B8E1");
            var CUS_ID = new Guid("AF355384-53BD-4EC5-8CB3-8E76DE87223D");


            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = ROLE_ID,
                Name = "ADMIN",
                NormalizedName = "ADMIN",
                Description = "Adminitrator Role Powerful, Can hack this"
            });

            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = SALE_ID,
                Name = "SALER",
                NormalizedName = "SALER",
                Description = "Saler account for accountant"
            });

            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = CUS_ID,
                Name = "CUSTOMER",
                NormalizedName = "CUSTOMER",
                Description = "Role normal can buy order view product"
            });

            var hasder = new PasswordHasher<AppUser>();

            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = ADMIN_ID,
                UserName = "Admin",
                NormalizedUserName = "Adminitrator",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasder.HashPassword(null, "Admin@123"),
                SecurityStamp = string.Empty,
                FirstName = "Tien",
                LastName = "Lam",
                Dob = new DateTime(1999,08,23)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID,
            });
        }
    }
}
