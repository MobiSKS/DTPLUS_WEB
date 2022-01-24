// JavaScript Document

	$(document).ready(function(){
		$('.drawermenu').drawermenu( {
			speed: 300,
			position: 'left'
		});
		
		
		if (window.outerWidth() < 991) {
			$('#sidebar').slideReveal({
				trigger: $("#trigger"),
				push: false,
				top:87,
				width:225,
				overlay: false
			});
		}
		
	})

function playVideo(e){
	$('video').trigger('pause');
	$('.play_btn').fadeIn();
	$(e).fadeOut();
	$(e).next('video').trigger('play');
}

 topMenu = $("#mainNav"),
 topMenuHeight = topMenu.outerHeight()+1,
 // All list items
 menuItems = topMenu.find("a:not(a[href='#'])"),
 // Anchors corresponding to menu items
 scrollItems = menuItems.map(function(){
   var item = $($(this).attr("href"));
    if (item.length) { return item; }
 });

// Bind click handler to menu items
// so we can get a fancy scroll animation
var count = 0;
menuItems.click(function(e){
  var href = $(this).attr("href"),
      offsetTop = href === "#" ? 0 : $(href).offset().top-topMenuHeight+1;
	
	
	if(count > 0) {
		$('html, body').stop().animate({ 
			  scrollTop: offsetTop - 180
		  }, 850);
		
	}
	else {
		$('html, body').stop().animate({ 
			  scrollTop: offsetTop - 300
		  }, 850);
	}
	count++;
	
  
  e.preventDefault();
});

$(window).on("scroll", function(){
	if($(window).scrollTop()> 50){
		$("nav").addClass("header_sticky")
	}
	else {
		$("nav").removeClass("header_sticky")
	}		
	
	if($(window).scrollTop()> 350){
		$("#mainNav").addClass("scroll_sticky")
	}
	else {
		$("#mainNav").removeClass("scroll_sticky")
	}		
	
	//console.log(offsetTop);
	//$(".scroll_spy a[href='"+offsetTop+"']").addClass("active");
})

$('.brand-slider').owlCarousel({
		items:5,
		loop:true,
		margin:10,
		merge:true,
		autoplayTimeout:3000,
		autoplaySpeed:700,
		autoplay:true,
		responsive:{
			320:{
				mergeFit:true,
				items:2,
			},
			480:{
				mergeFit:true,
				items:3,
			},
			678:{
				mergeFit:true,
				items:4,
			},
			1000:{
				mergeFit:false
			}
		}
	});
$('.aids-slider').owlCarousel({
		items:3,
		loop:true,
		margin:10,
		merge:true,
		autoplayTimeout:3000,
		autoplaySpeed:700,
		autoplay:true,
		dots:false,
		responsive:{
			320:{
				mergeFit:true,
				items:1,
			},
			480:{
				mergeFit:true,
				items:1,
			},
			678:{
				mergeFit:true,
				items:3,
			},
			1000:{
				mergeFit:false
			}
		}
	});		
$('.brand-slider33').owlCarousel({
		items:6,
		loop:true,
		margin:10,
		merge:true,
		autoplayTimeout:3000,
		autoplaySpeed:700,
		autoplay:true,
		responsive:{
			320:{
				mergeFit:true,
				items:3,
			},
			480:{
				mergeFit:true,
				items:3,
			},
			678:{
				mergeFit:true,
				items:4,
			},
			1000:{
				mergeFit:false
			}
		}
	});
$('.speciality_slider').owlCarousel({
			items:3,
			loop:false,
			margin:10,
			merge:true,
			autoplayTimeout:3000,
			autoplaySpeed:700,
			dots:false,
			nav:true,
			navText: ["<",">"],
			autoplay:true,
			responsive:{
				320:{
					mergeFit:true,
					items:1,
					margin:0,
				},
				480:{
					mergeFit:true,
					items:1,
				},
				678:{
					mergeFit:true,
					items:2,
				},
				1000:{
					mergeFit:false
				}
			}
		});
