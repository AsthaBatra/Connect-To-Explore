app.factory('userService', function ($http) {

  var userServicefactory = {};


   userServicefactory.getUsers = function (filter) {
       if (!filter)
           filter = "";
        return $http.get("http://localhost/ES.Api/OData/users" + filter);

    }

    userServicefactory.createUsers = function (model) {
        return $http.post("http://localhost/ES.Api/OData/users", model);
    }


    return userServicefactory;


});