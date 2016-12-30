using System.Web.Mvc;
using IST.WebBase.Mvc;
using Microsoft.Practices.Unity;

namespace IST.WebBase
{
    public static class TypeRegistrations
    {
        public static void RegisterTypes(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IExceptionFilter, LogExceptionFilterAttribute>();
            unityContainer.RegisterType<System.Web.Http.Filters.IExceptionFilter, LogExceptionFilterAttribute>();
        }
    }
}
