$(document).ready(function () {
    $(document).on('click', '.likeButton', function () {
        var data = {
            "Post_ID": $('#specificPostID').attr('value'),
            "Receiver_ID": $('#posterID').attr('value')
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
            window.location = homePageUrl;
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
            window.location = homePageUrl;
        }
    })

    //Source: http://stackoverflow.com/questions/10194728/jquery-click-event-works-only-once
    $(document).delegate('.CommentButton', 'click', function () {
        var selector = $(this).data('comment');
        var data = {
            "Post_ID": this.value,
            "Content": $('.textAreaComment[value=' + this.value + ']').val()
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
            window.location = homePageUrl;
        }
    })

    $(document).on('mouseover', '.likeList', function () {
        $('[data-toggle="popover"]').popover();
    })

});