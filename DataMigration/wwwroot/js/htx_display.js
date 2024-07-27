if (window.addEventListener) {
    window.addEventListener("scroll", function () {fix_sidemenu(); });
    window.addEventListener("resize", function () {fix_sidemenu(); });    
    window.addEventListener("touchmove", function () {fix_sidemenu(); });    
    window.addEventListener("load", function () {fix_sidemenu(); });
} else if (window.attachEvent) {
    window.attachEvent("onscroll", function () {fix_sidemenu(); });
    window.attachEvent("onresize", function () {fix_sidemenu(); });    
    window.attachEvent("ontouchmove", function () {fix_sidemenu(); });
    window.attachEvent("onload", function () {fix_sidemenu(); });
}


function openLeftnav() {
	$("#belowtopnav_wrapper").toggleClass("toggled");
	$("#pgfooter_wrapper").toggleClass("toggled");
}

function fix_sidemenu() {
	var w, top;
	top = scrolltop() ;
	w = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
	//document.getElementById("test").innerHTML = top + " " + w  ;

	
	if (top > 1) {
		document.getElementById("topnav_wrapper").style.position = "fixed";        
		document.getElementById("topnav_wrapper").style.top = "0";                
		document.getElementById("topnav_wrapper").style.padding = "0px";
		if (document.getElementById("leftmenu_wrapper")) {
		    document.getElementById("leftmenu_wrapper").style.top = "50px";
        }
        if (document.getElementById("leftFloater")) {
            document.getElementById("leftFloater").style.top = "135px";
        }
		$("#nav-logo").show();
		$("#pgfooter_wrapper").css("bottom", "-50px");
	    //document.getElementById("leftmenu_wrapper").style.top = "72px" ;
		//alert(document.getElementById("topnav_wrapper").style.height) ;
		//document.getElementById("belowtopnav_wrapper").style.position = "" ;
	} else {
	    document.getElementById("topnav_wrapper").style.position = "relative";
	    if (document.getElementById("leftmenu_wrapper")) {
	        document.getElementById("leftmenu_wrapper").style.top = "auto";
        }
        if (document.getElementById("leftFloater")) {
            document.getElementById("leftFloater").style.top = "215px";
        }
	    $("#nav-logo").hide();
	    $("#pgfooter_wrapper").css("bottom", "0");
	    //document.getElementById("nav-logo").style.display = "auto";
		//document.getElementById("belowtopnav_wrapper").style.position = "fixed" ;
		//document.getElementById("topnav_wrapper").style.padding = "5px";                
	}
 
	
	if (w < 500) {
		if (top > 1) {
			//document.getElementById("leftmenu_wrapper").style.top = "132px" ;
		} else {
			//document.getElementById("leftmenu_wrapper").style.top = "200px" ;
		}
		//document.getElementById("leftmenubar").style.width = "10px" ;
		//document.getElementById("leftmenubar").style.height = "10px" ;
		//document.getElementById("leftmenubar").style.padding = "0px" ;
	} else {
		//document.getElementById("leftmenubar").style.width = "200px" ;
		//document.getElementById("leftmenubar").style.height = "initial" ;
		//document.getElementById("leftmenubar").style.padding = "initial" ;
		
	}
	
	
}

  function scrolltop() {
    var top = 0;
    if (typeof(window.pageYOffset) == "number") {
        top = window.pageYOffset;
    } else if (document.body && document.body.scrollTop) {
        top = document.body.scrollTop;
    } else if (document.documentElement && document.documentElement.scrollTop) {
        top = document.documentElement.scrollTop;
    }
    return top;
  }

  function setSliderChecked(control) {

      if ($(control).is(":checked")) {
          $(control).val(true)
      } else {
          $(control).val(false)
      }

  }

function setSliderCheckedInt(control) {

    if ($(control).is(":checked")) {
        $(control).val(1)
    } else {
        $(control).val(0)
    }

}

