$(document).ready(function () {
    sessionStorage.clear();
    window.setTimeout('CountDown()', 100);
});

var start = new Date();
start = Date.parse(start) / 1000;
var counts = 10;
function CountDown() {
    var now = new Date();
    now = Date.parse(now) / 1000;
    var x = parseInt(counts - (now - start), 10);

    if (document.getElementById("frmError1") && x > 1) { document.getElementById("clock").innerHTML = x + " seconds "; }
    else {

        document.getElementById("clock").innerHTML = x + " second ";
    }


    if (x > 0) {
        timerID = setTimeout("CountDown()", 100)

    } else {


        window.location = "/Views/Login/Welcome.aspx";
    }
    return
}

