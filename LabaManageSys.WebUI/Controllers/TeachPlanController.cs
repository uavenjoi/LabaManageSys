using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.TeachPlan;

namespace LabaManageSys.WebUI.Controllers
{
    // [Authorize(Roles = "Teachers")]
    public class TeachPlanController : Controller
    {
        private ILessonsRepository repository;

        public TeachPlanController(ILessonsRepository repo)
        {
            this.repository = repo;
        }

        // GET: Teacher
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel { Courses = this.repository.Courses };
            return this.View(model);
        }

        public ActionResult Edit(int courseId)
        {
            EditViewModel course = new EditViewModel
            {
                Course = this.repository.GetCourseById(courseId)
            };
            return this.View(course);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel courseModel, string dates)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (ModelState.IsValid)
            {
                var str_dates = dates.Replace(".", "/").Trim().Split(',');
                var arr_dates = new DateTime[str_dates.Length];
                for (int i = 0; i < str_dates.Length; i++)
                {
                    arr_dates[i] = DateTime.ParseExact(str_dates[i], "g", new CultureInfo("fr-FR"));
                }

                this.repository.CourseUpdate(courseModel.Course, arr_dates);
                this.TempData["message"] = string.Format("Изменения в курсе \"{0}\" были сохранены", courseModel.Course.Name);
                return this.RedirectToAction("List");
            }
            else
            {
                // Что-то не так со значениями данных
                return this.View(new EditViewModel
                {
                    Course = courseModel.Course
                });
            }
        }

        // Создание курса
        public ViewResult Create()
        {
            return this.View("Edit", new EditViewModel { Course = new CourseModel() });
        }

        // Удаление курса из базы
        [HttpPost]
        public ActionResult Delete(int courseId)
        {
           CourseModel deletedCourse = this.repository.CourseDelete(courseId);
           if (deletedCourse != null)
              {
                this.TempData["message"] = string.Format("Курс \"{0}\" успешно удален.", deletedCourse.Name);
              }

            return this.RedirectToAction("List");
        }
    }
}