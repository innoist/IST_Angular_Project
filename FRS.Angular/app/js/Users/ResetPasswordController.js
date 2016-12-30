(function () {
    //angular
    //    .module('app.pages')
    //    .controller('ResetPasswordController', ResetPasswordController);
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ResetPasswordController', ResetPasswordController);

    ResetPasswordController.$inject = ['$http', '$state', '$stateParams', '$rootScope','toaster'];
    function ResetPasswordController($http, $state, $stateParams, $rootScope, toaster) {
        var vm = this;
        vm.email = $stateParams.email;
        vm.resetPassword = function () {
            if (vm.resetPasswordForm.$valid) {
                var url = frsApiUrl + '/api/Account/ResetPassword';
                var data = { Email: $stateParams.email, code: $stateParams.code, password: vm.password, confirmpassword: vm.account_password_confirm };
                $http.post(url, data).success(function (response) {
                    console.log(response);
                    if (response) {
                        toaster.success(response);
                        $state.go('account.login');
                    }
                }).error(function (err) {
                    vm.authMsg = showErrors(err);
                    toaster.error("error",showErrors(err));
                });
            } else {
                vm.resetPasswordForm.email.$dirty = true;
                vm.resetPasswordForm.account_password.$dirty = true;
                vm.resetPasswordForm.account_password_confirm.$dirty = true;
            }
        }
    }
})();