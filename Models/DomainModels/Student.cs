using System;

namespace IST.Models.DomainModels
{
    public class Student : BaseModel
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool HilalOnly { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Image { get; set; }
        public string GuardianFirstName { get; set; }
        public string GuardianLastName { get; set; }
        public string GuardianEmail { get; set; }
        public string GuardianPhone { get; set; }
        public string AddonsDescription { get; set; }
    }
}
