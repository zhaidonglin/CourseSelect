(function () {
    $(function () {
        var courseService = abp.services.app.course;
        var coursev2Service = abp.services.app.courseV2;

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
                    Start: data.start,
                    KeyWord: data.search.value
                };

                coursev2Service.query(input).done(function (result) {
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
                { "data": "subjectProject.name" },
                { "data": "name" },
                { "data": "credit" },
                { "data": "limitNumbers" },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html('<a href="/manage/courseEdit?id=' + cellData + '" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>编辑</a>&nbsp;&nbsp;<a href="/manage/GetOrderStudentTables?id=' + cellData + '" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>查看已选学生</a>&nbsp;&nbsp;<a href="javascript:void(0);" onclick="fnDeleteCourse(' + cellData + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>删除</a>');
                    }
                }
            ],

            "aaSorting": []
        });
    });
})();

function fnDeleteCourse(id) {
    var courseService = abp.services.app.course;
    var coursev2Service = abp.services.app.courseV2;
    abp.message.confirm("您确定要删除该条信息么？", function (isconfirm) {
        if (isconfirm) {
            coursev2Service.delete(id).done(function () {
                $('#simpledatatable').dataTable().fnReloadAjax(null, null, true);
                abp.message.info("删除成功！");
            });
        }
    });
}