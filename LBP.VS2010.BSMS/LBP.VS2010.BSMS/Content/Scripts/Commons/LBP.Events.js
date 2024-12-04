// dropdowns

function loadDropdown(ddl, result, enabled, defaultValue, defaultText, callback) {
    if (result.length > 10) {
        // transform to editable select
        loadEditableDDL(ddl, result, enabled, defaultValue, defaultText, callback);
    }
    else {

        clearDropdown(ddl);

        $.each(result, function () {
            document.getElementById(ddl).options.add(new Option(this.description, this.value));
        });

        if (defaultValue != null) {
            setDropdownDefaultValue(ddl, defaultValue);
        }
        else {
            setDropdownDefaultValue(ddl, "");
        }

        if (defaultText != null) {
            setDropdownDefaultText(ddl, defaultText);
        }

        if (enabled != null) {
            enableControl(ddl, enabled);
        }
        else {
            enableControl(ddl, true);
        }

        if (callback != undefined) {
            callback();
        }
    }
}

function clearDropdown(ddl) {
    var dd = document.getElementById(ddl);

    if ($(dd).hasClass("es-input")) {
        $(dd).editableSelect('destroy');

        dd = document.getElementById(ddl);
    }

    dd.options.length = 0;
}


function getDropdownValue(dropdownName) {
    var dd = document.getElementById(dropdownName);

    if ($(dd).hasClass("es-input")) {
        return getEditableDDLValue(dropdownName);
    }
    else {
        return dd.options[dd.selectedIndex].value;
    }
}

function getDropdownText(dropdownName) {
    var dd = document.getElementById(dropdownName);

    if ($(dd).hasClass("es-input")) {
        return getEditableDDLText(dropdownName);
    }
    else {
        return unquoteattr(dd.options[dd.selectedIndex].innerHTML);
    }

}



function setDropdownDefaultValue(dropdownName, defaultValue, placeholderText, callback) {
    var dd = document.getElementById(dropdownName);

    if ($("#" + dropdownName).hasClass("es-input")) {
        setEditableDDLValue(dropdownName, defaultValue, placeholderText, callback);
    }
    else {
        $("#" + dropdownName).val(checkDropdownValue(dd, defaultValue)).change();
    }

}

function setDropdownDefaultText(dropdownName, defaultText, callback) {
    var dd = document.getElementById(dropdownName);

    if ($("#" + dropdownName).hasClass("es-input")) {
        setEditableDropdownText(dropdownName, defaultText, callback);
    }
    else {
        for (var i = 0; i < dd.options.length; i++) {
            if (dd.options[i].text == defaultText) {
                dd.options[i].selected = true;
                $("#" + dropdownName).change();

                return;
            }
        }
    }
}

function checkDropdownValue(ddl, defaultValue) {
    var setValue = "";

    for (var i = 0; i < ddl.options.length; i++) {
        if (ddl.options[i].value == defaultValue) {
            setValue = ddl.options[i].value;

            return setValue;
        }
    }

    return setValue;
}


function disableSelectOptions(ctrlOption, disabledOptions) {



    if ($("#" + ctrlOption).hasClass("es-input")) {
        disableSelectOptionsValueEditable(ctrlOption, disabledOptions);
    }
    else {
        var option = document.getElementById(ctrlOption).getElementsByTagName("option");
        $(option).each(function () {
            if ($.inArray($(this).val().trim(), disabledOptions) != -1) {
                this.disabled = true;
                $(this).attr("hidden", "true");
            }
            else {
                this.disabled = false;
                $(this).removeAttr('hidden');
            }
        });
    }


}


function setRequiredControls(ctrl, enabled) {
    $.each(ctrl, function () {
        toggleRequiredConditional($("#" + this), enabled)
    });
}


function loadYear(ddl, defaultValue, rangeFrom, rangeTo) {
    var year = [];

    for (var i = rangeFrom; i > rangeTo; i--) {
        year.push({ description: i, value: i });
    }
    loadDropdown(ddl, year, defaultValue);
}


// textboxes

function getTextboxValue(txtboxName) {
    return document.getElementById(txtboxName).value.trim();
}

function setTextboxValue(txtboxName, txtValue) {
    var txt = document.getElementById(txtboxName);

    if (txtValue == null) {
        $(txt).val((txtValue));
    }
    else {
        $(txt).val(unquoteattr(txtValue));
    }

    $(txt).change();
}

