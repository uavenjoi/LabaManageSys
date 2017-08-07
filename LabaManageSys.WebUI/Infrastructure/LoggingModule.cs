using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Concrete;
using LabaManageSys.WebUI.Filters;
using log4net;
using Ninject.Modules;
using Ninject.Web.Mvc.FilterBindingSyntax;

namespace LabaManageSys.WebUI.Infrastructure
{
    public class LoggingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType))
                .InSingletonScope();
            Bind<ILogger>().To<Logger>().InSingletonScope();
        }
    }
}