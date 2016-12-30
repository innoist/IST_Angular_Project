(function() {
    //angular
    //    .module('app.Users')
    //    .service('UsersService', UsersService);
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('UsersService', UsersService);

    UsersService.$inject = ['$http'];
    function UsersService($http) {
         
        this.url = '';

        this.getUsers = function getUsers(data, onSuccess, onError) {
            
            onError = onError || function () { alert('Failure loading Data'); };
            
            $http
              .get(this.url, data)
              .success(onSuccess)
              .error(onError);
        }

        this.getBaseData = function getBaseData(onSuccess, onError) {
            
            onError = onError || function () { alert('Failure loading Data'); };

            $http
                .get(this.url)
                .success(onSuccess)
                .error(onError);
        }

        this.loadProfile = function loadProfile(data, onSuccess, onError) {
            
            onError = onError || function () { alert('Failure loading Data'); };

            $http
                .get(this.url,data)
                .success(onSuccess)
                .error(onError);
        }
    }

})();