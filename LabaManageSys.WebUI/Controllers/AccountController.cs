﻿using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.Account;

namespace LabaManageSys.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IRepository repository;
        private string defaultRole = "Users";

        public AccountController(IRepository repo)
        {
            this.repository = repo;
        }

        public ActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.View();
            }

            this.TempData["message"] = string.Format("Вам необходимо сначала выйти из логина: {0}", User.Identity.Name);
            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в репозитории
                var user = this.repository.GetUserByName(model.Name);

                if (user != null)
                {
                    // проверка пароля
                    if (this.repository.UserPasswordValidate(user, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return this.RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        this.TempData["message"] = string.Format("Пользователя с таким логином и паролем нет");
                    }
                }
                else
                {
                    this.TempData["message"] = string.Format("Пользователя с таким логином нет");
                }
            }

            return this.View(model);
        }

        public ActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.View();
            }

            this.TempData["message"] = string.Format("Вам необходимо сначала выйти из логина: {0}", User.Identity.Name);
            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в репозитории
                if (this.repository.GetUserByName(model.Name) == null && this.repository.GetUserByName(model.Email) == null)
                {
                    // создаем нового пользователя
                    var newUser = new UserModel
                    {
                        UserId = 0,
                        Name = model.Name,
                        Email = model.Email,
                        RoleId = this.repository.GetRoleByName(this.defaultRole).RoleId
                    };
                    this.repository.UserUpdate(newUser);
                    this.repository.UserPasswordSet(newUser, model.Password);

                    // если пользователь создан
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    this.TempData["message"] = string.Format("Пользователь с таким логином уже существует");
                }
            }

            return this.View(model);
        }

        [Authorize]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
