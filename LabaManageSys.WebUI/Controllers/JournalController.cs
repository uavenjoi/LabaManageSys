using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.Journal;
using LabaManageSys.WebUI.ViewModels.TeachPlan;

namespace LabaManageSys.WebUI.Controllers
{
    [Authorize(Roles = "Teachers")]
    public class JournalController : Controller
    {
        private ILessonsRepository lessonRepository;
        private IUsersRepository userRepository;
        private int pageSize = 10;

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

        public ActionResult Edit(int courseId, int page = 1)
        {
            JournalEditModel model = new JournalEditModel
                {
                    Course = this.lessonRepository.GetCourseById(courseId),
                    Lessons = this.lessonRepository.GetLessonsByCourse(courseId, page, this.pageSize),
                    Users = this.userRepository.GetUsersInRole("Students"),
                    PagingInfo = 
                    new PagingInfo
                    {
                        CurrentPage = page,
                        TotalItems = this.lessonRepository.LessonsCountByCourse(courseId),
                        ItemsPerPage = this.pageSize
                    }
            };
            return this.View(model);
        }

        [HttpPost]
        public void AddUserMissLesson()
        {
            var userId = Request.Form.GetValues("userId")[0];
            var lessonId = Request.Form.GetValues("lessonId")[0];
            var userLesson = new UserLesson(lessonId, userId);
            this.lessonRepository.AddUserMissLesson(userLesson);
        }

        [HttpPost]
        public void RemoveUserMissLesson()
        {
            var userId = Request.Form.GetValues("userId")[0];
            var lessonId = Request.Form.GetValues("lessonId")[0];
            var userLesson = new UserLesson(lessonId, userId);
            this.lessonRepository.RemoveUserMissLesson(userLesson);
        }

        [HttpPost]
        public ActionResult Edit(JournalEditModel journal)
        {
            if (ModelState.IsValid)
            {
                // this.lessonRepository.UpdateUsersMissLessons(journal.UsersMissLesson, journal.Course.CourseId);
                this.TempData["message"] = string.Format("Изменения в журнале посещений по \"{0}\" были сохранены", journal.Course.Name);
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