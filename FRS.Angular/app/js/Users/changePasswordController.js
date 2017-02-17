/**=========================================================
 * Module: change password
 * change password Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('changePasswordController', changePasswordController);

    changePasswordController.$inject = ['$scope', 'ReferenceDataService', 'toaster', 'SweetAlert','$state'];
    function changePasswordController($scope, changePasswordService, toaster, SweetAlert, $state) {
        var vm = this;
        vm.UserPassword = {};

        vm.resetPassword = function () {
            if (vm.changePasswordForm.$valid) {

                changePasswordService.save(vm.UserPassword, function (response) {
                    if (response) {
                        $.unblockUI();
                        toaster.success("Notification", "Password Changed Successfully.");
                        vm.changePasswordForm.$setPristine();
                    }
                },
                function (err) {
                    $.unblockUI();
                    toaster.error("Error", showErrors(err));
                    vm.changePasswordForm.$setPristine();
                }, '/api/Account/ChangePassword/');
            } else {
                vm.changePasswordForm.OldPassword.$dirty = true;
                vm.changePasswordForm.NewPassword.$dirty = true;
                vm.changePasswordForm.ConfirmPassword.$dirty = true;
            }
        }

        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };

        vm.cancelBtn = function () {
            if (!vm.changePasswordForm.$dirty) {
                $state.go('app.Solution');
            } else {
                SweetAlert.swal(null, function (isConfirm) {
                    if (isConfirm) {
                        $state.go('app.Solution');
                    }
                });
            }
        }

    }
})();
