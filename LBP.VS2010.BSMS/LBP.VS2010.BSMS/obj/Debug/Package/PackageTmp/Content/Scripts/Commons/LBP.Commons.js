var CountDown = (function ($) {
    // Length ms 
    var TimeOut = 10000;
    // Interval ms        
    var TimeGap = 1000;

    var Minutes;
    var Seconds;
    var LblTimer;

    var CurrentTime = (new Date()).getTime();
    var EndTime = (new Date()).getTime() + TimeOut;

    //var GuiTimer = $('#' + lblTimer);

    //var Running = true;
    var Running;

    var UpdateTimer = function () {
        // Run till timeout
        //            if (CurrentTime + TimeGap < EndTime) {
        //                setTimeout(UpdateTimer, TimeGap);
        //            }

        //console.log("min " + Minutes);
        //console.log("sec " + Seconds);
        if ((Seconds <= 0) && (Minutes <= 00)) {
            window.location = "/Views/ErrorPages/ExpiredSession.aspx";
        }
        else {
            setTimeout(UpdateTimer, TimeGap);
        }

        // Countdown if running
        if (Running) {
            //                CurrentTime += TimeGap;
            //                if (CurrentTime >= EndTime) {
            //                    GuiTimer.css('color', 'red');
            //                }

            --Seconds;
            Minutes = (Seconds < 0) ? --Minutes : Minutes;
            Seconds = (Seconds < 0) ? 59 : Seconds;
            Seconds = (Seconds < 10) ? '0' + Seconds : Seconds;

        }
        // Update Gui
        //            var Time = new Date();
        //            Time.setTime(EndTime - CurrentTime);
        //            var Minutes = Time.getMinutes();
        //            var Seconds = Time.getSeconds();

        //            GuiTimer.html(
        //            (Minutes < 10 ? '0' : '') + Minutes
        //            + ':'
        //            + (Seconds < 10 ? '0' : '') + Seconds);

        $("#" + LblTimer).html(Minutes + ':' + Seconds);
    };

    var Pause = function () {
        Running = false;
    };

    var Resume = function (minutes, seconds) {
        Minutes = minutes;
        Seconds = seconds;
        Running = true;
    };


    var Start = function (minutes, seconds, lblTimer, running) {
        Minutes = minutes;
        Seconds = seconds;
        LblTimer = lblTimer;
        Running = running;
        UpdateTimer();
    };

    return {
        Pause: Pause,
        Resume: Resume,
        Start: Start
    };
})(jQuery);

var countdownRun = false;

// control creators

$(document).ready(function () {
    createPager();

    initSessionMonitor();


});

/* Key validation */
function isValidChar(e, rgex, obj) {
    var evt = (e) ? e : window.event;
    var key = (evt.keyCode) ? evt.keyCode : evt.which;

    if ($(obj).is("textarea")) {
        if (key == 13) { // allow enter to be pressed in textareas
            return true;
        }
    }

    if (key != null) {
        key = String.fromCharCode(parseInt(key));
        var rx = new RegExp(rgex);
        return rx.test(key);
    }
    return true;
}

function testForValidCharacters(input, rgex) {
    var rx = new RegExp(rgex);


    var currentValue = $(input).val();

    if (currentValue != "") {
        if (!rx.test(currentValue)) {
            $(input).val("");
            $(input).change();
        }
    }
}

function isEmptyFloatValue(val) {
    return (val === undefined || val == null || val.length <= 0) ? "0" : val.replace(/\,/g, "");

}

/* end - Key validation */

/* confirm dialog for delete */
function confirmDialog(e, a, message) {
    e = $.event.fix(e);

    e.preventDefault();

    bootbox.confirm('Are you sure you want to ' + message + ' this record(s)', function (result) {

        if (result == true) {
            window.location.href = a.href;
        }
    });
}
/* end - confirm dialog for delete */




