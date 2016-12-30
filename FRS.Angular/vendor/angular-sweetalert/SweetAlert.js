/**
@fileOverview

@toc

*/

'use strict';

angular.module('oitozero.ngSweetAlert', [])
.factory('SweetAlert', ['$rootScope', function ($rootScope) {

    var swal = window.swal;

    //public methods
    var self = {

        swal: function (arg1, arg2, arg3) {
            
            $rootScope.$evalAsync(function () {
                var options = {
                    title: 'Are you sure?',
                    text: 'All data you entered in form will be lost!',
                    type: 'warning',
                    showCancelButton: true,
                    //confirmButtonColor: '#DD6B55',
                    confirmButtonColor: '#ee3d3d',
                    confirmButtonText: 'Yes, cancel saving form.!',
                    cancelButtonText: 'No, stay on this page!',
                    closeOnConfirm: true,
                    closeOnCancel: true
                };
                if (arg1) {
                    options = arg1;
                }
                if (typeof (arg2) === 'function') {

                    swal(options, function (isConfirm) {
                        $rootScope.$evalAsync(function () {
                            arg2(isConfirm);
                        });
                    }, arg3);
                } else {
                    swal(arg1, arg2, arg3);
                }
            });
        },
        success: function (title, message) {
            $rootScope.$evalAsync(function () {
                swal(title, message, 'success');
            });
        },
        error: function (title, message) {
            $rootScope.$evalAsync(function () {
                swal(title, message, 'error');
            });
        },
        warning: function (title, message) {
            $rootScope.$evalAsync(function () {
                swal(title, message, 'warning');
            });
        },
        info: function (title, message) {
            $rootScope.$evalAsync(function () {
                swal(title, message, 'info');
            });
        }
    };

    return self;
}]);
