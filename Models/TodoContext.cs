using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class TodoContext: DbContext
    {
        public TodoContext()
        {
        }
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public virtual DbSet<TodoItem> TodoItems { get; set; } = null!;     // null免除演算子(!) nullではないことを保証する
    }
}
