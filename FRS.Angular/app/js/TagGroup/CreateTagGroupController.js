//Create TagGroup
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('CreateTagGroupController', CreateTagGroupController);

    CreateTagGroupController.$inject = ['$state', '$stateParams', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster'];

    function CreateTagGroupController($state, $stateParams, $rootScope, TagGroupService, SweetAlert, toaster) {
        var vm = this;
        var tagGroupId = 0;
        vm.update = false;
        vm.submitted = false;
        vm.TagGroup = {};
        TagGroupService.url = "/api/TagGroup/";

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

            TagGroupService.save(vm.TagGroup, onSuccess, onError);
            function onSuccess(response) {
                if (response.data === true) {
                    toaster.pop("success", "Notification", "Data has been saved successfully.");
                    if (isNew) {
                        //reseting form
                        vm.formValidate.$setPristine();
                        vm.submitted = false;
                        tagGroupId = 0;
                        vm.TagGroup = {};
                        $state.go('app.CreateTagGroup');
                    }
                    if (!isNew) {
                        $state.go('app.TagGroup');
                    }
                } else {
                    toaster.error("Notification", "Tag Group with this code already exists.");
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
                $state.go('app.TagGroup');
            } else {
                SweetAlert.swal(null, function (isConfirm) {
                    if (isConfirm) {
                        $state.go('app.TagGroup');
                    }
                });
            }
        }

        if ($stateParams.Id === "") {
            vm.TagGroup.IsActive = true;
            vm.edit = true;
            return;
        }
            

        tagGroupId = $stateParams.Id;
        TagGroupService.loadById(tagGroupId, function (response) {
            if (response) {
                vm.update = true;
                vm.TagGroup = response;
                tagGroupId = response.Id;
                toaster.success("", "Data has been loaded successfully.");
            } else {
                tagGroupId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });
    }
})();