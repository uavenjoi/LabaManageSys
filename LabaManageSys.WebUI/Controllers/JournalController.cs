using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.ViewModels.Journal;
using LabaManageSys.WebUI.ViewModels.TeachPlan;

namespace LabaManageSys.WebUI.Controllers
{
    public class JournalController : Controller
    {
        private ILessonsRepository lessonRepository;
        private IUsersRepository userRepository;

        public JournalController(ILessonsRepository repoL, IUsersRepository repoU)
        {
            this.lessonRepository = repoL;
            this.userRepository = repoU;
        }

        // GET: Journal
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel { Courses = this.lessonRepository.Courses };
            return this.View(model);
        }

        public ActionResult Edit(int courseId)
        {
            JournalEditModel model = new JournalEditModel
                {
                    CourseName = this.lessonRepository.GetCourseById(courseId).Name,
                    Lessons = this.lessonRepository.GetLessonsByCourse(courseId),
                    Users = this.userRepository.GetUsersInRole("Students"),
                    UsersInLesson = this.lessonRepository.GetUsersLessonsByCourse(courseId)
                };
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Edit(JournalEditModel journal)
        {
            if (ModelState.IsValid)
            {
                this.TempData["message"] = string.Format("Изменения в журнале посещений по \"{0}\" были сохранены", journal.CourseName);
                return this.RedirectToAction("List");
            }
            else
            {
                // Что-то не так со значениями данных
                return this.View(journal);
            }
        }
    }
}