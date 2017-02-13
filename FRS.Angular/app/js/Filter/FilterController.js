/**=========================================================
 * Module: Filter
 * Filter view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('FilterController', FilterController);

    FilterController.$inject = ['$state', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster', 'DTOptionsBuilder', 'DTColumnBuilder', 'DTColumnDefBuilder'];

    function FilterController($state, $rootScope, FilterService, SweetAlert, toaster, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder) {
        var vm = this;
        vm.Name = '';
        vm.IsDataLoaded = false;
        FilterService.url = "/api/Filter/";

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

            FilterService.getAll(function (response) {
                $.unblockUI();
                if (response) {
                    vm.Filters = response;
                    vm.IsDataLoaded = true;
                }
            });

            vm.dtOptions = DTOptionsBuilder.newOptions()
                            .withOption('bFilter', true)
                            //.withOption("scrollX", true)
                            .withOption('aLengthMenu', [10, 25, 100, 500])
                            .withPaginationType('full_numbers');

            vm.dtColumns = [
                //DTColumnDefBuilder.newColumnDef(0).notVisible(),
                DTColumnDefBuilder.newColumnDef(1),
                DTColumnDefBuilder.newColumnDef(2).notSortable(),
                DTColumnDefBuilder.newColumnDef(3).notSortable()
            ];

            vm.dtInstance = {};

            vm.delete = function (deletetype, filter) {
                SweetAlert.swal({
                    title: 'Are you sure, you want to delete this?',
                    text: 'It can\'t recover!',
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#ee3d3d',
                    confirmButtonText: 'Yes, Delete!',
                    cancelButtonText: 'No!',
                    closeOnConfirm: true,
                    closeOnCancel: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        var deleteModel = { Id: filter.Id, DeleteType: deletetype }
                        FilterService.save(deleteModel, onSuccess, onError, '/api/Filter/PostDelete/');
                        function onSuccess(response) {
                            $.unblockUI();
                            if (response) {
                                var index = vm.Filters.indexOf(filter);
                                vm.Filters.splice(index, 1);
                                toaster.success("", "Deleted successfully.");
                            }
                        }
                        function onError(err) {
                            $.unblockUI();
                            toaster.error(err.statusText, err.data.Message);
                            showErrors(err);
                        }
                    }
                });
            }

            vm.filterData = function (isReset) {
                vm.Name = isReset ? '' : vm.Name;
                $('#dataTable').DataTable().draw();
            }
        }
    }
})();