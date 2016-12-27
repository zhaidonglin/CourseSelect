(function () {
    $(function () {
        var studentService = abp.services.app.student;
        $("#department").select2({ minimumResultsForSearch: Infinity });
        $("#major").select2({ minimumResultsForSearch: Infinity });
        $("#gender").select2({ minimumResultsForSearch: Infinity });

        var formAddStudent = $('#formAddStudent').bootstrapValidator({
            // Only disabled elements are excluded
            // The invisible elements belonging to inactive tabs must be validated
            excluded: [':disabled'],
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            submitHandler: function (validator, form, submitButton) {

                studentService.editStudent({
                    Id: $("#lblStudentId").val(),
                    //StudentNo: form.context["studentNo"].value,
                    StudentName: form.context["studentName"].value,
                    //EntryDate: form.context["studentEntryDate"].value,
                    //UserName: form.context["userName"].value,           //2
                    UserName: form.context["loginId"].value,


                    //Deparment: form.context["department"].value,
                    Department: form.context["department"].value,
                    Major: form.context["major"].value,
                    Gender: form.context["gender"].value,
                    LoginId: form.context["loginId"].value
                }).done(function () {
                    bootbox.alert("<div class=\"modal-body\">修改成功!</div>", function () {
                        $('#formAddStudent').data("bootstrapValidator").resetForm(false);
                    });
                });

            },
            fields: {
                //studentNo: {
                //    validators: {
                //        notEmpty: {
                //            message: '学生学号不能为空'
                //        },
                //        stringLength: {
                //            min: 11,
                //            max: 11,
                //            message: '学生学号长度为11个字符'
                //        }
                //    }
                //},
                studentName: {
                    validators: {
                        notEmpty: {
                            message: '学生姓名不能为空'
                        },
                        stringLength: {
                            
                            max: 32,
                            message: '学生姓名不能为空且必须为中文'
                        }
                    }
                },
                department: {
                    validators: {
                        notEmpty: {
                            message: '院系不能为空'
                        }
                    }
                },
                major: {
                    validators: {
                        notEmpty: {
                            message: '专业不能为空'
                        }
                    }
                },
                gender: {
                    validators: {
                        notEmpty: {
                            message: '性别不能为空'
                        }
                    }
                },
                loginId: {
                    validators: {
                        notEmpty: {
                            message: '登录账号不能为空'
                        },
                        stringLength: {
                            min: 11,
                            max: 11,
                            message: '登录账号长度为11个字符'
                        }
                    }
                }
            }
        });

    });
})();