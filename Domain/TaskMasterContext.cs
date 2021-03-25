using System.IO;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain
{
    public class TaskMasterContext : DbContext
    {
        public TaskMasterContext()
        {
        }

        public TaskMasterContext(DbContextOptions<TaskMasterContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ActionItem> ActionItems { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ProjectTags> ProjectTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskMasterContext).Assembly);
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<ActionItem>().ToTable("ActionItem");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<ProjectTags>().HasKey(p => new { p.ProjectId, p.TagId });

            modelBuilder.Entity<ProjectTags>()
                .HasOne(p => p.Project)
                .WithMany(p => p.ProjectTags)
                .HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<ProjectTags>()
                    .HasOne(p => p.Tag)
                    .WithMany(p => p.ProjectTags)
                    .HasForeignKey(p => p.TagId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationBuilder builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                var configuration = builder.Build();
                var connectionString = configuration.GetConnectionString("TaskMasterContext");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}