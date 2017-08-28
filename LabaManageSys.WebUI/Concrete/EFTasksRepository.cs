using System;
using System.Collections.Generic;
using System.Linq;
using LabaManageSys.Domain.Abstract;
using LabaManageSys.Domain.EntitiesModel;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.Concrete
{
    public class EFTasksRepository : ITasksRepository
    {
        private IEFDbContext context;

        public EFTasksRepository(IEFDbContext cont)
        {
            this.context = cont;
        }

        public IEnumerable<TaskModel> Tasks
        {
            get
            {
                return this.context.Tasks.Select(_ => new TaskModel
                {
                    TaskId = _.TaskId,
                    Author = _.Author,
                    Level = _.Level,
                    Text = _.Text,
                    Topic = _.Topic
                }).ToList();
            }
        }

        public FilterLists GetFilterLists()
        {
            var filterLists = new FilterLists
            {
                Authors = this.context.Tasks.Select(_ => _.Author).Distinct().ToList(),
                Topics = this.context.Tasks.Select(_ => _.Topic).Distinct().ToList(),
                Levels = this.context.Tasks.Select(_ => _.Level.ToString()).Distinct().ToList()
            };
            return filterLists;
        }

        public TaskModel GetTaskById(int taskId)
        {
            return new TaskModel(this.context.Tasks.FirstOrDefault(_ => _.TaskId == taskId));
        }

        public IEnumerable<TaskModel> GetTasksByFilter(FilterModel filterModel, int page, int pageSize)
        {
            return this.context.Tasks.Where(_ => (filterModel.Author == null || filterModel.Author == string.Empty || _.Author == filterModel.Author)
                         && (filterModel.Level == 0 || _.Level == filterModel.Level)
                         && (filterModel.Topic == null || filterModel.Topic == string.Empty || _.Topic == filterModel.Topic))
                .OrderBy(_ => _.TaskId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(_ => new TaskModel
                {
                    TaskId = _.TaskId,
                    Author = _.Author,
                    Level = _.Level,
                    Text = _.Text,
                    Topic = _.Topic
                }).ToList();
        }

        public int GetTasksCount()
        {
            return this.context.Tasks.Count();
        }

        public void TaskAddAll(IEnumerable<TaskModel> tasks)
        {
            foreach (var task in tasks)
            {
                this.TaskUpdate(task);
            }
        }

        public TaskModel TaskDelete(int taskId)
        {
            Task taskDbentry = this.context.Tasks.Find(taskId);
            if (taskDbentry != null)
            {
                this.context.Tasks.Remove(taskDbentry);
                this.context.SaveChanges();
            }

            return new TaskModel(taskDbentry);
        }

        public void TaskUpdate(TaskModel task)
        {
            if (task.TaskId == 0)
            {
                this.context.Tasks.Add(new Task
                {
                    Author = task.Author,
                    Level = task.Level,
                    Text = task.Text,
                    Topic = task.Topic
                });
            }
            else
            {
                Task taskDbentry = this.context.Tasks.Find(task.TaskId);
                if (taskDbentry != null)
                {
                    taskDbentry.Author = task.Author;
                    taskDbentry.Level = task.Level;
                    taskDbentry.Topic = task.Topic;
                    taskDbentry.Text = task.Text;
                }
            }

            this.context.SaveChanges();
        }
    }
}