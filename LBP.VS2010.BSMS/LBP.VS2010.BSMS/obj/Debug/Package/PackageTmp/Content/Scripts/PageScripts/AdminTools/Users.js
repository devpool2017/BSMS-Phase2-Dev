$(document).ready(function () {
sessionStorage.BranchCode ="";
    loadingScreen(false, 0);
     toggleEventForElement("#ddlRole", 'change', ddlRoleOnChange, true);
     toggleEventForElement('#ddlSearchRegion', 'change', AJAXWrapPageMethodCall, true, 'ddlSearchRegionOnChange');
    

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
        var canInsert = data.CanInsert;
        var canUpdate = data.CanUpdate;
        var canApprove = data.CanApprove;
        var canUnlock = data.CanUnlock;
        var roleList = data.RoleLst;
        var branchList = data.BranchLst;
        var regionList = data.RegionLst;
        var GH = data.GH;
        var BH = data.BH;
        var TA = data.TA;
        sessionStorage.GHId = GH;
        sessionStorage.BHId = BH;
        sessionStorage.TAId = TA;

        try {
            var access = {
                CanModify: canInsert && canUpdate,
                CanApprove: canApprove,
                CanUnlock: canUnlock
            };

            $('#acc-Main').data('access', access);
           
            if (canInsert && canUpdate) {
                
                loadDropdown('ddlRole', roleList, true);
             
                loadDropdown('ddlRegion', regionList, true);
                
                var lstFilter = roleList;
                lstFilter.splice(0, 0, { description: 'All', value: 'All' });
                if (lstFilter.length > 1) lstFilter.splice(0, 0, { value: '' });
                loadDropdown('ddl-r-f', lstFilter, true, 'All');


                var lstFilter2 = regionList;
                lstFilter2.g
                lstFilter2.splice(0, 0, { description: 'All', value: 'All' });
                if (lstFilter2.length > 1) lstFilter2.splice(0, 0, { value: '' });
                 loadDropdown('ddlSearchRegion', lstFilter2, true, 'All');

                toggleEventForElement('#btnSearch', 'click', AJAXWrapPageMethodCall, true, 'searcLst');
                toggleEventForElement('#ddlRegion', 'change', AJAXWrapPageMethodCall, true, "loadBranchPerRegion");
                toggleEventForElement('#btnDownload', 'click', AJAXWrapPageMethodCall, true, 'generateReport')
            
                toggleEventForElement('#btnReset', 'click', function () {
                    setTextboxValue('txtUser', '');
                    setDropdownDefaultValue('ddl-s-f', '');
                    setDropdownDefaultValue('ddl-r-f', 'All');
                    searcLst();
                }, true);

                toggleEventForElement('#btnAdd', 'click', AJAXWrapPageMethodCall, true, 'addUser');

                var result = await getList('', '', '','','','All','All','All','All');

                createUserTable(result);}
            
            else {
                var element = document.getElementById('acc-Users');
                element.parentNode.removeChild(element);
            }

            if (canApprove) {
                toggleEventForElement('#btnSearchTemp', 'click', AJAXWrapPageMethodCall, true, 'searcLstTemp');
                toggleEventForElement('#btnResetTemp', 'click', function () {
                    setTextboxValue('txtUserTemp', '');
                    setDropdownDefaultValue('ddl-s-t-f', '');
                    searcLstTemp();
                }, true);

              
                var result = await getTempList('', '');

                createUserTempTable(result);

                toggleEventForElement('#btnApprove', 'click', AJAXWrapPageMethodCall, true, 'approveUser');
                toggleEventForElement('#btnReject', 'click', AJAXWrapPageMethodCall, true, 'rejectUser');

            }
            else {
                var element = document.getElementById('acc-UsersTemp');
                element.parentNode.removeChild(element);
            }

            var modalUser = document.getElementById('modalUser');

            modalUser.addEventListener('hide.bs.modal', function () {
                toggleEventForElement('#btnSave', 'click', AJAXWrapPageMethodCall, false, 'validateUser');
                toggleEventForElement('#btnRstPword', 'click', AJAXWrapPageMethodCall, false, 'resetUser');
                toggleEventForElement('#btnValidate', 'click', AJAXWrapPageMethodCall, false, 'validateUserAPI');
                toggleEventForElement("#ddlRole", 'change', ddlRoleOnChange, true);
                toggleEventForElement('#ddlRegion', 'change', AJAXWrapPageMethodCall, true, "loadBranchPerRegion");
                clearModalFields();
            });

        } catch (e) {
            showErrorModal(e);
        }

        loadingScreen(true, 0);
    }).fail(function (e) {
        showErrorModal(e);
        loadingScreen(true, 0);
    });
});




