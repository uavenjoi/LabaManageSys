using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;

namespace LabaManageSys.WebUI.Filters
{
    public class FilterExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var exc = filterContext.Exception;

                // Формирование сообщения об ошибке
                StringBuilder logText = new StringBuilder();
                logText.Append("Возникло исключение: " + exc.HResult.ToString() + "-" + exc.Message);
                logText.Append(" в методе " + exc.TargetSite);
                DependencyResolver.Current.GetService<ILogger>().Debug(logText.ToString());
                base.OnException(filterContext);
            }
        }
    }
}