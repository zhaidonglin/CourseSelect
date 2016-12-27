namespace CourseSelecting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseSelectingV19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CsCourseTimes", "End", c => c.DateTime(nullable: false));
            AddColumn("dbo.CsCourseTimes", "FitGrade", c => c.String(maxLength: 521));
            AddColumn("dbo.CsStudents", "EntryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CsStudents", "EntryDate");
            DropColumn("dbo.CsCourseTimes", "FitGrade");
            DropColumn("dbo.CsCourseTimes", "End");
        }
    }
}
