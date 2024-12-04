var WeekList;
$(async function () {
    hideError();
    loadingScreen(false, 0);
//        $('#divChangesInPotentialAccounts').hide();
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
            var RegionList = data.RegionList;
//            var UsersList = data.UsersList;
            var BranchesList = data.BranchesList;
            var YearList = data.YearList;
            var MonthList = data.MonthList;
            var currentUser = data.currentUser;
            WeekList = data.WeekList;
            //var isAdmn = ['3', '4'].includes(currentUser.RoleID);
            var isSH = data.SectorCode.includes(currentUser.RoleID);
            var isGH = data.GroupCode.includes(currentUser.RoleID);

            if (RegionList.length > 0) RegionList.splice(0, 0, { description: '', value: '' });
//            if (UsersList.length > 0) UsersList.splice(0, 0, { description: '', value: '' });
            if (BranchesList.length > 0) BranchesList.splice(0, 0, { description: '', value: '' });
            if (YearList.length > 0) YearList.splice(0, 0, { description: '', value: '' });
            if (MonthList.length > 0) MonthList.splice(0, 0, { description: 'All', value: 'All' });
            if (WeekList.length > 0) WeekList.splice(0, 0, { description: 'All', value: 'All' });

            loadDropdown('ddl-region', RegionList, (isGH ? false : true), isGH ? currentUser.GroupCode : RegionList[1].value);
//            loadDropdown('ddl-userFullName', UsersList, false);
            loadDropdown('ddl-branch', BranchesList, true, BranchesList[1].value);
            loadDropdown('ddl-year', YearList, YearList.length > 0, new Date().getFullYear());
            loadDropdown('ddl-Month', MonthList, MonthList.length > 0);
            loadDropdown('ddl-week', WeekList, WeekList.length > 0);
            loadMonthDropdown();

//            if ($('#ddl-userFullName').hasClass('es-input')) $('#ddl-userFullName').attr("placeholder", " ");
            if ($('#ddl-branch').hasClass('es-input')) $('#ddl-branch').attr("placeholder", " ");
            if ($('#ddl-year').hasClass('es-input')) $('#ddl-year').attr("placeholder", " ");
            if ($('#ddl-Month').hasClass('es-input')) $('#ddl-Month').attr("placeholder", " ");
            if ($('#ddl-week').hasClass('es-input')) $('#ddl-week').attr("placeholder", " ");


            toggleEventForElement('#ddl-region', 'change', async function (event) {
                loadingScreen(false, 0);
                hideError();
                showDiv(['tblOthers', 'divChangesInPotentialAccounts'], false);
                region = getDropdownValue('ddl-region') || '';
                var branchDeferred = $.Deferred();
                PageMethods.GetBranchesList(region,
                    function OnSuccess(result) {
                        branchDeferred.resolve(result);
                    },
                    function OnError(err) {
                        branchDeferred.reject(err.get_message());
                    });

                $.when(branchDeferred).done(async function (data) {
                    try {
                        if (data.length > 0) data.splice(0, 0, { description: '', value: '' });
                        loadDropdown('ddl-branch', data, data.length > 0);
                        setDropdownDefaultValue('ddl-branch', (data[1].value));
                    } catch (e) {
                        showErrorModal(e);
                    }

                    loadingScreen(true, 0);
                })
                    .fail(function (e) {
                        showErrorModal(e);
                        loadingScreen(true, 0);
                    });
            }, isSH);

            toggleEventForElement('#ddl-branch', 'change', async function (event) {
             showDiv(['tblOthers', 'divChangesInPotentialAccounts'], false);
//                loadingScreen(false, 0);
//                hideError();
//                region = getDropdownValue('ddl-region') || '';
//                branch = getDropdownValue('ddl-branch') || '';
//                showDiv(['tblOthers', 'divChangesInPotentialAccounts'], false);
//                var branchDeferred = $.Deferred();
//                PageMethods.GetUsersList(region, branch,
//                    function OnSuccess(result) {
//                        branchDeferred.resolve(result);
//                    },
//                    function OnError(err) {
//                        branchDeferred.reject(err.get_message());
//                    });

//                $.when(branchDeferred).done(async function (data) {
//                    try {
//                        setDropdownDefaultValue('ddl-userFullName', '');
//                        if (data.length > 0) data.splice(0, 0, { description: '', value: '' });
//                        if (data.length > 0) {
//                            loadDropdown('ddl-userFullName', data, false);
//                            setDropdownDefaultValue('ddl-userFullName', (branch != '' ? data[1].value : ''));
//                        }
//                    } catch (e) {
//                        showErrorModal(e);
//                    }

//                    loadingScreen(true, 0);
//                })
//                    .fail(function (e) {
//                        showErrorModal(e);
//                        loadingScreen(true, 0);
//                    });
            }, true);

                if (getDropdownValue('ddl-Month') !== '') {
                    toggleEventForElement('#ddl-year', 'change', AJAXWrapPageMethodCall, true, 'loadWeekList');
                }

            toggleEventForElement('#ddl-Month', 'change', function () {
            loadWeekList();
                if (getDropdownValue('ddl-Month') == 'All') {
                    setDropdownDefaultValue('ddl-week', 'All');
                    enableControl('ddl-week', false);
                }
                else {
                    setDropdownDefaultValue('ddl-week', '');
                    enableControl('ddl-week', true);
                }
            }, true);
            
            toggleEventForElement('#ddl-week', 'change', function () {
           showDiv(['tblOthers', 'divChangesInPotentialAccounts'], false);
            }, true);

            toggleEventForElement('#btn-ResetFilters', 'click', function () {
//                setDropdownDefaultValue('ddl-userFullName', '');
                setDropdownDefaultValue('ddl-region', isGH ? currentUser.GroupCode : RegionList[1].value);
                setDropdownDefaultValue('ddl-year', '');
                setDropdownDefaultValue('ddl-Month', null);
                setDropdownDefaultValue('ddl-week', '');
                onLoadTables();
            }, true);

            toggleEventForElement('#btn-ViewTables', 'click', AJAXWrapPageMethodCall, true, 'validateFilters');
            toggleEventForElement('#btn-Print', 'click', AJAXWrapPageMethodCall, true, 'generateReport');

            await onLoadTables();
        } catch (e) {
            showErrorModal(e);
        }

        loadingScreen(true, 0);
    }).fail(function (e) {
        showErrorModal(e);
        loadingScreen(true, 0);
    });
});

