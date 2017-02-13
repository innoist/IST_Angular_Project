/**=========================================================
 * Module: SolutionOwner
 * SolutionOwner view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('SolutionOwnerController', SolutionOwnerController);

    SolutionOwnerController.$inject = ['$state', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster', 'DTOptionsBuilder', 'DTColumnBuilder', 'DTColumnDefBuilder'];

    function SolutionOwnerController($state, $rootScope, SolutionOwnerService, SweetAlert, toaster, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder) {
        var vm = this;
        vm.Name = '';
        vm.IsDataLoaded = false;
        SolutionOwnerService.url = "/api/SolutionOwner/";

        activate();

        function activate() {
            //to empties previous function in search array 
            $.fn.dataTable.ext.search = [];
            //Custom filter in Datatable
            $.fn.dataTable.ext.search.push(
                function (settings, data, dataIndex) {
                    var name = data[0] || '';
                    name = name.replace(/\n/g, " ").replace(/<.*?>/g, "").toLowerCase();
                    var toSearch = vm.Name.replace(/\n/g, " ").replace(/<.*?>/g, "").toLowerCase();
                    var temp = name.indexOf(toSearch);
                    if (temp !== -1) {
                        return true;
                    }
                    return false;
                }
            );

            SolutionOwnerService.getAll(function (response) {
                $.unblockUI();
                if (response) {
                    vm.SolutionOwners = response;
                    vm.IsDataLoaded = true;
                }
            });

            vm.dtOptions = DTOptionsBuilder.newOptions()
                            .withOption('bFilter', true)
                            .withOption('aLengthMenu', [10, 25, 100, 500])
                            .withPaginationType('full_numbers');

            vm.dtColumns = [
                //DTColumnDefBuilder.newColumnDef(0).notVisible(),
                DTColumnDefBuilder.newColumnDef(1),
                DTColumnDefBuilder.newColumnDef(2).notSortable()
            ];

            vm.dtInstance = {};

            vm.delete = function (isCascade, solutionOwner) {
                if (isCascade) {
                    SweetAlert.swal({
                        title: 'Are you sure, you want to delete this?',
                        text: 'It cannot be undone!',
                        type: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#ee3d3d',
                        confirmButtonText: 'Yes, Delete!',
                        cancelButtonText: 'No!',
                        closeOnConfirm: true,
                        closeOnCancel: true
                    }, function(isConfirm) {
                        if (isConfirm) {
                            SolutionOwnerService.url = "/api/SolutionOwner/DeleteCascade/";
                            SolutionOwnerService.delete(solutionOwner.Id, function(response) {
                                $.unblockUI();
                                if (response) {
                                    var index = vm.SolutionOwners.indexOf(solutionOwner);
                                    vm.SolutionOwners.splice(index, 1);
                                    toaster.success("", "Deleted successfully.");
                                    SolutionOwnerService.url = "/api/SolutionOwner/";
                                }
                            });
                        }
                    });
                } else {
                    SweetAlert.swal({
                        title: 'Are you sure, you want to remove this?',
                        text: '',
                        type: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#ee3d3d',
                        confirmButtonText: 'Yes, Remove!',
                        cancelButtonText: 'No!',
                        closeOnConfirm: true,
                        closeOnCancel: true
                    }, function (isConfirm) {
                        if (isConfirm) {
                            SolutionOwnerService.url = "/api/SolutionOwner/DeleteSoft/";
                            SolutionOwnerService.delete(solutionOwner.Id, function (response) {
                                $.unblockUI();
                                if (response) {
                                    var index = vm.SolutionOwners.indexOf(solutionOwner);
                                    vm.SolutionOwners.splice(index, 1);
                                    toaster.success("", "Removed successfully.");
                                    SolutionOwnerService.url = "/api/SolutionOwner/";
                                }
                            });
                        }
                    });
                }
            }

            vm.filterData = function (isReset) {
                vm.Name = isReset ? '' : vm.Name;
                $('#dataTable').DataTable().draw();
            }
        }
    }
})();