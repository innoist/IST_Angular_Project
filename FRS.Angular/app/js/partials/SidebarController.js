﻿/**=========================================================
 * Module: sidebar-menu.js
 * Handle sidebar collapsible elements
 =========================================================*/

(function () {
    'use strict';

    var core = angular
        .module('app.sidebar');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.controller('SidebarController', SidebarController);
    SidebarController.$inject = ['$http', '$rootScope', '$scope', '$state', 'SidebarLoader', 'Utils'];
    function SidebarController($http, $rootScope, $scope, $state, SidebarLoader, Utils) {
        activate();

        ////////////////

        function activate() {
            var collapseList = [];

            // demo: when switch from collapse to hover, close all items
            $rootScope.$watch('app.layout.asideHover', function (oldVal, newVal) {
                if (newVal === false && oldVal === true) {
                    closeAllBut(-1);
                }
            });


            // Load menu from json file
            // ----------------------------------- 

            SidebarLoader.getMenu(sidebarReady);

            function sidebarReady(items) {
                $scope.menuItems = items;
            }

            // Handle sidebar and collapse items
            // ----------------------------------

            $scope.getMenuItemPropClasses = function (item) {
                return (item.heading ? 'nav-heading' : '') +
                       (isActive(item) ? ' active' : '');
            };

            $scope.addCollapse = function ($index, item) {
                collapseList[$index] = $rootScope.app.layout.asideHover ? true : !isActive(item);
            };

            $scope.isCollapse = function ($index) {
                return (collapseList[$index]);
            };

            $scope.toggleCollapse = function ($index, isParentItem) {

                // collapsed sidebar doesn't toggle drodopwn
                if (Utils.isSidebarCollapsed() || $rootScope.app.layout.asideHover) return true;

                // make sure the item index exists
                if (angular.isDefined(collapseList[$index])) {
                    if (!$scope.lastEventFromChild) {
                        collapseList[$index] = !collapseList[$index];
                        closeAllBut($index);
                    }
                }
                else if (isParentItem) {
                    closeAllBut(-1);
                }

                $scope.lastEventFromChild = isChild($index);

                return true;

            };

            // Controller helpers
            // ----------------------------------- 

            // Check item and children active state
            function isActive(item) {

                if (!item) return;

                if (!item.sref || item.sref === '#') {
                    var foundActive = false;
                    angular.forEach(item.submenu, function (value) {
                        if (isActive(value)) foundActive = true;
                    });
                    return foundActive;
                }
                else
                    return $state.is(item.sref) || $state.includes(item.sref);
            }

            function closeAllBut(index) {
                index += '';
                for (var i in collapseList) {
                    if (index < 0 || index.indexOf(i) < 0)
                        collapseList[i] = true;
                }
            }

            function isChild($index) {
                /*jshint -W018*/
                return (typeof $index === 'string') && !($index.indexOf('-') < 0);
            }

        } // activate
    }

})();

