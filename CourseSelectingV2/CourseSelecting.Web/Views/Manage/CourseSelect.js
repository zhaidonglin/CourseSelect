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
                    KeyWord: data.search.value,
                    Start: data.start
                };

                subjectService.getSelectSubjectProjects(input).done(function (result) {
                    var callbackObj = {
                        darw: data.darw,
                        recordsTotal: result.totalCount,      //1
                        recordsFiltered: result.totalCount,    //1
                        data: result.items
                    }
                    callback(callbackObj);
                });
            },

            "columns": [
                { "data": "id" },
                { "data": "name" },
                { "data": "subjectStyle" },
                { "data": "type" },
                {
                    "data": "isCompulsory",
                    "createdCell": function (td, cellData, rowData, row, col) { $(td).html(cellData ? "<span style='color:red;'>必修</span>" : "<span style='color:green;'>选修</span>") }
                },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        var htmlstr = "";
                        
                        console.log(rowData);

                        //if (!rowData.isCompulsory) {

                            if (rowData.isSelected) {
                                htmlstr = '<a href="javascript:void(0);" onclick="fnDeleteSubject(' + cellData + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>取消</a>';
                            } else {
                                htmlstr = '<a "javascript:void(0);" id="color" onclick="fnSelectSubject(' + cellData + ')" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>选择</a>';
                            }
                        //}

                        $(td).html(htmlstr);
                        }
                    }
                ],

            "aaSorting": []
        });
    });
})();

function fnSelectSubject(id) {
    var subjectService = abp.services.app.subjectProject;
    abp.message.confirm("您确定要选择本次课程么？", function (isconfirm) {
        if (isconfirm) {
            
            subjectService.addSelectSubjectProject({
                courseId: id
                
            }).done(function () {
                bootbox.alert("<div class=\"modal-body\">选择成功!</div>", function () {

                    
                    $('#subjectdatatable').dataTable().fnReloadAjax(null, null, true);
                    
                });
            });
        }
    });
}

function fnDeleteSubject(id) {

    var subjectService = abp.services.app.subjectProject;
    abp.message.confirm("您确定要取消本次课程么？", function (isconfirm) {
        if (isconfirm) {
            subjectService.selectDeleteSubjectProject(id).done(function () {

                bootbox.alert("<div class=\"modal-body\">取消成功!</div>", function () {

                    $('#subjectdatatable').dataTable().fnReloadAjax(null, null, true);

                });
            });
        }
    });
}
