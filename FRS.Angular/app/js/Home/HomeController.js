/**=========================================================
 * Module: Home
 * Home view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('HomeController', HomeController);

    HomeController.$inject = ['$localStorage', '$rootScope', '$scope', '$http', '$state', 'ReferenceDataService', '$window', 'SweetAlert', 'toaster', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile', 'DTColumnDefBuilder'];

    function HomeController($localStorage, $rootScope, $scope, $http, $state, HomeService, $window, SweetAlert, toaster, DTOptionsBuilder, DTColumnBuilder, $compile, DTColumnDefBuilder) {
        var vm = this;

        HomeService.url = "/api/Project/";
        vm.apiUrl = frsApiUrl;
        vm.CategoryId = 0;
        vm.Favorites = false;
        vm.Projects = [];
        //For first call to server
        vm.firstCall = true;
        vm.isScrolled = false;
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
            OrderByColumn: 1,
            ClientRequest: HomeService.url
        }

        var onSuccessLoadProjects = function (response) {
            vm.clientMainSpinner = true;
            if (!vm.isScrolled) {
                if (vm.firstCall) {
                    vm.FilterCategories = response.FilterCategories;
                }
                vm.Projects = [];
                angular.forEach(response.Data, function (project) {
                    project.isNew = true;
                    vm.Projects.push(project);
                    $.unblockUI();
                });
                vm.clicked = false;
                if (response.Data.length < vm.ProjectSearchRequest.PageSize) {
                    angular.forEach(vm.Projects, function (project) {
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
                    angular.forEach(vm.Projects, function (project) {
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
                vm.isScrolled = false;
            }
        }

        vm.getDataFromSever = function () {
            if (vm.clientMainSpinner) {
                $.blockUI({ message: '<img src="/Ecommerce/img/Spinner/balls.gif" />' });
            }
            vm.clientMainSpinner = true;
            vm.ProjectSearchRequest.Name = vm.searchString ? vm.searchString.DisplayName : null;
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
                    vm.isScrolled = true;
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

        //#region Post Data
        vm.Solution = {};
        vm.save = function (solutionId) {
            vm.submitted = true;
            var solution = {};
            vm.Solution.Id = solutionId;
            angular.forEach(vm.Projects, function (project) {
                if (project.Id === solutionId) {
                    solution = project;
                }
            });
            vm.Solution.IsFavorite = solution.IsFavorite;
            HomeService.save(vm.Solution, onSuccess, onError);
            function onSuccess(response) {
                if (response.data === true) {
                    vm.tooltip = 'Added';
                    $('#' + solutionId).addClass("rating-selected");
                    $('#' + solutionId).removeClass("rating");
                    angular.forEach(vm.Projects, function (project) {
                        if (project.Id === solutionId) {
                            project.IsFavorite = true;
                        }
                    });
                    vm.saved = true;
                } else {

                    $('#' + solutionId).addClass("rating");
                    $('#' + solutionId).removeClass("rating-selected");
                    vm.saved = false;
                    angular.forEach(vm.Projects, function (project) {
                        if (project.Id === solutionId) {
                            project.IsFavorite = false;
                        }
                    });
                }
            }
            function onError(err) {
                toaster.error(err.statusText, err.data.Message);
                showErrors(err);
            }
        }
        //#endregion


        /************************************/
        /************* Typeahead ************/
        /************************************/
        vm.getSolutions = function (val) {
            var url = "/api/ProjectBaseData";
            //Check if input is more that 1 char and less than 10
            if (val.length >= 3 && val.length < 15)
                return HomeService.retrieveItems(url, val)
                    .then(function (res) {
                        //local array to store items from server response
                        var items = [];
                        angular.forEach(res.data, function (item) {
                            items.push(item);
                        });
                        return items;
                    });
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

        vm.resetdata = function () {
            vm.searchString = '';
            vm.getDataFromSever();
        }

        vm.resetCategories = function () {
            var filtersarr = vm.ProjectSearchRequest.FilterIds;
            angular.forEach(filtersarr, function (id) {
                $('#' + id)[0].checked = $('#' + id)[0].checked ? false : true;
            });
            vm.ProjectSearchRequest.FilterIds = [];
            vm.getDataFromSever();
        }

        //to unbind the scroll event when scope destroy
        $scope.$on("$destroy", function () {
            $(window).unbind('scroll');
        });


        vm.seeFavorites = function () {
            if (vm.Favorites) {
                vm.clientMainSpinner = true;
                vm.ProjectSearchRequest.IsFavorite = true;
                vm.ProjectSearchRequest.PageNo = 1;
                vm.getDataFromSever();
            } else {
                vm.ProjectSearchRequest.IsFavorite = false;
                vm.getDataFromSever();
            }
        };

        $(document).on('click', '[data-toggle="lightbox"]', function (event) {
            event.preventDefault();
            $(this).ekkoLightbox();
        });

        vm.zoomImage = function (image) {
            var img = image ? frsApiUrl + image : "/Ecommerce/img/blog/project.png";
            SweetAlert.swal({
                title: "",
                text: '<img src="' + img + '">',
                //imageUrl: frsApiUrl + image,
                html: true
            });
        }
    }
})();