/**=========================================================
 * Module: sidebar.js
 * Wraps the sidebar and handles collapsed state
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.sidebar')
        .directive('sidebar', sidebar);

    sidebar.$inject = ['$rootScope', '$timeout', '$window', 'Utils'];
    function sidebar($rootScope, $timeout, $window, Utils) {
        var $win = angular.element($window);
        var directive = {
            // bindToController: true,
            // controller: Controller,
            // controllerAs: 'vm',
            link: link,
            restrict: 'EA',
            template: '<nav class="sidebar" ng-transclude></nav>',
            transclude: true,
            replace: true
            // scope: {}
        };
        return directive;

        function link(scope, element, attrs) {

            var currentState = $rootScope.$state.current.name;
            var $sidebar = element;

            var eventName = Utils.isTouch() ? 'click' : 'mouseenter';
            var subNav = $();

            $sidebar.on(eventName, '.nav > li', function () {

                if (Utils.isSidebarCollapsed() || $rootScope.app.layout.asideHover) {

                    subNav.trigger('mouseleave');
                    subNav = toggleMenuItem($(this), $sidebar);

                    // Used to detect click and touch events outside the sidebar          
                    sidebarAddBackdrop();

                }

            });

            scope.$on('closeSidebarMenu', function () {
                removeFloatingNav();
            });

            // Normalize state when resize to mobile
            $win.on('resize', function () {
                if (!Utils.isMobile())
                    asideToggleOff();
            });

            // Adjustment on route changes
            $rootScope.$on('$stateChangeStart', function (event, toState) {
                currentState = toState.name;
                // Hide sidebar automatically on mobile
                asideToggleOff();

                $rootScope.$broadcast('closeSidebarMenu');
            });

            // Autoclose when click outside the sidebar
            if (angular.isDefined(attrs.sidebarAnyclickClose)) {

                var wrapper = $('.wrapper');
                var sbclickEvent = 'click.sidebar';

                $rootScope.$watch('app.asideToggled', watchExternalClicks);

            }

            //////

            function watchExternalClicks(newVal) {
                // if sidebar becomes visible
                if (newVal === true) {
                    $timeout(function () { // render after current digest cycle
                        wrapper.on(sbclickEvent, function (e) {
                            // if not child of sidebar
                            if (!$(e.target).parents('.aside').length) {
                                asideToggleOff();
                            }
                        });
                    });
                }
                else {
                    // dettach event
                    wrapper.off(sbclickEvent);
                }
            }

            function asideToggleOff() {
                $rootScope.app.asideToggled = false;
                if (!scope.$$phase) scope.$apply(); // anti-pattern but sometimes necessary
            }
        }

        ///////

        function sidebarAddBackdrop() {
            var $backdrop = $('<div/>', { 'class': 'dropdown-backdrop' });
            $backdrop.insertAfter('.aside-inner').on('click mouseenter', function () {
                removeFloatingNav();
            });
        }

        // Open the collapse sidebar submenu items when on touch devices 
        // - desktop only opens on hover
        function toggleTouchItem($element) {
            $element
              .siblings('li')
              .removeClass('open')
              .end()
              .toggleClass('open');
        }

        // Handles hover to open items under collapsed menu
        // ----------------------------------- 
        function toggleMenuItem($listItem, $sidebar) {

            removeFloatingNav();

            var ul = $listItem.children('ul');

            if (!ul.length) return $();
            if ($listItem.hasClass('open')) {
                toggleTouchItem($listItem);
                return $();
            }

            var $aside = $('.aside');
            var $asideInner = $('.aside-inner'); // for top offset calculation
            // float aside uses extra padding on aside
            var mar = parseInt($asideInner.css('padding-top'), 0) + parseInt($aside.css('padding-top'), 0);
            var subNav = ul.clone().appendTo($aside);

            toggleTouchItem($listItem);

            var itemTop = ($listItem.position().top + mar) - $sidebar.scrollTop();
            var vwHeight = $win.height();

            subNav
              .addClass('nav-floating')
              .css({
                  position: $rootScope.app.layout.isFixed ? 'fixed' : 'absolute',
                  top: itemTop,
                  bottom: (subNav.outerHeight(true) + itemTop > vwHeight) ? 0 : 'auto'
              });

            subNav.on('mouseleave', function () {
                toggleTouchItem($listItem);
                subNav.remove();
            });

            return subNav;
        }

        function removeFloatingNav() {
            $('.dropdown-backdrop').remove();
            $('.sidebar-subnav.nav-floating').remove();
            $('.sidebar li.open').removeClass('open');
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('app.sidebar')
        .service('SidebarLoader', SidebarLoader);

    SidebarLoader.$inject = ['$http', '$localStorage', '$rootScope'];
    function SidebarLoader($http, $localStorage, $rootScope) {
        this.getMenu = getMenu;

        ////////////////

        function getMenu(onReady, onError) {
            if (!$localStorage['authorizationData']) {
                return;
            }
            var menuJson = frsApiUrl + '/api/Menu',
            //var menuJson = '../../server/sidebar-menu.js',
                menuURL = menuJson + '?&v=' + (new Date().getTime()); // jumps cache

            onError = onError || function () { alert('Failure loading menu'); };

            $http
              .get(menuURL)
              .success(onReady)
              .error(onError);
        }
    }
})();