//jQuery functions
if (window.jQuery) {
    /* All onload functions */
    $(document).ready(function () {

        // Javascript input validations

        // numerics

        $(document.body).on('keypress', '.wholenum', function (event) { return isValidChar(event, '[0-9]'); });
        $(document.body).on('change', '.wholenum', function () { testForValidCharacters(this, '^[0-9]*$'); });

        $(document.body).on('keypress', '.wholenumAllowNegative', function (event) { return isValidChar(event, '[0-9-]'); });
        $(document.body).on('change', '.wholenumAllowNegative', function () { testForValidCharacters(this, '^[0-9-]*$'); });

        // currency/amounts

        $(document.body).on('keypress', '.currency', function (event) { return isValidChar(event, '[0-9.,]'); });
        $(document.body).on('change', '.currency', function () { testForValidCharacters(this, '^[0-9.,]*$'); });
        $(document.body).on('focus', '.currency', function () { $(this).select(); });
        $(document.body).on('blur', '.currency', function () { $(this).val(Currencify($(this).val())); });
        $(document.body).on('change', '.currency', function () { $(this).val(Currencify($(this).val())); });

        $(document.body).on('keypress', '.currencyAllowNegative', function (event) { return isValidChar(event, '[0-9.-]'); });
        $(document.body).on('change', '.currencyAllowNegative', function () { testForValidCharacters(this, '^[0-9.-]*$'); });
        $(document.body).on('focus', '.currencyAllowNegative', function () { $(this).select(); });
        $(document.body).on('blur', '.currencyAllowNegative', function () { $(this).val(Currencify($(this).val())); });
        $(document.body).on('change', '.currencyAllowNegative', function () { $(this).val(Currencify($(this).val())); });

        $(document.body).on('blur', '.currencyDefaultValue', function () { $(this).val(Currencify(isEmptyFloatValue($(this).val()))); $(this).change(); });
        $(document.body).on('change', '.currencyDefaultValue', function () { $(this).val(Currencify(isEmptyFloatValue($(this).val()))); $(this).change(); });

        // alphanumerics

        $(document.body).on('keypress', '.alphanumeric', function (event) { return isValidChar(event, '[ a-zA-Z0-9]'); });
        $(document.body).on('change', '.alphanumeric', function () { return testForValidCharacters(this, '^[ a-zA-Z0-9]*$'); });

        $(document.body).on('keypress', '.alphanumericWithDash', function (event) { return isValidChar(event, '[ a-zA-Z0-9\-]'); });
        $(document.body).on('change', '.alphanumericWithDash', function () { return testForValidCharacters(this, '^[ a-zA-Z0-9\-]*$'); });

        $(document.body).on('keypress', '.alphanumericWithSpecialChar', function (event) { return isValidChar(event, '[ a-zA-Z0-9!@#$%^&amp;*()/%$#\`~]'); });
        $(document.body).on('change', '.alphanumericWithSpecialChar', function () { return testForValidCharacters(this, '^[ a-zA-Z0-9!@#$%^&amp;*()/%$#\`~]'); });

        $(document.body).on('keypress', '.alphabetic', function (event) { return isValidChar(event, '[ a-zA-Z]'); });
        $(document.body).on('change', '.alphabetic', function () { return testForValidCharacters(this, '^[ a-zA-Z]*$'); });

        // other special regex

        $(document.body).on('keypress', '.time', function (event) { return isValidChar(event, '[0-9:]'); });
        $(document.body).on('change', '.time', function () { return testForValidCharacters(this, '^[0-9:]*$'); });

        $(document.body).on('keypress', '.name', function (event) { return isValidChar(event, '[ a-zA-Z0-9\u00F1\u00D1!/@#$%^&*();,.\'-_+]'); });
        $(document.body).on('change', '.name', function () { testForValidCharacters(this, '^[ a-zA-Z0-9\u00F1\u00D1!/@#$%^&*();,.\'-_+]*$'); });

        $(document.body).on('keypress', '.email', function (event) { return isValidChar(event, '[ a-zA-Z0-9.,/-_@]'); });
        $(document.body).on('change', '.email', function () { return testForValidCharacters(this, '^[ a-zA-Z0-9.,/-_@]*$'); });

        $(document.body).on('keypress', '.ip', function (event) { return isValidChar(event, '^[0-9.]+$'); });
        $(document.body).on('change', '.ip', function () { return testForValidCharacters(this, '^[0-9.]+$'); });

        $(document.body).on('keypress', '.num-dash-com', function (event) { return isValidChar(event, '^[0-9,\-]+$'); });
        $(document.body).on('change', '.num-dash-com', function () { return testForValidCharacters(this, '^[0-9,\-]+$'); });

        $(document.body).on('keypress', '.alpha-num-dash', function (event) { return isValidChar(event, '^[a-zA-Z0-9 -]*$'); });
        $(document.body).on('change', '.alpha-num-dash', function () { return testForValidCharacters(this, '^[a-zA-Z0-9 -]*$'); });


        // dates

        $(document.body).on('blur', '.shortdate', function () { $(this).val(PadDate($(this).val())); $(this).change(); });
        $(document.body).on('blur', '.noForwardDate', function () { $(this).val(PadNoForwardDate($(this).val())); $(this).change(); });
        $(document.body).on('blur', '.noCurrentForwardDate', function () { $(this).val(PadDate($(this).val())); $(this).change(); });
        $(document.body).on('blur', '.noBackDate', function () { $(this).val(PadDate($(this).val())); $(this).change(); });
        $(document.body).on('blur', '.noBackDate3Months', function () { $(this).val(PadDate($(this).val())); $(this).change(); });
        
        // required

        $(document.body).on('change', '.required', function () { toggleRequired($(this), $(this).val() == ""); });
        $(document.body).on('click', '.requiredRadio', function () { toggleRequiredRadio($(this), false); });
        $(document.body).on('click', '.requiredCheckbox', function () { toggleRequiredCheckbox($(this)); });



        // mask

        $(".shortdate").mask("99/99/9999", { placeholder: "mm/dd/yyyy" });
        $(".noCurrentForwardDate").mask("99/99/9999", { placeholder: "mm/dd/yyyy" });
        $(".noForwardDate").mask("99/99/9999", { placeholder: "mm/dd/yyyy" });
        $(".noBackDate").mask("99/99/9999", { placeholder: "mm/dd/yyyy" });
        $(".noBackDate3Months").mask("99/99/9999", { placeholder: "mm/dd/yyyy" });
       
        //Upload Class

        $('input[type=file]').bootstrapFileInput();
        $('.file-inputs').bootstrapFileInput();

        $("select:not(.hardcodedSelect,.ui-datepicker-month,.ui-datepicker-year)").attr("disabled", "disabled").each(function () {
            $get(this.id).options.add(new Option("Loading...", 0));
        });

        // table row highlight
        $(document.body).on('click', '.highlightClass', function () { highlightRow($(this), false); });

        // textarea compute remaining characters
        $(document.body).on('keyup', 'textarea', function () { computeRemainingTextAreaCharacterCount(this); });

        // accordion

        $('.disabledAccordion').removeAttr('data-toggle');

        $(document.body).on('click', '.es-input', function () { clearSelectFilter(this); });


    })
    /* end - All onload functions */
}



