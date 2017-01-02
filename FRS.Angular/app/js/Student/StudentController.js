/**=========================================================
 * Module: Student
 * Student view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('StudentController', StudentController);

    StudentController.$inject = ['$localStorage', '$rootScope', '$scope', '$state', 'ReferenceDataService', 'SweetAlert', 'toaster', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile', 'DTColumnDefBuilder'];

    function StudentController($localStorage, $rootScope, $scope, $state, StudentService, SweetAlert, toaster, DTOptionsBuilder, DTColumnBuilder, $compile, DTColumnDefBuilder) {
        var vm = this;

        vm.isReset = true;
        StudentService.url = "/api/Student/";
        

        //datepicker config start
        vm.today = function () {
            vm.dt = new Date();
        };
        //vm.today();

        vm.clear = function () {
            vm.dt = null;
        };

        vm.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.opened = true;
        };

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        vm.initDate = new Date();
        vm.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd-MM-yyyy', 'shortDate'];
        vm.format = vm.formats[2];
        //Datepicker config end


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
                    params.Name = vm.Name = null;
                    params.DateOfBirth = vm.dt = null;
                } else {
                    params.Name = vm.Name;
                    params.DateOfBirth = vm.dt != null ? vm.dt.toJSON() : '';
                }
            },
            url: frsApiUrl + "/api/Student/",
            type: 'GET',
            complete: function (data) {
            }
        })
        .withDataProp('data')
        .withOption('processing', true)
        .withOption('serverSide', true)
        .withOption('bFilter', false)
        .withOption('createdRow', function createdRow(row, data, dataIndex) {
            // Recompiling so we can bind Angular directive to the DT
                if (data.Anaphylactic) {
                    $compile(angular.element(row).contents())($scope);
                    row.setAttribute('class', 'rowcolor');
                } else {
                    $compile(angular.element(row).contents())($scope);
                }
            })
        .withOption('aLengthMenu', [10, 25, 100, 500])
        .withPaginationType('full_numbers');

        vm.dtColumns = [
            //DTColumnBuilder.newColumn('StudentId').withTitle('Id').notSortable().notVisible(),
            DTColumnBuilder.newColumn(null).withTitle('Image').notSortable().renderWith(function(data, type, full, meta) {
                return '<div class=\"media\">' +
                    '<img class=\"img img-circle img-thumbnail img-responsive img-datatable\" src=\"' + (data.Image ? data.Image : '/app/img/user/default.png') + '\"/>' +
                    '</div>';
            }),
            DTColumnBuilder.newColumn(null).withTitle('Name').withClass('colwidth').renderWith(function (data, type, full, meta) {
                var fullname = data.FirstName + ' ' + (data.MiddleName || "") + ' ' + data.LastName;
                var stuname = fullname.length > 20 ? fullname.substring(0, 19) + '...' : fullname;
                return '<span title="' + fullname + '">' + stuname + '</span>';

            }),
            DTColumnBuilder.newColumn('Age').withTitle('Age'),
            DTColumnBuilder.newColumn(null).withTitle("Guardian's Name").withClass('colwidth').renderWith(function (data, type, full, meta) {
                var guardianname = data.GuardianFirstName + ' ' + data.GuardianLastName;
                var shortname = guardianname.length > 20 ? guardianname.substring(0, 19) + '...' : guardianname;
                return '<span title="' + guardianname + '">' + shortname + '</span>';
            }),
            DTColumnBuilder.newColumn('GuardianPhone').withTitle('Guardian\'s Phone'),
            
            DTColumnBuilder.newColumn(null).withTitle('Actions').withClass('text-right').notSortable()
            .renderWith(function (data, type, full, meta) {
                return '<div uib-dropdown="dropdown" class="btn-group">' +
                    '       <a ui-sref="app.CreateStudent({Id : ' + data.StudentId + '})" uib-tooltip="{{\'Basic Info\'}}" uib-tooltip-trigger="focus" uib-tooltip-placement="bottom" type="button" class="btn btn-xs btn-info"><i class="fa fa-search"></i></a>' +
                    '       <button type="button" class="btn btn-info btn-xs dropdown-toggle" uib-dropdown-toggle="">' +
                    '          <span class="caret"></span>' +
                    '          <span class="sr-only">Toggle Dropdown</span>' + 
                    '       </button>' +
                    '          <ul role="menu" class="dropdown-menu animated flipInX custom_position">' +
                    '              <li><a ui-sref="app.StudentSchedule({Id : '+ data.StudentId +'})"><i class="icon-clock"></i>&nbsp;&nbsp;Schedule</a></li>' +
                    '              <li><a ui-sref="app.StudentAddons({Id : ' + data.StudentId + '})"><i class="icon-layers"></i>&nbsp;&nbsp;Requirements</a></li>' +
                    '              <li><a ui-sref="app.StudentAllergy({Id : ' + data.StudentId + '})"><i class="icon-settings"></i>&nbsp;&nbsp;Allergies</a></li>' +
                    '          </ul>' + 
                    '   </div>' + 

                    '&nbsp;&nbsp;&nbsp;<button"button" uib-tooltip="{{\'Remove\'}}" uib-tooltip-trigger="focus" tooltip-class="tooltip-danger" ng-click="sc.delete(' + data.StudentId + ')" class="btn btn-xs btn-danger"><i class="fa fa-trash"></i></button>'
                    ;
            })
        ];
        vm.dtInstance = {};

        vm.fiterData = function (toFilter) {
            vm.isReset = toFilter;
            vm.dtInstance.reloadData(function (json) {  }, true);
        }

        vm.delete = function (id) {
            SweetAlert.swal({
                title: 'Are you sure?',
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
                    StudentService.delete(id, function (response) {
                        if (response) {
                            vm.dtInstance.reloadData(function (json) {  }, false);
                            toaster.success("", "Deleted successfully.");
                        }
                    });
                }
            });
        }
    }
})();