using System;
using IST.Models.IdentityModels;

namespace IST.Models.Common
{
    public class ClaimParamters
    {
        public Int16 LicenseTypeId { get; set; }
        public Int16 RaPerMonth { get; set; }
        public Int16 Employee { get; set; }
        public Int16 Branches { get; set; }
        public Int16 FleetPools { get; set; }
        public Int16 Vehicles { get; set; }
        public double UserDomainKey { get; set; }
        public AspNetUser CaresUser { get; set; }
    }
}
