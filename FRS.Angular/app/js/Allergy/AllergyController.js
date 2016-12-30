/**=========================================================
 * Module: Allergy
 * Allergy view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    //ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('AllergyController', AllergyController);

    AllergyController.$inject = ['$state', '$scope', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile', 'DTColumnDefBuilder'];

    function AllergyController($state, $scope, $rootScope, AllergyService, SweetAlert, toaster, DTOptionsBuilder, DTColumnBuilder, $compile, DTColumnDefBuilder) {
        var vm = this;
        vm.IsDataLoaded = false;
        AllergyService.url = "/api/Allergy/";
        vm.Title = '';

        activate();

        function activate() {
            //to empties previous function in search array 
            $.fn.dataTable.ext.search = [];
            //Custom Filter in datatable
            $.fn.dataTable.ext.search.push(
                function (settings, data, dataIndex) {
                    var name = data[0] || '';
                    name = name.replace(/\n/g, " ").replace(/<.*?>/g, "").toLowerCase();
                    var toSearch = vm.Title.replace(/\n/g, " ").replace(/<.*?>/g, "").toLowerCase();
                    var temp = name.indexOf(toSearch);
                    if (temp !== -1) {
                        return true;
                    }
                    return false;
                }
            );

            AllergyService.getAll(function (response) {
                if (response) {
                    vm.Allergies = response;
                    vm.IsDataLoaded = true;
                }
            });

            vm.dtOptions = DTOptionsBuilder.newOptions()
                            .withOption('bFilter', true)
                            .withOption("scrollX", true)
                            .withOption('aLengthMenu', [10, 25, 100, 500])
                            .withPaginationType('full_numbers');

            vm.dtColumns = [
                DTColumnDefBuilder.newColumnDef(0),
                DTColumnDefBuilder.newColumnDef(1),
                DTColumnDefBuilder.newColumnDef(2),
                DTColumnDefBuilder.newColumnDef(3),
                DTColumnDefBuilder.newColumnDef(4).notSortable()
            ];

            vm.dtInstance = {};

            vm.delete = function (item) {
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
                }, function (isConfirm) {
                    if (isConfirm) {
                        AllergyService.delete(item.AllergyId, function (response) {
                            if (response) {

                                var index = vm.Allergies.indexOf(item);
                                vm.Allergies.splice(index, 1);
                                toaster.success("", "Deleted successfully.");
                            }
                        });
                    }
                });
            }

            vm.fiterData = function (isReset) {
                vm.Title = isReset ? '' : vm.Title;
                $('#dataTable').DataTable().draw();
            }
        }
    }
})();