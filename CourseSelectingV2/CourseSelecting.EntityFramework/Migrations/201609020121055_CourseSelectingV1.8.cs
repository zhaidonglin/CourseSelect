namespace CourseSelecting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseSelectingV18 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CsStudentSubjectProjects", "CourseId");
            CreateIndex("dbo.CsStudentSubjectProjects", "StudentId");
            AddForeignKey("dbo.CsStudentSubjectProjects", "StudentId", "dbo.CsStudents", "Id");
            AddForeignKey("dbo.CsStudentSubjectProjects", "CourseId", "dbo.CsSubjectProjects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CsStudentSubjectProjects", "CourseId", "dbo.CsSubjectProjects");
            DropForeignKey("dbo.CsStudentSubjectProjects", "StudentId", "dbo.CsStudents");
            DropIndex("dbo.CsStudentSubjectProjects", new[] { "StudentId" });
            DropIndex("dbo.CsStudentSubjectProjects", new[] { "CourseId" });
        }
    }
}
