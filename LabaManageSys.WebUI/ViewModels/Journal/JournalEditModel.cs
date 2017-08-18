using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.ViewModels.Journal
{
    public class JournalEditModel
    {
        public string CourseName { get; set; }

        public IEnumerable<UserModel> Users { get; set; }

        public IEnumerable<LessonModel> Lessons { get; set; }

        public IEnumerable<UserLesson> UsersInLesson { get; set; }
    }
}