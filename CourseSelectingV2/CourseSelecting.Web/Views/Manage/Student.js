var stduentdatatable;
(function () {
    var studentService = abp.services.app.student;

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
                    //PageCount: data.start
                    Start: data.start,      //1
                    //KeyWord: data.search.value
                    KeyWord: data.search.value
                };

                studentService.getStudents(input).done(function (result) {
                    var callbackObj = {
                        darw: data.darw,
                        //recordsTotal: result.items.length,
                        //recordsFiltered: result.items.length,
                        recordsTotal: result.totalCount,      //1
                        recordsFiltered: result.totalCount,    //1
                        data: result.items
                    }
                    callback(callbackObj);
                });
            },

            "columns": [
                { "data": "id" },
                //{ "data": "studentNo" },       //2
                { "data": "name" },
                { "data": "department" },
                { "data": "major" },
                {
                    "data": "gender",
                    "createdCell": function (td, cellData, rowData, row, col) { $(td).html(cellData==0 ? "<option>男</option>" : "<option>女</option>")}
                },
                    

                { "data": "userName" },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html('<a href="/manage/StudentEdit?id=' + cellData + '" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>编辑</a>&nbsp;&nbsp;<a href="/manage/SelectStudentCourse?id=' + cellData + '" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>选项目</a>&nbsp;&nbsp;<a href="/manage/SSCourseTimeTables?id=' + cellData + '" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>选课</a>&nbsp;&nbsp;<a href="javascript:void(0);" onclick="fnResetStudent(' + cellData + ')" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>重置密码</a>&nbsp;&nbsp;<a href="javascript:void(0);" onclick="fnDeleteStudent(' + cellData + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>删除</a>');
                    }
                }
            ],

            "aaSorting": []
        });

        return table;
    }
    $(function () {
        studentdatatable = initTable();
    });
})();

function fnDeleteStudent(id) {

    var studentService = abp.services.app.student;
    abp.message.confirm("学生选课信息及预约信息将会被一起删除，您确定要删除该条信息么？", function (isconfirm) {
        if (isconfirm) {
            studentService.deleteStudent(id).done(function () {

                studentdatatable.fnReloadAjax(null, null, true);

                abp.message.info("删除成功！");
            });
        }
    });
}
function fnResetStudent(id) {

    var studentService = abp.services.app.student;

    abp.message.confirm("确认将该学生的密码重置为888888么？", function (isconfirm) {
        if (isconfirm) {
            studentService.resetStudent(id).done(function (result) {

                abp.message.info("修改成功！");
            });
        }
    });
}
