using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Business;
using TodoList.Domain.Model;

namespace TodoList.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskStatusController : ControllerBase
    {
        #region Fields

        private readonly ITaskBusiness _taskBusiness;
        private readonly ILogger<TaskStatusController> _logger;

        #endregion

        #region Constructor

        public TaskStatusController(ITaskBusiness taskBusiness,
                                ILogger<TaskStatusController> logger)
        {
            _taskBusiness = taskBusiness;
            _logger = logger;
        }

        #endregion

        #region API Methods

        [HttpGet]
        public async Task<IEnumerable<UserTaskStatus>> Get()
        {
            return await _taskBusiness.GetTaskStatuses();
        }

        #endregion
    }
}
