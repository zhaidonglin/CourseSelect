(function () {
    $(function () {
        var courseTimeService = abp.services.app.courseTimeV2;

        $('#courseTimeTable').dataTable({
            "sDom": "Tflt<'row DTTTFooter'<'col-sm-6'i><'col-sm-6'p>>",

            "oTableTools": {
                "aButtons": [],
                "sSwfPath": "/beyondadmin/assets/swf/copy_csv_xls_pdf.swf"
            },

            "language": {
                "search": "",
                "sLengthMenu": "_MENU_",
                "info": "第 _PAGE_ 页 （共 _PAGES_ 页）",
                "oPaginate": {
                    "sPrevious": "上一页",
                    "sNext": "下一页"
                },
                "emptyTable": "没有查询到数据!",
                "zeroRecords": "没有查询到数据!",
                "infoEmpty": ""
            },

            "processing": true,
            "serverSide": true,

            ajax: function (data, callback, settings) {

                var input = {
                    PageSize: data.length,
                    KeyWord: data.search.value,
                    Start: data.start,
                    TeacherId: $("#lblTeacherId").val()
                };

                courseTimeService.query(input)
                    .done(function (result) {

                        console.log(result.items);

                        var callbackObj = {
                            darw: data.darw,
                            recordsTotal: result.totalCount,
                            recordsFiltered: result.totalCount,
                            data: result.items
                        }
                        callback(callbackObj);
                    });
            },

            "columns": [
                { "data": "id" },
                { "data": "teacherCourse.course.name" },
                {
                    "data": "start",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html(new Date(cellData.replace("T", " ")).format("yyyy年MM月dd日 hh：ss"));
                    }
                },
                { "data": "address" },
                { "data": "times" },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html('<a href="javascript:void(0);"  onclick="fnListOrderdStudent(' + cellData + ')" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>查看已预约学生</a>&nbsp;&nbsp;<a href="javascript:void(0);" onclick="fnDeleteTeacherCourseTables(' + cellData + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>删除</a>');
                    }
                }
            ],

            "aaSorting": []
        });
    });
})();

function fnListOrderdStudent(id) {
    var studentCourseTime = abp.services.app.studentCourseTimeV2;

    var input = {
        PageSize: 0,
        Start: 0,
        CourseTimeId: id
    };

    studentCourseTime.query(input).done(function (result) {
        console.log(result.items);
        bootbox.dialog({
            message: BootboxContent(result.items),
            title: "已预约学生列表",
            backdrop: false,
            className: "modal-darkorange",
            buttons: {
                "取消": {
                    className: "btn",
                    callback: function () { }
                }
            }
        });
    });
}

function BootboxContent(objs) {
    var html = '';
    html += '<div id="listOrderdStudentModal">';
    html += '    <div class="col-md-12">';
    html += '        <div class="row form-horizontal bv-form modal-body">';
    html += '            <table id="orderdStudentTable" class="table table-striped table-bordered table-hover">';
    html += '                <thead><tr><th>序号</th><th>学生姓名</th><th>学生学号</th><th style="width: 80px;">操作</th></tr></thead>';
    html += '                <tbody>';
    for (var index in objs) {
        if (objs.hasOwnProperty(index)) {
            html += '                    <tr class="even" data="' + objs[index].id + '"><td>' +
                objs[index].id +
                '</td><td>' +
                objs[index].student.name +
                '</td><td>' +
                objs[index].student.userName +
                '</td><td><a href="javascript:void(0);" onclick="fnDeleteStudentOrderd(' + objs[index].id + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>删除</a></td></tr>';
        }
    }
    html += '                </tbody>';
    html += '            </table>';
    html += '        </div>';
    html += '    </div>';
    html += '</div>';

    var object = $('<div/>').html(html).contents();

    return object;
}

function fnDeleteStudentOrderd(id) {
    var studentCourseTime = abp.services.app.studentCourseTimeV2;
    abp.message.confirm("您确定要删除该条信息么？", function (isconfirm) {
        if (isconfirm) {
            studentCourseTime.delete(id).done(function () {
                abp.message.info("删除成功！");
                $('#courseTimeTable').dataTable().fnReloadAjax(null, null, true);
                $("#orderdStudentTable").find("tr[data=" + id + "]").remove();
            });
        }
    });
}

function fnDeleteTeacherCourseTables(id) {
    var courseTimeService = abp.services.app.courseTimeV2;
    abp.message.confirm("您确定要删除该条信息么？", function (isconfirm) {
        if (isconfirm) {
            courseTimeService.delete(id).done(function () {
                $('#courseTimeTable').dataTable().fnReloadAjax(null, null, true);
                abp.message.info("删除成功！");
            });
        }
    });
}