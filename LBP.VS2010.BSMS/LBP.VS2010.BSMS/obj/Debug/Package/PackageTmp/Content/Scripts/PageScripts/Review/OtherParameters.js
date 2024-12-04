$(document).ready(function () {
    loadingScreen(true, 0);

    loadOtherParameters();
    loadGroup();

    toggleEventForElement('#ddlRegion', 'change', AJAXWrapPageMethodCall, true, 'GetOtherParametersRH');
});

function loadOtherParameters() {
    loadingScreen(false, 0);

    PageMethods.GetOtherParameters(
        function OnSuccess(result) {
            if (result != null) {
                setTextboxValue("txtDaysBefore", result[0].DaysBefore);
                setTextboxValue("txtTargetLeads", result[0].TargetLeads);
                setTextboxValue("txtTargetAccountsClosed", result[0].TargetNewAccountsClosed);
                setTextboxValue("txtTargetADB", result[0].TargetADB);
                setTextboxValue("txtDaysBefore2", result[0].CustomerDaysBefore);
                setTextboxValue("txtEditDate", result[0].EditVisitTag);
                setTextboxValue("txtSearch", result[0].MaximumSearchCount);
                setTextboxValue("txtCPAYear", result[0].CPAYear);
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showError(err.get_message());
        });
}

function loadGroup() {
    loadingScreen(false, 0);

    PageMethods.GetGroup(
        function OnSuccess(result) {
            if (result.length > 0) {
                loadDropdown("ddlRegion", result, true, result[0].value);
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showError(err.get_message());
        });
}

function GetOtherParametersRH() {
    loadingScreen(false, 0);
    var RegionCode = getDropdownValue("ddlRegion");

    PageMethods.GetOtherParametersRH(RegionCode,
        function OnSuccess(result) {
            if (result.length > 0) {
                setTextboxValue("txtStartDateRange", result[0].ParameterValue);
                setTextboxValue("txtEndDateRange", result[1].ParameterValue);
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showError(err.get_message());
        });
}