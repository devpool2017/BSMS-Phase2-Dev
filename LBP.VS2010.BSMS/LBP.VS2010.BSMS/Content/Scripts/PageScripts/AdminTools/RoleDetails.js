$(document).ready(async function () {
    loadingScreen(false, 0);

    toggleEventForElement('#btnBack', 'click', AJAXWrapPageMethodCall, true, 'redirectToRole');

    try {
        var result = await getDetails(null);

        if (result) {
            createMatrixTable(result);

            var action = parseInt(result.Action);
            var canInsert = result.Access.CanInsert == 'Y';
            var canUpdate = result.Access.CanUpdate == 'Y';
            var canApprove = result.Access.CanApprove == 'Y';
            var inEdit = result.InEdit ? parseInt(result.InEdit) : 0;

            var canSave = ((action === 1 && canInsert) || (action === 3 && canUpdate)) && inEdit === 0;

            showDiv(['btnApprove', 'btnReject'], (action === 5 || action === 6 || action === 3 || action === 1) && canApprove);
            showDiv(['btnViewOrig'], action === 5 && canApprove);
            showDiv(['btnSave'], canSave);

            toggleEventForElement('#btnApprove', 'click', AJAXWrapPageMethodCall, (action === 5 || action === 6 || action === 3 || action === 1) && canApprove, 'approveRole');
            toggleEventForElement('#btnReject', 'click', AJAXWrapPageMethodCall, (action === 5 || action === 6 || action === 3 || action === 1) && canApprove, 'rejectRole');
            toggleEventForElement('#btnViewOrig', 'click', AJAXWrapPageMethodCall, action === 5 && canApprove, 'viewOrig');
            toggleEventForElement('#btnViewTemp', 'click', AJAXWrapPageMethodCall, action === 5 && canApprove, 'viewTemp');
            toggleEventForElement('#btnSave', 'click', AJAXWrapPageMethodCall, canSave, 'validateRole');

            disableControls(['txtRoleName'], !(action === 1));
            disableControls(['txtRoleDescription'], !(action === 1 || action === 3) || inEdit === 1);
            showDiv(['divEditLabel'], action === 3 && inEdit === 1);

            $('#tblAccess').data({ RoleId: result.RoleId, RoleTempId: result.RoleTempId, Action: result.Action });
        }
    } catch (e) {
        showErrorModal(e);
    }

    loadingScreen(true, 0);
});

function getDetails(mode) {
    var deferred = $.Deferred();

    PageMethods.GetDetails(mode,
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

function createMatrixTable(result) {
    setTextboxValue('txtRoleName', result.RoleName);
    setTextboxValue('txtRoleDescription', result.RoleDescription);

    const tableBody = document.querySelector('#tblAccess tbody');
    tableBody.innerHTML = '';

    var lstHeader;

    result.ProfileList.forEach(item => {
        const mainMenuHeader = generateTableRow(item, true, false, lstHeader);
        tableBody.appendChild(mainMenuHeader);

        const mainMenuRow = generateTableRow(item, false, false);
        tableBody.appendChild(mainMenuRow);

        if (item.TabProfile && item.TabProfile.length > 0) {
            item.TabProfile.forEach(tabMenu => {
                var tabMenuRow = generateTableRow(tabMenu, false, true);
                tableBody.appendChild(tabMenuRow);
            });
        }

        lstHeader = item.MainMenuID;
    });

    var action = parseInt(result.Action);
    var inEdit = parseInt(result.InEdit);
    var checkboxes = document.querySelectorAll("#tblAccess tbody input[type='checkbox']");

    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].disabled = !(action === 1 || action === 3) || inEdit === 1;
    }
}

function generateTableRow(data, isHeader, isTab, lastMenu) {
    var row = document.createElement('tr');
    var menuCell = document.createElement('td');

    row.style.backgroundColor = '#fff';

    if (isTab) {
        menuCell.colSpan = '2';
        menuCell.style.paddingLeft = '40px';
        row.style.backgroundColor = '#EFE9E5';
    }

    var access = ['CanView', 'CanInsert', 'CanUpdate', 'CanDelete', 'CanPrint', 'CanApprove'];
    var accessHeader = ['View', 'Insert', 'Update', 'Delete', 'Print', 'Approve'];

    if (isHeader) {
        if (data.MainMenuID != lastMenu) {
            row.style.backgroundColor = '#666666';
            row.style.color = '#fff';
            row.style.fontWeight = 'bold';
            row.style.height = '30px';

            menuCell.textContent = data.MainMenuName;
            menuCell.vAlign = 'middle';
            menuCell.align = 'center';
            row.appendChild(menuCell);

            accessHeader.forEach(header => {
                const cell = document.createElement('td');
                cell.textContent = header;
                cell.vAlign = 'middle';
                cell.align = 'center';
                cell.width = '10%';

                row.appendChild(cell);
            });
        }
    }
    else {
        menuCell.textContent = data.TabName || data.SubMenuName;

        row.appendChild(menuCell);

        access.forEach(permission => {
            var chkId = !isTab ? data.SubMenuID : data.SubMenuID + '_' + data.TabID;
            var chkClass = (!isTab ? 'check-submenu-' : 'check-tab-') + data.SubMenuID;

            if (!(isTab && permission === 'CanView')) {
                var cell = document.createElement('td');
                var checkbox = document.createElement('input');
                checkbox.type = 'checkbox';
                checkbox.name = permission + '_' + chkId;
                checkbox.checked = data[permission] === 'Y';
                checkbox.classList.add(chkClass);
                checkbox.setAttribute('aria-label', permission);

                if (isTab) {
                    var chkView = document.getElementsByName('CanView_' + data.SubMenuID)[0];
                    checkbox.addEventListener('click', function (event) {
                        if (event.target.checked) {
                            chkView.checked = true;
                        }
                    });

                    chkView.addEventListener('click', function (event) {
                        if (!event.target.checked) {
                            var chkbox = document.getElementsByClassName('check-tab-' + data.SubMenuID);

                            for (var i = 0; i < chkbox.length; i++) {
                                chkbox[i].checked = false;
                            }
                        }
                    });
                }

                cell.width = '10%';
                cell.appendChild(checkbox);
                row.appendChild(cell);
            }
        });
    }

    return row;
}