function clearSelectFilter(obj) {
    //$(obj).editableSelect('clear');
}





function highlightRow(g) {
    $(g).siblings().children("td").removeClass("selected");
    $(g).children("td").addClass('selected');
}


/* Datepicker & date formatter */
//date formatter
function PadDate(d) {
    var vDt = new Date(d);

    var ds = d.split("/");
    var dy = ds[2];

    var mm = vDt.getMonth() + 1;
    var dd = vDt.getDate();
    var yyyy = vDt.getFullYear();

    if ((ds.length == 3) && (dd <= 31) && (mm <= 12)) {
        //goto closest year
        if (dy.length < 4) {
            if (yyyy < 1940) { yyyy = yyyy + 100; }
        }
        //end
        if (mm < 10) { mm = '0' + mm; }
        if (dd < 10) { dd = '0' + dd; }
        var xdate = mm + '/' + dd + '/' + yyyy;
        return xdate
    }
    else { return ""; }
}


function PadNoForwardDate(d) {
    var vDt = new Date(d);

    var ds = d.split("/");
    var dy = ds[2];

    var mm = vDt.getMonth() + 1;
    var dd = vDt.getDate();
    var yyyy = vDt.getFullYear();


    if ((ds.length == 3) && (dd <= 31) && (mm <= 12)) {
        var curDate = new Date();

        var cm = curDate.getMonth() + 1;
        var cd = curDate.getDate();
        var cyyyy = curDate.getFullYear();


        //goto closest year 
        if (dy.length < 4) {
            if (yyyy < 1940) { yyyy = yyyy + 100; }
        }
        //end
        if (mm < 10) { mm = '0' + mm; }
        if (dd < 10) { dd = '0' + dd; }
        if (cm < 10) { cm = '0' + cm; }
        if (cd < 10) { cd = '0' + cd; }

        var xdate;
        var mDate = new Date("01/01/1900");

        if (vDt > curDate || vDt < mDate) {
            xdate = cm + '/' + cd + '/' + cyyyy;
        }
        else {
            xdate = mm + '/' + dd + '/' + yyyy;
        }

        return xdate
    }
    else { return ""; }
}

