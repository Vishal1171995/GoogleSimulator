jQuery(document).ready(function () {

    var sectionOffset = 50, subsOffset = 35;
    var regFail = $("#regFail").text();
    function slideReg() {
        $("section#register").slideDown();
        $('html,body').animate({
            scrollTop: $("section#register").offset().top
        },
            500);
    }
    $("#goToReg").click(function () { slideReg(); });
    if (regFail == "Did not Login") { slideReg(); }

    $(".scrollLink").click(function() {
        var id = $(this).attr("id");
        var boxHeight = $(".subs").innerHeight();
        var boxWidth = $(".subs").innerWidth();
        if (id == 'demoLink') {
            $('html,body').animate({
                scrollTop: $("#demo").offset().top},
                500);
        }
        if (id == 'pricingLink') {
            $('html,body').animate({
                scrollTop: $("#pricing").offset().top},
                500);               
        }
    });

    /* scroll Effects */
    $('.js--anim-1').waypoint(function(direction){
        $('.js--anim-1').addClass('animate');
    }, {
        offset: sectionOffset + '%'
    });
    
    $('.js--anim-2').waypoint(function(direction){
        $('.js--anim-2').addClass('animate slideIn');
    }, {
        offset: sectionOffset + '%'
    });
    
    $('.js--anim-3').waypoint(function(direction){
        $('.js--anim-3').addClass('animate');
    }, {
        offset: sectionOffset + '%'
    });
    
    $('.js--anim-4').waypoint(function(direction){
        $('.js--anim-4').addClass('animate bounce');
    }, {
        offset: subsOffset + '%'
    });
});