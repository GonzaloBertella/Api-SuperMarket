namespace SuperMamiApi.Commands.ShippingCommands
{
    public partial class CommandRegisterShipping
    {

        public int? IdShippingCompany { get; set; }
        public int? IdDeliveryOrder { get; set; }
        public int? IdUser { get; set; }
        // DETAIL        
        public string Comment { get; set; }
        public int? Weight { get; set; }

    }
}
