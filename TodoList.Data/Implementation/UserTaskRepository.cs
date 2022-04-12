using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Model;

namespace TodoList.Data
{
    public class UserTaskRepository : BaseRepository<UserTask>, IUserTaskRepository
    {
        #region Constructor

        public UserTaskRepository(TodoListDbContext context) : base(context)
        {

        }

        #endregion

        #region Public Methods

        public async Task AddTask(UserTask task)
        {
            await AddAsync(task);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<UserTask>> GetAllUserTasks(string userId)
        {
            return await GetAll(x => x.ApplicationUserId == userId);
        }

        public async Task<UserTask> GetTaskById(int id)
        {
            return await GetById(x => x.Id == id);
        }

        public async Task<bool> UpdateTaskActive(int taskId, bool isActive)
        {
            var task = await GetById(x => x.Id == taskId);

            if (task == null)
            {
                return false;
            }

            task.IsActive = isActive;

            var result = await SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateTask(UserTask task)
        {
            return await UpdateAsync(task, task.Id);
            //var taskFind = await GetById(x => x.Id == task.Id);

            //if (taskFind == null) return false;

            ////taskFind.TaskName = task.TaskName;
            ////taskFind.TaskDate = task.TaskDate;
            ////taskFind.TaskStatus = task.TaskStatus;
            ////taskFind.IsActive = task.IsActive;

            //task.

            //var result = await SaveChangesAsync();

            //return result > 0;
        }

        public async Task<bool> DeleteTask(int id)
        {
            return await RemoveAsync(id);
        }

        #endregion
    }
}
