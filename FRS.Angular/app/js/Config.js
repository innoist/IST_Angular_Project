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
                templateUrl: helper.basepath('../../../../app/views/Aversion.html?v=' + currentVersion),
                resolve: helper.resolveFor('fastclick', 'ng-animate', 'modernizr', 'icons', 'screenfull', 'animo', 'sparklines', 'slimscroll', 'classyloader', 'toaster', 'whirl', 'loaders.css', 'spinkit', 'Utility.module', 'partials.module')
            })
            .state('home', {
                url: '/home',
                templateUrl: helper.basepath('../../../../app/views/Shared/static.html?v=' + currentVersion),
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
                templateUrl: helper.basepath('../../../../app/views/Home/home.html?v=' + currentVersion),
                resolve: helper.resolveFor('Home.module', 'oitozero.ngSweetAlert', 'toaster', 'datatables', 'slimscroll', 'login.module')
            })
            .state('app.dashboard', {
                url: '/dashboard',
                title: 'Dashboard',
                controller: 'DashboardController',
                controllerAs: 'dc',
                templateUrl: helper.basepath('../../../../app/views/Dashboard/Dashboard.html?v=' + currentVersion),
                resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins', 'weather-icons', 'Dashboard.module')
            })
            .state('app.adminDashboard', {
                url: '/admin/dashboard',
                title: 'Admin Dashboard',
                templateUrl: helper.basepath('../../../../app/views/dashboard-admin.html?v=' + currentVersion),
                resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins', 'weather-icons', 'AdminDashboard.module')
            })

            //Users
            .state('app.Users', {
                url: '/Users',
                title: 'Users',
                templateUrl: helper.basepath('../../../../app/views/Users/Users.html?v=' + currentVersion),
                controller: 'UsersController',
                controllerAs: 'uc',
                resolve: helper.resolveFor('ui.select', 'users.module', 'oitozero.ngSweetAlert', 'datatables')
            })
            //Solutions
            .state('app.Solution', {
                url: '/Solutions',
                title: 'Solutions',
                templateUrl: helper.basepath('../../../../app/views/Solution/Solution.html?v=' + currentVersion),
                controller: 'SolutionController',
                controllerAs: 'sc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'Solution.module')
            })
             .state('app.CreateSolution', {
                 url: '/CreateSolution/:Id',
                 title: 'Solution - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/Solution/CreateSolution.html?v=' + currentVersion),
                 controller: 'CreateSolutionController',
                 controllerAs: 'csc',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'Solution.module', 'filestyle', 'ui.select')
             })
            //Filter Category
            .state('app.FilterCategory', {
                url: '/FilterCategories',
                title: 'Filter Categories',
                templateUrl: helper.basepath('../../../../app/views/FilterCategory/FilterCategory.html?v=' + currentVersion),
                controller: 'FilterCategoryController',
                controllerAs: 'fcc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'FilterCategory.module')
            })
             .state('app.CreateFilterCategory', {
                 url: '/CreateFilterCategory/:Id',
                 title: 'Filter Category - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/FilterCategory/CreateFilterCategory.html?v=' + currentVersion),
                 controller: 'CreateFilterCategoryController',
                 controllerAs: 'cfcc',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'FilterCategory.module', 'filestyle', 'ui.select')
             })
             //Filter
            .state('app.Filter', {
                url: '/Filters',
                title: 'Filters',
                templateUrl: helper.basepath('../../../../app/views/Filter/Filter.html?v=' + currentVersion),
                controller: 'FilterController',
                controllerAs: 'fc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'Filter.module')
            })
             .state('app.CreateFilter', {
                 url: '/CreateFilter/:Id',
                 title: 'Filter - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/Filter/CreateFilter.html?v=' + currentVersion),
                 controller: 'CreateFilterController',
                 controllerAs: 'cfc',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'Filter.module', 'filestyle', 'ui.select')
             })
             //Tag
            .state('app.Tag', {
                url: '/Tags',
                title: 'Tags',
                templateUrl: helper.basepath('../../../../app/views/Tag/Tag.html?v=' + currentVersion),
                controller: 'TagController',
                controllerAs: 'tc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'Tag.module')
            })
             .state('app.CreateTag', {
                 url: '/CreateTag/:Id',
                 title: 'Tag - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/Tag/CreateTag.html?v=' + currentVersion),
                 controller: 'CreateTagController',
                 controllerAs: 'ctc',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'Tag.module', 'filestyle', 'ui.select')
             })
             //Tag Group
            .state('app.TagGroup', {
                url: '/TagGroups',
                title: 'Tag Groups',
                templateUrl: helper.basepath('../../../../app/views/TagGroup/TagGroup.html?v=' + currentVersion),
                controller: 'TagGroupController',
                controllerAs: 'tgc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'TagGroup.module')
            })
             .state('app.CreateTagGroup', {
                 url: '/CreateTagGroup/:Id',
                 title: 'Tag Group - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/TagGroup/CreateTagGroup.html?v=' + currentVersion),
                 controller: 'CreateTagGroupController',
                 controllerAs: 'ctgc',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'TagGroup.module', 'filestyle', 'ui.select')
             })
            //Solution Owner
            .state('app.SolutionOwner', {
                url: '/SolutionOwners',
                title: 'Solution Owner',
                templateUrl: helper.basepath('../../../../app/views/SolutionOwner/SolutionOwner.html?v=' + currentVersion),
                controller: 'SolutionOwnerController',
                controllerAs: 'vm',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'SolutionOwner.module')
            })
             .state('app.CreateSolutionOwner', {
                 url: '/CreateSolutionOwner/:Id',
                 title: 'Solution Owner - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/SolutionOwner/CreateSolutionOwner.html?v=' + currentVersion),
                 controller: 'CreateSolutionOwnerController',
                 controllerAs: 'vm',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'SolutionOwner.module', 'filestyle', 'ui.select')
             })
            //Solution Type
            .state('app.SolutionType', {
                url: '/SolutionTypes',
                title: 'Solution Type',
                templateUrl: helper.basepath('../../../../app/views/SolutionType/SolutionType.html?v=' + currentVersion),
                controller: 'SolutionTypeController',
                controllerAs: 'vm',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'ui.select', 'SolutionType.module')
            })
             .state('app.CreateSolutionType', {
                 url: '/CreateSolutionType/:Id',
                 title: 'Solution Type - Add/Edit',
                 templateUrl: helper.basepath('../../../../app/views/SolutionType/CreateSolutionType.html?v=' + currentVersion),
                 controller: 'CreateSolutionTypeController',
                 controllerAs: 'vm',
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'SolutionType.module', 'filestyle', 'ui.select')
             })
            //Profile
            .state('app.Profile', {
                url: '/Profile/:userName',
                title: 'Profile',
                templateUrl: helper.basepath('../../../../app/views/Users/Profile.html?v=' + currentVersion),
                controller: 'ProfileController',
                controllerAs: 'upc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'profile.module')
            })
            .state('app.changePassword', {
                url: '/changePassword',
                title: 'Change Password',
                templateUrl: helper.basepath('../../../../app/views/Users/ChangePassword.html?v=' + currentVersion),
                controller: 'changePasswordController',
                controllerAs: 'cpc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'changePassword.module')
            })
            .state('app.rightsManagement', {
                url: '/RightsManagement',
                title: 'Rights Management',
                templateUrl: helper.basepath('../../../../app/views/RightsManagement/RightManagement.html?v=' + currentVersion),
                controller: 'RightsManagementController',
                controllerAs: 'rmc',
                resolve: helper.resolveFor('rightsManagement.module', 'ui.grid', 'loaders.css', 'spinkit', 'ui.select')
            })

            //Account
            .state('account', {
                url: '/account',
                templateUrl: 'app/views/Shared/page.html?v=' + currentVersion,
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
                templateUrl: 'app/Views/Users/login.html?v=' + currentVersion,
                resolve: helper.resolveFor('login.module', 'oitozero.ngSweetAlert')
            })
            .state('account.register', {
                url: '/register',
                title: 'Register',
                templateUrl: 'app/Views/Users/Register.html?v=' + currentVersion,
                resolve: helper.resolveFor('register.module')
            })

            .state('account.recover', {
                url: '/recover',
                title: 'Recover',
                templateUrl: 'app/Views/Users/Recover.html?v=' + currentVersion,
                controller: 'ForgotPasswordController',
                controllerAs: 'fpc',
                resolve: helper.resolveFor('recover.module')
            })
            .state('account.lock', {
                url: '/lock',
                title: 'Lock',
                templateUrl: 'app/Views/Users/Lock.html?v=' + currentVersion
            })
            .state('account.401', {
                url: '/401',
                title: 'Not Authorized',
                templateUrl: 'app/views/Shared/401.html?v=' + currentVersion
            })
            .state('account.ResetPassword', {
                url: '/ResetPassword?:email:code',
                title: 'Reset Password',
                templateUrl: 'app/Views/Users/ResetPassword.html?v=' + currentVersion,
                controller: 'ResetPasswordController',
                controllerAs: 'rpc',
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
