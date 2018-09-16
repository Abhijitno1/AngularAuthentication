angular.module('AngularAuth').controller('playlistVideos', function ($scope, $routeParams, $route, uTubeService) {
    console.log('playlists view controller loaded');
    // Ref: https://www.guru99.com/angularjs-routes.html

    var playlistId = $routeParams.playlistId;
    uTubeService.getPlaylistVideos(playlistId).then(function (videos) {
        $scope.videos = videos;
    });

    $scope.openVideo = function (video) {
        window.open('https://www.youtube.com/watch?v=' + video.snippet.resourceId.videoId);
    };
});