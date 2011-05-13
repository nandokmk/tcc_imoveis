// JavaScript Document

jQuery(document).ready(function(){

	 jQuery(window).scroll(function () { 
		jQuery('#back_to_top').show(500);
		 if (jQuery(window).scrollTop() == 0) {
			jQuery('#back_to_top').fadeOut(500);		 
		 }
    });
	 
	 $(".pretty_photo").prettyPhoto();
	 
});

//Functions

jQuery(function(){ //smoothscroll
	jQuery('.toc a, #back_to_top a').click(function() {
		if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') 
			&& location.hostname == this.hostname) {
				var $target = $(this.hash);
				$target = $target.length && $target || $('[name=' + this.hash.slice(1) +']');
			if ($target.length) {
				var targetOffset = $target.offset().top;
				$('html,body').animate({scrollTop: targetOffset}, 500);
				return false;
			}
		}
	});
});