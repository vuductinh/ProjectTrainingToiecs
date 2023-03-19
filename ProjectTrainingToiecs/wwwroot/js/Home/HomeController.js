$('#sign-out').click(function (e) {
    e.preventDefault();
    e.stopPropagation();
    $.ajax({
        url: "/Users/SignOut",
        type: "GET",
        success: function (result) {
            if (result.result == 1) {
                var url = window.location.protocol + "//" + window.location.host + '/Users/Login';
                location.href = url;
            }
        }
    });
});
