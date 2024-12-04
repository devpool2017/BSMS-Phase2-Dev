$(document).ready(function () {
    loadingScreen(true, 0);
    $("#divDetails").hide();
    toggleEventForElement("#btnSearch", "click", checkDDLValue, true);
    toggleEventForElement("#chkAddVisit", "click", checkChkBox, true);
    toggleEventForElement("#btnUpdateCPA", "click", addProspect, true);
    toggleEventForElement("#btnUpdateCPA2", "click", addComment, true);
    toggleEventForElement("#btnCancel", "click",cancel,true);
    createTextAreaCounter();
    //checkCPA();
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
        checkCPA: $.Deferred(),
        loadAcct: $.Deferred()
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

        PageMethods.checkCPA(/// <reference path="../../jquery/" />
        function OnSuccess(result) {
//        deferreds.checkCPA.resolve(result);
            if (result != null) {
                      setTextboxValue('txtAccountName',result);
                      showDiv(['txtAccountName'], true);   
                      showDiv(['ddlAcctName'], false);   
                      showDiv(['btnSearch','btnReset'],false);
                      enableControl("txtAccountName", false);
                      loadAccountFromSummary();
                      loadRevisitHistoryFromSummary();
            }
            else {
                 loadAccountList();
            }
            loadingScreen(true, 1000);
        },
        function OnError(err) {
//        deferreds.checkCPA.reject(err.get_message());
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });

        PageMethods.AccountDetailsFromSummary(
        function OnSuccess(result) {
            deferreds.loadAcct.resolve(result);
            //loadRevisitHistoryFromSummary();
        },
        function OnError(err) {
            deferreds.loadAcct.reject(err.get_message());
        });

        $.when(deferreds.checkCPA,
               deferreds.loadAcct,
               deferreds.view,
               deferreds.insert,
               deferreds.update,
               deferreds.print,
               deferreds.delete).done(
            async function (checkCPA, loadAcct, canView, canInsert, canUpdate, canPrint,canDelete, branchListx) {
                try {
                    $("#divDetails").hide();
                    toggleEventForElement("#btnSearch", "click", checkDDLValue, true);
                    toggleEventForElement("#chkAddVisit", "click", checkChkBox, true);
                    toggleEventForElement("#btnUpdateCPA", "click", addProspect, true);
                    toggleEventForElement("#btnUpdateCPA2", "click", addComment, true);
                    toggleEventForElement("#btnCancel", "click",cancel,true);
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

function checkCPA()
{
   PageMethods.checkCPA(/// <reference path="../../jquery/" />
        function(result) {
            if (result != null) {
                      showDiv(['txtAccountName'], true);   
                      setTextboxValue('txtAccountName',result);
                      showDiv(['ddlAcctName'], false);   
                      showDiv(['btnSearch','btnReset'],false);
                      enableControl("txtAccountName", false);
                      loadAccountFromSummary();
                      loadRevisitHistoryFromSummary();
            }
            else {
                 loadAccountList();
            }
            loadingScreen(true, 1000);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });

    loadingScreen(true, 0);
}

function loadAccountFromSummary(){
 hideError();
 loadingScreen(false, 0);
 PageMethods.AccountDetailsFromSummary(
// function OnSuccess(result) {
//            deferreds.loadAcct.resolve(result);
//        },
        function(result) {
            if (result != null) {
                $("#divDetails").show();
                sessionStorage.accDetail = JSON.stringify(result);
                $("#txtAcctName").text(result.ClientName);
                var clientId = (result.ClientID).replace('-', '');
                //setDropdownDefaultValue('ddlAcctName', clientId);
                setLabelText("lblChatHistory", result.Revisits[0].Remarks);
                let lastrevisit = new Date(result.Revisits[0].Prospect);
                checkDate(lastrevisit);
                $("#txtLastRevisit").text(lastrevisit.toShortFormat());
                setTextboxValue("txtNumberVisit", result.Revisits[0].Visits);

                if (result.Revisits[0].Access.CanView = 'Y') {
                    enableControl("txtCreateFeedback", true);
                }


                enableControl("txtNumberVisit", false);
                enableControl("ddlAcctName", false);
                enableControl("txtAccountName", false);
                enableControl("chkAddVisit", false);
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

function loadRevisitHistoryFromSummary()
{
    hideError();
    loadingScreen(false, 0);
    ClientID = getDropdownValue("ddlAcctName");
    sessionStorage.setItem('FromSummaryClientID',ClientID);
    PageMethods.ListRevisit(ClientID,
        function (data) {
            if (data != null) {

                loadDropdown('ddlRevisitHistory', data, true);
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
function loadAccountList() {
    hideError();
    loadingScreen(false, 0); 
    var param = new Object;
    param.UserID = $("#hdnSession").data('value');
    param.Tag = $("#hdnSession1").data('value');
    PageMethods.ListAccounts(param,
        function(data) {
            if (data != null) {

                loadDropdown('ddlAcctName', data, true);
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

function checkDDLValue() {
    hideError();
    loadingScreen(false, 0);
    ClientID = getDropdownValue("ddlAcctName");
    if (ClientID == "") {
    showErrorModal('Please select Potential Account Name');
    } else if (ClientID == "0") {
    checkCPA();
    } else {
    loadAccountDetails();
    loadRevisitHistory();}

     loadingScreen(true, 0);
}

function checkDate(lastrevisit) {
    hideError();
    //loadingScreen(false, 0);
    let dateNow = new Date();
    let dateNow1 = dateNow.toShortFormat();
    let lastrevisit1 = lastrevisit.toShortFormat();
    if (lastrevisit1 == dateNow1) {
        enableControl("chkAddVisit", false);
        $("#btnUpdateCPA").hide();
        $("#btnUpdateCPA2").show();
        enableControl("txtCreateFeedback", true);
    } else {
        enableControl("chkAddVisit", true);
        $("#btnUpdateCPA2").hide();
        $("#btnUpdateCPA").show();
        enableControl("txtCreateFeedback", false);
    }

}


function resetFunc() {
    window.location = "/Views/PotentialAccount/Revisits.aspx";
}



function checkChkBox() {

    var NumOfVisits = parseInt(getTextboxValue('txtNumberVisit'));
        if (chkAddVisit.checked == true) {
            enableControl("txtCreateFeedback", true);
            NumOfVisits = NumOfVisits + 1;
            loadAllRevisitHistory();
            $("#txtNumberVisit").val(NumOfVisits);
            let today = new Date();
            $("#txtLastRevisit").text(today.toShortFormat());
            
        } else {
            NumOfVisits = NumOfVisits - 1;
            $("#txtNumberVisit").val(NumOfVisits);
            loadRevisitHistory();
            loadAccountDetails();

            }
    }


function loadAccountDetails() {
    hideError();
    loadingScreen(false, 0);
    ClientID = getDropdownValue("ddlAcctName");
    PageMethods.AccountDetailsList(ClientID,
        function OnSuccess(result) {
            if (result != null) {
                $("#divDetails").show();
                sessionStorage.accDetail = JSON.stringify(result);
                var clientId = ClientID.replace('-', '');
                setDropdownDefaultValue('ddlAcctName', clientId);
                //$("#txtAcctName").text(result.ClientName);
                setLabelText("lblChatHistory", result.Remarks);
                let lastrevisit = new Date(result.Prospect);
                checkDate(lastrevisit);
                $("#txtLastRevisit").text(lastrevisit.toShortFormat());
                setTextboxValue("txtNumberVisit", result.Visits);
                enableControl("txtNumberVisit", false);
                enableControl("ddlAcctName", false);
            }
            else {
                //showErrorModal(result);
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

function loadRevisitHistory() {
    hideError();
    loadingScreen(false, 0);
    ClientID = getDropdownValue("ddlAcctName");
    PageMethods.ListRevisit(ClientID,
        function (data) {
            if (data != null) {
                loadDropdown('ddlRevisitHistory', data, true);
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

function loadAllRevisitHistory() {
    hideError();
    loadingScreen(false, 0);
    ClientID = getDropdownValue("ddlAcctName");
    PageMethods.ListAllRevisit(ClientID,
        function (data) {
            if (data != null) {
                loadDropdown('ddlRevisitHistory', data, true);
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

function countRevisitHistory() {
    hideError();
    loadingScreen(false, 0);
    ClientID = getDropdownValue("ddlAcctName");
    PageMethods.CountRevisit(ClientID,
        function OnSuccess(result) {
            if (result != null) {
                sessionStorage.accDetail = JSON.stringify(result);
                setTextboxValue('txtNumberVisit', result.RevisitCount);
                enableControl("txtNumberVisit", false);
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

function addProspect(){
    hideError();
    loadingScreen(false, 0);
    

    const today = new Date();
    const yyyy = today.getFullYear();
    let mm = today.getMonth() + 1; // Months start at 0!
    let dd = today.getDate();

    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;

    const formattedToday = mm + '/' + dd + '/' + yyyy;


    var param = new Object;
    param.ClientID = getDropdownValue("ddlAcctName");
    param.UserID = $("#hdnSession").data('value');
    param.Prospect = formattedToday;
    param.NewRemarks = getTextboxValue("txtCreateFeedback");
    param.Visits = getTextboxValue("txtNumberVisit");

    if (chkAddVisit.checked == true) {
        param.isChecked = true;
    } else {
        param.isChecked = false;
    }

    PageMethods.addProspect(param,
        function OnSuccess(result) {
            if (result == '') {
                checkDDLValue();
                loadingScreen(true, 0);
                setTextboxValue("txtCreateFeedback", '')
               
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

function addComment(){
    hideError();
    loadingScreen(false, 0);

    var param = new Object;
    param.ClientID = getDropdownValue("ddlAcctName");
    param.UserID = $("#hdnSession").data('value');
    param.NewRemarks = getTextboxValue("txtCreateFeedback");
    param.isChecked = false;

    PageMethods.addComment(param,
        function OnSuccess(result) {
            if (result == '') {
                checkDDLValue();
                setTextboxValue("txtCreateFeedback", '')
                //location.reload();
                loadingScreen(true, 0);
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

Date.prototype.toShortFormat = function() {

    const monthNames = ["Jan", "Feb", "Mar", "Apr",
                        "May", "Jun", "Jul", "Aug",
                        "Sep", "Oct", "Nov", "Dec"];
    
    const day = this.getDate();
    
    const monthIndex = this.getMonth();
    const monthName = monthNames[monthIndex];
    
    const year = this.getFullYear();
    
    return `${day}-${monthName}-${year}`;  
}

function cancel()
{
  var summaryClientID =   sessionStorage.getItem('FromSummaryClientID');

  if(summaryClientID == "" || summaryClientID == null)
  {
       window.location = "/Views/PotentialAccount/Revisits.aspx";  
  }
  else{
   window.location = "/Views/PotentialAccount/SummaryPerBMAnnual.aspx";
   
  }
}