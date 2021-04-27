
 /* jQuery Pre loader
  -----------------------------------------------*/
$(window).load(function(){
    $('.preloader').fadeOut(1000); // set duration in brackets    
});


/* HTML document is loaded. DOM is ready. 
-------------------------------------------*/
$(document).ready(function() {

  /* template navigation
  -----------------------------------------------*/
 $('.main-navigation').onePageNav({
        scrollThreshold: 0.2, // Adjust if Navigation highlights too early or too late
        scrollOffset: 75, //Height of Navigation Bar
        filter: ':not(.external)',
        changeHash: true
    }); 

    /* Navigation visible on Scroll */
    mainNav();
    $(window).scroll(function () {
        mainNav();
    });

    function mainNav() {
        var top = (document.documentElement && document.documentElement.scrollTop) || document.body.scrollTop;
        if (top > 40) $('.sticky-navigation').stop().animate({
            "opacity": '1',
            "top": '0'
        });
        else $('.sticky-navigation').stop().animate({
            "opacity": '0',
            "top": '-75'
        });
    }
    

   /* Hide mobile menu after clicking on a link
    -----------------------------------------------*/
    $('.navbar-collapse a').click(function(){
        $(".navbar-collapse").collapse('hide');
    });


  /*  smoothscroll
  ----------------------------------------------*/
   $(function() {
        $('.navbar-default a, #home a, #overview a').bind('click', function(event) {
            var $anchor = $(this);
            $('html, body').stop().animate({
                scrollTop: $($anchor.attr('href')).offset().top - 49
            }, 1000);
            event.preventDefault();
        });
    });


 /* Parallax section
    -----------------------------------------------*/
  function initParallax() {
    $('#home').parallax("100%",0.1);
    $('#overview').parallax("100%", 0.3);
    $('#trainer').parallax("100%", 0.2);
    $('#loginForm').parallax("100%", 0.3);
    $('#blog').parallax("100%", 0.1);
    

  }
  initParallax();


   /* home slider section
  -----------------------------------------------*/
  $(function () {
      jQuery(document).ready(function () {
    $('#home').backstretch([
        "../Content/images/Healthy-fitness-and-fruit-concept-with-Exercise-Equipment-and-fruit-lose-weight-on-wooden-background.jpg",
        "../Content/images/blog-thumb.jpg",
        "../Content/images/slider2.jpg",

        ],  {duration: 2000, fade: 750});
    });
  })


  /* wow
  -------------------------------*/
  new WOW({ mobile: false }).init();

  });

