(function () {
    $(function () {
        var teacherService = abp.services.app.teacher;

        $('#simpledatatable').dataTable({
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
                    //PageCount: data.start
                    Start: data.start,          //1
                    KeyWord: data.search.value,
                    CourseId: $("#lblCourseId").val(),
                };

                teacherService.getStudentCourseTables(input).done(function (result) {

                    console.log(result);
                    console.log(result.items);
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

                { "data": "courseTime.Student.studentNo" },

                { "data": "courseTime.Student.studentName" },
                { "data": "courseTime.Student.department" },
                { "data": "courseTime.Student.gender" },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html('<a href="javascript:void(0);" onclick="fnDeleteStudentCourseTables(' + cellData + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>删除</a>');
                    }
                }
            ],

            "aaSorting": []
        });
    });
})();

function fnDeleteStudentCourseTables(id) {
    var teacherService = abp.services.app.teacher;
    abp.message.confirm("您确定要删除该条信息么？", function (isconfirm) {
        if (isconfirm) {
            teacherService.deleteStudentCourseTables(id).done(function () {
                $('#simpledatatable').dataTable().fnReloadAjax(null, null, true);
                abp.message.info("删除成功！");
            });
        }
    });
}