
app.controller('registrationController', function ($scope, userService, $location, $rootScope) {

    
    $scope.loginData = {};
    $scope.registerData = {};
    $rootScope.userdetails = {};
    var flag = false;
    

    var path = $location.path();
   

    $scope.loginUser = function () {

     userService.getUsers().then(function (results) {

            $scope.user = results.data.value;

                 angular.forEach($scope.user, function (value, key) {

               if (value.email == $scope.loginData.email && value.pwd == $scope.loginData.pwd) {
         
                      alert("LogIn Successful");
                      flag = true;
                       $rootScope.userdetails = value;
                       $location.path('/home');
               }
               else if (($scope.user.length - 1) == key && flag == false) {

                       $scope.loginfailed = true;
                       alert("LogIn Failed");
               }
               
            });

        }, function (error) {

            alert(error);
        });
    }

    $scope.register = function () {
        
        userService.createUsers($scope.registerData).then(function (results) {


            alert("Registration Successful");
            $location.path('/registration');
            $(".signin_form").css('opacity', '100');
            $(".signup_form").css('opacity', '0');

            $("#card").flip(false);
            return false;


        }, function (error) {

           alert("Registration Failed");



        });

    }

    $scope.flipfunction = function () {


        $().ready(function () {
            $("#card").flip({
                trigger: 'manual'
            });
        });


        $(".signup_link").click(function () {

            $(".signin_form").css('opacity', '0');
            $(".signup_form").css('opacity', '100');


            $("#card").flip(true);

            return false;
        });

        $("#unflip-btn").click(function () {

            $(".signin_form").css('opacity', '100');
            $(".signup_form").css('opacity', '0');

            $("#card").flip(false);

            return false;

        });
       
    }

    $scope.flipfunction();
 

});