using System.Collections.Generic;
using TodoList.Domain.Model;

namespace TodoList.UI.Models
{
    public class TaskListViewModel : BaseViewModel
    {
        public UserTask Task { get; set; } = new UserTask();

        public IList<UserTask> Tasks { get; set; } = new List<UserTask>();
    }
}
