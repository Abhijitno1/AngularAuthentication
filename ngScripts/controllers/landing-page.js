angular.module('AngularAuth').controller('landingPage', function ($scope, $rootScope, $location, dataService) {
    console.log('landing page controller loaded');
    $rootScope.$watch('globals.currentUser', function () {
        $scope.isUserAuthenticated = $rootScope.globals.currentUser != null;
    });

    $scope.logoff = function () {
        dataService.doLogOff()
        .success(function () {
            $location.url('/');
        });
    };
});