(function () {
    $(function () {
        $("#department").select2({ minimumResultsForSearch: Infinity });
        $('#formRegister').bootstrapValidator({
            excluded: [':disabled'],
            submitHandler: function (validator, form, submitButton) {
                console.log(form.context["teacherNo"]);
                abp.ui.setBusy(
                    $('#formRegister'),
                    abp.ajax({
                        url: abp.appPath + 'Account/TeacherRegister',
                        type: 'POST',
                        data: JSON.stringify({
                            LoginId: form.context["loginid"].value,
                            Pwd: form.context["pwd"].value,
                            TeacherNo: form.context["teacherNo"].value,
                            Name: form.context["name"].value,
                            Department: form.context["department"].value
                        })
                    })
                );
            },
            fields: {
                loginid: {
                    validators: {
                        notEmpty: {
                            message: '登录账号不能为空'
                        },
                        stringLength: {
                            max: 32,
                            message: '登录账号长度为1-32个字符'
                        }
                    }
                },
                pwd: {
                    validators: {
                        notEmpty: {
                            message: '密码不能为空'
                        },
                        stringLength: {
                            min:6,
                            max: 32,
                            message: '密码长度为6-32个字符'
                        }
                    }
                },
                pwd2: {
                    validators: {
                        identical: {
                            field: 'pwd',
                            message: '两次输入密码不一致'
                        },
                        notEmpty: {
                            message: '请再次确认密码'
                        }
                    }
                },
                teacherNo: {
                    validators: {
                        notEmpty: {
                            message: '工号可为空'
                        },
                        stringLength: {
                            max: 64,
                            message: '工号长度为1-64个字符'
                        }
                    }
                },
                name: {
                    validators: {
                        notEmpty: {
                            message: '姓名不能为空且必须中文'
                        },
                        stringLength: {
                            
                            max: 32,
                            message: '姓名长度为1-32个字符'
                        }
                    }
                }
            }
        });

    });
})();