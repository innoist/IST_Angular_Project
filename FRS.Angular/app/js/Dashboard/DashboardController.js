(function () {
    'use strict';

    var core = angular.module('app.core');
    core.lazy.controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$scope', '$state', 'ReferenceDataService', 'UtilityService', '$rootScope', '$stateParams', '$interval', '$sce', '$filter'];
    function DashboardController($scope, $state, DashboardService, UtilityService, $rootScope, $stateParams, $interval, $sce, $filter) {
        var vm = this;
        activate();

        ////////////////
        function activate() {
            vm.Rooms = [];
            vm.TotalStudents = 0;
            vm.Present = 0;
            vm.Absent = 0;
            vm.IsDayCareOpen = false;
            $scope.time = "00:00:00";

            DashboardService.url = "/api/Dashboard/";
            //Service call for fetching data
            var getData = function () {
                DashboardService.getAll(function (response) {
                    if (response) {
                        vm.Rooms = response.Rooms;
                        vm.TotalStudents = response.TotalStudents;
                        vm.Present = response.StudentsPresent;
                        vm.Absent = response.StudentsAbsent;
                        vm.TodayMeals = response.TodayMeals;
                        vm.IsDayCareOpen = response.IsDayCareOpen;

                        if (response.TodayMeals && response.TodayMeals.length && vm.IsDayCareOpen) {
                            vm.KeyIndex = 0;

                            //Iterate over the grouped meals to set the latest meal in queue
                            var temp;
                            angular.forEach(vm.TodayMeals, function (meal, index) {
                                meal.IsActive = true;
                                meal.Meals.forEach(function (dailyMeal) {
                                    dailyMeal.MealTimeString = UtilityService.formatTime(dailyMeal.MealTimeString);
                                });
                                if (!temp) {
                                    //find the first occurence of the latest meal
                                    temp = meal.Meals.find(x => x.SortOrder === 1);
                                    vm.Meal = temp;
                                    meal.IsActive = false;
                                    //Set the key index to the latest meal
                                    vm.KeyIndex = index;
                                }
                            });
                            if (!vm.Meal) {
                                vm.Meal = vm.TodayMeals[vm.TodayMeals.length - 1].Meals[0];
                            }
                            //Start the count down timer
                            countDownTimer();
                        }
                        //Iterate over the rooms and set the age class and color for styling
                        var index = 0;
                        vm.Rooms.forEach(function (room) {
                            index++;
                            index = index === vm.RoomColor.length ? 0 : index;
                            var ageClass = vm.AgeClasses.find(x => x.Id === room.AgeClassId);
                            room.AgeClass = ageClass ? ageClass.DisplayName : null;
                            room.Class = vm.RoomColor[index];
                        });
                    }
                });
            }

            //getData();
            //Local array for AgeClass
            vm.AgeClasses = [{ Id: 1, DisplayName: "0-1" }, { Id: 2, DisplayName: "1-2" }, { Id: 3, DisplayName: "2-3" }, { Id: 4, DisplayName: "3-4" }, { Id: 5, DisplayName: "4-5" }, { Id: 6, DisplayName: "5-6" }, { Id: 7, DisplayName: "6-7" }, { Id: 8, DisplayName: "7-8" }];
            vm.RoomColor = ['panel-info', 'panel-warning', 'panel-success', 'panel-primary', 'panel-danger', 'panel-pink', 'panel-purple'];

            vm.gotoDetail = function(index, event) {
                event.stopPropagation();
                event.preventDefault();
                if (!vm.Rooms[index].StudentsCount)
                    return;
                var id = vm.Rooms[index].RoomId;
                DashboardService.setSharedData(vm.Rooms[index].Title);
                $state.go("app.RoomStudents", { Id: id });
            }

            //Method to get the next meal in queue
            vm.NextMeal = function () {
                if (vm.KeyIndex === vm.TodayMeals.length - 1)
                    return;
                vm.KeyIndex = vm.KeyIndex + 1;
                vm.Meal = vm.TodayMeals[vm.KeyIndex].Meals[0];
            }

            //Method to get the previous meal in queue
            vm.PreviousMeal = function () {
                if (vm.KeyIndex === 0)
                    return;
                vm.KeyIndex = vm.KeyIndex - 1;
                vm.Meal = vm.TodayMeals[vm.KeyIndex].Meals[0];
            }

            //get meals for tooltip
            vm.getOtherMeals = function (key) {
                var templateTootip = '';
                var mealNames = vm.TodayMeals.find(x=>x.MealName === key).Meals.slice();
                mealNames.splice(1).forEach(function (meal) {
                    templateTootip += '<span>' + meal.Name + '</span><br/>';
                });

                return (templateTootip);
            }

            vm.prepareMeal = function (meal) {
                DashboardService.setSharedData({
                    MealScheduleId: meal.MealScheduleId,
                    dailyMealId : meal.MealId,
                    meal: {
                        name: meal.MealName,
                        time: meal.MealTimeString
                    }
                });
                $state.go("app.prepareMeals", { ref: 'dashboard' });
            }
            //Countdown timer for meal that's next
            function countDownTimer() {
                //helper function for padded digits
                var getPadded = function (val) {
                    return val < 10 ? ('0' + val) : val;
                };

                //Set Time for next meal
                var nextMealTime = new Date();
                var hours = vm.Meal.MealTime.split(":")[0];
                var minutes = vm.Meal.MealTime.split(":")[1];

                nextMealTime.setHours(hours);
                nextMealTime.setMinutes(minutes);
                nextMealTime.setSeconds(0);

                //Helper function to return remaining time span object
                function getTimeRemaining(endtime) {
                    var t = Date.parse(endtime) - Date.parse(new Date());
                    var seconds = Math.floor((t / 1000) % 60);
                    var minutes = Math.floor((t / 1000 / 60) % 60);
                    var hours = Math.floor((t / (1000 * 60 * 60)) % 24);
                    var days = Math.floor(t / (1000 * 60 * 60 * 24));
                    return {
                        'total': t,
                        'days': days,
                        'hours': hours,
                        'minutes': minutes,
                        'seconds': seconds
                    };
                }

                //Interval function to called after 1 second
                var timeInterval = $interval(function () {
                    var t = getTimeRemaining(nextMealTime);
                    //if time expired clear interval
                    if (t.total <= 0) {

                        $scope.time = "00:00:00";
                        $interval.cancel(timeInterval);
                    } else {
                        //set/show time
                        $scope.t = t;
                        $scope.time = getPadded(t.hours) + ':' + getPadded(t.minutes) + ':' + getPadded(t.seconds);
                    }
                }, 1000);
            }
        }
    }
})();