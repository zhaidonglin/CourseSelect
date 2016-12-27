(function () {
    $(function () {
        var subjectService = abp.services.app.subjectProject;

        $("#subjectStyle").select2({ minimumResultsForSearch: Infinity });
        $("#subjectType").select2({ minimumResultsForSearch: Infinity });
        $("#teachingStyle").select2({ minimumResultsForSearch: Infinity });
        //$('#spinCredit').spinner('value', $("#credit").val());

        $("#formSubjectProjectEdit").bootstrapValidator({
            excluded: [':disabled'],

            fields: {
                subjectName: {
                    validators: {
                        notEmpty: {
                            message: '项目名称不能为空'
                        }
                    }
                },
                subjectStyle: {
                    validators: {
                        notEmpty: {
                            message: '项目模式不能为空'
                        }
                    }
                },
                subjectType: {
                    validators: {
                        notEmpty: {
                            message: '分类不能为空'
                        }
                    }
                },
                teachingStyle: {
                    validators: {
                        notEmpty: {
                            message: '授课模式不能为空'
                        }
                    }
                },
                isCompulsory: {
                    validators: {
                        notEmpty: {
                            message: '项目性质不能为空'
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
                }
            },

            submitHandler: function (validator, form, submitButton) {
                subjectService.editSubjectProject({
                    Id: form.context["id"].value,
                    Name: form.context["subjectName"].value,
                    SubjectStyle: form.context["subjectStyle"].value,
                    Type: form.context["subjectType"].value,
                    Credit: form.context["credit"].value,
                    IsCompulsory: form.context["isCompulsory"][0].checked,
                    AimedAt: form.context["aimedAt"].value,
                    TeachingStyle: form.context["teachingStyle"].value,
                    Discription: form.context["description"].value
                }).done(function () {
                    bootbox.alert("<div class=\"modal-body\">修改成功!</div>", function () {
                        $('#formSubjectProjectEdit').data("bootstrapValidator").resetForm(false);
                    });
                });

            }
        });
    });
})();