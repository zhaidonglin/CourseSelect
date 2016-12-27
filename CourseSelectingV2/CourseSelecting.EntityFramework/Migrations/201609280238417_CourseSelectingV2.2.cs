namespace CourseSelecting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseSelectingV22 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CsStudentCourseTimes", "StudentId");
            AddForeignKey("dbo.CsStudentCourseTimes", "StudentId", "dbo.CsStudents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CsStudentCourseTimes", "StudentId", "dbo.CsStudents");
            DropIndex("dbo.CsStudentCourseTimes", new[] { "StudentId" });
        }
    }
}
