$(function () {

    $("#loginForm").submit(function (e) {

        e.preventDefault();

        var form = $(this);

        $("#loginMessage")
            .addClass("d-none")
            .text("");

        $("#btnLogin")
            .prop("disabled", true);

        $("#btnLoginText")
            .text("در حال ورود...");

        $.ajax({

            url: form.attr("action"),

            type: "POST",

            data: form.serialize(),

            success: function (response) {

                if (response.success) {

                    $("#signin-modal").modal("hide");
                    /*var modal = bootstrap.Modal.getInstance(document.getElementById('#signin-modal'));

                    if (modal) {
                        modal.hide();
                    }*/
                    location.reload();

                }
                else {

                    $("#loginMessage")
                        .removeClass("d-none")
                        .text(response.message);

                    $("#btnLogin")
                        .prop("disabled", false);

                    $("#btnLoginText")
                        .text("ورود");
                }

            },

            error: function () {

                $("#loginMessage")
                    .removeClass("d-none")
                    .text("ارتباط با سرور برقرار نشد.");

                $("#btnLogin")
                    .prop("disabled", false);

                $("#btnLoginText")
                    .text("ورود");

            }

        });

    });
    $("#registerForm").submit(function (e) {

        e.preventDefault();

        var form = $(this);

        $("#registerMessage")
            .addClass("d-none")
            .removeClass("alert-success")
            .addClass("alert-danger")
            .html("");

        $("#btnRegister")
            .prop("disabled", true);

        $("#btnRegisterText")
            .html(
                '<span class="spinner-border spinner-border-sm"></span> در حال ثبت نام...'
            );

        $.ajax({

            url: form.attr("action"),

            type: "POST",

            data: form.serialize(),

            success: function (response) {

                if (response.success) {

                    $("#registerMessage")
                        .removeClass("d-none")
                        .removeClass("alert-danger")
                        .addClass("alert-success")
                        .html(response.message);

                    setTimeout(function () {

                        location.reload();

                    }, 1000);

                    return;
                }

                $("#registerMessage")
                    .removeClass("d-none")
                    .html(response.message);

                $("#btnRegister")
                    .prop("disabled", false);

                $("#btnRegisterText")
                    .text("ثبت نام");
            },

            error: function () {

                $("#registerMessage")
                    .removeClass("d-none")
                    .html("خطایی در ارتباط با سرور رخ داده است.");

                $("#btnRegister")
                    .prop("disabled", false);

                $("#btnRegisterText")
                    .text("ثبت نام");
            }
        });
    });
    

});