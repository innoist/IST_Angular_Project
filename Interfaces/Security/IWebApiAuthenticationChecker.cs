namespace IST.Interfaces.Security
{
    /// <summary>
    /// Web Api Authentication interface
    /// </summary>
    public interface IWebApiAuthenticationChecker
    {
        /// <summary>
        /// Check from the database if the combination of username and password exist
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="password">Password </param>
        /// <returns>returns true if combination of username and password exists</returns>
        bool Check(string userName, string password);
    }
}
