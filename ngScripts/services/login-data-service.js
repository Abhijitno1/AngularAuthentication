angular.module('AngularAuth').factory('dataService', function ($http, $cookieStore, $rootScope) {

    return {
        doLogin: function (loginViewModel) {
            var me = this;
            return $http.post('api/NGAccount/Login', loginViewModel)
                .success(function (data, status, headers) {
                    if (data.success === true) {
                        $rootScope.globals = {
                            currentUser: data.userName
                        };
                        //$cookieStore.put('globals', $rootScope.globals);
                    }
                });
        },
        doLogOff: function () {
            //$cookieStore.remove('globals');
            return $http.get('api/NGAccount/LogOff')
                .success(function (data, status, headers) {
                    if (data.success === true) {
                        $rootScope.globals = {};
                        //$cookieStore.delete('globals');
                    }
                });
        },
        getOAuth2Token: function (loginViewModel) {
            return $http.post('token', {
                grant_type: 'password',
                username: loginViewModel.UserName,
                password: loginViewModel.Password
            }, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                }
            })
            .success(function (data, status, headers) {
                if (data.token_type == 'bearer') {
                    $rootScope.globals = {
                        currentUser: 'shivani',
                        access_token: data.access_token
                    };
                }
            });
        },
        getProtectedData: function () {
            var accessToken = $rootScope.globals.access_token;
            return $http.get('api/NGAccount/ProtectedData', {
                headers: { authorization: accessToken ? 'Bearer ' + accessToken : null }
            });
        }
    };

});

/*
ToDo: Make changes to make login screen mandatory before accessing any other page.
After successful login set auth cookie with Base64 encoded basic authentication token
Also set token on every server request in the HTTP header
read the cookie when page is refreshed or when server redirects from login to Home page
Accordingly retrieve username from cookie and set info in rootScope for shared usage by all controllers
*/