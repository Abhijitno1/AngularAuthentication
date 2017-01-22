angular.module('AngularAuth').controller('login', function ($scope, dataService, $location) {
    console.log('login controller loaded');
    $scope.loginViewModel = {
        UserName: '',
        Password: '',
        RememberMe: false
    };

    $scope.doLogin = function () {
        dataService.doLogin($scope.loginViewModel)
        .success(function (data, status, headers) {
            if (data.success === true)
                $location.url('/');
        });
    };
});