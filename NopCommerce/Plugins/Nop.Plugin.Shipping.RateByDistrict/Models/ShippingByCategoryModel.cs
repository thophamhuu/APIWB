﻿using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Api.Framework;
using Nop.Api.Framework.Mvc;

namespace Nop.Plugin.Shipping.RateByDistrict.Models
{
    public class ShippingByCategoryModel : BaseNopEntityModel
    {
        public ShippingByCategoryModel()
        {
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        /// <summary>
        /// Gets or sets the additional fixed cost
        /// </summary>
        public decimal AdditionalFixedCost { get; set; }
        public string PrimaryStoreCurrencyCode { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; } = new List<SelectListItem>();
        public IList<SelectListItem> AvailableProductTypes { get; set; } = new List<SelectListItem>();
        public IList<SelectListItem> AvailableCategories { get; set; } = new List<SelectListItem>();
    }
}