(function () {
    $(function () {
        var teacherService = abp.services.app.teacher;

        $("#department").select2({ minimumResultsForSearch: Infinity });
        $("#major").select2({ minimumResultsForSearch: Infinity });
        $("#diploma").select2({ minimumResultsForSearch: Infinity });
        $("#degree").select2({ minimumResultsForSearch: Infinity });
        $("#positionalTitle").select2({ minimumResultsForSearch: Infinity });
        $("#yearsOfWorking").select2({ minimumResultsForSearch: Infinity });
        $("#yearsOfTeaching").select2({ minimumResultsForSearch: Infinity });
        $("#gender").select2({ minimumResultsForSearch: Infinity });

        var formAddTeacher = $('#formAddTeacher').bootstrapValidator({
            excluded: [':disabled'],
            submitHandler: function (validator, form, submitButton) {

                teacherService.addTeacher({
                    TeacherNo: form.context["teacherNo"].value,
                    TeacherName: form.context["teacherName"].value,
                    Department: form.context["department"].value,
                    LoginId: form.context["loginId"].value,
                    Gender: form.context["gender"].value,
                    Major: form.context["major"].value,
                    Diploma: form.context["diploma"].value,
                    Degree: form.context["degree"].value,
                    PositionalTitle: form.context["positionalTitle"].value,
                    YearsOfWorking: form.context["yearsOfWorking"].value,
                    YearsOfTeaching: form.context["yearsOfTeaching"].value,
                    IsFullTime: form.context["isFullTime"][0].checked,
                    Tel: form.context["tel"].value
                }).done(function () {
                    bootbox.alert("<div class=\"modal-body\">添加成功!</div>", function () {
                        $('#formAddTeacher').data("bootstrapValidator").resetForm(true);


                        $("#department").select2("data", null);
                        //$("#major").select2("data", null);
                        //$("#diploma").select2("data", null);
                        //$("#degree").select2("data", null);
                        //$("#positionalTitle")("data", null);
                        //$("#yearsOfWorking")("data", null);
                        //$("#yearsOfTeaching")("data", null);
                        //$("#gender").select2("data", null);
                    });
                });

            },
            fields: {
                teacherNo: {
                    validators: {
                        notEmpty: {
                            message: '教师工号可为空'
                        },
                        stringLength: {
                            max: 64,
                            message: '教师工号长度为1-64个字符'
                        }
                    }
                },
                teacherName: {
                    validators: {
                        notEmpty: {
                            message: '教师姓名不能为空'
                        },
                        stringLength: {
                            
                            max: 32,
                            message: '教师姓名长度为1-32个字符'
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
                loginId: {
                    validators: {
                        notEmpty: {
                            message: '登录账号不能为空'
                        },
                        stringLength: {
                            max: 32,
                            message: '登录账号长度为1-32个字符'
                        }

                    }
                }
            }
        });

    });
})();