using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using TodoList.Domain.Model;

namespace TodoList.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Tasks = new HashSet<UserTask>();
        }

        public virtual ICollection<UserTask> Tasks { get; set; }
    }
}
