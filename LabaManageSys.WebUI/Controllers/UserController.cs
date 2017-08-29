﻿using System.Linq;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Filters;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.User;

namespace LabaManageSys.WebUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUsersRepository repository;
        private ILogger log;
        private int pageSize = 5;//магические числа нужно заменять на константы

        public UserController(IUsersRepository repo, ILogger log)
        {
            this.log = log;
            this.repository = repo;
        }

        public ViewResult List(int page = 1)
        {
            ListViewModel model = new ListViewModel
            {
                Users = this.repository.GetUserList(page, this.pageSize),
                PagingInfo = new PagingInfo { CurrentPage = page, TotalItems = this.repository.UserModels.Count(), ItemsPerPage = this.pageSize }
            };
            return this.View(model);
        }

        [Authorize(Roles = "Administrators")]
        public ViewResult Edit(int userId)
        {
            EditViewModel model = new EditViewModel
            {
                User = this.repository.GetUserById(userId),
                Roles = this.repository.RoleModels
            };
            return this.View(model);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public ActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var message = "User " + user.Name + ((user.UserId == 0) ? " created" : " edited");
                this.repository.UserUpdate(user);
                this.TempData["message"] = $"Изменения в пользователе {user.Name} были сохранены";
                this.log.Info(message);
                return this.RedirectToAction("List");
            }
            else
            {
                // Что-то не так со значениями данных. (коммент лишний, из кода и так все понятно)
                return this.View(new EditViewModel
                {
                    User = user,
                    Roles = this.repository.RoleModels
                });
            }
        }

        // Создание пользователя
        [Authorize(Roles = "Administrators")]
        public ViewResult Create()
        {
            var model = new EditViewModel
            {
                User = new UserModel { RoleId = this.repository.GetFirstRole().RoleId },
                Roles = this.repository.RoleModels
            };

            return this.View("Edit", model);
        }

        // Удаление пользователя из базы
        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int userId)
        {
            UserModel deletedUser = this.repository.UserDelete(userId);
            if (deletedUser != null)
            {
                this.TempData["message"] = string.Format("Пользователь \"{0}\" успешно удален.", deletedUser.Name);
            }

            return this.RedirectToAction("List");
        }
    }
}
