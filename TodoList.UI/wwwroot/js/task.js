
//$(function () {


//    function Index() {
//        var $this = this;
//        function initialize() {

//            $(".popup").on('click', function (e) {
//                modelPopup(this);
//            });

//            function modelPopup(reff) {
//                var url = $(reff).data('url');

//                //$.ajax({
//                //    url: url,
//                //    type: 'POST',
//                //    data: data,
//                //    success: function () {
//                //        $('#modal-task').find(".modal-dialog").html(data);
//                //        $('#modal-task').modal("show");
//                //    },
//                //    error: function (err) {
//                //        console.log(err)
//                //    }
//                //})

//                $.get(url).done(function (data) {
//                    $('#modal-task').find(".modal-dialog").html(data);
//                    $('#modal-task').modal("show");
//                });

//            }
//        }

//        $this.init = function () {
//            initialize();
//        };
//    }
//    $(function () {
//        var self = new Index();
//        self.init();
//    });
//});

$(function () {

    $(".datepicker").datepicker();

    $(".popup").on('click', function (e) {
        modelPopup(this);
    });

    function modelPopup(reff) {
        var url = $(reff).data('url');

        $.get(url).done(function (data) {
            $('#modal-task').find(".modal-dialog").html(data);
            $('#modal-task').modal("show");
            $.validator.unobtrusive.parse('#task-create');
            $(".datepicker").datepicker();

            //Fix,have to check modal popup close button
            $('#modal-task').find('button.close').on('click', function () {
                $('#modal-task').modal("hide");
            });
        });
    }
});

function onTaskUpsert(data) {
    console.log(data);
    $('#modal-task').modal("hide");
    location.reload();
}

function onTaskActiveChange(thisObj) {
    const taskId = $(thisObj).data("task-id");
    const url = $(thisObj).data("url");
    const checked = $(thisObj).prop("checked");
    
    $.ajax({
        type: 'GET',
        url: url,
        data: {
            'taskId': taskId,
            'isActive': checked
        },
        success: function (data) {
            console.info(typeof (data.isSucceeded));

            if (data.isSucceeded) {
                console.info(data.successMessage);
                toast.info(data.successMessage);
            }
            
        },
        error: function (err) {
            console.error(err);
            toast.error(err);
        }
    });
}

function onTaskDelete(thisObj) {
    const taskId = $(thisObj).data("task-id");
    const url = $(thisObj).data("url");

    console.info('taskId', taskId);

    $.ajax({
        type: 'GET',
        url: url,
        data: {
            'taskId': taskId,
        },
        success: function (data) {
            console.info(typeof (data.isSucceeded));

            if (data && data.isSucceeded) {
                console.info(data.successMessage);
                toast.info(data.successMessage);
                location.reload();
            }

        },
        error: function (err) {
            console.error(err);
            toast.error(err);
        }
    });
}