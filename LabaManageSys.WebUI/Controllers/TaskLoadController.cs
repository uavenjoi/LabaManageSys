using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;
using LabaManageSys.XML.Abstract;
using LabaManageSys.XML.Models;

namespace LabaManageSys.WebUI.Controllers
{
    [Authorize(Roles = "Teachers")]
    public class TaskLoadController : Controller
    {
        private IxmlProcessor processor;
        private ITasksRepository repository;

        public TaskLoadController(IxmlProcessor proc, ITasksRepository repo)
        {
            this.processor = proc;
            this.repository = repo;
        }

        // GET: TaskLoad
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Downloadfile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
            string fileName = "tasks.xml";

            var tasks = this.repository.Tasks.Select(_ => 
            new TaskXMLModel
            {
                TaskId = _.TaskId,
                Author = _.Author,
                Level = _.Level,
                Topic = _.Topic,
                Text = _.Text
            }).ToList();
            this.processor.DownloadTasksToFile(tasks, path + fileName);
            this.TempData["message"] = string.Format("Отправка файла началась.");
            return this.File(path + fileName, "text/xml", fileName);
        }

        [HttpPost]
        public ActionResult Uploadfile(HttpPostedFileBase xmlfile = null)
        {
            if (xmlfile != null && xmlfile.ContentLength > 0 && xmlfile.ContentType == "text/xml")
            {
                var tasks = this.processor.UploadTasksFromFile(xmlfile.InputStream);
                this.repository.TaskAddAll(tasks.Select(_ => 
                new TaskModel
                {
                    TaskId = _.TaskId,
                    Author = _.Author,
                    Level = _.Level,
                    Topic = _.Topic,
                    Text = _.Text
                }).ToList());
            }
            
            return this.RedirectToAction("List", "TaskManage");
        }
    }
}