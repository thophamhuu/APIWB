using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Api.Framework;
using Nop.Api.Framework.Mvc;

namespace Nop.Plugin.Shipping.RateByDistrict.Models
{
    public class ShippingByProductTypeModel : BaseNopEntityModel
    {
        public ShippingByProductTypeModel()
        {
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        /// <summary>
        /// Gets or sets the Product Type Name
        /// </summary>
        public string ProductTypeName { get; set; }
        public decimal AdditionalFixedCost { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; } = new List<SelectListItem>();
        public string PrimaryStoreCurrencyCode { get; set; }
    }
}