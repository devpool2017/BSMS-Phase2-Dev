$(document).ready(function () {
    loadingScreen(true, 0);
    loadRelationshipOfficerList();
});

function loadRelationshipOfficerList() {
    loadingScreen(false, 0);
    PageMethods.ListRelationships(
        function OnSuccess(result) {
            if (result != null) {
                var index = 1;
                var itemsPerPage = 10;
                var list = [];
                $("#tBodyRelationshipOfficer").empty();
                for (var r = 0; r < result.length; r++) {
                    list.push(result[r]);
                }
                secureStorage.setItem('list', JSON.stringify(list));
                allItems = result.length;
                createTable("divRelationshipOfficer", "tblRelationshipOfficer", "tBodyRelationshipOfficer", list, index, itemsPerPage, allItems, "list", "RelationshipOfficers");
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
}

function viewRelationshipOfficers(FirstName, MiddleInitial, LastName, Username, RegBrCode) {
    $('#divViewRelationshipOfficerModal').modal('show');
    setTextboxValue("txtViewPosition", "Relationship Officer");
    setTextboxValue("txtViewFirstName", FirstName);
    setTextboxValue("txtViewMiddleInitial", MiddleInitial);
    setTextboxValue("txtViewLastName", LastName);
    setTextboxValue("txtViewGroup", RegBrCode);
    setTextboxValue("txtViewUserID", Username);

}
