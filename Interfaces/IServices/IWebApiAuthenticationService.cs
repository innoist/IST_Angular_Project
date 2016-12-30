namespace IST.Interfaces.IServices
{
    /// <summary>
    /// Web Api Authentication Service
    /// </summary>
    public interface IWebApiAuthenticationService
    {
        /// <summary>
        /// Check if WebApiUser is valid
        /// </summary>
        bool IsValidWebApiUser(string userName, string password);
    }
}
