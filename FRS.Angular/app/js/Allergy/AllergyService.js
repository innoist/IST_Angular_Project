/**=========================================================
 * Module: Currency
 * Currency view Service
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('AllergyService', AllergyService);

    AllergyService.$inject = ['$http'];

    function AllergyService($http) {
        var url = frsApiUrl + "/api/Allergy/";
        this.getAllergies = function (onReady, onError) {
            
           onError = onError || function () { alert('Failed to load data'); };
           $http
             .get(url)
             .success(onReady)
             .error(onError);
        }

        this.loadAllergyById = function (Id, onReady, onError) {
            var urlMetaData = url + Id;

            onError = onError || function () { alert('Failure loading Data'); };

            $http
              .get(urlMetaData)
              .success(onReady)
              .error(onError);
        };

        this.deleteAllergy = function (Id, onReady, onError) {
            var urlMetaData = url + Id;

            onError = onError || function () { alert('Failure deleting Data'); };

            $http
              .delete(urlMetaData)
              .success(onReady)
              .error(onError);
        };


        this.saveAllergy = function (data, onReady, onError) {

            //var urlMetaData = window.frsApiUrl + '/api/LoadMetaData';

            onError = onError || function () { alert('Failure saving Data'); };

            $http(
                {
                    method: 'POST',
                    url: url,
                    data: JSON.stringify(data)
                }
              )
              .then(onReady, onError);
        }

    }
})();