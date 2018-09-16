var app = angular.module('AngularAuth', ['ngRoute', 'ngCookies']);
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
        .when('/Login', {
            controller: 'login',
            templateUrl: '/ngViews/login.html'
        })
        .when('/Register', {
            controller: 'register',
            templateUrl: '/ngViews/register.html'
        })
        .when('/playlists', {
            controller: 'playlistsView',
            templateUrl: '/ngViews/playlists-view.html'
        })
        .when('/videolist/:playlistId', {
            controller: 'playlistVideos',
            templateUrl: '/ngViews/playlist-videos.html'
        })
        .otherwise({
            redirectTo: '/'
        })   
});

app.run(function ($rootScope, $location, $cookieStore, $http) {
    $rootScope.globals = $cookieStore.get('globals') || {};
    /*
    $rootScope.on('$locationChangeStart', function (event, next, current) {
        if ($location.path() !== '/login' && !$rootScope.globals.currentUser)
            $location.path('/login');
    });
    */
});