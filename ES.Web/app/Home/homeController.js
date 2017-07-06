
app.controller('homeController', function ($scope, $rootScope) {

    $('.flip').hover(function () {
        $(this).find('.card').toggleClass('flipped');

    });

   

});