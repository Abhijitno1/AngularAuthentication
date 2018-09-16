angular.module('AngularAuth').controller('playlistsView', function ($scope, $location, uTubeService) {
    console.log('playlists view controller loaded');
    // Ref: https://www.guru99.com/angularjs-routes.html

    uTubeService.getMyPlaylists().then(function (playlists) {
        $scope.playlists = playlists;
    });

    $scope.openPlaylist = function (playlistId) {
        $location.path('videolist/' + playlistId)
    };
});