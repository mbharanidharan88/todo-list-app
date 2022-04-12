using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Service;
using TodoList.UI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using TodoList.ResourceLibrary;

namespace TodoList.UI.Controllers
{
    [Authorize]
    public class TaskController : BaseController
    {
        #region Fields

        private readonly ITaskService _taskService;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        #endregion

        #region Contructor

        public TaskController(ITaskService taskService,
                                IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _taskService = taskService;
            _sharedLocalizer = sharedLocalizer;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var model = new TaskListViewModel();
            var taskResult = await _taskService.GetAllUserTasks(UserId);

            if (taskResult.IsSucceeded)
            {
                model.Tasks = taskResult.PagedData.ToList();
            }

            return View(model);
        }

        public IActionResult Add()
        {
            return View(new AddTaskViewModel());
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var model = new AddTaskViewModel();
            var statuses = await _taskService.GetTaskStatuses();

            if (id.HasValue)
            {
                var task = await _taskService.GetTaskById(id.Value);

                if (task.IsSucceeded)
                {
                    model.Task = task.Data;
                }

                model.PageTitle = _sharedLocalizer["Edit Task"];
            }
            else
            {
                model.PageTitle = _sharedLocalizer["Add Task"];
            }

            model.TaskStatuses = statuses.ToList();

            return PartialView("_Upsert", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AddTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Task.ApplicationUserId = UserId;

                if (model.Task.Id > 0)
                {
                    await _taskService.UpdateTask(model.Task);
                }
                else
                {
                    await _taskService.AddTask(model.Task);
                }
                

                return RedirectToAction("index");
            }

            return new JsonResult(new { Value = "Ok" });
        }

        public async Task<IActionResult> UpdateTaskActive([FromQuery] int taskId, bool isActive)
        {
            var result = await _taskService.UpdateTaskActive(taskId, isActive);

            return new JsonResult(result);
        }

        public async Task<IActionResult> DeleteTask([FromQuery] int taskId)
        {
            var result = await _taskService.DeleteTask(taskId);

            return new JsonResult(result);
        }
    }
}
