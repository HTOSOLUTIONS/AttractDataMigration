/*
$(document).ready(function () {

    paintControls(document);

});
*/

function paintControls(element) {

    paintToolTip($(element).find(".hto-details-ctrl"), 'glyphicons-info-sign', 'Details');
    paintToolTip($(element).find(".hto-builder-ctrl"), 'glyphicons-claw-hammer', 'Builder');
    paintToolTip($(element).find(".hto-edit-ctrl"), 'glyphicons-edit', 'Edit');
    paintToolTip($(element).find(".hto-delete-ctrl"), 'glyphicons-remove-sign', 'Delete');
    paintToolTip($(element).find(".hto-analysis-ctrl"), 'glyphicons-riflescope', 'Analysis');
    paintToolTip($(element).find(".hto-login-ctrl"), 'glyphicons-log-in', 'Login');
    paintToolTip($(element).find(".hto-refresh-ctrl"), 'glyphicons-refresh', 'Refresh');
    //paintToolTip($(element).find(".hto-search-ctrl"), 'glyphicons-search', 'Search');

    $(element).find(".hto-create-ctrl").prepend("<span class='glyphicons glyphicons-plus-sign'></span>");

    $(element).find(".hto-copy-ctrl").prepend("<span class='glyphicons glyphicons-copy'></span>");
    $(element).find(".hto-copy-ctrl").addClass("hto-tooltip");
    $(element).find(".hto-copy-ctrl").prepend("<span class='tooltiptext'>Copy</span>");

    $(element).find(".hto-clone-ctrl").prepend("<span class='glyphicons glyphicons-duplicate'></span>");
    $(element).find(".hto-clone-ctrl").addClass("hto-tooltip");
    $(element).find(".hto-clone-ctrl").prepend("<span class='tooltiptext'>Clone</span>");

    $(element).find(".hto-paste-ctrl").prepend("<span class='glyphicons glyphicons-paste'></span>");
    $(element).find(".hto-paste-ctrl").addClass("hto-tooltip");
    $(element).find(".hto-paste-ctrl").prepend("<span class='tooltiptext'>Paste</span>");

    $(element).find(".hto-open-ctrl").prepend("<span class='glyphicons glyphicons-folder-open'></span>");
    $(element).find(".hto-open-ctrl").addClass("hto-tooltip");
    $(element).find(".hto-open-ctrl").prepend("<span class='tooltiptext'>Open</span>");

    $(element).find(".hto-assign-ctrl").prepend("<span class='glyphicons glyphicons-send'></span>");
    $(element).find(".hto-assign-ctrl").attr("title", "Send to a Contact");
    configureTooltip($(element).find(".hto-assign-ctrl"));

    $(element).find(".hto-send-ctrl").prepend("<span class='glyphicons glyphicons-send'></span>");
    $(element).find(".hto-send-ctrl").addClass("hto-tooltip");
    $(element).find(".hto-send-ctrl").prepend("<span class='tooltiptext'>Send</span>");

    $(element).find(".hto-receive-ctrl").prepend("<span class='glyphicons glyphicons-disk-save'></span>");
    $(element).find(".hto-receive-ctrl").addClass("hto-tooltip");
    $(element).find(".hto-receive-ctrl").prepend("<span class='tooltiptext'>Receive</span>");

    $(element).find(".hto-view-ctrl").prepend("<span class='glyphicons glyphicons-folder-open'></span>");
    $(element).find(".hto-view-ctrl").addClass("hto-tooltip");
    $(element).find(".hto-view-ctrl").prepend("<span class='tooltiptext'>View</span>");

    $(element).find(".hto-play-ctrl").prepend("<span class='glyphicons glyphicons-play-button'></span>");
    $(element).find(".hto-play-ctrl").addClass("hto-tooltip");
    $(element).find(".hto-play-ctrl").prepend("<span class='tooltiptext'>Play</span>");

    $(element).find(".hto-search-ctrl").prepend("<span class='glyphicons glyphicons-search'></span>");
    $(element).find(".hto-search-ctrl").addClass("hto-tooltip");
    $(element).find(".hto-search-ctrl").prepend("<span class='tooltiptext'>Search or Find</span>");

    $(element).find(".hto-download-ctrl").prepend("<span class='glyphicons glyphicons-download'></span>");
    $(element).find(".hto-download-ctrl").addClass("hto-tooltip");
    $(element).find(".hto-download-ctrl").prepend("<span class='tooltiptext'>Download</span>");

    $(element).find(".hto-sign-ctrl").prepend("<span class='glyphicons glyphicons-pen'></span>");
    $(element).find(".hto-sign-ctrl").addClass("hto-tooltip");
    $(element).find(".hto-sign-ctrl").prepend("<span class='tooltiptext'>Sign</span>");

    $(element).find(".hto-merge-ctrl").prepend("<span class='glyphicons glyphicons-git-merge'></span>");
    $(element).find(".hto-merge-ctrl").addClass("hto-tooltip");
    $(element).find(".hto-merge-ctrl").prepend("<span class='tooltiptext'>Merge</span>");

    $(element).find(".hto-recycle-ctrl").prepend("<span class='glyphicons glyphicons-recycle'></span>");
    $(element).find(".hto-start-ctrl").prepend("<span class='glyphicons glyphicons-play'></span>");

    $(element).find(".hto-exit-ctrl").prepend("<span class='glyphicons glyphicons-exit'></span>");
    $(element).find(".hto-save-ctrl").prepend("<span class='glyphicons glyphicons-floppy-save'></span>");
    $(element).find(".hto-next-ctrl").prepend("<span class='glyphicons glyphicons-step-forward	'></span>");

    $(element).find(".pg-title-details").prepend("<span class='glyphicons glyphicons-info-sign'></span>");
    $(element).find(".pg-title-edit").prepend("<span class='glyphicons glyphicons-edit'></span>");
    $(element).find(".pg-title-delete").prepend("<span class='glyphicons glyphicons-remove-sign'></span>");


}

