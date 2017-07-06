app.controller('myconnectionsController', function ($scope, userService, $rootScope, connectionService) {


    $scope.searchUser = function () {

        userService.getUsers().then(function (results) {

            $scope.users = results.data.value;

            //angular.forEach($scope.user, function (value, key) {

            //    if (value.email != $scope.loginData.email) {

            //        alert("can send request!");
                    
                  
            //    }
            //    else {

            //        alert("Cannot sent request");
            //    }

            //});

        }, function (error) {

            alert(error);
        });
    }

    $scope.searchUser();
    

    $scope.addUser = function () {

        $scope.connectionData = {
            sender_id: $rootScope.userdetails.id
           
        }

        connectionService.createConnection($scope.connectionData).then(function (results) {

            alert("Connection added successfully");

        }, function (error) {

            alert(error);
        });
    }



});