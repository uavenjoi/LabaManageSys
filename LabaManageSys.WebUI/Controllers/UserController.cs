using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LabaManageSys.Domain.Abstract;
using LabaManageSys.Domain.EntitiesModel;
using LabaManageSys.WebUI.Models;

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
            IEnumerable<UserViewModel> model = this.repository.AppUsers
                .Select(_ => new UserViewModel{ UserId = _.UserId, Name = _.Name, Email = _.Email, RoleId = _.RoleId});
            this.ViewBag.Roles = this.repository.Roles.ToList();
            return this.View(model);
        }

        public ViewResult Edit(int userId)
        {
            UserViewModel model = new UserViewModel(this.repository.AppUsers
                .FirstOrDefault(_ => _.UserId == userId));
            this.ViewBag.RoleId = this.CreateRoleList(model.RoleId);
            return this.View(model);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(UserViewModel viewUser)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser {UserId = viewUser.UserId, Email = viewUser.Email,
                    Name = viewUser.Name, RoleId = viewUser.RoleId };
                this.repository.UserUpdate(user);
                this.TempData["message"] = string.Format("Изменения в пользователе \"{0}\" были сохранены", user.Name);
                return this.RedirectToAction("List");
            }
            else
            {
                // Что-то не так со значениями данных
                this.ViewBag.RoleId = this.CreateRoleList(viewUser.RoleId);
                return this.View(viewUser);
            }
        }

        // Создание пользователя
        public ViewResult Create()
        {
            this.ViewBag.RoleId = this.CreateRoleList(null);
            return this.View("Edit", new UserViewModel());
        }

        // Удаление пользователя из базы
        [HttpPost]
        public ActionResult Delete(int userId)
        {
            AppUser deletedUser = this.repository.UserDelete(userId);
            if (deletedUser != null)
            {
                this.TempData["message"] = string.Format("Пользователь \"{0}\" успешно удален.", deletedUser.Name);
            }

            return this.RedirectToAction("List");
        }

        private SelectList CreateRoleList(int? selectRole)
        {
            List<Role> roles = this.repository.Roles.ToList();
            SelectList list = new SelectList(roles, "RoleId", "Name", selectedValue: selectRole);
            return list;
        }
    }
}