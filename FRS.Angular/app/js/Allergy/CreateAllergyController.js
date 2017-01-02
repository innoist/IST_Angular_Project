//Create MetaData
(function () {

    var core = angular.module('app.core');
     //ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('CreateAllergyController', CreateAllergyController);

    CreateAllergyController.$inject = ['$state', '$stateParams', '$rootScope', 'ReferenceDataService', 'SweetAlert', 'toaster'];

    function CreateAllergyController($state, $stateParams, $rootScope, AllergyService, SweetAlert, toaster) {
        var vm = this;
        var allergyId = 0;
        vm.update = false;
        vm.submitted = false;
        vm.Allergy = {};

        //Api Url
        AllergyService.url = "/api/Allergy/";
        vm.Allergy = {};
        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };
        //#region Post Data
        vm.save = function (isNew) {
            vm.submitted = true;
            if (vm.formValidate.$valid) {
                console.log('Submitted!!');
            } else {
                toaster.pop("error", "Error", "Fields are required");
                return false;
            }

            vm.Allergy.Title = vm.Allergy.Title.trim();
            AllergyService.save(vm.Allergy, onSuccess, onError);
            function onSuccess(response) {
                if (response.data === true) {
                    toaster.pop("success", "Notification", "Data has been saved successfully.");
                    if (isNew) {
                        //reseting form
                        vm.formValidate.$setPristine();
                        vm.submitted = false;
                        allergyId = 0;
                        vm.Allergy = {};
                        $state.go('app.CreateAllergy');
                    }
                    if (!isNew) {
                        $state.go('app.Allergy');
                    }
                } else {
                    toaster.error("Notification", "Allergy with this name already exists.");
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
                $state.go('app.Allergy');
            } else {
                SweetAlert.swal(null, function (isConfirm) {
                    if (isConfirm) {
                        $state.go('app.Allergy');
                    }
                });
            }
        }

        if ($stateParams.Id === "") {
            vm.Allergy.IsActive = true;
            vm.edit = true;
            return;
        }
            

        allergyId = $stateParams.Id;
        AllergyService.loadById(allergyId, function (response) {
            if (response) {
                vm.update = true;
                vm.Allergy = response;
                allergyId = response.AllergyId;
                toaster.success("", "Data has been loaded successfully.");
            } else {
                allergyId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });
    }
})();