angular.module('AngularAuth').controller('about', function ($scope, dataService) {
    console.log('about controller loaded');

    loginViewModel = {
        UserName: 'shivani',
        Password: 'shivani',
        RememberMe: false
    };

    $scope.getToken = function () {
        dataService.getOAuth2Token(loginViewModel);
    };

    $scope.getProtectedData = function () {
        dataService.getProtectedData()
        .success(function (data, status, headers) {
            alert('Hello ' + data.join(' '));
        });
    }
});