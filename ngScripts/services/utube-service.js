angular.module('AngularAuth').factory('uTubeService', function ($http, $cookieStore, $rootScope) {

    return {
        apiKey: 'AIzaSyDriGwN2baflxOS446GPVh8bphOToAvJdk',
        channel: 'UCoaobuD6LxVQ2uO6Ic3acRg',
        getMyPlaylists: function () {
            return $http.get('https://www.googleapis.com/youtube/v3/playlists?key=' + this.apiKey + '&channelId=' + this.channel + '&part=snippet,id&maxResults=20')
                .then(function (data, status, headers) {
                    return data.data.items;
                })
        },
        getPlaylistVideos: function (listId) {
            return $http.get('https://www.googleapis.com/youtube/v3/playlistItems?key=' + this.apiKey + '&playlistId=' + listId + '&part=snippet,id&maxResults=50')
            .then(function (data, status, headers) {
                return data.data.items;
            })
        }
    };

});
