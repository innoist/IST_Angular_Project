(function () {
    'use strict';

    var core = angular
        .module('app.sidebar')
        .controller('UserBlockController', UserBlockController);

    UserBlockController.$inject = ['$http', '$state', '$rootScope', '$scope', '$localStorage', 'toaster', '$timeout'];
    function UserBlockController($http, $state, $rootScope, $scope, $localStorage, toaster, $timeout) {

        activate();
        ////////////////

        function activate() {
            $rootScope.user = {
                name: $localStorage['authorizationData'].userName,
                role: $localStorage['authorizationData'].UserRole,
                picture: $localStorage['authorizationData'].Image ? $localStorage['authorizationData'].Image : '../../app/img/user/default.png'
            };

            $rootScope.selectDashboard = function () {
                //if ($rootScope.user.role === "SystemAdministrator") {
                //    $state.go("app.adminDashboard");
                //} else if ($rootScope.user.role === "Admin") {
                //    $state.go("app.dashboard");
                //} else if ($rootScope.user.role === "Client") {
                //    $state.go("app.dashboard");
                //}
            }

            // Hides/show user avatar on sidebar
            $rootScope.toggleUserBlock = function () {
                $rootScope.$broadcast('toggleUserBlock');
            };

            $rootScope.userBlockVisible = true;

            var detach = $rootScope.$on('toggleUserBlock', function (/*event, args*/) {

                $rootScope.userBlockVisible = !$rootScope.userBlockVisible;

            });

            $scope.$on('$destroy', detach);
            $rootScope.logout = function () {
                $http.post(frsApiUrl + "/api/Account/Logout")
                    .success(function () {
                        delete $localStorage['authorizationData'];
                        console.log("LoggedOut");
                        //$.connection.hub.stop();
                        $rootScope.isAuthenticated = false;
                        $http.defaults.headers.common = {
                            'Content-Type': 'application/json'
                        };
                        $state.go('account.login');
                    })
                    .error(function (err) {
                        showErrors(err);
                    });
            }

            window.onresize = function () {
                if (window.innerWidth <= 480) {
                    $rootScope.isMobileView = true;
                }else{
                    $rootScope.isMobileView = false;
                }
            }
            
        }

        //$timeout(function () {
        //    /*
        //     * SignalR (M.Jahangir - 28/10/2016)
        //     */
        //    $rootScope.Notifications = [];
        //    window.hub = $.connection.notificationHub; // create a proxy to signalr hub on web server
        //    $.connection.hub.logging = true;
        //    $.connection.hub.qs = "dayCareName=" + $rootScope.domainKeyName;
        //    $.signalR.ajaxDefaults.headers = { Authorization: "Bearer " + $localStorage["authorizationData"].token };
        //    $.connection.hub.url = frsApiUrl + "/signalr";
        //    window.hub.client.recieveMsg = function (message, studentId) {
        //        $rootScope.Notifications.push({
        //            message: message,
        //            isRead: false,
        //            timeStamp: Date.now(),
        //            localTimeString: (new Date()).toLocaleString(),
        //            studentId: studentId
        //        });
        //        toaster.pop("info", message);

        //    }
        //    $.connection.hub.start({ transport: ['longPolling'] }).done(function (response) {
        //        console.log("Connected");

        //    }).fail(function (err) {
        //        alert("SignalR : Connection cannot be established");
        //        console.log(err);
        //        ////Retry in case of failure
        //        //$.connection.hub.start({ transport: 'longPolling' }).done(function (response) {
        //        //    console.log("Connected");

        //        //}).fail(function (err) {
        //        //    alert("Connection failed. Retrying...");
        //        //    console.log(err);
        //        //});
        //    });

        //    $rootScope.markAsRead = function (index, $event) {
        //        $event.preventDefault();
        //        $event.stopPropagation();
        //        $rootScope.Notifications[index].isRead = true;
        //    }
        //    $rootScope.notificationDetail = function (index, $event) {
        //        $event.preventDefault();
        //        $event.stopPropagation();
        //        $rootScope.Notifications[index].isRead = true;
        //        var id = $rootScope.Notifications[index].studentId;
        //        $state.go("app.CreateAttendance", { Id: id });
        //    }

        //}, 3000);
    }
})();