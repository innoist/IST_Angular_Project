//Create Filter
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('CreateFilterController', CreateFilterController);

    CreateFilterController.$inject = ['$state', '$stateParams', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster'];

    function CreateFilterController($state, $stateParams, $rootScope, FilterService, SweetAlert, toaster) {
        var vm = this;
        var filterId = 0;
        vm.update = false;
        vm.submitted = false;
        vm.Filter = {};
        FilterService.url = "/api/Filter/";

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
            vm.Filter.FilterCategoryId = vm.FilterCategories.selected ? vm.FilterCategories.selected.Id : null;
            FilterService.save(vm.Filter, onSuccess, onError);
            function onSuccess(response) {
                $.unblockUI();
                if (response.data === true) {
                    toaster.pop("success", "Notification", "Data has been saved successfully.");
                    if (isNew) {
                        //reseting form
                        vm.formValidate.$setPristine();
                        vm.submitted = false;
                        filterId = 0;
                        vm.Filter = {};
                        vm.FilterCategories.selected = null;
                        $state.go('app.CreateFilter');
                    }
                    if (!isNew) {
                        $state.go('app.Filter');
                    }
                } else {
                    toaster.error("Notification", "Filter with this code already exists.");
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
                $state.go('app.Filter');
            } else {
                SweetAlert.swal(null, function (isConfirm) {
                    if (isConfirm) {
                        $state.go('app.Filter');
                    }
                });
            }
        }

        if ($stateParams.Id === "") {
            vm.Filter.IsActive = true;
            vm.edit = true;
        }

        filterId = $stateParams.Id ? $stateParams.Id : 0;
        FilterService.loadById(filterId, function (response) {
            $.unblockUI();
            if (response) {
                if (response.FilterModel) {
                    vm.Filter = response.FilterModel;
                    vm.update = true;
                    toaster.success("", "Data has been loaded successfully.");
                }
                vm.FilterCategories = response.FilterCategories;
                filterId = response.Id;
                if (vm.Filter) {
                    //Filter category dropdown
                    if (vm.Filter.FilterCategoryId && vm.Filter.FilterCategoryId > 0) {
                        var filter = vm.FilterCategories.find(f => f.Id === response.FilterModel.FilterCategoryId);
                        if (filter)
                            vm.FilterCategories.selected = filter;
                        vm.FilterCategories.required = true;
                    }
                }
            } else {
                filterId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });

        vm.clearFilterCategories = function ($event) {
            $event.stopPropagation();
            vm.FilterCategories.selected = null;
        };
    }
})();