using System;
using System.Collections.Generic;
using IST.Models.Common.DropDown;

namespace IST.WebApi2.Models
{
    public class DomainKeyModel
    {
        public int KeyId { get; set; }
        public int DayCareGroupId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string DomainKeyName { get; set; }
        public string DayCareGroup { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string ContactPerson { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string StartTimeString { get; set; }
        public string EndTimeString { get; set; }
        public int MealFrequencyId { get; set; }
        public string Email { get; set; }
    }

    public class DayCareViewModel
    {
        public DomainKeyModel DayCare { get; set; }
        public IEnumerable<DropDownModel> DayCareGroups { get; set; }
        public IEnumerable<DropDownModel> MealFrequencies { get; set; }
    }
}
