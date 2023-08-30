namespace Teacher_011.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Teacher_011.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Teacher_011.Models.TeacherDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Teacher_011.Models.TeacherDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            Teacher a = new Teacher { TeacherName = " MR.Khan", CourseName = "C#", BirthDate = DateTime.Parse("1976-07-11"), Gender = Gender.Male, ExpectedSalary=10000, IsReadyToTeachAnySubject = true, Picture = "e1.jpg" };
            a.TeacherLogs.Add(new TeacherLog { InstitudeName = "AtComputerLimited", SubjcetTopic = "SQL", CourseDuration = 5, TeachingAbility = "GOOD" });
            a.TeacherLogs.Add(new TeacherLog { InstitudeName = "AtComputerLimited", SubjcetTopic = "MVCCORE", CourseDuration = 4, TeachingAbility = "Excelent" });
            context.Teachers.Add(a);
            context.SaveChanges();
        }
    }
    }

