namespace Teacher_011.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitailCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeacherLogs",
                c => new
                    {
                        TeacherLogId = c.Int(nullable: false, identity: true),
                        InstitudeName = c.String(nullable: false, maxLength: 30),
                        SubjcetTopic = c.String(nullable: false, maxLength: 40),
                        CourseDuration = c.Int(nullable: false),
                        TeachingAbility = c.String(nullable: false, maxLength: 20),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherLogId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        TeacherName = c.String(nullable: false, maxLength: 40),
                        BirthDate = c.DateTime(nullable: false, storeType: "date"),
                        Gender = c.Int(nullable: false),
                        CourseName = c.String(nullable: false, maxLength: 30),
                        ExpectedSalary = c.Decimal(nullable: false, storeType: "money"),
                        Picture = c.String(nullable: false, maxLength: 30),
                        IsReadyToTeachAnySubject = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId);

            CreateStoredProcedure("dbo.DeleteTeacher",
                 id => new { id = id.Int() },
                 "DELETE FROM Teachers WHERE TeacherId=@id");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherLogs", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.TeacherLogs", new[] { "TeacherId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.TeacherLogs");
            DropStoredProcedure("dbo.DeleteTeacher");
        }
    }
}