function approveRole() {
    hideError();

    var data = $('#tblAccess').data();

    var postData = {
        RoleId: data.RoleId,
        RoleTempId: data.RoleTempId,
        TempStatus: data.Action
    };

    showConfirmModal('approve', function () {
        loadingScreen(false, 0);

        PageMethods.Approve(postData,
            function OnSuccess(result) {
                if (result == '') {
                    showSuccessModal('approved', redirectToRole);
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

function rejectRole() {
    hideError();

    showConfirmModal('reject', function () {
        loadingScreen(false, 0);

        var data = $('#tblAccess').data();

        PageMethods.Reject(data.RoleTempId,
            function OnSuccess(result) {
                if (result == '') {
                    showSuccessModal('rejected', redirectToRole);
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

async function viewOrig() {
    hideError();
    loadingScreen(false, 0);

    try {
        var result = await getDetails(1);

        if (result) {
            createMatrixTable(result);
        }

        showDiv(['btnViewTemp'], true);
        showDiv(['btnViewOrig'], false);
    } catch (e) {
        showErrorModal(e);
    }

    loadingScreen(true, 0);
}

async function viewTemp() {
    hideError();
    loadingScreen(false, 0);

    try {
        var result = await getDetails(2);

        if (result) {
            createMatrixTable(result);
        }

        showDiv(['btnViewTemp'], false);
        showDiv(['btnViewOrig'], true);
    } catch (e) {
        showErrorModal(e);
    }

    loadingScreen(true, 0);
}

function validateRole() {
    hideError();
    loadingScreen(false, 0);

    var postData = createRole();

    PageMethods.ValidateRole(postData,
        function OnSuccess(result) {
            if (result == '') {
                showConfirmModal('save', saveRole, null, postData);
            } else {
                showError(result);
            }

            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}


function saveRole(postData) {
    loadingScreen(false, 0);

    PageMethods.SaveTemp(postData,
        function OnSuccess(result) {
            if (result == '') {
                showSuccessModal('submited for approval', redirectToRole);
            } else {
                showError(result);
            }

            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

function redirectToRole() {
    loadingScreen(false, 0);

    PageMethods.RedirectToHome(
        function OnSuccess(result) {
            location.href = result;
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

function createRole() {
    var data = $('#tblAccess').data();

    roleId = data.RoleId == null ? '' : data.RoleId;
    //roleId = data.RoleId;

    var postData = {
        RoleId: roleId,
        RoleName: getTextboxValue('txtRoleName'),
        RoleDescription: getTextboxValue('txtRoleDescription'),
        ProfileList: createMatrix(roleId),
        Action: data.Action
    };

    return postData;
}

function createMatrix(roleId) {
    var table = document.getElementById('tblAccess');
    var data = [];

    function parseCheckbox(checkbox) {
        return checkbox.checked ? 'Y' : 'N';
    }

    for (let i = 1; i < table.rows.length; i++) {
        var row = table.rows[i];
        var checkboxes = Array.from(row.querySelectorAll('input[type="checkbox"]'));

        if (checkboxes.length === 6) {
            var subMenuName = row.cells[0].textContent.trim();
            let [canView, canInsert, canUpdate, canDelete, canPrint, canApprove] = checkboxes.map(parseCheckbox);
            var subMenuId = checkboxes[0].name.split('_')[1];
            var arrTab = [];

            if (table.rows[i + 1] && table.rows[i + 1].cells.length === 6) {
                do {
                    i++;

                    var tabRow = table.rows[i];
                    var tabCheckboxes = Array.from(tabRow.querySelectorAll('input[type="checkbox"]'));
                    var tabId = tabCheckboxes[0].name.split('_')[2];

                    if (tabCheckboxes.length > 0) {
                        var tabName = tabRow.cells[0].textContent.trim();
                        let [canInsert, canUpdate, canDelete, canPrint, canApprove] = tabCheckboxes.map(parseCheckbox);

                        var tabData = {
                            TabID: tabId,
                            RoleID: roleId,
                            TabName: tabName,
                            SubMenuID: subMenuId,
                            CanInsert: canInsert,
                            CanUpdate: canUpdate,
                            CanDelete: canDelete,
                            CanPrint: canPrint,
                            CanApprove: canApprove,
                        };

                        arrTab.push(tabData);
                    }
                } while (table.rows[i + 1] && table.rows[i + 1].cells.length === 6);
            }

            var rowData = {
                RoleID: roleId,
                SubMenuID: subMenuId,
                SubMenuName: subMenuName,
                CanView: canView,
                CanInsert: canInsert,
                CanUpdate: canUpdate,
                CanDelete: canDelete,
                CanPrint: canPrint,
                CanApprove: canApprove,
                TabProfile: arrTab,
            };

            data.push(rowData);
        }
    }

    return data;
}