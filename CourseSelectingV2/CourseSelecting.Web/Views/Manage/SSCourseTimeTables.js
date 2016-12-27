(function () {
    $(function () {
        var courseService = abp.services.app.course;

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
                    StudentId: $("#lblStudentId").val(),
                };

                courseService.getSSCourseTimeTables(input).done(function (result) {

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
                
                { "data": "courseTime.teacherCourse.course.name" },
                { "data": "courseTime.teacherCourse.course.subjectProject.name" },
                { "data": "courseTime.teacherCourse.teacher.surname" },
                {
                    "data": "courseTime.start",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html(new Date(cellData.replace("T", " ")).format("yyyy年MM月dd日 hh：ss"));
                    }
                
                    
                },
                { "data": "courseTime.address" },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html('<a href="javascript:void(0);" onclick="fnDeleteSSCourse(' + cellData + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>删除</a>');
                    }
                }
            ],

            "aaSorting": []
        });
    });
})();

function fnDeleteSSCourse(id) {
    var courseService = abp.services.app.course;
    abp.message.confirm("您确定要删除该条信息么？", function (isconfirm) {
        if (isconfirm) {
            courseService.deleteSSCourse(id).done(function () {
                $('#simpledatatable').dataTable().fnReloadAjax(null, null, true);
                abp.message.info("删除成功！");
            });
        }
    });
}