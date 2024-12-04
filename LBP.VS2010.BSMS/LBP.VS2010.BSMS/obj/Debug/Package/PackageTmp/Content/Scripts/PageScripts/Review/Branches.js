$(document).ready(function () {
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
                    var ModalRegionList = dataList.RegionList;
                    var AllBranchList = dataList.AllBranchList;
                    var currentUser = dataList.currentUser;
                    var isAdmn = dataList.AdminCode.includes(currentUser.RoleID);
                    var isBM = dataList.BMCode.includes(currentUser.RoleID);
                    var isGH = dataList.GroupCode.includes(currentUser.RoleID);
                  
                   loadDropdown('ddlAddRegionModal',ModalRegionList, (isGH? false : true));
                   setDropdownDefaultValue('ddlAddRegionModal', (isGH? currentUser.GroupCode : ModalRegionList));
               
                 if (RegionList.length > 0) RegionList.splice(0, 0, 0, {value: '',description: 'All', data: ''});
                 loadDropdown('ddlRegion',RegionList, (isGH? false : true));
                 if ($('#ddlRegion').hasClass('es-input')) $('#ddlRegion').attr("placeholder", "All");
                 setDropdownDefaultValue('ddlRegion', (isGH? currentUser.GroupCode : RegionList));
                
                
                 isGH?showDiv(['btnSearch'],false):showDiv(['btnSearch'],true);
                 isAdmn?showDiv(['ddlAddRegionModal'],true):showDiv(['txtRegion'],false);

                 toggleEventForElement("#btnSearch", "click", searchBranchPerRegion, true);
                 toggleEventForElement("#btnAddBranches", "click", AddBranches, true);
                 toggleEventForElement("#btnAddModalSave", "click", AddSaveBranches, true);
                 toggleEventForElement("#btnUpdateModalSave", "click", UpdateBranches, true);
                 toggleEventForElement("#btnAddModalCancel", "click", cancelModal, true);
                 toggleEventForElement("#btnModalclose", "click", cancelModal, true);
             
                // isGH?createSummaryTableGH(AllBranchList):"";
                 //isGH?showDiv(['divBranchesGH'],true):showDiv(['divBranchesSH'],false);
                 //isAdmn?createSummaryTableAdmn(AllBranchList):"";
                 
                 //canUpdate?createSummaryTableCanUpdateDelete(AllBranchList):createSummaryTable(AllBranchList);

                 //isAdmn?showDiv(['divBranchesSH'],true):showDiv(['divBranchesGH'],false);

                 createSummaryTable(AllBranchList) //For Review Branch
                 sessionStorage.setItem('isGH',isGH);
                 sessionStorage.setItem('isAdmn',isAdmn);
                 sessionStorage.setItem('isBM',isBM);
                 sessionStorage.setItem('GroupCode',currentUser.GroupCode);
                 sessionStorage.setItem('canUpdate',canUpdate);
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

 function createSummaryTableCanUpdateDelete(result)
 {
            var index = 1;
            var itemsPerPage = 10;
            var list = [];
            $("#tBodyBranchesGH").empty();
            for (var r = 0; r < result.length; r++) {
                list.push(result[r]);
            }
            secureStorage.setItem('list', JSON.stringify(list));
            allItems = result.length;
            createTable("divBranchesGH", "tblBranchesGH", "tBodyBranchesGH", list, index, itemsPerPage, allItems, "list", "Branches");
 }

  function createSummaryTable(result)
 {
            var index = 1;
            var itemsPerPage = 10;
            var list = [];
            $("#tBodyBranchesSH").empty();
            for (var r = 0; r < result.length; r++) {
                list.push(result[r]);
            }
            secureStorage.setItem('list', JSON.stringify(list));
            allItems = result.length;
            createTable("divBranchesSH", "tblBranchesSH", "tBodyBranchesSH", list, index, itemsPerPage, allItems, "list", "Branches");
 }

 function searchBranchPerRegion()
 {
   loadingScreen(false, 0);
   var obj = new Object();
   obj.RegionCode = getDropdownValue('ddlRegion');
    PageMethods.SearchBranchPerReg(obj,
                    function OnSuccess(result) {
                        if (result.AllBranchList != '') {
                        var isAdmn = result.AdminCode.includes(result.currentUser.RoleID);
                        var isBM = result.BMCode.includes(result.currentUser.RoleID);
                        var isGH = result.GroupCode.includes(result.currentUser.RoleID);
                        var canUpdate = sessionStorage.getItem('canUpdate');
                       
                        createSummaryTable(result.AllBranchList); //For Review Branch 
                         //  canUpdate == 'true'?createSummaryTableCanUpdateDelete(result.AllBranchList):createSummaryTable(result.AllBranchList);
                         
                         //   isGH?createSummaryTableGH(result.AllBranchList):"";
                         //   isGH?showDiv(['divBranchesGH'],true):showDiv(['divBranchesSH'],false);
                         //   isAdmn?createSummaryTableAdmn(result.AllBranchList):"";
                         //   isAdmn?showDiv(['divBranchesSH'],true):showDiv(['divBranchesGH'],false);
                           
                        } else {
                            showError(result.message);
                        }

                        loadingScreen(true, 0);
                    },
                    function OnError(err) {
                        showErrorModal(err.get_message());
                        loadingScreen(true, 0);
                    });
            
   
 }

 function AddBranches()
 {  
     var isGH, isAdmin,isBM,GroupCode;
       
     isGH = sessionStorage.getItem('isGH');
     isAdmin = sessionStorage.getItem('isAdmn');
     isBM = sessionStorage.getItem('isBM');
     GroupCode = sessionStorage.getItem('GroupCode');

     setLabelText('modalTitle','Add Branch');
     showDiv(['btnAddModalSave'],true);
     showDiv(['btnUpdateModalSave'],false);
     $('#divAddBranchModal').modal('show');
     isGH == "true"?setDropdownDefaultValue('ddlAddRegionModal',GroupCode):$("#ddlAddRegionModal").val('');
     $("#ddlAddRegionModal").addClass("requiredField");
     setTextboxValue('txtBranchCodeModal',"");
     setTextboxValue('txtBranchNameModal',"");
     showDiv(['divModalAddError'],false);
    
 }

 function editBranches(BranchCode,BranchName,RegionName,RegionCode)
 {
   setLabelText('modalTitle','Edit Branch');
   $('#divAddBranchModal').modal('show');
   showDiv(['btnAddModalSave'],false);
   showDiv(['btnUpdateModalSave'],true);
     
   setTextboxValue('txtBranchCodeModal',BranchCode);
   setTextboxValue('txtBranchNameModal',BranchName);
   showDiv(['divModalAddError'],false);
 }

 function deleteBranches(BranchCode,BranchName,RegionName,RegionCode)
 {
   var obj = new Object();
   obj.BranchCode = BranchCode;
   obj.BranchName = BranchName;
   obj.RegionName = RegionName;
   obj.RegionCode = RegionCode

  showConfirmModal('delete', function () {
                loadingScreen(false, 0);
                PageMethods.DeleteBranches(obj,
                    function OnSuccess(result) {
                        if (result.message == '') {
                            showSuccessModal('deleted', window.location.href = "/Views/Review/Branches.aspx");
                           
                        } else {
                            showError(result.message);
                        }

                        loadingScreen(true, 0);
                    },
                    function OnError(err) {
                        showErrorModal(err.get_message());
                        loadingScreen(true, 0);
                    });
            });
 }

 function AddSaveBranches()
 {
     var obj = new Object();
     obj.BranchCode = getTextboxValue('txtBranchCodeModal');
     obj.BranchName = getTextboxValue('txtBranchNameModal');
     obj.RegionCode = getDropdownValue('ddlAddRegionModal');
     obj.message = "For Add";
     showConfirmModal('save', function () {
                loadingScreen(false, 0);
                PageMethods.AddUpdateBranch(obj,
                    function OnSuccess(result) {
                        if (result.errMsg == '') {
                            showSuccessModal('save');
                            window.location.href = "/Views/Review/Branches.aspx";
                        } else {
                           showDiv(['divModalAddError'],true);
                           showSpecificError(result.errMsg, 'lblModalAddErrorMessage', 'divModalAddError');
                        }
                        loadingScreen(true, 0);
                    },
                    function OnError(err) {
                        showErrorModal(err.get_message());
                        loadingScreen(true, 0);
                    });
            });
 }

 function UpdateBranches()
 {
     var obj = new Object();
     obj.BranchCode = getTextboxValue('txtBranchCodeModal');
     obj.BranchName = getTextboxValue('txtBranchNameModal');
     obj.RegionCode = getDropdownValue('ddlAddRegionModal');
     obj.message = "For Update";
     showConfirmModal('update', function () {
                loadingScreen(false, 0);
                PageMethods.AddUpdateBranch(obj,
                    function OnSuccess(result) {
                        if (result.errMsg == '') {
                            showSuccessModal('updated');
                            window.location.href = "/Views/Review/Branches.aspx";
                        } else {
                        showDiv(['divModalAddError'],true);
                           showSpecificError(result.errMsg, 'lblModalAddErrorMessage', 'divModalAddError');
                        }
                        loadingScreen(true, 0);
                    },
                    function OnError(err) {
                        showErrorModal(err.get_message());
                        loadingScreen(true, 0);
                    });
            });
 }

 function cancelModal()
 {
     
     $('#divAddBranchModal').modal('hide');
 }