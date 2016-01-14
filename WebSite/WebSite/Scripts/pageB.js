$(document).ready(function () {
    $('.popup-send-message').magnificPopup({
        type: 'inline',
        preloader: false,
        focus: '#messageBody',

        callbacks: {
            beforeOpen: function () {
                if ($(window).width() < 700) {
                    this.st.focus = false;
                } else {
                    this.st.focus = '#messageBody';
                }
            }
        }
    });

    $('.popup-send-email-message').magnificPopup({
        type: 'inline',
        preloader: false,
        focus: '#email',

        callbacks: {
            beforeOpen: function () {
                if ($(window).width() < 700) {
                    this.st.focus = false;
                } else {
                    this.st.focus = '#email';
                }
            }
        }
    });

    $(document).on('click', '.popup-modal-dismiss', function (e) {
        e.preventDefault();
        $.magnificPopup.close();
        $(this).parents('form')[0].reset();
    });

    $("#sendmessage").submit(function (event) {
        var that = this;
        $.ajax({
            url: that.action,
            type: that.method,
            data: $('form#sendmessage').serialize(),
            success: function (result) {
                $.magnificPopup.close();
                that.reset();
                $('#pageBMessage').html(result);
            },
            error: function (jqXHR, exception) {

            }
        });
        event.preventDefault();
    });

    $("#sendemail").submit(function (event) {
        var that = this;
        $.ajax({
            url: that.action,
            type: that.method,
            data: $('form#sendemail').serialize(),
            success: function (result) {
                $.magnificPopup.close();
                that.reset();
            },
            error: function (jqXHR, exception) {

            }
        });
        event.preventDefault();
    });

});