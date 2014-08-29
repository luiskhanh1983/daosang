function TopSlider(){
        var k1=0.5;
        var k2=0.6;
        var w0=jQuery(window).width();
        if(w0 > 1600) { k2=0.55}
        var w2= w0*k1;
        var w1= w0*k2;
        var w3= (w0-jQuery(".container").width())*0.5;
        var h1=w2/1.9;
        if(h1 < 235) {h1=235;}
	
		jQuery("#slider_top").css({"height":h1+"px"});
		jQuery("#slider_top").css({"width":w0+"px"});
		jQuery("#carousel1 li").css({"width":w0+"px"});
		jQuery(".overlap_widget_wrapper").css({"width":w0+"px"});

		jQuery("#slider_top a.callbacks_nav.next").css({"right":w3+"px","top":h1*0.5-jQuery("#slider_top a.callbacks_nav.next").height()*0.5+"px"});
		jQuery("#slider_top a.callbacks_nav.prev").css({"left":w3+"px","top":h1*0.5-jQuery("#slider_top a.callbacks_nav.next").height()*0.5+"px"});
		jQuery("#carousel1 .overlap_widget_wrapper .left_image .title").css({"left":w3+"px"});
		jQuery("#carousel1 .overlap_widget_wrapper .right_image .title").css({"right":w3+"px"});

		jQuery(".overlap_widget_wrapper .left_image").css({"width":w1+"px"});
        jQuery(".overlap_widget_wrapper .left_image .placeholder").css({"width":w2+"px"});
		jQuery(".overlap_widget_wrapper .right_image").css({"width":w1+"px"});
        jQuery(".overlap_widget_wrapper .right_image .placeholder").css({"width":w2+"px"});
		jQuery("#back-top").css({"bottom":jQuery("#footer").height()+150+"px"});

		var is_open = false;
        var z_index = 0;

        jQuery(".placeholder").mouseenter(function(){
            is_open = true;
            jQuery(this).parent().css({"zIndex":"999"});
            jQuery(this).stop().animate({
                "width":w1+"px"
            }, 550, 'easeOutQuad');
        });
            
        jQuery(".placeholder").mouseleave(function(){
            is_open = false;
            z_index++;
            jQuery(this).parent().css({"zIndex":z_index});
            jQuery(this).stop().animate({
                "width":w2+"px"
            }, 550, 'easeOutQuad');
        }); 
        
           
};

function showOptions(id){
    jQuery('#fancybox'+id).trigger('click');
}

function setAjaxData(data,iframe,flag,id){
    if(data.status == 'ERROR'){
        alert(data.message);
    }else{
       if(jQuery('.shopping_cart')){
            jQuery('.shopping_cart').replaceWith(data.sidebar);
        }
        if(jQuery('.nav_block_dropdown')){
            jQuery('.nav_block_dropdown').html(data.toplink);
        }
        jQuery.fancybox.close();
        if(flag)flyToCart(id);

        jQuery('#cartInfo').html(data.name+' was added to your cart').fadeIn();
        setTimeout(function(){
           jQuery('#cartInfo').fadeOut();
        },3000)

    }
}

function flyToCart(id){

    var productIDVal 	= id;

    var productX 		= jQuery("#productImageWrapID_" + productIDVal).offset().left;
    var productY 		= jQuery("#productImageWrapID_" + productIDVal).offset().top;

    var basketX 		= jQuery(".shopping_cart").offset().left;
    var basketY 		= jQuery(".shopping_cart").offset().top;

    var gotoX 			= basketX - productX;
    var gotoY 			= basketY - productY;

    var newImageWidth 	= jQuery("#productImageWrapID_" + productIDVal+" img").width() / 5;
    var newImageHeight	= jQuery("#productImageWrapID_" + productIDVal+" img").height() / 5;

    jQuery("#productImageWrapID_" + productIDVal + " img")
    .clone()
    .prependTo("#productImageWrapID_" + productIDVal)
    .css({'position' : 'absolute'})
    .animate({opacity: 0.4}, 100 )
    .animate({opacity: 0.1, marginLeft: gotoX, marginTop: gotoY, width: newImageWidth, height: newImageHeight}, 1200, function() {
        jQuery(this).remove();
    });

}






