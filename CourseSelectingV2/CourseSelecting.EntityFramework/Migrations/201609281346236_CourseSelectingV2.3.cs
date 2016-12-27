namespace CourseSelecting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseSelectingV23 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CsStudentExamTimes", "StudentId");
            AddForeignKey("dbo.CsStudentExamTimes", "StudentId", "dbo.CsStudents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CsStudentExamTimes", "StudentId", "dbo.CsStudents");
            DropIndex("dbo.CsStudentExamTimes", new[] { "StudentId" });
        }
    }
}
