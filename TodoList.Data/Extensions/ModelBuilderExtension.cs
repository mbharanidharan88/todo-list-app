using Microsoft.EntityFrameworkCore;
using TodoList.Data.Configurations;
using TodoList.Domain.Model;

namespace TodoList.Data.Extensions
{
    internal static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(
          this ModelBuilder modelBuilder,
          BaseEntityConfiguration<TEntity> entityConfiguration) where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
        }
    }
}
