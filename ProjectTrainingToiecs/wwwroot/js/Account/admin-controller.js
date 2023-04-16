//(function () {
//    'use strict';

//    angular
//        .module('app')
//        .controller('admin_controller', admin_controller);

//    admin_controller.$inject = ['$location','$scope'];

//    function admin_controller($location,$scope) {
//        /* jshint validthis:true */
//        var vm = this;
//        vm.title = 'admin_controller';

//        activate();

//        function activate() { }
//        $scope.getData = function () {
//            $.ajax({
//                url: "/Users/GetDataAccount",
//                type: "POST",
//                data: data,
//                success: function (result) {
//                    if (result.status == "OK") {
//                        $scope.items = result.data;
//                    } else {
//                        alert('Lỗi lấy dữ liệu');
//                    }
//                }
//            });
//        }
//    }
//    $sco
//})();
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

