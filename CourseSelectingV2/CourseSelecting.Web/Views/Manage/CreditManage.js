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
                console.log(data);

                var input = {
                    PageSize: data.length,
                    Start: data.start,
                    KeyWord: data.search.value//1
                };

                courseService.getCredits(input).done(function (result) {

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
                { "data": "student.name" },
                { "data": "subjectProject.name" },
                { "data": "classCredit" },
                { "data": "examCredit" },
                { "data": "totalCadit" },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        //console.log(cellData);
                        //console.log(rowData);
                        //console.log(row);
                        //console.log(col);
                        $(td).html('&nbsp;&nbsp;<a href="javascript:void(0);"  onclick="fnListOrderdStudent(' + cellData + ')" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>查看随堂分数</a>&nbsp;&nbsp;<a href="/manage/creditEdit?id=' + cellData + '" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>编辑</a>&nbsp;&nbsp;');
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
        StudentSubjectProjectId: id
    };

    studentCourseTime.query(input).done(function (result) {

        console.log(result.items);

        bootbox.dialog({
            message: BootboxContent(result.items),
            title: "学生随堂分数列表",
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
    html += '                <thead><tr><th>序号</th><th>课程名</th><th>授课老师</th><th>上课时间</th><th>随堂分数</th></tr></thead>';
    html += '                <tbody>';
    for (var index in objs) {
        if (objs.hasOwnProperty(index)) {
            html += '                    <tr class="even" data="' + objs[index].id + '"><td>' +
                objs[index].id +
                '</td><td>' +
                objs[index].courseTime.teacherCourse.course.name +
                '</td><td>' +
                objs[index].courseTime.teacherCourse.teacher.name +
                '</td><td>' +
                new Date(objs[index].courseTime.start.replace("T", " ")).format("yyyy年MM月dd日 hh：ss") +
                '</td><td>' +
                objs[index].classCredit +
                '</td></tr>';
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

//function fnDeleteStudentOrderd(id) {
//    var studentCourseTime = abp.services.app.studentCourseTimeV2;
//    abp.message.confirm("您确定要删除该条信息么？", function (isconfirm) {
//        if (isconfirm) {
//            studentCourseTime.delete(id).done(function () {
//                abp.message.info("删除成功！");
//                $('#courseTimeTable').dataTable().fnReloadAjax(null, null, true);
//                $("#orderdStudentTable").find("tr[data=" + id + "]").remove();
//            });
//        }
//    });
//}

