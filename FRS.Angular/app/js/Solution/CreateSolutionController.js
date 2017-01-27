//Create MetaData
(function () {

    var core = angular.module('app.core');
    //ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('CreateSolutionController', CreateSolutionController);

    CreateSolutionController.$inject = ['$state', '$timeout', '$scope', '$stateParams', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster'];

    function CreateSolutionController($state, $timeout, $scope, $stateParams, $rootScope, SolutionService, SweetAlert, toaster) {
        var vm = this;
        var solutionId = 0;
        vm.update = false;
        vm.submitted = false;
        vm.Solution = {};
        vm.formdata = new FormData();

        //Api Url
        SolutionService.url = "/api/Solution/";

        vm.dtInstance = {};
        vm.ResetImage = function () {
            vm.Solution.Image = null;
            $('.SolutionImage').attr('src', '/app/img/image-default.png');
        }

        vm.multiple = {};

        $timeout(function () {
            //to remove input field that contains name of image.
            $('.bootstrap-filestyle.input-group input').css("display", "none");
        }, 1000);

        //Upload File
        $scope.readFile = function (input) {

            if (input.files && input.files[0]) {
                //calculate size of image in MB's
                var sizeOfImageInMB = parseFloat(input.files[0].size) / parseFloat((1024 * 1024));
                //Check if greater than 1MB
                if (sizeOfImageInMB > parseFloat(1)) {
                    toaster.pop("error", "Too large", "Select an image less than 1 MB");
                    return;
                }
                vm.Image = '/app/img/loading.gif';
                $('.SolutionImage').attr('src', vm.Image);
                vm.FileExtension = input.files[0].name.split('.')[1];
                vm.FileName = input.files[0].name.split('.')[0];

                vm.formdata.append(0, input.files[0]);

                var reader = new FileReader();
                reader.onload = function (e) {
                    vm.Image = reader.result;
                    $('.SolutionImage').attr('src', vm.Image);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };

        //#region Post Data
        vm.save = function (isNew) {
            vm.submitted = true;

            if (!vm.formValidate.$valid) {
                toaster.pop("error", showValidationErrors(vm.formValidate));
                return false;
            }
            vm.Solution.TagIds = vm.multiple.Tags.map(x=>x.Id);
            vm.Solution.FilterIds = [];
            if (vm.multiple.Filters && vm.multiple.Filters.length > 0)
                vm.Solution.FilterIds = vm.multiple.Filters.map(x=>x.Id);

            vm.Solution.OwnerId = vm.SolutionOwners.selected.Id;
            vm.Solution.TypeId = vm.SolutionTypes.selected.Id;
            vm.formdata.append(1, JSON.stringify(vm.Solution));
            SolutionService.uploadFile(vm.formdata, onSuccess, onError);
            function onSuccess(response) {
                if (response.data === true) {
                    toaster.pop("success", "Notification", "Data has been saved successfully.");
                    if (isNew) {
                        //reseting form
                        vm.formValidate.$setPristine();
                        vm.submitted = false;
                        solutionId = 0;
                        $('.SolutionImage').attr('src', '/app/img/image-default.png');
                        vm.Solution = {};
                        vm.SolutionOwners.selected = null;
                        vm.SolutionTypes.selected = null;
                        vm.multiple.Tags = [];
                        vm.multiple.Filters = [];
                        $state.go('app.CreateSolution');
                    }
                    if (!isNew) {
                        $state.go('app.Solution');
                    }
                } else {
                    toaster.error("Notification", "Solution with this name already exists.");
                }
            }
            function onError(err) {
                toaster.error(err.statusText, err.data.Message);
                showErrors(err);
            }
        }
        //#endregion

        vm.cancelBtn = function () {
            if (!vm.formValidate.$dirty) {
                $state.go('app.Solution');
            } else {
                SweetAlert.swal(null, function (isConfirm) {
                    if (isConfirm) {
                        $state.go('app.Solution');
                    }
                });
            }
        }

        if ($stateParams.Id === "") {
            vm.edit = true;
        }

        solutionId = $stateParams.Id === "" ? 0 : $stateParams.Id;

        SolutionService.loadById(solutionId, function (response) {
            if (response) {
                if (response.SolutionModel) {
                    vm.Solution = response.SolutionModel;
                    vm.Image = (vm.Solution.Image ? frsApiUrl + vm.Solution.Image : '/app/img/image-default.png');
                    $('.SolutionImage').attr('src', vm.Image);
                    vm.update = true;
                    toaster.success("", "Data has been loaded successfully.");
                }
                vm.Tags = response.Tags;
                vm.Filters = response.Filters;
                vm.SolutionTypes = response.SolutionTypes;
                vm.SolutionOwners = response.SolutionOwners;

                if (vm.Solution) {
                    solutionId = vm.Solution.Id;

                    //Solution Type DropDown
                    if (vm.Solution.TypeId && vm.Solution.TypeId > 0) {
                        var solutiontype = vm.SolutionTypes.find(sol => sol.Id === response.SolutionModel.TypeId);
                        if (solutiontype)
                            vm.SolutionTypes.selected = solutiontype;
                        vm.SolutionTypes.required = true;
                    }

                    //Solution Owner DropDown
                    if (vm.Solution.OwnerId && vm.Solution.OwnerId > 0) {
                        var owner = $(vm.SolutionOwners).filter(function (index, sol) {
                            return sol.Id === response.SolutionModel.OwnerId;
                        });
                        if (owner.length > 0) {
                            vm.SolutionOwners.selected = owner[0];
                        }
                    }

                    //Tags multiselect
                    if (vm.Solution.TagIds) {
                        vm.multiple.Tags = vm.Solution.Tags.slice();
                    }

                    //Filter multiselect
                    if (vm.Solution.FilterIds) {
                        vm.multiple.Filters = vm.Solution.Filters.slice();
                    }

                    if (vm.Solution.Image)
                        $('.SolutionImage').attr('src', vm.Solution.Image);
                }
            } else {
                solutionId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });
        vm.clearSolutionTypes = function ($event) {
            $event.stopPropagation();
            vm.SolutionTypes.selected = null;
        };
        vm.clearSolutionOwners = function ($event) {
            $event.stopPropagation();
            vm.SolutionOwners.selected = null;
        };
    }
})();
