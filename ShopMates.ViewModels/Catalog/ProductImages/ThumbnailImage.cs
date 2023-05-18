using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.ViewModels.Catalog.ProductImages
{
    public class ThumbnailImage
    {
        public string ContentType { get; set; }
        public string ContentDisposition { get; set; }
        public Dictionary<string, List<string>> Headers { get; set; }
        public int Length { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
    }
}
