$(document).ready(function () {

    $('#post').on('click', function () {
        var data = {
            postContent: $('#textAreaPost').val()
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
    $(document).delegate('.likeButton', 'click', function(){
        var data = {
            "Post_ID" : this.value,
            "Liked_By": ID,
            "ID" : null
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

    $('#addFriend').on('click', function () {
        var data = {
            "User_ID" : ID,
            "Friend_ID" : $('#user_id').attr('value'),
            "Request": 'N',
            "Blocked" : 'N',
            "ID" : null
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
        //$.post(data.Url, function(partial) {
        //    $('#postArea').html(partial);
        //});
        alert("Successfully Added!")
    }



});