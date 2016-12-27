(function () {
    $(function () {
        var courseService = abp.services.app.course;

        $('.spinner').spinner();

        $("#btnNewCourse").on('click', function () {

            var html = '';
            html += '<div id="addCourseModal">';
            html += '   <form id="formAddCourse" novalidate="novalidate" method="post" class="row form-horizontal bv-form modal-body">';
            html += '       <div class="col-md-12">';
            html += '           <div class="form-group">';
            html += '               <label>课程名称</label>';
            html += '               <input type="text" class="form-control" placeholder="课程名称" id="courseName" name="courseName">';
            html += '           </div>';
            html += '           <div class="form-group">';
            html += '               <label>学分</label>';
            html += '               <div class="spinner spinner-horizontal spinner-right">';
            //html += '                   <div class="spinner-buttons	btn-group">';
            //html += '                       <button type="button" class="btn spinner-down blueberry">';
            //html += '                           <i class="fa fa-minus"></i>';
            //html += '                       </button>';
            //html += '                       <button type="button" class="btn spinner-up purple">';
            //html += '                           <i class="fa fa-plus"></i>';
            //html += '                       </button>';
            //html += '                   </div>';
            html += '                   <input type="text" id="credit" name="credit" placeholder="学分" class=" form-control">';
            html += '               </div>';
            html += '           </div>';
            html += '           <div class="form-group">';
            html += '               <label>限选人数</label>';
            html += '               <div class="spinner spinner-horizontal spinner-right">';
            html += '                   <div class="spinner-buttons	btn-group">';
            html += '                       <button type="button" class="btn spinner-down blueberry">';
            html += '                           <i class="fa fa-minus"></i>';
            html += '                       </button>';
            html += '                       <button type="button" class="btn spinner-up purple">';
            html += '                           <i class="fa fa-plus"></i>';
            html += '                       </button>';
            html += '                   </div>';
            html += '                   <input type="text" id="limit" name="limit" placeholder="限选人数" class="spinner-input form-control">';
            html += '               </div>';
            html += '           </div>';
            html += '           <div class="form-group">';
            html += '               <label>备注</label>';
            html += '               <textarea rows="6" class="form-control" id="remark" name="remark" placeholder="备注"></textarea>';
            html += '           </div>';
            html += '       </div>';
            html += '   </form>';
            html += '</div>';

            bootbox.dialog({
                message: html,
                title: "新建课程",
                backdrop: false,
                className: "modal-darkorange",
                buttons: {
                    success: {
                        label: "提交",
                        className: "btn-blue",
                        callback: function () {

                            $("#formAddCourse").bootstrapValidator({
                                excluded: [':disabled'],

                                fields: {
                                    courseName: {
                                        validators: {
                                            notEmpty: {
                                                message: '课程名称不能为空'
                                            }
                                        }
                                    },
                                    credit: {
                                        validators: {
                                            notEmpty: {
                                                message: '课程名称不能为空'
                                            }
                                        }
                                    },
                                    limit: {
                                        validators: {
                                            notEmpty: {
                                                message: '课程名称不能为空'
                                            }
                                        }
                                    }
                                }
                            });

                            $("#formAddCourse").data("bootstrapValidator").validate();
                            if ($("#formAddCourse").data("bootstrapValidator").isValid()) {

                                courseService.addCourse({
                                    SubjectProjectId: $("#subjectId").val(),
                                    CourseName: $("#courseName").val(),
                                    CourseCredit: $("#credit").val(),
                                    CourseLimitNumbers: $("#limit").val(),
                                    Remark: $("#remark").val()
                                }).done(function () {
                                    $('#courseTable').dataTable().fnReloadAjax(null, null, true);
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
        });

        $('#courseTable').dataTable({
            "sDom": "Tt<'row DTTTFooter'<'col-sm-6'i><'col-sm-6'p>>",
            "iDisplayLength": 50,

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
                "infoEmpty": ""
            },

            "processing": true,
            "serverSide": true,
            //"ajax": "@Url.Action("JsonData")",
            ajax: function (data, callback, settings) {

                var input = {
                    PageSize: data.length,
                    PageCount: data.start,
                    SubjectProjectId: $("#subjectId").val()
                };

                courseService.getCourses(input).done(function (result) {
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
                { "data": "name" },
                { "data": "credit" },
                { "data": "limitNumbers" },
                {
                    "data": "id",
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).html('<a href="javascript:void(0);" onclick="fnDeleteCourse(' + cellData + ')" class="btn btn-danger btn-xs delete"><i class="fa fa-trash-o"></i>删除</a>');
                    }
                }
            ],

            "aaSorting": []
        });
    });
})();



function fnDeleteCourse(id) {

    var courseService = abp.services.app.course;
    abp.message.confirm("您确定要删除该条信息么？", function (isconfirm) {
        if (isconfirm) {
            courseService.deleteCourse(id).done(function () {
                $('#courseTable').dataTable().fnReloadAjax(null, null, true);
            });
        }
    });
}