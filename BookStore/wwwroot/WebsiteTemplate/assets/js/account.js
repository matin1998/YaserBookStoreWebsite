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
    const tokenElement =
        document.querySelector("#antiForgeryForm input[name='__RequestVerificationToken']");

    if (tokenElement) {

        const token = tokenElement.value;

        document.addEventListener("click", async function (e) {

            const button = e.target.closest(
                ".btn-increment, .btn-decrement, .remove-btn"
            );

            if (!button)
                return;

            let productId;
            let action;

            if (button.classList.contains("remove-btn")) {

                productId = button.dataset.productId;
                action = "remove";

            } else {

                const quantityContainer =
                    button.closest(".cart-product-quantity");

                if (!quantityContainer)
                    return;

                productId = quantityContainer.dataset.productId;

                action = button.classList.contains("btn-increment")
                    ? "increase"
                    : "decrease";
            }

            let url = "";

            switch (action) {

                case "increase":
                    url = "/CustomerPanel/Cart/Increase";
                    break;

                case "decrease":
                    url = "/CustomerPanel/Cart/Decrease";
                    break;

                case "remove":
                    url = "/CustomerPanel/Cart/Remove";
                    break;

                default:
                    return;
            }

            const formData = new FormData();

            formData.append("productId", productId);
            formData.append("__RequestVerificationToken", token);

            const response = await fetch(url, {
                method: "POST",
                body: formData
            });

            if (!response.ok)
                return;

            const result = await response.json();

            if (!result.success)
                return;

            updateCart(result);
        });
    }
    function updateCart(result) {

        if (result.removed) {

            document
                .getElementById("cart-row-" + result.productId)
                ?.remove();

        }
        else {

            const quantityInput = document.querySelector(
                `.cart-product-quantity[data-product-id="${result.productId}"] input`
            );

            if (quantityInput) {
                quantityInput.value = result.count;
            }

            /*document
                .getElementById("count-" + result.productId)
                .innerText = result.count;*/

            document
                .getElementById("item-total-" + result.productId)
                .innerText = result.itemTotalPrice.toLocaleString() + " تومان";
        }

        document
            .getElementById("cart-total")
            .innerText = result.cartTotalPrice.toLocaleString() + " تومان";

        document
            .getElementById("cart-subtotal")
            .innerText = result.cartTotalPrice.toLocaleString() + " تومان";
    }

    document.addEventListener("click", async function (e) {

        const updateButton = e.target.closest("#update-cart-btn");

        if (!updateButton)
            return;


        e.preventDefault();

        const tokenElement = document.querySelector(
            "#antiForgeryForm input[name='__RequestVerificationToken']"
        );

        if (!tokenElement)
            return;

        const formData = new FormData();

        formData.append(
            "__RequestVerificationToken",
            tokenElement.value
        );

        document
            .querySelectorAll(".cart-product-quantity")
            .forEach((container, index) => {

                const input = container.querySelector(
                    ".cart-quantity-input"
                );

                if (!input)
                    return;

                formData.append(
                    `Items[${index}].ProductId`,
                    container.dataset.productId
                );

                formData.append(
                    `Items[${index}].Count`,
                    input.value
                );
            });
        updateButton.classList.add("disabled");

        try {

            const response = await fetch(
                "/CustomerPanel/Cart/UpdateCart",
                {
                    method: "POST",
                    body: formData
                }
            );

            if (!response.ok)
                return;

            const result = await response.json();

            if (!result.success)
                return;

            result.items.forEach(item => {

                const input = document.querySelector(
                    `.cart-product-quantity[data-product-id="${item.productId}"] input`
                );

                if (input) {
                    input.value = item.count;
                }

                const itemTotal = document.getElementById(
                    "item-total-" + item.productId
                );

                if (itemTotal) {
                    itemTotal.innerText =
                        item.totalPrice.toLocaleString() + " تومان";
                }
            });



            document.getElementById("cart-total").innerText =
                result.totalPrice.toLocaleString() + " تومان";

            document.getElementById("cart-subtotal").innerText =
                result.totalPrice.toLocaleString() + " تومان";

        }
        finally {

            updateButton.classList.remove("disabled");
        }
    });

    document.addEventListener("submit", async function (e) {

        const form = e.target.closest("#coupon-form");

        if (!form)
            return;

        e.preventDefault();

        const codeInput = document.getElementById("coupon-code");
        const message = document.getElementById("coupon-message");
        const token = form.querySelector(
            "input[name='__RequestVerificationToken']"
        );

        if (!codeInput || !token)
            return;

        const code = codeInput.value.trim();

        if (!code) {
            message.innerText = "کد تخفیف را وارد کنید.";
            return;
        }

        const formData = new FormData();

        formData.append("Code", code);
        formData.append(
            "__RequestVerificationToken",
            token.value
        );

        try {

            const response = await fetch(
                "/CustomerPanel/Cart/ApplyCoupon",
                {
                    method: "POST",
                    body: formData
                }
            );

            if (!response.ok)
                return;

            const result = await response.json();

            message.innerText = result.message;

            if (!result.success)
                return;

            document.getElementById("cart-total").innerText =
                result.finalPrice.toLocaleString() + " تومان";

        }
        catch (error) {
            message.innerText =
                "در اعمال کد تخفیف خطایی رخ داد.";
        }
    });
    
});