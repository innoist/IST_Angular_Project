namespace IST.Models.DomainModels
{
    /// <summary>
    /// WebApi User Domain Model
    /// </summary>
    public class WebApiUser
    {
        #region Persisted Properties

        /// <summary>
        /// Unique Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string PasswordHash { get; set; }
        
        /// <summary>
        /// User Domain Key
        /// </summary>
        public long UserDomainKey { get; set; }

        #endregion
    }
}
