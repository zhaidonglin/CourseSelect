(function () {
    $(function () {
        var courseService = abp.services.app.course;

        $("#subject").select2({ minimumResultsForSearch: Infinity });
        //$('.spinner').spinner();
        $("#fitgrade").select2({ minimumResultsForSearch: Infinity });
        $('#formCourseAdd').bootstrapValidator({
            excluded: [':disabled'],
            fields: {
                subject: {
                    validators: {
                        notEmpty: {
                            message: '项目不能为空'
                        }
                    }
                },
                name: {
                    validators: {
                        notEmpty: {
                            message: '课程不能为空'
                        }
                    }
                },
                credit: {
                    validators: {
                        regexp: {
                            regexp: /^-?\d+\.?\d{0,2}$/,
                            message: '学分格式不正确'
                        }
                    }
                },
                limitNumbers: {
                    validators: {
                        notEmpty: {
                            message: '限选人数不能为空'
                        }
                    }
                }
               
            },

            submitHandler: function (validator, form, submitButton) {
                courseService.addCourse({
                    SubjectProjectId: form.context["subject"].value,
                    CourseName: form.context["name"].value,
                    //CourseGrade: form.context["fitGrade"].value,
                    CourseCredit: form.context["credit"].value,
                    CourseLimitNumbers: form.context["limitNumbers"].value,
                    Remark: form.context["remark"].value
                }).done(function () {
                    bootbox.alert("<div class=\"modal-body\">添加成功!</div>", function () {
                        $('#formCourseAdd').data("bootstrapValidator").resetForm(true);
                        $("#subject").select2('data', null);
                        $("#fitgrade").select2('data', null);
                    });
                });

            }
        });

    });
})();