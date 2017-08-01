using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.User;

namespace LabaManageSys.WebUI.Controllers
{
    public class UserController : Controller
    {
        private IRepository repository;

        public UserController(IRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List()
        {
           ListViewModel model = new ListViewModel { Users = this.repository.UserModels };
           return this.View(model);
        }

        public ViewResult Edit(int userId)
        {
            EditViewModel model = new EditViewModel
            {
                User = this.repository.UserModels
                .FirstOrDefault(_ => _.UserId == userId),
                Roles = this.repository.RoleModels
            };
            return this.View(model);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {
                this.repository.UserUpdate(user);
                this.TempData["message"] = string.Format("Изменения в пользователе \"{0}\" были сохранены", user.Name);
                return this.RedirectToAction("List");
            }
            else
            {
                // Что-то не так со значениями данных
                return this.View(new EditViewModel
                {
                    User = user,
                    Roles = this.repository.RoleModels
                });
            }
        }

        // Создание пользователя
        public ViewResult Create()
        {
            return this.View("Edit", new EditViewModel
            {
                User = new UserModel { RoleId = this.repository.RoleModels.FirstOrDefault().RoleId},
                Roles = this.repository.RoleModels
            });
        }

        // Удаление пользователя из базы
        [HttpPost]
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