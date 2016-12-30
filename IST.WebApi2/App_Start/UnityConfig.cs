using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace IST.WebApi2
{
    public static class UnityConfig
    {
        public static UnityContainer Container;
        public static void RegisterComponents()
        {
			Container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

             WebBase.TypeRegistrations.RegisterTypes(Container);
             Repository.TypeRegistrations.RegisterType(Container);
             Implementation.TypeRegistrations.RegisterType(Container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(Container);
        }
    }
}