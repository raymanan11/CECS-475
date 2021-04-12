using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models {
    public class AnTodoContext : DbContext {
        public AnTodoContext(DbContextOptions<AnTodoContext> options)
            : base(options) {
        }

        public DbSet<AnTodoItem> TodoItems { get; set; }
    }
}