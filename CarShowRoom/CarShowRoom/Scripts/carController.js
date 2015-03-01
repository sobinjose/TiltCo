// Creating a module
var app = angular.module("app", ["ngRoute"])

// Creating a factory from the module
app.factory('carFactory', function ($http) {
    return {
        getFormData: function (callback) {
            $http.get('api/Car').success(callback);
        }
    }
})

//Angular routing 
//To be:check and fix
app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.when('/Index', {
        templateUrl: 'Home/Index',
        controller: 'carController'
    });
    $routeProvider.when('/AddCarModel', {
        templateUrl: 'Home/AddCarModel',
        controller: 'carController'
    });

    $routeProvider.otherwise({
        redirectTo: '/'
    });
    // $locationProvider.html5Mode(true);
    $locationProvider.html5Mode(true).hashPrefix('!')
}]);

// Creating a controller from the module
app.controller("carController", function ($scope, carFactory, $http) {
    getFormData();

    function getFormData() {
        carFactory.getFormData(function (results) {
            $scope.carMakes = results.carMakes;
            $scope.carModels = results.carModels;
        })
    }
    //Get all car makes
    $http.get('/api/Car/GetAll').success(function (list) {
        $scope.makes = list.makes;
    })
    $scope.changeValue = function changeValue(value) {
        $http.get('/api/Car/' + value.ID).success(function (data) {
            $scope.carModels = data;
        });
    };

    //Find ID from Car Make
    $scope.findIdFromMake = function findIdFromMake(value) {
        $scope.newmodel.make_id = value.ID;
    };

    //Add new car model
    $scope.Save = function () {
        var defaultForm = {
            make_id: "",
            make: "",
            model: ""
        };
        $http.post('/api/Car/', this.newmodel).success(function (data) {
            alert("New car model added !!!");
            $scope.carModels.push(data);
            $scope.NewModel.$setPristine();
            $scope.newmodel = defaultForm;
        }).error(function (data) {
            $scope.error = "An Error has occured when adding new car model !!! " + data;
        });
    }
})