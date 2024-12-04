$(document).ready(function () {
    sessionStorage.BranchCode = "";
    loadingScreen(false, 0);
   
    toggleEventForElement('#ddlSearchRegion', 'change', AJAXWrapPageMethodCall, true, 'ddlSearchRegionOnChange');
    toggleEventForElement('#ddlRegionTemp', 'change', AJAXWrapPageMethodCall, true, 'ddlSearchRegionTempOnChange');
   

    var deferred = $.Deferred();

    PageMethods.OnLoadData(
        function OnSuccess(result) {
            deferred.resolve(result);
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    //$.when(deferreds.insert, deferreds.update, deferreds.approve, deferreds.roleList).done(async function (canInsert, canUpdate, canApprove, roleList) {
    $.when(deferred).done(async function (data) {
        var currentUser = data.currentUser;
        var canInsert = data.CanInsert;
        var canUpdate = data.CanUpdate;
        var canApprove = data.CanApprove;
        var branchList = data.BranchLst;
        var regionList = data.RegionLst;
        sessionStorage.GroupCode = currentUser.GroupCode;
        try {
            var access = {
                CanModify: canInsert && canUpdate,
                CanApprove: canApprove
            };

            $('#acc-Main').data('access', access);

            if (canInsert && canUpdate) {
              
                loadDropdown('ddlRegion', regionList, false);
                
                sessionStorage.BranchCode = '';

                var lstFilter2 = regionList; 
                lstFilter2.splice(0, 0, { description: 'All', value: 'All' });
                if (lstFilter2.length > 1) lstFilter2.splice(0, 0, { value: '' });
                loadDropdown('ddlSearchRegion', lstFilter2, false, 'All');
                setDropdownDefaultValue('ddlSearchRegion', currentUser.GroupCode);
                 
                toggleEventForElement('#btnSearch', 'click', AJAXWrapPageMethodCall, true, 'searcLst');
                toggleEventForElement('#ddlRegion', 'change', AJAXWrapPageMethodCall, true, "loadBranchPerRegion");
                toggleEventForElement("#ddlBranch", 'change', AJAXWrapPageMethodCall,true,'loadBranchManager');
             
                toggleEventForElement('#btnAdd', 'click', AJAXWrapPageMethodCall, true, 'addUserTarget');

                var result = await getList(currentUser.GroupCode, '');

                createUserTargetTable(result);
            }

            else {
                var element = document.getElementById('acc-UserTarget');
                element.parentNode.removeChild(element);
            }

            if (canApprove) {
                toggleEventForElement('#btnSearchTemp', 'click', AJAXWrapPageMethodCall, true, 'searcLstTemp');
                var lstFilter3 = regionList;
                lstFilter3.splice(0, 0, { description: 'All', value: 'All' });
                if (lstFilter3.length > 1) lstFilter3.splice(0, 0, { value: '' });
                loadDropdown('ddlRegionTemp', lstFilter3, false, 'All');
                setDropdownDefaultValue('ddlRegionTemp', currentUser.GroupCode);
                 
                var regionCode = getDropdownValue('ddlRegionTemp');

                var result = await getTempList(regionCode, '','');

                createUserTargetTempTable(result);

                toggleEventForElement('#btnApprove', 'click', AJAXWrapPageMethodCall, true, 'approveUserTarget');
                toggleEventForElement('#btnReject', 'click', AJAXWrapPageMethodCall, true, 'rejectUserTarget');

            }
            else {
                var element = document.getElementById('acc-UserTargetTemp');
                element.parentNode.removeChild(element);
            }

            var modalUserTarget = document.getElementById('modalUserTarget');

        } catch (e) {
            showErrorModal(e);
        }

        loadingScreen(true, 0);
    }).fail(function (e) {
        showErrorModal(e);
        loadingScreen(true, 0);
    });

});



function ddlSearchRegionOnChange() {
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);

    var region = getDropdownValue('ddlSearchRegion');
    PageMethods.GetBranchPerRegion(region,
        function OnSuccess(result) {
            var branchList = result;

            var lstFilter = branchList;
            lstFilter.splice(0, 0, { description: 'All', value: 'All' });
            if (lstFilter.length > 1) lstFilter.splice(0, 0, { value: '' });
            loadDropdown('ddlSearchBranches', lstFilter, true, 'All');

            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });


    loadingScreen(true, 0);
}

function ddlSearchRegionTempOnChange() {
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);

    var region = getDropdownValue('ddlRegionTemp');
    PageMethods.GetBranchPerRegion(region,
        function OnSuccess(result) {
            var branchList = result;

            var lstFilter = branchList;
            lstFilter.splice(0, 0, { description: 'All', value: 'All' });
            if (lstFilter.length > 1) lstFilter.splice(0, 0, { value: '' });
            loadDropdown('ddlBranchTemp', lstFilter, true, 'All');

            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });


    loadingScreen(true, 0);
}


