

function configsourcecolumns() {

    $(".sourcecolumns tr").on("click", function () {

        $(".sourcecolumns .selected").removeClass("selected");
        $(this).addClass("selected");

    });


}

function configtargetcolumns() {

    $(".targetcolumns tr").on("click", function () {

        $(".targetcolumns .selected").removeClass("selected");
        $(this).addClass("selected");

    });


}
