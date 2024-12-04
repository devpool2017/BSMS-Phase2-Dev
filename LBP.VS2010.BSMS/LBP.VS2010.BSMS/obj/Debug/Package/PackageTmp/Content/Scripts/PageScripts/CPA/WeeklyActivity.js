$(async function () {
    hideError();
    loadingScreen(false, 0);

    var deferred = $.Deferred();
    PageMethods.OnLoadData(
        function OnSuccess(result) {
            deferred.resolve(result);
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    $.when(deferred).done(async function (data) {
        try {
            var GroupList = data.GroupList;
            var UsersList = data.UsersList;
            var BranchesList = data.BranchesList;
            var YearList = data.YearList;
            var currentUser = data.currentUser;
            var isGH = (data.GroupCode).includes(currentUser.RoleID);
            var PostData = JSON.parse(secureStorage.getItem('ClientDetails'));

            if (GroupList.length == 0) GroupList.splice(0, 0, { description: '', value: '' });
            if (BranchesList.length == 0) BranchesList.splice(0, 0, { description: '', value: '' });
            if (UsersList.length == 0) UsersList.splice(0, 0, { description: '', value: '' });
            if (YearList.length == 0) YearList.splice(0, 0, { description: '', value: '' });
            
            loadDropdown('ddl-group', GroupList, !isGH, (isGH ? currentUser.GroupCode : GroupList[0].value));
            loadDropdown('ddl-BranchHead', UsersList, false, (isGH ? currentUser.Username : ''));
            loadDropdown('ddl-branch', BranchesList, true, BranchesList[0].value);
            loadDropdown('ddl-year', YearList, YearList.length > 0, YearList[0].value);

            if ($('#ddl-BranchHead').hasClass('es-input')) $('#ddl-BranchHead').attr("placeholder", " ");
            if ($('#ddl-year').hasClass('es-input')) $('#ddl-year').attr("placeholder", " ");

            toggleEventForElement('#ddl-group', 'change', async function (event) {
                loadingScreen(false, 0);
                hideError();
                setLabelText("lblReportHeader", "");
                var ddlGroup = document.getElementById("ddl-group");
                ddlGroup = (ddlGroup && ddlGroup.value !== '') ? (typeof getDropdownValue("ddl-group") !== 'undefined' ? getDropdownValue("ddl-group") : "") : "";
                var branchDeferred = $.Deferred();
                PageMethods.GetBranchesList(ddlGroup,
                    function OnSuccess(result) {
                        branchDeferred.resolve(result);
                    },
                    function OnError(err) {
                        branchDeferred.reject(err.get_message());
                    });

                $.when(branchDeferred).done(async function (data) {
                    try {
                        if (data.length == 0) data.splice(0, 0, { description: '', value: '' });
                        loadDropdown('ddl-branch', data, true, data[0].value);
                    } catch (e) {
                        showErrorModal(e);
                    }

                    loadingScreen(true, 0);
                })
                    .fail(function (e) {
                        showErrorModal(e);
                        loadingScreen(true, 0);
                    });

                loadingScreen(true, 0);
            }, true);

            toggleEventForElement('#ddl-branch', 'change', async function (event) {
                loadingScreen(false, 0);
                hideError();
                setLabelText("lblReportHeader", "");

                var ddlGroup = document.getElementById("ddl-group");
                ddlGroup = (ddlGroup && ddlGroup.value !== '') ? (typeof getDropdownValue("ddl-group") !== 'undefined' ? getDropdownValue("ddl-group") : "") : "";
                var ddlBranch = document.getElementById("ddl-branch");
                ddlBranch = (ddlBranch && ddlBranch.value !== '') ? (typeof getDropdownValue("ddl-branch") !== 'undefined' ? getDropdownValue("ddl-branch") : "") : "";
                
                var userDeferred = $.Deferred();
                PageMethods.GetUsersList(ddlGroup, ddlBranch,
                    function OnSuccess(result) {
                        userDeferred.resolve(result);
                    },
                    function OnError(err) {
                        userDeferred.reject(err.get_message());
                    });

                $.when(userDeferred).done(async function (data) {
                    try {
                        if (data.length == 0) data.splice(0, 0, { description: '', value: '' });
                        loadDropdown('ddl-BranchHead', data, false, data[0].value);
                    } catch (e) {
                        showErrorModal(e);
                    }

                    loadingScreen(true, 0);
                })
                    .fail(function (e) {
                        showErrorModal(e);
                        loadingScreen(true, 0);
                    });
            }, true);

            if (PostData != null) {
                setDropdownDefaultValue("ddl-year", PostData.YearNumber);
                setDropdownDefaultValue("ddl-branch", PostData.BranchCode);
                setDropdownDefaultValue("ddl-BranchHead", PostData.BranchHeadName);
            }

            toggleEventForElement('#btnReset', 'click', function () {
                hideError();
                loadingScreen(false, 0);
                setDropdownDefaultValue('ddl-group', (isGH ? currentUser.GroupCode : GroupList[0].value));
                setDropdownDefaultValue('ddl-BranchHead', '');
                setDropdownDefaultValue('ddl-year', YearList[0].value);

                var BranchesList = { description: '', value: '' };
                loadDropdown('ddl-branch', BranchesList, false, '');
                setLabelText("lblReportHeader", "");
                showDiv(['divTblWeeklyActivity'], false);
                loadingScreen(true, 0);
            }, true);

            toggleEventForElement('#btnSearch', 'click', AJAXWrapPageMethodCall, true, 'ValidateSearch');

        } catch (e) {
            showErrorModal(e);
        }
        loadingScreen(true, 0);

    }).fail(function (e) {
        showErrorModal(e);
        loadingScreen(true, 0);
    });
});

async function ValidateSearch() {
    hideError();
    loadingScreen(false, 0);
    var obj = await getFiltersObj();
    PageMethods.ValidateSearchFilter(obj,
        function OnSuccess(result) {
            if (result == '') {
                SearchWeeklyActivity(obj);
            }
            else {
                showDiv(['divTblWeeklyActivity'], false);
                showError(result);
                loadingScreen(true, 0);
            }
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

async function SearchWeeklyActivity(postData) {
    loadingScreen(false, 0);
    hideError();
    try {
        setLabelText("lblReportHeader", (postData.BranchHeadName != '' ? "Weekly Activity : Summary Count of  Branch Head " + postData.UploadedBy : ''));
        var data = await GetWeeklyActivity(postData);
        createWeeklyActivityTable(data);
        loadingScreen(true, 0);
    }
    catch (e) {
        showError(e);
        loadingScreen(true, 0);
    }
}

function createWeeklyActivityTable(data) {
    var index = 1;
    var itemsPerPage = 100;
    var list = [];
    $('#tBody-WeeklyActivity').empty();
    for (var r = 0; r < data.length - 1; r++) {
        list.push(data[r]);
    }
    secureStorage.setItem('listWeeklyActivity', JSON.stringify(list));
    allItems = data.length;

    setLabelText("lblTotalLead", data.length > 0 ? data[data.length - 1].Lead : '');
    setLabelText("lblTotalRevisit", data.length > 0 ? data[data.length - 1].Revisit : '');
    setLabelText("lblGrandTotal", data.length > 0 ? data[data.length - 1].Total : '');
    showDiv(['divTblWeeklyActivity'], true);
    createTable('div-WeeklyActivity', 'tbl-WeeklyActivity', 'tBody-WeeklyActivity', list, index, itemsPerPage, allItems, 'listWeeklyActivity', 'ListWeeklyActivity');
}

function GetWeeklyActivity(obj) {
    var deferred = $.Deferred();
    PageMethods.GetWeeklyActivity(obj,
        function OnSuccess(result) {
            if (result != null) {
                deferred.resolve(result);
            }
            else {
                deferred.reject($("[id$=hdnError]").val());
            }
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    return deferred.promise();
}

async function getFiltersObj() {
    var ddlBranch = document.getElementById("ddl-branch");
    var ddlBranchHead = document.getElementById("ddl-BranchHead");

    var postData = {
        Branch: (ddlBranch && ddlBranch.value !== '') ? (typeof getDropdownText("ddl-branch") !== 'undefined') ? getDropdownText("ddl-branch") : "" : "",
        BranchCode: (ddlBranch && ddlBranch.value !== '') ? (typeof getDropdownValue("ddl-branch") !== 'undefined') ? getDropdownValue("ddl-branch") : "" : "",
        YearNumber: (typeof getDropdownValue("ddl-year") !== 'undefined') ? getDropdownValue("ddl-year") : "",
        BranchHeadName: (ddlBranchHead && ddlBranchHead.value !== '') ? (typeof getDropdownValue("ddl-BranchHead") !== 'undefined') ? getDropdownValue("ddl-BranchHead") : "" : "",
        UploadedBy: (ddlBranchHead && ddlBranchHead.value !== '') ? (typeof getDropdownText("ddl-BranchHead") !== 'undefined') ? getDropdownText("ddl-BranchHead") : "" : "",
        GroupCode: (typeof getDropdownValue("ddl-group") !== 'undefined') ? getDropdownValue("ddl-group") : "",
        ViewPage: "TableView"
    };
    
    return postData;
}

 async function viewListWeeklyActivity(month, week) {
     var postData = await getFiltersObj();
     postData.MonthCode = month;
     postData.WeekNumber = week;

     var deferred = $.Deferred();
     PageMethods.ClientDetails(postData,
         function OnSuccess(result) {
            deferred.resolve();
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

     $.when(deferred).done(async function () {
         try {
             window.location = "/Views/PotentialAccount/WeeklyActivityView.aspx";
         }
         catch (e) {
             showErrorModal(e);
         }

         loadingScreen(true, 0);
    })
        .fail(function (e) {
            showErrorModal(e);
            loadingScreen(true, 0);
        });
}
