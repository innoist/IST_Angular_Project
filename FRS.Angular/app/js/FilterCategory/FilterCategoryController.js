/**=========================================================
 * Module: FilterCategory
 * FilterCategory view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('FilterCategoryController', FilterCategoryController);

    FilterCategoryController.$inject = ['$state', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster', 'DTOptionsBuilder', 'DTColumnBuilder', 'DTColumnDefBuilder'];

    function FilterCategoryController($state, $rootScope, FilterCategoryService, SweetAlert, toaster, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder) {
        var vm = this;
        vm.Name = '';
        vm.IsDataLoaded = false;
        FilterCategoryService.url = "/api/FilterCategory/";

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

            FilterCategoryService.getAll(function (response) {
                $.unblockUI();
                if (response) {
                    vm.FilterCategories = response;
                    vm.IsDataLoaded = true;
                }
            });

            vm.dtOptions = DTOptionsBuilder.newOptions()
                            .withOption('bFilter', true)
                            .withOption("scrollX", true)
                            .withOption('aLengthMenu', [10, 25, 100, 500])
                            .withPaginationType('full_numbers');

            vm.dtColumns = [
                //DTColumnDefBuilder.newColumnDef(0).notVisible(),
                DTColumnDefBuilder.newColumnDef(1),
                DTColumnDefBuilder.newColumnDef(2).notSortable()
            ];

            vm.dtInstance = {};

            vm.delete = function (isCascade, filtercategory) {
                if (isCascade) {
                    //SweetAlert.swal({
                    //    title: 'Are you sure, you want to delete this?',
                    //    text: 'It can\'t recover!',
                    //    type: 'warning',
                    //    showCancelButton: true,
                    //    confirmButtonColor: '#ee3d3d',
                    //    confirmButtonText: 'Yes, Delete!',
                    //    cancelButtonText: 'No!',
                    //    closeOnConfirm: true,
                    //    closeOnCancel: true
                    //}, function (isConfirm) {
                    //    if (isConfirm) {
                    //        FilterCategoryService.url = "/api/FilterCategory/DeleteCascade/";
                    //        FilterCategoryService.delete(filtercategory.Id, function (response) {
                    //            $.unblockUI();
                    //            if (response) {
                    //                var index = vm.FilterCategories.indexOf(filtercategory);
                    //                vm.FilterCategories.splice(index, 1);
                    //                toaster.success("", "Deleted successfully.");
                    //                FilterCategoryService.url = "/api/FilterCategory/";
                    //            }
                    //        });
                    //    }
                    //});
                } else {
                    SweetAlert.swal({
                        title: 'Are you sure, you want to remove this?',
                        text: '',
                        type: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#ee3d3d',
                        confirmButtonText: 'Yes, Delete!',
                        cancelButtonText: 'No!',
                        closeOnConfirm: true,
                        closeOnCancel: true
                    }, function(isConfirm) {
                        if (isConfirm) {
                            FilterCategoryService.url = "/api/FilterCategory/DeleteSoft/";
                            FilterCategoryService.delete(filtercategory.Id, function (response) {
                                $.unblockUI();
                                if (response) {
                                    var index = vm.FilterCategories.indexOf(filtercategory);
                                    vm.FilterCategories.splice(index, 1);
                                    toaster.success("", "Removed successfully.");
                                    FilterCategoryService.url = "/api/FilterCategory/";
                                }
                            });
                        }
                    });
                }
            }

            vm.filterData = function (isReset) {
                vm.Name = isReset ? '' : vm.Name;
                $('#filterCategoryDataTable').DataTable().draw();
            }
        }
    }
})();