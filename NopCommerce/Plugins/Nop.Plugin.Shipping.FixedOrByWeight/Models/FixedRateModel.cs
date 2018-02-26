using Nop.Api.Framework;

namespace Nop.Plugin.Shipping.FixedOrByWeight.Models
{
    public class FixedRateModel
    {
        public int ShippingMethodId { get; set; }

        public string ShippingMethodName { get; set; }

        public decimal Rate { get; set; }
    }
}