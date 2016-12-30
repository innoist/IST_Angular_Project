
namespace IST.ExceptionHandling
{
    /// <summary>
    /// Cares Exception Contents
    /// </summary>
    public sealed class ISTExceptionContent
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// IST Exception Type
        /// </summary>
        public string ExceptionType => ISTExceptionTypes.AversionGeneralException;
    }
}
