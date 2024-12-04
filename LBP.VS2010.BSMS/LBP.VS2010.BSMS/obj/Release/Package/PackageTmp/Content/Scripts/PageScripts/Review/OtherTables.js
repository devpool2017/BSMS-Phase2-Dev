$(document).ready(function () {
    loadingScreen(true, 0);
    //loadDropdownlist();
    toggleEventForElement("#btnSearch", "click", checkDDLValue, true);
});

function checkDDLValue() {
    hideError();
    loadingScreen(false, 0);
    tableName = getDropdownValue("ddlTable");
    PageMethods.checkDDLValue(tableName,
        function (data) {
            if (data != null) {
                var index = 1;
                var itemsPerPage = 10;
                var list = [];
                $("#tBodyTableList").empty();
                for (var r = 0; r < data.length; r++) {
                    list.push(data[r]);
                }
                secureStorage.setItem('list', JSON.stringify(list));
                allItems = data.length;

                createTable("divTableList", "tblTableList", "tBodyTableList", list, index, itemsPerPage, allItems, "list", "List");
            }
            else {
                showSpecificError(data, "lblModalErrorMessage", "divModalError");
            }
            loadingScreen(true, 1000);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });

    loadingScreen(true, 0);
}


