$(document).ready(function () {
  showDiv(['divBranchManager'],false);
  showDiv(['divButtons'],false);
    loadingScreen(false, 0);
      var deferreds = {
        view: $.Deferred(),
        insert: $.Deferred(),
        update: $.Deferred(),
        print: $.Deferred(),
        delete: $.Deferred(),
        mainmenuid: $.Deferred(),
         dataList: $.Deferred(),
         branchList: $.Deferred(),
        //summaryBMList: $.Deferred()
        summaryBMList: $.Deferred()
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

        PageMethods.OnLoadData(
        function OnSuccess(result) {
            deferreds.dataList.resolve(result);
        },
        function OnError(err) {
            deferreds.dataList.reject(err.get_message());
        });
            $.when(deferreds.dataList,deferreds.view,deferreds.insert,
                   deferreds.update, deferreds.print,
                   deferreds.delete).done(
            async function (dataList,canView,canInsert, canUpdate, canPrint,canDelete, branchList) {
                try {
                 
                    var RegionList = dataList.RegionList;
                    var AllBranchManagerList = dataList.BranchManagerList;
                    var currentUser = dataList.currentUser;
                    var isAdmn = (dataList.AdminCode).includes(currentUser.RoleID);
                    var isBM = (dataList.BMCode).includes(currentUser.RoleID);
                    var isGH = (dataList.GroupCode).includes(currentUser.RoleID);
                    
                 createSummaryTable(AllBranchManagerList); //For Review Branch

                 if (RegionList.length > 0) RegionList.splice(0, 0, 0, {value: '',description: 'All', data: ''});
                 loadDropdown('ddlRegion',RegionList, (isGH || isBM? false : true));
                 if ($('#ddlRegion').hasClass('es-input')) $('#ddlRegion').attr("placeholder", "All");
                 setDropdownDefaultValue('ddlRegion', (isGH || isBM? currentUser.GroupCode : RegionList));
                 
                 isAdmn?showDiv(['divGroupHead','divButtons'],true) : showDiv(['divGroupHead'],false);
                 isGH || isBM?loadBranchesPerGH(isGH,isBM,currentUser):enableControl('ddlBranches',false);
                 toggleEventForElement("#ddlRegion", "change", changeRegion, true, null);
                 toggleEventForElement("#btnSearch", "click", loadBranchManagers, true, null);
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

 function createSummaryTable(result)
 {
          var index = 1;
            var itemsPerPage = 10;
            var list = [];
            $("#tBodyBranchManager").empty();
            for (var r = 0; r < result.length; r++) {
                list.push(result[r]);
            }
            secureStorage.setItem('list', JSON.stringify(list));
            allItems = result.length;
            createTable("divBranchManager", "tblBranchManager", "tBodyBranchManager", list, index, itemsPerPage, allItems, "list", "BranchManager");
 }

function changeRegion() {
    loadingScreen(false, 0);
    var obj = new Object();
    obj.region = getDropdownValue('ddlRegion');
    PageMethods.GetBranchPerRegion(obj,
    function OnSuccess(result) {
        if (result.length > 0) {
            if (result.length > 0) result.splice(0, 0, 0, {value: '',description: 'All', data: ''});
            loadDropdown('ddlBranches', result, true, '', 'All');
            if ($('#ddlBranches').hasClass('es-input')) $('#ddlBranches').attr("placeholder", "All");
            getGroupHead(obj.region);
        }
        loadingScreen(true, 0);
    },
    function OnError() {
        alert(err.get_message());
    });
}

function loadBranchesPerGH(isGH,isBM,currentUser)
{
   loadingScreen(false,0);
   var obj = new Object();
   obj.region = getDropdownValue('ddlRegion');
    PageMethods.GetBranchPerRegion(obj,
    function OnSuccess(result) {
        if (result.length > 0) {
            if (result.length > 0) result.splice(0, 0, 0, {value: '',description: 'All', data: ''});
            if(isGH == true)
            {
            loadDropdown('ddlBranches', result, true, '', 'All');
            showDiv(['divButtons'],true);
            }
            else if(isBM == true)
            {
             loadDropdown('ddlBranches', result, true);
             enableControl('ddlBranches',false);
             setDropdownDefaultValue('ddlBranches', currentUser.BranchCode);
             loadBranchManager();
            }
             if ($('#ddlBranches').hasClass('es-input')) $('#ddlBranches').attr("placeholder", "All");
        }
        loadingScreen(true, 0);
    },
    function OnError() {
        alert(err.get_message());
    });
}

function getGroupHead(region)
{
  loadingScreen(false, 0);
    var obj = new Object();
    obj.region = region;
    PageMethods.GetGroupHeadPerRegion(obj,
    function OnSuccess(result) {
        if (result != null) {
            setTextboxValue('txtGroupHead', result.GroupHead);
        }
        else {
            setTextboxValue('txtGroupHead', '');
        }
        loadingScreen(true, 0);
    },
    function OnError() {
        alert(err.get_message());
    });
}
function changeRoles() {
    var obj = new Object;
    obj.role = getDropdownValue('ddlSearchBy');
    if (obj.role == "8") {
        enableControl('ddlRegion', true);
        loadRegionLst();
    }
    else if(obj.role == ""){
        enableControl('ddlRegion', false);
        setDropdownDefaultValue('ddlRegion', '');
        setDropdownDefaultText('ddlRegion', '');
    }
}

function loadBranchManager()
{
  var obj = new Object();
  obj.Branch = getDropdownValue('ddlBranches');
  obj.region = getDropdownValue('ddlRegion');
  obj.role = "8";
   PageMethods.BranchManagerListRegionBranch(obj,
    function OnSuccess(result) {
        if (result != null) {
            createSummaryTable(result);
        }
        loadingScreen(true, 0);
    },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
}

function loadBranchManagers()
{
 loadingScreen(false,0);
  var obj = new Object();
  obj.Branch = getDropdownValue('ddlBranches');
  obj.region = getDropdownValue('ddlRegion');
  obj.role = "8";
   PageMethods.BranchManagerListRegionBranch(obj,
    function OnSuccess(result) {
        if (result != null) {
            createSummaryTable(result);
        }
        loadingScreen(true, 0);
    },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
}

function loadBranchManagerList() {
    loadingScreen(false, 0);
    var obj = new Object;
    obj.role ="8";
    obj.region = getDropdownValue('ddlRegion');
    PageMethods.BranchManagerList(obj,
    function OnSuccess(result) {
        if (result != null) {
            createSummaryTable(result);
        }
        loadingScreen(true, 0);
    },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
}


function loadRegionList() {
    loadingScreen(false, 0);
    PageMethods.RegionList(
    function OnSuccess(result) {
        if (result.length > 0) {
            result[0].description = "All";
            result[0].value = "";
            loadDropdown('ddlRegion', result, true, '', 'All');
                    }
        loadingScreen(true, 0);
    },
    function OnError() {
        alert(err.get_message());
    });
}

