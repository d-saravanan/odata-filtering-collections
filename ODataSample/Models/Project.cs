using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODataSample
{
    public class Project //: BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        //[Required, ForeignKey("CreatedBy")]
        //public virtual User Creator { get; set; }
        //[Required, ForeignKey("UpdatedBy")]
        //public virtual User Updator { get; set; }

        public decimal Cost { get; set; }
        public long StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual ProjectStatus Status { get; set; }
    }

    public class ProjectStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ProjectStatusText> ProjectStatusTexts { get; set; }
    }

    public class ProjectStatusText
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
        public bool IsActive { get; set; }
    }

    public class Language
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class User 
    {
        public User()
        {
            this.Projects = new HashSet<Project>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }

    //public class BaseEntity : IBaseEntity
    //{
        
    //}

    public interface IBaseEntity : IAuditableEntity
    {
        string Name { get; set; }
        string Description { get; set; }
    }

    public interface IAuditableEntity
    {
        long Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime UpdatedOn { get; set; }
        long CreatedBy { get; set; }
        long UpdatedBy { get; set; }
    }
}