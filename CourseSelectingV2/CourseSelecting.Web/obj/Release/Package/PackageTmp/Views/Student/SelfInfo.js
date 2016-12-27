(function () {
    $(function () {
        var studentService = abp.services.app.student;

        $("#GradeStyle").select2({ minimumResultsForSearch: Infinity });
        //$("#ClassStyle").select2({ minimumResultsForSearch: Infinity });
        $("#CollegeStyle").select2({ minimumResultsForSearch: Infinity });
        $("#SexStyle").select2({ minimumResultsForSearch: Infinity });
        $("#DepartmentStyle").select2({ minimumResultsForSearch: Infinity });
        $("#LocalStyle").select2({ minimumResultsForSearch: Infinity });
        $("#MajorStyle").select2({ minimumResultsForSearch: Infinity });

        

        $("#registrationForm").bootstrapValidator({
            excluded: [':disabled'],
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },            

            submitHandler: function (validator, form, submitButton) {
                studentService.creatStudentInfo({
                    Id: $("#StudentId").val(),

                   // StudentNo: form.context["xuehao"].value,            //2
                    //StudentNo: form.context["xuehao"].value,
                    LoginId: form.context["loginId"].value,

                   // Major: form.context["nianji"].value,
                    Class: form.context["banji"].value,
                    Major: form.context["zhuanye"].value,
                    Department: form.context["yuanxi"].value,

                    Tel: form.context["dianhua"].value,

                    //Grade: form.context["nianji"].value,

                    Gender: form.context["sex"].value,

                    //EntryDate: form.context["entryDate"].value,

                    ProfessionLevel: form.context["dengji"].value,
                    //Discription

                    OriginOfStudent:form.context["shengyuandi"].value

                }).done(function () {
                    bootbox.alert("<div class=\"modal-body\">修改成功!</div>", function () {
                        $('#registrationForm').data("bootstrapValidator").resetForm(false);
                    });
                });

            }
        });


        //var formAddTeacher = $('#formAddTeacher').bootstrapValidator({
        //    // Only disabled elements are excluded
        //    // The invisible elements belonging to inactive tabs must be validated
        //    excluded: [':disabled'],
        //    feedbackIcons: {
        //        valid: 'glyphicon glyphicon-ok',
        //        invalid: 'glyphicon glyphicon-remove',
        //        validating: 'glyphicon glyphicon-refresh'
        //    },
        //    submitHandler: function (validator, form, submitButton) {

        //        _teacherService.addTeacher({
        //            TeacherNo: form.context["teacherNo"].value,
        //            TeacherName: form.context["teacherName"].value,
        //            Department: form.context["department"].value,
        //            LoginId: form.context["loginId"].value
        //        }).done(function () {
        //            bootbox.alert("<div class=\"modal-body\">添加成功!</div>", function () {
        //                $('#formAddTeacher').data("bootstrapValidator").resetForm(true);
        //            });
        //        });

        //    }
        //});

    });
})();