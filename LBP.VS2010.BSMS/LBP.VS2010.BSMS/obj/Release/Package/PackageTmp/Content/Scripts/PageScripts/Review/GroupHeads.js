$(document).ready(function () {
    loadingScreen(true, 0);
    loadGroupHeadList();
    toggleEventForElement('#btnAddGroupHead', 'click', AJAXWrapPageMethodCall, true, 'addGroupHead');
        toggleEventForElement('#btnAddSave', 'click', AJAXWrapPageMethodCall, true, 'confirmAdd');
      toggleEventForElement('#btnAddCancel', 'click', AJAXWrapPageMethodCall, true, 'addGroupHeadCancel');
        toggleEventForElement('#btnEditSave', 'click', AJAXWrapPageMethodCall, true, 'confirmUpdate');
      toggleEventForElement('#btnEditCancel', 'click', AJAXWrapPageMethodCall, true, 'editGroupHeadCancel');
});

    function confirmUpdate() {
        showConfirmModal("update", editGroupSave);
    }

    function confirmAdd() {
        showConfirmModal("added", addNewGroupHead);

    }

function loadGroupHeadList() {
    loadingScreen(false, 0);
    PageMethods.ListGroupHeads(
        function OnSuccess(result) {
            if (result != null) {
                var index = 1;
                var itemsPerPage = 10;
                var list = [];
                $("#tBodyGroupHeads").empty();
                for (var r = 0; r < result.length; r++) {
                    list.push(result[r]);
                }
                secureStorage.setItem('list', JSON.stringify(list));
                allItems = result.length;
                createTable("divGroupHeads", "tblGroupHeads", "tBodyGroupHeads", list, index, itemsPerPage, allItems, "list", "GroupHeads");
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
}

function addGroupHead() {
    loadGroupList();
    $('#divAddGroupHeadModal').modal('show');
    setTextboxValue('txtAddPosition', 'Group Head')
    setTextboxValue("txtAddFirstName", "");
    setTextboxValue("txtAddMiddleInitial", "");
    setTextboxValue("txtAddLastName", "");
    loadGroupList();
    setTextboxValue("txtAddUserID", "");
}

function loadGroupList() {
    loadingScreen(false, 0);
    PageMethods.GroupList(
    function OnSuccess(result) {
        if (result.length > 0) {
            loadDropdown("ddlAddGroup", result, true, "");
            loadDropdown("ddlEditGroup", result, true, "");
        }
        loadingScreen(true, 0);
    },
    function OnError() {
        alert(err.get_message());
    });
}

function addNewGroupHead() {
    var obj = new Object;
    obj.FirstName = getTextboxValue('txtAddFirstName');
    obj.MiddleInitial = getTextboxValue('txtAddMiddleInitial');
 obj.LastName = getTextboxValue('txtAddLastName');
 obj.Username = getTextboxValue('txtAddUserID');
 obj.BrCode = getDropdownValue('ddlAddGroup');
 obj.RegBrCode = getDropdownText('ddlAddGroup');
 obj.RoleName = getTextboxValue('txtAddPosition')

    PageMethods.AddNewGroups(obj,
        function OnSuccess(result) {
            if (result != "") {
                showSpecificError(result, 'lblModalAddErrorMessage', 'divModalAddError');
                loadingScreen(true, 0);
            }
            else {
                showSuccessModal("added", loadGroupList);
                $('#divAddGroupHeadModal').modal('hide');
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
    }

    function addGroupHeadCancel() {
        $('#divAddGroupHeadModal').modal('hide');
        setTextboxValue("txtAddFirstName", "");
        setTextboxValue("txtAddMiddleInitial", "");
        setTextboxValue("txtAddLastName", "");
        setTextboxValue("txtAddUserID", "");

    }

    function viewGroupHeads(FirstName, MiddleInitial, LastName, Username, RegBrCode) {
        $('#divViewGroupHeadModal').modal('show');
        setTextboxValue("txtViewPosition", "Group Head");
        setTextboxValue("txtViewFirstName", FirstName);
        setTextboxValue("txtViewMiddleInitial", MiddleInitial);
        setTextboxValue("txtViewLastName", LastName);
        setTextboxValue("txtViewGroup", RegBrCode);
        setTextboxValue("txtViewUserID", Username);
        
    }
//    function editGroupHeads(FirstName, MiddleInitial, LastName, Username, RegBrCode) {
//        $('#divEditGroupHeadModal').modal('show');
//        setTextboxValue("txtEditPosition", "Group Head");
//        setTextboxValue("txtEditFirstName", FirstName);
//        setTextboxValue("txtEditMiddleInitial", MiddleInitial);
//        setTextboxValue("txtEditLastName", LastName);
//        setTextboxValue("txtEditUserID", Username);
//        setDropdownDefaultText("ddlEditGroup", RegBrCode);
//    }

    function editGroupHeadCancel() {
        $('#divEditGroupHeadModal').modal('hide');
        setTextboxValue("txtEditPosition", "");
        setTextboxValue("txtEditFirstName", "");
        setTextboxValue("txtEditMiddleInitial", "");
        setTextboxValue("txtEditLastName", "");
        setTextboxValue("txtEditUserID", "");
        setDropdownDefaultValue("ddlEditGroup", "");
    }