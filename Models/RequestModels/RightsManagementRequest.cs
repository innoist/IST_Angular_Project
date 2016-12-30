namespace IST.Models.RequestModels
{
    /// <summary>
    /// Rights Management Request Model
    /// </summary>
    public class RightsManagementRequest
    {
        /// <summary>
        /// Role
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// Selected Menu Ids
        /// </summary>
        public string SelectedMenuIds { get; set; }
    }
}
