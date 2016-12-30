(function() {
    'use strict';

    //angular.module('app.Profile')
    //    .service('ProfileService', ProfileService);
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('ProfileService', ProfileService);

    ProfileService.$inject = ['$http', '$rootScope'];

    function ProfileService($http, $rootScope) {
        this.loadProfile = loadProfile;
        this.saveProfile = saveProfile;
        this.getBaseData = getBaseData;

        function saveProfile(data,onSuccess, onError) {
            var url = window.frsApiUrl + '/api/Account/Register';

            onError = onError || function () { alert('Failure loading Data'); };

            $http
                .post(url,data)
                .success(onSuccess)
                .error(onError);
        }

        function loadProfile(userName, onSuccess, onError) {
            var url = window.frsApiUrl + '/api/UserBaseData?userName=' + userName;

            onError = onError || function () { alert('Failure saving Data'); };
            onSuccess = onSuccess || function () { alert('Save Complete'); };

            $http
                .get(url)
                .success(onSuccess)
                .error(onError);
        }

        function getBaseData(onSuccess, onError) {
            var url = window.frsApiUrl + '/api/UserBaseData';
            onError = onError || function () { alert('Failure loading Data'); };

            $http
                .get(url)
                .success(onSuccess)
                .error(onError);
        }
    }
})();