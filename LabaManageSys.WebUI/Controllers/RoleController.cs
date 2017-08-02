using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.Role;

namespace LabaManageSys.WebUI.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private IRepository repository;

        public RoleController(IRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List()
        {
            ListViewModel roles = new ListViewModel { Roles = this.repository.RoleModels };
            return this.View(roles);
        }

        public ViewResult Edit(int roleId)
        {
            EditViewModel viewRole = new EditViewModel { Role = this.repository.RoleModels.FirstOrDefault(_ => _.RoleId == roleId) };
            return this.View(viewRole);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(RoleModel role)
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
            return this.View("Edit", new EditViewModel { Role = new RoleModel() });
        }

        // Удаление роли из базы
        [HttpPost]
        public ActionResult Delete(int roleId)
        {
            if (!this.repository.UserModels.Any(_ => _.RoleId == roleId))
            {
                RoleModel deletedRole = this.repository.RoleDelete(roleId);
                if (deletedRole != null)
                {
                    this.TempData["message"] = string.Format("Роль \"{0}\" успешно удалена.", deletedRole.Name);
                }
            }
            else
            {
                this.TempData["message"] = string.Format("Роль \"{0}\" нельзя удалить пока есть пользователи в этой роли.", this.repository.RoleModels.FirstOrDefault(_ => _.RoleId == roleId).Name);
            }

            return this.RedirectToAction("List");
        }
    }
}