/* Number validators & formatters */

function QtyListener(d) {
    d += "";

    x = d.split(",");
    x1 = x[0];
    x2 = x.length > 1 ? "," + x[1] : "";

    var c = /(\d+)(\d{3})/;

    while (c.test(x1)) {
        x1 = x1.replace(c, "$1,$2")
    }

    return x1 + x2
}

function ConvertToFormattedQuantity(d) {
    var e = d.replace(/\,/g, "");

    if (isValidNumba(e)) {
        var c = parseFloat(e);
        nStr = c.toFixed(0);

        return QtyListener(nStr);
    }
    else { return ""; }
}


function isValidNumba(c) {
    return !isNaN(parseFloat(c)) && isFinite(c)
}

function AmtListener(d) {
    d += "";

    x = d.split(".");
    x1 = x[0];
    x2 = x.length > 1 ? "." + x[1] : "";

    var c = /(\d+)(\d{3})/;

    while (c.test(x1)) {
        x1 = x1.replace(c, "$1,$2")
    }

    return x1 + x2
}

function Currencify(d) {
    var e = d.replace(/\,/g, "");
    if (isValidNumba(e)) {
        var c = new bigDecimal(e)//parseFloat(e);
        nStr = c.round(2)//c.toFixed(2);
        return AmtListener(nStr.getValue()); //AmtListener(nStr);
    }
    else { return ""; }
}
/* end - Number validators & formatters */


//Added by ITDJDV 8/17/2016
function isValidPaste(e, rgex) {
    var text = e.clipboardData.getData('Text')

    if (text != null) {
        var rx = new RegExp(rgex);
        var isValid = true;

        for (var i = 0; i < text.length; i++) {
            isValid = rx.test(text[i]);
            if (isValid == false) {
                return isValid;
            }
        }
    }

    return true;
}

function getSessionList(sessionName) {
    //if (sessionStorage[sessionName] != null && sessionStorage[sessionName] != "") {
    //    //        return JSON.parse(sessionStorage[sessionName]);
    //    return JSON.parse(sessionStorage[sessionName]);
    //}
    //else {
    //    return [];
    //}

    if (secureStorage.getItem(sessionName) != null && secureStorage.getItem(sessionName) != "") {
        return JSON.parse(secureStorage.getItem(sessionName));
    }
    else {
        return [];
    }
}

