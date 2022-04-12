using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Domain.Model
{
    public class UserTask : BaseEntity
    {
        [Required]
        public string TaskName { get; set; }

        [Required]
        public string TaskStatus { get; set; }

        [Required]
        public DateTime TaskDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [NotMapped]
        public string TaskStatusStr { get; set; }
    }
}
