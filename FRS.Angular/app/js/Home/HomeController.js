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
            OrderByColumn: 1
        }

        HomeService.load(HomeService.url, vm.ProjectSearchRequest, function (response) {
            if (response) {
                vm.Projects = response.Data;
                vm.TotalProjects = response.RecordsTotal;
            }
        });

        var onSuccessLoadProject = function (response) {
            angular.forEach(vm.Projects, function (project) {
                project.isNew = false;
            });
            angular.forEach(response.Data, function (project) {
                project.isNew = true;
                vm.Projects.push(project);
            });
            if (response.Data.length < vm.ProjectSearchRequest.PageSize) {
                angular.forEach(vm.Projects, function (project) {
                    project.isNew = false;
                });
                vm.NoMoreProjects = true;
            }

            else
                vm.NoMoreProjects = false;
            vm.IsDataLoaded = false;
        }

        vm.IsDataLoaded = false;
        $(window).scroll(function () {
            if ($(window).scrollTop() > 100) {
                $(".header-v5.header-static").addClass("header-fixed-shrink");
            }
            else {
                $(".header-v5.header-static").removeClass("header-fixed-shrink");
            }
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

        vm.OpenGPopUp = function () {
            $('#sendtofriend').show();
            //if ($('.g-popup-wrapper').is(':visible')) 
            //    $('div.wrapper').addClass('g-blur');
        }
        $('.g-popup__close').on('click', function (e) {
            $('.g-popup-wrapper').hide();
            $('body').removeClass('g-blur');
        });

        vm.OpenProjectDetail = function (id) {
            $('#projectdetail').show();
            HomeService.loadById(id, function (response) {
                vm.Project = response;
            });
        }

        vm.OpenLoginPage = function () {
            $('#loginpage').show();
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