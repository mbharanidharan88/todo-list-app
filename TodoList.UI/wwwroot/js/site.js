// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {

    $('#modal-task').find('button.close').on('click', function () {
        $('#modal-task').modal("hide");
    });

});



function toastError(msg) {
    var toast = $('.toast')
    $(toast).removeClass('bg-success')
        .addClass('bg-danger');
    $(toast).find('.toast-body').html(msg);
    $(toast).toast('show');
}

function toastSuccess(msg) {
    var toast = $('.toast')
    $(toast).removeClass('bg-danger')
        .addClass('bg-success');
    $(toast).find('.toast-body').html(msg);
    $(toast).toast('show');
}


function Toast() {
    this._success = 'success';
    this._error = 'error';
    this._info = 'info';
    this.toast = $('.toast');

    this.show = function (msg, type) {

        console.log(this.toast);
        $(this.toast).find('.toast-body').html(msg);
        this.assignClass(type);
        $(this.toast).toast('show');
        console.log(this.toast);
    }

    this.assignClass = function (type) {

        $(this.toast).removeClass("bg-success bg-info bg-danger");

        switch (type) {
            case (this._info):
                $(this.toast).addClass("bg-info");
                break;
            case (this._error):
                $(this.toast).addClass("bg-danger");
                break;
            default:
                $(this.toast).addClass("bg-success");
        }
    }
}

Toast.prototype.success = function (msg) {
    this.show(msg, this._success);
}

Toast.prototype.info = function (msg) {
    this.show(msg, this._info);
}

Toast.prototype.error = function (msg) {
    this.show(msg, this._error);
}

var toast = new Toast;