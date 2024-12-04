var GroupCode;
$(async function () {
    hideError();
    loadingScreen(false, 0);

    $('iframe[name="ifrmReportViewer"]').on('load', function () {
        resizeIframe(this);
    });

    var deferred = $.Deferred();
    PageMethods.OnLoadData(
        function OnSuccess(result) {
            setPrintButton(false);
            deferred.resolve(result);
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    $.when(deferred).done(async function (data) {
        try {
            var GroupList = data.GroupList;
            var YearList = data.YearList;
            var IndustryList = data.IndustryList;
            var currentUser = data.currentUser;
            currentUser.RoleID = (currentUser.RoleID.startsWith("0") ? currentUser.RoleID.substring(1) : currentUser.RoleID);
            GroupCode = (data.GroupCode).includes(currentUser.RoleID);
            IndustryList.splice(IndustryList.length, 0, { description: 'All', value: 'All' });

            loadDropdown('ddl-year', YearList, true, YearList[0].value);
            loadDropdown('ddl-industryType', IndustryList, true, "All");
            loadDropdown('ddl-group', GroupList, !GroupCode, (currentUser.GroupCode != "" ? currentUser.GroupCode : GroupList[0].value));
            showDiv(['divTblSummaryPerGroupBranch'], false);

            if (GroupCode) {
                $("#tbl-SummaryPerGroupBranchReport").find('tr').each(function () {
                    var trow = $(this);
                    if (trow.index() === 0) {
                        if (trow[0].id == '') {
                            trow.append('<th align="center" valign="middle" width="20%" data-name="TotalRevisits" data-alignment="right" data-columnname="TotalRevisits">Revisits</th >');
                        } else {
                            trow.append('<td width="20%" id="lblTotalRevisits"></td>');
                        }
                    }
                });
            }
            
            toggleEventForElement('#btnReset', 'click', function () {
                hideError();
                loadingScreen(false, 0);
                setDropdownDefaultValue('ddl-year', YearList[0].value);
                setDropdownDefaultValue('ddl-industryType', 'All');
                setDropdownDefaultValue('ddl-group', (currentUser.GroupCode != "" ? currentUser.GroupCode : GroupList[0].value));                
                showDiv(['divTblSummaryPerGroupBranch'], false);
                loadingScreen(true, 0);
            }, true);
                        
            showDiv(['divReportDetails'], GroupCode);

            toggleEventForElement('#btnSearch', 'click', AJAXWrapPageMethodCall, true, 'ValidateSearch');
            toggleEventForElement('#btnPrintReport', 'click', AJAXWrapPageMethodCall, GroupCode, 'GenerateReport');
            toggleEventForElement('#ddl-year', 'change', function () { setPrintButton(false); }, true);
            toggleEventForElement('#ddl-industryType', 'change', function () { setPrintButton(false); }, true);
            toggleEventForElement('#ddl-group', 'change', function () { setPrintButton(false); }, true);
            toggleEventForElement('#ddl-branch', 'change', function () { setPrintButton(false); }, true);
        } catch (e) {
            showErrorModal(e);
        }
        loadingScreen(true, 0);

    }).fail(function (e) {
        showErrorModal(e);
        loadingScreen(true, 0);
    });
});


async function getFiltersObj() {
    var group = document.getElementById("ddl-group");
    var industryType = document.getElementById("ddl-industryType");
    var postData = {
        Year: (typeof getDropdownValue("ddl-year") !== 'undefined') ? getDropdownValue("ddl-year") : "",
        GroupCode: (group && group.value !== '') ? ((typeof getDropdownValue("ddl-group") !== 'undefined') ? getDropdownValue("ddl-group") : "") : "",
        Group: (group && group.value !== '') ? ((typeof getDropdownText("ddl-group") !== 'undefined') ? getDropdownText("ddl-group") : "") : "",
        IndustryType: (industryType && industryType.value !== '') ? ((typeof getDropdownValue("ddl-industryType") !== 'undefined') ? getDropdownValue("ddl-industryType") : "") : "",
        IndustryTypeDesc: (industryType && industryType.value !== '') ? ((typeof getDropdownText("ddl-industryType") !== 'undefined') ? getDropdownText("ddl-industryType") : "") : "",
    };
    return postData;
}

async function ValidateSearch() {
    hideError();
    loadingScreen(false, 0);
    var obj = await getFiltersObj();
    PageMethods.ValidateSearchFilter(obj,
        function OnSuccess(result) {
            if (result == '') {
                SearchSummaryPerGroupBranch(obj);
                setPrintButton(true);
            }
            else {
                //showDiv(['tBody-SummaryPerGroupBranchReport', 'tBody-SummaryReportTotal'], false);
                showError(result);
                loadingScreen(true, 0);
            }
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

function SearchSummaryPerGroupBranch(obj) {
    hideError();
    var deferred = $.Deferred();
    PageMethods.GetSummaryPerGroupBranch(obj,
        function OnSuccess(data) {
            if (data != null) {

                var index = 1;
                var itemsPerPage = data.length;
                var list = [];
                $('#tBody-SummaryPerGroupBranchReport').empty();
                for (var r = 0; r < data.length; r++) {
                    list.push(data[r]);
                }
                secureStorage.setItem('listSummaryPerGroupBranch', JSON.stringify(list));
                allItems = data.length;
                showDiv(['divTblSummaryPerGroupBranch'], true);
                setLabelText("lblTotalCPACount", data.length > 0 ? data[0].TotalCPACount_Group : '');
                setLabelText("lblTotalLeads", data.length > 0 ? data[0].TotalLeads_Group : '');
                if (GroupCode) setLabelText("lblTotalRevisits", data.length > 0 ? data[0].TotalRevisits_Group : '');
                createTable('div-SummaryPerGroupBranchReport', 'tbl-SummaryPerGroupBranchReport', 'tBody-SummaryPerGroupBranchReport', list, index, itemsPerPage, allItems, 'listSummaryPerGroupBranch', 'SummaryPerGroupBranch');

                loadingScreen(true, 0);
                deferred.resolve();
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

async function GenerateReport() {
    hideError();
    loadingScreen(false, 0);
    var obj = await getFiltersObj();
    PageMethods.GenerateReport(obj,
        function OnSuccess(result) {
            if (result == '') {
                $('iframe[name="ifrmReportViewer"]').attr('src', '../../../../Views/Reports/ReportViewer.aspx');
                showDiv(['divReport'], true);
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

function setPrintButton(isEnabled) {
    hideError();
    if (!isEnabled) {
        $(document.body).off('click', '#btnPrintReport');
        if (!$('#btnPrintReport').hasClass('btn-lbp-gray'))
            $('#btnPrintReport').addClass('btn-lbp-gray');
        $('#btnPrintReport').removeClass('btn-lbp-green');
    }
    else {
        if (!$('#btnPrintReport').hasClass('btn-lbp-green'))
            $('#btnPrintReport').addClass('btn-lbp-green');
        $('#btnPrintReport').removeClass('btn-lbp-gray');
    }
}

function resizeIframe(obj) {
    obj.style.height = obj.contentWindow.document.documentElement.scrollHeight + 'px';
    loadingScreen(true, 0);
}