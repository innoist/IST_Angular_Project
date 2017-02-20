/**=========================================================
 * Module: config.js
 * App routes and resources configuration
 =========================================================*/


(function () {
    'use strict';

    angular
        .module('app.routes')
        .config(routesConfig);

    routesConfig.$inject = ['$stateProvider', '$locationProvider', '$urlRouterProvider', 'RouteHelpersProvider', '$controllerProvider', '$provide'];

    function routesConfig($stateProvider, $locationProvider, $urlRouterProvider, helper, $controllerProvider, $provide) {

        // Set the following to true to enable the HTML5 Mode
        // You may have to set <base> tag in index and a routing configuration in your server
        $locationProvider.html5Mode(false);

        // defaults to dashboard
        $urlRouterProvider.otherwise('/home/index');

        var core = angular.module('app.core');
        // Lazy loading
        core.lazy = {
            controller: $controllerProvider.register,
            factory: $provide.factory,
            service: $provide.service
        }

        //
        // Application Routes
        // -----------------------------------
        $stateProvider
            .state('app', {
                url: '/GoCentralize',
                abstract: true,
                templateUrl: helper.basepath('../../../../app/views/Aversion.html'),
                resolve: helper.resolveFor('fastclick', 'ng-animate', 'modernizr', 'icons', 'screenfull', 'animo', 'sparklines', 'slimscroll', 'classyloader', 'toaster', 'whirl', 'loaders.css', 'spinkit', 'Utility.module', 'partials.module')
            })
            .state('home', {
                url: '/home',
                templateUrl: helper.basepath('../../../../app/views/static.html'),
                controller: ['$rootScope', '$localStorage', '$scope', function ($rootScope, $localStorage, $scope) {
                    $rootScope.app.layout.isBoxed = false;
                    if ($localStorage['authorizationData'] && $localStorage['authorizationData'].isAuthenticated) {
                        $scope.isAuthenticated = true;
                    } else
                        $scope.isAuthenticated = false;
                    $scope.CurrentDate = new Date();
                }]
            })
            .state('home.index', {
                url: '/index',
                title: 'Home',
                controller: 'HomeController',
                controllerAs: 'hc',
                templateUrl: helper.basepath('../../../../app/views/Home/home.html'),
                resolve: helper.resolveFor('Home.module', 'oitozero.ngSweetAlert', 'toaster', 'datatables', 'slimscroll', 'login.module')
            })
            .state('home.signin', {
                url: '/SignIn?:returnUrl',
                title: 'Sign In',
                templateUrl: helper.basepath('../../../../app/views/SignIn/SignIn.html'),
                resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins', 'weather-icons', 'login.module', 'slimscroll')
            })
            .state('app.dashboard', {
                url: '/dashboard',
                title: 'Dashboard',
                controller: 'DashboardController',
                controllerAs: 'dc',
                templateUrl: helper.basepath('../../../../app/views/Dashboard/Dashboard.html'),
                resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins', 'weather-icons', 'Dashboard.module')
            })
            .state('app.adminDashboard', {
                url: '/admin/dashboard',
                title: 'Admin Dashboard',
                templateUrl: helper.basepath('../../../../app/views/dashboard-admin.html'),
                resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins', 'weather-icons', 'AdminDashboard.module')
            })

            //Users
            .state('app.Users', {
                url: '/Users',
                title: 'Users',
                templateUrl: helper.basepath('../../../../app/views/Users/Users.html'),
                controller: 'UsersController',
                controllerAs: 'uc',
                resolve: helper.resolveFor('ui.select', 'users.module', 'oitozero.ngSweetAlert', 'datatables')
            })
            //Solutions
            .state('app.Solution', {
                url: '/Solutions',
                title: 'Solutions',
                templateUrl: helper.basepath('../../../../app/views/Solution/Solution.html'),
                controller: 'SolutionController',
                controllerAs: 'sc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'Solution.module')
            })
             .state('app.CreateSolution', {
                 url: '/CreateSolution/:Id',
                 title: 'Solution - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/Solution/CreateSolution.html'),
                 controller: 'CreateSolutionController',
                 controllerAs: 'csc',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'Solution.module', 'filestyle', 'ui.select')
             })
            //Filter Category
            .state('app.FilterCategory', {
                url: '/FilterCategories',
                title: 'Filter Categories',
                templateUrl: helper.basepath('../../../../app/views/FilterCategory/FilterCategory.html'),
                controller: 'FilterCategoryController',
                controllerAs: 'fcc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'FilterCategory.module')
            })
             .state('app.CreateFilterCategory', {
                 url: '/CreateFilterCategory/:Id',
                 title: 'Filter Category - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/FilterCategory/CreateFilterCategory.html'),
                 controller: 'CreateFilterCategoryController',
                 controllerAs: 'cfcc',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'FilterCategory.module', 'filestyle', 'ui.select')
             })
             //Filter
            .state('app.Filter', {
                url: '/Filters',
                title: 'Filters',
                templateUrl: helper.basepath('../../../../app/views/Filter/Filter.html'),
                controller: 'FilterController',
                controllerAs: 'fc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'Filter.module')
            })
             .state('app.CreateFilter', {
                 url: '/CreateFilter/:Id',
                 title: 'Filter - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/Filter/CreateFilter.html'),
                 controller: 'CreateFilterController',
                 controllerAs: 'cfc',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'Filter.module', 'filestyle', 'ui.select')
             })
             //Tag
            .state('app.Tag', {
                url: '/Tags',
                title: 'Tags',
                templateUrl: helper.basepath('../../../../app/views/Tag/Tag.html'),
                controller: 'TagController',
                controllerAs: 'tc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'Tag.module')
            })
             .state('app.CreateTag', {
                 url: '/CreateTag/:Id',
                 title: 'Tag - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/Tag/CreateTag.html'),
                 controller: 'CreateTagController',
                 controllerAs: 'ctc',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'Tag.module', 'filestyle', 'ui.select')
             })
             //Tag Group
            .state('app.TagGroup', {
                url: '/TagGroups',
                title: 'Tag Groups',
                templateUrl: helper.basepath('../../../../app/views/TagGroup/TagGroup.html'),
                controller: 'TagGroupController',
                controllerAs: 'tgc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'TagGroup.module')
            })
             .state('app.CreateTagGroup', {
                 url: '/CreateTagGroup/:Id',
                 title: 'Tag Group - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/TagGroup/CreateTagGroup.html'),
                 controller: 'CreateTagGroupController',
                 controllerAs: 'ctgc',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'TagGroup.module', 'filestyle', 'ui.select')
             })
            //Solution Owner
            .state('app.SolutionOwner', {
                url: '/SolutionOwners',
                title: 'Solution Owner',
                templateUrl: helper.basepath('../../../../app/views/SolutionOwner/SolutionOwner.html'),
                controller: 'SolutionOwnerController',
                controllerAs: 'vm',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'SolutionOwner.module')
            })
             .state('app.CreateSolutionOwner', {
                 url: '/CreateSolutionOwner/:Id',
                 title: 'Solution Owner - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/SolutionOwner/CreateSolutionOwner.html'),
                 controller: 'CreateSolutionOwnerController',
                 controllerAs: 'vm',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'SolutionOwner.module', 'filestyle', 'ui.select')
             })
            //Solution Type
            .state('app.SolutionType', {
                url: '/SolutionTypes',
                title: 'Solution Type',
                templateUrl: helper.basepath('../../../../app/views/SolutionType/SolutionType.html'),
                controller: 'SolutionTypeController',
                controllerAs: 'vm',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'SolutionType.module')
            })
             .state('app.CreateSolutionType', {
                 url: '/CreateSolutionType/:Id',
                 title: 'Solution Type - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/SolutionType/CreateSolutionType.html'),
                 controller: 'CreateSolutionTypeController',
                 controllerAs: 'vm',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'SolutionType.module', 'filestyle', 'ui.select')
             })
            //Profile
            .state('app.Profile', {
                url: '/Profile/:userName',
                title: 'Profile',
                templateUrl: helper.basepath('../../../../app/views/Users/Profile.html'),
                controller: 'ProfileController',
                controllerAs: 'upc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'profile.module')
            })
            .state('app.changePassword', {
                url: '/changePassword',
                title: 'Change Password',
                templateUrl: helper.basepath('../../../../app/views/Users/ChangePassword.html'),
                controller: 'changePasswordController',
                controllerAs: 'cpc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'changePassword.module')
            })
            .state('app.rightsManagement', {
                url: '/RightsManagement',
                title: 'Rights Management',
                templateUrl: helper.basepath('../../../../app/views/RightsManagement/RightManagement.html'),
                controller: 'RightsManagementController',
                controllerAs: 'rmc',
                resolve: helper.resolveFor('rightsManagement.module', 'ui.grid', 'loaders.css', 'spinkit', 'ui.select')
            })

            //Account
            .state('account', {
                url: '/account',
                templateUrl: 'app/pages/page.html',
                resolve: helper.resolveFor('modernizr', 'icons', 'toaster', 'whirl', 'loaders.css', 'spinkit'),
                controller: [
                    '$rootScope', function ($rootScope) {
                        $rootScope.app.layout.isBoxed = false;
                    }
                ]
            })
            .state('account.login', {
                url: '/login?:returnUrl',
                title: 'Login',
                templateUrl: 'app/Views/Users/login.html',
                resolve: helper.resolveFor('login.module', 'oitozero.ngSweetAlert')
            })
            .state('account.register', {
                url: '/register',
                title: 'Register',
                templateUrl: 'app/Views/Users/Register.html',
                resolve: helper.resolveFor('register.module')
            })
            .state('account.recover', {
                url: '/recover',
                title: 'Recover',
                templateUrl: 'app/Views/Users/Recover.html',
                controller: 'ForgotPasswordController',
                controllerAs: 'fpc',
                resolve: helper.resolveFor('recover.module')
            })
            .state('account.lock', {
                url: '/lock',
                title: 'Lock',
                templateUrl: 'app/Views/Users/Lock.html'
            })
            .state('account.404', {
                url: '/401',
                title: 'Not Found',
                templateUrl: 'app/views/Shared/404.html'
            })
            .state('account.ResetPassword', {
                url: '/ResetPassword?:email:code',
                title: 'Reset Password',
                templateUrl: 'app/Views/Users/ResetPassword.html',
                controller: 'ResetPasswordController',
                controllerAs: 'resetPassword',
                resolve: helper.resolveFor('resetPassword.module', 'toaster')
            });


        //
        // CUSTOM RESOLVES
        //   Add your own resolves properties
        //   following this object extend
        //   method
        // -----------------------------------
        // .state('app.someroute', {
        //   url: '/some_url',
        //   templateUrl: 'path_to_template.html',
        //   controller: 'someController',
        //   resolve: angular.extend(
        //     helper.resolveFor(), {
        //     // YOUR RESOLVES GO HERE
        //     }
        //   )
        // })

    } // routesConfig

})();
