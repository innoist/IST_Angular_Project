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

        HomeService.url = "/api/Project/";
        vm.CategoryId = 0;
        vm.Projects = [];
        //For first call to server
        vm.firstCall = true;
        //For no no more solutions
        vm.NoMoreProjects = false;
        //to show end of solutions
        vm.endOfSolutions = false;
        //to show spinner on scroll
        vm.clientSpinnerOnScroll = false;
        //to show spinner on data load
        vm.clientMainSpinner = true;
        vm.ProjectSearchRequest = {
            PageSize: 9,
            PageNo: 1,
            IsAsc: true,
            OrderByColumn: 1
        }
        var onSuccessLoadProjects = function (response) {
            vm.clientMainSpinner = true;
            if (vm.clicked || vm.searchString) {
                    vm.Projects = [];
                    angular.forEach(response.Data, function(project) {
                        project.isNew = true;
                        vm.Projects.push(project);
                        $.unblockUI();
                    });
                    vm.clicked = false;
                if (response.Data.length < vm.ProjectSearchRequest.PageSize) {
                    angular.forEach(vm.Projects, function(project) {
                        project.isNew = false;
                    });
                    vm.clientSpinnerOnScroll = false;
                    vm.NoMoreProjects = true;
                    vm.endOfSolutions = true;
                    $.unblockUI();
                } else {
                    vm.NoMoreProjects = false;
                    vm.endOfSolutions = false;
                    $.unblockUI();
                }
            } else {
                if (vm.firstCall) {
                    vm.FilterCategories = response.FilterCategories;
                }
                //vm.FilterCategories = response.FilterCategories;
                angular.forEach(vm.Projects, function (project) {
                    project.isNew = false;
                });
                //To Factorize
                angular.forEach(response.Data, function (project) {
                    project.isNew = true;
                    vm.Projects.push(project);
                    $.unblockUI();
                });
                vm.clientSpinnerOnScroll = false;
                if (response.Data.length < vm.ProjectSearchRequest.PageSize) {
                    angular.forEach(vm.Projects, function(project) {
                        project.isNew = false;
                    });
                    vm.clientSpinnerOnScroll = false;
                    $.unblockUI();
                    vm.NoMoreProjects = true;
                    vm.endOfSolutions = true;
                } else {
                    $.unblockUI();
                    vm.NoMoreProjects = false;
                    vm.endOfSolutions = false;
                }
                vm.IsDataLoaded = false;
            }
        }

        vm.getDataFromSever = function () {
            if (vm.clientMainSpinner) {
                $.blockUI({ message: '<img src="/Ecommerce/img/Spinner/balls.gif" />' });
            }
            vm.clientMainSpinner = true;
            vm.ProjectSearchRequest.Name = vm.searchString ? vm.searchString : null;
            HomeService.load(HomeService.url, vm.ProjectSearchRequest, onSuccessLoadProjects);
        }

        vm.ProjectSearchRequest.FilterIds = [];
        vm.filterProjects = function (id) {
            vm.clientMainSpinner = true;
            vm.firstCall = false;
            if ($('#' + id)[0].checked) {
                vm.ProjectSearchRequest.FilterIds.push(id);
                vm.ProjectSearchRequest.PageNo = 1;
                vm.getDataFromSever();
            } else {
                vm.ProjectSearchRequest.FilterIds.splice(vm.ProjectSearchRequest.FilterIds.indexOf(id), 1);
                vm.ProjectSearchRequest.PageNo = 1;
                vm.getDataFromSever();
            }
        }

        vm.getDataFromSever();

        vm.IsDataLoaded = false;
        $(window).scroll(function () {
            vm.clientMainSpinner = false;
            if (vm.IsDataLoaded)
                return false;
            if (!vm.NoMoreProjects) {
                if ($(window).scrollTop() + $(window).height() === $(document).height()) {
                    vm.IsDataLoaded = true;
                    vm.clientSpinnerOnScroll = true;
                    vm.ProjectSearchRequest.PageNo += 1;
                    vm.getDataFromSever();
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
                vm.Project = response.SolutionModel;
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