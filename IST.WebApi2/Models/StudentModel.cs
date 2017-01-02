using System;
using System.Collections.Generic;
using IST.Models.Common.DropDown;

namespace IST.WebApi2.Models
{
    public class StudentModel : BaseDayCare
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public bool HilalOnly { get; set; }
        public bool FromWizard { get; set; }
        public bool? Anaphylactic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DateOfBirthString { get; set; }
        public string Image { get; set; }
        public string GuardianFirstName { get; set; }
        public string GuardianLastName { get; set; }
        public string GuardianEmail { get; set; }
        public string GuardianPhone { get; set; }
        public string Age { get; set; }
        public string AddonsDescription { get; set; }
        public string RoomNumber { get; set; }
        public int? StudentIngredientCategoriesCount { get; set; }
    }

    public class StudentLV
    {
        public StudentLV()
        {
            data = new List<StudentModel>();
        }
        public IEnumerable<StudentModel> data { get; set; }
        public int TotalCount { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }

    public class StudentViewModel
    {
        public StudentModel Student { get; set; }
        public IEnumerable<AllergyModel> Allergies { get; set; }
        public IEnumerable<DropDownModel> DailyMeals { get; set; }
        public IEnumerable<DropDownModel> Rooms { get; set; }
        public IEnumerable<DropDownModel> HilalCategories { get; set; } 
        public IEnumerable<DropDownModel> QuantityScales { get; set; } 
        public IEnumerable<DropDownModel> AdviceSources { get; set; } 
    }
}
