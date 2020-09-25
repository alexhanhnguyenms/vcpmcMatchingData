using Vcpmc.Mis.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Catalog.Products
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}