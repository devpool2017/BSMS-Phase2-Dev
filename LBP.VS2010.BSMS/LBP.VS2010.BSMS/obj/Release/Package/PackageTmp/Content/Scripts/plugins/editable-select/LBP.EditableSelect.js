function loadEditableDDL(ddl, result, enabled, defaultValue, defaultText, callback) {
    if ($('#' + ddl).hasClass("es-input")) {
        $('#' + ddl).editableSelect('destroy');
    }

    clearDropdown(ddl);

    $.each(result, function () {
        document.getElementById(ddl).options.add(new Option(this.description, this.value));
    });

    setEditableDDLValue(ddl, defaultValue, defaultText, callback);

    if (enabled != null) {
        enableControl(ddl, enabled);
    }
    else {
        enableControl(ddl, true);
    }
}

function clearDropdownEditableSelect(ddl) {
    $('#' + ddl).editableSelect('clear');
}


function setEditableDDLValue(ddl, value, defaultText, func) {
    if (value == null) {
        $('#' + ddl).editableSelect('clearSelect')
         .on('select.editable-select', function (e, li) {
             if (func != undefined) {
                 func(li);
             }
         });

     } else {

        $('#' + ddl).editableSelect('setValue', value)
          .on('select.editable-select', function (e, li) {
              if (func != undefined) {
                  func(li);
              }
          });
    }
}

function setEditableDropdownText(ddl, defaultText, func) {
    if (defaultText == null) {
        $('#' + ddl).editableSelect()
         .on('select.editable-select', function (e, li) {
             if (func != undefined) {
                 func(li);
             }
         });
         //$('#' + ddl).prop('placeholder', defaultText);
         $('#' + ddl).val('').editableSelect('filter');
    } else {
        $('#' + ddl).editableSelect('setText', defaultText)
          .on('select.editable-select', function (e, li) {
              if (func != undefined) {
                  func(li);
              }
          });
    }
}


function getEditableDDLValue(ddl) {
    var i = '';
    $('#' + ddl).next().find('li').filter(function () {
        if ($(this).text() == getTextboxValue(ddl)) {         

            i = $(this).attr('data-val');
        }
    });
    return i;
}

function getEditableDDLText(ddl) {
    var i = '';
    $('#' + ddl).next().find('li').filter(function () {
        if ($(this).text() == getTextboxValue(ddl)) {
            i = $(this).text();
        }
    });
    return i;
}

function disableSelectOptionsValueEditable(ctrlOption, disabledOptions) {
    var option = $("#" + ctrlOption).parent().children("ul");
    $(option).each(function () {
        $(this).find('li').each(function () {            
            if ($.inArray($("#" + ctrlOption + $(this).val()).attr('data-val'), disabledOptions) != -1) {
                this.disabled = true;
                $(this).attr("hidden", "true");
            }
            else {
                this.disabled = false;
                $(this).removeAttr('hidden');
            }
        });
    });
}
