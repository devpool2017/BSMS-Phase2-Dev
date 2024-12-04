$(document).ready(function () {
    loadUserActivity();
    checkDDLValue();
    toggleEventForElement("#ddlSearchBy", "change", checkDDLValue, true);
    toggleEventForElement("#btnSearch", "click", AJAXWrapPageMethodCall, true, 'loadUserActivity');
});

function loadUserActivity() {
    hideError();
    loadingScreen(false, 0);

    var param = {};

    param.searchBy = getDropdownValue("ddlSearchBy")

    switch (param.searchBy) {
        case "ModuleName":
            param.ModuleName = getTextboxValue("txtSearchValue");
            break;
        case "ActivityType":
            param.ActivityType = getTextboxValue("txtSearchValue");
            break;
        case "UserName":
            param.UserName = getTextboxValue("txtSearchValue");
            break;
        case "ActivityDate":
            param.ActivityDate = getTextboxValue("txtSearchValue");
            break;
        case "Browser":
            param.Browser = getTextboxValue("txtSearchValue");
            break;
    }

    PageMethods.LoadUserActivity(param,
        function OnSuccess(result) {
            if (result) {
                if (!Array.isArray(result)) {
                    showError(result.errMsg);
                }
                else {
                    var index = 1;
                    var itemsPerPage = 10;
                    var list = [];
                    $("#tBodyUserLog").empty();
                    for (var r = 0; r < result.length; r++) {
                        list.push(result[r]);
                    }
                    secureStorage.setItem('list', JSON.stringify(list));
                    allItems = result.length;

                    createTable("divUserLog", "tblUserLog", "tBodyUserLog", list, index, itemsPerPage, allItems, "list", "List");
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
    document.getElementById('txtSearchValue').type = (sType == "ActivityDate" ? 'date' : 'text');
}