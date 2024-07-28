


function htoModalMgr(modalConfig) {

    this.localName = "htoModalMgr";

    this.configloaded = false;

    this.modalConfig = {
        formparentid: null,
        formid: null,
        sourceController: null,
        sourceAction: null,
        sourceQueryDelimiter: null,
        baseurl: null,
        sourceurl: null,
        useDefaultSuccessInsert: false,
        useDefaultSuccessEdit: false,
        useDefaultLoadValues: false,
        useFormData: false,
        closeOnSubmit: true,
        posturl: "",
        postreturntype: "html",
        insertContainerID: null,
        parentObj: null,
        mvcoptions: {
            dataDescriptor: "Record"
        },
        overrideSubmit: true,
        configBeforeOpen: false,
        paintAfterLoad: false,
        openAfterLoad: false,
        dialogoptions: {
            autoOpen: false,
            modal: true,
            width: 650
        },
        defaultInsertConfigurator: null

    };

    if (typeof modalConfig == "object") {
        modalConfig.parentObj = this;
        $.extend(true, this.modalConfig, modalConfig);
        this.configloaded = true;
    }

    this.loadConfig = function (modalConfig) {
        if (typeof modalConfig == "object") {
            modalConfig.parentObj = this;
            $.extend(true, this.modalConfig, modalConfig);
            this.configloaded = true;
        }
    }

    this.loadModalRecord = function (control) {

        var recordid = $(control).getrecordid();

        if (recordid == null) {
            recordid = $(control).getclosestrow().find("[recordKeys]").attr("recordKeys");
        }
        if (recordid != null) {
            this.loadModalParams(recordid, control);
        } else {
            this.loadModal(null,control);
        }

    }

    this.loadModalParams = function (params, control) {

        this.modalConfig.sourceurl = this.bldSourceUrl(params);

        /*
        if (Object.keys(params).length == 1
            && this.modalConfig.baseurl.substring(this.modalConfig.baseurl.length - 1) == "/"
            && Object.keys(params)[0] == "id")
        {
            this.modalConfig.sourceurl = this.modalConfig.baseurl + Object.values(params)[0];
        } else {
            this.modalConfig.sourceurl = this.modalConfig.baseurl + $.param(params);
        }
         */

        this.loadModal(null, control);

    }

    this.loadModal = function (modalConfig, control) {

        /*
            This global variable 'lastloader' is set and used in the callback function when the Ajax load method
            is completed.  This can be a bit of a problem when more than one htoModalMgr is created
            and loaded on the same page at the same time.  The variable can be overwritten by the second
            instance before the first one is finished.  See questionnaire_builder.js as an example. The 
            work around is to create the second instance in the postload function of the first instance,
            somewhat of a "daisy chain" approach.
        */
        lastloader = this;

        var parentid = null;
        if (typeof (modalConfig) == "object" && modalConfig != null) {

            this.loadConfig(modalConfig);

        } else {
            if ((typeof (this.modalConfig) != "object" || !this.configloaded) && typeof modalformconfig == "object") {
                this.loadConfig(modalformconfig);
            }
        }
        if (this.modalConfig.sourceurl == null || this.modalConfig.sourceurl == "") {
            this.modalConfig.sourceurl = this.bldSourceUrl();
        }

        var oConfig = this.modalConfig;
        if (typeof (oConfig.formparentid) != "string" || oConfig.formparentid.length == 0) {
            parentid = '#hto' + Math.random().toString(10).substring(2);
            oConfig.formparentid = parentid;
            this.modalConfig.formparentid = parentid;
        } else {
            parentid = oConfig.formparentid;
        }

        if ($(parentid).length == 0) {
            $("body").append("<div id='" + parentid.replace("#","") + "'></div>");
        }


        $(parentid).load(
                oConfig.sourceurl,
                null,
                function (response, status, xhr) {
                    if (status == "error") {
                        htoAjaxMessageDisplay(response);
                    } else {
                        if (oConfig.formid === undefined || oConfig.formid === null) {
                            var sourceform = $(parentid).find("form");
                            if (sourceform.length == 1) {
                                oConfig.formid = "#" + $(sourceform).attr("id");
                            }
                        }
                        $(parentid).dialog(oConfig.dialogoptions);
                        if (typeof oConfig.loadconfig == "function") {
                            oConfig.loadconfig();
                        }
                        $('.ui-dialog').css('z-index', 3000);

                        if (typeof $.validator == "function") {

                            $.validator.unobtrusive.parse(oConfig.formid);

                        }

                        //$(oConfig.formid).data("modalmanager", lastloader);
                        $(oConfig.formparentid).data("modalmanager", lastloader);
                        $(oConfig.formparentid).addClass("hasModalManager");

                        if (oConfig.overrideSubmit) {

                            $(oConfig.formid).submit(
                                function (ev) {
                                    var valid = true;
                                    if (typeof $(this).data("validator") == "object") {
                                        valid = $(this).valid();
                                    }
                                    if (valid) {
                                        //xhtoModalMgr.submitModal();
                                        //$(this).data("modalmanager").submitModal() ;
                                        if (typeof $(this).closest(".hasModalManager").data("modalmanager") === "object") {
                                            $(this).closest(".hasModalManager").data("modalmanager").submitModal();
                                        } else {
                                            alert("modalmanager not set");
                                        }
                                        ev.preventDefault();
                                    }
                                }
                            );

                        }

                        if (oConfig.paintAfterLoad) {
                            paintControls(oConfig.formparentid);
                        }

                        if (typeof oConfig.postload == "function") {
                            oConfig.postload();
                        }

                        if (oConfig.openAfterLoad) {
                            //$(oConfig.formid).data("modalmanager").openModal();
                            oConfig.dialogoptions.autoOpen = true;
                            $(oConfig.formparentid).data("modalmanager").openModal(control);
                        }
                    }

                }
        )

            /*
            .fail(function (data) {
            $("#modalWait").removeClass("loading");
            var message = "Error on Ajax Post.";
            try {
                message = JSON.parse(data.responseText).message;
            } catch (err) {
                try {
                    var response = JSON.parse(data);
                    message = response.responseText;
                } catch (err2) {
                }
            }
            htomessagebox(message);
        })
             */

    };

    this.finishload = function (status) {

        var oConfig = this.modalConfig;

        lastloader = this;

        if (typeof (oConfig.formparentid) == "undefined") {
            parentid = "#modalForm";
        } else {
            parentid = oConfig.formparentid;
        }

        $(parentid).dialog(oConfig.dialogoptions);

        if (status == "error") {
            htomessagebox("There was an error.  " + response);
        }
        if (typeof oConfig.loadconfig == "function") {
            oConfig.loadconfig();
        }
        $('.ui-dialog').css('z-index', 3000);

        if (typeof $.validator == "function") {

            $.validator.unobtrusive.parse(oConfig.formid);

        }

        //$(oConfig.formid).data("modalmanager", lastloader);
        $(oConfig.formparentid).data("modalmanager", lastloader);

        $(oConfig.formid).submit(
                function (ev) {
                    var valid = true;
                    if (typeof $(this).data("validator") == "object") {
                        valid = $(this).valid();
                    }
                    if (valid) {
                        //xhtoModalMgr.submitModal();
                        //$(this).data("modalmanager").submitModal();
                        $(this).parent.data("modalmanager").submitModal();
                        ev.preventDefault();
                    }
                }
            );

        if (typeof oConfig.postload == "function") {
            oConfig.postload();
        }

    };

    this.openModal = function (control) {

        var action = $(control).attr("data-modal-action");
        var oConfig = this.modalConfig;
        if (typeof (oConfig.formparentid) == "undefined") {
            parentid = '#hto' + Math.random().toString(10).substring(2);
            oConfig.formparentid = parentid;
            this.modalConfig.formparentid = parentid;
        } else {
            parentid = oConfig.formparentid;
        }

        if ($(parentid).length == 0) {
            this.loadModal();
            oConfig.configBeforeOpen = true;
        }


        if (action == 'edit' && oConfig.useDefaultSuccessEdit) {
            //Tag the element that will be updated.
            var targetrow = $(control).getclosestrow();
            $(targetrow).targeteditrow();
            this.modalConfig.successpost = function (data) {

                //If we wanted to pull the edited content out of a row wrapper
                //we'd do something like the following:
                /**
                var dataAsDomElement = $.parseHTML($.trim(data));
                var html = $(dataAsDomElement).html();
                $().geteditedrow().html(html);


                 **/
                $().geteditedrow().html(data);

            }

        }

        if (oConfig.useDefaultLoadValues) {
            this.defLoadValues(control);
        }

        if (oConfig.useDefaultSuccessInsert && action != 'edit') {
            this.setDefaultInsert();
        }

        if (oConfig.useDefaultSuccessDelete && action == 'delete') {
            this.setDefaultDelete(control);
        }

        if (typeof oConfig.openconfig == "function") {
            oConfig.openconfig(control);
        }
        if (oConfig.configBeforeOpen) {
            $(parentid).dialog(oConfig.dialogoptions).dialog("option", "modal", true);
        } else {
            $(parentid).dialog("option", "modal", true);
        }
        $(parentid).dialog("open");

    };

    //Function called when the modalForm is opened.
    //Loads values into the form controls and sets the post actions.
    //Operates in two modes depending on the "action" attribute of the control that calls openModal().
    this.defLoadValues = function (control) {


        var action = $(control).attr("data-modal-action");
        var oConfig = this.modalConfig;
        if (typeof (oConfig.formparentid) == "undefined") {
            parentid = "#modalForm";
        } else {
            parentid = oConfig.formparentid;
        }

        if (action == 'edit') {


            targetrecord = $(control).getclosestrecord(oConfig.recordSelector);

            //Set the form title specific to inserting a new record.
            $(parentid).dialog("option", "title", "Edit " + this.modalConfig.mvcoptions.dataDescriptor);


            $(parentid).find("[data-modal-get]").each(function () {
                var thisinput = this;

                //2017.10.19 - use targetrecord instead of target row.  It can now be different.
                var valuesource = $(targetrecord).find('[data-modal-for="' + $(this).attr("name") + '"]');
                if (valuesource.length == 1) {

                    var nodeName = $(valuesource)[0].nodeName;
                    if (nodeName == "INPUT") {
                        $(this).val($(valuesource).val());
                    } else {
                        $(this).val($(valuesource).html().trim());
                    }

                } else {
                    $(this).val("");
                }

            });

            this.modalConfig.posturl = $(this.modalConfig.formid).attr("action");

        } else {

            //Set the form title specific to inserting a new record
            $(parentid).dialog("option", "title", "New " + this.modalConfig.mvcoptions.dataDescriptor);

            //Clear out values for an insert.
            $(parentid).find("[data-modal-get]").each(function () {
                $(this).val("");
            });

            //Set the URL for the Ajax post
            if (typeof this.modalConfig.inserturl == "string" && this.modalConfig.inserturl.length > 0) {
                this.modalConfig.posturl = this.modalConfig.inserturl;
            } else if (typeof this.modalConfig.posturl != "string" || this.modalConfig.posturl.length == 0) {
                this.modalConfig.posturl = $(this.modalConfig.formid).attr("action");
            }

        }

    };

    this.submitModal = function () {
        var oConfig = this.modalConfig;
        if (typeof (oConfig.formparentid) == "undefined") {
            parentid = "#modalForm";
        } else {
            parentid = oConfig.formparentid;
        }

        if (oConfig.closeOnSubmit) {
            $(parentid).dialog("close");
        }
        if (typeof oConfig.prepformdata == "function") {
            oConfig.prepformdata();
        }


        var formvars = $(oConfig.formid).serialize();
        var postdata = {};
        var poststring = "";
        if (oConfig.posturl.length > 0) {
            poststring = oConfig.posturl;
        } else {
            poststring = $(oConfig.formid).attr("action");
        }

        if (typeof oConfig.getpostdata == "function") {
            postdata = oConfig.getpostdata();
            if (oConfig.posturl.indexOf("?") >= 0) {
                poststring = poststring + "&";
            } else {
                poststring = poststring + "?";
            }
            poststring = poststring + formvars;
        } else if (oConfig.useFormData) {

            var formData = new FormData($(oConfig.formid)[0]);
            postdata = formData;

        } else {
            postdata = formvars;
        }

        if (!oConfig.useFormData) {
            $.post(poststring, postdata, oConfig.successpost, oConfig.postreturntype)
                .done(oConfig.donepost)
                .always(oConfig.alwaysfunc)
                .fail(
                    function (data) {
                        $("#modalWait").removeClass("loading");
                        htoAjaxMessageDisplay(data);
                    },
                    "html"
                );

        } else {

            $.ajax({
                type: "POST",
                url: poststring,
                data: postdata,
                success: oConfig.successpost,
                dataType: oConfig.postreturntype,
                contentType: false,
                processData: false
            })
            .always(oConfig.alwaysfunc)
            .fail(
                function (data) {
                    $("#modalWait").removeClass("loading");
                    htoAjaxMessageDisplay(data);
                },
                "html"
            );

        }


    };

    this.closeModal = function () {
        var oConfig = this.modalConfig;
        if (typeof (oConfig.formparentid) == "undefined") {
            parentid = "#modalForm";
        } else {
            parentid = oConfig.formparentid;
        }
        $(parentid).dialog("close");

        if (typeof oConfig.afterClose == "function") {
            oConfig.afterClose();
        }

    };

    this.setDefaultInsert = function () {

        if (typeof this.modalConfig.insertContainerID == "string") {
            $(this.modalConfig.insertContainerID).targetappendcontainer();
        }

        this.modalConfig.successpost = function (data) {
            var dataAsDomElement = $.parseHTML($.trim(data));
            $(".lastInsertedElement").removeClass("lastInsertedElement");
            $(dataAsDomElement).addClass("lastInsertedElement");
            var appendContainer = $().getappendcontainer();
            if (appendContainer.length > 0) {
                $(appendContainer).append(dataAsDomElement);
            } else {
                $(globalbodydiv).append(dataAsDomElement);
            }
        }


    };

    this.setDefaultDelete = function (control) {

        var targetrow = $(control).getclosestrow();
        $(targetrow).targetdeleteelement();

        this.modalConfig.successpost = function (data) {
            $().getdeletedelement().remove();

            var message = "Error on Ajax Post.";
            try {
                message = JSON.parse(data.responseText).message;
            } catch (err) {
                try {
                    var response = JSON.parse(data);
                    message = response.responseText;
                } catch (err2) {
                }
            }
            htomessagebox(message);

        }

    };

    this.removeRecord = function () {


        if (typeof this.modalConfig.deleteurl == "string") {

            var token = $("[name='__RequestVerificationToken']").val();

            var recordid = $().geteditedrow().getrecordid();

            $().geteditedrow().getrecordid();
            var requeststring = this.modalConfig.deleteurl + "?id=" + recordid;

            $.post(requeststring,
                { __RequestVerificationToken: token },
                function (data) {
                    $().geteditedrow().remove();
                }
            ).fail(
                function (data) {
                    htomessagebox(data.responseText);
                }
            );
            /*
            */

        }

    };

    this.bldSourceUrl = function (params) {

        var url = "/";
        var oConfig = this.modalConfig;

        if (typeof oConfig.baseurl == "string" && oConfig.baseurl.length > 0) {

            url = oConfig.baseurl;

        } else if (typeof oConfig.sourceController == "string" && oConfig.sourceController.length > 0
            && typeof oConfig.sourceAction == "string" && oConfig.sourceAction.length > 0) {

            url = "/" + oConfig.sourceController + "/" + oConfig.sourceAction;

        }

        if (typeof params == "string" || typeof params == "number" || typeof params == "object") {

            var queryDelim = url.substring(url.length - 1);

            if (queryDelim != "/" && queryDelim != "?") {

                if (typeof oConfig.sourceQueryDelimiter == "string" && oConfig.sourceQueryDelimiter.length > 0) {

                    queryDelim = oConfig.sourceQueryDelimiter;

                } else {

                    queryDelim = "/";
                }

            } else {

                url = url.substring(0, url.length - 1);

            }

            if (typeof params == "string" && params.charAt(0) == "{" && params.charAt(params.length - 1) == "}") {
                try {
                    params = JSON.parse(params);
                } catch (e) {
                }
            }

            if (typeof params == "string" || typeof params == "number") {

                url = url + queryDelim + params;

            } else {

                if (Object.keys(params).length == 1
                    && queryDelim == "/"
                    && Object.keys(params)[0] == "id") {

                    url = url + "/" + Object.values(params)[0];

                } else {

                    if (queryDelim == "/"
                        && Object.keys(params)[0] == "id") {

                        url = url + "/" + Object.values(params)[0];

                    }

                    url = url + "?" + $.param(params);

                }

            }

        }

        return url;

    };

}


