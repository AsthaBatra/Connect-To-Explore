
app.controller('contactController',function ($scope){


    function myMap() {
        var mapProp = {
            center: new google.maps.LatLng(21.265602, 81.660922),
            zoom: 5,
        };
        var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(21.265602, 81.660922),
            map: map,
            animation: google.maps.Animation.Drop
        });

        var infowindow = new google.maps.InfoWindow({
            content: "Beside Sankalp School, Kapa, Mowa, Raipur, Chhattisgarh, India"
        });

        google.maps.event.addListener(marker, 'click', function () {
            infowindow.open(map, marker);
        });
      


    }

});