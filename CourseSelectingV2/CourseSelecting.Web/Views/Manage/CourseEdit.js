(function () {
    $(function () {
        var courseService = abp.services.app.course;

        $("#subject").select2({ minimumResultsForSearch: Infinity });
        //$('#spinCredit').spinner('value', $("#credit").val());
        //$('#spanLimit').spinner('value', $("#limitNumbers").val());

        $('#formCourseEdit').bootstrapValidator({
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
                var input = {
                    Id: $("#lblCourseId").val(),
                    SubjectProjectId: form.context["subject"].value,
                    CourseName: form.context["name"].value,
                    CourseCredit: form.context["credit"].value,
                    CourseLimitNumbers: form.context["limitNumbers"].value,
                    Remark: form.context["remark"].value
                };

                courseService.editCourse(input).done(function () {
                    bootbox.alert("<div class=\"modal-body\">修改成功!</div>", function () {
                        $('#formCourseEdit').data("bootstrapValidator").resetForm(false);
                    });
                });

            }
        });

    });
})();