(function () {
    angular.module('admin_controller', [])
        .controller('admin_controller', function ($scope, $ionicActionSheet) {
            debugger;
            $scope.getData = function () {
                $.ajax({
                    url: "/Users/GetDataAccount",
                    type: "POST",
                    data: data,
                    success: function (result) {
                        if (result.status == "OK") {
                            $scope.items = result.data;
                        } else {
                            alert('Lỗi lấy dữ liệu');
                        }
                    }
                });
            }
        })
})

