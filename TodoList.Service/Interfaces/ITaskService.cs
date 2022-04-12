using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Model;
using TodoList.Domain.Wrapper;

namespace TodoList.Service
{
    public interface ITaskService
    {
        Task<PagedResult<UserTask>> GetAllUserTasks(string userId);

        Task<Result<UserTask>> GetTaskById(int id);

        Task AddTask(UserTask task);

        Task<IEnumerable<UserTaskStatus>> GetTaskStatuses();

        Task<Result<bool>> UpdateTaskActive(int taskId, bool isActive);

        Task<Result<bool>> UpdateTask(UserTask task);

        Task<Result<bool>> DeleteTask(int id);
    }
}