//Edited by ITDDDL 09/02/2016
function determineFileUpload(ctrlId, userId, requestType, lblStatus, doneFunctionName, attachmentListName, fileSize) {
    $('#' + ctrlId).fileupload({
        dataType: 'json',
        url: '/Upload/FileInputHandler.ashx?userId=' + userId + '&requestType=' + requestType,
        add: function (e, data) {
            var valid = true;
            var file = data.files[0];
            var attachmentList;
            try {
                attachmentList = JSON.parse(sessionStorage[attachmentListName]);
            }
            catch (ex) {
                attachmentList = null;
            }
            //Commented by ITDRSD so all file types will be accepted.
            //            var regex = /^.+\.((txt)|(jpg)|(xls)|(xlsx)|(doc)|(docx)|(pdf))$/i;
            //            if (!regex.test(file.name)) { //Check if valid file type
            //                $("#" + lblStatus).html("Invalid file Type.");
            //                valid = false;
            //            }
            if (file.name.length > 100) {
                $("#" + lblStatus).html("Filename too long.");
                valid = false;
            }
            else if (attachmentList != null) {
                if (arrayObjectIndexOf(attachmentList, file.name, "fileName") >= 0) { //Check if file already in list
                    $("#" + lblStatus).html("File already exists.");
                    valid = false;
                }
            }
            //added by itdrsd   
            else if (file.size > fileSize) {
                $("#" + lblStatus).html("Maximum file size of 10mb exceeded.");
                valid = false;
            }
            if (valid == true) {
                $("#" + lblStatus).html("Uploading...");
                data.submit();
            }
        },
        done: function (e, data) {
            //            var refObj = new Object();
            //            refObj.name = data.name;
            //            refObj.path = data.path;
            //            refObj.type = data.type;


            PageMethods.GetFileItem(data.result[0],
                function OnSuccess(result) {
                    window[doneFunctionName](result);
                    $("#" + lblStatus).html("Upload finished.");
                },
                function OnError(err) {
                    $("#" + lblStatus).html("Upload failed.");
                    if (!sessionErrorCatcher(err.get_message())) {
                        bootbox.alert(err.get_message());
                    }
                });
            //            $.each(data.result, function (index, file) {
            //                createAttachmentParameter(file);
            //                refereshFileProjectTable();
            //            });
        },
        fail: function (e, data) {
            $("#" + lblStatus).html("Upload failed.");
        }
    });
}

function arrayObjectIndexOf(array, searchTerm, property) {
    for (var i = 0, len = array.length; i < len; i++) {
        if (array[i][property] === searchTerm) return i;
    }
    return -1;
}


// modal
function initializeAllModals() {
    // success modal
    $('#modalSuccess').modal({ backdrop: 'static', keyboard: false });

    toggleEventForElement("#btnSuccessOK", "click", "", false);

    $("#successSpanMessage").html('');
    $("#modalSuccess").modal('hide');


    // confirm modal
    $('#modalConfirm').modal({ backdrop: 'static', keyboard: false });

    toggleEventForElement("#btnConfirmOK", "click", "", false);
    toggleEventForElement("#btnConfirmCancel", "click", "", false);

    $("#confirmSpanMessage").html('');
    $("#modalConfirm").modal('hide');

    // error modal
    $('#modalError').modal({ backdrop: 'static', keyboard: false });

    $("#errorSpanMessage").html('');
    $("#modalError").modal('hide');

    // timeout modal
    $('#modalTimeOut').modal({ backdrop: 'static', keyboard: false });

    $("#spanExpireMinutes").html('');
    $("#modalTimeOut").modal('hide');

    toggleEventForElement("#btnTimeOutOK", "click", "", false);

    // session extended modal
    $('#modalSessionExtended').modal({ backdrop: 'static', keyboard: false });

    $("#spanSessionExtendedMessage").html('');
    $("#modalSessionExtended").modal('hide');

    // session extended modal
    $('#modalSessionExpiring').modal({ backdrop: 'static', keyboard: false });

    $("#modalSessionExpiring").modal('hide');

    // session expired modal
    $('#modalSessionExpired').modal({ backdrop: 'static', keyboard: false });

    $("#modalSessionExpired").modal('hide');

}