function setLocationAjax(url,id,home){
    url += 'isAjax/1';
    //url = url.replace("checkout/cart","ajax/index");
    jQuery('.ajax_loader'+id).show();
    if (typeof home != 'undefined')
    {
        
    } else
    {
        flyToCart(id);
    }

    try {
        jQuery.ajax( {
            url : url,
            dataType : 'json',
            success : function(data) {
                jQuery('.ajax_loader'+id).hide();

                setAjaxData(data,false);

            }
        });
    } catch (e) {
    }
}




set_reset_tab=function (tab) {
    jQuery('#bestsellers_activate,#specials_activate,#newproducts_activate').removeClass('active_slider');
    tab.addClass('active_slider');
}


jQuery(function(jQuery) {
    jQuery('#bestsellers_activate').click(function() {
        jQuery('#bestsellers_slider').show();
        jQuery('#newproducts_slider').hide();
        jQuery('#specials_slider').hide();
        set_reset_tab(jQuery(this));



    });

     jQuery('#nav li').hover(
            function(){ jQuery('>ul',this).addClass('shown-sub'); },
            function(){jQuery('>ul',this).removeClass('shown-sub');}
     );

    jQuery('#bestsellers_activate').click(function() {
        jQuery('#bestsellers_slider').show();
        jQuery('#newproducts_slider').hide();
        jQuery('#specials_slider').hide();
        set_reset_tab(jQuery(this));

    });

    jQuery('#specials_activate').click(function() {
        jQuery('#specials_slider').show();
        jQuery('#newproducts_slider').hide();
        jQuery('#bestsellers_slider').hide();

        set_reset_tab(jQuery(this));

    });

    jQuery('#newproducts_activate').click(function() {
        jQuery('#newproducts_slider').show();
        jQuery('#bestsellers_slider').hide();
        jQuery('#specials_slider').hide();

        set_reset_tab(jQuery(this));

    });




     jQuery('.fancybox').fancybox(
            {
               hideOnContentClick : true,
               width: 420,
               height:500,
               autoDimensions: true,
               type : 'iframe',
               showTitle: false,
               scrolling: 'yes',
              
            }
        );

    jQuery('#nav_block_head').click(function() {
        jQuery('.nav_block_dropdown').toggleClass('visible_on');
    });

    var old_q;
    old_q=parseFloat(jQuery('#qty').val());
    jQuery('.marker_qty_right').click(function(){
        jQuery('#qty').val(++old_q);
    });

    jQuery('.marker_qty_left').click(function(){

        if(old_q<2)old_q=1;
        jQuery('#qty').val(--old_q);
    });


    jQuery('#menu_block_head').click(function() {
        jQuery('.menu_block_dropdown').toggleClass('visible_on');
    });

	jQuery("#select1").selectbox();
	jQuery("#select2").selectbox();
	jQuery("#select3").selectbox();
    jQuery(".sixteen.columns.alpha div:first").show();



    jQuery(".megamenu").megamenu();

    if(!jQuery('#bestsellers_slider').length)
    {
        jQuery(".tabs div:eq(0)").remove();
    };

    if(!jQuery('#specials_slider').length)
    {
        jQuery("#pecials_activate").parent().remove();
    };

    if(!jQuery('#newproducts_slider').length)
    {
        jQuery("#newproducts_activate").parent().remove();
    };



    jQuery('.tabs > div:first a').addClass('active_slider');

    jQuery("iframe").each(function(){
        var ifr_source = jQuery(this).attr('src');
        var wmode = "wmode=transparent";
//        if(ifr_source.indexOf('?') != -1) {
//            var getQString = ifr_source.split('?');
//            var oldString = getQString[1];
//            var newString = getQString[0];
//            jQuery(this).attr('src',newString+'?'+wmode+'&'+oldString);
//        }
//else jQuery(this).attr('src',ifr_source+'?'+wmode);
});


		jQuery("#back-top").hide();
		jQuery(function () {
			jQuery(window).scroll(function () {
				if (jQuery(this).scrollTop() > 600) {
					jQuery('#back-top').fadeIn();
				} else {
					jQuery('#back-top').fadeOut();
				}
			});
			jQuery('#back-top a').click(function () {
				jQuery('body,html').animate({
					scrollTop: 0
				}, 400);
				return false;
			});
		});
									
			  jQuery("#carousel1").responsiveSlides({
				pager: false,
				nav: true,
				speed: 1000,
				auto: true,            
				timeout: 3600, 				
				namespace: "callbacks",
			  });
			  
			jQuery('#carousel').elastislide({
			easing		: 'easeInOutQuad',
			speed		: 1200
			});
			
			
			jQuery('#carousel_media').elastislide({
				imageW 		: 90,
				margin		: 10,	// image margin right
				border		: 0	// image border
			});
			
			
			jQuery('#carousel_bestsellers').elastislide({
			easing		: 'easeInOutQuad',
			speed		: 1200
			});
			
			jQuery('#carousel_specials').elastislide({
			easing		: 'easeInOutQuad',
			speed		: 1200
			});
			
			jQuery('#carousel_newproducts').elastislide({
			easing		: 'easeInOutQuad',
			speed		: 1200
			});
			
			  
			jQuery('#slider_top a.callbacks_nav').hover(function() {
			jQuery(this).stop().animate({opacity:1.0},500);
			},
			function() {
			jQuery(this).stop().animate({opacity:0.6},500);
			});

			jQuery(" #shopping_cart_mini a.button-delete").live('click',function() {
				jQuery("#shopping_cart_mini").fadeToggle(300, "linear");
			});
			jQuery(".shopping_cart a.open").live('click',function() {
				jQuery("#shopping_cart_mini").fadeToggle(300, "linear");
			});
			
			jQuery(".product").hover(function() {
				jQuery(this).find(".roll_over_img").fadeToggle("fast", "linear");
				jQuery(this).find(".product-image-wrapper-hover").fadeToggle("fast", "linear");
			});

            jQuery(".product-list-wrapper").hover(function() {
                jQuery(this).find(".roll_over_img").fadeToggle("fast", "linear");
            });
			
			
			
			
			
			
			
			jQuery(".product").hover(function() {
				jQuery(this).find(".wrapper-hover").animate({ backgroundColor: "#444444" }, 500);
			},function() {
				jQuery(this).find(".wrapper-hover").animate({ backgroundColor: "#fff" }, 500);
			});
			
			jQuery(".product").hover(function() {
				jQuery(this).find(".wrapper-hover-hidden ").fadeToggle(500, "linear");
			});
			
			jQuery(".product").hover(function() {
				jQuery(this).find(".wrapper-hover span").animate({color:"#fff"}, 500);
				jQuery(this).find(".wrapper-hover a").animate({color:"#fff"}, 500);
			},function() {
				jQuery(this).find(".wrapper-hover span").animate({color:"#222"}, 500);
				jQuery(this).find(".wrapper-hover a").animate({color:"#222"}, 500);
			});
			
			
			
			
			jQuery('#back-top a').hover(function() {
			jQuery(this).stop().animate({opacity:1.0},500);
			},
			function() {
			jQuery(this).stop().animate({opacity:0.4},500);
			});	});





	
jQuery(document).ready(TopSlider);
jQuery(window).resize(TopSlider);

//jQuery.noConflict();


jQuery(function () {
    $("#tabs").tabs();
})