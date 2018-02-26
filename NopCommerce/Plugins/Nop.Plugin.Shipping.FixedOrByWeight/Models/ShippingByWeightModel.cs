using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Api.Framework;
using Nop.Api.Framework.Mvc;

namespace Nop.Plugin.Shipping.FixedOrByWeight.Models
{
    public class ShippingByWeightModel : BaseNopEntityModel
    {
        public ShippingByWeightModel()
        {
            AvailableCountries = new List<SelectListItem>();
            AvailableStates = new List<SelectListItem>();
            AvailableShippingMethods = new List<SelectListItem>();
            AvailableStores = new List<SelectListItem>();
            AvailableWarehouses = new List<SelectListItem>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }

        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public int StateProvinceId { get; set; }
        public string StateProvinceName { get; set; }

        public string Zip { get; set; }

        public int ShippingMethodId { get; set; }
        public string ShippingMethodName { get; set; }

        public decimal From { get; set; }

        public decimal To { get; set; }

        public decimal AdditionalFixedCost { get; set; }

        public decimal PercentageRateOfSubtotal { get; set; }

        public decimal RatePerWeightUnit { get; set; }

        public decimal LowerWeightLimit { get; set; }

        public string DataHtml { get; set; }

        public string PrimaryStoreCurrencyCode { get; set; }
        public string BaseWeightIn { get; set; }

        public IList<SelectListItem> AvailableCountries { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }
        public IList<SelectListItem> AvailableShippingMethods { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }
        public IList<SelectListItem> AvailableWarehouses { get; set; }
    }
}