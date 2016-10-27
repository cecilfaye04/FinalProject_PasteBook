$(document).ready(function () {

    $("#txtFirstName").blur(function () {
        isNull($(this).val(), "Firstname", "validationFirstName")
        isMaxLength($(this).val(), 50, "Firstname", "validationFirstName")
        isCorrectFormat($(this).val(), "^([A-Za-z0-9-' ]+$)?", "Firstname", "validationFirstName")
    });
    $("#txtLastName").blur(function () {
        isNull($(this).val(), "Lastname", "validationLastName")
        isMaxLength($(this).val(), 50, "Lastname", "validationLastName")
        isCorrectFormat($(this).val(), "^([A-Za-z0-9-' ]+$)?", "Lastname", "validationLastName")
    });
    $("#bisrthdate").blur(function () {
        isNull($(this).val(), "Birthday", "validationBirthday")
    });
    $("#txtEmail").blur(function () {
        isNull($(this).val(), "Email Address", "validationEmail")
        isEmailExist($(this).val())
        isMaxLength($(this).val(), 50, "Email Address", "validationEmail")

    });
    $("#txtUserName").blur(function () {
        isNull($(this).val(), "Username", "validationUserName")
        isUsernameExist($(this).val())
        isMaxLength($(this).val(), 50, "Username", "validationUserName")
    });
    $("#txtPassword").blur(function () {
        isNull($(this).val(), "Password", "validationPassword")
        isMaxLength($(this).val(), 50, "Password", "validationPassword")
    });
    $("#txtConfirmPassword").blur(function () {
        isNull($(this).val(), "Confirm Password", "validationConfirmPassword")
        isPasswordMatch($('#txtPassword').val(), $(this).val())
        isMaxLength($(this).val(), 50, "Confirm Password", "validationConfirmPassword")
    });


    function isNull(value,type,textField)
    {
        if (value == "") {
            $('#'+textField+'').text("The " + type + " field is required.");
        }
    }

    function isMaxLength(value,maxLength,type,textField)
    {
        if (value.length > maxLength) {
            $('#' + textField + '').text("The " + type + " is too long, maximum length is "+ maxLength+".");
        }
    }

    function isCorrectFormat(value,expression,type,textField)
    {
        if (value.search(expression) == -1) {
            $('#' + textField + '').text("Invalid format, " + type + " can only contain alphanumeric characters,hyphen and apostrophe.");
        }
    }

    function isUsernameExist(username) {
        var data = {
            "username": username
        }
        $.ajax({
            url: checkIfUserExistUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                checkSuccess(data);
            },
            error: function () {
                alert('Something went wrong')
            }
        })
        function checkSuccess(data) {
            if (data.result == true) {
                $('#validationUserName').text("Username already exists.");
            }
        }
    }

    function isEmailExist(email) {
        var data = {
            "email": email
        }
        $.ajax({
            url: checkIfEmailExistUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                checkemailSuccess(data);
            },
            error: function () {
                alert('Something went wrong')
            }
        })
        function checkemailSuccess(data) {
            if (data.result == true) {
                $('#validationEmail').text("Email Address already exists.");
            }
        }
    }

    function isPasswordMatch(password,confirmPassword) {
        if (password != confirmPassword) {
            $('#validationPassword').text("Your password do not match.");
        }
    }






});