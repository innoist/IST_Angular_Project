//Create Solution Type
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('CreateSolutionTypeController', CreateSolutionTypeController);

    CreateSolutionTypeController.$inject = ['$state', '$stateParams', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster'];

    function CreateSolutionTypeController($state, $stateParams, $rootScope, SolutionTypeService, SweetAlert, toaster) {
        var vm = this;
        var solutionTypeId = 0;
        vm.update = false;
        vm.submitted = false;
        vm.SolutionType = {};
        SolutionTypeService.url = "/api/SolutionType/";

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

            SolutionTypeService.save(vm.SolutionType, onSuccess, onError);
            function onSuccess(response) {
                $.unblockUI();
                if (response.data === true) {
                    toaster.pop("success", "Notification", "Data has been saved successfully.");
                    if (isNew) {
                        //reseting form
                        vm.formValidate.$setPristine();
                        vm.submitted = false;
                        solutionTypeId = 0;
                        vm.SolutionType = {};
                        $state.go('app.CreateSolutionType');
                    }
                    if (!isNew) {
                        $state.go('app.SolutionType');
                    }
                } else {
                    toaster.error("Notification", "Solution Type with this code already exists.");
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
                $state.go('app.SolutionType');
            } else {
                SweetAlert.swal(null, function (isConfirm) {
                    if (isConfirm) {
                        $state.go('app.SolutionType');
                    }
                });
            }
        }

        if ($stateParams.Id === "") {
            vm.edit = true;
            return;
        }


        solutionTypeId = $stateParams.Id;
        SolutionTypeService.loadById(solutionTypeId, function (response) {
            $.unblockUI();
            if (response) {
                vm.update = true;
                vm.SolutionType = response;
                solutionTypeId = response.Id;
                toaster.success("", "Data has been loaded successfully.");
            } else {
                solutionTypeId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });
    }
})();