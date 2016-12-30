/**=========================================================
 * Module: change password
 * change password Service
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('changePasswordService', changePasswordService);

    changePasswordService.$inject = ['$http', '$rootScope'];
    function changePasswordService($http, $rootScope) {

        this.changePassword = function (data, onReady, onError) {
            var url = frsApiUrl + "/api/Account/ChangePassword";
            onError = onError || function () { alert('Failed to load rights'); };
            $http
              .post(url, data)
              .success(onReady)
              .error(onError);
        }
    }


}
)();