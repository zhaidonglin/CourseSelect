(function () {
    $(function () {
        var _userService = abp.services.app.user;

        var reviseForm = $('#registrationForm').bootstrapValidator({
            // Only disabled elements are excluded
            // The invisible elements belonging to inactive tabs must be validated
            excluded: [':disabled'],
            //feedbackIcons: {                                             ///
            //    valid: 'glyphicon glyphicon-ok',
            //    invalid: 'glyphicon glyphicon-remove',
            //    validating: 'glyphicon glyphicon-refresh'
            //},

            submitHandler: function (validator, form, submitButton) {

                _userService.updatePassword({
                    Password: form.context["oldPassword"].value,
                    NewPassword: form.context["Password"].value,
                    TNewPassword: form.context["newPassword"].value,

                }).done(function () {
                    bootbox.alert("<div class=\"modal-body\">修改成功!</div>", function () {
                        $('#registrationForm').data("bootstrapValidator").resetForm(true);
                    });
                });

            },

            fields: {
                Password: {
                    validators: {
                        notEmpty: {
                            message: '不能为空'
                        },
                        stringLength: {
                            min: 6,
                            max: 32,
                            message: '密码长度为6-32个字符'
                        }
                    }
                },
                oldPassword: {
                    validators: {
                        notEmpty: {
                            message: '不能为空'
                        },
                        stringLength: {
                            min: 6,
                            max: 32,
                            message: '密码长度为6-32个字符'
                        }
                    }
                },
                newPassword: {
                    validators: {
                        identical: {
                            field: 'Password',
                            message: '两次输入密码不一致'
                        },
                        notEmpty: {
                            message: '请再次确认密码'
                        },
                        stringLength: {
                            min: 6,
                            max: 32,
                            message: '密码长度为6-32个字符'
                        }
                    }
                },



            }
        });

    });
})();