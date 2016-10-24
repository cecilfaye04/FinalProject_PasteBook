$(document).ready(function () {

    $("#txtUserName").blur(function () {
        isNull($(this).val(), "Username")

    });




    function isNull(value,type)
    {
        if (value == "") {
            $('#validationUserName').text(type + " is required.");
        }
    }

    function CanContain(value,type)
    {
        if (value ) {

        }
    }











});