$(document).ready(function () {

    loadingScreen(true, 0);
    //showDiv(['divForGH'], false);
    toggleEventForElement('#btnEditDate', 'click', EditDate, true);
    toggleEventForElement('#btnCancel', 'click', CancelEditDate, true);
    toggleEventForElement('#btnSaveDate', 'click', AJAXWrapPageMethodCall, true, 'ChangeDates');
    toggleEventForElement('#btnApprove', 'click', AJAXWrapPageMethodCall, true, 'ApproveCPADates');
    toggleEventForElement('#btnReject', 'click', AJAXWrapPageMethodCall, true, 'rejectCPADates');
    var deferred = $.Deferred();

    PageMethods.OnLoadData(
        function OnSuccess(result) {
            deferred.resolve(result);
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    $.when(deferred).done(async function (data) {
        var currentUser = data.currentUser;
        var canUpdate = data.CanUpdate;
        var canApprove = data.CanApprove;
        sessionStorage.GroupCode = currentUser.GroupCode;
        sessionStorage.RoleId = currentUser.RoleID;
        try {
            var access = {
                CanModify: canUpdate,
                CanApprove: canApprove
            };

            showDiv(['divForTA'], true);
            if (canUpdate) {
                loadDate();
                showDiv(['divFooter'], true);
            }
            if (canApprove) {
                showDiv(['divFooter'], false);
                loadDate();
                DisplayForApprovalDates();
            }

        } catch (e) {
            showErrorModal(e);
        }

        loadingScreen(true, 0);
    }).fail(function (e) {
        showErrorModal(e);
        loadingScreen(true, 0);
    });

});

function EditDate() {
    var inEdit = sessionStorage.inEdit;
    if (inEdit == "true") {
        showErrorModal("Edit is disabled due to pending request.")
    }
    else {
        disableControls(['txtStartDate'], false);
        disableControls(['txtEndDate'], false);
        $(divSave).removeAttr('hidden');
        $(divEdit).attr("hidden", "true");
    }

}

function CancelEditDate() {
    loadDate();
    hideSpecificError('lblError');
}

function loadDate() {
    var deferred = $.Deferred();

    $(divEdit).removeAttr('hidden');
    $(divSave).attr("hidden", "true");
    PageMethods.GetCPADates(
        function OnSuccess(result) {
            if (result != null) {
                sessionStorage.inEdit = result.InEdit;
                sessionStorage.PAStartDate = result.PAStartDate;
                sessionStorage.PAEndDate = result.PAEndDate;
                setTextboxValue('txtStartDate', result.PAStartDate);
                setTextboxValue('txtEndDate', result.PAEndDate);
                disableControls(['txtStartDate'], true);
                disableControls(['txtEndDate'], true);

                deferred.resolve(result);
            }
            else {
                deferred.reject('There was a system problem while processing your request. Please verify if the transaction was posted correctly and report the problem to IT Support.');
            }
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    return deferred.promise();
}

function ApproveCPADates() {
    hideError();
    showConfirmModal("approve", function () {
        loadingScreen(false, 0);

        PageMethods.ApproveCPADates(
            function (result) {
                if (result == '') {
                    showSuccessModal('approved', function () {
                        loadDate();
                    });
                    DisplayForApprovalDates();

                }
                else {
                    showSpecificError(result, 'lblError', 'divError');
                }
                loadingScreen(true, 0);
            },
            function (err) {
                showErrorModal(err.get_message());
                loadingScreen(true, 0);
            });
    });
}


function rejectCPADates() {
    hideError();
    showConfirmModal("reject", function () {
        loadingScreen(false, 0);

        PageMethods.RejectCPADates(
            function (result) {
                if (result == '') {
                    showSuccessModal('rejected', function () {
                        loadDate();
                    });
                    DisplayForApprovalDates();
                }
                else {
                    showSpecificError(result, 'lblError', 'divError');
                    loadingScreen(true, 0);
                }
                loadingScreen(true, 0);
            },
            function (err) {
                showErrorModal(err.get_message());
                loadingScreen(true, 0);
            });
    });
}

function ChangeDates() {
    hideError();
    loadingScreen(false, 0);

    var newStartDate;
    var newEndDate;
    var postData = {
        PAStartDate: new Date(getTextboxValue('txtStartDate')).toLocaleDateString(),
        PAEndDate: new Date(getTextboxValue('txtEndDate')).toLocaleDateString()
    }
    var exData = {
        PAStartDate: new Date(sessionStorage.PAStartDate).toLocaleDateString(),
        PAEndDate: new Date(sessionStorage.PAEndDate).toLocaleDateString()
    }
    var bool = true;
    for (const key in postData) {
       
            if (postData[key] !== exData[key]) {
                bool = false
                break;
            }
    }

    if (bool) {
        showSpecificError('There are no changes made.', 'lblError', 'divError');
        loadingScreen(true, 0);
        return;
    }
    else {
        PageMethods.EditCPADates(postData,
            function (result) {
                if (result == '') {
                    showSuccessModal('submitted for approval', function () {
                        loadDate();
                    });

                    loadingScreen(true, 0);
                }
                else {
                    showSpecificError(result, 'lblError', 'divError');
                    loadingScreen(true, 0);
                }
            },
            function (err) {
                showSpecificError(err.get_message, 'lblError', 'divError');
                loadingScreen(true, 0);
            });
    }
}


function DisplayForApprovalDates() {
    hideError();
    loadingScreen(false, 0);
    //showDiv(['divApproval'], true);
    var regionCode = sessionStorage.GroupCode
    PageMethods.GetMergeDetails(regionCode,
        function OnSuccess(result) {
            if (result != null) {
                // $(divFooter2).removeAttr('hidden');
                var index = 1;
                var itemsPerPage = 50;
                var list = [];

                $('#tBodyApproval').empty();
                for (var r = 0; r < result.length; r++) {
                    list.push(result[r]);
                }

                secureStorage.setItem('listApproval', JSON.stringify(list));
                allItems = result.length;

                createTable('divApproval', 'tblApproval', 'tBodyApproval', list, index, itemsPerPage, allItems, 'listApproval', 'EncodingDates');
                showDiv(['divForGH'], true);
            }
            else {
                showDiv(['divForGH'], false);
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}
