﻿(function () {
    $(function () {
        var _studentService = abp.services.app.student;
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

                _studentService.addStudent({
                    //StudentNo: form.context["studentNo"].value,
                    StudentName:form.context["studentName"].value,
                    EntryDate: form.context["studentEntryDate"].value,
                    //Deparment: form.context["department"].value,
                    Department: form.context["department"].value,
                    Major: form.context["major"].value,
                    Gender: form.context["gender"].value,
                    LoginId: form.context["loginId"].value
                   
                }).done(function () {
                    bootbox.alert("<div class=\"modal-body\">添加成功!</div>", function () {
                        $('#formAddStudent').data("bootstrapValidator").resetForm(true);

                        $("#department").select2("data", null);
                        $("#major").select2("data", null);
                        $("#gender").select2("data", null);
                    });
                });

            },
            fields: {
                studentNo: {
                    validators: {
                        notEmpty: {
                            message: '学生学号不能为空'
                        },
                        stringLength: {
                            min:11,
                            max: 11,
                            message: '学生学号长度为11个字符'
                        }
                    }
                },
                studentName: {
                    validators: {
                        notEmpty: {
                            message: '学生姓名不能为空且必须为中文'
                        },
                        stringLength: {
                           
                            max: 32,
                            message: '学生姓名长度为1-32个字符'
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
                            max: 32,
                            message: '登录账号长度为1-32个字符'
                        }
                    }
                }
            }
        });

    });
})();