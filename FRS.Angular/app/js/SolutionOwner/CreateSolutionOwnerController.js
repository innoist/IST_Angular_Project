//Create Solution Owner
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('CreateSolutionOwnerController', CreateSolutionOwnerController);

    CreateSolutionOwnerController.$inject = ['$state', '$stateParams', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster'];

    function CreateSolutionOwnerController($state, $stateParams, $rootScope, SolutionOwnerService, SweetAlert, toaster) {
        var vm = this;
        var solutionOwnerId = 0;
        vm.update = false;
        vm.submitted = false;
        vm.SolutionOwner = {};
        SolutionOwnerService.url = "/api/SolutionOwner/";

        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };
        //#region Post Data
        vm.save = function (isNew) {
            vm.submitted = true;
            if (vm.formValidate.$valid) {
                console.log('Submitted!!');
            } else {
                toaster.pop("error", "Error", "Fields are required");
                return false;
            }

            SolutionOwnerService.save(vm.SolutionOwner, onSuccess, onError);
            function onSuccess(response) {
                $.unblockUI();
                if (response.data === true) {
                    toaster.pop("success", "Notification", "Data has been saved successfully.");
                    if (isNew) {
                        //reseting form
                        vm.formValidate.$setPristine();
                        vm.submitted = false;
                        solutionOwnerId = 0;
                        vm.SolutionOwner = {};
                        $state.go('app.CreateSolutionOwner');
                    }
                    if (!isNew) {
                        $state.go('app.SolutionOwner');
                    }
                } else {
                    toaster.error("Notification", "Solution Owner with this code already exists.");
                }
            }
            function onError(err) {
                toaster.error(err.statusText, err.data.Message);
                showErrors(err);
            }
        }
        //#endregion

        vm.cancelBtn = function () {
            if (!vm.formValidate.$dirty) {
                $state.go('app.SolutionOwner');
            } else {
                SweetAlert.swal(null, function (isConfirm) {
                    if (isConfirm) {
                        $state.go('app.SolutionOwner');
                    }
                });
            }
        }

        if ($stateParams.Id === "") {
            vm.edit = true;
            return;
        }


        solutionOwnerId = $stateParams.Id;
        SolutionOwnerService.loadById(solutionOwnerId, function (response) {
            $.unblockUI();
            if (response) {
                vm.update = true;
                vm.SolutionOwner = response;
                solutionOwnerId = response.Id;
                toaster.success("", "Data has been loaded successfully.");
            } else {
                solutionOwnerId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });
    }
})();