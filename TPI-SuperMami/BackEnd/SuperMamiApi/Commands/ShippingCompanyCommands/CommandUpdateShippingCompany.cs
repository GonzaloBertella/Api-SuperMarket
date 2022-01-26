namespace SuperMamiApi.Commands.ShippingCompanyCommands
{
    public class CommandUpdateShippingCompany
    {

        public int IdShippingCompany { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Cuit { get; set; }
        public int? IdShippingType { get; set; }
        public double? Salary { get; set; }
        public string ContactName { get; set; }
        public int? MaxShippingsPerDay { get; set; }

    }
}