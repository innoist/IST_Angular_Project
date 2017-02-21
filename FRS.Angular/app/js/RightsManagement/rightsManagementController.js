/**=========================================================
 * Module: Rights Management
 * Rights Management Controller
 =========================================================*/
(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('RightsManagementController', RightsManagementController);

    RightsManagementController.$inject = ['$scope', 'ReferenceDataService', 'toaster'];

    function RightsManagementController($scope, rightsManagementService, toaster) {
        // ReSharper restore InconsistentNaming

        var vm = this;

        $scope.role = {};

        // Wire Role Change
        $scope.$watch("role.selected", function (newValue, oldValue) {
            if (oldValue && newValue && (oldValue.Id !== newValue.Id)) {
                vm.getRights(newValue.Id);
            }
        });

        // Keep filter open initially
        $scope.isFilterOpen = true;

        // on get Rights success
        vm.onGetRightsSuccess = function (data) {
            $.unblockUI();
            if (!data) {
                return;
            }

            vm.roles = data.Roles;
            vm.rights = data.Rights;
            // If Selected Role then get the role from roles list and select it
            if (data.SelectedRoleId) {
                var selectedRole = $(data.Roles).filter(function (index, item) {
                    return item.Id === data.SelectedRoleId;
                });
                if (selectedRole.length > 0) {
                    $scope.role.selected = selectedRole[0];
                }
            }
            // Reset Form
            vm.rightsForm.$setPristine();
        };

        // on get Rights failure
        vm.onGetRightsFailure = function () {
            $.unblockUI();
        };

        // Get Rights
        vm.getRights = function (roleId) {
            // get rights from service
            var data = { RoleId: roleId }
            rightsManagementService.load('/api/RightsManagement/', data, vm.onGetRightsSuccess, vm.onGetRightsFailure);
        };

        vm.getRights();

        // Wire up input checkbox event
        vm.onChange = function (data) {
            // If item is parent and Is Selected then find its child and mark them Selected
            if (data) {
                if (data.IsParent) {
                    var childMenuItems = $(vm.rights).filter(function (index, item) {
                        return item.ParentId === data.MenuId;
                    });
                    if (childMenuItems) {
                        angular.forEach(childMenuItems, function (item) {
                            item.IsSelected = data.IsSelected;
                        });
                    }
                }
                else {
                    var parent = $(vm.rights).filter(function (index, item) {
                        return item.MenuId === data.ParentId;
                    });

                    if (parent.length > 0) {
                        parent = parent[0];
                        if (!data.IsSelected) {
                            // Check if other siblings are also unchecked then uncheck parent
                            var siblingMenuItems = $(vm.rights).filter(function (index, item) {
                                return item.ParentId === data.ParentId && item.MenuId !== data.MenuId && item.IsSelected;
                            });
                            if (siblingMenuItems.length === 0) {
                                parent.IsSelected = false;
                            }
                        }
                        else {
                            parent.IsSelected = true;
                        }
                    }
                }
            }
        };

        // Get Selected Menus
        vm.getSelectedMenuIds = function () {
            var selectedMenuIds = "";
            $(vm.rights).each(function (index, item) {
                if (item.IsSelected) {
                    if (!selectedMenuIds) {
                        selectedMenuIds = item.MenuId;
                    } else {
                        selectedMenuIds += "," + item.MenuId;
                    }
                }
            });

            return selectedMenuIds;
        };

        // ON Update Rights Success
        vm.onUpdateSuccess = function () {
            $.unblockUI();
            toaster.pop("success", "Notification", "Rights updated successfully");
            // Reset Form
            vm.rightsForm.$setPristine();
        };

        // ON Update Rights Failure
        vm.onUpdateFailure = function (error) {
            $.unblockUI();
            toaster.error("Failed to update rights. Error: " + error);
        };

        // Update Rights
        vm.update = function () {
            // Get the Selected Menu Ids
            var selectedMenuIds = vm.getSelectedMenuIds();
            var data= {
                RoleId: $scope.role.selected.Id,
                SelectedMenuIds: selectedMenuIds
            }

            rightsManagementService.save(data, vm.onUpdateSuccess, vm.onUpdateFailure, '/api/RightsManagement/');
        };
    }
})();