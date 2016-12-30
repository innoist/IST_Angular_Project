(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ForgotPasswordController', ForgotPasswordController);

    ForgotPasswordController.$inject = ['$http', '$state', '$stateParams', 'toaster', '$rootScope'];
    function ForgotPasswordController($http, $state, $stateParams, toaster, $rootScope) {
        var vm = this;
        vm.hasError = false;
        vm.recoverPassword = function () {

            if (vm.recoverForm.$valid) {
                var url = frsApiUrl + '/api/Account/ForgotPassword';
                var data = { Email: vm.Email };
                vm.submitButton = true;
                $http.post(url, data, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).success(function (response) {
                    if (response === true) {
                        vm.hasError = false;
                        vm.authMsg = "An email containing your username and instructions for resetting your password has been sent to your email-address.";
                    }
                }).error(function (err) {
                    vm.submitButton = false;
                    vm.hasError = true;
                    vm.authMsg = showErrors(err);
                    //toaster.error("Invalid Request", showErrors(err));
                });
            } else {
                vm.recoverForm.email.$dirty = true;
            }


        }

    }
})();