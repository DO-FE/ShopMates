using Microsoft.AspNetCore.Http;
using ShopMates.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.ViewModels.Catalog.Products
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }

        public string LanguageId { get; set; }

        public int CategoryId { get; set; }
        public int Stock { set; get; }

        public bool? IsFeatured { get; set; }
    }
}
