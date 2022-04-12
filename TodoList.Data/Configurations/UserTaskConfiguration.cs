using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoList.Data.Configurations
{
    internal class UserTaskConfiguration : BaseEntityConfiguration<UserTask>
    {
        public override void Configure(EntityTypeBuilder<UserTask> entity)
        {
            entity.ToTable("UserTask");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.ApplicationUserId).IsRequired();
            entity.Property(c => c.IsActive).IsRequired();
            entity.Property(c => c.TaskName).IsRequired();
            entity.HasOne(c => c.User);
        }
    }
}
