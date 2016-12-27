(function () {
    $(function () {
        $("#gender").select2({ minimumResultsForSearch: Infinity });
        $('#formRegister').bootstrapValidator({
            excluded: [':disabled'],
            submitHandler: function (validator, form, submitButton) {

                var data = {
                    LoginId: form.context["loginid"].value,
                    Pwd: form.context["pwd"].value,
                    // StudentNo: form.context["studentNo"].value,             //2
                    StudentNo: form.context["loginid"].value,

                    Name: form.context["name"].value,
                    Gender: form.context["gender"].value,

                    EntryDate: form.context["entryDate"].value,

                    Tel: form.context["tel"].value
                };

                console.log(data);

                abp.ui.setBusy(
                    $('#formRegister'),
                    abp.ajax({
                        url: abp.appPath + 'Account/StudentRegister',
                        type: 'POST',
                        data: JSON.stringify(data)
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
                            min: 11,
                            max: 11,
                            message: '登录账号长度为11个字符'
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
                studentNo: {
                    validators: {
                        notEmpty: {
                            message: '学号不能为空'
                        },
                        stringLength: {
                            min: 11,
                            max: 11,
                            message: '学生学号长度为11个字符'
                        }
                    }
                },                                       //2
                name: {
                    validators: {
                        notEmpty: {
                            message: '姓名不能为空'
                        },
                        stringLength: {
                            
                            max: 32,
                            message: '学生姓名不能为空且必须为中文'
                        }
                    }
                },
                entryDate: {
                    validators: {
                        notEmpty: {
                            message: '入学时间不能为空'
                        }
                    }
                },
                tel: {
                    validators: {
                        notEmpty: {
                            message: '联系方式不能为空'
                        },
                        stringLength: {
                            min: 11,
                            max: 11,
                            message: '手机号长度为11个字符'
                        },
                        regexp: {
                            regexp: /((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)/,
                            message: '电话号码格式不正确'
                        }
                    }
                }
            }
        });
    });
})();