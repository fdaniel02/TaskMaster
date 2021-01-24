using Microsoft.EntityFrameworkCore;
using TaskMaster.Domain.Models;

namespace TaskMaster.Domain
{
    public class TaskMasterContext : DbContext
    {
        public TaskMasterContext(DbContextOptions<TaskMasterContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ActionItem> ActionItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ProjectState> ProjectState { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<ActionItem>().ToTable("ActionItem");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<ProjectState>().ToTable("ProjectState");

            modelBuilder.Entity<ProjectState>().HasData(
                new { ID = 1, Name = "Inbox" },
                new { ID = 2, Name = "Next" },
                new { ID = 3, Name = "Scheduled" },
                new { ID = 4, Name = "Waiting" },
                new { ID = 5, Name = "Delegated" },
                new { ID = 6, Name = "Later" }
            );
        }
    }
}