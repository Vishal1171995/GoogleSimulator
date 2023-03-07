$(window).load(function () {

    $('.dropdown-menu').click(function () {

        $('.top_menu').slideToggle();

    });
});

window.onresize = function (event) {

    if ($(window).width() > 800) {
        $('.top_menu').show();
    }
    else {
        $('.top_menu').hide();
    }
};