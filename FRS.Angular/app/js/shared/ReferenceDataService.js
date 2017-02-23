/**=========================================================
                 * Module: ReferenceDataService
                 * ReferenceDataService
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('ReferenceDataService', ReferenceDataService);

    ReferenceDataService.$inject = ['$http', 'toaster', '$localStorage', '$state', '$q'];

    function ReferenceDataService($http, toaster, $localStorage, $state, $q) {

        this.url = "";

        this.getAll = function (onReady, onError, url) {
            $.blockUI({ message: '<div class="line-spin-fade-loader" style="left:50%; top:50%"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>' });
            if (!url)
                url = this.url;

            onError = onError || function (response) {
                if (response) {
                    $.unblockUI();
                    if (response.ExceptionMessage) {
                        toaster.error(response.ExceptionMessage);
                    }
                    if (response.Message) {
                        toaster.error(response.Message);
                    }
                }
            };
            $http
              .get(frsApiUrl + url)
              .success(onReady)
              .error(onError);
        }

        this.loadById = function (Id, onReady, onError) {
            $.blockUI({ message: '<div class="line-spin-fade-loader" style="left:50%; top:50%"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>' });
            var urlMetaData = frsApiUrl + this.url + "?id=" + Id;

            onError = onError || function () {
                alert('Failure loading Data');
                $.unblockUI();
            };

            $http
              .get(urlMetaData)
              .success(onReady)
              .error(onError);
        };

        this.load = function (url, data, onReady, onError) {
            $.blockUI({ message: '<div class="line-spin-fade-loader" style="left:50%; top:50%"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>' });
            if (!url)
                url = this.url;

            onError = onError || function (response) {
                $.unblockUI();
                if (response.ExceptionMessage) {
                    toaster.error(response.ExceptionMessage);
                } if (response.Message) {
                    toaster.error(response.Message);
                }
            };

            $http
              .get(frsApiUrl + url, {
                  params: data
              })
              .success(onReady)
              .error(onError);
        };

        this.delete = function (Id, onReady, onError) {
            $.blockUI({ message: '<div class="line-spin-fade-loader" style="left:50%; top:50%"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>' });
            var urlMetaData = frsApiUrl + this.url + Id;

            onError = onError || function (response) {
                $.unblockUI();
                if (response.ExceptionMessage) {
                    toaster.error(response.ExceptionMessage);
                };
                if (response.Message)
                    toaster.error(response.Message);
            };

            $http
              .delete(urlMetaData)
              .success(onReady)
              .error(onError);
        };

        this.save = function (data, onReady, onError, url) {
            $.blockUI({ message: '<div class="line-spin-fade-loader" style="left:50%; top:50%"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>' });
            if (!url)
                url = this.url;
            //var urlMetaData = window.frsApiUrl + '/api/LoadMetaData';

            onError = onError || function (response) {
                $.unblockUI();
                if (response.ExceptionMessage) {
                    toaster.error('Error', response.ExceptionMessage);
                } else {
                    toaster.error('Error', response.data.ExceptionMessage);
                }
            };

            $http(
                {
                    method: 'POST',
                    url: frsApiUrl + url,
                    data: JSON.stringify(data)
                }
              ).then(onReady, onError);
        }

        this.uploadFile = function (data, onReady, onError, url) {
            if (!url)
                url = this.url;
            //var urlMetaData = window.frsApiUrl + '/api/LoadMetaData';

            onError = onError || function (response) {
                $.unblockUI();
                if (response.ExceptionMessage) {
                    toaster.error('Error', response.ExceptionMessage);
                } else {
                    toaster.error('Error', response.data.ExceptionMessage);
                }
            };

            $http.post(frsApiUrl + url, data,
                {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }
              ).then(onReady, onError);
        }

        this.retrieveItems = function (url, val, ids) {
            return $http.get(frsApiUrl + url, {
                params: {
                    name: val,
                    filterIds: ids
                }
            });
        }

        this.logout = function () {
            $.blockUI({ message: '<div class="line-spin-fade-loader" style="left:50%; top:50%"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>' });
            var defered = $q.defer();
            $http.post(window.frsApiUrl + "/api/Account/Logout")
                .success(function (response) {
                    $state.go('account.login', {}, { reload: true });
                    delete $localStorage['authorizationData'];
                    console.log("LoggedOut");

                    $http.defaults.headers.common = {
                        'Content-Type': 'application/json'
                    };
                    defered.resolve(response);
                })
                .error(function (err) {
                    $.unblockUI();
                    showErrors(err);
                });

            return defered.promise;
        }
    }
})();