function loadBranchPerRegion() {
    var brCode =sessionStorage.BranchCode;
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);
    //var region = getDropdownValue('ddlRegion');
    var region = sessionStorage.GroupCode;
    var actionType =sessionStorage.Action;
    var inEdit =sessionStorage.InEdit;
    var res=true;
    if ((actionType=='view') || actionType=='edit' || (inEdit == 1))
     { 
       res=false
     }
    PageMethods.GetBranchPerRegion(region,
        function OnSuccess(result) {
            var lstFilter = result;

            lstFilter.splice(0, 0, { description: 'Please Select', value: '' });
            lstFilter.splice(lstFilter.findIndex(a => a.value == ''), 1);

            loadDropdown('ddlBranch', lstFilter, res, brCode,'');
            
            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
             loadingScreen(true, 0);
            });
    }

    
 function loadBranchManager() {
        var obj = new Object();
        var bHName =sessionStorage.Username;
        
        obj.GroupCode = sessionStorage.GroupCode;
       
        try {
            obj.BranchCode = getDropdownValue('ddlBranch');
        } catch (e) {
            obj.BranchCode = '';
        }

       sessionStorage.BranchCode = obj.BranchCode;
     
        obj.Role = "8";

        var actionType =sessionStorage.Action;
        var inEdit =sessionStorage.InEdit;
        var res=true;
        if ((actionType=='view') || (actionType=='edit') || (inEdit == 1))
        { res=false }

        PageMethods.BranchManagerListRegionBranch(obj,
            function OnSuccess(result) {
                var bhList = result;

                bhList.splice(0, 0, { description: 'Please Select', value: '' });
                bhList.splice(bhList.findIndex(a => a.value == ''), 1);

                loadDropdown('ddlUserFullName', bhList, res, bHName,'');
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
                loadDropdown('ddlRegion', result, false, '', 'All');
            }
            loadingScreen(true, 0);
        },
        function OnError() {
            alert(err.get_message());
        });
}

