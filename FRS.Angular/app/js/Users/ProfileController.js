(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ProfileController', ProfileController);

    ProfileController.$inject = ['$scope', '$stateParams', '$state', '$localStorage', 'ProfileService', 'toaster'];

    function ProfileController($scope, $stateParams, $state, $localStorage, ProfileService, toaster) {
        var vm = this;

        vm.Admin = true;
        $scope.disabled = false;

        if ($localStorage.authorizationData.UserRole === "Admin") {
            vm.Admin = false;
        }

        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };

        vm.saveProfile = function () {
            if (vm.userForm.$valid) {
                ProfileService.saveProfile(vm.user, function (response) {
                    if (response) {
                        toaster.success("Data has been saved successfully.");
                        $state.go('app.Users');
                    }
                }, function (err) {
                    toaster.pop("error", "Alert", showErrors(err));
                });
            }
        }
        if ($localStorage.authorizationData.UserRole !== "SystemAdministrator")
            $scope.disabled = true;

        if (!$stateParams.userName || $stateParams.userName === "")
            return;

        ProfileService.loadProfile($stateParams.userName, function (response) {
            if (!response)
                return;
            vm.user = response;
            $scope.update = true;

        });
    }
})();