function getList(searchText, filterStatus, filterRole,filterRegion,filterBranch, filterStatusDesc, filterRoleName,filterRegionName,filterBranchName) {
    var deferred = $.Deferred();

    PageMethods.GetList(searchText, filterStatus, filterRole,filterRegion,filterBranch, filterStatusDesc, filterRoleName,filterRegionName,filterBranchName,
        function OnSuccess(result) {
            if (result != null) {
               deferred.resolve(result);
               // createUserTable(result);
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

function getTempList(searchText, filterStatus) {
    var deferred = $.Deferred();

    PageMethods.GetTempList(searchText, filterStatus,
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

function createUserTable(result) {
    var index = 1;
    var itemsPerPage = 10;
    var list = [];

    $('#tBodyUsers').empty();
    for (var r = 0; r < result.length; r++) {
        list.push(result[r]);
    }

    secureStorage.setItem('list', JSON.stringify(list));
    allItems = result.length;

       createTable('divUsers', 'tblUsers', 'tBodyUsers', list, index, itemsPerPage, allItems, 'list', 'User');
}


function createUserTempTable(result) {
    var index = 1;
    var itemsPerPage = 10;
    var list = [];

    $('#tBodyUsersTemp').empty();
    for (var r = 0; r < result.length; r++) {
        list.push(result[r]);
    }

    secureStorage.setItem('listTemp', JSON.stringify(list));
    allItems = result.length;

    createTable('divUsersTemp', 'tblUsersTemp', 'tBodyUsersTemp', list, index, itemsPerPage, allItems, 'listTemp', 'UserTemp');
}
async function searcLst() {
    loadingScreen(false, 0);

    try {
        var searchText = getTextboxValue('txtUser');
        var filterStatus = getDropdownValue('ddl-s-f');
        var role = getDropdownValue('ddl-r-f');
        var filterRole = role == 'All' ? '' : role;
        var region = getDropdownValue('ddlSearchRegion');
        var filterRegion = region == 'All' ? '' : region;
        var branch = getDropdownValue('ddlSearchBranch');
        var filterBranch = branch == 'All' ? '' : branch;

        var filterStatusDesc = getDropdownText('ddl-s-f');
        var filterRoleName = getDropdownText('ddl-r-f');
        var filterRegionName = getDropdownText('ddlSearchRegion');
        var filterBranchName = getDropdownText('ddlSearchBranch');


        var result = await getList(searchText, filterStatus, filterRole,filterRegion,filterBranch, filterStatusDesc, filterRoleName,filterRegionName,filterBranchName);

        createUserTable(result);
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

async function searcLstTemp() {
    loadingScreen(false, 0);

    try {
        var searchText = getTextboxValue('txtUserTemp');
        var filterStatus = getDropdownValue('ddl-s-t-f');

        var result = await getTempList(searchText, filterStatus);

        createUserTempTable(result);
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

function addUser() {
    loadUser(null, 'add');
}

function viewUser(username) {
    loadUser(username, 'view');
}

function editUser(username) {
    loadUser(username, 'edit');
}


async function loadUser(username, action) {
    hideError();
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);

    try {
        var result = await getDetails(username, action)
        var mdlHdr = 'Add New User';

        var loginAttempt = result.PasswordAttempt;
        var inEdit = result.InEdit;

        switch (action) {
            case 'view':
                mdlHdr = 'View User - ' + result.Username;
                break;

            case 'edit':
                mdlHdr = 'Edit User - ' + result.Username;
                break;

            default:
                mdlHdr = 'Add New User';
        }

        setLabelText('span-u-a', mdlHdr);

        setDropdownDefaultValue('ddlRole', result.RoleId);
        sessionStorage.BranchCode = result.BranchCode;
        setDropdownDefaultValue('ddlRegion', result.RegionCode);
     
        disableBr(action);
        setDropdownDefaultValue('ddlStatus', result.Status);
        setTextboxValue('txtUserName', result.Username);
        setTextboxValue('txt-f-n', result.FirstName);
        setTextboxValue('txt-l-n', result.LastName);
        setTextboxValue('txt-m-i', result.MiddleInitial);
        setTextboxValue('txt-name', result.Name);
    
        setTextboxValue('txtIPAddress', result.IPAddress);
        setTextboxValue('txtEmail', result.EmailAddress);
        secureStorage.setItem('_Username', result.Username);

        disableControls(['txtUserName'], !(action == 'add'));
        disableControls(['ddlRole','ddlStatus','ddlRegion','ddlBranch','txtCasaTarget','txtCbgTarget', 'txtEmail'], action == 'view' || inEdit === 1);
        disableControls(['txt-f-n', 'txt-l-n', 'txt-m-i', 'txt-suff', 'txtIPAddress','txt-name'], action == 'view' || inEdit === 1 || (action == 'add' && result.IsAPI === 1));

        showDiv(['btnSave'], !inEdit && !(action == 'view'));
        showDiv(['btnRstPword'], !inEdit && action == 'edit');
       showDiv(['divEditLabel'], inEdit && action == 'edit');
        showDiv(['btnValidate'], result.IsAPI && action == 'add');

        toggleEventForElement('#btnSave', 'click', AJAXWrapPageMethodCall, !inEdit && !(action == 'view'), 'validateUser');
      toggleEventForElement('#btnValidate', 'click', AJAXWrapPageMethodCall, result.IsAPI && action == 'add', 'validateUserAPI');

        $('#modalUser').data('userData', result);
        $('#modalUser').modal('show');
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

async function deleteUser(username) {
    hideError();
    loadingScreen(false, 0);

    try {
        var result = await getDetails(username, null)

        if (!result.InEdit) {

            result.Action = 'delete';

            showConfirmModal("delete", function () {
                loadingScreen(false, 0);

                PageMethods.SaveUser(result,
                    function (result) {
                        if (result == '') {
                            showSuccessModal('submited for approval', function () {
                                closeModal('modalUser', reload);
                            });
                        }
                        else {
                            showError(result);
                        }
                        loadingScreen(true, 0);
                    },
                    function (err) {
                        showErrorModal(err.get_message());
                    });
            });
        }
        else {
            showError('User is in use, it cannot be deleted.');
        }
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
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

function viewUserTemp(username, tempStatus, name, firstname) {
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

                createTable('divApproval', 'tblApproval', 'tBodyApproval', list, index, itemsPerPage, allItems, 'listApproval', 'User');

                var header = 'For Approval - ';
                if (tempStatus == '6') header += 'New';
                if (tempStatus == '5') header += 'Update';
                if (tempStatus == '7') header += 'Deletion';
                if (tempStatus == '8') header += 'Unlock';
                if (tempStatus == '9') header += 'Reset Password';

                header += ' (' + username + ')';

                setLabelText('span-u-t-a', header);

                showDiv(['divUnlocked'], tempStatus == '8');
                showDiv(['divRstPword'], tempStatus == '9');

                $('#modalUserTemp').data('userTempData', { UserName: username, TempStatusID: tempStatus, Name: name, FirstName: firstname });
                $('#modalUserTemp').modal('show');
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

    
function ddlRoleOnChange() {
   
    var lstFilter = sessionStorage.RoleLst;

    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);
    var roleCode = getDropdownValue('ddlRole');
    var reg = getDropdownValue('ddlRegion');
    var br = getDropdownValue('ddlBranch');

    if (!(roleCode == sessionStorage.GHId) && !(roleCode == sessionStorage.BHId) && !(roleCode == sessionStorage.TAId)) {
        $('#ddlBranch').removeClass('required requiredField');
        $('#ddlRegion').removeClass('required requiredField');
    }
    else {
        if ((roleCode == sessionStorage.GHId) || (roleCode == sessionStorage.TAId))  {
            if ((reg == "") || (reg == null)){
                if (!$('#ddlRegion').hasClass('required requiredField')) {
                    $('#ddlRegion').addClass('required requiredField');
                }
                else {
                    $('#ddlRegion').removeClass('required requiredField');
                }
            }
            $('#ddlBranch').removeClass('required requiredField');
        }
        else if (roleCode == sessionStorage.BHId) {
            if ((reg == "") || (reg == null)) {
                if (!$('#ddlRegion').hasClass('required requiredField')) {
                    $('#ddlRegion').addClass('required requiredField');
                }
            }
            if ((br == "") || (br == null) || (br == 0)) {
                if (!$('#ddlBranch').hasClass('required requiredField')) {
                    $('#ddlBranch').addClass('required requiredField');
                }
            }
        }
    }

    loadingScreen(true, 0); 
    }

function roleValidation() {
    var br = getDropdownValue('ddlBranch');
    var roleCode = getDropdownValue('ddlRole');
    if (roleCode == "8") {
        if ((br == "") || (br == null) || (br == 0)) {
            if (!$('#ddlBranch').hasClass('required requiredField')) {
                $('#ddlBranch').addClass('required requiredField');
            }
        }
    }
    else {

        $('#ddlBranch').removeClass('required requiredField');
    }
}
function ddlRegionOnChange() {
  
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);
    
  var region = getDropdownValue('ddlRegion');
   PageMethods.GetBranchPerRegion(region,
        function OnSuccess(result) {
           var branchList = result;

            var lstFilter = branchList;
                lstFilter.splice(0, 0, { description: 'Please Select', value: '' });
                if (lstFilter.length > 1) lstFilter.splice(0, 0, { value: '' });
                loadDropdown('ddlBranch', lstFilter, true, '');
                roleValidation();
            
            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
      

 loadingScreen(true, 0);
}


function ddlSearchRegionOnChange() {
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);

  var region = getDropdownValue('ddlSearchRegion');
   PageMethods.GetBranchPerRegion(region,
        function OnSuccess(result) {
           var branchList = result;

            var lstFilter = branchList;
                lstFilter.splice(0, 0, { description: 'All', value: 'All' });
                //lstFilter.splice(lstFilter.findIndex(a => a.value == ''), 1);
                if (lstFilter.length > 1) lstFilter.splice(0, 0, { value: '' });
                loadDropdown('ddlSearchBranch', lstFilter, true, 'All');

            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
      
  
 loadingScreen(true, 0);
}

function disableBr(action)
{
    if (action == 'view')
        {
            disableControls(['ddlBranch'], (action == 'view'));
        }
}

function loadBranchPerRegion(BranchCode) {
  

    var brCode =sessionStorage.BranchCode;
    
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);
    
  var region = getDropdownValue('ddlRegion');
  //var brCode = getDropdownValue('ddlBranch');
   PageMethods.GetBranchPerRegion(region,
        function OnSuccess(result) {
           var branchList = result;

            var lstFilter = branchList;
                lstFilter.splice(0, 0, { description: 'Please Select', value: '' });
                lstFilter.splice(lstFilter.findIndex(a => a.value == ''), 1);
                
                loadDropdown('ddlBranch', lstFilter, true, brCode,'');
                //setDropdownDefaultValue('ddlBranch', brCode);
            roleValidation();
            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
      

 loadingScreen(true, 0);
}


function validateUserAPI() {
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);

    PageMethods.ValidateUserAPI(getTextboxValue('txtUserName'),
        function (result) {
            if (result.errMsg == '') {
                setTextboxValue('txt-f-n', result.FirstName);
                setTextboxValue('txt-l-n', result.LastName);
                setTextboxValue('txt-m-i', result.MiddleInitial);
                setTextboxValue('txtEmail', result.EmailAddress);
                setTextboxValue('txt-name', result.Name);
                setTextboxValue('txtIPAddress', result.IPAddress);

                $('#modalUser').data('userData', result);
            }
            else {
                setTextboxValue('txt-f-n', '');
                setTextboxValue('txt-l-n', '');
                setTextboxValue('txt-m-i', '');
                setTextboxValue('txt-name', '');
                setTextboxValue('txtEmail', '');
                setTextboxValue('txtIPAddress', '');
                showSpecificError(result.errMsg, 'lblUserErrorMessage', 'divUserError');
            }
            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

function validateUser() {
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);

    var userData = $('#modalUser').data('userData');
    var uName = getTextboxValue('txtUserName');
    var fName = getTextboxValue('txt-f-n');
    var lName = getTextboxValue('txt-l-n');
    var mInitial = getTextboxValue('txt-m-i');
    var email = getTextboxValue('txtEmail');
    var role = getDropdownValue('ddlRole');
    var brcode = getDropdownValue('ddlBranch') !== '0' ? getDropdownValue('ddlBranch') : '';
    var regionCode =getDropdownValue('ddlRegion');
    var displayName = getTextboxValue('txt-name');
    var IPAddress = getTextboxValue('txtIPAddress');
    //var EcasaTarget=getTextboxValue('txtEcasaTarget');
    //var EcbgTarget=getTextboxValue('txtEcbgTarget');
    //var CasaTarget=getTextboxValue('txtCasaTarget');
    //var CbgTarget=getTextboxValue('txtCbgTarget');
   

    if (userData.IsAPI && userData.Action == 'add') {
       if (!userData.Username) {
      
            showSpecificError('Please click Validate button to refresh user information.', 'lblUserErrorMessage', 'divUserError');
            loadingScreen(true, 0);
            return;
        }

        //if (uName && (uName.toUpperCase() != userData.Username.toUpperCase() || fName != userData.FirstName || lName != userData.LastName || mInitial != userData.MiddleInitial || email != userData.EmailAddress)) {
        //    showSpecificError('User information do not match with the username. Please click Validate button to refresh user information.', 'lblUserErrorMessage', 'divUserError');
        //    loadingScreen(true, 0);
        //    return;
        //}
    }

    var postData = {
        Action: userData.Action,
        RoleId: role,
    
        UserName: userData.Action == 'edit' ? userData.Username : uName,
        FirstName: fName,
        LastName: lName,
        MiddleInitial: mInitial,
        BranchCode: brcode,
        RegionCode: regionCode,
        IPAddress: getTextboxValue('txtIPAddress'),
        EmailAddress: email,
        Status: getDropdownValue('ddlStatus'),
        //EcasaTarget:EcasaTarget,
        //EcbgTarget:EcbgTarget,
        //CasaTarget:CasaTarget,
        //CbgTarget:CbgTarget
        
    };

    if (userData.Action == 'edit') {
        var bool = true;
        for (const key in postData) {
            if (userData.hasOwnProperty(key)) {
                if (postData[key] !== userData[key]) {
                    bool = false
                    break;
                }
            }
        }

        if (bool) {
            showSpecificError('There are no changes made.', 'lblUserErrorMessage', 'divUserError');
            loadingScreen(true, 0);
            return;
        }
    }

    PageMethods.ValidateUser(postData,
        function (result) {
            if (result == '') {
                showConfirmModal("save", saveUser, null, postData);
            }
            else {
                showSpecificError(result, 'lblUserErrorMessage', 'divUserError');
            }
            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

function saveUser(obj) {
    loadingScreen(false, 0);

    PageMethods.SaveUser(obj,
        function (result) {
            if (result == '') {
                showSuccessModal('submitted for approval', function () {
                    closeModal('modalUser', reload);
                });
            }
            else {
                showSpecificError(result, 'lblUserErrorMessage', 'divUserError');
            }
            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

function unlockUser(Username) {
    hideSpecificError('lblUserErrorMessage');

    showConfirmModal("unlock", function () {
        //var postData = $('#modalUser').data('userData');
        loadingScreen(false, 0);
        PageMethods.UnlockUser(Username,
            function (result) {
                if (result == '') {
                    showSuccessModal('Unlocked user.', function () {
                        closeModal('modalUser', reload);
                    });
                }
                else {
                    showSpecificError(result, 'lblUserErrorMessage', 'divUserError');
                }
                loadingScreen(true, 0);
            },
            function (err) {
                showErrorModal(err.get_message());
                loadingScreen(true, 0);
            });
    });
}

function resetUser() {
    hideSpecificError('lblUserErrorMessage');

    showConfirmModal("reset password", function () {
        var postData = $('#modalUser').data('userData');
        postData.Action = 'reset';

        saveUser(postData);
    });
}

function approveUser() {
    hideSpecificError('lbl-e-t-m');

    showConfirmModal("approve", function () {
        loadingScreen(false, 0);

        var postData = $('#modalUserTemp').data('userTempData');

        PageMethods.ApproveUser(postData,
            function (result) {
                if (result == '') {
                    showSuccessModal('approved', function () {
                        closeModal('modalUserTemp', reload);
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

function rejectUser() {
    hideSpecificError('lbl-e-t-m');

    showConfirmModal("reject", function () {
        loadingScreen(false, 0);

        var postData = $('#modalUserTemp').data('userTempData');

        PageMethods.RejectUser(postData,
            function (result) {
                if (result == '') {
                    showSuccessModal('rejected', function () {
                        closeModal('modalUserTemp', reload);
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

function clearModalFields() {
    setTextboxValue('txtUserName', '');
    setDropdownValue('ddlRole', '');
    setTextboxValue('txt-f-n', '');
    setTextboxValue('txt-l-n', '');
    setTextboxValue('txt-m-i', '');
    setTextboxValue('txtIPAddress', '');
    setTextboxValue('txtEmail', '');
    setDropdownValue('ddlStatus', '');

    $('#modalUser').removeData('userData');
}

function EnableModalFields() {

    document.getElementById("txt-f-n").disabled = false;
    document.getElementById("txt-m-i").disabled = false;
    document.getElementById("txt-l-n").disabled = false;
    document.getElementById("txtIPAddress").disabled = false;
    document.getElementById("txtEmail").disabled = false;
    document.getElementById("txtBranch").disabled = false;
    document.getElementById("ddlRole").disabled = false;
    document.getElementById("ddlBranch").disabled = false;
    document.getElementById("ddlRegion").disabled = false;
    //document.getElementById("txtEcasaTarget").disabled = false;
    //document.getElementById("txtEcbgTarget").disabled = false;
    //document.getElementById("txtCasaTarget").disabled = false;
    //document.getElementById("txtCbgTarget").disabled = false;
    document.getElementById("ddlStatus").disabled = false;


}

async function reload() {
    loadingScreen(false, 0);

    var access = $('#acc-Main').data('access');

    try {
        if (access.CanModify) {

            var searchText = getTextboxValue('txtUser');
            var filterStatus = getDropdownValue('ddl-s-f');
            var role = getDropdownValue('ddl-r-f');
            var filterRole = role == 'All' ? '' : role;
            var region = getDropdownValue('ddlSearchRegion');
            var filterRegion = region == 'All' ? '' : region;
            var branch = getDropdownValue('ddlSearchBranch');
            var filterBranch = branch == 'All' ? '' : branch;

            
            var filterStatusDesc = getDropdownText('ddl-s-f');
            var filterRoleName = getDropdownText('ddl-r-f');
            var filterRegionName = getDropdownText('ddlSearchRegion');
            var filterBranchName = getDropdownText('ddlSearchBranch');

            var result = await getList(searchText, filterStatus, filterRole,filterRegion,filterBranch, filterStatusDesc, filterRoleName,filterRegionName,filterBranchName);

            createUserTable(result);
        }

        if (access.CanApprove) {
            var searchText = getTextboxValue('txtUserTemp');
            var filterStatus = getDropdownValue('ddl-s-t-f');

            var result = await getTempList(searchText, filterStatus);

            createUserTempTable(result);
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


function generateReport() {
    loadingScreen(false, 0);
    showDiv(['divReport'], false);
  
    PageMethods.GenerateUsersReport(
    function OnSuccess(result) {
        if (result == '') {
         showDiv(["divReport"], true);
            var iframe = $('#ifrmReportViewer');
            iframe.attr('src', '/Views/Reports/ReportViewer.aspx');
            //window.open('/Views/Reports/ReportViewer.aspx');
            loadingScreen(true, 3000);
            
        }
       
    },
    function OnError(err) {
        showErrorModal(err.get_message());
        loadingScreen(true, 0);
    });

}

