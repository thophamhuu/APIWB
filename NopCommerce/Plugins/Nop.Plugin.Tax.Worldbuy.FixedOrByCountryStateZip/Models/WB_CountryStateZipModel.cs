using Nop.Api.Framework;
using Nop.Api.Framework.Mvc;

namespace Nop.Plugin.Tax.Worldbuy.FixedOrByCountryStateZip.Models
{
    public class WB_CountryStateZipModel : BaseNopEntityModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        public int TaxCategoryId { get; set; }
        public string TaxCategoryName { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public int StateProvinceId { get; set; }
        public string StateProvinceName { get; set; }
        public string Zip { get; set; }
        public decimal Percentage { get; set; }
    }
}