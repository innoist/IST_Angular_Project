/**=========================================================
 * Module: Tag
 * Tag view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('TagController', TagController);

    TagController.$inject = ['$state', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster', 'DTOptionsBuilder', 'DTColumnBuilder', 'DTColumnDefBuilder'];

    function TagController($state, $rootScope, TagService, SweetAlert, toaster, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder) {
        var vm = this;
        vm.Name = '';
        vm.IsDataLoaded = false;
        TagService.url = "/api/Tag/";

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

            TagService.getAll(function (response) {
                $.unblockUI();
                if (response) {
                    vm.Tags = response;
                    vm.IsDataLoaded = true;
                }
            }, function (response) {
                $.unblockUI();
                if (response.ExceptionMessage === '401') {
                    $state.go('account.401');
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

            vm.delete = function (isCascade, tag) {
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
                            TagService.url = "/api/Tag/DeleteCascade/";
                            TagService.delete(tag.Id, function(response) {
                                $.unblockUI();
                                if (response) {
                                    var index = vm.Tags.indexOf(tag);
                                    vm.Tags.splice(index, 1);
                                    toaster.success("", "Deleted successfully.");
                                    TagService.url = "/api/Tag/";
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
                            TagService.url = "/api/Tag/DeleteSoft/";
                            TagService.delete(tag.Id, function (response) {
                                $.unblockUI();
                                if (response) {
                                    var index = vm.Tags.indexOf(tag);
                                    vm.Tags.splice(index, 1);
                                    toaster.success("", "Removed successfully.");
                                    TagService.url = "/api/Tag/";
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