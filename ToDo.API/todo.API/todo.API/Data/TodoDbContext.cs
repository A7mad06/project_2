using Microsoft.EntityFrameworkCore;
using todo.API.Models;

namespace todo.API.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }


    }
}
