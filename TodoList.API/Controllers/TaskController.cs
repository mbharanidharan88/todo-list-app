using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Business;
using TodoList.Domain.Model;

namespace TodoList.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        #region Fields

        private readonly ITaskBusiness _taskBusiness;
        private readonly ILogger<TaskController> _logger;

        #endregion

        #region Constructor

        public TaskController(ITaskBusiness taskBusiness,
                                ILogger<TaskController> logger)
        {
            _taskBusiness = taskBusiness;
            _logger = logger;
        }

        #endregion

        #region API Methods

        [HttpGet]
        [Route("GetAllUserTasks/{userId}")]
        public async Task<IEnumerable<UserTask>> GetAllUserTasks([FromRoute] string userId)
        {
            return  await _taskBusiness.GetAllUserTasks(userId);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<UserTask> Get([FromRoute] int id)
        {
            return await _taskBusiness.GetTaskById(id);
        }

        [HttpPut]
        public async Task<bool> Put(UserTask task)
        {
            return await _taskBusiness.UpdateTask(task);
        }

        [HttpGet]
        [Route("{taskId}/{isActive}")]
        public async Task<bool> UpdateTaskActive([FromRoute] int taskId, bool isActive)
        {
            return await _taskBusiness.UpdateTaskActive(taskId, isActive);
        }

        [HttpPost]
        public async Task CreateTask([FromBody] UserTask task)
        {
            await _taskBusiness.AddTask(task);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _taskBusiness.DeleteTask(id);
        }

        #endregion
    }
}
