namespace CourseSelecting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseSelectingV21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CsStudentExamTimes", "CourseTime_Id", "dbo.CsCourseTimes");
            DropIndex("dbo.CsStudentExamTimes", new[] { "CourseTime_Id" });
            AddColumn("dbo.CsExamTimes", "End", c => c.DateTime(nullable: false));
            AddColumn("dbo.CsExamTimes", "FitGrade", c => c.String(maxLength: 521));
            AddColumn("dbo.CsStudentCourseTimes", "ClassCredit", c => c.Double(nullable: false));
            AddColumn("dbo.CsStudentExamTimes", "Credit", c => c.Double(nullable: false));
            CreateIndex("dbo.CsStudentExamTimes", "ExamTimeId");
            AddForeignKey("dbo.CsStudentExamTimes", "ExamTimeId", "dbo.CsExamTimes", "Id", cascadeDelete: true);
            DropColumn("dbo.CsStudentExamTimes", "CourseTime_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CsStudentExamTimes", "CourseTime_Id", c => c.Int());
            DropForeignKey("dbo.CsStudentExamTimes", "ExamTimeId", "dbo.CsExamTimes");
            DropIndex("dbo.CsStudentExamTimes", new[] { "ExamTimeId" });
            DropColumn("dbo.CsStudentExamTimes", "Credit");
            DropColumn("dbo.CsStudentCourseTimes", "ClassCredit");
            DropColumn("dbo.CsExamTimes", "FitGrade");
            DropColumn("dbo.CsExamTimes", "End");
            CreateIndex("dbo.CsStudentExamTimes", "CourseTime_Id");
            AddForeignKey("dbo.CsStudentExamTimes", "CourseTime_Id", "dbo.CsCourseTimes", "Id");
        }
    }
}
