using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Contexts;
using Teacher_011.Models;

namespace Teacher_011.Models
{

    public enum Gender { Male = 1, Female }
    public class Teacher
    {
        public int TeacherId { get; set; }
        [Required, StringLength(40)]
        public string TeacherName { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required, StringLength(30)]
        public string CourseName { get; set; }

        [Required, Column(TypeName = "money"), DataType(DataType.Currency)]
        public decimal ExpectedSalary { get; set; }
        [Required, StringLength(30)]

        public string Picture { get; set; }
        public bool IsReadyToTeachAnySubject { get; set; }
        public virtual ICollection<TeacherLog> TeacherLogs { get; set; } = new List<TeacherLog>();
    }
    public class TeacherLog
    {
        public int TeacherLogId { get; set; }
        [Required, StringLength(30)]
        public string InstitudeName { get; set; }
        [Required, StringLength(40)]
        public string SubjcetTopic { get; set; }
        [Required]
        public int CourseDuration { get; set; }
        [Required, StringLength(20)]
        public string TeachingAbility { get; set; }
        [Required, ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
    public class TeacherDbContext : DbContext
    {
        public TeacherDbContext()
        {
            //Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherLog> TeacherLogs { get; set; }
    }
    //public class DbInitializer : DropCreateDatabaseIfModelChanges<TeacherDbContext>
    //{
    //    protected override void Seed(TeacherDbContext context)
    //    {
    //        Teacher a = new Teacher { TeacherName = "A1", PreferSubject = "C#", BirthDate = DateTime.Parse("1976-07-11"), Gender = Gender.Male, IsReadyToTeachAnySubject = true, Picture = "e1.jpg" };
    //        a.Subjects.Add(new Subject { SubjectName = "SQL", SubjcetTopic = "DATABASE", NumberOfTopic = 5, TeachingAbility = "GOOD" });
    //        a.Subjects.Add(new Subject { SubjectName = "MVC", SubjcetTopic = "MVCCORE", NumberOfTopic = 4, TeachingAbility = "Excelent" });
    //        context.Teachers.Add(a);
    //        context.SaveChanges();

    //    
    //    }
    //}
    //    public Configuration()
    //    {
    //        AutomaticMigrationsEnabled = false;
    //    }

    //    protected override void Seed(Teacher_011.Models.TeacherDbContext context)
    //    {
    //        Teacher a = new Teacher
    //        {
    //            TeacherName = "A1",
    //            PreferSubject = "C#",
    //            BirthDate = DateTime.Parse("1976-07-11"),
    //            Gender = Gender.Male,
    //            IsReadyToTeachAnySubject = true,
    //            ExpectedSalary = 10000,
    //            Picture = "e1.jpg"
    //        };
    //        a.Subjects.Add(new Subject { SubjectName = "SQL", SubjcetTopic = "DATABASE", NumberOfTopic = 5, TeachingAbility = "GOOD" });
    //        a.Subjects.Add(new Subject { SubjectName = "MVC", SubjcetTopic = "MVCCORE", NumberOfTopic = 4, TeachingAbility = "Excelent" });
    //        context.Teachers.Add(a);
    //        context.SaveChanges();
    //    }
    //}
    //}
    //public override void Up()
    //{
    //    CreateTable(
    //        "dbo.Subjects",
    //        c => new
    //        {
    //            SubjectId = c.Int(nullable: false, identity: true),
    //            SubjectName = c.String(nullable: false, maxLength: 30),
    //            SubjcetTopic = c.String(nullable: false, maxLength: 40),
    //            NumberOfTopic = c.Int(nullable: false),
    //            TeachingAbility = c.String(nullable: false, maxLength: 20),
    //            TeacherId = c.Int(nullable: false),
    //        })
    //        .PrimaryKey(t => t.SubjectId)
    //        .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
    //        .Index(t => t.TeacherId);

    //    CreateTable(
    //        "dbo.Teachers",
    //        c => new
    //        {
    //            TeacherId = c.Int(nullable: false, identity: true),
    //            TeacherName = c.String(nullable: false, maxLength: 40),
    //            BirthDate = c.DateTime(nullable: false, storeType: "date"),
    //            Gender = c.Int(nullable: false),
    //            PreferSubject = c.String(nullable: false, maxLength: 30),
    //            Picture = c.String(nullable: false, maxLength: 30),
    //            IsReadyToTeachAnySubject = c.Boolean(nullable: false),
    //        })
    //        .PrimaryKey(t => t.TeacherId);
    //    CreateStoredProcedure("dbo.DeleteTeacher",
    //        id => new { id = id.Int() }, "DELETE FROM Teachers WHERE TeacherId=@id");

    //}



    //        Teacher a = new Teacher { TeacherName = "Habibul Haq", CourseName = "C#", BirthDate = DateTime.Parse("1976-07-11"), Gender = Gender.Male,  ExpectedSalary=10000,IsReadyToTeachAnySubject = true, Picture = "e1.jpg" };
    //    a.TeacherLogs.Add(new TeacherLog { InstitudeName = "AtComputerLimited", SubjcetTopic = "SQL", CourseDuration = 5, TeachingAbility = "GOOD" });
    //a.TeacherLogs.Add(new TeacherLog { InstitudeName = "AtComputerLimited", SubjcetTopic = "MVCCORE", CourseDuration = 4, TeachingAbility = "Excelent" });
    //context.Teachers.Add(a);
    //context.SaveChanges();
}