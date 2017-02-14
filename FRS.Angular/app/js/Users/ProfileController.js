(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ProfileController', ProfileController);

    ProfileController.$inject = ['$scope', '$stateParams', '$state', '$localStorage', 'ReferenceDataService', 'toaster', 'SweetAlert'];

    function ProfileController($scope, $stateParams, $state, $localStorage, ProfileService, toaster, SweetAlert) {
        var vm = this;
        var userName = '';
        vm.Admin = true;
        $scope.disabled = false;
        vm.matchPassword = true;
        vm.userTaken = false;
        ProfileService.url = '/api/UserBaseData/';

        vm.save = function (isNew) {
            if (vm.formValidate.$valid) {

                if (!angular.equals(vm.user.Password, vm.user.ConfirmPassword)) {
                    vm.matchPassword = false;
                    toaster.error('Alert', 'Password don\'t match');
                    return false;
                }
                ProfileService.save(vm.user, function (response) {
                    $.unblockUI();
                    if (response) {
                        toaster.success("Data has been saved successfully.");

                        if (isNew) {
                            //reseting form
                            vm.formValidate.$setPristine();
                            vm.submitted = false;
                            userName = '';
                            vm.user = {};
                            $state.go('app.Profile');
                        }
                        if (!isNew) {
                            $state.go('app.Users');
                        }
                    }
                }, function (err) {
                    $.unblockUI();
                    toaster.error("Alert", showErrors(err));
                }, '/api/Account/Register');
            }
        }

        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };

        if ($stateParams.userName === "") {
            vm.edit = true;
        }

        userName = $stateParams.userName;
        ProfileService.load('/api/UserBaseData/GetUser?userName=' + userName, null, function (response) {
            $.unblockUI();
            if (response) {
                vm.user = response;
                vm.update = true;
            }
        });

        vm.cancelBtn = function () {
            if (!vm.formValidate.$dirty) {
                $state.go('app.Users');
            } else {
                SweetAlert.swal(null, function (isConfirm) {
                    if (isConfirm) {
                        $state.go('app.Users');
                    }
                });
            }
        }

        vm.checkUserName = function () {
            var val = vm.user.UserName;
            //Check if input is more that 1 char and less than 10
            if (val.length >= 4 && val.length < 20)
                return ProfileService.load('/api/UserBaseData/GetUserName?username=' + val, null, function(response) {
                    $.unblockUI();
                    if (response) {
                        vm.userTaken = true;
                    } else {
                        vm.userTaken = false;
                    }
                });
        }


    }
})();