// radio buttons

function getRadioButtonValue(rblName) {
    var selectedValue = $("input[name=" + rblName + "]:checked").val();


    return selectedValue;
}


function getRadioButtonValueYesNoBoolean(rblName, trueValue) {
    var selectedValue = $("input[name=" + rblName + "]:checked").val();

    if (selectedValue == trueValue) {
        return true;
    }
    else {
        return false;
    }


}

function getRadioButtonText(rblName) {
    var selectedRadio = $("input[name=" + rblName + "]:checked");

    var selectedText = $("label[for='" + $(selectedRadio).prop('id') + "']").text()

    return $.trim(selectedText);
}

function setRadioButtonValue(rblName, defaultValue) {
    var rd = $("input[name=" + rblName + "]");


    for (var i = 0; i < rd.length; i++) {
        if (rd[i].value == defaultValue) {

            rd[i].checked = true;
            $(rd[i]).click();
            rd.trigger("change");
            return;
        }
    }
}

function deselectRadioButton(rblName, isRequired) {
    var set = $("input:radio[name='" + rblName + "']");
    var length = set.length;

    set.each(function (i, e) {
        this.checked = false;

        if (i === (length - 1)) {
            toggleRequiredRadio(this, isRequired);
        }
    });
}



// checkboxes

function getCheckboxListValues(input) {
    var checkBoxListValue = [];

    var child = $("#" + input + "  input:checked")

    child.each(function () {
        checkBoxListValue.push($(this).val());
    });

    return checkBoxListValue;
}

function getCheckBoxCheckedValue(chkName) {
    return document.getElementById(chkName).checked;
}

function setCheckBoxCheckedValue(chkName, chkValue) {
    // this is used to click the checkbox

    var chk = document.getElementById(chkName);

    if (chkValue == true || (chkValue == false && chk.checked)) {
        if ($(chk).prop('disabled')) {
            $(chk).removeAttr('disabled');
            $(chk).click();
            $(chk).attr('disabled', 'disabled');
        }
        else {
            $(chk).click();
        }
    }

}

// labels

function setLabelText(lblName, lblValue) {
    var lbl = document.getElementById(lblName);
    lbl.innerHTML = lblValue;
    $(lbl).change();
}

function getLabelText(lblName) {
    return unquoteattr(document.getElementById(lblName).innerHTML);
}

// text area

function setTextAreaValue(txtboxName, txtValue) {
    var txt = document.getElementById(txtboxName);
    $(txt).val(txtValue).change();
    $(txt).keyup();

}

// error/success messages

function showError(msg) {
    $('#lblError').empty();
    $('#lblError').html(msg);
    $('#lblError').show();
    $('#divError').show();
    $('#divError').focus();
    $('#divSuccess').show();
    $('#divSuccess').focus();

}
function showSpecificError(msg, errorMessageID, divErrorMessageID) {
    $('#' + errorMessageID).empty();
    $('#' + errorMessageID).html(msg);
    $('#' + errorMessageID).show();
    $('#' + divErrorMessageID).show();
    $('#' + divErrorMessageID).focus();
}

function hideSpecificError(errorMessageID) {
    $('#' + errorMessageID).empty();
    $('#' + errorMessageID).hide();
}


function appendErrorMsg(msg) {
    if ($('#lblError').html() == "") {
        showError(msg);
    }
    else {
        if ($('#lblError').html() != msg) {
            if (~$('#lblError').html().indexOf(msg) == 0) {
                $('#lblError').append("<br/>" + msg);
            }
        }
    }
}


function hideError() {
    $('#lblError').empty();
    $('#lblError').hide();
}

function hideSpecificError(errorMessageID) {
    $('#' + errorMessageID).empty();
    $('#' + errorMessageID).hide();
}


function removeErrorMsg(msg) {
    if ($('#lblError').html() == msg) {
        hideError();
    }
    else {
        $('#lblError').html($('#lblError').html().replace("<br>" + msg, ""));
    }
}

