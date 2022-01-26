
using System;

namespace SuperMamiApi.Commands.ShippingPaymentCommands
{
    public class CommandRegisterShippingPayment
    {
        public double? TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public bool? IsActive { get; set; }

    }
} 