using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.Task;

namespace LabaManageSys.WebUI.Controllers
{
    public class TaskManageController : Controller
    {
        private const int PageSize = 5;

        private ITasksRepository repository;

        public TaskManageController(ITasksRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List(FilterModel filter, int page = 1)
        {
            ListViewModel model = 
                new ListViewModel
                {
                    Tasks = this.repository.GetTasksByFilter(filter, page, PageSize),
                    Filter = filter,
                    Lists = this.repository.GetFilterLists(),
                    PagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = PageSize }
                };
            model.SetTotalItems();
            return this.View(model);
        }

        public ActionResult GetTasksDataJson(FilterModel filter, string author, string topic, string level, int page)
        {
            filter.Author = (author == string.Empty) ? string.Empty : (author != null) ? author : filter.Author;
            filter.Topic = (topic == string.Empty) ? string.Empty : (topic != null) ? topic : filter.Topic;
            filter.Level = (level == string.Empty) ? 0 : (level != null) ? int.Parse(level) : filter.Level;
            var result = Json(this.repository.GetTasksByFilter(filter, page, PageSize), JsonRequestBehavior.AllowGet);
            return result;
        }

        public ViewResult Edit(int userId)
        {
            EditViewModel model = new EditViewModel
            {
                Task = this.repository.GetTaskById(userId)
            };
            return this.View(model);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                this.repository.TaskUpdate(task);
                this.TempData["message"] = string.Format("Изменения в задании были сохранены");
                return this.RedirectToAction("List");
            }
            else
            {
                // Что-то не так со значениями данных
                return this.View(new EditViewModel
                {
                    Task = task
                });
            }
        }

        // Создание задания
        public ViewResult Create()
        {
            var model = new EditViewModel
            {
                Task = new TaskModel()
            };
            return this.View("Edit", model);
        }

        // Удаление задания из базы
        [HttpPost]
        public ActionResult Delete(int taskId)
        {
            TaskModel deletedTask = this.repository.TaskDelete(taskId);
            if (deletedTask != null)
            {
                this.TempData["message"] = string.Format("Задание успешно удалено.");
            }

            return this.RedirectToAction("List");
        }
    }
}
