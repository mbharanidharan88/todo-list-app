using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Model;

namespace TodoList.Data.Configurations
{
    internal abstract class BaseEntityConfiguration<TEntity> where TEntity : BaseEntity
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}
