angular.module('AngularAuth').controller('landingPage', function ($scope, $rootScope) {
    //alert('Angular is working');
    $scope.isUserAuthenticated = $rootScope.globals.currentUser != null;
    $scope.userName = $rootScope.globals.currentUser;

    $scope.logoff = function () {
        alert('logging off');
    };
});