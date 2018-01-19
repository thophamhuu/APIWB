using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Api.Models.Requests
{
    public class UpdateShoppingCartItemRequest
    {
        public int customerId { get; set; }
        public int shoppingCartItemId { get; set; }
        public string attributesXml { get; set; }
        public decimal customerEnteredPrice { get; set; }
        public DateTime? rentalStartDate { get; set; } = null;
        public DateTime? rentalEndDate { get; set; } = null;
        public int quantity { get; set; } = 1;
        public bool resetCheckoutData { get; set; } = true;
    }
}