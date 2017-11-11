using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace ODataSample.Contexts
{
    public class ProjectsContext : DbContext
    {
        public ProjectsContext() : base("ProjectsConnectionString") { Database.SetInitializer(new ProjectDBInitializer()); }
        public ProjectsContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            //Database.SetInitializer(new ProjectDBInitializer());
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<ProjectStatusText> ProjectStatusTexts { get; set; }
        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Project>().HasRequired(p => p.Updator).WithRequiredDependent().WillCascadeOnDelete(true);
            //modelBuilder.Entity<Project>().HasRequired(p => p.Creator).WithRequiredDependent().WillCascadeOnDelete(true);
        }
    }

    public class ProjectDBInitializer : DropCreateDatabaseAlways<ProjectsContext>
    {
        public ProjectDBInitializer()
        {
        }

        protected override void Seed(ProjectsContext context)
        {
            context.Languages.AddRange(new[]
            {
                new Language { Description = "Spanish", IsActive = true, LanguageId = 1, Name = "es" },
                new Language { Description = "English", IsActive = true, LanguageId = 2, Name = "en" }
            });

            context.ProjectStatuses.AddRange(new[]
            {
                new ProjectStatus { StatusId=1, Name="Active", Description="Active", IsActive= true },
                new ProjectStatus { StatusId=2, Name="Closed", Description="Closed", IsActive= true },
                new ProjectStatus { StatusId=3, Name="InProgress", Description="In Progress", IsActive= true }
            });

            context.ProjectStatusTexts.AddRange(new[]
            {
                new ProjectStatusText {Id = 1, LanguageId=1, StatusId = 1,Name = Path.GetRandomFileName(),Description = Path.GetRandomFileName(),IsActive=true },
                new ProjectStatusText {Id = 2, LanguageId=2, StatusId = 1,Name = "Active",Description = "Active",IsActive=true },
                new ProjectStatusText {Id = 3, LanguageId=1, StatusId = 2,Name = Path.GetRandomFileName(),Description = Path.GetRandomFileName(),IsActive=true },
                new ProjectStatusText {Id = 4, LanguageId=2, StatusId = 3,Name = "InProgress",Description = "InProgress",IsActive=true }
            });

            context.Projects.AddRange(new[]
            {
                new Project{ Cost = default(decimal),  Id=1,Name="project 1",StatusId = 1, Description="project 1", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now,  CreatedBy = 1, UpdatedBy =1},
                new Project{ Cost = default(decimal), Id=2,Name="project 2",StatusId = 1, Description="project 2", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now,  CreatedBy = 1, UpdatedBy =1},
                new Project{ Cost = default(decimal), Id=3,Name="project 3",StatusId = 1, Description="project 3", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, CreatedBy = 1, UpdatedBy =1 }
            });

            base.Seed(context);
        }
    }
}