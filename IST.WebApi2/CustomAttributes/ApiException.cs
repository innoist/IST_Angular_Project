using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Filters;
using IST.ExceptionHandling;
using IST.Interfaces.IServices;
using Newtonsoft.Json;

namespace IST.WebApi2.CustomAttributes
{
    /// <summary>
    /// Api Exception filter attribute for Api controller methods
    /// </summary>
    public class ApiException : ActionFilterAttribute
    {
        #region Private

        // ReSharper disable InconsistentNaming
        private static ILogger frsLogger;
        // ReSharper restore InconsistentNaming
        /// <summary>
        /// Get Configured logger
        /// </summary>
        // ReSharper disable InconsistentNaming
        private static ILogger ISTLogger
        // ReSharper restore InconsistentNaming
        {
            get
            {
                if (frsLogger != null) return frsLogger;
                //frsLogger = (UnityConfig.GetConfiguredContainer()).Resolve<ILogger>();
                return frsLogger;
            }
        }
        /// <summary>
        /// Set status code and contents of the Application exception
        /// </summary>
        private void SetApplicationResponse(HttpActionExecutedContext filterContext)
        {
            ISTExceptionContent contents = new ISTExceptionContent
            {
                Message = filterContext.Exception.Message
            };
            filterContext.Response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(contents))
            };
        }
        /// <summary>
        /// Set General Exception
        /// </summary>
        private void SetGeneralExceptionApplicationResponse(HttpActionExecutedContext filterContext)
        {
            ISTExceptionContent contents = new ISTExceptionContent
            {
                Message = string.IsNullOrEmpty(filterContext.Exception.Message) ? "There is some problem while performing this operation." : filterContext.Exception.Message
            };
            filterContext.Response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(contents))
            };
        }
        #endregion

        #region Public
        /// <summary>
        /// Exception Handler for api calls; apply this attribute for all the Api calls
        /// </summary>
        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                return;
            }
            if (filterContext.Exception is ISTExceptionContent)
            {
                SetApplicationResponse(filterContext);
                ISTLogger.Write(filterContext.Exception, LoggerCategories.Error, -1, 0, TraceEventType.Warning, "Web Api Exception - FRS Exception", null);
            }
            else
            {
                SetGeneralExceptionApplicationResponse(filterContext);
                //ISTLogger.Write(filterContext.Exception, LoggerCategories.Error, -1, 0, TraceEventType.Warning, "Web Api Exception - General", null);
            }
        }

        #endregion
    }
}
