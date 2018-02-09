using Nop.API.Framework;

namespace Nop.Plugin.Tax.Worldbuy.FixedOrByCountryStateZip.Models
{
    public class WB_FixedTaxRateModel
    {
        public int TaxCategoryId { get; set; }

        public string TaxCategoryName { get; set; }

        public decimal Rate { get; set; }

        public bool IsAbsolute { get; set; }
    }
}