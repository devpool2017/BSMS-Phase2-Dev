$(document).ready(function () {
    loadAuditTrail();
    checkDDLValue();
    toggleEventForElement("#ddlSearchBy", "change", checkDDLValue, true);
    toggleEventForElement("#btnSearch", "click", AJAXWrapPageMethodCall, true, 'loadAuditTrail');
});

function loadAuditTrail() {
    hideError();
    loadingScreen(false, 0);

    var param = {};

    param.searchBy = getDropdownValue("ddlSearchBy")

    switch (param.searchBy) {
        case "DomainID":
            param.DomainID = getTextboxValue("txtSearchValue");
            break;
        case "TableName":
            param.TableName = getTextboxValue("txtSearchValue");
            break;
        case "ActionType":
            param.ActionType = getTextboxValue("txtSearchValue");
            break;
        case "AuditTrailDate":
            param.AuditTrailDate = getTextboxValue("txtSearchValue");
            break;
    }

    PageMethods.LoadAuditTrail(param,
        function OnSuccess(result) {
            if (result) {
                if (!Array.isArray(result)) {
                    showError(result.errMsg);
                }
                else {
                    var index = 1;
                    var itemsPerPage = 10;
                    var list = [];
                    $("#tBodyAuditTrail").empty();
                    for (var r = 0; r < result.length; r++) {
                        list.push(result[r]);
                    }
                    secureStorage.setItem('list', JSON.stringify(list));
                    allItems = result.length;

                    createTable("divAuditTrail", "tblAuditTrail", "tBodyAuditTrail", list, index, itemsPerPage, allItems, "list", "List");
                }
            }
            else {
                showError('There was a system problem while processing your request. Please verify if the transaction was posted correctly and report the problem to IT Support.');
            }

            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

function checkDDLValue() {
    setTextboxValue("txtSearchValue", "");
    var sType = getDropdownValue("ddlSearchBy");
    disableDivControl(["divSearchValue"], sType == "All");
    document.getElementById('txtSearchValue').type = (sType == "AuditTrailDate" ? 'date' : 'text');
}