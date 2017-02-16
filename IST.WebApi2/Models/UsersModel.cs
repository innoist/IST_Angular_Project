namespace IST.WebApi2.Models
{
    public class UsersModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string ImageName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Role { get; set; }
    }


    public class RoleDDL
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}