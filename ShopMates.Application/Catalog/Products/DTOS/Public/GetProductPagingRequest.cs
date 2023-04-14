using ShopMates.Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Application.Catalog.Products.DTOS.Public
{
    public class GetProductPagingRequest : PagingRequestBase
    {

        public int? CategoryIds { get; set; }

    }
}
