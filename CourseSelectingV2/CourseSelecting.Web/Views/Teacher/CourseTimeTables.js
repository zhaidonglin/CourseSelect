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
                courseService.getTeacherCourseTimes({
                    Start: start,
                    End: end
                }).done(function (result) {
                    var eventArray = new Array();
                    console.log(result);
                    for (var index in result.items) {

                        var itemObj = result.items[index];
                        var eventObj = {
                            id: itemObj.id,
                            title: itemObj.teacherCourse.course.subjectProject.name + "[" + itemObj.teacherCourse.course.name + "]",
                            start: itemObj.start,
                            endTime: new Date(itemObj.end.replace("T", " ")),
                            allDay: false,
                            address: itemObj.address,
                            times: itemObj.times,
                            limits: itemObj.teacherCourse.course.limitNumbers,
                            grade: itemObj.fitGrade
                        }

                        console.log(eventObj);

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

                                    var checkboxValue = new Array();
                                    var checkboxStr = document.getElementsByName("grade");
                                    for (var i = 0; i < checkboxStr.length; i++) {
                                        if (checkboxStr[i].checked) {
                                            checkboxValue.push(checkboxStr[i].value);
                                        }
                                    }

                                    checkboxValue = checkboxValue.toString();
                                    if (checkboxValue.length > 0) {
                                        checkboxValue = "," + checkboxValue + ",";
                                    }

                                    courseService.courseTimeAdd({

                                        TeacherCourseId: originalEventObject.id,
                                        Date: date,
                                        Start: $("#courseTime").val(),
                                        End: $("#courseEndTime").val(),
                                        Address: $("#address").val(),
                                        EnabledGrade: checkboxValue
                                    }).done(function (result) {
                                        console.log(result);
                                        bootbox.alert("<div class=\"modal-body\">添加成功!</div>", function () {

                                            var copiedEventObject = $.extend({}, originalEventObject);
                                            copiedEventObject.id = result.id;
                                            copiedEventObject.start = result.start;
                                            copiedEventObject.endTime = new Date(result.end.replace("T", " "));
                                            copiedEventObject.allDay = false;
                                            copiedEventObject.address = result.address;
                                            copiedEventObject.times = result.times;
                                            copiedEventObject.grade = result.fitGrade,
                                            copiedEventObject.limits = result.teacherCourse.course.limitNumbers;

                                            $('#calendar').fullCalendar('renderEvent', copiedEventObject, false);
                                        });
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
                console.log(calEvent);
                var enabledList = [false, false, false, false, false];

                var gradestr = calEvent.grade;
                if (gradestr != null) {
                    var enabledIndexs = gradestr.split(",");
                    for (var i = 0; i < enabledIndexs.length; i++) {
                        enabledList[enabledIndexs[i] - 1] = true;
                    }
                }
                console.log(enabledList);


                bootbox.dialog({
                    message: BootboxDetailsContent(calEvent.id, calEvent.title, calEvent.start, calEvent.endTime, calEvent.address, calEvent.limits, calEvent.times, enabledList),
                    title: "上课时间",
                    backdrop: false,
                    className: "modal-darkorange",
                    buttons: {
                        success: {
                            label: "删除",
                            className: "btn-blue",
                            callback: function () {
                                courseService.courseTimeDelete(calEvent.id).done(function () {
                                    bootbox.alert("<div class=\"modal-body\">删除成功!</div>", function () {
                                        $('#calendar').fullCalendar('removeEvents', calEvent.id);
                                    });
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
    html += '               <label>截止时间</label>';
    html += '               <div class="input-group">';
    html += '                   <input class="form-control endTime" id="courseEndTime" type="text" name="courseEndTime" data-date-format="yyyy-mm-dd">';
    html += '                   <span class="input-group-addon">';
    html += '                       <i class="fa fa-calendar"></i>';
    html += '                   </span>';
    html += '               </div>';
    html += '           </div>';
    html += '           <div class="form-group">';
    html += '               <label>上课地点</label>';
    html += '               <input type="text" class="form-control" placeholder="上课地点" id="address" name="address">';
    html += '           </div>';
    html += '           <div class="form-group">';
    html += '               <label>限选年级</label>';
    html += '                 <div class="checkbox checkboxValue"> ';
    html += '                 <label >';
    html += '                   <input class="colored-success" type="checkbox"  id="one" name="grade" value="1">';
    html += '                   <span class="text">一年级</span>';
    html += '                </label>';
    html += '                <label >';
    html += '                   <input class="colored-success" type="checkbox"  id="two" name="grade" value="2">';
    html += '                   <span class="text">二年级</span>';
    html += '                </label>';
    html += '                <label >';
    html += '                   <input class="colored-success" type="checkbox"  id="three" name="grade" value="3">';
    html += '                   <span class="text">三年级</span>';
    html += '                </label>';
    html += '                <label >';
    html += '                   <input class="colored-success" type="checkbox"  id="four" name="grade" value="4">';
    html += '                   <span class="text">四年级</span>';
    html += '                </label>';
    html += '                <label >';
    html += '                   <input class="colored-success" type="checkbox"  id="five" name="grade" value="5">';
    html += '                   <span class="text">五年级</span>';
    html += '                </label>';
    html += '            </div>';
    html += '       </div>';
    html += '   </form>';
    html += '</div>';

    var object = $('<div/>').html(html).contents();

    object.find('.startTime').timepicker();

    object.find('.endTime').datepicker({ autoclose: true }); //object.find('#courseEndTime').on("changeDate", function () {//var defaults = $.fn.datepicker.defaults = {//    format: 'yyyy/dd/mm', //    language: 'cn'     //}//toValue: function (date, format, language) { //    var d = new Date(date);//    d.setDate(d.getDate() + 7);//    return new Date(d);//}  //});   
    return object;
}

function BootboxDetailsContent(id, courseName, startTime, endTime, address, limits, times, enabledList) {
    console.log(endTime);
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
    html += '               <label>截止时间</label>';
    html += '               <input type="text" class="form-control" readonly="readonly" value="' + endTime.format("yyyy年MM月dd日") + '">';
    html += '           </div>';
    html += '           <div class="form-group">';
    html += '               <label>上课地点</label>';
    html += '               <input type="text" class="form-control" readonly="readonly" value="' + address + '">';
    html += '           </div>';
    html += '           <div class="form-group">';
    html += '               <label>限选人数</label>';
    html += '               <input type="text" class="form-control" readonly="readonly" value="' + limits + '">';
    html += '           </div>';
    html += '           <div class="form-group">';
    html += '               <label>已预约人数</label>';
    html += '               <div class="input-group input-group-sm">';
    html += '                   <input class="form-control" type="text"  readonly="readonly" value="' + times + '">';
    html += '                   <span class="input-group-btn">';
    html += '                       <a class="btn btn-default" href="/Teacher/GetCourseTimeOrderdStudents/' + id + '">详细信息</a>';
    html += '                   </span>';
    html += '               </div>';
    html += '           </div>';
    html += '           <div class="form-group">';
    html += '               <label>限选年级</label>';
    html += '                 <div class="checkbox"> ';
    html += '                 <label >';
    html += '                   <input class="colored-success" disabled="true" type="checkbox" ' + (enabledList[0] ? 'checked="checked"' : '') + ' id="one" name="grade" value="1">';
    html += '                   <span class="text">一年级</span>';
    html += '                </label>';
    html += '                <label>';
    html += '                   <input class="colored-success" disabled="true" type="checkbox"  ' + (enabledList[1] ? 'checked="checked"' : '') + ' id="two" name="grade" value="2">';
    html += '                   <span class="text">二年级</span>';
    html += '                </label>';
    html += '                <label >';
    html += '                   <input class="colored-success" disabled="true" type="checkbox"  ' + (enabledList[2] ? 'checked="checked"' : '') + ' id="three" name="grade" value="3">';
    html += '                   <span class="text">三年级</span>';
    html += '                </label>';
    html += '                <label >';
    html += '                   <input class="colored-success" disabled="true" type="checkbox" ' + (enabledList[3] ? 'checked="checked"' : '') + '  id="four" name="grade" value="4">';
    html += '                   <span class="text">四年级</span>';
    html += '                </label>';
    html += '                <label >';
    html += '                   <input class="colored-success" disabled="true" type="checkbox"  ' + (enabledList[4] ? 'checked="checked"' : '') + ' id="five" name="grade" value="5">';
    html += '                   <span class="text">五年级</span>';
    html += '                </label>';
    html += '            </div>';
    html += '       </div>';
    html += '   </form>';
    html += '</div>';

    var object = $('<div/>').html(html).contents();

    return object;
}