$(document).ready(function () {

    $("#btn1").click(function () {
        console.log("click");

        $.get("api/utility/quit", null);
    });
    $("#btn2").click(function () {
        console.log("click");

        $.get("api/utility/minimize", null);
    });

    $("#btn3").click(function () {
        console.log("click batterystate ");

        $.get("api/utility/batterystate", function (val) {
            console.log("return batterty state value: ", val);
        });
    });
});