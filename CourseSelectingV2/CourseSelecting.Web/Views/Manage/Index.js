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
                }
            },

            "processing": true,
            "serverSide": true,
            //"ajax": "@Url.Action("JsonData")",
            ajax: function (data, callback, settings) {

                var input = {
                    PageSize: data.length,
                    PageCount: data.start
                };

                teacherService.getTeachers(input).done(function (result) {
                    var callbackObj = {
                        darw: data.darw,
                        recordsTotal: result.items.length,
                        recordsFiltered: result.items.length,
                        data: result.items
                    }
                    callback(callbackObj);
                });
            },

            "columns": [
                { "data": "id" },
                { "data": "teacherNo" },
                { "data": "name" },
                { "data": "department" },
                { "data": "userName" },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html('<a href="/manage/editTeacher?id=' + cellData + '" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>编辑</a>&nbsp;&nbsp;<a href="javascript:void(0);" onclick="fnDeleteTeacher(' + cellData + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>删除</a>');
                    }
                }
            ],

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
    abp.message.confirm("您确定要删除该条信息么？", function (isconfirm) {
        if (isconfirm) {
            teacherService.deleteTeacher(id).done(function () {
                teacherdatatable.fnReloadAjax(null, null, true);
            });
        }
    });
}
