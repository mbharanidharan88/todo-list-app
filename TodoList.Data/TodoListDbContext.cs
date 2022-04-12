using Microsoft.EntityFrameworkCore;
using TodoList.Data.Configurations;
using TodoList.Data.Extensions;
using TodoList.Domain.Model;

namespace TodoList.Data
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new UserTaskConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserTask> Tasks { get; set; }
    }
}
