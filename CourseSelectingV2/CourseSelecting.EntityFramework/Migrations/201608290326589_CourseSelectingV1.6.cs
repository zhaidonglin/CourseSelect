namespace CourseSelecting.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class CourseSelectingV16 : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.CsTeacherCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        CourseId = c.Int(nullable: false),
                        TeacherId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TeacherCourse_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.CsTeacherCourses", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CsTeacherCourses", "DeleterUserId", c => c.Long());
            AddColumn("dbo.CsTeacherCourses", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.CsTeacherCourses", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.CsTeacherCourses", "LastModifierUserId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CsTeacherCourses", "LastModifierUserId");
            DropColumn("dbo.CsTeacherCourses", "LastModificationTime");
            DropColumn("dbo.CsTeacherCourses", "DeletionTime");
            DropColumn("dbo.CsTeacherCourses", "DeleterUserId");
            DropColumn("dbo.CsTeacherCourses", "IsDeleted");
            AlterTableAnnotations(
                "dbo.CsTeacherCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        CourseId = c.Int(nullable: false),
                        TeacherId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TeacherCourse_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
