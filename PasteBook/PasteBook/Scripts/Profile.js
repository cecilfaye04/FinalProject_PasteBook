$(document).ready(function () {

    $('#post').on('click', function () {
        var data = {
            postContent: $('#textAreaPost').val(),
            profileOwnerID: $('#user_id').attr('value')
        }

        $.ajax({
            url: addPostUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                AddPostSuccess(data);
            },
            error: function () {
                alert('Something went wrong')
            }
        })
        function AddPostSuccess(data) {
            $('#textAreaPost').val("")
            $("#homePartial").load(homePageUrl);
        }
    })
    //Source: http://stackoverflow.com/questions/10194728/jquery-click-event-works-only-once
    $(document).on('click', '.likeButton', function () {
        var data = {
            "Post_ID": this.value,
            "Receiver_ID": $('#posterID').attr('value')
            //"Liked_By": ID,
            //"ID" : null
        }

        $.ajax({
            url: likePostUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                LikeSuccess(data);
            },
            error: function () {
                alert('Something went wrong')
            }
        })
        function LikeSuccess(data) {
            $(document).trigger('ready');
            $("#homePartial").load(homePageUrl);
        }
    })

    $(document).on('click', '.unlikeButton', function () {
        var data = {
            "ID": this.value
        }

        $.ajax({
            url: unlikePostUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                UnLikeSuccess(data);
            },
            error: function () {
                alert('Something went wrong')
            }
        })
        function UnLikeSuccess(data) {
            $(document).trigger('ready');
            $("#homePartial").load(homePageUrl);
        }
    })

    //Source: http://stackoverflow.com/questions/10194728/jquery-click-event-works-only-once
    $(document).delegate('.CommentButton', 'click', function () {
        var selector = $(this).data('comment');
        var data = {
            "Post_ID": this.value,
            "Content": $('.textAreaComment[value='+this.value+']').val()
        }

        $.ajax({
            url: addCommentUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                CommentSuccess(data);
            },
            error: function () {
                alert('Something went wrong')
            }
        })
        function CommentSuccess(data) {
            $("#homePartial").load(homePageUrl);
        }
    })


    $(document).on('click', '#aboutMebtn', function () {
        var data = {
            "userID": ID,
            "content": $('#aboutMetextArea').val()
        }

        $.ajax({
            url: editAboutMe,
            data: data,
            type: 'POST',
            success: function (data) {
                editAboutMeSuccess(data);
            },
            error: function () {
                alert('Something went wrong')
            }
        })

        function editAboutMeSuccess(data) {
            window.location.href = profilePageUrl;
        }
    })

    $('#addFriend').on('click', function () {
        var data = {
            "User_ID": ID,
            "Friend_ID": $('#user_id').attr('value'),
            "Request": 'N',
            "Blocked": 'N',
            "ID": null
        }

        $.ajax({
            url: addFriendUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                AddSuccess(data);
            },
            error: function () {
                alert('Something went wrong')
            }
        })
    });

    function AddSuccess(data) {
        $("#profileFriendHeader").load(headerProfileUrl);
    }

    $('#btnAcceptFriend').on('click', function () {
        var data = {
            "friendID": $('#friend_id').attr('value'),
        }

        $.ajax({
            url: acceptFriendUrl,
            data: data,
            type: 'POST',
            success: function (data) {
               AcceptSuccess(data);
            },
            error: function () {
                alert('Something went wrong')
            }
        })
    });

    function AcceptSuccess(data) {
        $("#profileFriendHeader").load(headerProfileUrl);
    }

    $('#btnRejectFriend').on('click', function () {
        var data = {
            "friendID": $('#friend_id').attr('value'),
        }

        $.ajax({
            url: rejectFriendUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                AcceptSuccess(data);
            },
            error: function () {
                alert('Something went wrong')
            }
        })
    });

    function AcceptSuccess(data) {
        $("#profileFriendHeader").load(headerProfileUrl);
    }




    $(document).on('mouseover', '.likeList', function () {
        $('[data-toggle="popover"]').popover();
    })

    function readURL(input) {

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#previewImage').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#fileButton").change(function () {
        readURL(this);
    });

    $('#profilePicture').on('hidden.bs.modal', function (e) {
        $('#previewImage').attr('src', $('~/Content/Images/default.png').attr('src'));
    });


});