function getList(regionCode,brCode) {
    var deferred = $.Deferred();
    if ((brCode == null) || (brCode == 'All')) {
        brCode = '';
    }
    PageMethods.GetList(regionCode,brCode,
        function OnSuccess(result) {
            if (result != null) {
               deferred.resolve(result);
                         }
            else {
                deferred.reject('There was a system problem while processing your request. Please verify if the transaction was posted correctly and report the problem to IT Support.');
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    return deferred.promise();
}

function getTempList(regionCode, brCode, filterStatus) {
    if ((brCode == null) || (brCode == 'All')) {
        brCode = '';
    }
    var deferred = $.Deferred();

    PageMethods.GetTempList(regionCode,brCode, filterStatus,
        function OnSuccess(result) {
            if (result != null) {
                deferred.resolve(result);
            }
            else {
                deferred.reject('There was a system problem while processing your request. Please verify if the transaction was posted correctly and report the problem to IT Support.');
            }
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    return deferred.promise();
}


function createUserTargetTable(result) {
    var index = 1;
    var itemsPerPage = 10;
    var list = [];

    $('#tBodyUserTarget').empty();
    for (var r = 0; r < result.length; r++) {
        list.push(result[r]);
    }

    secureStorage.setItem('list', JSON.stringify(list));
    allItems = result.length;

       createTable('divUserTarget', 'tblUserTarget', 'tBodyUserTarget', list, index, itemsPerPage, allItems, 'list', 'UserTarget');
}


function createUserTargetTempTable(result) {
    var index = 1;
    var itemsPerPage = 10;
    var list = [];

    $('#tBodyUserTargetTemp').empty();
    for (var r = 0; r < result.length; r++) {
        list.push(result[r]);
    }

    secureStorage.setItem('listTemp', JSON.stringify(list));
    allItems = result.length;

    createTable('divUserTargetTemp', 'tblUserTargetTemp', 'tBodyUserTargetTemp', list, index, itemsPerPage, allItems, 'listTemp', 'UserTargetTemp');
}

async function searcLst() {
    loadingScreen(false, 0);

    try {
        //var regionCode = getDropdownValue('ddlRegion');
        var regionCode = sessionStorage.GroupCode;
        var brCode = getDropdownValue('ddlSearchBranches');
       
        var result = await getList(regionCode,brCode);

        createUserTargetTable(result);
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

async function searcLstTemp() {
    loadingScreen(false, 0);

    try {
        var regionCode = getDropdownValue('ddlRegionTemp');
        var brCode = getDropdownValue('ddlBranchTemp');
        var filterStatus = getDropdownValue('ddl-s-t-f');

        var result = await getTempList(regionCode,brCode,filterStatus);

        createUserTargetTempTable(result);
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

async function reload() {
    loadingScreen(false, 0);

    var access = $('#acc-Main').data('access');

    try {
        if (access.CanModify) {

            var region = sessionStorage.GroupCode;
            var branch = getDropdownValue('ddlBranch');
            
            var result = await getList(region,'');
            createUserTargetTable(result);
        }

        if (access.CanApprove) {
            var regionCode = getDropdownValue('ddlRegionTemp');
            var brCode = getDropdownValue('ddlBranchTemp');
            var filterStatus = getDropdownValue('ddl-s-t-f');

            var result = await getTempList(regionCode,brCode,filterStatus);

            createUserTargetTempTable(result);
        }
    } catch (e) {
        showErrorModal(e);
    }

    loadingScreen(true, 0);
}


function closeModal(modalId, callbackOK) {
    $('#' + modalId).modal('hide');

    if (callbackOK) {
        if (typeof callbackOK != "object") {
            callbackOK();
        }
    }
}

function addUserTarget() {
    loadUser(null, 'add');
}

function viewUserTarget(username) {
    loadUser(username, 'view');
}

function viewUserTargetTemp(username, tempStatus, fullname) {
    hideError();
    loadingScreen(false, 0);

    PageMethods.GetMergeDetails(username,
        function OnSuccess(result) {
            if (result != null) {

                var index = 1;
                var itemsPerPage = 50;
                var list = [];

                $('#tBodyApproval').empty();
                for (var r = 0; r < result.length; r++) {
                    list.push(result[r]);
                }

                secureStorage.setItem('listApproval', JSON.stringify(list));
                allItems = result.length;

                createTable('divApproval', 'tblApproval', 'tBodyApproval', list, index, itemsPerPage, allItems, 'listApproval', 'UserTarget');

                var header = 'For Approval - ';
                if (tempStatus == '6') header += 'New';
                if (tempStatus == '5') header += 'Update';
                
                header += ' (' + username + ')';

                setLabelText('span-u-t-a', header);

                $('#modalUserTargetTemp').data('userTargetTempData', { UserName: username, TempStatusID: tempStatus, Fullname: fullname });
                $('#modalUserTargetTemp').modal('show');
            }
            else {
                showError('There was a system problem while processing your request. Please verify if the transaction was posted correctly and report the problem to IT Support.');
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}



function editUserTarget(username) {
    loadUser(username, 'edit');
}

function approveUserTarget() {
    hideSpecificError('lbl-e-t-m');

    showConfirmModal("approve", function () {
        loadingScreen(false, 0);

        var postData = $('#modalUserTargetTemp').data('userTargetTempData');

        PageMethods.ApproveUserTarget(postData,
            function (result) {
                if (result == '') {
                    showSuccessModal('approved', function () {
                        closeModal('modalUserTargetTemp', reload);
                    });
                }
                else {
                    showSpecificError(result, 'lbl-e-t-m', 'div-e-t-m');
                }
                loadingScreen(true, 0);
            },
            function (err) {
                showErrorModal(err.get_message());
                loadingScreen(true, 0);
            });
    });
}

function rejectUserTarget() {
    hideSpecificError('lbl-e-t-m');

    showConfirmModal("reject", function () {
        loadingScreen(false, 0);

        var postData = $('#modalUserTargetTemp').data('userTargetTempData');

        PageMethods.RejectUserTarget(postData,
            function (result) {
                if (result == '') {
                    showSuccessModal('rejected', function () {
                        closeModal('modalUserTargetTemp', reload);
                    });
                }
                else {
                    showSpecificError(result, 'lbl-e-t-m', 'div-e-t-m');
                }
                loadingScreen(true, 0);
            },
            function (err) {
                showErrorModal(err.get_message());
                loadingScreen(true, 0);
            });
    });
}

function getDetails(username, action) {
    var deferred = $.Deferred();

    PageMethods.GetDetails(username, action,
        function OnSuccess(result) {
            deferred.resolve(result);
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    return deferred.promise();
}


async function loadUser(username, action) {
    hideError();
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);
  
    try {
        var result = await getDetails(username, action)
        var mdlHdr = 'Add New User';

        var inEdit = result.InEdit;

        switch (action) {
            case 'view':
                mdlHdr = 'View User Target - ' + result.Username;
                break;

            case 'edit':
                mdlHdr = 'Edit User Target - ' + result.Username;
                break;

            default:
                mdlHdr = 'Add New User Target';
        }

        if ((result.UserStatus =='1') || (result.UserStatus ==null))
        {
        setLabelText('span-u-a', mdlHdr);
        sessionStorage.BranchCode = result.BranchCode;
        secureStorage.setItem('_BranchCode', result.BranchCode);
        setDropdownDefaultValue('ddlRegion', sessionStorage.GroupCode);
       
        setTextboxValue('txtCasaTarget', result.CASATarget);
        setTextboxValue('txtLoansTarget', result.LoansTarget);
        setDropdownDefaultValue('ddlBranch', result.BranchCode);
        if (!(result.Username==null)) {
         setDropdownDefaultValue('ddlUserFullName', result.Username);
        }
       
        sessionStorage.Username = result.Username;
        sessionStorage.Action = action;
        sessionStorage.InEdit = inEdit;
        showDiv(['btnSave'], !inEdit && !(action == 'view'));
        showDiv(['divEditLabel'], inEdit && action == 'edit');
        
        toggleEventForElement('#btnSave', 'click', AJAXWrapPageMethodCall, !inEdit && !(action == 'view'), 'ValidateUserTarget');
        
        disableControls(['ddlRegion','ddlBranch','ddlUserFullName','txtCasaTarget','txtLoansTarget'], action == 'view' || inEdit === 1);
        disableControls(['ddlRegion'], true);
        $('#modalUserTarget').data('userTargetData', result);
        $('#modalUserTarget').modal('show');
        }
        else
        {
         showErrorModal("Selected branch head "+username+" is not active.");
         loadingScreen(true, 0);
        }
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

function ValidateUserTarget() {
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);

    var userTargetData = $('#modalUserTarget').data('userTargetData');
  
    var brcode = getDropdownValue('ddlBranch');
    var regionCode =getDropdownValue('ddlRegion');
    var casaTarget=getTextboxValue('txtCasaTarget');
    var loansTarget=getTextboxValue('txtLoansTarget');
    var username = getDropdownValue('ddlUserFullName');
    
    var postData = {
        Action: userTargetData.Action,
        BranchCode:brcode,
        GroupCode:regionCode,
        CASATarget:casaTarget,
        LoansTarget: loansTarget,
        Username: username
    };
  
    if (userTargetData.Action == 'edit') {
        var bool = true;
        for (const key in postData) {
            if (userTargetData.hasOwnProperty(key)) {
                if (postData[key] !== userTargetData[key]) {
                    bool = false
                    break;
                }
            }
        }

        if (bool) {
            showSpecificError('There are no changes made.', 'lblUserErrorMessage', 'divUserTargetError');
            loadingScreen(true, 0);
            return;
        }
    }


    PageMethods.ValidateUserTarget(postData,
        function (result) {
            if (result == '') {
                showConfirmModal("save", saveUserTarget, null, postData);
            }
            else {
                showSpecificError(result, 'lblUserErrorMessage', 'divUserTargetError');
            }
            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}


function saveUserTarget(obj) {

    loadingScreen(false, 0);

        PageMethods.SaveTarget(obj,
        function (result) {
            if (result == '') {
                showSuccessModal('submitted for approval', function () {
                    closeModal('modalUserTarget', reload);
                });
            }
            else {
                showSpecificError(result, 'lblUserErrorMessage', 'divUserTargetError');
            }
            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
  }

  
function EnableModalFields() {
    document.getElementById("ddlBranch").disabled = false;
   // document.getElementById("ddlRegion").disabled = false;
    document.getElementById("txtCasaTarget").disabled = false;
    document.getElementById("txtLoansTarget").disabled = false;
    
}