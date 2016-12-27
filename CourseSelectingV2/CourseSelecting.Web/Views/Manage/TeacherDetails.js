
(function () {
    $(function () {
        var courseService = abp.services.app.course;
        $("#department").select2({ minimumResultsForSearch: Infinity });

        $("#courses").select2({ minimumResultsForSearch: Infinity });
        var oldcourses = $("#courses").select2('data');

        $('#formAddTeacher').bootstrapValidator({
            submitHandler: function (validator, form, submitButton) {
                var courses = $("#courses").select2('data');
                var coursesIds = new Array();

                for (var index in courses) {
                    if (courses.hasOwnProperty(index)) {
                        coursesIds.push(courses[index].id);
                    }
                }

                courseService.editTeacherCourses({
                    Id: $("#lblTeacherId").val(),
                    CoursesIds: coursesIds
                }).done(function() {
                    bootbox.alert("<div class=\"modal-body\">修改成功!</div>", function () {
                        oldcourses = $("#courses").select2('data');
                        $('#formAddTeacher').data("bootstrapValidator").resetForm(false);
                    });
                }).fail(function () {
                    console.log(oldcourses)
                    $("#courses").select2("data", oldcourses);
                });
            },
            fields: {
                courses: {
                    validators: {
                        notEmpty: {
                            message: '课程不能为空'
                        }
                    }
                }
            }
        });
    });
})();