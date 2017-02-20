/**=========================================================
 * Module: access-login.js
 * Demo for login api
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('LoginFormController', LoginFormController);

    LoginFormController.$inject = ['$http', '$state', '$stateParams', '$localStorage', '$rootScope'];
    function LoginFormController($http, $state, $stateParams, $localStorage, $rootScope) {
        //if ($localStorage['authorizationData']) {
        //    $rootScope.isAuthenticated = true;
        //    $state.go('home.signin');
        //}

        var vm = this;

        activate();

        ////////////////

        function activate() {
            // bind here all data from the form
            vm.account = {};
            // place the message if something goes wrong
            vm.authMsg = '';

            vm.login = function () {
                vm.authMsg = '';

                if (vm.loginForm.$valid) {
                    var data = "grant_type=password&username=" + vm.account.email + "&password=" + vm.account.password;
                    vm.submitButton = true;
                    $http
                        .post(window.frsApiUrl + '/token', data, {
                            headers: {
                                'Content-Type': 'application/x-www-form-urlencoded'
                            }
                        })
                        .success(function (response) {
                            $localStorage['authorizationData'] = {
                                token: response.access_token,
                                userName: response.userName,
                                userId: response.UserId,
                                userFullName: response.UserFullName,
                                email: response.email,
                                UserRole: response.UserRole,
                                isAuthenticated: true,
                                issued: response['.issued'],
                                expires: response['.expires'],
                                expires_in: response['expires_in']
                            };
                            $rootScope.isAuthenticated = true;
                            // Add Authorization token to every http request
                            $http.defaults.headers.common = {
                                'Authorization': 'Bearer ' + $localStorage['authorizationData'].token
                            };

                            switch (response.UserRole) {
                                case 'Admin':
                                    $rootScope.CurrentUserRole = 'Admin';
                                    if ($stateParams.returnUrl)
                                        $state.go($stateParams.returnUrl);
                                    else 
                                        $state.go('home.index');
                                    break;
                                case 'User':
                                    $rootScope.CurrentUserRole = 'User';
                                    if ($stateParams.returnUrl)
                                        $state.go($stateParams.returnUrl);
                                    else
                                        $state.go('home.index');
                                    break;
                                case 'Solution Editor':
                                    $rootScope.CurrentUserRole = 'Editor';
                                    if ($stateParams.returnUrl)
                                        $state.go($stateParams.returnUrl);
                                    else
                                    $state.go('home.index');
                                    break;
                                case 'Solution Manager':
                                    $rootScope.CurrentUserRole = 'Manager';
                                    if ($stateParams.returnUrl)
                                        $state.go($stateParams.returnUrl);
                                    else
                                    $state.go('home.index');
                                    break;
                            }

                            // assumes if ok, response is an object with some data, if not, a string with error
                            // customize according to your api

                        })
                        .error(function (err, status) {

                            vm.submitButton = false;
                            if (status === 400) {
                                vm.authMsg = 'Incorrect credentials.';
                            }
                            if (status === 500) {
                                vm.authMsg = 'Server Request Error';
                            }
                        });
                }
                else {
                    // set as dirty if the user click directly to login so we can show the validation messages
                    /*jshint -W106*/
                    vm.loginForm.account_email.$dirty = true;
                    vm.loginForm.account_password.$dirty = true;
                }
            };

        }
    }
})();