function htoDialogDefaults() {

    this.getInfoDialogOptions = function () {
        var dialogoptions = {
            autoOpen: false,
            modal: true,
            width: 800,
            classes: {
                "ui-dialog": "hto-dg-info",
                "ui-dialog-titlebar": "hto-dg-titlebar-info",
                "ui-dialog-title": "hto-dg-title-info",
                "ui-dialog-titlebar-close": "ui-dialog-titlebar-close hto-dg-titlebar-close-info"
            },
            show: { effect: "fade", duration: 400 }
        }

        return dialogoptions;
    }

    /***********************************
     * Used by:
     *      Partners\PartnerNextSteps
     * */
    this.getInfoWizardDialogOptions = function (closeRef) {

        var dialogoptions = {
            autoOpen: false,
            modal: true,
            width: 800,
            closeText: "hide",
            classes: {
                "ui-dialog": "hto-dg-info",
                "ui-dialog-titlebar": "hto-dg-titlebar-info",
                "ui-dialog-title": "hto-dg-title-info",
                "ui-dialog-titlebar-close": "ui-dialog-titlebar-close hto-dg-titlebar-close-info"
            },
            title: "Wizard",
            show: { effect: "blind", duration: 800 },
            close: function (event, ui) {
                window.location = $(closeRef).attr("href");
            }
        }

        return dialogoptions;

    }


}
/***********************************************************
 * Builds configurations for specific use cases that can be
 * passed to htoModalMgr
 *
 * */

function htoModalConfigBuilder() {

    /***********************************
     * PROTOTYPE
     * */
    this.getModalPrototype = function () {

        var modalConfig = {
            description: "Description",
            formparentid: "{#modalFormParentContainer}",
            formid: "{#modalFormID}",
            sourceController: "{controllername}",
            sourceAction: "{action}",
            sourceurl: "/{controllername}/{action}/",
            sourceQueryDelimiter: "{delimiter}",
            baseurl: "{controllername}/{action}",
            postController: "{controllername}",
            postAction: "{action}",
            openAfterLoad: true,
            dialogoptions: {
                title: "Create Address"
            },
            useDefaultSuccessInsert: false,
            useDefaultSuccessEdit: false,
            useDefaultSuccessDelete: false,
            donepost: function () {
                //Code for when Post is finished.
            },
            insertContainerID: "{#containerID}",
            openconfig: function () {
                this.parentObj.setDefaultInsert();
            }
        };

        return modalConfig;

    }

    /***********************************
     * Used by:
     *      {.........]
     * */
    this.getModal_____Details = function () {

        var modalConfig = {
            description: "Form to View a _____ Details",
            sourceController: "{controller}",
            sourceAction: "{action}",
            paintAfterLoad: true,
            openAfterLoad: true,
            dialogoptions: {
                title: "_____ Details"
            }

        };

        return modalConfig;


    }

    this.getModal_____Create = function () {

        var modalConfig = {
            description: "Form to Create a _____",
            sourceController: "{controller}",
            sourceAction: "{action}",
            dialogoptions: {
                title: "Create _____"
            }

        };

        return modalConfig;


    }

    this.getModal_____Edit = function () {


        var modalConfig = {
            description: "Form to Edit a _____",
            sourceController: "{controller}",
            sourceAction: "{action}",
            dialogoptions: {
                title: "Edit _____"
            }

        };

        return modalConfig;

    }

    this.getModal_____Delete = function () {


        var modalConfig = {
            description: "Form to Delete a _____",
            sourceController: "{controller}",
            sourceAction: "{action}",
            dialogoptions: {
                title: "Delete _____"
            }

        };

        return modalConfig;

    }


    this.getPreviewLinkModalConfig = function () {
        var modalconfig = {
            formid: "#linkcolumns",
            configBeforeOpen: true,
            openAfterLoad: true,
            baseurl: "/KitchenSink/PreviewLink",
            dialogoptions: {
                title: "Preview Link Columns",
                maxWidth: "800px"
            }

        };

        return modalconfig;
    }

    this.getForeignKeySnapshotModalConfig = function () {
        var modalconfig = {
            formid: "#foreignkeysnapshot",
            configBeforeOpen: true,
            openAfterLoad: true,
            baseurl: "/TargetTables/ForeignKeySnapshot",
            dialogoptions: {
                title: "Foreign Key Snapshot",
                maxWidth: "800px"
            }

        };

        return modalconfig;
    }

    this.getTargetTableSnapshotModalConfig = function () {
        var modalconfig = {
            formid: "#targettablesnapshot",
            configBeforeOpen: true,
            openAfterLoad: true,
            baseurl: "/TargetTables/Snapshot",
            dialogoptions: {
                title: "Target Table Snapshot",
                maxWidth: "800px"
            }

        };

        return modalconfig;
    }

    this.getTargetTableCreateSQLModalConfig = function () {
        var modalconfig = {
            formid: "#targettablecommitsql",
            configBeforeOpen: true,
            openAfterLoad: true,
            baseurl: "/TargetTables/CreateSQLStatement",
            dialogoptions: {
                title: "Target Table Create Statement",
                maxWidth: "800px"
            }

        };

        return modalconfig;
    }

    this.getTargetTableInsertSQLModalConfig = function () {
        var modalconfig = {
            formid: "#targettablecommitsql",
            configBeforeOpen: true,
            openAfterLoad: true,
            baseurl: "/TargetTables/InsertSQLStatement",
            dialogoptions: {
                title: "Target Table Insert Statement",
                maxWidth: "800px"
            }

        };

        return modalconfig;
    }

    this.getFullParentPathModalConfig = function () {

        var modalconfig = {
            formid: "#fullparentpathmodal",
            configBeforeOpen: true,
            openAfterLoad: true,
            baseurl: "/TargetTables/FullParentPath",
            dialogoptions: {
                title: "Full Parent Path",
                maxWidth: "800px"
            }

        };

        return modalconfig;
    }




}