function removeErrorMsgWithIdentifier(msg, identifierStart, identifierEnd) {
    var existingMsg = $('#lblError').html()

    var existingErrMsg1 = existingMsg.substr(existingMsg.indexOf(identifierStart) + identifierStart.length);
    var currentErrMsgValue = existingErrMsg1.substr(0, existingErrMsg1.indexOf("'"));

    var existingErrMsg = identifierStart + currentErrMsgValue + identifierEnd;

    if ($('#lblError').html() == existingErrMsg) {
        hideError();
    }
    else {
        var newErrorMessage;

        if (existingMsg.indexOf(existingErrMsg) == 0) {
            newErrorMessage = existingMsg.replace(existingErrMsg + "<br>", "")
        }
        else {
            newErrorMessage = existingMsg.replace("<br>" + existingErrMsg, "")
        }

        $('#lblError').html(newErrorMessage);
    }
}


// asp control ids

function getASPControlClientID(literalID) {
    var aspCtrl = $("[id$=" + literalID + "]");

    return aspCtrl[0].id;
}

function getASPRadioButtonListID(input) {
    return input.parentNode.parentNode.parentNode.parentNode.id
}



// divs/elements toggling


function showDiv(divNames, visible) {
    $.each(divNames, function (index, value) {
        var divID = "#" + value;
        if (visible) {
            $(divID).removeAttr('hidden');
        }
        else {
            $(divID).attr("hidden", "true");
        }
    });
}

function disableDivControl(ctrlName, enabled) {
    var ctrl = document.getElementById(ctrlName).getElementsByTagName('*');

    for (var i = 0; i < ctrl.length; i++) {
        ctrl[i].disabled = enabled;
    }
}

function disableDivControlForOthers(ctrlName, enabled) {
    var ctrl = document.getElementById(ctrlName).getElementsByTagName('*');

    for (var i = 0; i < ctrl.length; i++) {
        ctrl[i].disabled = enabled;

        if (ctrl[i].type == "text") {
            setTextboxValue(ctrl[i].id.toString(), "");
        }

        toggleRequired($(ctrl[i]), !enabled);
    }
}

function disableDivControlForTextbox(ctrlName, enabled) {
    var ctrl = document.getElementById(ctrlName).getElementsByTagName('*');

    for (var i = 0; i < ctrl.length; i++) {

        if (ctrl[i].type == "text") {
            ctrl[i].disabled = enabled;
            setTextboxValue(ctrl[i].id.toString(), "");
            toggleRequired($(ctrl[i]), !enabled);
        }


    }
}

function disableDivControlFields(ctrlname, enabled) {
    var ctrl = document.getElementById(ctrlname).getElementsByTagName('*');

    for (var i = 0; i < ctrl.length; i++) {
        $(ctrl[i]).attr('disabled', enabled);
        $(ctrl[i]).removeClass("required");
        $(ctrl[i]).removeClass('requiredField');
    }
}


function enableControl(ctrlName, enabled) {
    var ctrl = document.getElementById(ctrlName);

    $(ctrl).attr('disabled', !enabled);

}

function disableControls(ctrlName, disable) {
    $.each(ctrlName, function (index, value) {
        $("#" + value).attr('disabled', disable);
    });
}

function toggleInputReadonly(id, isReadonly) {
    var inputElement = document.getElementById(id);
    if (isReadonly) {
        inputElement.setAttribute("readonly", "true");
    }
    else {
        inputElement.removeAttribute("readonly");
    }
}



// required

function toggleRequired(input, isRequired) {
    var inputType = input.type || input.attr('type') || input.prop('type');

    if (inputType === 'radio' || inputType === 'checkbox') {
        handleRadioChange(input.attr('name'), inputType);
    }
    else {
        if (isRequired) {
            $(input).addClass("requiredField");
        }
        else {
            $(input).removeClass("requiredField");
        }
    }
}

function handleRadioChange(groupName, inputType) {
    const radioOptions = document.querySelectorAll('input[type="' + inputType + '"].required[name="' + groupName + '"]');
    let isRequired = true;

    radioOptions.forEach(function (radio) {
        if (radio.checked) {
            isRequired = false;
            return;
        }
    });

    radioOptions.forEach(function (radio) {
        if (!isRequired) {
            radio.classList.remove('requiredField');
        } else {
            if (!radio.classList.contains('requiredField')) {
                radio.classList.add('requiredField');
            }

        }
    });
}

function toggleRequiredRadio(input, isRequired) {

    var rbl = $(input).closest(".requiredTable");

    if (rbl != undefined) {
        toggleRequiredTable(rbl, isRequired)
    }

}

function toggleRequiredTable(input, isRequired) {
    if (isRequired) {
        $(input).addClass("requiredTableField");
    }
    else {

        $(input).removeClass("requiredTableField");
    }
}


