/**=========================================================
 * Module: UtilityService
 * UtilityService
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('UtilityService', UtilityService);

    UtilityService.$inject = ['$filter'];

    function UtilityService($filter) {

        var get12HourFormatTime = function(timeString) {
            var time = timeString.split(":");
            var d = new Date();
            d.setHours(time[0], time[1], 0);
            timeString = $filter('date')(d, "hh:mm a");
            return timeString;
        }

        return {
            formatTime: get12HourFormatTime
        }
    }
})();