$('.article_slider').owlCarousel({
			items:3,
			loop:false,
			margin:10,
			merge:true,
			autoplayTimeout:3000,
			autoplaySpeed:700,
			dots:false,
			nav:true,
			navText: ["<",">"],
			autoplay:true,
			responsive:{
				320:{
					mergeFit:true,
					items:1,
					margin:10,
				},
				480:{
					mergeFit:true,
					items:1,
				},
				678:{
					mergeFit:true,
					items:2,
				},
				1000:{
					mergeFit:false
				}
			}
		});
$('.testimonial_slider').owlCarousel({
			items:3,
			loop:false,
			margin:10,
			merge:true,
			autoplayTimeout:3000,
			autoplaySpeed:700,
			dots:false,
			nav:true,
			navText: ["<",">"],
			autoplay:false,
			responsive:{
				320:{
					mergeFit:true,
					items:1,
					margin:0,
				},
				480:{
					mergeFit:true,
					items:1,
				},
				678:{
					mergeFit:true,
					items:2,
				},
				1000:{
					mergeFit:false
				}
			}
		});
$('.condition_slider').owlCarousel({
			items:5,
			loop:false,
			margin:2,
			merge:true,
			autoplayTimeout:3000,
			autoplaySpeed:700,
			dots:false,
			nav:true,
			navText: ["<",">"],
			autoplay:false,
			responsive:{
				320:{
					mergeFit:true,
					items:1,
					margin:0,
				},
				480:{
					mergeFit:true,
					items:3,
				},
				678:{
					mergeFit:true,
					items:4,
				},
				1000:{
					mergeFit:false
				}
			}
		});	
$('.main-slider').owlCarousel({
			items:1,
			loop:true,
			margin:0,
			merge:true,
			autoplayTimeout:3000,
			autoplaySpeed:700,
			autoplay:false,
			dots:true,
			nav:false,
			navText: ["<img src='assets/images/arrow_left.png' class='img-fluid'>","<img src='assets/images/arrow_right.png' class='img-fluid'>"]
	});
$('.analytics-slider').owlCarousel({
			items:1,
			loop:true,
			margin:10,
			merge:true,
			nav:false,
			autoplayTimeout:3000,
			autoplaySpeed:700,
			autoplay:true,
			responsive:{				
				600:{
					items:1,
					margin:10,
				},				
				0:{
					items:1,
					mergeFit:false
				}
			}		
	});
$('.live_on_slider').owlCarousel({
			items:2,
			loop:true,
			margin:10,
			merge:true,
			nav:false,
			autoplayTimeout:3000,
			autoplaySpeed:700,
			autoplay:true,
			responsive:{				
				600:{
					items:2,
					margin:10,
				},				
				0:{
					items:1,
					mergeFit:false
				}
			}		
	});
$('.reviews-slider').owlCarousel({
			items:1,
			loop:true,
			margin:10,
			merge:true,
			nav:false,
			autoplayTimeout:3000,
			autoplaySpeed:700,
			autoplay:true,
			responsive:{				
				0:{
					items:1,
					margin:0,
				},
				540:{
					items:1,
					margin:0,
				},				
				1000:{
					items:1,
					nav:false,
					margin:10,
					mergeFit:false
				}
			}		
	});
				
		$("input[name=prob_faced]").on("change", function(){
			if ($("input[name=prob_faced]:checked").val()=='others'){
				$("#other_problem").fadeIn();
			}
			else {
				$("#other_problem").fadeOut();
			}
		 })

jQuery(document).ready(function($){
		$('.question').on('click', function(){
			if($(this).hasClass('active')){
				$('.question').removeClass('active');
				$('.arrow').removeClass('arrow-active');
				$('.answer').slideUp();
			}
			else{
				$('.question').removeClass('active');
				$('.arrow').removeClass('arrow-active');
				$('.answer').slideUp();
				$(this).addClass('active');
				$(this).children('.arrow').addClass('arrow-active');
				$(this).children('.answer').slideToggle('slow');
			}
		});
	});


