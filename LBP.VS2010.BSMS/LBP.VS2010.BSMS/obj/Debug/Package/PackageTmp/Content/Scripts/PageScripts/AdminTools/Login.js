//Form Ready
$(document).ready(function () {
    formatPasswordControl();
    loadingScreen(true, 0);
    ClearFields();
    //updateDivsMargins();
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_endRequest(function () {
        // re-bind your jQuery events here            
        formatPasswordControl();
    });

    function formatPasswordControl() {
        $(".togglePassword").showHidePassword();
        $(".show-hide-password").css("margin-top", "35px");
    }

    //Prevent Double Submit Events
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
    function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }

    $(".enterLogin").keyup(function (e) {
        if (e.which == 13) {
            if (checkFadeOutLoadingScreen()) {
                if (!$('#txtUsername').hasClass("unlockUser")) {
                    Login();
                }
                else {
                    UnlockUser();
                }
            }
        }
    });

    toggleEventForElement("#btnLogin", "click", Login, true, null);
    toggleEventForElement("#btnReset", "click", UnlockUser, true, null);
});

function ClearFields() {
    document.getElementById('txtPassword').value = '';
    document.getElementById('txtUsername').value = '';
    setLabelText('lblErrorMsg', '');
    $('#lblErrorMsg').addClass("hidden");
    document.getElementById('txtUsername').focus();

    return false;
}


function getUserLoginParameters() {
    var userObj = new Object();

    userObj.UserId = getTextboxValue("txtUsername");
    userObj.Password = getTextboxValue("txtPassword");

    return userObj;
}

function Login() {
    loadingScreen(false, 0);
    $('#lblErrorMsg').addClass("hidden");
    PageMethods.Login(getUserLoginParameters(),
        function onSuccess(result) {
            if (result.isSuccess) {
                window.location.replace('/Views/Login/Welcome.aspx');
              
            }
            else {
                setLabelText('lblErrorMsg', result.objErrorMessage);
                $('#lblErrorMsg').removeClass("hidden");

                if (result.AllowToUnlock) {
                 //   $('#divLogin').addClass("hidden");
                   // $('#divUnlock').removeClass("hidden");

                    $('#txtUsername').removeClass("enterLogin");
                    $('#txtPassword').removeClass("enterLogin");
                    $('#txtUsername').addClass("unlockUser");
                    $('#txtPassword').addClass("unlockUser");
                }
            }
            setTextboxValue('txtPassword', '');
            loadingScreen(true, 0);
        },
        function onError(err) {
            if (!sessionErrorCatcher(err.get_message())) {
                bootbox.alert(err.get_message());
            }
            loadingScreen(true, 0);
        }
    );
}

function UnlockUser() {
    loadingScreen(false, 0);
    $('#lblErrorMsg').addClass("hidden");
    PageMethods.UnlockUser(getUserLoginParameters(),
        function onSuccess(result) {
            if (result.isSuccess) {
                //$('#divLogin').removeClass("hidden");
                //$('#divUnlock').addClass("hidden");

                $('#txtUsername').addClass("enterLogin");
                $('#txtPassword').addClass("enterLogin");
                $('#txtUsername').removeClass("unlockUser");
                $('#txtPassword').removeClass("unlockUser");
            }
            else {
                setLabelText('lblErrorMsg', result.objErrorMessage);
                $('#lblErrorMsg').removeClass("hidden");
            }
            setTextboxValue('txtPassword', '');
            loadingScreen(true, 0);
        },
        function onError(err) {
            if (!sessionErrorCatcher(err.get_message())) {
                bootbox.alert(err.get_message());
            }
            loadingScreen(true, 0);
        }
    );
}