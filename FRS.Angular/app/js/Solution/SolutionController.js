/**=========================================================
 * Module: Solution
 * Solution view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    //ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('SolutionController', SolutionController);

    SolutionController.$inject = ['$state', '$scope', '$localStorage', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile'];

    function SolutionController($state, $scope, $localStorage, $rootScope, SolutionService, SweetAlert, toaster, DTOptionsBuilder, DTColumnBuilder, $compile) {
        var vm = this;
        vm.IsDataLoaded = false;

        vm.SolutionTypes = [];
        vm.SolutionOwners = [];

        SolutionService.url = "/api/Solution/";
        //Datatable config
        vm.dtOptions = DTOptionsBuilder.newOptions()
        .withOption('ajax', {
            headers: {
                //'Content-Type': 'application/x-www-form-urlencoded',
                'Authorization': 'Bearer ' + $localStorage['authorizationData'].token
            },
            data: function (params) {
                params = ajaxData(params);
                params.SortBy = params.order[0].column;
                if (params.SortBy === 0) {
                    params.SortBy = 1;
                }
                //other filter param here
                if (vm.isReset) {
                    params.Name = vm.SolutionName = null;
                    params.TypeId = vm.SolutionTypes.selected = null;
                    params.OwnerId = vm.SolutionOwners.selected = null;
                } else {
                    params.Name = vm.SolutionName ? vm.SolutionName : null;
                    params.TypeId = vm.SolutionTypes.selected ? vm.SolutionTypes.selected.Id : null;
                    params.OwnerId = vm.SolutionOwners.selected ? vm.SolutionOwners.selected.Id : null;
                }
                $.blockUI({ message: '<div class="line-spin-fade-loader" style="left:50%; top:50%"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>' });
            },
            url: frsApiUrl + "/api/Solution/",
            type: 'GET',
            complete: function (data) {
                $.unblockUI();
                if (data.responseJSON.ExceptionMessage === '401') {
                    $state.go('account.401');
                }
            }
        })
        .withDataProp('Data')
        .withOption('processing', true)
        .withOption('serverSide', true)
        .withOption('bFilter', false)
        //.withOption('scrollX', true)
        .withOption('createdRow', function createdRow(row, data, dataIndex) {
            // Recompiling so we can bind Angular directive to the DT
            $compile(angular.element(row).contents())($scope);
        })
        .withOption('aLengthMenu', [10, 25, 100, 500])
        .withPaginationType('full_numbers');
        vm.dtColumns = [
            //DTColumnBuilder.newColumn('AttendanceId').withTitle('Id').notSortable().notVisible(),

            DTColumnBuilder.newColumn('Name').withTitle('Name'),
            DTColumnBuilder.newColumn('Type').withTitle('Type').notSortable(),
            DTColumnBuilder.newColumn('Owner').withTitle('Owner').notSortable(),
            DTColumnBuilder.newColumn('Active').withTitle('Active').renderWith(function (data, type, full, meta) {
                return "<span ng-class={'text-green':" + full.Active + ",'text-danger':" + !full.Active + "}><i class='fa' ng-class={'fa-check':" + full.Active + ",'fa-times':" + !full.Active + "}></i></span>";
            }).notSortable(),
            DTColumnBuilder.newColumn(null).withTitle('Actions').withClass('text-right').notSortable()
            .renderWith(function (data, type, full, meta) {
                return '<a ui-sref="app.CreateSolution({Id : \'' + data.Id + '\'})" uib-tooltip="{{\'View\'}}" class="btn btn-xs btn-green"><i class="fa fa-search"></i></a>&nbsp;&nbsp;&nbsp;' +
                       '<button ng-click="sc.delete(\'' + data.Id + '\')" uib-tooltip="{{\'Delete\'}}" uib-tooltip-trigger="focus" tooltip-class="tooltip-danger" class="btn btn-xs btn-danger"><i class="fa fa-trash"></i></button>' +
                       '</div>';
                       //'<div uib-dropdown="dropdown" class="btn-group">' +
                       //'    <a ng-click="sc.delete(' + false + ',' + data.Id + ')" uib-tooltip="{{\'Remove\'}}" tooltip-class="tooltip-danger" class="btn btn-xs btn-danger"><i class="fa fa-trash-o"></i></a>' +
                       //'    <button type="button" class="btn btn-danger btn-xs dropdown-toggle" uib-dropdown-toggle="">' +
                       //'        <span class="caret"></span>' +
                       //'        <span class="sr-only">Toggle Dropdown</span>' +
                       //'    </button>' +
                       //'    <ul role="menu" class="dropdown-menu animated flipInX left-63 min-width111">' +
                       //'        <li><a ng-click="sc.delete(' + true + ',' + data.Id + ')"><i class="icon-trash"></i>&nbsp;&nbsp;Delete</a></li>' +
                       //'    </ul>' +
                       //'</div>';
            })
        ];
        vm.dtInstance = {};

        SolutionService.getAll(function (response) {
            $.unblockUI();
            vm.SolutionTypes = response.SolutionTypes;
        }, null, "/api/SolutionBaseData");

        vm.delete = function (id) {
            //if (isCascade) {
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
                    SolutionService.url = "/api/Solution/DeleteCascade/";
                    SolutionService.delete(id, function (response) {
                        $.unblockUI();
                        if (response) {
                            vm.dtInstance.reloadData(function (json) { }, false);
                            toaster.success("", "Deleted successfully.");
                            SolutionService.url = "/api/Solution/";
                        }
                    });
                }
            });
            //} else {
            //    SweetAlert.swal({
            //        title: 'Are you sure? You want to remove this.',
            //        text: '',
            //        type: 'warning',
            //        showCancelButton: true,
            //        confirmButtonColor: '#ee3d3d',
            //        confirmButtonText: 'Yes, Remove!',
            //        cancelButtonText: 'No!',
            //        closeOnConfirm: true,
            //        closeOnCancel: true
            //    }, function (isConfirm) {
            //        if (isConfirm) {
            //            SolutionService.url = "/api/Solution/DeleteSoft/";
            //            SolutionService.delete(id, function (response) {
            //                $.unblockUI();
            //                if (response) {
            //                    vm.dtInstance.reloadData(function (json) { }, false);
            //                    toaster.success("", "Removed successfully.");
            //                    SolutionService.url = "/api/Solution/";
            //                }
            //            });
            //        }
            //    });
            //}
        }

        vm.filterData = function (toFilter) {
            vm.isReset = toFilter;
            vm.dtInstance.reloadData(function (json) { }, true);
        }

    }
})();