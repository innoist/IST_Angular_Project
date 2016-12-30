
namespace IST.Models.CommonTypes
{
    /// <summary>
    /// Organisation Claim Value
    /// </summary>
    public class DomainLicenseDetailClaim
    {
        #region Public
       
        /// <summary>
        /// Rental Agreements per month
        /// </summary>
        public int RaPerMonth { get; set; }
        /// <summary>
        /// Employee per month
        /// </summary>
        public int Employee { get; set; }
        /// <summary>
        /// Branches per month
        /// </summary>
        public int Branches { get; set; }
        /// <summary>
        /// Number of fleet Pools
        /// </summary>
        public int FleetPools { get; set; }
        /// <summary>
        /// Number of Vehicles
        /// </summary>
        public int Vehicles { get; set; }
        /// <summary>
        /// User Domain Key
        /// </summary>
        public long UserDomainKey { get; set; }
        #endregion
    }
}
