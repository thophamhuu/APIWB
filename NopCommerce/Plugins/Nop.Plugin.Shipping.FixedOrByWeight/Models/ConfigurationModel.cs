using Nop.Api.Framework;
using Nop.Api.Framework.Mvc;

namespace Nop.Plugin.Shipping.FixedOrByWeight.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public bool LimitMethodsToCreated { get; set; }

        public bool ShippingByWeightEnabled { get; set; }
    }
}