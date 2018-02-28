	
	// TRIGGER ANIMATIONS
	// http://www.oxygenna.com/tutorials/scroll-animations-using-waypoints-js-animate-css 
	$(window).bind('load resize', function(){
		
		function onScrollInit( items, trigger ) {
		
			items.each( function() {
				var osElement = $(this),
				osAnimationClass = osElement.attr('data-os-animation'),
				osAnimationDelay = osElement.attr('data-os-animation-delay');

				osElement.css({
					'-webkit-animation-delay':  osAnimationDelay,
					'-moz-animation-delay':     osAnimationDelay,
					'-ms-animation-delay':     	osAnimationDelay,
					'animation-delay':          osAnimationDelay
				});

				var osTrigger = ( trigger ) ? trigger : osElement;

				osTrigger.waypoint(function() {
					osElement.addClass('animated').addClass(osAnimationClass);
					$('.slick-slide .os-animation').removeClass('fadeInUp animated');
					$('.slick-active .os-animation').addClass('fadeInUp animated');
				},{
					//triggerOnce: true,
					offset: '90%'
				});
			});
		
		}
		onScrollInit( $('.os-animation') );
	
	});
	
	$(document).ready(function() {
				
		$(".item:nth-child(odd), ul li:nth-child(odd), dl dt:nth-child(odd), dl dd:nth-child(odd), tr:nth-child(odd), .tables .body .item:nth-child(odd)").addClass("odd");
		$(".item:nth-child(even), ul li:nth-child(even), dl dt:nth-child(even), dl dd:nth-child(even), tr:nth-child(even), .tables .body .item:nth-child(even)").addClass("even");		

		// DROP DOWN - CLICK & HOVER - MAIN NAV
		$(".navigation nav.main ul li.has-child").hover(function(){ 
			$(this).toggleClass("hover"); 
			$(this).toggleClass("hover"); 
		});
		//$(".navigation nav.main ul ul").hide();
		$(".navigation nav.main ul li > i").click(function(){
			if ($(".navigation nav.main ul li > i").length ) { 
				$(this).parent().toggleClass("open");
				$(this).parent().siblings().removeClass("open");
			}
			else{
				$(this).parent().toggleClass("open");
			}
		});
		 $("html").click(function() {
			$(".navigation nav.main ul li.open").removeClass("open");
		 });
		$(".navigation nav.main ul li > i").click(function(e){
			e.stopPropagation(); 
		});
		
		// EXPAND MOBILE NAVIVAGTION  
		$("header a.expand").click(function(){
			if ($(".navigation .reveal").length ) { 
				$("header a.expand").toggleClass('active');
				$("html").toggleClass('reveal-out');
			}
			else{
				$("header a.expand").toggleClass('active');
				$("html").toggleClass('reveal-out');
			}
		});

		// SLIDESHOWS
      	$(".banner .slides").slick({
			arrows: true,
			dots: true,
			infinite: true,
			speed: 600,
			fade: true,
  			adaptiveHeight: true,
			prevArrow: '<div class="slick-prev"><i class="ion-chevron-left"></i>',
			nextArrow: '<div class="slick-next"><i class="ion-chevron-right"></i>',
				responsive: [
				{
					breakpoint: 767,
					settings: {
						arrows: false
					}
				}
			]
		});
		
		// SLICK CAROUSEL ANIMATE EACH SLIDE
		$('.banner .slides').on('afterChange', function(event, slick, currentSlide){
		    $('.slick-active .os-animation').removeClass('fadeInUp animated');
		    $('.slick-active .os-animation').addClass('fadeInUp animated');
		});
		$('.banner .slides').on('beforeChange', function(event, slick, currentSlide){
		    $('.slick-active .os-animation').removeClass('fadeInUp animated');
		});	
		
      	$(".slideshow").slick({
			arrows: true,
			dots: false,
			infinite: true,
			speed: 600,
			fade: false,
  			adaptiveHeight: true,
			prevArrow: '<div class="slick-prev"><i class="ion-chevron-left"></i>',
			nextArrow: '<div class="slick-next"><i class="ion-chevron-right"></i>'
		});
		
      	$(".image-carousel .slides").slick({
			arrows: true,
			prevArrow: '<div class="slick-prev"><i class="ion-chevron-left"></i>',
			nextArrow: '<div class="slick-next"><i class="ion-chevron-right"></i>',
			dots: true,
			infinite: true,
			speed: 600,
			easing: 'linear',
			slidesToShow: 4,
			slidesToScroll: 4,
				responsive: [
				{
					breakpoint: 1200,
					settings: {
						slidesToShow: 3,
						slidesToScroll: 3,
					}
				},
				{
					breakpoint: 768,
					settings: {
						slidesToShow: 2,
						slidesToScroll: 2
					}
				},
				{
					breakpoint: 480,
					settings: {
						slidesToShow: 1,
						slidesToScroll: 1
					}
				}
			]
		});   	
		
		// SCROLL PROMPT LINK
		$('.advanced-page .scroll-prompt').click(function() {
			
  			var target;
  			$("section").not($(this).parent().parent().parent().parent().parent()).each(function(i, element) {
	
  				target = ($(element).offset().top - 60);
  				if (target - 10 > $(document).scrollTop()) {
  					    return false; // break
  					}
  				});
  				$("html, body").animate({
  					scrollTop: target
  				}, 600);

		});
		$('.standard-page .scroll-prompt').click(function() {
			
  			var target;
  			$("section").not($(this).parent().parent()).each(function(i, element) {
	
  				target = ($(element).offset().top - 60);
  				if (target - 10 > $(document).scrollTop()) {
  					    return false; // break
  					}
  				});
  				$("html, body").animate({
  					scrollTop: target
  				}, 600);

		});
		
		// BACK TO TOP
		if ( ($(window).height() + 100) < $(document).height() ) {
			
	        $('#top-link-block').addClass('show').affix({
	            // how far to scroll down before link "slides" into view
	            offset: {top:140}
	        });
	
		}

		// PARALLAX
		$('.item.parallax').parallax({
			speed: -0.25
		});
		
		// MATCH HEIGHTS
		// LISTING - GRID
		$(".listing.grid .item").matchHeight({
			byRow: true
		});
		$(".listing.grid .text").matchHeight({
			byRow: true
		});
		
		// PROMO PODS
		$(".promo-pods .item").matchHeight({
			byRow: true
		});
		$(".promo-pods .text").matchHeight({
			byRow: true
		});
		
		// TEXT WITH VIDEO OR IMAGE
		$(".apc.text-with-image-or-video .image, .apc.text-with-image-or-video .item").matchHeight({
			byRow: true
		});
		
		// WIDE COLUMN SITE WIDE POD
		$(".swp > .col-xs-12").matchHeight({
			byRow: true
		});
		
	});
	
	// OPEN MULTIPLE COLLAPSE PANELS
	// http://www.bootply.com/90JfjI2Q7n
	$(function () {	
				
		$('a[data-toggle="collapse"]').on('click',function(){
			var objectID=$(this).attr('href');
			if($(objectID).hasClass('in'))
			{
				$(objectID).collapse('hide');
			}
			else{
				$(objectID).collapse('show');
			}
		});
		
		// EXPAND ALL
		$('#expandAll').on('click',function(){
			$('a[data-toggle="collapse"]').each(function(){
				var objectID=$(this).attr('href');
				if($(objectID).hasClass('in')===false)
				{
					$(objectID).collapse('show');
				}
			});
		});
		
		// COLLAPSE ALL
		$('#collapseAll').on('click',function(){
			$('a[data-toggle="collapse"]').each(function(){
				var objectID=$(this).attr('href');
				$(objectID).collapse('hide');
			});
		});
	});
	
	// LIGTHBOX
	$(document).delegate('*[data-toggle="lightbox"]', 'click', function(event) {
    	event.preventDefault();
        $(this).ekkoLightbox();
	}); 

	// HEADER SCROLLING
	var didScroll;
	var lastScrollTop = 0;
	var delta = 160;
	var navbarHeight = $('header').outerHeight();

	$(window).scroll(function(event){
	    didScroll = true;
	});

	setInterval(function() {
	    if (didScroll) {
	        hasScrolled();
	        didScroll = false;
	    }
	}, 160);

	function hasScrolled() {
	    var st = $(this).scrollTop();
    
	    // Make sure they scroll more than delta
	    if(Math.abs(lastScrollTop - st) <= delta)
	        return;
	
	    if (st > lastScrollTop && st > navbarHeight){
	        // Scroll Down
	        $('html').removeClass('nav-down').addClass('nav-up');
	    } else {
	        // Scroll Up
	        $('html').addClass('nav-down').removeClass('nav-up');
	    }
    
	    lastScrollTop = st;
	}
	
	// FIXED HEADER
    $(window).scroll(function() {
	
        var scroll = $(window).scrollTop();

        if (scroll >= 160) {
            $("html").removeClass("reached-top");
        } else {
            $("html").addClass("reached-top");
        }

    });	

