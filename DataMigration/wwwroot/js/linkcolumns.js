



$(document).ready(function () {

    var defTable = $("[data-destinationtable]").data("destinationtable");
    if (defTable.length > 0) {
        loadTargetTable(defTable);
    }
    configsourcecolumns();

    $("#previewlinkbtn").on("click", function () {
        openpreview();
    });


    $("#targettablename").on('keypress', function (e) {
        if (e.which == 13) {
            loadTargetTable($("#targettablename").val());
        }
    });


});


function processPostedMatch(data) {

    var model = JSON.parse(data);

    //$("[data-sourcecolumn].selected .destinationtable").html(model.targetColumn.tableName);
    //$("[data-sourcecolumn].selected .destinationcolumn").html(model.targetColumn.columnName);

    $("[data-sourcecolumn].selected .columntargets").html(model.sourceColumn.columnTargetsDisp);


    //$("[data-targetcolumn].selected .sourcetable").html(model.sourceColumn.tableName);
    //$("[data-targetcolumn].selected .sourcecolumn").html(model.sourceColumn.columnName);

    $("[data-targetcolumn].selected .columntargets").html(model.targetColumn.columnSourcesDisp);


    $("[data-sourcecolumn].selected").data("sourcecolumn", model.sourceColumn);
    $("[data-targetcolumn].selected").data("targetcolumn", model.targetColumn);



    globalModel = model;

    console.log(model);
}



function loadTargetTable(tablename) {

    $(".targetColumns").load("/TargetTables/TargetTableVC?tablename=" + tablename, function () { configTableLinks(); configtargetcolumns(); });
}

function configTableLinks() {
    $("a.pathpart").on("click", function () {

        var tablewithschema = $(this).html();
        var nameparts = tablewithschema.split(".");
        var tablename = nameparts[1];
        loadTargetTable(tablename);
    });

}


function configsourcecolumns() {

    $(".sourcecolumns tr").on("click", function () {

        if ($(this).is(".selected")) {
            $(this).removeClass("selected");
        } else {
            $(".sourcecolumns .selected").removeClass("selected");
            $(this).addClass("selected");
        }


    });


}

function configtargetcolumns() {

    $(".targetcolumns tr").on("click", function () {
        if ($(this).is(".selected")) {
            $(this).removeClass("selected");

        } else {
            $(".targetcolumns .selected").removeClass("selected");
            $(this).addClass("selected");
        }

    });

    var targetTable = $("[data-targettablename]").data("targettablename");
    $("#targettablename").val(targetTable);


}


function openpreview() {

    var srccolumn = $("[data-sourcecolumn].selected").data("sourcecolumn");
    var tgtcolumn = $("[data-targetcolumn].selected").data("targetcolumn");
    var parms = {

        SrcTableSchema: srccolumn.tableSchema,
        SrcTableName: srccolumn.tableName,
        SrcColumnName: srccolumn.columnName,
        TgtTableSchema: tgtcolumn.tableSchema,
        TgtTableName: tgtcolumn.tableName,
        TgtColumnName: tgtcolumn.columnName

    };

    previewlinkmoalmgr.loadModalParams(parms);

}

