/**=========================================================
 * Module: Home
 * Home view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('HomeController', HomeController);

    HomeController.$inject = ['$localStorage', '$rootScope', '$scope', '$http', '$state', 'ReferenceDataService', 'SweetAlert', 'toaster', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile', 'DTColumnDefBuilder'];

    function HomeController($localStorage, $rootScope, $scope, $http, $state, HomeService, SweetAlert, toaster, DTOptionsBuilder, DTColumnBuilder, $compile, DTColumnDefBuilder) {
        var vm = this;

        vm.isReset = true;
        HomeService.url = "/api/Project/";

        vm.ProjectSearchRequest = {
            PageSize: 9,
            PageNo: 1,
            IsAsc: true,
            OrderByColumn: 1,
            ProjectIds: []
        }

        HomeService.load(HomeService.url, vm.ProjectSearchRequest, function (response) {
            if (response) {
                vm.Projects = response.Data;
                
            }
        });

        var onSuccessLoadProject = function (response) {
            angular.forEach(response.Data, function (project) {
                vm.Projects.push(project);
            });
            if (response.Data.length < vm.ProjectSearchRequest.PageSize)
                vm.NoMoreProjects = true;
            else
                vm.NoMoreRecipes = false;
            vm.IsDataLoaded = false;
        }
        vm.IsDataLoaded = false;
        $(window).scroll(function () {
            if (vm.IsDataLoaded)
                return false;
            if (!vm.NoMoreProjects) {
                if ($(window).scrollTop() >= ($(document).height() - $(window).height()) * 0.6) {
                    vm.IsDataLoaded = true;
                    vm.ProjectSearchRequest.PageNo += 1;
                    HomeService.load(HomeService.url, vm.ProjectSearchRequest, onSuccessLoadProject);
                }
            }
        });
        if ($localStorage['authorizationData'] && $localStorage['authorizationData'].isAuthenticated) {
            $scope.isAuthenticated = true;
            $scope.userName = $localStorage['authorizationData'].userName;
        } else
            $scope.isAuthenticated = false;

        //Logout
        $scope.logout = function () {
            $http.post(window.frsApiUrl + "/api/Account/Logout")
                .success(function () {
                    delete $localStorage['authorizationData'];
                    console.log("LoggedOut");
                    //$.connection.hub.stop();
                    $scope.isAuthenticated = false;
                    $http.defaults.headers.common = {
                        'Content-Type': 'application/json'
                    };
                    $state.go('home.index');
                })
                .error(function (err) {
                    showErrors(err);
                });
        }


        vm.fiterData = function (toFilter) {
            vm.isReset = toFilter;
            vm.dtInstance.reloadData(function (json) { }, true);
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
                    HomeService.delete(id, function (response) {
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