(function () {
    $(function () {
        var courseService = abp.services.app.course;


        $('#formCourseEdit').bootstrapValidator({
            excluded: [':disabled'],
            fields: {
                StudentName: {
                    validators: {
                        notEmpty: {
                            message: '不能为空'
                        }
                    }
                },
                SubjectName: {
                    validators: {
                        notEmpty: {
                            message: '不能为空'
                        }
                    }
                }
                //ClassCredit: {
                //    validators: {
                //        notEmpty: {
                //            message: '不能为空'
                //        }
                //    }
                //},
                //ExamCredit: {
                //    validators: {
                //        notEmpty: {
                //            message: '不能为空'
                //        }
                //    }
                //}
            },
            submitHandler: function (validator, form, submitButton) {
                var input = {
                    Id: $("#lblCreditId").val(),
                    StudentName: form.context["studentName"].value,
                    SubjectName: form.context["subjectName"].value,
                    ClassCredit: form.context["classCredit"].value,
                    ExamCredit: form.context["examCredit"].value,
                    SubjectCredit: form.context["subjectCredit"].value
                };

               // console.log(input);

                courseService.editCredits(input).done(function () {
                    bootbox.alert("<div class=\"modal-body\">修改成功!</div>", function () {
                        $('#formCourseEdit').data("bootstrapValidator").resetForm(false);
                    });
                });

            }
        });

    });
})();