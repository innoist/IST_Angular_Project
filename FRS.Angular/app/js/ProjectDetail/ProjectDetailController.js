/**=========================================================
 * Module: Detail
 * Project Detail view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ProjectDetailController', ProjectDetailController);

    ProjectDetailController.$inject = ['$localStorage', '$rootScope', '$scope', '$http', '$stateParams', '$state', 'ReferenceDataService', 'SweetAlert', 'toaster', 'DTOptionsBuilder', 'DTColumnBuilder', '$location', 'DTColumnDefBuilder', '$anchorScroll'];

    function ProjectDetailController($localStorage, $rootScope, $scope, $http, $stateParams, $state, ProjectDetailService, SweetAlert, toaster, DTOptionsBuilder, DTColumnBuilder, $location, DTColumnDefBuilder, $anchorScroll) {
        var vm = this;
        vm.Projects = [];
        vm.ProjectSearchRequest = {
            PageSize: 9,
            PageNo: 1,
            IsAsc: true,
            OrderByColumn: 1
        }
        ProjectDetailService.url = "/api/Project/";

        ProjectDetailService.load(ProjectDetailService.url, vm.ProjectSearchRequest, function (response) {
            if (response) {
                vm.Projects = response.Data;
                console.log(vm.Projects);
                vm.TotalProjects = response.RecordsTotal;
            }
        });

        var projectId = $stateParams.Id;
        ProjectDetailService.loadById(projectId, function (response) {
            vm.Project = response;
        });
        if ($localStorage['authorizationData'] && $localStorage['authorizationData'].isAuthenticated) {
            $scope.isAuthenticated = true;
            $scope.userName = $localStorage['authorizationData'].userName;
        } else
            $scope.isAuthenticated = false;

        vm.showTab = function (id) {
            if (id === 'mydescription') {
                $('#myreviews').removeClass('in active');
                $('#mydescription')[0].className += ' in active';
            } else if (id === 'myreviews') {
                $('#mydescription').removeClass('in active');
                $('#myreviews')[0].className += ' in active';
            }
        }


        //Logout
        $scope.logout = function () {
            $http.post(window.frsApiUrl + "/api/Account/Logout")
                .success(function () {
                    $state.go('home.index', {}, { reload: true });
                    delete $localStorage['authorizationData'];
                    console.log("LoggedOut");
                    //$.connection.hub.stop();
                    $scope.isAuthenticated = false;
                    $http.defaults.headers.common = {
                        'Content-Type': 'application/json'
                    };

                })
                .error(function (err) {
                    showErrors(err);
                });
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
                    ProjectDetailService.delete(id, function (response) {
                        if (response) {
                            vm.dtInstance.reloadData(function (json) { }, false);
                            toaster.success("", "Deleted successfully.");
                        }
                    });
                }
            });
        }
    }
})();