function toggleRequiredConditional(input, isRequired) {
    if (isRequired) {
        $(input).addClass("required");

        if (!$(input).val()) {
            $(input).addClass("requiredField");
        }
    }
    else {
        $(input).removeClass("required");
        $(input).removeClass("requiredField");
    }
}

function toggleRequiredCheckbox(input) {

    var div = $(input).closest(".requiredTable");
    var child = $(div).children().find(".requiredCheckbox:checked");

    if (child.length > 0) {
        toggleRequiredTable(div, false);
    } else {
        toggleRequiredTable(div, true);
    }

}
// loading screen

function loadingScreen(isFadeOut, fadeTime) {
    ((isFadeOut) ? $(".divLoader").fadeOut(fadeTime) : $(".divLoader").fadeIn(fadeTime));
}

// control mirrorring

function mirrorText(input, target, checkbox) {
    if (checkbox.checked) {
        setTextboxValue(getASPControlClientID(target), input);
    }
}

function mirrorDropdown(input, target, checkbox) {
    if (checkbox.checked) {
        setDropdownDefaultValue(document.getElementById(target), input);
    }
}

function mirrorRadioButton(input, target, checkbox) {
    if (checkbox.checked) {
        selectRadioButton(target, input);
    }
}

function mirrorCheckBox(input, target, checkbox) {
    if (checkbox.checked) {
        setCheckedValue(target, input);
    }
}

function unquoteattr(s) {
    //    /* 
    //    Note: this can be implemented more efficiently by a loop searching for 
    //    ampersands, from start to end of ssource string, and parsing the 
    //    character(s) found immediately after after the ampersand. 
    //    */
    //    s = ('' + s); /* Forces the conversion to string type. */
    //    /* 
    //    You may optionally start by detecting CDATA sections (like 
    //    `<![CDATA[` ... `]]>`), whose contents must not be reparsed by the 
    //    following replacements, but separated, filtered out of the CDATA 
    //    delimiters, and then concatenated into an output buffer. 
    //    The following replacements are only for sections of source text 
    //    found *outside* such CDATA sections, that will be concatenated 
    //    in the output buffer only after all the following replacements and 
    //    security checkings. 
    //  
    //    This will require a loop starting here. 
    //  
    //    The following code is only for the alternate sections that are 
    //    not within the detected CDATA sections. 
    //    */
    //    /* Decode by reversing the initial order of replacements. */
    //    s = s
    //                    .replace(/\r\n/g, '\n') /* To do before the next replacement. */
    //                    .replace(/[\r\n]/, '\n')
    //                    .replace(/&#13;&#10;/g, '\n') /* These 3 replacements keep whitespaces. */
    //                    .replace(/&#1[03];/g, '\n')
    //                    .replace(/&#9;/g, '\t')
    //                    .replace(/&gt;/g, '>') /* The 4 other predefined entities required. */
    //                    .replace(/&lt;/g, '<')
    //                    .replace(/&quot;/g, '"')
    //                    .replace(/&apos;/g, "'")
    //                    ;
    //    /* 
    //    You may add other replacements here for predefined HTML entities only 
    //    (but it's not necessary). Or for XML, only if the named entities are 
    //    defined in *your* assumed DTD. 
    //    But you can add these replacements only if these entities will *not* 
    //    be replaced by a string value containing *any* ampersand character. 
    //    Do not decode the '&amp;' sequence here ! 
    //  
    //    If you choose to support more numeric character entities, their 
    //    decoded numeric value *must* be assigned characters or unassigned 
    //    Unicode code points, but *not* surrogates or assigned non-characters, 
    //    and *not* most C0 and C1 controls (except a few ones that are valid 
    //    in HTML/XML text elements and attribute values: TAB, LF, CR, and 
    //    NL='\x85'). 
    //  
    //    If you find valid Unicode code points that are invalid characters 
    //    for XML/HTML, this function *must* reject the source string as 
    //    invalid and throw an exception. 
    //  
    //    In addition, the four possible representations of newlines (CR, LF, 
    //    CR+LF, or NL) *must* be decoded only as if they were '\n' (U+000A). 
    //  
    //    See the XML/HTML reference specifications ! 
    //    */
    //    /* Required check for security! */

    //    /*var found = /&[^;])*;?/.match(s);
    //    if (found.length >0 && found[0] != '&amp;') 
    //    throw 'unsafe entity found in the attribute literal content';  */


    //    /* This MUST be the last replacement. */

    s = s.replace(/&amp;/g, '&');
    //    /* 
    //    The loop needed to support CDATA sections will end here. 
    //    This is where you'll concatenate the replaced sections (CDATA or 
    //    not), if you have splitted the source string to detect and support 
    //    these CDATA sections. 
    //  
    //    Note that all backslashes found in CDATA sections do NOT have the 
    //    semantic of escapes, and are *safe*. 
    //  
    //    On the opposite, CDATA sections not properly terminated by a 
    //    matching `]]>` section terminator are *unsafe*, and must be rejected 
    //    before reaching this final point. 
    //    */
    return s;
}


