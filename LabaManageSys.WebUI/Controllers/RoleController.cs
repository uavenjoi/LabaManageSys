using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LabaManageSys.Domain.Abstract;
using LabaManageSys.Domain.EntitiesModel;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.Controllers
{
    public class RoleController : Controller
    {
        private IRepository repository;

        public RoleController(IRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List()
        {
            IEnumerable<RoleViewModel> roles = this.repository.Roles.Select(_ => new RoleViewModel {  RoleId = _.RoleId, Name = _.Name});
            return this.View(roles);
        }

        public ViewResult Edit(int roleId)
        {
            RoleViewModel viewRole = new RoleViewModel(this.repository.Roles.FirstOrDefault(_ => _.RoleId == roleId));
            return this.View(viewRole);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                this.repository.RoleUpdate(role);
                this.TempData["message"] = string.Format("Изменения в роли \"{0}\" были сохранены", role.Name);
                return this.RedirectToAction("List");
            }
            else
            {
                // Что-то не так со значениями данных
                return this.View(role);
            }
        }

        // Создание роли
        public ViewResult Create()
        {
            return this.View("Edit", new RoleViewModel());
        }

        // Удаление роли из базы
        [HttpPost]
        public ActionResult Delete(int roleId)
        {
            if (!this.repository.AppUsers.Any(_ => _.RoleId == roleId))
            {
                Role deletedRole = this.repository.RoleDelete(roleId);
                if (deletedRole != null)
                {
                    this.TempData["message"] = string.Format("Роль \"{0}\" успешно удалена.", deletedRole.Name);
                }
            }
            else
            {
                this.TempData["message"] = string.Format("Роль \"{0}\" нельзя удалить пока есть пользователи в этой роли.",
                    this.repository.Roles.FirstOrDefault(_ => _.RoleId == roleId).Name);
            }

            return this.RedirectToAction("List");
        }
    }
}