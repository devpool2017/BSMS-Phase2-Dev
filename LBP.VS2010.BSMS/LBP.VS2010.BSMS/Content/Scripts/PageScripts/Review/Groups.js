$(document).ready(function () {
    loadingScreen(false, 0);
      var deferreds = {
        view: $.Deferred(),
        insert: $.Deferred(),
        update: $.Deferred(),
        print: $.Deferred(),
        delete: $.Deferred(),
        dataList: $.Deferred()
    };

       PageMethods.HasAccess('CanView',
        function OnSuccess(result) {
            deferreds.view.resolve(result);
        },
        function OnError(err) {
            deferreds.view.reject(err.get_message());
        });

       PageMethods.HasAccess('CanInsert',
        function OnSuccess(result) {
            deferreds.insert.resolve(result);
        },
        function OnError(err) {
            deferreds.insert.reject(err.get_message());
        });

       PageMethods.HasAccess('CanUpdate',
        function OnSuccess(result) {
            deferreds.update.resolve(result);
        },
        function OnError(err) {
            deferreds.update.reject(err.get_message());
        });
        
       PageMethods.HasAccess('CanPrint',
        function OnSuccess(result) {
            deferreds.print.resolve(result);
        },
        function OnError(err) {
            deferreds.print.reject(err.get_message());
        });

       PageMethods.HasAccess('CanDelete',
        function OnSuccess(result) {
            deferreds.delete.resolve(result);
        },
        function OnError(err) {
            deferreds.delete.reject(err.get_message());
        });

        PageMethods.ListGroups(
        function OnSuccess(result) {
            deferreds.dataList.resolve(result);
        },
        function OnError(err) {
            deferreds.dataList.reject(err.get_message());
        });
            $.when(deferreds.dataList,deferreds.view,deferreds.insert,
                   deferreds.update, deferreds.print,
                   deferreds.delete).done(
                  async function (dataList,canView,canInsert, canUpdate, canPrint,canDelete) {
                try {
                    var GroupList = dataList.RegionList;
                    //canUpdate?CreateGridviewUpdateDelete(GroupList):CreateGridview(GroupList);
                    CreateGridview(GroupList); //For Review Group
                    toggleEventForElement('#btnAddGroup', 'click', AJAXWrapPageMethodCall, true, 'addGroup');
                    toggleEventForElement('#btnAddSave', 'click', AJAXWrapPageMethodCall, true, 'confirmAdd');
                    toggleEventForElement('#btnAddCancel', 'click', AJAXWrapPageMethodCall, true, 'addGroupCancel');
                    toggleEventForElement('#btnEditSave', 'click', AJAXWrapPageMethodCall, true, 'confirmUpdate');
                    toggleEventForElement('#btnEditCancel', 'click', AJAXWrapPageMethodCall, true, 'editGroupCancel');
                }
                catch (e) {
                    showErrorModal(e);
                }
                loadingScreen(true, 0);
            }).fail(function (e) {
                showErrorModal(e);
                loadingScreen(true, 0);
            });
});


function CreateGridviewUpdateDelete(result)
{
               var index = 1;
                var itemsPerPage = 10;
                var list = [];
                $("#tBodyGroupsUpdateDelete").empty();
                for (var r = 0; r < result.length; r++) {
                    list.push(result[r]);
                }
                secureStorage.setItem('list', JSON.stringify(list));
                allItems = result.length;
                sessionStorage.lastIndex = result.length;
                createTable("divGroupsUpdateDelete", "tblGroupUpdateDelete", "tBodyGroupsUpdateDelete", list, index, itemsPerPage, allItems, "list", "Groups");
}

function CreateGridview(result)
{
               var index = 1;
                var itemsPerPage = 10;
                var list = [];
                $("#tBodyGroups").empty();
                for (var r = 0; r < result.length; r++) {
                    list.push(result[r]);
                }
                secureStorage.setItem('list', JSON.stringify(list));
                allItems = result.length;
                sessionStorage.lastIndex = result.length;
                createTable("divGroups", "tblGroup", "tBodyGroups", list, index, itemsPerPage, allItems, "list", "Groups");
}

function confirmUpdate() {
    showConfirmModal("update", editGroupSave);
}

function confirmAdd() {
    showConfirmModal("added", addNewGroup);

}

function confirmDelete() {
}

function loadGroupList() {
    loadingScreen(false, 0);
    PageMethods.ListGroups(
        function OnSuccess(result) {
            if (result != null) {
                var index = 1;
                var itemsPerPage = 10;
                var list = [];
                $("#tBodyGroups").empty();
                for (var r = 0; r < result.length; r++) {
                    list.push(result[r]);
                }
                secureStorage.setItem('list', JSON.stringify(list));
                allItems = result.length;
                sessionStorage.lastIndex = result.length;
                createTable("divGroups", "tblGroup", "tBodyGroups", list, index, itemsPerPage, allItems, "list", "Groups");
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
}

function addGroup() {
    $('#divAddGroupModal').modal('show');
    var lastindex2 = parseInt(sessionStorage.lastIndex) + 1;
    setTextboxValue("txtAddGroupCode", lastindex2.toString());
    setTextboxValue("txtAddGroupName", "");
    setLabelText("lblModalAddErrorMessage", "");
}

function addNewGroup() {
    var obj = new Object;
    obj.GroupCode = getTextboxValue('txtAddGroupCode');
    obj.GroupName = getTextboxValue('txtAddGroupName');

    PageMethods.AddNewGroups(obj,
        function OnSuccess(result) {
            if (result != "") {
                showSpecificError(result, 'lblModalAddErrorMessage', 'divModalAddError');
                loadingScreen(true, 0);
            }
            else {
              
                showSuccessModal("added", loadGroupList);
                $('#divAddGroupModal').modal('hide');
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );

}

function addGroupCancel() {
    $('#divAddGroupModal').modal('hide');
    setTextboxValue("txtAddGroupCode", "");
    setTextboxValue("txtAddGroupName", "");
    setLabelText("lblModalAddErrorMessage", "");
}
function editGroups(GroupCode, GroupName) {
    $('#divEditGroupModal').modal('show');
    setTextboxValue("txtEditGroupCode", GroupCode);
    setTextboxValue("txtEditGroupName", GroupName);
    setLabelText("lblModalEditErrorMessage", "");
}

function editGroupSave() {
    var obj = new Object();
    obj.GroupName = getTextboxValue('txtEditGroupName');
    obj.GroupCode = getTextboxValue('txtEditGroupCode');
    PageMethods.UpdateGroups(obj,
        function OnSuccess(result) {
            if (result != "") {

                showSpecificError(result, 'lblModalEditErrorMessage', 'divModalEditError');
                loadingScreen(true, 0);
            }
            else {
                showSuccessModal("updated", loadGroupList);
                $('#divEditGroupModal').modal('hide');
            }
         loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
}

function editGroupCancel() {
    $('#divEditGroupModal').modal('hide');
    setTextboxValue("txtEditGroupCode", "");
    setTextboxValue("txtEditGroupName", "");
}
