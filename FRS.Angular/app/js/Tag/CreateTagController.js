//Create Tag
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('CreateTagController', CreateTagController);

    CreateTagController.$inject = ['$state', '$stateParams', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster'];

    function CreateTagController($state, $stateParams, $rootScope, TagService, SweetAlert, toaster) {
        var vm = this;
        var tagId = 0;
        vm.update = false;
        vm.submitted = false;
        vm.Tag = {};
        TagService.url = "/api/Tag/";

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
            vm.Tag.TagGroupId = vm.TagGroups.selected ? vm.TagGroups.selected.Id : null;
            TagService.save(vm.Tag, onSuccess, onError);
            function onSuccess(response) {
                $.unblockUI();
                if (response.data === true) {
                    toaster.pop("success", "Notification", "Data has been saved successfully.");
                    if (isNew) {
                        //reseting form
                        vm.formValidate.$setPristine();
                        vm.submitted = false;
                        tagId = 0;
                        vm.Tag = {};
                        vm.TagGroups.selected = null;
                        $state.go('app.CreateTag');
                    }
                    if (!isNew) {
                        $state.go('app.Tag');
                    }
                } else {
                    toaster.error("Notification", "Tag with this code already exists.");
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
                $state.go('app.Tag');
            } else {
                SweetAlert.swal(null, function (isConfirm) {
                    if (isConfirm) {
                        $state.go('app.Tag');
                    }
                });
            }
        }

        if ($stateParams.Id === "") {
            vm.edit = true;
        }

        tagId = $stateParams.Id ? $stateParams.Id : 0;
        TagService.loadById(tagId, function (response) {
            $.unblockUI();
            if (response) {
                if (response.TagModel) {
                    vm.Tag = response.TagModel;
                    vm.update = true;
                    toaster.success("", "Data has been loaded successfully.");
                }
                vm.TagGroups = response.TagGroups;
                tagId = response.Id;
                if (vm.Tag) {
                    //Tag Group dropdown
                    if (vm.Tag.TagGroupId && vm.Tag.TagGroupId > 0) {
                        var tag = vm.TagGroups.find(t => t.Id === response.TagModel.TagGroupId);
                        if (tag)
                            vm.TagGroups.selected = tag;
                        vm.TagGroups.required = true;
                    }
                }

            } else {
                tagId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });

        vm.clearTagGroups = function ($event) {
            $event.stopPropagation();
            vm.TagGroups.selected = null;
        };
    }
})();