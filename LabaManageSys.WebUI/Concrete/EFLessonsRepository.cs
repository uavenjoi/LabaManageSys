using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LabaManageSys.Domain.Abstract;
using LabaManageSys.Domain.EntitiesModel;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.Concrete
{
    public class EFLessonsRepository : ILessonsRepository
    {
        private IEFDbContext context;

        public EFLessonsRepository(IEFDbContext cont)
        {
            this.context = cont;
        }

        public IEnumerable<CourseModel> Courses
        {
            get
            {
                return this.context.Courses.Select(_ => new CourseModel { CourseId = _.CourseId, Name = _.Name }).ToList();
            }
        }

        public IEnumerable<LessonModel> Lessons
        {
            get
            {
                return this.context.Lessons.Select(_ => new LessonModel { CourseId = _.CourseId, Date = _.Date, LessonId = _.LessonId }).ToList();
            }
        }

        public bool AreLessonsInCourse(int courseId)
        {
            return this.context.Lessons.Any(_ => _.CourseId == courseId);
        }

        public CourseModel CourseDelete(int courseId)
        {
            Course entryDb = this.context.Courses.Find(courseId);
            if (entryDb != null)
            {
                this.context.Courses.Remove(entryDb);
                this.context.SaveChanges();
            }

            return new CourseModel(entryDb);
        }

        public CourseModel GetCourseById(int courseId)
        {
            var course = this.context.Courses.FirstOrDefault(_ => _.CourseId == courseId);
            return (course != null) ? new CourseModel(course) : null;
        }

        public IEnumerable<DateTime> GetDatesByCourse(int courseId)
        {
            return this.context.Lessons.Where(_ => _.CourseId == courseId).Select(_ => _.Date).ToList();
        }

        public void CourseUpdate(CourseModel course, IEnumerable<DateTime> dates)
        {
            if (course.CourseId == 0)
            {
                this.context.Courses.Add(new Course
                {
                    Name = course.Name,
                    Lessons = dates.Select(_ => new Lesson
                    {
                        Date = _,
                        CourseId = course.CourseId,
                        AppUsers = this.context.AppUsers.Where(m => m.Role.Name == "Students").ToList()
                    }).ToList()
                });
            }
            else
            {
                Course entryDb = this.context.Courses.Find(course.CourseId);
                if (entryDb != null)
                {
                    entryDb.Name = course.Name;
                }
            }

            this.context.SaveChanges();
        }

        public IEnumerable<LessonModel> GetLessonsByCourse(int courseId)
        {
            return this.context.Lessons.Where(_ => _.CourseId == courseId).Select(_ => new LessonModel { CourseId = _.CourseId, Date = _.Date, LessonId = _.LessonId }).ToList();
        }

        public void AddLessonsToCourse(int courseId, IEnumerable<DateTime> dates)
        {
            foreach (var date in dates)
            {
                this.context.Lessons.Add(new Lesson { CourseId = courseId, Date = date, AppUsers = this.context.AppUsers.Where(_ => _.Role.Name == "Students").ToList() });
            }

            this.context.SaveChanges();
        }

        public IEnumerable<UserLesson> GetUsersLessonsByCourse(int courseId)
        {
            return this.context.Lessons.SelectMany(_ => _.AppUsers, (parent, child) => new UserLesson { LessonId = parent.LessonId, UserId = child.UserId }).ToList();
        }
    }
}