function paintToolTip(element, symbol, text) {

    $(element).prepend("<span class='glyphicons " + symbol + "'></span>");

    if (1 > 0) {
        $(element).addClass("hto-tooltip");
        $(element).prepend("<span class='tooltiptext'>" + text + "</span>");
    } else {
        $(element).attr("title", text);
        configureTooltip(element);
    }

}

function configureTooltip(selector) {
    $(selector).tooltip({
        position: {
            my: "center bottom-20",
            at: "center top",
            using: function (position, feedback) {
                $(this).css(position);
                $("<div>")
                    .addClass("arrow")
                    .addClass(feedback.vertical)
                    .addClass(feedback.horizontal)
                    .appendTo(this);
            }
        }
    });
}


function configureFormControls() {

    if ($(".hto-next-ctrl").filter("[target]").length == 1) {

        var formid = $(".hto-next-ctrl").filter("[target]").first().attr("target");
        if ($(formid).length == 1) {

            $(formid).find(".btn-default").hide();

            if ($(formid).find(".hto-form-btnbar").length == 0) {
                $(formid).prepend('<div class="hto-form-btnbar"></div>');
            }
            var btnBar = $(formid).find(".hto-form-btnbar");
            $(".hto-next-ctrl").addClass("pull-right");
            $(".hto-next-ctrl").appendTo(btnBar);

            $(".hto-next-ctrl").filter("[target]").click(null, function (e) {
                e.preventDefault();
                var formid = $(this).attr("target");
                var form = $(formid);
                $(form).attr("submitted", true);
                $(form).find("input").prop("disabled", false);
                console.log("Turned off Disable, Ready to submit form.");
                $(form).submit();
                console.log("Submitted form.");
            });

        }
    }

}


jQuery.fn.extend({




});

