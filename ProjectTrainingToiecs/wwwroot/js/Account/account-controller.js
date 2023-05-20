$(document).ready(function () {
    $('course-type').val(1);
});
$(function () {
    $('#on-login').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var userName = $('#user-name').val();
        var password = $('#password').val();
        userName = userName == "" ? null : userName;
        password = password == "" ? null : password;
        if (userName == null || password == null) {
            alert('Tên đăng nhập hoặc mật khẩu không được để trống');
            return;
        }
        var data = { UserName: userName, Password: password };
        $.ajax({
            url: "/Users/LoginUser",
            type: "POST",
            data: data,
            success: function (result) {
                if (result.result == 1) {
                    var url = result.role == 1 ?
                        window.location.protocol + "//" + window.location.host + '/Users/Index1'
                        :window.location.protocol + "//" + window.location.host + '/Home/Index';
                    location.href = url;
                } else {
                    alert('Tên đăng nhập hoặc mật khẩu không đúng');
                }
            }
        });
    })
})
function onLogin() {
    
}
function onOpenFormRegister() {
    var url = window.location.protocol + "//" + window.location.host + '/Users/Register';
    location.href = url;
}
$(function () {
    $('#on-register').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var userId = $('#user-id').val();
        if (userId  > 0) { return; }
        var userName = $('#user-name').val();
        var password = $('#password').val();
        var rePassword = $('#re-password').val();
        var fullName = $('#full-name').val();
        var email = $('#email').val();
        var phone = $('#phone').val();
        var type = $('#course-type').val();
        userName = userName == "" ? null : userName;
        password = password == "" ? null : password;
        rePassword = rePassword == "" ? null : rePassword;
        fullName = fullName == "" ? null : fullName;
        email = email == "" ? null : email;
        phone = phone == "" ? null : phone;
        if (userName == null || password == null || rePassword == null || email == null || fullName == null) {
            alert('Trường gán dấu (*) là thông tin bắt buộc');
            return;
        }
        if (rePassword != password) {
            alert('Mật khẩu không khớp');
            return;
        }
        var passw = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
        if (!password.match(passw)) {
            alert('Correct, try another...')
            return true;
        }
        document.getElementById("loading").style.display = "block";
        var data = { UserName: userName, Password: password, FullName: fullName, Email: email, TypeCourse: type };
        $.ajax({
            url: "/Users/RegsiterUser",
            type: "POST",
            data: data,
            success: function (result) {
                document.getElementById("loading").style.display = "none";
                if (result.data == 1) {
                    $('#re-code-muber').val(result.code);
                    document.getElementById("view-code").style.display = "block";
                    $('#on-register').prop('disabled', true);
                    alert('Bạn vui lòng kiểm tra email để hoàn thành đăng ký');
                    //setInterval();
                }else {
                    alert('Gặp lỗi trong quá trình đăng ký');
                   
                }
            }
        });
    });
});
function onDirLogin() {
    var url = window.location.protocol + "//" + window.location.host + '/Users/Login';
    location.href = url;
}

