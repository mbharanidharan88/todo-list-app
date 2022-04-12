using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Model;

namespace TodoList.Data
{
    public interface IUserTaskRepository
    {
        Task<IEnumerable<UserTask>> GetAllUserTasks(string userId);

        Task<UserTask> GetTaskById(int id);

        Task AddTask(UserTask task);

        Task<bool> UpdateTaskActive(int taskId, bool isActive);
        Task<bool> UpdateTask(UserTask task);

        Task<bool> DeleteTask(int id);
    }
}