function showSuccessModal(msg, functionToRunOnOK, callbackOK) {
    initializeAllModals();

    $("#successSpanMessage").html(msg);
    $("#modalSuccess").modal('show');

    $(document.body).on('click', '#btnSuccessOK', function (event) {
        //$("#btnSuccessOK").click(function () {
        if (functionToRunOnOK) {
            if (callbackOK) {
                functionToRunOnOK(callbackOK);
            }
            else {
                functionToRunOnOK();
            }


        }

        $("#modalSuccess").modal('hide');

    });
}

function showConfirmModal(msg, functionToRunOnOK, functionToRunOnCancel, callbackOK, callbackCancel) {
    initializeAllModals();

    $("#confirmSpanMessage").html(msg);
    $("#modalConfirm").modal('show');

    $(document.body).on('click', '#btnConfirmOK', function (event) {
        //$("#btnConfirmOK").click(function () {
        if (functionToRunOnOK) {
            if (callbackOK) {
                functionToRunOnOK(callbackOK);
            }
            else {
                functionToRunOnOK();
            }

        }
        else {
            showErrorModal("No function to run on OK click. Please Contact Your Administrator.");
        }

        $("#modalConfirm").modal('hide');

    });


    $(document.body).on('click', '#btnConfirmCancel', function (event) {
        //$("#btnConfirmCancel").click(function () {
        if (functionToRunOnCancel) {
            if (callbackCancel) {
                functionToRunOnCancel(callbackCancel);
            }
            else {
                functionToRunOnCancel();
            }

        }

        $("#modalConfirm").modal('hide');
    });

}

function showErrorModal(msg) {
    initializeAllModals();

    $("#errorSpanMessage").html(msg);
    $("#modalError").modal('show');
}


function showTimeOutModal(msg) {

    initializeAllModals();

    //$("#spanExpireMinutes").html(msg);
    $("#modalTimeOut").modal('show');

    $(document.body).on('click', '#btnTimeOutOK', function (event) {
        //$("#btnTimeOutOK").click(function () {
        extendSession();

        $("#modalTimeOut").modal('hide');

    });

    $(document.body).on('click', '#btnTimeOutCancel', function (event) {
        //$("#btnTimeOutCancel").click(function () {
        showSessionExpringModal();

        $("#modalTimeOut").modal('hide');

    });

    sessionCountDown(msg - 1, 59, 'spanExpireMinutes', true);
}


function showSessionExtendedModal(msg) {
    initializeAllModals();

    $("#spanSessionExtendedMessage").html(msg);
    $("#modalSessionExtended").modal('show');


}

function showSessionExpringModal(msg) {
    initializeAllModals();

    $("#modalSessionExpiring").modal('show');
}

function showSessionExpiredModal() {
    initializeAllModals();

    $("#modalSessionExpired").modal('show');

    $(document.body).on('click', '#btnSessionExpiredOK', function (event) {
        //$("#btnSessionExpiredOK").click(function () {
        window.location = "/Views/ErrorPages/ExpiredSession.aspx";

        $("#modalSessionExpired").modal('hide');

    });
}


function sessionCountDown(minutes, seconds, lblTimer, running) {
    if (running) {
        if (!countdownRun) {
            CountDown.Start(minutes, seconds, lblTimer, running)
            countdownRun = true;
        }
        else {
            CountDown.Resume(minutes, seconds);
        }


    }
    else {
        CountDown.Pause();
    }



}

// accordion

function toggleAccordionCollapse(accordionID, enabled) {
    if (enabled) {
        $('#' + accordionID).attr('data-bs-toggle', 'collapse');
        $('#' + accordionID).children('.card-header').removeClass('btn-lbp-warm-gray').addClass('btn-lbp-green');
    }
    else {
        $('#' + accordionID).removeAttr('data-bs-toggle');
        $('#' + accordionID).children('.card-header').removeClass('btn-lbp-green').addClass('btn-lbp-warm-gray');
    }
}


function closeTab() {
    sessionStorage.clear();
}