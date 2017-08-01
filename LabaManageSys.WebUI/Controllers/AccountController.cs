using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;

namespace LabaManageSys.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IRepository repository;

        public AccountController(IRepository repo)
        {
            this.repository = repo;
        }

        public ActionResult Login()
        {
            return this.View();
        }

        public ActionResult Register()
        {
            return this.View();
        }
    }
}