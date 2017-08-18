using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.Abstract
{
    public interface ILessonsRepository
    {
        IEnumerable<CourseModel> Courses { get; }

        IEnumerable<LessonModel> Lessons { get; }

        IEnumerable<LessonModel> GetLessonsByCourse(int courseId);

        bool AreLessonsInCourse(int courseId);

        CourseModel CourseDelete(int courseId);

        CourseModel GetCourseById(int courseId);

        IEnumerable<DateTime> GetDatesByCourse(int courseId);

        void CourseUpdate(CourseModel course, IEnumerable<DateTime> dates);

        void AddLessonsToCourse(int courseId, IEnumerable<DateTime> dates);

        IEnumerable<UserLesson> GetUsersLessonsByCourse(int courseId);
    }
}
