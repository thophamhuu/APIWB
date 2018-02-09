using Nop.Services.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Nop.Services.Orders.OrderProcessingService;

namespace Nop.Api.Models.Requests
{
    public class PlaceOrderRequest
    {
        public ProcessPaymentRequest processPaymentRequest { get; set; }
        public ProcessPaymentResult processPaymentResult { get; set; }
        public PlaceOrderContainter details { get; set; }
    }
}