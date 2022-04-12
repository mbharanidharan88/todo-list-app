using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Data;
using TodoList.Domain.Enums;
using TodoList.Domain.Model;
using System.Linq;
using TodoList.Domain.Extensions;

namespace TodoList.Business
{
    public class TaskBusiness : ITaskBusiness
    {
        #region Fields

        private readonly IUserTaskRepository _userTaskRepository;

        #endregion

        #region Constructor

        public TaskBusiness(IUserTaskRepository userTaskRepository)
        {
            _userTaskRepository = userTaskRepository;
        }

        #endregion

        #region Interface Methods

        /// <summary>
        /// Get All User Tasks
        /// </summary>
        /// <param name="userId"></param>
        /// <returns cref="Task<IEnumerable<UserTask>>"></returns>
        public async Task<IEnumerable<UserTask>> GetAllUserTasks(string userId)
        {
            var result = await _userTaskRepository.GetAllUserTasks(userId);

            try
            {

                if (result.Any())
                {
                    result.ToList().ForEach(x =>
                    {
                        x.TaskStatusStr = ((TaskStatusEnum)Convert.ToInt32(x.TaskStatus)).ToDescription();
                    });
                }
            }
            catch 
            {
                // Have to use the wrapper to send data. something which I've implemented in TodoList.Service with Result Object
            }

            return result;
        }

        /// <summary>
        /// Get Task By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns cref="Task<UserTask></UserTask>"></returns>
        public async Task<UserTask> GetTaskById(int id)
        {
            return await _userTaskRepository.GetTaskById(id);
        }

        /// <summary>
        /// Add Task
        /// </summary>
        /// <param name="task"></param>
        /// <returns cref="Task"></returns>
        public async Task AddTask(UserTask task)
        {
            await _userTaskRepository.AddTask(task);
        }

        /// <summary>
        /// Get Task Statuses
        /// </summary>
        /// <returns cref="Task<IEnumerable<UserTaskStatus>>"></returns>
        public async Task<IEnumerable<UserTaskStatus>> GetTaskStatuses()
        {
            return await Task.Run(() => getTaskStatuses());
        }

        /// <summary>
        /// Update Task Active
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTaskActive(int taskId, bool isActive)
        {
            return await _userTaskRepository.UpdateTaskActive(taskId, isActive);
        }

        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTask(UserTask task)
        {
            return await _userTaskRepository.UpdateTask(task);
        }

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTask(int id)
        {
            return await _userTaskRepository.DeleteTask(id);
        }

        #endregion

        #region Private Methods

        private IEnumerable<UserTaskStatus> getTaskStatuses()
        {
            var list = new List<UserTaskStatus>();
            var values = Enum.GetValues(typeof(TaskStatusEnum));

            foreach (TaskStatusEnum val in values)
            {
                list.Add(new UserTaskStatus
                {
                    Id = (int)val,
                    Description = val.ToDescription()
                });
            }

            return list;
        }

        #endregion
    }
}
