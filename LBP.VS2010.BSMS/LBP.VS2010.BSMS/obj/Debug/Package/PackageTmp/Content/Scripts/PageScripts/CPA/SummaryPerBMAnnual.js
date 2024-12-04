$(document).ready(function () {
    loadingScreen(false, 0);
    var obj = new Object();
    obj.Lead = "";
    obj.Prospect = "";
    obj.Customer = "";
    obj.role= "";
    obj.region = "";

    $('#btn-PrintReport').hide();
      var deferreds = {
        view: $.Deferred(),
        insert: $.Deferred(),
        update: $.Deferred(),
        print: $.Deferred(),
        delete: $.Deferred(),
        SessionSaveSummaryDetailList: $.Deferred(),
        SummaryAnnualPerBMList: $.Deferred()
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
        PageMethods.SessionSaveSummaryDetails(
        function OnSuccess(result) {
            deferreds.SessionSaveSummaryDetailList.resolve(result);
        },
        function OnError(err) {
            deferreds.SessionSaveSummaryDetailList.reject(err.get_message());
        });
        PageMethods.SummaryPerBMDetails(
        function OnSuccess(result) {
            deferreds.SummaryAnnualPerBMList.resolve(result);
        },
        function OnError(err) {
            deferreds.SummaryAnnualPerBMList.reject(err.get_message());
        });
            $.when(deferreds.view,deferreds.insert, deferreds.update, deferreds.print,deferreds.delete, deferreds.SummaryAnnualPerBMList,deferreds.SessionSaveSummaryDetailList).done(
            async function (canView,canInsert, canUpdate, canPrint,canDelete, SummaryAnnualPerBMList,SessionSaveSummaryDetailList) {
                try {
                    setLabelText("lblBranchManager","Potential Accounts of Branch Head: " + SessionSaveSummaryDetailList[0].Fullname.toString()); 
                    createSummaryTable(SummaryAnnualPerBMList);
                    toggleEventForElement('#btn-PrintReport', 'click', AJAXWrapPageMethodCall, true, 'generateReport');
                    toggleEventForElement('#ddlSearch', 'change', AJAXWrapPageMethodCall, true, 'changeSearchBy');
                    toggleEventForElement('#btn-Back', 'click', AJAXWrapPageMethodCall, true, 'backToSummaryList');
                    toggleEventForElement('#btnSearch', 'click', AJAXWrapPageMethodCall, true, 'SearchAnnualReport');      
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
      var TotalLead = 0,TotalProspect=0,TotalCustomer=0;
      var wordFound;

            $("#tBodySummaryBMAnnual").empty();
            for (var r = 0; r < result.length; r++) {
                list.push(result[r]);
                 $('#btn-PrintReport').show();
                wordFound = result[r].Lead.split(" ").includes("Y");
                if(wordFound == true)
                {
                 TotalLead += 1;
                }
                 wordFound = result[r].Prospect.split(" ").includes("Y");
                  if(wordFound == true)
                {
                 TotalProspect += 1;
                }
                 wordFound = result[r].Customer.split(" ").includes("Y");
                  if(wordFound == true)
                {
                 TotalCustomer += 1;
                }
            }

             setLabelText("lblTotalLead", TotalLead.toString());
             setLabelText("lblTotalProspect", TotalProspect.toString());
             setLabelText("lblTotalCustomer", TotalCustomer.toString());
   
            secureStorage.setItem('list', JSON.stringify(list));
            allItems = result.length;

            createTable("divSummaryBMAnnual", "tblSummaryBMAnnual", "tBodySummaryBMAnnual", list, index, itemsPerPage, allItems, "list", "SummaryBM");
 }

  function generateReport() {
    hideError();
    loadingScreen(false, 0);
   var obj = new Object;
   obj.Lead = getDropdownValue('ddlLead');
   obj.Prospect = getDropdownValue('ddlProspect');
   obj.Customer = getDropdownValue('ddlCustomer');
    showDiv(['divReport'],true);
    PageMethods.GenerateReport(obj,
        function OnSuccess(result) {
            if (result == '') {
                $('iframe[name="ifrmReportViewer"]').attr('src', '../../../../Views/Reports/ReportViewer.aspx');
                 showDiv(['divReport'], true);
                 loadingScreen(true, 0);
            }
            else {
                showError(result);
                loadingScreen(true, 0);
            }
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}


function editSummaryBM(CPAID,Fullname,Industry,DateEncoded,Lead,Prospect,Customer,ClientID)
{
  var obj = new Object();
  obj.ClientID = ClientID;
  obj.Fullname = Fullname;
  obj.Industry = Industry;
  obj.DateEncoded = DateEncoded;
  obj.Lead = Lead;
  obj.Prospect = Prospect;
  obj.Customer = Customer;
  obj.CPAID = CPAID;

    PageMethods.SaveSessionDetail(obj,
        function OnSuccess(result) {
            if (result == '') {
               window.location = "/Views/PotentialAccount/Revisits.aspx";
            }
            else {
                showError(result);
                loadingScreen(true, 0);
            }
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

function backToSummaryList()
{
    window.location = "/Views/PotentialAccount/SummaryPerBM.aspx";
}

function SearchAnnualReport()
{
   loadingScreen(false, 0);
   var obj = new Object();
   obj.Lead = getDropdownValue('ddlLead');
   obj.Prospect = getDropdownValue('ddlProspect');
   obj.Customer = getDropdownValue('ddlCustomer');
   showDiv(['divReport'],false);
    PageMethods.SearchSummaryPerBMDetails(obj,
                    function OnSuccess(result) {
                        if (result != '') {
                              createSummaryTable(result);
                        } else {
                              createSummaryTable(result);
                        }
                        loadingScreen(true, 0);
                    },
                    function OnError(err) {
                        showErrorModal(err.get_message());
                        loadingScreen(true, 0);
                    });

}

function deleteSummaryBM(CPAID,Fullname,Industry,DateEncoded,Lead,Prospect,Customer,ClientID)
{
    var obj = new Object();
    obj.CPAID = CPAID;

            showConfirmModal('delete', function () {
                loadingScreen(false, 0);
                PageMethods.DeleteCPA(obj,
                    function OnSuccess(result) {
                        if (result == '') {
                             showSuccessModal("deleted",
                               function (success) {
                                 location.reload();
                                }
                          );
                        } else {
                            showError(result);
                        }

                        loadingScreen(true, 0);
                    },
                    function OnError(err) {
                        showErrorModal(err.get_message());
                        loadingScreen(true, 0);
                    });
            });
}