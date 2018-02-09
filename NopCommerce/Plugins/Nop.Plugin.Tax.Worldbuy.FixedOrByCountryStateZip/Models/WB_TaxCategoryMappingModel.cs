using Nop.API.Framework.Mvc;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nop.Plugin.Tax.Worldbuy.FixedOrByCountryStateZip.Models
{
    public class WB_TaxCategoryMappingModel : BaseNopEntityModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        public int TaxCategoryId { get; set; }
        public string TaxCategoryName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public decimal Percentage { get; set; }

        public IList<SelectListItem> AvailableStores { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }
        public IList<SelectListItem> AvailableTaxCategories { get; set; }
    }
}