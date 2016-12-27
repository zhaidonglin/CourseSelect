(function () {
    $(function () {
        var studentExamTime = abp.services.app.studentExamTimeV2;

        $('#studentExamTimeTable').dataTable({
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
                    KeyWord: data.search.value,
                    StudentExamTimeId: $("#txtCourseId").val()
                };


                studentExamTime.query(input).done(function (result) {
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
                { "data": "examTime.teacherCourse.course.subjectProject.name" },
                { "data": "examTime.teacherCourse.course.name" },
                { "data": "examTime.teacherCourse.teacher.name" },
                {
                    "data": "examTime.start",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html(new Date(cellData.replace("T"," ")).format("yyyy年MM月dd日 hh：ss"));
                    }
                },
                { "data": "examTime.address" },
                { "data": "student.name" },
                { "data": "student.userName" },
                {
                    "data": "student.gender",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html(cellData === 0 ? "男" : "女");
                    }
                },
                //{ "data": "classCredit" },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html('<a href="javascript:void(0);" onclick="fnDeleteStudentExamTime(' + cellData + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>删除</a>');
                    }
                }
            ],

            "aaSorting": []
        });
    });
})();

function fnDeleteStudentExamTime(id) {
    var studentExamTime = abp.services.app.studentExamTimeV2;
    abp.message.confirm("您确定要删除该条信息么？", function (isconfirm) {
        if (isconfirm) {
            studentExamTime.delete(id).done(function () {
                $('#studentExamTimeTable').dataTable().fnReloadAjax(null, null, true);
                abp.message.info("删除成功！");
            });
        }
    });
}

//.


//function BootboxContent() {
//    var html = '';
//    html += '<div id="updateCreditModal">';
//    html += '   <form id="formUpdateCredit" novalidate="novalidate" method="post" class="row form-horizontal bv-form modal-body">';
//    html += '       <div class="col-md-12">';
//    html += '           <div class="form-group">';
//    html += '               <input type="text" class="form-control" placeholder="分数" id="credit" name="credit">';
//    html += '           </div>';
//    html += '       </div>';
//    html += '   </form>';
//    html += '</div>';

//    var object = $('<div/>').html(html).contents();

//    return object;
//}