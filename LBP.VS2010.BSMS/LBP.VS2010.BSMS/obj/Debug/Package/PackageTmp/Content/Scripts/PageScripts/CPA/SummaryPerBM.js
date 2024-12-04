$(document).ready(function () {
    loadingScreen(false, 0);
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
        //branchList: $.Deferred(),
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
            async function (dataList,canView,canInsert, canUpdate, canPrint,canDelete, branchListx) {
                try {
                 
                    toggleEventForElement('#btnFilter', 'click', AJAXWrapPageMethodCall, true, 'addFilter');
                    toggleEventForElement('#btnRemoveFilter', 'click', AJAXWrapPageMethodCall, true, 'removeFilter');
                    toggleEventForElement('#ddlUsername', 'change', AJAXWrapPageMethodCall, true, 'loadBranchPerUser');
                    toggleEventForElement('#btnSearch', 'click', AJAXWrapPageMethodCall, true, 'searchSummary');
            
                    var RegionList = dataList.RegionList;
                    var UsersList = dataList.UsersList;
                    var BranchesList = dataList.BranchesList;
                    var currentUser = dataList.currentUser;
                    var summaryBMList = dataList.SummaryBMList;
                    var isAdmn = (dataList.AdminCode).includes(currentUser.RoleID);
                    var isBM = (dataList.BHCode).includes(currentUser.RoleID);
                    var isGH = (dataList.GroupCode).includes(currentUser.RoleID);

                if (RegionList.length > 0) RegionList.splice(0, 0, { description: 'All', value: '' });
                if (UsersList.length > 0) UsersList.splice(0, 0, { description: '', value: '' });
                if (BranchesList.length > 0) BranchesList.splice(0, 0, { description: 'All', value: '' });
                

                loadDropdown('ddlRegion',RegionList, (isGH? false : true));
                //loadDropdown('ddlRegion',RegionList, (isTA? false : true));
                loadDropdown('ddlBranches', BranchesList,(isAdmn? false : true));
              
                if ($('#ddlRegion').hasClass('es-input')) $('#ddlRegion').attr("placeholder", "All");
                if ($('#ddlBranches').hasClass('es-input')) $('#ddlBranches').attr("placeholder", "All");
                setDropdownDefaultValue('ddlRegion', (isGH? currentUser.GroupCode : RegionList))
                //setDropdownDefaultValue('ddlRegion', (isTA? currentUser.GroupCode : RegionList))

               //isGH? setDropdownDefaultValue('ddlRegion', currentUser.GroupCode) : '';
               //isBM? setDropdownDefaultValue('ddlUsername', currentUser.Username) : '';
                
                // setDropdownDefaultValue('ddlRegion', (isGH? currentUser.GroupCode :''));
                 toggleEventForElement("#ddlRegion", "change", changeRegion, true);
                 createSummaryTable(summaryBMList);
               //enableControl('ddlBranches',false);

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
      $("#tBodySummaryBM").empty();
            for (var r = 0; r < result.length; r++) {
                list.push(result[r]);
            }
            secureStorage.setItem('list', JSON.stringify(list));
            allItems = result.length;
            createTable("divSummaryBM", "tblSummaryBM", "tBodySummaryBM", list, index, itemsPerPage, allItems, "list", "SummaryBM");
 }


 function searchSummary()
 {
  var obj = new Object();
  obj.Branch = getDropdownValue('ddlBranches');
  obj.RegionCode = getDropdownValue('ddlRegion');
   loadingScreen(false, 0);
    PageMethods.SearchBranchFilter(obj,
        function OnSuccess(result) {
            if (result != "") {
                   createSummaryTable(result);
            }
            else
            {
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

 function changeRegion()
 {
   var obj = new Object();
   obj.RegionCode = getDropdownValue("ddlRegion");
   obj.RoleID = '8'
    PageMethods.BranchesListRegion(obj, 
        function OnSuccess(result) {
            if (result.BranchesList != "") {
             var BranchesList = result.BranchesList;
             BranchesList.splice(0, 0, { description: 'All', value: '' });
             loadDropdown('ddlBranches', BranchesList);
             enableControl('ddlBranches',true);
                  //window.location = "/Views/PotentialAccount/SummaryPerBMAnnual.aspx";
            }
            else
            {
             setDropdownDefaultText('ddlBranches', '');
             setDropdownDefaultValue('ddlBranches', '');
             enableControl('ddlBranches',false);

            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
 }

 function viewSummaryBM(Fullname,Branch,Position,UserID)
 {
   loadingScreen(false, 0);
   var obj = new Object();
   obj.Fullname = Fullname;
   obj.Branch = Branch;
   obj.Position = Position;
   obj.UploadBy = UserID;
    PageMethods.SummaryPerBMDetails(obj,
        function OnSuccess(result) {
            if (result == "") {
                  window.location = "/Views/PotentialAccount/SummaryPerBMAnnual.aspx";
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
 }