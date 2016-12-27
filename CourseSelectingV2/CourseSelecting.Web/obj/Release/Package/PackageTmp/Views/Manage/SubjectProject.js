(function () {
    $(function () {
        var subjectService = abp.services.app.subjectProject;

        $('#subjectdatatable').dataTable({
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
                    Start: data.start,    //1
                    KeyWord: data.search.value
                };

                subjectService.getSubjectProjects(input).done(function (result) {
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
                { "data": "semester.name" },
                { "data": "name" },
                { "data": "subjectStyle" },
                { "data": "type" },
                {
                    "data": "isCompulsory",
                    "createdCell": function (td, cellData, rowData, row, col) { $(td).html(cellData ? "<span style='color:red;'>必修</span>" : "<span style='color:green;'>选修</span>") }
                },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) { $(td).html('<a href="/manage/SubjectProjectCourses?id=' + cellData + '" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>课程列表</a>') }
                },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html('<a href="/manage/SubjectProjectEdit?id=' + cellData + '" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>编辑</a>&nbsp;&nbsp;<a href="javascript:void(0);" onclick="fnDeleteSubject(' + cellData + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>删除</a>');
                    }
                }
            ],

            "aaSorting": []
        });
    });
})();

function fnDeleteSubject(id) {

    var subjectService = abp.services.app.subjectProject;
    abp.message.confirm("您确定要删除该条信息么？", function (isconfirm) {
        if (isconfirm) {
            subjectService.deleteSubjectProject(id).done(function () {
                //subjectdatatable.fnReloadAjax();
                $('#subjectdatatable').dataTable().fnReloadAjax(null, null, true);
            });
        }
    });
}
