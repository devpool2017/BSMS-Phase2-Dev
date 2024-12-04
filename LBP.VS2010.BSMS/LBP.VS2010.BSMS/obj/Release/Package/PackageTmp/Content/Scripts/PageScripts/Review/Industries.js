$(document).ready(function () {
    loadingScreen(false, 0);
    loadIndustriesGridview();
});

function loadIndustriesGridview() {
    loadingScreen(false, 0);
var obj = new Object();
PageMethods.GetIndustriesList(
    function OnSuccess(result) {
        if (result != null) {
            var index = 1;
            var itemsPerPage = 10;
            var list = [];
            $("#tBodyIndustries").empty();
            for (var r = 0; r < result.length; r++) {
                list.push(result[r]);
            }
            secureStorage.setItem('list', JSON.stringify(list));
            allItems = result.length;
            if (result.length > 0) {
                createTable("divIndustries", "tblIndustries", "tBodyIndustries", list, index, itemsPerPage, allItems, "list", "Industries");
            }
        }
        loadingScreen(true, 0);
    },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
}