(function () {
    $(function () {
        var courseService = abp.services.app.course;

        var selectedColor = "#5db2ff";
        var disabledColor = "#ed4e2a";

        $('#external-events div.external-event').each(function () {
            var eventObject = {
                title: $.trim($(this).text()),
                id: $(this).attr("data-id")
            };
            $(this).data('eventObject', eventObject);
            $(this).draggable({
                zIndex: 999,
                revert: true,
                revertDuration: 0
            });

        });
        var date = new Date();
        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();



        $('#calendar').fullCalendar({
            header: {
                right: 'month,agendaWeek,agendaDay prev,next',
                left: 'title'
            },
            editable: false,
            monthNames: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
            monthNamesShort: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
            dayNames: ["周日", "周一", "周二", "周三", "周四", "周五", "周六"],
            dayNamesShort: ["周日", "周一", "周二", "周三", "周四", "周五", "周六"],
            today: ["今天"],
            firstDay: 1,
            buttonText: {
                prev: '<i class="fa fa-chevron-left"></i>',
                next: '<i class="fa fa-chevron-right"></i>',
                today: '今天',
                month: '月',
                week: '周',
                day: '天'
            },
            events: function (start, end, callback) {
                courseService.getStudentCourseTimes({
                    Start: start,
                    End: end
                    
                }).done(function (result) {

                    var eventArray = new Array();

                    for (var index in result.items) {

                        var itemObj = result.items[index];
                        var eventObj = {
                            id: itemObj.id,
                            title: itemObj.teacherCourse.course.subjectProject.name + "[" + itemObj.teacherCourse.course.name + "]",
                            start: itemObj.start,
                            endTime: new Date(itemObj.end.replace("T", " ")),
                            allDay: false,
                            address: itemObj.address,
                            teacherName: itemObj.teacherCourse.teacher.surname,
                            enabledSelecting: itemObj.enabledSelecting,
                            enabledEndTime: itemObj.enabledEndTime,
                            enabledStartTime: itemObj.enabledStartTime,
                            isSelected: itemObj.isSelected
                        }
                        
                        if (itemObj.isSelected) {
                            eventObj.textColor = selectedColor;
                            eventObj.borderColor = selectedColor;
                        } else if (!itemObj.enabledSelecting || !itemObj.enabledEndTime || itemObj.enabledStartTime) {
                            eventObj.textColor = disabledColor;
                            eventObj.borderColor = disabledColor;
                        }

                        eventArray.push(eventObj);
                    }

                    callback(eventArray);
                });
            },

            eventClick: function (calEvent, jsEvent, view) {

                bootbox.dialog({
                    message: BootboxDetailsContent(calEvent.title, calEvent.teacherName, calEvent.start, calEvent.endTime, calEvent.address),
                    title: "上课时间",
                    backdrop: false,
                    className: "modal-darkorange",
                    borderColor: "rgb(160, 212, 104)",
                    buttons: {
                        success: {
                            label: calEvent.isSelected ? "取消预约" : "预约",
                            className: calEvent.isSelected ? "btn-darkorange" : "btn-blue",

                            callback: function () {
                                
                                if (calEvent.isSelected) {
                                    courseService.studentCourseTimeDeleteOrdered(calEvent.id).done(function () {
                                        bootbox.alert("<div class=\"modal-body\">取消成功!</div>", function() {
                                            calEvent.isSelected = false;
                                            calEvent.textColor = "";
                                            calEvent.borderColor = "";

                                            $('#calendar').fullCalendar('renderEvent', calEvent, false);
                                        });
                                    });
                                } else if (!calEvent.enabledSelecting || !calEvent.enabledEndTime || calEvent.enabledTime) {
                                    bootbox.alert("<div class=\"modal-body\">预约失败，预约人数已满或超过预约时间!</div>");
                                } else {
                                    courseService.studentCourseTimeOrdered(calEvent.id).done(function () {
                                        bootbox.alert("<div class=\"modal-body\">预约成功!</div>", function () {
                                            calEvent.isSelected = true;
                                            calEvent.textColor = selectedColor;
                                            calEvent.borderColor = selectedColor;

                                            $('#calendar').fullCalendar('renderEvent', calEvent, false);
                                        });
                                    });
                                }
                            }
                        },
                        "返回": {
                            className: "btn",
                            callback: function () { }
                        }
                    }
                });
            }
        });

    });
})();

function BootboxDetailsContent(courseName, teacherName, startTime, endTime, address) {
    var html = '';
    html += '<div id="addCourseModal">';
    html += '   <form id="formAddCourseTime" novalidate="novalidate" method="post" class="row form-horizontal bv-form modal-body">';
    html += '       <div class="col-md-12">';
    html += '           <div class="form-group">';
    html += '               <label>课程名称</label>';
    html += '               <input type="text" readonly="readonly" value="' + courseName + '" class="form-control">';
    html += '           </div>';
    html += '           <div class="form-group">';
    html += '               <label>授课教师</label>';
    html += '               <input type="text" readonly="readonly" value="' + teacherName + '" class="form-control">';
    html += '           </div>';
    html += '           <div class="form-group">';
    html += '               <label>上课时间</label>';
    html += '               <input type="text" class="form-control" readonly="readonly" value="' + startTime.format("yyyy年MM月dd日 hh:mm") + '">';
    html += '           </div>';
    html += '           <div class="form-group">';
    html += '               <label>截止时间</label>';
    html += '                   <input type="text" class="form-control" readonly="readonly" value="' + endTime.format("yyyy年MM月dd日") + '">';
    html += '           </div>';
    html += '           <div class="form-group">';
    html += '               <label>上课地点</label>';
    html += '               <input type="text" class="form-control" readonly="readonly" value="' + address + '">';
    html += '           </div>';
    html += '       </div>';
    html += '   </form>';
    html += '</div>';

    var object = $('<div/>').html(html).contents();

    return object;
}