(function () {
    $(function () {
        var studentCourseTime = abp.services.app.studentCourseTimeV2;

        $('#studentCourseTimeTable').dataTable({
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
                    CourseId: $("#txtCourseId").val()
                };
                

                studentCourseTime.query(input).done(function (result) {
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
                { "data": "courseTime.teacherCourse.course.subjectProject.name" },
                { "data": "courseTime.teacherCourse.course.name" },
                {
                    "data": "courseTime.start",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html(new Date(cellData.replace("T"," ")).format("yyyy年MM月dd日 hh：mm"));
                    }
                },
                { "data": "student.name" },
                { "data": "student.userName" },
                {
                    "data": "student.gender",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html(cellData === 0 ? "男" : "女");
                    }
                },
                { "data": "classCredit" },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html('<a href="javascript:void(0);" onclick="fnUpdateCredit(' + cellData + ')" class="btn btn-info btn-xs edit"><i class="fa fa-edit"></i>评分</a>&nbsp;&nbsp;<a href="javascript:void(0);" onclick="fnDeleteCourse(' + cellData + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>删除</a>');
                    }
                }
            ],

            "aaSorting": []
        });
    });
})();

function fnDeleteCourse(id) {
    var studentCourseTime = abp.services.app.studentCourseTimeV2;
    abp.message.confirm("您确定要删除该条信息么？", function (isconfirm) {
        if (isconfirm) {
            studentCourseTime.delete(id).done(function () {
                $('#studentCourseTimeTable').dataTable().fnReloadAjax(null, null, true);
                abp.message.info("删除成功！");
            });
        }
    });
}

function fnUpdateCredit(id) {
    var studentCourseTime = abp.services.app.studentCourseTimeV2;
    bootbox.dialog({
        message: BootboxContent,
        title: "编辑学分",
        backdrop: false,
        className: "modal-darkorange",
        buttons: {
            success: {
                label: "提交",
                className: "btn-blue",
                callback: function () {

                    $("#formUpdateCredit").bootstrapValidator({
                        excluded: [':disabled'],
                        fields: {
                            credit: {
                                validators: {
                                    regexp: {
                                        regexp: /^-?\d+\.?\d{0,2}$/,
                                        message: '学分格式不正确'
                                    },
                                    notEmpty: {
                                        message: '学分格式不正确'
                                    }
                                }
                            }
                        }
                    });

                    $("#formUpdateCredit").data("bootstrapValidator").validate();
                    if ($("#formUpdateCredit").data("bootstrapValidator").isValid()) {

                        var input = {
                            Id: id,
                            ClassCredit: $("#credit").val()
                        };

                        studentCourseTime.edit(input)
                            .done(function () {
                                $('#studentCourseTimeTable').dataTable().fnReloadAjax(null, null, true);
                                abp.message.info("评分成功！");
                            });
                        return true;
                    }
                    return false;
                }
            },
            "取消": {
                className: "btn",
                callback: function () { }
            }
        }
    });

}


function BootboxContent() {
    var html = '';
    html += '<div id="updateCreditModal">';
    html += '   <form id="formUpdateCredit" novalidate="novalidate" method="post" class="row form-horizontal bv-form modal-body">';
    html += '       <div class="col-md-12">';
    html += '           <div class="form-group">';
    html += '               <input type="text" class="form-control" placeholder="分数" id="credit" name="credit">';
    html += '           </div>';
    html += '       </div>';
    html += '   </form>';
    html += '</div>';

    var object = $('<div/>').html(html).contents();

    return object;
}