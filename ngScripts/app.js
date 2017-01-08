var app = angular.module('AngularAuth', ['ngRoute']);
app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            controller: 'index',
            templateUrl: '/ngViews/home.html'
        })
        .when('/About', {
            controller: 'about',
            templateUrl: '/ngViews/about.html'
        })
        .when('/Contact', {
            //controller: 'about',
            templateUrl: '/ngViews/contact.html'
        })
        .when('/Register', {
            controller: 'register',
            templateUrl: '/ngViews/register.html'
        })
        .otherwise({
            redirectTo: '/'
        })
});