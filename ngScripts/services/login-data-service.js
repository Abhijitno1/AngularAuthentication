angular.module('AngularAuth').factory('dataService', function ($http, $cookieStore, $rootScope) {
    var doLogin = function (loginViewModel) {
        return $http.post('api/NGAccount/Login', loginViewModel)
            .success(function (data, status, headers) {
                if (data.success === true) {
                    $rootScope.globals = {
                        currentUser: data.userName
                    };
                    //$cookieStore.set('globals', 1);
                }
            });
    };

    return {
        doLogin : doLogin
    }
});

/*
ToDo: Make changes to make login screen mandatory before accessing any other page.
After successful login set auth cookie with Base64 encoded basic authentication token
Also set token on every server request in the HTTP header
read the cookie when page is refreshed or when server redirects from login to Home page
Accordingly retrieve username from cookie and set info in rootScope for shared usage by all controllers
*/