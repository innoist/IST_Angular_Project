//Users Controller


(function () {
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('UsersController', UsersController);

    UsersController.$inject = ['$scope', '$rootScope', '$localStorage', 'ReferenceDataService', 'SweetAlert', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile', 'toaster', '$state'];

    function UsersController($scope, $rootScope, $localStorage, UsersService, SweetAlert, DTOptionsBuilder, DTColumnBuilder, $compile, toaster, $state) {
        var vm = this;
        UsersService.url = '/api/Users';
        //vm.SystemAdmin = false;
        //vm.SysAdmin = true;

        //if ($localStorage['authorizationData'].UserRole === "Client") {
        //    vm.SysAdmin = false;
        //}

        //if ($localStorage['authorizationData'].UserRole === "SystemAdministrator") {
        //    vm.SystemAdmin = true;
        //}

        vm.dtOptions = DTOptionsBuilder.newOptions()
        .withOption('ajax', {
            headers: {
                //'Content-Type': 'application/x-www-form-urlencoded',
                'Authorization': 'Bearer ' + $localStorage['authorizationData'].token
            },
            data: function (params) {
                params = ajaxData(params);

                //other filter param here
                params.UserRole = $localStorage['authorizationData'].UserRole;
                params.Name = vm.name;
                params.UserName = vm.userName;
                params.Email = vm.email;

                //reset filter pareams
                if (vm.isReset) {
                    params.Name = vm.name = null;
                    params.UserName = vm.userName = null;
                    params.Email = vm.email = null;
                }
                $.blockUI({ message: '<div class="line-spin-fade-loader" style="left:50%; top:50%"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>' });
            },
            url: frsApiUrl + UsersService.url,
            type: 'GET',
            complete: function (data) {
                $.unblockUI();
            }
        })
        .withDataProp('Data')
        .withOption('processing', true)
        .withOption('serverSide', true)
        .withOption('scrollX', true)
        .withOption('bFilter', false)
        .withOption('createdRow', function createdRow(row, data, dataIndex) {
            // Recompiling so we can bind Angular directive to the DT
            $compile(angular.element(row).contents())($scope);
        })
        .withOption('aLengthMenu', [10, 25, 100, 500])
        .withPaginationType('full_numbers');

        vm.dtColumns = [
            DTColumnBuilder.newColumn(null).withTitle('Name').renderWith(function (data, type, full, meta) {
                return '<span>' + data.FirstName + ' ' + data.LastName + '</span>';
            }),
            DTColumnBuilder.newColumn('Email').withTitle('Email'),
            DTColumnBuilder.newColumn('UserName').withTitle('Username')
        ];

        vm.dtColumns.push(DTColumnBuilder.newColumn('Address').withTitle('Address'));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Telephone').withTitle('Phone').notSortable());
        vm.dtColumns.push(DTColumnBuilder.newColumn(null).withTitle('Actions').withClass('text-right').notSortable()
            .renderWith(function (data, type, full, meta) {
                return '<a ui-sref="app.Profile({userName : \'' + data.UserName + '\'})" uib-tooltip="{{\'View\'}}" uib-tooltip-trigger="focus" class="btn btn-xs btn-green"><i class="fa fa-search"></i></a>';
                //+ '&nbsp;&nbsp;<button type="button" ng-click="uc.delete(\'' + data.UserName + '\')" class="btn btn-xs btn-danger"><i class="fa fa-trash"></i></button>';
            }));



        //app.Profile({userName : ' + data.UserName + '})
        vm.dtInstance = {};

        vm.delete = function (item) {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'It cannot be undone!',
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#ee3d3d',
                confirmButtonText: 'Yes, Delete!',
                cancelButtonText: 'No!',
                closeOnConfirm: true,
                closeOnCancel: true,
            }, function (isConfirm) {
                if (isConfirm) {
                    $state.go('app.Allergy');
                    UsersService.delete(item.UserName, function (response) {
                        $.unblockUI();
                        if (response) {
                            vm.dtInstance.reloadData(function (json) { }, false);
                            toaster.success("", "Deleted successfully.");
                        }
                    });
                }
            });
        }

        vm.resetFilter = function () {
            vm.isReset = true;
            vm.dtInstance.reloadData(function (json) { }, true);
        }

        vm.fiterData = function () {
            vm.isReset = false;
            vm.dtInstance.reloadData(function (json) { }, true);
        }

        //Template for action buttons in dropdown template
        var actionBtnTemplate = '<div uib-dropdown="dropdown" class="btn-group">' +
            '<button type="button" class="btn btn-inverse">Action</button>' +
            '<button type="button" uib-dropdown-toggle="" class="btn dropdown-toggle btn-inverse">' +
            '</button>' +
            '<ul role="menu" class="dropdown-menu">' +
            '</li><li>' +
            '<a href="#">Another action</a>' +
            '</li><li><a href="#">Something else here</a>' +
            '</li>' +
            '<li class="divider"></li><li><a href="#">Separated link</a></li></ul></div>';
    }
})();
