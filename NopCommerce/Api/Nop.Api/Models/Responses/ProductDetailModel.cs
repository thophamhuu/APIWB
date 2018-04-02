using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Api.Models.Responses
{
    public class ProductDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<string> Image { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
    }
}