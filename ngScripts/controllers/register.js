﻿angular.module('AngularAuth').controller('register', function ($scope) {
    console.log('register controller loaded');

    $scope.registerUser = function () {
        alert('user registered');
    };
});