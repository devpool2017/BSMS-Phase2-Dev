$(document).ready(function () {
    loadingScreen(false, 0);

    var deferreds = {
        insert: $.Deferred(),
        update: $.Deferred(),
        approve: $.Deferred(),
        deactivate: $.Deferred(),
        activate: $.Deferred(),
    };

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

    PageMethods.HasAccess('CanApprove',
        function OnSuccess(result) {
            deferreds.approve.resolve(result);
        },
        function OnError(err) {
            deferreds.approve.reject(err.get_message());
        });

    PageMethods.HasAccess('CanDelete',
        function OnSuccess(result) {
            deferreds.deactivate.resolve(result);
        },
        function OnError(err) {
            deferreds.deactivate.reject(err.get_message());
        });

  
    $.when(deferreds.insert, deferreds.update, deferreds.approve, deferreds.deactivate).done(async function (canInsert, canUpdate, canApprove,canDelete) {
        try {
            var access = {
                CanModify: canInsert && canUpdate,
                CanApprove: canApprove,
                CanDelete: canDelete,
                CanActivate: false
            };

            $('#acc-Main').data('access', access);
            var a = $('#acc-Main').data;
            if (canInsert && canUpdate) {
                toggleEventForElement('#btnSearch', 'click', AJAXWrapPageMethodCall, true, 'searcLst');
                toggleEventForElement('#btnReset', 'click', function () {
                    setTextboxValue('txtRole', '');
                    searcLst();
                }, true);

                toggleEventForElement('#btnAdd', 'click', AJAXWrapPageMethodCall, true, 'addRole');

                var result = await getList('','');

                createRoleTable(result);
            }
            else {
                var element = document.getElementById('acc-App');
                element.parentNode.removeChild(element);
            }

            if (canApprove) {
                toggleEventForElement('#btnSearchTemp', 'click', AJAXWrapPageMethodCall, true, 'searcLstTemp');
                toggleEventForElement('#btnResetTemp', 'click', function () {
                    setTextboxValue('txtRoleTemp', '');
                    setDropdownDefaultValue('ddlFilterTemp', '');
                    searcLstTemp();
                }, true);

                var result = await getTempList('', '');

                createRoleTempTable(result);
            }
            else {
                var element = document.getElementById('acc-Temp');
                element.parentNode.removeChild(element);
            }
        } catch (e) {
            showErrorModal(e);
        }

        loadingScreen(true, 0);
    }).fail(function (e) {
        showErrorModal(e);
        loadingScreen(true, 0);
    });
});

async function searcLst() {
    loadingScreen(false, 0);

    try {
        var searchText = getTextboxValue('txtRole');
        var filterStat = getDropdownValue('ddlStat');
        var result = await getList(searchText,filterStat);

        createRoleTable(result);
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

async function searcLstTemp() {
    loadingScreen(false, 0);

    var searchText = getTextboxValue('txtRoleTemp');
   // var filterStat = getDropdownValue('ddlStat');
    var filterStatus = getDropdownValue('ddlFilterTemp');

    try {
        var result = await getTempList(searchText, filterStatus);

        createRoleTempTable(result);
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

function getList(searchText,filterStat) {
    var deferred = $.Deferred();
   
    PageMethods.GetList(searchText, filterStat,
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

function getTempList(searchText, filterStatus, filterStat) {
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

function createRoleTable(data) {
    var index = 1;
    var itemsPerPage = 10;
    var list = [];

    $('#tBodyRole').empty();
    for (var r = 0; r < data.length; r++) {
        list.push(data[r]);
    }

    secureStorage.setItem('list', JSON.stringify(list));
    allItems = data.length;

    createTable('divRole', 'tblRole', 'tBodyRole', list, index, itemsPerPage, allItems, 'list', 'Role');
}

function createRoleTempTable(data) {
    var index = 1;
    var itemsPerPage = 10;
    var list = [];

    $('#tBodyRoleTemp').empty();
    for (var r = 0; r < data.length; r++) {
        list.push(data[r]);
    }

    secureStorage.setItem('listTemp', JSON.stringify(list));
    allItems = data.length;

    createTable('divRoleTemp', 'tblRoleTemp', 'tBodyRoleTemp', list, index, itemsPerPage, allItems, 'listTemp', 'RoleTemp');
}

function addRole() {
    redirectToDetails(null, null, 1, 1);
}

function viewRole(roleId) {
    redirectToDetails(roleId, null, 2, 1);
}

function editRole(roleId) {
    redirectToDetails(roleId, null, 3, 1);
}

async function deactivateRole(roleId) {
    hideError();
    loadingScreen(false, 0);

    try {
        var result = await getDetails(roleId);

        if (parseInt(result.InEdit) === 0) {

            showConfirmModal('deactivate', function () {
                loadingScreen(false, 0);

                result.Action = 4;

                PageMethods.SaveTemp(result,
                    function OnSuccess(result) {
                        if (result == '') {
                            showSuccessModal('submited for approval', reload);
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
      
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

async function activateRole(roleId) {
    hideError();
    loadingScreen(false, 0);

    try {
        var result = await getDetails(roleId);

        if (parseInt(result.InEdit) === 0) {

            showConfirmModal('activate', function () {
                loadingScreen(false, 0);

                result.Action = 10;

                PageMethods.SaveTemp(result,
                    function OnSuccess(result) {
                        if (result == '') {
                            showSuccessModal('submited for approval', reload);
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

    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

function getDetails(roleId) {
    var deferred = $.Deferred();

    PageMethods.GetDetails(roleId,
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

function viewRoleTemp(roleId, tempId, tempStatus) {
    redirectToDetails(roleId, tempId, tempStatus, 2);
}

function redirectToDetails(roleId, tempId, action, mode) {
    loadingScreen(false, 0);

    PageMethods.RedirectToDetails(roleId, tempId, action, mode,
        function OnSuccess(result) {
            location.href = result;
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

async function reload() {
    loadingScreen(false, 0);

    var access = $('#acc-Main').data('access');

    try {
        if (access.CanModify) {
            var searchText = getTextboxValue('txtRole');
            var filterStat = getDropdownValue('ddlStat');
            var result = await getList(searchText,filterStat);

            createRoleTable(result);
        }

        if (access.CanApprove) {
            var searchText = getTextboxValue('txtRoleTemp');
            var filterStatus = getDropdownValue('ddlFilterTemp');

            var tempResult = await getTempList(searchText, filterStatus);

            createRoleTempTable(tempResult);
        }
    } catch (e) {
        showErrorModal(e);
    }

    loadingScreen(true, 0);
}