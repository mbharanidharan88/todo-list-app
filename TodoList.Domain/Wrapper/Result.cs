using System;

namespace TodoList.Domain.Wrapper
{
    public class Result <TEntity>
    {
        public bool IsSucceeded { get; set; }

        public TEntity Data { get; set; }

        public Exception Error { get; set; }

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }
    }
}
