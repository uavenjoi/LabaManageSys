using System.Web.Mvc;
using LabaManageSys.WebUI.ViewModels.Home;

namespace LabaManageSys.WebUI.Controllers
{
    public class HomeController : Controller
    {
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