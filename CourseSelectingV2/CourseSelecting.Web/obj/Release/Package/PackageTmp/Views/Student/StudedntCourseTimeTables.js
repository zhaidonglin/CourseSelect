(function () {
    $(function () {
        var courseService = abp.services.app.course;

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
                courseService.getTeacherCourseTimes().done(function (result) {
                    var eventArray = new Array();

                    for (var index in result.items) {

                        var itemObj = result.items[index];
                        var eventObj = {
                            id: itemObj.id,
                            title: itemObj.teacherCourse.course.subjectProject.name + "[" + itemObj.teacherCourse.course.name + "]",
                            start: itemObj.start,
                            allDay: false,
                            address: itemObj.address
                        }

                        eventArray.push(eventObj);
                    }

                    callback(eventArray);
                });
            },
            droppable: true,
            drop: function (date, allDay) {

                var originalEventObject = $(this).data('eventObject');

                bootbox.dialog({
                    message: BootboxContent,
                    title: "新建上课时间",
                    backdrop: false,
                    className: "modal-darkorange",
                    buttons: {
                        success: {
                            label: "提交",
                            className: "btn-blue",
                            callback: function () {

                                $("#formAddCourseTime").bootstrapValidator({
                                    excluded: [':disabled'],
                                    fields: {
                                        courseTime: {
                                            validators: {
                                                notEmpty: {
                                                    message: '上课时间不能为空'
                                                }
                                            }
                                        },
                                        address: {
                                            validators: {
                                                notEmpty: {
                                                    message: '上课地点不能为空'
                                                }
                                            }
                                        }
                                    }
                                });

                                $("#formAddCourseTime").data("bootstrapValidator").validate();
                                if ($("#formAddCourseTime").data("bootstrapValidator").isValid()) {

                                    courseService.courseTimeAdd({
                                        TeacherCourseId: originalEventObject.id,
                                        Date: date,
                                        Start: $("#courseTime").val(),
                                        Address: $("#address").val()
                                    }).done(function (result) {

                                        var copiedEventObject = $.extend({}, originalEventObject);
                                        copiedEventObject.Id = result.Id;
                                        copiedEventObject.start = result.start;
                                        copiedEventObject.allDay = false;
                                        copiedEventObject.address = result.address;

                                        $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);
                                    });

                                    return true;
                                }
                                return false;
                            }
                        },
                        "取消": {
                            className: "btn",
                            callback: function () { }
                        }
                    }
                });
            },
            eventClick: function (calEvent, jsEvent, view) {

                //alert('Event: ' + calEvent.title);
                //alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
                //alert('View: ' + view.name);
                //// change the border color just for fun
                //$(this).css('border-color', 'red');

                bootbox.dialog({
                    message: BootboxDetailsContent(calEvent.title, calEvent.start, calEvent.address),
                    title: "上课时间",
                    backdrop: false,
                    className: "modal-darkorange",
                    buttons: {
                        success: {
                            label: "删除",
                            className: "btn-blue",
                            callback: function () {
                                courseService.courseTimeDelete(calEvent.id).done(function () {
                                    $('#calendar').fullCalendar('removeEvents', calEvent.id);
                                });
                            }
                        },
                        "取消": {
                            className: "btn",
                            callback: function () { }
                        }
                    }
                });
            }
        });

    });
})();

function BootboxContent() {
    var html = '';
    html += '<div id="addCourseModal">';
    html += '   <form id="formAddCourseTime" novalidate="novalidate" method="post" class="row form-horizontal bv-form modal-body">';
    html += '       <div class="col-md-12">';
    html += '           <div class="form-group">';
    html += '               <label>上课时间</label>';
    html += '               <div class="input-group">';
    html += '                   <input class="form-control startTime" type="text" id="courseTime" name="courseTime">';
    html += '                   <span class="input-group-addon">';
    html += '                       <i class="fa fa-clock-o"></i>';
    html += '                   </span>';
    html += '               </div>';
    html += '           </div>';
    html += '           <div class="form-group">';
    html += '               <label>上课地点</label>';
    html += '               <input type="text" class="form-control" placeholder="上课地点" id="address" name="address">';
    html += '           </div>';
    html += '       </div>';
    html += '   </form>';
    html += '</div>';

    var object = $('<div/>').html(html).contents();

    object.find('.startTime').timepicker();

    return object;
}

function BootboxDetailsContent(courseName, startTime, address) {
    var html = '';
    html += '<div id="addCourseModal">';
    html += '   <form id="formAddCourseTime" novalidate="novalidate" method="post" class="row form-horizontal bv-form modal-body">';
    html += '       <div class="col-md-12">';
    html += '           <div class="form-group">';
    html += '               <label>课程名称</label>';
    html += '               <input type="text" readonly="readonly" value="' + courseName + '" class="form-control">';
    html += '           </div>';
    html += '           <div class="form-group">';
    html += '               <label>上课时间</label>';
    html += '               <input type="text" class="form-control" readonly="readonly" value="' + startTime.format("yyyy年MM月dd日 hh:mm") + '">';
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