function loadMonthDropdown() {
    var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    var objDate = new Date();
    var objCurrentDay, objCurrentMonth, objCurrentYear;
    objDay = String(objDate.getDate()).padStart(2, '0');
    objCurrentMonth = monthNames[objDate.getMonth()];
    objCurrentYear = getDropdownValue('ddl-year');
    setDropdownDefaultText('ddl-Month', objCurrentMonth);
    setCurrentWeek();
}

function setCurrentWeek() {
    var d = new Date();
    var date = d.getDate();
    var day = d.getDay();
    //var weekOfMonth = Math.ceil((date - 1 - day) / 7) + 1;
    PageMethods.GetWeekNumber(d,
        function OnSuccess(result) {
            if (result != '') {
                setDropdownDefaultValue('ddl-week', result);
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

async function validateFilters() {
    hideError();
    loadingScreen(false, 0);
    var obj = await getFiltersObj();
    PageMethods.ValidateSearchFilter(obj,
        function OnSuccess(result) {
            if (result == '') {
                searchTables();
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

async function getFiltersObj() {
//    var ddlUserName = document.getElementById("ddl-userFullName");
    var ddlBranch = document.getElementById("ddl-branch");
    var ddlWeek = document.getElementById("ddl-week");

    var postData = {
        GroupCode: getDropdownValue('ddl-region') || '',
        Username:'aaa',
//        Username: (ddlUserName && ddlUserName.value !== '') ? (typeof getDropdownValue("ddl-userFullName") !== 'undefined') ? getDropdownValue("ddl-userFullName") : "" : "",
        BranchCode: (ddlBranch && ddlBranch.value !== '') ? (typeof getDropdownValue("ddl-branch") !== 'undefined') ? getDropdownValue("ddl-branch") : "" : "",
        Year: getDropdownValue('ddl-year') || '',
        MonthCode: getDropdownValue('ddl-Month') || '',
        Month: getDropdownText('ddl-Month') || '',
        WeekNumber: (ddlWeek && ddlWeek.value !== '') ? (typeof getDropdownValue("ddl-week") !== 'undefined') ? getDropdownValue("ddl-week") : "" : "",
//        BranchManagerName: (ddlUserName && ddlUserName.value !== '') ? (typeof getDropdownText("ddl-userFullName") !== 'undefined') ? getDropdownText("ddl-userFullName") : "" : ""
    };

    return postData;
}


async function searchTables() {
    loadingScreen(false, 0);
    hideError();
    var postData = {
//        Username: getDropdownValue('ddl-userFullName') || '',
        BranchCode: getDropdownValue('ddl-branch') || '',
        BranchName: getDropdownText('ddl-branch'),
        Year: getDropdownValue('ddl-year') || '',
        Month: getDropdownText('ddl-Month') || '',
        WeekNumber: getDropdownValue('ddl-week') || '',
//        BranchManagerName: getDropdownText('ddl-userFullName'),
    };
    var validpostData = ((postData.Username == '' && postData.Year == '' && postData.Month == '' && postData.WeekNumber == '') ? false : true);
    showDiv(['tblOthers'], validpostData);
    try {
        PageMethods.GetDate(postData, 
            async function (result) {
                if (result != null) {
                    showDiv(['divReport'], false);
//                    displayName = getDropdownText('ddl-userFullName') || '';
                    setLabelText("lblReportHeader", "Change In Potential Accounts of Branch (" + postData.BranchName +" )");
//                     $('#divChangesInPotentialAccounts').show();
                     showDiv(['divChangesInPotentialAccounts'], true);
                    var data = await getPotentialAccountsList(postData.BranchCode, result.FromDate, result.ToDate, result.Year, result.Month);
                    createPotentialAccountsTable(data);
                    loadingScreen(true, 0);
                }
                else {
                    showError(result);
                    loadingScreen(true, 0);
                }
                loadingScreen(true, 0);
            },
            function (err) {
                showErrorModal(err.get_message());
                loadingScreen(true, 0);
            });
    }
    catch (e) {
        showError(e);
        loadingScreen(true, 0);
    }
}

async function onLoadTables() {
    loadingScreen(false, 0);
    hideError();
    showDiv(['tblOthers', 'divChangesInPotentialAccounts'], false);
    setLabelText("lblReportHeader", "");

    var data1 = await getPotentialAccountsList('', '', '', '', '');
    createPotentialAccountsTable(data1);

    loadingScreen(true, 0);
}

async function loadWeekList() {
    loadingScreen(false, 0);
    hideError();
    await onLoadTables();
    Year = getDropdownValue('ddl-year') || '';
    Month = getDropdownText('ddl-Month') || '';
    if (Month != '' && Year != '') {
        var weekDeferred = $.Deferred();
        PageMethods.GetWeek(Year, Month,
            function OnSuccess(result) {
                weekDeferred.resolve(result);
            },
            function OnError(err) {
                weekDeferred.reject(err.get_message());
            });

        $.when(weekDeferred).done(async function (data) {
            try {
                if (data.length > 1) data.splice(0, 0, { description: 'All', value: 'All' });
                loadDropdown('ddl-week', data, true);
                 setDropdownDefaultValue('ddl-week', 'All');
            } catch (e) {
                showErrorModal(e);
            }

            loadingScreen(true, 0);
        })
            .fail(function (e) {
                showErrorModal(e);
                loadingScreen(true, 0);
            });
    }
    else {
        loadDropdown('ddl-week', WeekList, true);
        loadingScreen(true, 0);
    }
}

function getPotentialAccountsList(BranchCode, DateFrom, DateToday, Year, Month) {
    var deferred = $.Deferred();
    PageMethods.GetPotentialAccountsList(BranchCode, DateFrom, DateToday, Year, Month,
        function OnSuccess(result) {
            if (result != null) {
                deferred.resolve(result);
//                var x = result.err();
            }
            else {
                deferred.reject('There was a system problem while processing your request.');
            }
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    return deferred.promise();
}

function createPotentialAccountsTable(data) {
    var index = 1;
    var itemsPerPage = 10;
    var list = [];
    $('#tBodyPotentialAccounts').empty();
    for (var r = 0; r < data.length; r++) {
        list.push(data[r]);
    }
    secureStorage.setItem('listPotentialAccounts', JSON.stringify(list));
    allItems = data.length;
    createTable('divPotentialAccounts', 'tblPotentialAccounts', 'tBodyPotentialAccounts', list, index, itemsPerPage, allItems, 'listPotentialAccounts', 'ListPotentialAccounts');
}

function generateReport() {
    hideError();
    loadingScreen(false, 0);
    PageMethods.GenerateReport(
        function OnSuccess(result) {
            if (result == '') {
                $('iframe[name="ifrmReportViewer"]').attr('src', '../../../../../Views/Reports/ReportViewer.aspx');
                showDiv(['divReport'], true);
            }
            else {
                showError(result);
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}
