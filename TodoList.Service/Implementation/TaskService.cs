using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data;
using TodoList.Domain.Model;
using TodoList.Domain.Wrapper;
using TodoList.ResourceLibrary;

namespace TodoList.Service
{
    public class TaskService : BaseService, ITaskService
    {

        #region Fields

        #endregion

        #region Constructor

        public TaskService(IStringLocalizer<SharedResource> sharedLocalizer,
                HttpClient httpClient,
                IHttpContextAccessor httpContextAccessor)
            : base(sharedLocalizer, httpClient, httpContextAccessor)
        {
        }

        #endregion

        #region Service Methods

        public async Task AddTask(UserTask task)
        {
            var uri = "Task";
            var content = JsonContent.Create(task);
            var response = await httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<PagedResult<UserTask>> GetAllUserTasks(string userId)
        {
            var result = new PagedResult<UserTask>();

            try
            {
                var uri = $"Task/GetAllUserTasks/{userId}";
                var response = await httpClient.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    var errorString = await response.Content.ReadAsStringAsync();

                    result.Error = new Exception(errorString);
                }
                else
                {
                    var responseData = await response.Content.ReadAsStringAsync();

                    result.PagedData = JsonConvert.DeserializeObject<IEnumerable<UserTask>>(responseData).ToList();

                    result.IsSucceeded = true;
                    result.SuccessMessage = sharedLocalizer["Task status updated successfully"];
                }
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }

            return result;
        }

        public async Task<Result<UserTask>> GetTaskById(int id)
        {
            var result = new Result<UserTask>();

            try
            {
                var uri = $"Task/{id}";
                var response = await httpClient.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    var errorString = await response.Content.ReadAsStringAsync();

                    result.Error = new Exception(errorString);
                }
                else
                {
                    var responseData = await response.Content.ReadAsStringAsync();

                    result.Data = JsonConvert.DeserializeObject<UserTask>(responseData);

                    result.IsSucceeded = true;
                }
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }

            return result;
        }

        public async Task<IEnumerable<UserTaskStatus>> GetTaskStatuses()
        {
            var uri = "TaskStatus";
            var response = await httpClient.GetAsync(uri);
            var statuses = new List<UserTaskStatus>();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                statuses = JsonConvert.DeserializeObject<IEnumerable<UserTaskStatus>>(data).ToList();
            }

            return statuses;
        }

        public async Task<Result<bool>> UpdateTaskActive(int taskId, bool isActive)
        {
            var result = new Result<bool>();

            try
            {
                var uri = $"Task/{taskId}/{isActive}";
                var response = await httpClient.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    var errorString = await response.Content.ReadAsStringAsync();

                    result.Error = new Exception(errorString);
                }
                else
                {
                    var responseData = await response.Content.ReadAsStringAsync();

                    result.Data = JsonConvert.DeserializeObject<bool>(responseData);
                    result.IsSucceeded = true;
                    result.SuccessMessage = sharedLocalizer["Task status updated successfully"];
                }
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<Result<bool>> UpdateTask(UserTask task)
        {
            var result = new Result<bool>();

            try
            {
                var uri = "Task";
                var content = JsonContent.Create(task);
                var response = await httpClient.PutAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorString = await response.Content.ReadAsStringAsync();

                    result.Error = new Exception(errorString);
                }
                else
                {
                    var responseData = await response.Content.ReadAsStringAsync();

                    result.Data = JsonConvert.DeserializeObject<bool>(responseData);
                    result.IsSucceeded = true;
                    result.SuccessMessage = sharedLocalizer["Task updated successfully"];
                }
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }

            return result;
        }

        public async Task<Result<bool>> DeleteTask(int id)
        {
            var result = new Result<bool>();

            try
            {
                var uri = $"Task/{id}";
                var response = await httpClient.DeleteAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    var errorString = await response.Content.ReadAsStringAsync();

                    result.Error = new Exception(errorString);
                }
                else
                {                    
                    result.IsSucceeded = true;
                    result.SuccessMessage = sharedLocalizer["Task deleted successfully"];
                }
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }

            return result;
        }

        #endregion
    }
}
