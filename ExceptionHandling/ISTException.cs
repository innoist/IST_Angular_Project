using System;

namespace IST.ExceptionHandling
{
    /// <summary>
    /// Cares Exception
    /// </summary>
    public sealed class ISTException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of FRS Exception
        /// </summary>
        public ISTException(string message): base(message)
        {            
        }
        /// <summary>
        /// Initializes a new instance of FRS Exception
        /// </summary>
        public ISTException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
