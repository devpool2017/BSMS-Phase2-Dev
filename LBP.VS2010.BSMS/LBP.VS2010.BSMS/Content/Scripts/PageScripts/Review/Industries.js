$(document).ready(function () {
    sessionStorage.BranchCode = "";
    sessionStorage.RegionCode = "";
    loadingScreen(false, 0);

    toggleEventForElement("#btnModalCancel", 'click', clearModalFields, true);

    var deferred = $.Deferred();

    PageMethods.OnLoadData(
        function OnSuccess(result) {
            deferred.resolve(result);
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    $.when(deferred).done(async function (data) {
        var industryTypeList = data.IndustryTypeList;
        var canInsert = data.CanInsert;
        var canUpdate = data.CanUpdate;
        var canApprove = data.CanApprove;
        var canDelete = data.CanDelete;
        var GH = data.GH;
        var BH = data.BH;
        var TA = data.TA;
        sessionStorage.GHId = GH;
        sessionStorage.BHId = BH;
        sessionStorage.TAId = TA;
        secureStorage.setItem('IndustryTypeList', data.IndustryTypeList);

        try {
            var access = {
                CanModify: canInsert && canUpdate,
                CanApprove: canApprove,
                CanDelete: canDelete
            };

            $('#acc-Main').data('access', access);

                toggleEventForElement('#btnSearch', 'click', AJAXWrapPageMethodCall, true, 'searcLst');
                toggleEventForElement('#btnDownload', 'click', AJAXWrapPageMethodCall, true, 'generateReport')

                toggleEventForElement('#btnReset', 'click', function () {
                    setTextboxValue('txtIndustry', '');
                    setDropdownDefaultValue('ddlStatusFilter', '');
                    searcLst();
                }, true);

                toggleEventForElement('#btnAdd', 'click', AJAXWrapPageMethodCall, true, 'addIndustry');

                var result = await getList('', '');

                createIndustyTable(result);

            if (canApprove) {

                toggleEventForElement('#btnApprove', 'click', AJAXWrapPageMethodCall, true, 'approveIndustry');
                toggleEventForElement('#btnReject', 'click', AJAXWrapPageMethodCall, true, 'rejectIndustry');

            }
//            else {
//                var element = document.getElementById('acc-UsersTemp');
//                element.parentNode.removeChild(element);
//            }

            var modalIndustry = document.getElementById('modalIndustry');

            modalIndustry.addEventListener('hide.bs.modal', function () {
                toggleEventForElement('#btnSave', 'click', AJAXWrapPageMethodCall, false, 'validateUser');
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

function getList(searchText, filterStatus) {
    var deferred = $.Deferred();

    PageMethods.GetList(searchText, filterStatus,
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

function createIndustyTable(result) {
    var index = 1;
    var itemsPerPage = 10;
    var list = [];

    $('#tBodyIndustries').empty();
    for (var r = 0; r < result.length; r++) {
        list.push(result[r]);
    }

    secureStorage.setItem('list', JSON.stringify(list));
    allItems = result.length;

    createTable('divIndustries', 'tblIndustries', 'tBodyIndustries', list, index, itemsPerPage, allItems, 'list', 'Industry');
}

async function searcLst() {
    loadingScreen(false, 0);

    try {
        var searchText = getTextboxValue('txtIndustry');
        var filterStatus = getDropdownValue('ddlStatusFilter');
        var result = await getList(searchText, filterStatus);

        createIndustyTable(result);
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

function addIndustry() {
    loadIndustry(null, null, null, null,'add');
}

function viewIndustry(industryCode, inEdit, status, tempStatusID) {
    loadIndustry(industryCode, inEdit, status, tempStatusID, 'view');
}

function editIndustry(industryCode, inEdit, status, tempStatusID) {
    loadIndustry(industryCode, inEdit, status, tempStatusID, 'edit');
}


async function loadIndustry(industryCode, inEdit, status, tempStatusID, action) {
    hideError();
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);

    var industryTypeList = secureStorage.getItem("IndustryTypeList");

    try {
        var result = new Object();

        if(status === "Active" || status === "Disabled"){
            result = await getDetails(industryCode, action)
        }else{
            result.IndustryCode = industryCode;
            result.InEdit = inEdit;
            result.Status = status;
            result.TempStatusID = tempStatusID;
        }

        var mdlHdr = 'Add New Industry';

        if(inEdit){
            var inEdit = result.InEdit;
        }
        

        switch (action) {
            case 'view':
                mdlHdr = 'View Industry - ' + result.IndustryCode;
                break;

            case 'edit':
                mdlHdr = 'Edit Industry - ' + result.IndustryCode;
                break;

            default:
                mdlHdr = 'Add New Industry';
        }

        setLabelText('span-u-a', mdlHdr);
        setDropdownDefaultValue('ddlStatus', result.Status);
        setTextboxValue('txtIndustryCode', result.IndustryCode);
        setTextboxValue('txtIndustryDesc', result.IndustryDesc);
        loadDropdown('ddlIndustryType', industryTypeList, true, result.IndustryType);
        secureStorage.setItem('_IndustryCode', result.IndustryCode);
        disableControls(['txtIndustryCode','txtIndustryDesc', 'ddlIndustryType', 'ddlStatus'], (action == 'view'));
        showDiv(['btnSave'], !inEdit && !(action == 'view'));
        showDiv(['divEditLabel'], inEdit && action == 'edit');

        $('#modalIndustry').data('industryData', result);
        toggleEventForElement('#btnSave', 'click', AJAXWrapPageMethodCall, !inEdit && !(action == 'view'), 'validateIndustry');

        if(((inEdit) && (!(status == "Active" || status == "Disabled"))) ||(status == "For Creation")){
        viewIndustryTemp(result.IndustryCode, result.TempStatusID);
        $('#modalIndustryTemp').modal('show');
        }else{
        
        $('#modalIndustry').modal('show');
        }
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

async function deleteIndustry(industryCode) {
    hideError();
    loadingScreen(false, 0);

    try {
        var result = await getDetails(industryCode, null)

        if (!result.InEdit && !result.InUse) {

            result.Action = 'delete';

            showConfirmModal("delete", function () {
                loadingScreen(false, 0);

                PageMethods.SaveIndustry(result,
                    function (result) {
                        if (result == '') {
                            showSuccessModal('submited for approval', function () {
                                closeModal('modalIndustry', reload);
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
            showError('Industry is in use/for approval, it cannot be deleted.');
        }
    } catch (e) {
        showError(e);
    }

    loadingScreen(true, 0);
}

function getDetails(IndustryCode, action) {
    var deferred = $.Deferred();

    PageMethods.GetDetails(IndustryCode, action,
        function OnSuccess(result) {
            deferred.resolve(result);
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    return deferred.promise();
}

function viewIndustryTemp(industryCode, tempStatusID) {
    hideError();
    loadingScreen(false, 0);

    PageMethods.GetMergeDetails(industryCode,
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

                createTable('divApproval', 'tblApproval', 'tBodyApproval', list, index, itemsPerPage, allItems, 'listApproval', 'Industry');

                var header = 'For Approval - ';
                if (tempStatusID == '6') header += 'New';
                if (tempStatusID == '5') header += 'Update';
                if (tempStatusID == '7') header += 'Deletion';

                header += ' (' + industryCode + ')';

                setLabelText('span-u-t-a', header);
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

function validateIndustry() {
    hideSpecificError('lblUserErrorMessage');
    loadingScreen(false, 0);

    var industryData = $('#modalIndustry').data('industryData');
    var industryCode = getTextboxValue('txtIndustryCode');
    var industryDesc = getTextboxValue('txtIndustryDesc');
    var industryType = getDropdownValue('ddlIndustryType');
    var status = getDropdownValue('ddlStatus');

    var postData = {
        Action: industryData.Action,
        IndustryCode: industryData.Action == 'edit' ? industryData.IndustryCode : industryCode,
        IndustryDesc: industryDesc,
        IndustryType: industryType,
        Status: status
    };

    if (industryData.Action == 'edit') {
        var bool = true;
        for (const key in postData) {
            if (industryData.hasOwnProperty(key)) {
                if (postData[key].trim() !== industryData[key].trim()) {
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

    PageMethods.ValidateIndustry(postData,
        function (result) {
            if (result == '') {
                showConfirmModal("save", saveIndustry, null, postData);
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

function saveIndustry(obj) {
    loadingScreen(false, 0);

    PageMethods.SaveIndustry(obj,
        function (result) {
            if (result == '') {
                showSuccessModal('submitted for approval', function () {
                    closeModal('modalIndustry', reload);
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

function approveIndustry() {
    hideSpecificError('lbl-e-t-m');

    showConfirmModal("approve", function () {
        loadingScreen(false, 0);

        var postData = $('#modalIndustry').data('industryData');

        PageMethods.ApproveIndustry(postData,
            function (result) {
                if (result == '') {
                    showSuccessModal('approved', function () {
                        closeModal('modalIndustry', reload);
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

function rejectIndustry() {
    hideSpecificError('lbl-e-t-m');

    showConfirmModal("reject", function () {
        loadingScreen(false, 0);

        var postData = $('#modalIndustry').data('industryData');

        PageMethods.RejectIndustry(postData,
            function (result) {
                if (result == '') {
                    showSuccessModal('rejected', function () {
                        closeModal('modalIndustry', reload);
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
    setTextboxValue('txtIndustryCode', '');
    setTextboxValue('txtIndustryDesc', '');
    setDropdownValue('ddlIndustryType', '');
    setDropdownValue('ddlStatus', '');
    $('#modalIndustry').removeData('industryData');
}

function EnableModalFields() {
    document.getElementById("txtIndustryDesc").disabled = false;
    document.getElementById("ddlIndustryType").disabled = false;
    document.getElementById("ddlStatus").disabled = false;
}

async function reload() {
    loadingScreen(false, 0);

    closeModal('modalIndustry');
    closeModal('modalIndustryTemp');

    var access = $('#acc-Main').data('access');

    try {
            var searchText = getTextboxValue('txtIndustry') == '' ? null : getTextboxValue('txtIndustry');
            var filterStatus = getDropdownValue('ddlStatusFilter') == '' ? null : getDropdownValue('ddlStatusFilter');
            var result = await getList(searchText, filterStatus);
            createIndustyTable(result);
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

    PageMethods.GenerateIndustryReport(
        function OnSuccess(result) {
            if (result == '') {
                showDiv(["divReport"], true);
                var iframe = $('#ifrmReportViewer');
                iframe.attr('src', '/Views/Reports/ReportViewer.aspx');
                loadingScreen(true, 3000);
            }

        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });

}

