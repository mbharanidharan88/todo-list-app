using System.ComponentModel;

namespace TodoList.Domain.Enums
{
    public enum TaskStatusEnum
    {
        [Description("Not Started")]
        NotStarted = 0,
        [Description("In Progress")]
        InProgress = 1,
        [Description("Done")]
        Done = 2
    }
}