// FOR AJAX PAGEMETHOD CALLS

// USE THIS FOR PURELY JS MANIPULATION. IF WITH PAGEMETHODCALL USE AJAXWRAPPER
function toggleEventForElement(element, event, functionName, toggle, args) {
    if (toggle == true) {
        $(document.body).on(event, '#' + $(element).attr('id'), function (event) {
            if (args != null) {
                functionName(this, args);
            }
            else {
                functionName(this);
            }
        });
    }
    else {
        $(document.body).off(event, '#' + $(element).attr('id'));
        //$(element).unbind(event);
    }
}

// NEED TO USE THIS TOGETHER WITH toggleEventForElement FOR ALL PAGEMETHOD CALLS

function AJAXWrapPageMethodCall(obj, functionToCall) {
    PageMethods.IsSessionExpired(function OnSuccess(result) {
        window[functionToCall]();

        // reset js session timeout timer
        sessSetInterval();
        sess_lastActivity = new Date();
    },
        function OnError(err) {
            if (!sessionErrorCatcher(err.get_message())) {
                //alert('boom');
            }
        }
    );
}

function sessionErrorCatcher(errMsg) {
    if (errMsg == "Session Expired.") {
        window.location = window.location.href;
        return true;
    }
    return false;
}

function exceptionError(bodyMessage) {
    bootbox.dialog({
        title: '<span class="exceptionMessageHeader">IULS Exception Error! </span>',
        message: '<span class="exceptionMessage">' + bodyMessage + '</span>'
    });
}


//ITDMMB 
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}
//END ADDED BY ITDMMB 

function setTextboxCompare(ctrl, oldTxtValue, NewTextValue, display) {
    var show;
    if (display == false) {
        show = display;
    } else {
        if ($("#lbl" + ctrl).length) {
            $("#lbl" + ctrl).remove();
        }
        if (oldTxtValue != NewTextValue) {
            if (oldTxtValue.toString() != "") {
                show = true;
            } else {
                show = false;
            }
        } else {
            show = false;
        }
    }

    if ($("#" + ctrl).is('select')) {
        setDropdownDefaultValue(document.getElementById(ctrl), NewTextValue);
        if (oldTxtValue != "") {
            oldTxtValue = $("#" + ctrl + ' option[value= ' + oldTxtValue + ']').text();
        }
    } else if ($("#" + ctrl).is('label')) {
        setLabelText(ctrl, NewTextValue);
    } else if ($("#" + ctrl).is(':checkbox')) {
        $("#" + ctrl).prop("checked", (NewTextValue) ? true : false)
        if (oldTxtValue != "") {
            oldTxtValue = (oldTxtValue) ? true : false
        }
    } else {
        setTextboxValue(ctrl, NewTextValue);
    }
    if (show) {
        $("#" + ctrl).after(function () {
            var br = '<br/>';

            var label = '<label id="lbl' + ctrl + '">';
            label += '<em class="legend-small">' + oldTxtValue + '</em>';
            label += '</label>';
            if ($("#" + ctrl).is(':checkbox')) {
                return br + label;
            } else {
                return label;
            }
        });
    } else {
        $("#" + ctrl).after(function () {
            var label = '<label id="lbl' + ctrl + '" hidden > ' + oldTxtValue.toString().trim() + '';
            label += '</label>';

            return label;
        });
    }
}

function setDropdownValue(dropdownName, defaultValue) {
    if ($('#' + dropdownName).hasClass("es-input")) {
        setTextboxValue(dropdownName, defaultValue);
    }
    else {
        setDropdownDefaultValue(dropdownName, defaultValue);
    }
}