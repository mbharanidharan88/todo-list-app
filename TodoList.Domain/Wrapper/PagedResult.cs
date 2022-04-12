using System.Collections.Generic;

namespace TodoList.Domain.Wrapper
{
    public class PagedResult<TEntity> : Result<TEntity>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<TEntity> PagedData { get; set; }
    }
}
