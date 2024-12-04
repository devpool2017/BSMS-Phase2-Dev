$(document).ready(function () {
    loadingScreen(false, 0);
    showDiv(['divCPADetails'],false);
    var obj = new Object();
 
    obj.Lead = "";
    obj.Prospect = "";
    obj.Customer = "";
    obj.role= "";
    obj.region = "";
 
      var deferreds = {
        view: $.Deferred(),
        insert: $.Deferred(),
        update: $.Deferred(),
        print: $.Deferred(),
        delete: $.Deferred(),
         dataList: $.Deferred(),
         checkDateRange: $.Deferred(), 
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
 
          PageMethods.CheckCPADateRange(
        function OnSuccess(result) {
            deferreds.checkDateRange.resolve(result);
        },
        function OnError(err) {
            deferreds.checkDateRange.reject(err.get_message());
        });
 
        PageMethods.loadAccounts(
        function OnSuccess(result) {
            deferreds.dataList.resolve(result);
        },
        function OnError(err) {
            deferreds.dataList.reject(err.get_message());
        });
            $.when(deferreds.checkDateRange,deferreds.dataList,deferreds.view,deferreds.insert,
                   deferreds.update, deferreds.print,
                   deferreds.delete).done(
            async function (checkDateRange,dataList,canView,canInsert, canUpdate, canPrint,canDelete, branchListx) {
                try {
                    var EncodingList = dataList.EncodingList;
                    var currentUser = dataList.currentUser;
                   toggleEventForElement("#btnSave", "click", validateSave, true);
                   toggleEventForElement("#btnCancel", "click", returnToList, true);
                   toggleEventForElement("#btnAdd", "click", addNewCPA, true);
                   if(checkDateRange.errorMessage == "")
                   {
                    createEncodingTable(EncodingList);
                   }
                   else{
                    $("#divCPAccounts").hide();
                   $("#divbtnAdd").hide();
                  setLabelText("lblError", checkDateRange.errorMessage);
                  showDiv(['divNotAllowed'],true);
                   $("#divNotAllowed").show();
                   }

 
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
 

function checkCPADateRange() {
    loadingScreen(false, 0);
    PageMethods.CheckCPADateRange(
        function OnSuccess(result) {
            if (result.errorMessage == null || result.errorMessage == "") {
                loadAccountList();
            }
            else {
                $("#divCPAccounts").hide();
                $("#divbtnAdd").hide();
                setLabelText("lblError", result.errorMessage);
                showDiv(['divNotAllowed'],true);
                $("#divNotAllowed").show();
//                showError(result.errorMessage);
            }
            loadingScreen(true, 1000);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}
 
function createEncodingTable(result)
{
  var index = 1;
                var itemsPerPage = 10;
                var list = [];
                $("#tBodyCPAccounts").empty();
                for (var r = 0; r < result.length; r++) {
                    list.push(result[r]);
                }
                secureStorage.setItem('list', JSON.stringify(list));
                allItems = result.length;
 
                createTable("divCPAccounts", "tblCPAccounts", "tBodyCPAccounts", list, index, itemsPerPage, allItems, "list", "List");
}
 
function loadAccountList() {
    hideError();
    loadingScreen(false, 0);
    var param = new Object;
  //  param.userName = $("#hdnSession").data('value');
    PageMethods.loadAccounts(
        function OnSuccess(result) {
            if (result.isSuccess == false) {
                showErrorModal(result.errMsg);
            }
            else {
                var index = 1;
                var itemsPerPage = 10;
                var list = [];
                $("#tBodyCPAccounts").empty();
                for (var r = 0; r < result.length; r++) {
                    list.push(result[r]);
                }
                secureStorage.setItem('list', JSON.stringify(list));
                allItems = result.length;
 
                createTable("divCPAccounts", "tblCPAccounts", "tBodyCPAccounts", list, index, itemsPerPage, allItems, "list", "List");
            }
            loadingScreen(true, 0);
        },
            function OnError(err) {
                showErrorModal(err.get_message());
                loadingScreen(true, 0);
            });
 
    loadingScreen(true, 0);
 
}
 
function viewList(ClientID) {
    hideError();
    loadingScreen(false, 0);
    setLabelText("lblAction", "view");
    PageMethods.viewAccount(ClientID,
        function OnSuccess(result) {
            if (result != null) {
                hideSpecificError("lblModalErrorMessage");
                sessionStorage.accDetail = JSON.stringify(result);
                initControls(result);
                showDiv(['divCPADetails'],true);
            }
            else {
                showSpecificError(result, "lblModalErrorMessage", "divModalError");
            }
            loadingScreen(true, 1000);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
 
    loadingScreen(true, 0);
}
 
function editList(ClientID) {
    hideError();
    loadingScreen(false, 0);
    setLabelText("lblAction", "update");
    PageMethods.viewAccount(ClientID,
        function OnSuccess(result) {
            if (result != null) {
                hideSpecificError("lblModalErrorMessage");
                sessionStorage.accDetail = JSON.stringify(result);
                showDiv(['divCPADetails'],true);
                initControls(result);
                //setDropdownValue('ddlIndustry', result.CIndustryID);
            }
            else {
                showSpecificError(result, "lblModalErrorMessage", "divModalError");
            }
            loadingScreen(true, 1000);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
 
    loadingScreen(true, 0);
}
 
 
function initControls(result) {
    var action = getLabelText("lblAction");
    var industry = result.CIndustryID;
    loadDropdownlist(industry);
 
    if (action == "update") {
        $("#divCPAccounts").hide();
        $("#divPagingdivCPAccounts").hide();
        $("#divCPADetails").show();
        $("#txtIndustry").hide();
        $("#ddlIndustry").show();
        $("#btnSave").show();
        $("#divbtnAdd").hide();
 
        setLabelText("lblCPAID", result.CID);
        setLabelText("lblAcctName", result.CName);
        setLabelText("lblIndID", result.CIndustry);
        
        //$("#ddlIndustry").val(result.CIndustryID);
        enableControl("txtAccountName", true);
        enableControl("ddlIndustry", true);
 
    }
    else {
        $("#divCPAccounts").hide();
        $("#divPagingdivCPAccounts").hide();
        $("#divCPADetails").show();
        $("#txtIndustry").show();
        $("#ddlIndustry").hide();
        $("#btnSave").hide();
        $("#divbtnAdd").hide();
        enableControl("txtAccountName", false);
        enableControl("txtIndustry", false);
    }
 
    setTextboxValue("txtAccountName", result.CName);
    setTextboxValue("txtIndustry", result.CIndustry);
}
 
function loadDropdownlist(industry) {
    hideError();
    loadingScreen(false, 0);
    PageMethods.ddlIndustry(
        function (data) {
            if (data != null) {
                loadDropdown('ddlIndustry', data, true, industry);
            }
            else {
                showSpecificError(result, "lblModalErrorMessage", "divModalError");
            }
            loadingScreen(true, 1000);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
 
    loadingScreen(true, 0);
 
}
 
function addNewCPA() {
    showDiv(['divNoRecordFounddivCPAccounts'],false);
    setLabelText("lblAction", "add");
    $("#divCPAccounts").hide();
    $("#divPagingdivCPAccounts").hide();
    showDiv(['divCPADetails'],true);
    $("#txtIndustry").hide();
    $("#ddlIndustry").show();
    $("#btnSave").show();
    $("#divbtnAdd").hide();
    loadDropdownlist();
    enableControl("txtAccountName", true);
    enableControl("ddlIndustry", true);
}
 
function returnToList() {
    window.location.replace("/Views/PotentialAccount/Encoding.aspx?submenuid=38");
}
 
 
function validateSave() {
    hideError();
    loadingScreen(false, 0);
 
    var param = new Object;
    param.Action = getLabelText("lblAction");
    param.userName = $("#hdnSession").data('value');
    param.userBranch = $("#hdnSession1").data('value');
    param.CName = getTextboxValue("txtAccountName");
    param.CIndustryID = getDropdownValue("ddlIndustry");
    param.CIndustry = getDropdownText("ddlIndustry");
    param.ClientName = getLabelText("lblAcctName");
    param.ClientIndustryID = getLabelText("lblIndID");
    param.ClientID = getLabelText("lblCPAID");
 
    PageMethods.validateSave(param,
        function OnSuccess(result) {
            if (result == '') {
                hideSpecificError("lblModalErrorMessage");
                showSuccessModal('saved', reload);
            }
            else {
                showErrorModal(result, "lblModalErrorMessage", "divModalError");
            }
            loadingScreen(true, 1000);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
 
    loadingScreen(true, 0);
}
 
function reload() {
    window.location = "/Views/PotentialAccount/Encoding.aspx?submenuid=38";  
}