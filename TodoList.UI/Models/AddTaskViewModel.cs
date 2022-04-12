using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Domain.Model;

namespace TodoList.UI.Models
{
    public class AddTaskViewModel : BaseViewModel
    {
        public UserTask Task { get; set; } = new UserTask();

        public IList<UserTaskStatus> TaskStatuses { get; set; } = new List<UserTaskStatus>();

        public string PageTitle { get; set; }
    }
}
