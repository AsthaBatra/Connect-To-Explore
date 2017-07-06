app.factory('connectionService', function ($http) {

    var connectionServiceFactory = {};


    connectionServiceFactory.createConnection = function (modal) {

        return $http.post("http://localhost/ES.Api/OData/connections",modal);

    }

    return connectionServiceFactory;


});