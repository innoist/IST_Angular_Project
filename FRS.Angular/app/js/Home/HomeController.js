/**=========================================================
 * Module: Home
 * Home view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('HomeController', HomeController);

    HomeController.$inject = ['$localStorage', '$rootScope', '$scope', '$http', '$state', 'ReferenceDataService', '$window', 'SweetAlert', 'toaster', '$timeout'];

    function HomeController($localStorage, $rootScope, $scope, $http, $state, HomeService, $window, SweetAlert, toaster, $timeout) {

        if ($localStorage['authorizationData'] && $localStorage['authorizationData'].isAuthenticated) {
            $scope.isAuthenticated = true;
            $scope.userName = $localStorage['authorizationData'].userName;
        } else
            $scope.isAuthenticated = false;

        var vm = this;

        HomeService.url = "/api/Project/";
        vm.apiUrl = frsApiUrl;

        vm.RatingId = 0;

        vm.showReply = false;

        vm.CategoryId = 0;
        //To show hide favorite solutions
        vm.Favorites = false;

        vm.Projects = [];
        //For first call to server
        vm.firstCall = true;
        //to check if user scroll or not
        vm.isScrolled = false;
        //For no no more solutions
        vm.NoMoreProjects = false;
        //to show end of solutions
        vm.endOfSolutions = false;
        //To store current project location
        vm.ProjectLocation = '';
        vm.SolutionRatingModel = {};

        vm.ProjectSearchRequest = {
            PageSize: 9,
            PageNo: 1,
            IsAsc: true,
            OrderByColumn: 1,
            ClientRequest: HomeService.url
        }

        var selectedStars = function (project) {
            project.RoundRating = Math.round(project.AverageRating);
            var j = 1;
            var rating = project.AverageRating;
            while (j <= project.RoundRating) {
                if (rating < 1 && rating % 1 !== 0) {
                    $('#avg-rating-' + j + '-' + project.Id).removeClass("fa-star-o").addClass("fa-star-half-o");
                } else {
                    $('#avg-rating-' + j + '-' + project.Id).removeClass("fa-star-o").addClass("fa-star");
                }
                rating = rating - 1;
                j++;
            }
        }

        var onSuccessLoadProjects = function (response) {
            
            if (!vm.isScrolled) {
                if (vm.firstCall) {
                    vm.FilterCategories = response.FilterCategories;
                }
                vm.Projects = [];
                angular.forEach(response.Data, function (project) {
                    project.isNew = true;

                    $timeout(function () {
                        selectedStars(project);
                    });

                    vm.Projects.push(project);
                    $.unblockUI();
                });
                vm.clicked = false;
                if (response.Data.length < vm.ProjectSearchRequest.PageSize) {
                    angular.forEach(vm.Projects, function (project) {
                        project.isNew = false;
                    });
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
                angular.forEach(vm.Projects, function (project) {
                    project.isNew = false;
                });
                //To Factorize
                angular.forEach(response.Data, function (project) {
                    project.isNew = true;
                    $timeout(function () {
                        selectedStars(project);
                    });
                    vm.Projects.push(project);
                    $.unblockUI();
                });
                if (response.Data.length < vm.ProjectSearchRequest.PageSize) {
                    angular.forEach(vm.Projects, function (project) {
                        project.isNew = false;
                    });
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

        vm.getDataFromServer = function () {
            
            HomeService.load(HomeService.url, vm.ProjectSearchRequest, onSuccessLoadProjects);
        }

        vm.getDataFromServer();

        //search solutions on the basis of filters
        vm.ProjectSearchRequest.FilterIds = [];
        vm.filterProjects = function (id) {
            
            vm.firstCall = false;
            if ($('#' + id)[0].checked) {
                vm.ProjectSearchRequest.FilterIds.push(id);
                vm.ProjectSearchRequest.PageNo = 1;
                vm.getDataFromServer();
            } else {
                vm.ProjectSearchRequest.FilterIds.splice(vm.ProjectSearchRequest.FilterIds.indexOf(id), 1);
                vm.ProjectSearchRequest.PageNo = 1;
                vm.getDataFromServer();
            }
        }

        vm.seeFavorites = function () {
            if (vm.Favorites) {
                
                vm.ProjectSearchRequest.IsFavorite = true;
                vm.ProjectSearchRequest.PageNo = 1;
                vm.getDataFromServer();
            } else {
                vm.ProjectSearchRequest.IsFavorite = false;
                vm.ProjectSearchRequest.PageNo = 1;
                vm.getDataFromServer();
            }
        };

        vm.searchSolutions = function () {
            if (vm.searchString) {
                vm.ProjectSearchRequest.Name = typeof vm.searchString === "object" ? vm.searchString.DisplayName : vm.searchString;
                vm.ProjectSearchRequest.PageNo = 1;
                vm.getDataFromServer();
            }
        }

        /************************************/
        /************* Typeahead ************/
        /************************************/
        vm.getSolutions = function (val) {
            var url = "/api/ProjectBaseData";

            //Check if input is more that 1 char and less than 10
            if (val.length >= 3 && val.length < 15)
                return HomeService.retrieveItems(url, val, vm.ProjectSearchRequest.FilterIds)
                    .then(function (res) {
                        //local array to store items from server response
                        var items = [];
                        angular.forEach(res.data, function (item) {
                            items.push(item);
                        });
                        return items;
                    });
        }

        vm.IsDataLoaded = false;
        $(window).scroll(function () {
            
            if (vm.IsDataLoaded)
                return false;
            if (!vm.NoMoreProjects) {
                if ($(window).scrollTop() + $(window).height() === $(document).height()) {
                    vm.IsDataLoaded = true;
                    vm.ProjectSearchRequest.PageNo += 1;
                    vm.getDataFromServer();
                    vm.isScrolled = true;
                }
            }
        });

        vm.OpenSendToFriend = function (location) {
            $('#sendtofriend').show();
            angular.element('#client-wrapper').toggleClass('position-fixed');
            vm.username = $localStorage.authorizationData.userName;
            vm.ProjectLocation = location;
        }

        vm.OpenProjectDetail = function (id) {
            $('#projectdetail').show();
            angular.element('#client-wrapper').toggleClass('position-fixed');
            HomeService.loadById(id, function (response) {
                $.unblockUI();
                vm.Project = response.SolutionModel;
                vm.SolutionRatings = response.SolutionRatings;

                $timeout(function () {
                    angular.forEach(vm.SolutionRatings, function (obj) {

                        for (var j = 1; j <= obj.Rating; j++) {
                            $('#rated-' + j + '-' + obj.RatingId).removeClass('fa-star-o').addClass('fa-star');
                        }
                        var createddate = new Date(obj.RecCreatedOn);
                        if (!angular.isDate(createddate)) {
                            obj.RecCreatedOn = null;
                            return;
                        }
                        var today = new Date();
                        var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
                        var finaldate = Math.round(Math.abs((createddate.getTime() - today.getTime()) / (oneDay)));
                        if (finaldate < 31) {
                            obj.reviewdate = finaldate === 1 ? finaldate.toString() + ' day ago' : finaldate.toString() + ' days ago';
                        }
                        else if (finaldate < 365) {
                            var temp = 0;
                            for (var i = 30; i < finaldate; i = i + 30) {
                                temp += 1;
                            }
                            obj.reviewdate = temp === 1 ? temp.toString() + ' month ago' : temp.toString() + ' months ago';
                        } else {
                            obj.reviewdate = 'more than one year ago';
                        }
                    });
                });

                vm.username = $localStorage.authorizationData.userName;
                vm.email = $localStorage.authorizationData.email;
                vm.disableprojectdetail = true;
            });
        }

        vm.OpenLoginPage = function () {
            $('#loginpage').show();
            angular.element('#client-wrapper').toggleClass('position-fixed');
        }

        $('.g-popup__close').on('click', function (e) {
            $('.g-popup-wrapper').hide();
            angular.element('#client-wrapper').toggleClass('position-fixed');
            vm.Comments = '';
            for (var i = 1; i < 6; i++) {
                if ($('#stars-rating-' + i).is(':checked')) {
                    $('#stars-rating-' + i)[0].checked = false;
                }
            }
            $('body').removeClass('g-blur');
        });

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
                $.unblockUI();
                if (response.data === true) {
                    $('#' + solutionId).addClass("rating-selected");
                    $('#' + solutionId).removeClass("rating");
                    angular.forEach(vm.Projects, function (project) {
                        if (project.Id === solutionId) {
                            project.IsFavorite = true;
                        }
                    });
                    //toaster.success("Notification", "Solution has been added successfully.");
                    vm.saved = true;
                } else {

                    $('#' + solutionId).addClass("rating");
                    $('#' + solutionId).removeClass("rating-selected");

                    angular.forEach(vm.Projects, function (project) {
                        if (project.Id === solutionId) {
                            project.IsFavorite = false;
                        }
                    });
                    //toaster.error("Notification", "Solution has been removed successfully.");
                    vm.saved = false;
                }
            }
            function onError(err) {
                toaster.error(err.statusText, err.data.Message);
                showErrors(err);
            }
        }
        //#endregion

        vm.getRatingId=function(ratingid) {
            vm.RatingId = ratingid;
            vm.showReply = true;
        }

        vm.SaveSolutionRating = function (projectid, isreply) {
            angular.element('#client-wrapper').toggleClass('position-fixed');
            if (isreply) {
                vm.SolutionRatingModel.IsReply = true;
                vm.SolutionRatingModel.ReplyParentId = vm.RatingId;

            } else {
                vm.SolutionRatingModel.IsReply = false;
            }
            vm.SolutionRatingModel.SolutionId = projectid;
            vm.SolutionRatingModel.Comments = vm.Comments;
            for (var i = 1; i < 6; i++) {
                if ($('#stars-rating-' + i).is(':checked')) {
                    vm.SolutionRatingModel.Rating = i;
                }
            }
            HomeService.save(vm.SolutionRatingModel, onSuccess, null, '/api/ProjectBaseData');
            function onSuccess(response) {
                $.unblockUI();
                vm.showReply = false;
                $('.g-popup-wrapper').hide();
                vm.Comments = '';
                for (var i = 1; i < 6; i++) {
                    if ($('#stars-rating-' + i).is(':checked')) {
                        $('#stars-rating-' + i)[0].checked = false;
                    }
                }
            }
        }

        $(document).on('click', '[data-toggle="lightbox"]', function (event) {
            event.preventDefault();
            $(this).ekkoLightbox();
        });

        vm.resetdata = function () {
            if (vm.searchString) {
                vm.searchString = '';
                vm.ProjectSearchRequest.Name = '';
                vm.ProjectSearchRequest.PageNo = 1;
                vm.getDataFromServer();
            }
        }

        vm.resetCategories = function () {
            var filtersarr = vm.ProjectSearchRequest.FilterIds;
            angular.forEach(filtersarr, function (id) {
                $('#' + id)[0].checked = $('#' + id)[0].checked ? false : true;
            });
            vm.ProjectSearchRequest.FilterIds = [];
            vm.ProjectSearchRequest.PageNo = 1;
            vm.getDataFromServer();
        }

        vm.signOut = function () {
            HomeService.logout().then(function (response) {
                console.log(response);
                $scope.isAuthenticated = false;
                $.unblockUI();
            });
        }

        //to unbind the scroll event when scope destroy
        $scope.$on("$destroy", function () {
            $(window).unbind('scroll');
        });
    }
})();