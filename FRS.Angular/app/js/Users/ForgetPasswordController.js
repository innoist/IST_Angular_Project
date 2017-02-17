(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ForgotPasswordController', ForgotPasswordController);

    ForgotPasswordController.$inject = ['$http', '$state', '$stateParams', 'toaster', '$rootScope', 'ReferenceDataService'];
    function ForgotPasswordController($http, $state, $stateParams, toaster, $rootScope, forgotPasswordService) {
        var vm = this;
        vm.hasError = false;

        vm.recoverPassword = function () {

            if (vm.recoverForm.$valid) {
                var data = { Email: vm.Email };
                forgotPasswordService.save(data, function (response) {
                    if (response) {
                        $.unblockUI();
                        vm.hasError = false;
                        vm.authMsg = "An e-mail containing instructions to reset your password has been sent to your email-address.";
                    }
                },
                function (err) {
                    $.unblockUI();
                    vm.hasError = true;
                    vm.authMsg = showErrors(err);
                }, '/api/Account/ForgotPassword/');
            } else {
                vm.recoverForm.email.$dirty = true;
            }


        }

    }
})();