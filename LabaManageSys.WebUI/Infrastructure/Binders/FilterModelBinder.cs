using System.Web.Mvc;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.Infrastructure.Binders
{
    public class FilterModelBinder : IModelBinder
    {
        private const string Sessionkey = "FilterModel";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Получаем объект FilterModel из сеанса
            FilterModel filterModel = null;
            if (controllerContext.HttpContext.Session != null)
            {
                filterModel = (FilterModel)controllerContext.HttpContext.Session[Sessionkey];
            }

            // Создаем объект FilterModel если не получили из сеанса
            if (filterModel == null)
            {
                filterModel = new FilterModel();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[Sessionkey] = filterModel;
                }
            }

            // Возвращаем объект FilterModel
            return filterModel;
        }
    }
}