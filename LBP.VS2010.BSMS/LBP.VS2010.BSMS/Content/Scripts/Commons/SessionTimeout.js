// How to frequently check for session expiration in milliseconds
var sess_pollInterval = 59000;

// How many minutes the session is valid for
var sess_expirationMinutes = 15;

// How many minutes before the warning prompt
var sess_warningMinutes = 13;

var sess_intervalID;
var sess_lastActivity;




function initSessionMonitor() {
    sess_lastActivity = new Date();

    sessSetInterval();
    $(document).bind('keypress.session', function (ed, e) { sessKeyPressed(ed, e); })
}


function sessSetInterval() {
    sess_intervalID = setInterval('sessInterval()', sess_pollInterval);
}

function sessClearInterval() {
    clearInterval(sess_intervalID);
}

function sessKeyPressed(ed, e) {
    sess_lastActivity = new Date(); initSessionMonitor}

function sessPingServer() {
    //call ajax function to reset session
    PageMethods.PingServer(function OnSuccess(result) {
        showSessionExtendedModal(result);
        sessionCountDown(100, 59, 'spanExpireMinutes', false);
    },
        function OnError(err) {
            if (!sessionErrorCatcher(err.get_message())) {
                //alert('boom');
            }
        }
    );

}

function sessExpired() {
    // redirect to session expired
    showSessionExpiredModal();
    //window.location = "/Views/ErrorPages/ExpiredSession.aspx";
}


function sessInterval() {
    var now = new Date();
    var diff = now - sess_lastActivity;


    var diffMins = (diff / 1000 / 60);
    if (diffMins > sess_expirationMinutes) {
        
        // session already expired
        sessExpired();
    }

}

