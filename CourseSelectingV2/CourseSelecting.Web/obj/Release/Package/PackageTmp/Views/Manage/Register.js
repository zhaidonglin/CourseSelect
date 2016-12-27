(function () {
    $(function () {
        var _studentService = abp.services.app.student;
        $("#gender").select2({ minimumResultsForSearch: Infinity });
        $("#major").select2({ minimumResultsForSearch: Infinity });
        $("#grade").select2({ minimumResultsForSearch: Infinity });
        $("#class").select2({ minimumResultsForSearch: Infinity });
        $("#originOfStudent").select2({ minimumResultsForSearch: Infinity });
        $("department").select2({ minimumResultsForSearch: Infinity });
        $("professionLevel").select2({ minimumResultsForSearch: Infinity });

        var formAddStudent= $('#formAddStudent').bootstrapValidator({
            // Only disabled elements are excluded
            // The invisible elements belonging to inactive tabs must be validated
            excluded: [':disabled'],
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            submitHandler: function (validator, form, submitButton) {

                _studentService.addStudent({
                    //StudentNo: form.context["studentNo"].value,        //2
                    StudentNo: form.context["loginId"].value,

                    Gender: form.context["gender"].value,
                    Tel: form.context["tel"].value,
                    Major: form.context["major"].value,
                    Grade: form.context["grade"].value,
                    Class: form.context["class"].value,
                    OriginOfStudent: form.context["originOfStudent"].value,
                    Deparment: form.context["department"].value,
                    ProfessionLevel: form.context["professionLevel"].value
                }).done(function () {
                    bootbox.alert("<div class=\"modal-body\">注册成功!</div>", function () {
                        $('#formAddStudent').data("bootstrapValidator").resetForm(true);
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
                tel: {
                    validators: {
                        notEmpty: {
                            message: '联系方式不能为空'
                        },
                        stringLength: {
                            min: 11,
                            max: 11,
                            message: '手机号长度为11个字符'
                        }
                    }
                },
                department: {
                    validators: {
                        notEmpty: {
                            message: '院系不能为空'
                        }
                    }
                }
                
            }
        });

    });
})();