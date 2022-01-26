namespace SuperMamiApi.Commands.UserCommands
{
    public partial class CommandRegisterUser
    {  
        public int? IdDocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? IdRol { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }

    }
}
