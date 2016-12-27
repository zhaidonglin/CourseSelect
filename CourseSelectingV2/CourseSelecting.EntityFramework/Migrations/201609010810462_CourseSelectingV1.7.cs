namespace CourseSelecting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseSelectingV17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CsStudentExamTimes", "CourseTime_Id", c => c.Int());
            CreateIndex("dbo.CsStudentCourseTimes", "CourseTimeId");
            CreateIndex("dbo.CsStudentExamTimes", "CourseTime_Id");
            AddForeignKey("dbo.CsStudentCourseTimes", "CourseTimeId", "dbo.CsCourseTimes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CsStudentExamTimes", "CourseTime_Id", "dbo.CsCourseTimes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CsStudentExamTimes", "CourseTime_Id", "dbo.CsCourseTimes");
            DropForeignKey("dbo.CsStudentCourseTimes", "CourseTimeId", "dbo.CsCourseTimes");
            DropIndex("dbo.CsStudentExamTimes", new[] { "CourseTime_Id" });
            DropIndex("dbo.CsStudentCourseTimes", new[] { "CourseTimeId" });
            DropColumn("dbo.CsStudentExamTimes", "CourseTime_Id");
        }
    }
}
