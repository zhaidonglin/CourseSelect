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
                "infoEmpty": ""
            },

            "processing": true,
            "serverSide": true,

            ajax: function (data, callback, settings) {
                console.log(data);

                var input = {
                    PageSize: data.length,
                    Start: data.start           //1
                };

                courseService.getSelfCredits(input).done(function (result) {

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
                { "data": "totalCadit" }
            ],

            "aaSorting": []
        });
    });
})();