using System;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.ViewModels.Home;

namespace LabaManageSys.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private ILogger log;

        public HomeController(ILogger logger)
        {
            this.log = logger;
        }
       
        // GET: Home
        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel { Status = "Вы не авторизованы" };
            if (User.Identity.IsAuthenticated)
            {
                model.Status = "Ваш логин: " + User.Identity.Name + " роль Админ:" + (User.IsInRole("Administrators") ? "да" : "нет");
            }

            return this.View(model);
        }
    }
}