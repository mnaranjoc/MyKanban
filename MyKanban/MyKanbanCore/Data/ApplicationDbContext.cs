using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyKanbanCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyKanbanCore.Models.Board> Board { get; set; }
        public DbSet<MyKanbanCore.Models.Column> Column { get; set; }
        public DbSet<MyKanbanCore.Models.Item> Item { get; set; }
    }
}