var x, i, j, l, ll, selElmnt, a, b, c;
/* Look for any elements with the class "custom-select": */
x = document.getElementsByClassName("search_filter_btn");
l = x.length;
for (i = 0; i < l; i++) {
  selElmnt = x[i].getElementsByTagName("select")[0];
  ll = selElmnt.length;
  /* For each element, create a new DIV that will act as the selected item: */
  a = document.createElement("DIV");
  a.setAttribute("class", "select-selected");
  a.innerHTML = selElmnt.options[selElmnt.selectedIndex].innerHTML;
  x[i].appendChild(a);
  /* For each element, create a new DIV that will contain the option list: */
  b = document.createElement("DIV");
  b.setAttribute("class", "select-items select-hide");
  for (j = 1; j < ll; j++) {
    /* For each option in the original select element,
    create a new DIV that will act as an option item: */
    c = document.createElement("DIV");
    c.innerHTML = selElmnt.options[j].innerHTML;
    c.addEventListener("click", function(e) {
        /* When an item is clicked, update the original select box,
        and the selected item: */
        var y, i, k, s, h, sl, yl;
        s = this.parentNode.parentNode.getElementsByTagName("select")[0];
        sl = s.length;
        h = this.parentNode.previousSibling;
        for (i = 0; i < sl; i++) {
          if (s.options[i].innerHTML == this.innerHTML) {
            s.selectedIndex = i;
            h.innerHTML = this.innerHTML;
            y = this.parentNode.getElementsByClassName("same-as-selected");
            yl = y.length;
            for (k = 0; k < yl; k++) {
              y[k].removeAttribute("class");
            }
            this.setAttribute("class", "same-as-selected");
            break;
          }
        }
        h.click();
    });
    b.appendChild(c);
  }
  x[i].appendChild(b);
  a.addEventListener("click", function(e) {
    /* When the select box is clicked, close any other select boxes,
    and open/close the current select box: */
    e.stopPropagation();
    closeAllSelect(this);
    this.nextSibling.classList.toggle("select-hide");
    this.classList.toggle("select-arrow-active");
  });
}

function closeAllSelect(elmnt) {
  /* A function that will close all select boxes in the document,
  except the current select box: */
  var x, y, i, xl, yl, arrNo = [];
  x = document.getElementsByClassName("select-items");
  y = document.getElementsByClassName("select-selected");
  xl = x.length;
  yl = y.length;
  for (i = 0; i < yl; i++) {
    if (elmnt == y[i]) {
      arrNo.push(i)
    } else {
      y[i].classList.remove("select-arrow-active");
    }
  }
  for (i = 0; i < xl; i++) {
    if (arrNo.indexOf(i)) {
      x[i].classList.add("select-hide");
    }
  }
}

/* If the user clicks anywhere outside the select box,
then close all select boxes: */
document.addEventListener("click", closeAllSelect);

//
//$(".top_nav li").on('click', 'a[href^="#"]:not(.primary_btn)', function (event) {
//	$(".top_nav li").removeClass("active");
//	$(this).parent("li").addClass("active")
//	//console.log(event.target.attributes.href);
//    event.preventDefault();
//    $('html, body').animate({
//        scrollTop: $($.attr(this, 'href')).offset().top - 80
//    }, 1000);
//	$("#navbar-collapse").collapse('hide');
//});


function Validate(){
	
	 if (document.register_form.username.value == "") {
		document.getElementById("username_error").innerHTML="This information is required";
		 document.register_form.username.focus;
		return (false);
	} 
	else {
		document.getElementById("username_error").innerHTML="";
	}
	
	 if (document.register_form.password.value == "") {
		document.getElementById("pass_error").innerHTML="This information is required";
		 document.register_form.password.focus;
		return (false);
	} 
	else {
		document.getElementById("pass_error").innerHTML="";
	}

	 if (document.register_form.captcha.value == "") {
		document.getElementById("captcha_error").innerHTML="This information is required";
		 document.register_form.captcha.focus;
		return (false);
	} 
	else {
		document.getElementById("captcha_error").innerHTML="";
	}

}


