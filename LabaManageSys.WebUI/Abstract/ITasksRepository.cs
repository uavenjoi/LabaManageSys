using System.Collections.Generic;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.Abstract
{
    public interface ITasksRepository
    {
        IEnumerable<TaskModel> Tasks { get; }

        int GetTasksCount();

        IEnumerable<TaskModel> GetTasksByFilter(FilterModel filterModel, int page, int pageSize);

        TaskModel TaskDelete(int taskId);

        TaskModel GetTaskById(int taskId);

        FilterLists GetFilterLists();

        void TaskUpdate(TaskModel task);
        
        void TaskAddAll(IEnumerable<TaskModel> tasks);
    }
}
