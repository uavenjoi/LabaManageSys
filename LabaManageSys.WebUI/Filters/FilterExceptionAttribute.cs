using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;

namespace LabaManageSys.WebUI.Filters
{
    public class FilterExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var exc = filterContext.Exception;

                // Формирование сообщения об ошибке
                StringBuilder logText = new StringBuilder();
                logText.AppendLine("Возникло исключение: " + exc.HResult.ToString() + "-" + exc.Message);
                logText.AppendLine("в " + exc.Source + " метод " + exc.TargetSite);
                
                // logText.AppendLine("Трассировка Стека:" + exc.StackTrace);
                DependencyResolver.Current.GetService<ILogger>().Debug(logText.ToString());
                
                // base.OnException(filterContext);
            }
        }
    }
}