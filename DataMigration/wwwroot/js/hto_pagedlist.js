
$(document).ready(function () {

    fixSakura();

    $("#analysislink").on("click", function () { openanalysis(); return false; });

});




function fixSakura() {

    $(".htx-ilist-pagectrl").find("a").each(function () {
        var url = $(this).attr("href");
        $(this).attr("href", url.replace("%2520", "+"));
    });

}

function movePagers() {

    ///Clear out the old Pager controls before transitioning 
    ///the new Pager Controls returned by the Ajax refresh.
    $(".htx-ilist-pager-parent").each(function () {
        $(this).find(".pagination").remove();
    });

    var pager = $("#pagerTemplate").find(".pagination");

    if (pager.length == 1) {

        var totalCount = $(pager).data("totalcount");
        $(".htx-ilist-recordcount").find(".badge").html(totalCount);

        var totalAmount = $(pager).data("totalamount");
        if (typeof totalAmount != "undefined") {
            let formatting_options = { style: 'currency', currency: 'USD', minimumFractionDigits: 2 };
            let dollarString = new Intl.NumberFormat("en-US", formatting_options);
            let finalString = dollarString.format(totalAmount);

            $(".htx-ilist-totalamount").find(".badge").html(finalString);

        }


        $(pager).find("a").each(function () {
            var url = $(this).attr("href");
            $(this).attr("href", url.replace("%2520", "+"));
        });

        $(".htx-ilist-pager-parent").each(function () {
            $(this).append(pager) ;
        });

    }

    paintControls("#htxAjaxList");

    if ($("#pagerTemplate").data("openfilter") == "in") {
        $(".htx-ilist-pagectrl").find(".glyphicons-filter").addClass("in");
    } else {
        $(".htx-ilist-pagectrl").find(".glyphicons-filter").removeClass("in");
    }

    $(".htx-ilist-header").find(".fa-spinner").hide();



}

function beginPager() {

    //$(".htx-ilist-header").find(".fa-spinner").show()  THIS IS HANDLED BY THE setting-link-attr-data-ajax-loading="#htoajaxloadspinner" ATTRIBUTE;
    console.log("beginPager() called...");

}


function moveIListHeader() {

    //Move the IList Header up below the Page Header.
    $("#topnav_wrapper").append('<div id="ilist_header_wrapper"></div>');
    var pgHeader = $(".htx-ilist-header").remove();
    $("#ilist_header_wrapper").append(pgHeader);
    $(".htx-ilist-header").css("margin-bottom", "0");
    $(".htx-ilist-header .row").css("border-radius", "0");

    ///Move the Pager Control to the header element where it can maintain visible persistence
    ///while Ajax calls refresh data elsewhere. This section of code has been moved out 
    ///of _hto_uiutils.cshtml.
    ///
    ///  CAUTION!
    /// This function is called by every Ajax call.  May not need to do this but once.  So
    /// it could be moved to the moveIListHeader() function.
    /*
    var pagerctrl = $(".htx-ilist-pagectrl").remove();
    if ($(pagerctrl).length > 0) {

        var footercontainer = $(".htx-ilist-footer-right");
        if ($(footercontainer).length > 0) {
            var pagectrlfooter = $(pagerctrl).clone();
            $(footercontainer).append(pagectrlfooter);
            $(pagectrlfooter).fadeIn(1500);

        }
        $(".htx-ilist-header-right").append(pagerctrl);

        $(pagerctrl).fadeIn(1500);

    }

   */

}

function fixbelowtopnav_wrapperFloating() {

    $(".htx-ilist-main").css("float", "none");
    $("#belowtopnav_wrapper").css("overflow", "inherit");

}

function toggleFilter() {

    $(".htx-ilist-main").toggleClass("toggled");
    $(".htx-ilist-filtercol").toggleClass("toggled");
}


function submitFilterForm() {
    console.log("Submitting form...");
    $(".htx-ilist-header").find(".fa-spinner").show();
    $("#filterForm").submit();
}

function clearFiltersandSubmit() {
    clearFilters();
    submitFilterForm();
}

function clearFilters() {

    var filterForm = document.getElementById("filterForm");
    var frm_elements = filterForm.elements;

    for (i = 0; i < frm_elements.length; i++) {
        field_type = frm_elements[i].type.toLowerCase();
        switch (field_type) {
            case "text":
            case "password":
            case "textarea":
            case "hidden":
            case "select-one":
                frm_elements[i].value = "";
                break;
            case "radio":
            case "checkbox":
                if (frm_elements[i].checked) {
                    frm_elements[i].checked = false;
                }
                break;
            case "select-multi":
                frm_elements[i].selectedIndex = -1;
                break;
            default:
                break;
        }
    }

    document.getElementById("fromForm").value = "true";


}

function procfailure(xhr) {

    alert(`Status: {xhr.status}, Status Text: {xhr.statusText}`);

}


function openanalysis() {
    var formvars = $("#filterForm").serialize();
    var analysisurl = "/Analysis/Returns?" + formvars;
    window.location.assign(analysisurl);
}

