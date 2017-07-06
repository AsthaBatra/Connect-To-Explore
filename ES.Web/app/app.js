
var app = angular.module('myApp', ['ngRoute']);



app.config(function ($routeProvider) {
    //start web routes

    $routeProvider.when("/home",
    {
        controller: "homeController",
        templateUrl: "Home/home.html"
    });


    $routeProvider.when("/registration",
    {
        controller: "registrationController",
        templateUrl: "Registration/registration.html"
    });


    $routeProvider.when("/myprofile",
    {
        controller: "myprofileController",
        templateUrl: "MyProfile/myprofile.html"
    });


    $routeProvider.when("/myconnections",
    {
        controller: "myconnectionsController",
        templateUrl: "MyConnections/myconnections.html"
    });

    $routeProvider.when("/contact",
   {
       controller: "contactController",
       templateUrl: "Contact/contact.html"
   });

    $routeProvider.when("/aboutus",
  {
      controller: "aboutusController",
      templateUrl: "Aboutus/aboutus.html"
  });

    $routeProvider.otherwise({

        redirectTo: "/home"
    });

    //End web routes 


});