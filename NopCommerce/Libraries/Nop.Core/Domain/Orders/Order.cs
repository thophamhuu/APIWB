using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using System.Runtime.Serialization;

namespace Nop.Core.Domain.Orders
{
    /// <summary>
    /// Represents an order
    /// </summary>
    public partial class Order : BaseEntity
    {

        private ICollection<DiscountUsageHistory> _discountUsageHistory;
        private ICollection<GiftCardUsageHistory> _giftCardUsageHistory;
        private ICollection<OrderNote> _orderNotes;
        private ICollection<OrderItem> _orderItems;
        private ICollection<Shipment> _shipments;

        #region Utilities

        protected virtual SortedDictionary<decimal, decimal> ParseTaxRates(string taxRatesStr)
        {
            var taxRatesDictionary = new SortedDictionary<decimal, decimal>();
            if (String.IsNullOrEmpty(taxRatesStr))
                return taxRatesDictionary;

            string[] lines = taxRatesStr.Split(new [] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (String.IsNullOrEmpty(line.Trim()))
                    continue;

                string[] taxes = line.Split(new [] { ':' });
                if (taxes.Length == 2)
                {
                    try
                    {
                        decimal taxRate = decimal.Parse(taxes[0].Trim(), CultureInfo.InvariantCulture);
                        decimal taxValue = decimal.Parse(taxes[1].Trim(), CultureInfo.InvariantCulture);
                        taxRatesDictionary.Add(taxRate, taxValue);
                    }
                    catch (Exception exc)
                    {
                        Debug.WriteLine(exc.ToString());
                    }
                }
            }

            //add at least one tax rate (0%)
            if (!taxRatesDictionary.Any())
                taxRatesDictionary.Add(decimal.Zero, decimal.Zero);

            return taxRatesDictionary;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the order identifier
        /// </summary>
        [DataMember]
        public Guid OrderGuid { get; set; }

        /// <summary>
        /// Gets or sets the store identifier
        /// </summary>
        [DataMember]
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        [DataMember]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the billing address identifier
        /// </summary>
        [DataMember]
        public int BillingAddressId { get; set; }

        /// <summary>
        /// Gets or sets the shipping address identifier
        /// </summary>
        [DataMember]
        public int? ShippingAddressId { get; set; }

        /// <summary>
        /// Gets or sets the pickup address identifier
        /// </summary>
        [DataMember]
        public int? PickupAddressId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a customer chose "pick up in store" shipping option
        /// </summary>
        [DataMember]
        public bool PickUpInStore { get; set; }

        /// <summary>
        /// Gets or sets an order status identifier
        /// </summary>
        [DataMember]
        public int OrderStatusId { get; set; }

        /// <summary>
        /// Gets or sets the shipping status identifier
        /// </summary>
        [DataMember]
        public int ShippingStatusId { get; set; }

        /// <summary>
        /// Gets or sets the payment status identifier
        /// </summary>
        [DataMember]
        public int PaymentStatusId { get; set; }

        /// <summary>
        /// Gets or sets the payment method system name
        /// </summary>
        [DataMember]
        public string PaymentMethodSystemName { get; set; }

        /// <summary>
        /// Gets or sets the customer currency code (at the moment of order placing)
        /// </summary>
        [DataMember]
        public string CustomerCurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the currency rate
        /// </summary>
        [DataMember]
        public decimal CurrencyRate { get; set; }

        /// <summary>
        /// Gets or sets the customer tax display type identifier
        /// </summary>
        [DataMember]
        public int CustomerTaxDisplayTypeId { get; set; }

        /// <summary>
        /// Gets or sets the VAT number (the European Union Value Added Tax)
        /// </summary>
        [DataMember]
        public string VatNumber { get; set; }

        /// <summary>
        /// Gets or sets the order subtotal (incl tax)
        /// </summary>
        [DataMember]
        public decimal OrderSubtotalInclTax { get; set; }

        /// <summary>
        /// Gets or sets the order subtotal (excl tax)
        /// </summary>
        [DataMember]
        public decimal OrderSubtotalExclTax { get; set; }

        /// <summary>
        /// Gets or sets the order subtotal discount (incl tax)
        /// </summary>
        [DataMember]
        public decimal OrderSubTotalDiscountInclTax { get; set; }

        /// <summary>
        /// Gets or sets the order subtotal discount (excl tax)
        /// </summary>
        [DataMember]
        public decimal OrderSubTotalDiscountExclTax { get; set; }

        /// <summary>
        /// Gets or sets the order shipping (incl tax)
        /// </summary>
        [DataMember]
        public decimal OrderShippingInclTax { get; set; }

        /// <summary>
        /// Gets or sets the order shipping (excl tax)
        /// </summary>
        [DataMember]
        public decimal OrderShippingExclTax { get; set; }

        /// <summary>
        /// Gets or sets the payment method additional fee (incl tax)
        /// </summary>
        [DataMember]
        public decimal PaymentMethodAdditionalFeeInclTax { get; set; }

        /// <summary>
        /// Gets or sets the payment method additional fee (excl tax)
        /// </summary>
        [DataMember]
        public decimal PaymentMethodAdditionalFeeExclTax { get; set; }

        /// <summary>
        /// Gets or sets the tax rates
        /// </summary>
        [DataMember]
        public string TaxRates { get; set; }

        /// <summary>
        /// Gets or sets the order tax
        /// </summary>
        [DataMember]
        public decimal OrderTax { get; set; }

        /// <summary>
        /// Gets or sets the order discount (applied to order total)
        /// </summary>
        [DataMember]
        public decimal OrderDiscount { get; set; }

        /// <summary>
        /// Gets or sets the order total
        /// </summary>
        [DataMember]
        public decimal OrderTotal { get; set; }

        /// <summary>
        /// Gets or sets the refunded amount
        /// </summary>
        [DataMember]
        public decimal RefundedAmount { get; set; }

        /// <summary>
        /// Gets or sets the reward points history entry identifier when reward points were earned (gained) for placing this order
        /// </summary>
        [DataMember]
        public int? RewardPointsHistoryEntryId { get; set; }

        /// <summary>
        /// Gets or sets the checkout attribute description
        /// </summary>
        [DataMember]
        public string CheckoutAttributeDescription { get; set; }

        /// <summary>
        /// Gets or sets the checkout attributes in XML format
        /// </summary>
        [DataMember]
        public string CheckoutAttributesXml { get; set; }

        /// <summary>
        /// Gets or sets the customer language identifier
        /// </summary>
        [DataMember]
        public int CustomerLanguageId { get; set; }

        /// <summary>
        /// Gets or sets the affiliate identifier
        /// </summary>
        [DataMember]
        public int AffiliateId { get; set; }

        /// <summary>
        /// Gets or sets the customer IP address
        /// </summary>
        [DataMember]
        public string CustomerIp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether storing of credit card number is allowed
        /// </summary>
        [DataMember]
        public bool AllowStoringCreditCardNumber { get; set; }

        /// <summary>
        /// Gets or sets the card type
        /// </summary>
        [DataMember]
        public string CardType { get; set; }

        /// <summary>
        /// Gets or sets the card name
        /// </summary>
        [DataMember]
        public string CardName { get; set; }

        /// <summary>
        /// Gets or sets the card number
        /// </summary>
        [DataMember]
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the masked credit card number
        /// </summary>
        [DataMember]
        public string MaskedCreditCardNumber { get; set; }

        /// <summary>
        /// Gets or sets the card CVV2
        /// </summary>
        [DataMember]
        public string CardCvv2 { get; set; }

        /// <summary>
        /// Gets or sets the card expiration month
        /// </summary>
        [DataMember]
        public string CardExpirationMonth { get; set; }

        /// <summary>
        /// Gets or sets the card expiration year
        /// </summary>
        [DataMember]
        public string CardExpirationYear { get; set; }

        /// <summary>
        /// Gets or sets the authorization transaction identifier
        /// </summary>
        [DataMember]
        public string AuthorizationTransactionId { get; set; }

        /// <summary>
        /// Gets or sets the authorization transaction code
        /// </summary>
        [DataMember]
        public string AuthorizationTransactionCode { get; set; }

        /// <summary>
        /// Gets or sets the authorization transaction result
        /// </summary>
        [DataMember]
        public string AuthorizationTransactionResult { get; set; }

        /// <summary>
        /// Gets or sets the capture transaction identifier
        /// </summary>
        [DataMember]
        public string CaptureTransactionId { get; set; }

        /// <summary>
        /// Gets or sets the capture transaction result
        /// </summary>
        [DataMember]
        public string CaptureTransactionResult { get; set; }

        /// <summary>
        /// Gets or sets the subscription transaction identifier
        /// </summary>
        [DataMember]
        public string SubscriptionTransactionId { get; set; }

        /// <summary>
        /// Gets or sets the paid date and time
        /// </summary>
        [DataMember]
        public DateTime? PaidDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the shipping method
        /// </summary>
        [DataMember]
        public string ShippingMethod { get; set; }

        /// <summary>
        /// Gets or sets the shipping rate computation method identifier or the pickup point provider identifier (if PickUpInStore is true)
        /// </summary>
        [DataMember]
        public string ShippingRateComputationMethodSystemName { get; set; }

        /// <summary>
        /// Gets or sets the serialized CustomValues (values from ProcessPaymentRequest)
        /// </summary>
        [DataMember]
        public string CustomValuesXml { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        [DataMember]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time of order creation
        /// </summary>
        [DataMember]
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the custom order number without prefix
        /// </summary>
        [DataMember]
        public string CustomOrderNumber { get; set; }

        #endregion

        #region Navigation properties

        /// <summary>
        /// Gets or sets the customer
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the billing address
        /// </summary>
        [DataMember]
        public virtual Address BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the shipping address
        /// </summary>
        [DataMember]
        public virtual Address ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets the pickup address
        /// </summary>
        [DataMember]
        public virtual Address PickupAddress { get; set; }

        /// <summary>
        /// Gets or sets the reward points history record (spent by a customer when placing this order)
        /// </summary>
        [DataMember]
        public virtual RewardPointsHistory RedeemedRewardPointsEntry { get; set; }

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        [DataMember]
        public virtual ICollection<DiscountUsageHistory> DiscountUsageHistory
        {
            get { return _discountUsageHistory ?? (_discountUsageHistory = new List<DiscountUsageHistory>()); }
            protected set { _discountUsageHistory = value; }
        }

        /// <summary>
        /// Gets or sets gift card usage history (gift card that were used with this order)
        /// </summary>
        [DataMember]
        public virtual ICollection<GiftCardUsageHistory> GiftCardUsageHistory
        {
            get { return _giftCardUsageHistory ?? (_giftCardUsageHistory = new List<GiftCardUsageHistory>()); }
            protected set { _giftCardUsageHistory = value; }
        }

        /// <summary>
        /// Gets or sets order notes
        /// </summary>
        [DataMember]
        public virtual ICollection<OrderNote> OrderNotes
        {
            get { return _orderNotes ?? (_orderNotes = new List<OrderNote>()); }
            protected set { _orderNotes = value; }
        }

        /// <summary>
        /// Gets or sets order items
        /// </summary>
        [DataMember]
        public virtual ICollection<OrderItem> OrderItems
        {
            get { return _orderItems ?? (_orderItems = new List<OrderItem>()); }
            protected set { _orderItems = value; }
        }

        /// <summary>
        /// Gets or sets shipments
        /// </summary>
        [DataMember]
        public virtual ICollection<Shipment> Shipments
        {
            get { return _shipments ?? (_shipments = new List<Shipment>()); }
            protected set { _shipments = value; }
        }

        #endregion

        #region Custom properties

        /// <summary>
        /// Gets or sets the order status
        /// </summary>
        public OrderStatus OrderStatus
        {
            get
            {
                return (OrderStatus)this.OrderStatusId;
            }
            set
            {
                this.OrderStatusId = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets the payment status
        /// </summary>
        public PaymentStatus PaymentStatus
        {
            get
            {
                return (PaymentStatus)this.PaymentStatusId;
            }
            set
            {
                this.PaymentStatusId = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets the shipping status
        /// </summary>
        public ShippingStatus ShippingStatus
        {
            get
            {
                return (ShippingStatus)this.ShippingStatusId;
            }
            set
            {
                this.ShippingStatusId = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets the customer tax display type
        /// </summary>
        public TaxDisplayType CustomerTaxDisplayType
        {
            get
            {
                return (TaxDisplayType)this.CustomerTaxDisplayTypeId;
            }
            set
            {
                this.CustomerTaxDisplayTypeId = (int)value;
            }
        }

        /// <summary>
        /// Gets the applied tax rates
        /// </summary>
        public SortedDictionary<decimal, decimal> TaxRatesDictionary
        {
            get
            {
                return ParseTaxRates(this.TaxRates);
            }
        }
        
        #endregion
    }
}
