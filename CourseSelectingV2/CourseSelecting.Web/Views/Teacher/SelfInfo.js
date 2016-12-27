(function () {
    $(function () {
        var _teacherService = abp.services.app.teacher;
        $("#gender").select2({ minimumResultsForSearch: Infinity });
        $("#department").select2({ minimumResultsForSearch: Infinity });
        $("#major").select2({ minimumResultsForSearch: Infinity });
        $("#diploma").select2({ minimumResultsForSearch: Infinity });
        $("#degree").select2({ minimumResultsForSearch: Infinity });
        $("#positionalTitle").select2({ minimumResultsForSearch: Infinity });
        $("#yearsOfWorking").select2({ minimumResultsForSearch: Infinity });
        $("#yearsOfTeaching").select2({ minimumResultsForSearch: Infinity });
        $("#isFullTime").select2({ minimumResultsForSearch: Infinity });

        var registrationForm = $('#registrationForm').bootstrapValidator({
            // Only disabled elements are excluded
            // The invisible elements belonging to inactive tabs must be validated
            excluded: [':disabled'],
            //feedbackIcons: {
            //    valid: 'glyphicon glyphicon-ok',
            //    invalid: 'glyphicon glyphicon-remove',
            //    validating: 'glyphicon glyphicon-refresh'
            //},
            submitHandler: function (validator, form, submitButton) {

                _teacherService.creatTeacherInfo({
                    Id: $("#lblTeacherId").val(),
                    TeacherNo: form.context["teacherNo"].value,
                    TeacherName: form.context["surName"].value,
                    Gender: form.context["gender"].value,
                    Department: form.context["department"].value,
                    Major: form.context["major"].value,
                    Diploma: form.context["diploma"].value,
                    Degree: form.context["degree"].value,
                    PositionalTitle: form.context["positionalTitle"].value,
                    YearsOfWorking: form.context["yearsOfWorking"].value,
                    YearsOfTeaching: form.context["yearsOfTeaching"].value,
                    Tel: form.context["tel"].value,
                    IsFullTime: (form.context["isFullTime"].value == 0),



                    SurName: form.context["surName"].value


                    
                    
                    //TeacherName: form.context["userName"].value,
                    //LoginId: form.context["loginId"].value

                }).done(function () {
                    bootbox.alert("<div class=\"modal-body\">编辑成功!</div>", function () {
                        $('#registrationForm').data("bootstrapValidator").resetForm(false);
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
                gender: {
                    validators: {
                        notEmpty: {
                            message: '性别不能为空'
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
                diploma: {
                    validators: {
                        notEmpty: {
                            message: '学历不能为空'
                        }
                    }
                },
                degree: {
                    validators: {
                        notEmpty: {
                            message: '学位不能为空'
                        }
                    }
                },
                positionalTitle: {
                    validators: {
                        notEmpty: {
                            message: '职称不能为空'
                        }
                    }
                },
                yearsOfWorking: {
                    validators: {
                        notEmpty: {
                            message: '工作年限不能为空'
                        }
                    }
                },
                yearsOfTeaching: {
                    validators: {
                        notEmpty: {
                            message: '教学年限不能为空'
                        }
                    }
                },
                isFullTime: {
                    validators: {
                        notEmpty: {
                            message: '教学年限不能为空'
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



//(function () {
//    $(function () {
//        var teacherService = abp.services.app.teacher;

       
//        $("#GenderStyle").select2();
//        $("#DepartmentStyle").select2();
//        $("#DiplomaStyle").select2();
//        $("#MajorStyle").select2();

//        $("#PositionalTitleStyle").select2();
//        $("#IsFullTimeStyle").select2();
// ;



//        $("#registrationForm").bootstrapValidator({
//            excluded: [':disabled'],
//            feedbackIcons: {
//                valid: 'glyphicon glyphicon-ok',
//                invalid: 'glyphicon glyphicon-remove',
//                validating: 'glyphicon glyphicon-refresh'
//            },

//            submitHandler: function (validator, form, submitButton) {
//                studentService.creatStudentInfo({
//                    Id: $("#TeacherNo").val(),

//                    TeacherNo: form.context["teacherNo"].value,
//                                        TeacherName: form.context["teacherName"].value,
//                                        Gender: form.context["gender"].value,
//                                        Department: form.context["department"].value,
//                                        Major: form.context["major"].value,
//                                        Diploma: form.context["diploma"].value,
//                                        Degree: form.context["degree"].value,
//                                        PositionalTitle: form.context["positionalTitle"].value,
//                                        YearsOfWorking: form.context["yearsOfWorking"].value,
//                                        YearsOfTeaching: form.context["yearsOfTeaching"].value,
//                                        IsFullTime: form.context["isFullTime"].value,



//                                        LoginId: form.context["loginId"].value


//                }).done(function () {
//                    bootbox.alert("<div class=\"modal-body\">修改成功!</div>", function () {
//                        $('#registrationForm').data("bootstrapValidator").resetForm(true);
//                        //$('#subjectStyle').select2('data', null);
//                        //$('#subjectType').select2('data', null);
//                        //$('#teachingStyle').select2('data', null);
//                        $("#GenderStyle").select2('data', null);
//                        $("#DepartmentStyle").select2('data', null);
//                        $("#DiplomaStyle").select2('data', null);
//                        $("#MajorStyle").select2('data', null);
//                        $("#DepartmentStyle").select2('data', null);
//                        $("#PositionalTitleStyle").select2('data', null);
//                        $("#IsFullTimeStyle").select2('data', null);
//                    });
//                });

//            }
//        });



//    });
//})();