var teacherdatatable;
(function () {
    var teacherService = abp.services.app.teacher;

    var initTable = function () {
        var table = $('#simpledatatable').dataTable({
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
            //"ajax": "@Url.Action("JsonData")",
            ajax: function (data, callback, settings) {

                var input = {
                    PageSize: data.length,
                    Start: data.start,
                    KeyWord: data.search.value
                };

                teacherService.getTeachers(input).done(function (result) {
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
                { "data": "name" },
                { "data": "userName" },
                { "data": "teacherNo" },
                { "data": "department" },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html('<a href="/manage/TeacherDetails?id=' + cellData + '" class="btn btn-info btn-xs edit"><i class="fa fa-info"></i>详细信息</a>');
                    }
                },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html('<a href="/manage/Teacheredit?id=' + cellData + '" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>编辑</a>&nbsp;&nbsp;<a href="/manage/TeacherCourseTables?id=' + cellData + '" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>查看课程</a>&nbsp;&nbsp;<a href="javascript:void(0);" onclick="fnResetTeacher(' + cellData + ')" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>重置密码</a>&nbsp;&nbsp;<a href="javascript:void(0);" onclick="fnDeleteTeacher(' + cellData + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>删除</a>');
                    }
                }
            ],
            //initComplete: function (settings) {
            //    $('.dataTables_filter input').unbind("keyup click").bind('keyup click',
            //    function (e) {
            //        //监听回车键
            //        if (e.keyCode === 13) {
            //            $('#simpledatatable').dataTable().fnReloadAjax(null, null, false);
            //        }
            //    });
            //},
            "aaSorting": []
        });

        return table;
    }
    $(function () {
        teacherdatatable = initTable();
    });
})();

function fnDeleteTeacher(id) {

    var teacherService = abp.services.app.teacher;
    var courseService = abp.services.app.course;

    abp.message.confirm("教师教授的课程信息及教师发布的上课信息将会一起被删除，您确定要删除该条信息么？", function (isconfirm) {
        if (isconfirm) {
            courseService.checkTeacherEnabledDeleted(id).done(function (result) {
                if (result.enabled) {
                    teacherService.deleteTeacher(id).done(function () {
                        teacherdatatable.fnReloadAjax(null, null, true);
                        abp.message.info("删除成功！");
                    });
                } else {
                    abp.message.info("无法删除该教师信息，已有学生预约了该教师的课程。");
                }
            });
        }
    });
}
function fnResetTeacher(id) {

    var teacherService = abp.services.app.teacher;

    abp.message.confirm("确认将该教师的密码重置为888888么？", function (isconfirm) {
        if (isconfirm) {
            teacherService.resetTeacher(id).done(function (result) {

                abp.message.info("修改成功！");
            });
        }
    });
}
