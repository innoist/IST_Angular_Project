//Create FilterCategory
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('CreateFilterCategoryController', CreateFilterCategoryController);

    CreateFilterCategoryController.$inject = ['$state', '$stateParams', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster'];

    function CreateFilterCategoryController($state, $stateParams, $rootScope, FilterCategoryService, SweetAlert, toaster) {
        var vm = this;
        var filterCategoryId = 0;
        vm.update = false;
        vm.submitted = false;
        vm.FilterCategory = {};
        FilterCategoryService.url = "/api/FilterCategory/";

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

            FilterCategoryService.save(vm.FilterCategory, onSuccess, onError);
            function onSuccess(response) {
                if (response.data === true) {
                    toaster.pop("success", "Notification", "Data has been saved successfully.");
                    if (isNew) {
                        //reseting form
                        vm.formValidate.$setPristine();
                        vm.submitted = false;
                        filterCategoryId = 0;
                        vm.FilterCategory = {};
                        $state.go('app.CreateFilterCategory');
                    }
                    if (!isNew) {
                        $state.go('app.FilterCategory');
                    }
                } else {
                    toaster.error("Notification", "Quantity Scale with this code already exists.");
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
                $state.go('app.FilterCategory');
            } else {
                SweetAlert.swal(null, function (isConfirm) {
                    if (isConfirm) {
                        $state.go('app.FilterCategory');
                    }
                });
            }
        }

        if ($stateParams.Id === "") {
            vm.FilterCategory.IsActive = true;
            vm.edit = true;
            return;
        }
            

        filterCategoryId = $stateParams.Id;
        FilterCategoryService.loadById(filterCategoryId, function (response) {
            if (response) {
                vm.update = true;
                vm.FilterCategory = response;
                filterCategoryId = response.Id;
                toaster.success("", "Data has been loaded successfully.");
            } else {
                filterCategoryId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });
    }
})();