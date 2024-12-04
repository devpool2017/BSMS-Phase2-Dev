var selectedSort = [];
var selectedRegion = null;
var selectedRegionTemp = "";
var firstRun = true;
//var defaultYear;
//var defaultMonth;
//var defaultWeek;

$(document).ready(function () {
    //loadingScreen(true, 0);

    toggleEventForElement("#btnAddFilter", "click", addFilter, true, null);
    toggleEventForElement("#btnSubFilter", "click", hideFilter, true, null);

    toggleEventForElement("#btnSearch", "click", searchClient, true, null);

    toggleEventForElement("#ddlSearchCategory", "change", changedSearchCategory, true, null);

    showDiv(['divUpdateClientList'], false);

    if((secureStorage.getItem("prevURL") === null) || (secureStorage.getItem("prevURL") === undefined)){
        secureStorage.setItem("prevURL", "");
    }

    if ((secureStorage.getItem("searchParams") !== null) && (secureStorage.getItem("prevURL").includes("SearchTargetMarket.aspx"))) {
        $.when(setParams()).done(function () {
            initDDLFilterEvents(true);
            firstRun = false;
            loadingScreen(true, 0);
            secureStorage.setItem("prevURL", "");
        });
    } else {
        getCurrentWeek();
    }
});

//function loadYearDropdown() {
//    loadingScreen(false, 0)
//    PageMethods.YearList(
//    function OnSuccess(result) {
//        if (result.length > 0) {
//            loadDropdown('ddlYear', result, true);
//            setDropdownDefaultText('ddlYear', defaultYear);
//            loadMonthDropdown();
//        }
//        loadingScreen(true, 0);
//    },
//    function OnError(err) {
//        alert(err.get_message());
//    });
//}

//function loadMonthDropdown() {
//    setDropdownDefaultText('ddlMonth', defaultMonth);
//    loadWeekDropdown(defaultYear, defaultMonth, defaultWeek);
//}


//function loadWeekDropdown(objCurrentYear, objCurrentMonth, value) {
//    var obj = new Object();
//    obj.Year = objCurrentYear;
//    obj.month = objCurrentMonth;

//    PageMethods.MinMaxWeekList(obj,
//    function OnSuccess(result) {
//        if (result.length > 0) {
//            loadDropdown('ddlWeek', result, true, value);
//            currentWeek = "";
//        }
//        loadingScreen(true, 0);
//    },
//    function OnError(err) {
//        alert(err.get_message());
//    });
//}


function addFilter() {
    $("#divFilters").show();
    showDiv(['btnSubFilter'], true);
    showDiv(['btnAddFilter'], false);
}

function hideFilter() {
    $("#divFilters").hide();
    showDiv(['btnSubFilter'], false);
    showDiv(['btnAddFilter'], true);

    setDropdownDefaultValue('ddlFilter', '');
    setDropdownDefaultValue('ddlSort', '');
    setDropdownDefaultValue('ddlSearchCategory', '');
}


function ddlFilterOnChanged() {
    var obj = getDropdownValue('ddlFilter');
    showDiv(['lbl1', 'divSort', 'divAssDes'], true);

    if(!firstRun){
        ddlSetFilter(obj, 'ddlFilterColumn1', ['ddlFilter1'], ['ddlFilter1', 'ddlFilter2', 'ddlFilter3']);
    }
    
}

function ddlFilterOnChanged1() {
    var obj = getDropdownValue('ddlFilter1');
    showDiv(['lbl2', 'divAssDes1', 'btnAddFilter2', 'divSort1'], true);

    if(!firstRun){
        ddlSetFilter(obj, 'ddlFilterColumn2', ['ddlWhere1', 'ddlFilter2'], ['ddlWhere1', 'ddlFilter2', 'ddlFilter3']);
    }
}

function ddlFilterOnChanged2() {
    var obj = getDropdownValue('ddlFilter2');
    showDiv(['lbl3', 'divSort2', 'divAssDes2'], true);

    if(!firstRun){
        ddlSetFilter(obj, 'ddlFilterColumn3', ['ddlWhere2', 'ddlFilter3'], ['ddlWhere2', 'ddlFilter3']);
    }
}


function ddlFilterOnChanged3() {
    var obj = getDropdownValue('ddlFilter3');
    showDiv(['lbl3', 'divSort2', 'divAssDes2'], true);

    if(!firstRun){
        ddlSetFilter(obj, 'ddlFilterColumn4', ['ddlWhere3'], ['ddlWhere3']);
    }
}

function ddlMonthOnChanged() {
    var obj = getDropdownValue('ddlMonth');
    var refObj = new Object();

    if (obj === "") {
        enableControl('ddlWeek', false);
        setDropdownDefaultValue('ddlWeek', '');
    } else {

        if(firstRun){
            firstRun = false;
        }else{
            enableControl('ddlWeek', true);
            var selectedYear = getDropdownText("ddlYear");
            var selectedMonth = getDropdownText("ddlMonth");
            loadWeekDropdown_filter(selectedYear, selectedMonth, '1');
        }

    }
}


function searchClient(element, filter) {
//    loadingScreen(false, 0);
    var obj = new Object();

    if (filter === undefined) {

        obj.Year = getDropdownValue('ddlYear');
        obj.month = getDropdownText('ddlMonth');
        obj.month = (obj.month == "None") ? "" : obj.month;
        obj.WeekNum = getDropdownValue('ddlWeek');

        obj.SearchCol = getDropdownValue('ddlSearchCategory') !== '' ? getDropdownValue('ddlSearchCategory') : null;
        obj.SearchText = getDropdownValue('ddlSearchCategory') !== '' ? getTextboxValue('txtName') : null;

        obj.Where1 = getDropdownValue('ddlFilter') !== '' ? 'AND' : null;
        obj.Where2 = getDropdownValue('ddlFilter1') !== '' ? getDropdownValue('ddlWhere1') : null;
        obj.Where3 = getDropdownValue('ddlFilter2') !== '' ? getDropdownValue('ddlWhere2') : null;
        obj.Where4 = getDropdownValue('ddlFilter3') !== '' ? getDropdownValue('ddlWhere3') : null;

        obj.FilterCol1 = getDropdownValue('ddlFilter') !== '' ? getDropdownValue('ddlFilter') : null;
        obj.FilterCol2 = getDropdownValue('ddlFilter1') !== '' ? getDropdownValue('ddlFilter1') : null;
        obj.FilterCol3 = getDropdownValue('ddlFilter2') !== '' ? getDropdownValue('ddlFilter2') : null;
        obj.FilterCol4 = getDropdownValue('ddlFilter3') !== '' ? getDropdownValue('ddlFilter3') : null;

        obj.FilterText1 = getDropdownValue('ddlFilter') !== '' ? getDropdownValue('ddlFilterColumn1') : null;
        obj.FilterText2 = getDropdownValue('ddlFilter1') !== '' ? getDropdownValue('ddlFilterColumn2') : null;
        obj.FilterText3 = getDropdownValue('ddlFilter2') !== '' ? getDropdownValue('ddlFilterColumn3') : null;
        obj.FilterText4 = getDropdownValue('ddlFilter3') !== '' ? getDropdownValue('ddlFilterColumn4') : null;

        obj.Order1 = getDropdownValue('ddlSort') !== '' ? getDropdownValue('ddlSort') : null;
        obj.Order2 = getDropdownValue('ddlSort1') !== '' ? getDropdownValue('ddlSort1') : null;
        obj.Order3 = getDropdownValue('ddlSort2') !== '' ? getDropdownValue('ddlSort2') : null;
        obj.Order4 = getDropdownValue('ddlSort3') !== '' ? getDropdownValue('ddlSort3') : null;

        obj.SortBy1 = getDropdownValue('ddlSort') !== '' ? getDropdownValue('ddlAssDes') : null;
        obj.SortBy2 = getDropdownValue('ddlSort1') !== '' ? getDropdownValue('ddlAssDes1') : null;
        obj.SortBy3 = getDropdownValue('ddlSort2') !== '' ? getDropdownValue('ddlAssDes2') : null;
        obj.SortBy4 = getDropdownValue('ddlSort2') !== '' ? getDropdownValue('ddlAssDes3') : null;

    } else {
        obj = filter;
    }


    PageMethods.ListSearchClient(obj,
    function OnSuccess(result) {
        setLabelText('lblSearchCount', result.message);
        if (result.searchCount <= result.maxSearchCount) {
            var index = 1;
            var itemsPerPage = 10;
            var list = [];
            $("#tBodySearchClientList").empty();
            for (var r = 0; r < result.SearchClientList.length; r++) {
                list.push(result.SearchClientList[r]);
            }
            secureStorage.setItem('list', JSON.stringify(list));
            allItems = result.SearchClientList.length;
            createTable("divSearchClientList", "tblSearchClientList", "tBodySearchClientList", list, index, itemsPerPage, allItems, "list", "ClientList");
        } else if (result.searchCount > result.maxSearchCount) {
            $("#divSearchClientList").attr("hidden", true);
            $("#divPagingdivSearchClientList").attr("hidden", true);
            $("#divNoRecordFounddivSearchClientList").attr("hidden", true);
        }

        loadingScreen(true, 0);
        secureStorage.setItem("searchParams", obj)
    },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
}

function viewClientList(ClientID) {
//    loadingScreen(false, 0);
    //var obj = new Object();
    //var clientID = ClientID;
    PageMethods.SaveDetailsSession(ClientID,
    function OnSuccess(result) {
        if (result == "") {
            window.location.href = "/Views/TargetMarket/UpdateClientDetails.aspx";
        }
        loadingScreen(true, 0);
    },
    function OnError() {
        alert(err.get_message());
    });

}

function changeddlSort(element) {
    var value = element.value;

    if (value !== '') {
        selectedSort[0] = value;
        disableControls(['ddlAssDes', 'ddlSort1'], false);
        setDropdownDefaultValue('ddlAssDes', 'ASC');

    } else {
        selectedSort[0] = null;

        disableControls(['ddlAssDes', 'ddlSort1', 'ddlSort2', 'ddlSort3', 'ddlAssDes1', 'ddlAssDes2', 'ddlAssDes3'], true);
        setDropdownDefaultValue('ddlSort1', '');
        setDropdownDefaultValue('ddlSort2', '');
        setDropdownDefaultValue('ddlSort3', '');
        setDropdownDefaultValue('ddlAssDes', 'ASC');
        setDropdownDefaultValue('ddlAssDes1', 'ASC');
        setDropdownDefaultValue('ddlAssDes2', 'ASC');
        setDropdownDefaultValue('ddlAssDes3', 'ASC');
    }

    disableSelectOptions('ddlSort', selectedSort);
    disableSelectOptions('ddlSort1', selectedSort);
    disableSelectOptions('ddlSort2', selectedSort);
    disableSelectOptions('ddlSort3', selectedSort);
}

function changeddlSort1(element) {
    var value = element.value;

    if (value !== '') {
        selectedSort[1] = value;
        disableControls(['ddlSort2', 'ddlAssDes1'], false);
        setDropdownDefaultValue('ddlAssDes1', 'ASC');
    } else {
        selectedSort[1] = null;
        disableControls(['ddlAssDes1', 'ddlSort2', 'ddlSort3', 'ddlAssDes2', 'ddlAssDes3'], true);
        setDropdownDefaultValue('ddlSort2', '');
        setDropdownDefaultValue('ddlSort3', '')
        setDropdownDefaultValue('ddlAssDes1', 'ASC');
        setDropdownDefaultValue('ddlAssDes2', 'ASC');
        setDropdownDefaultValue('ddlAssDes3', 'ASC');
    }

    disableSelectOptions('ddlSort', selectedSort);
    disableSelectOptions('ddlSort1', selectedSort);
    disableSelectOptions('ddlSort2', selectedSort);
    disableSelectOptions('ddlSort3', selectedSort);
}


function changeddlSort2(element) {
    var value = element.value;

    if (value !== '') {
        selectedSort[2] = value;
        disableControls(['ddlSort3', 'ddlAssDes2'], false);
        setDropdownDefaultValue('ddlAssDes2', 'ASC');
    } else {
        selectedSort[2] = null;
        disableControls(['ddlSort3', 'ddlAssDes2', 'ddlAssDes3'], true);
        setDropdownDefaultValue('ddlSort3', '')
        setDropdownDefaultValue('ddlAssDes3', 'ASC');
    }

    disableSelectOptions('ddlSort', selectedSort);
    disableSelectOptions('ddlSort1', selectedSort);
    disableSelectOptions('ddlSort2', selectedSort);
    disableSelectOptions('ddlSort3', selectedSort);
}

function changeddlSort3(element) {
    var value = element.value;

    if (value !== '') {
        selectedSort[3] = value;
        disableControls(['ddlAssDes3'], false);
        setDropdownDefaultValue('ddlAssDes3', 'ASC');
    } else {
        selectedSort[3] = null;
        disableControls(['ddlAssDes3'], true);
    }

    disableSelectOptions('ddlSort', selectedSort);
    disableSelectOptions('ddlSort1', selectedSort);
    disableSelectOptions('ddlSort2', selectedSort);
    disableSelectOptions('ddlSort3', selectedSort);
}

function clear_form_elements(id) {
    $("#" + id).find(':input').each(function () {
        switch (this.type) {
            case 'password':
            case 'text':
            case 'textarea':
            case 'file':
            case 'select-one':
            case 'select-multiple':
            case 'date':
            case 'number':
            case 'tel':
            case 'email':
                $(this).val('');
                break;
            case 'checkbox':
            case 'radio':
                this.checked = false;
                break;
        }
    });
}


function getCurrentWeek() {
    var deferred = $.Deferred();

    PageMethods.GetWeekNum(
        function OnSuccess(result) {
            deferred.resolve(result);
        },
        function OnError(err) {
            deferred.reject(err.get_message());
    });


        $.when(deferred).done(async function (data) {
        try {
            defaultYear = data[0].value;
            defaultMonth = data[0].description;
            defaultWeek = data[0].data;
            $.when(loadYearDropdown_filter(defaultYear, defaultMonth, defaultWeek)).done(function () {
                initDDLFilterEvents(true);
//                secureStorage.setItem("prevURL", document.referrer.toString());
                firstRun = false;
                loadingScreen(true, 0);
            });
        } catch (e) {
            showErrorModal(e);
        }

        loadingScreen(true, 0);
    }).fail(function (e) {
        showErrorModal(e);
        loadingScreen(true, 0);
    });
}

function populateDDLRegion(ddlName, value) {
//    loadingScreen(false, 0);

    PageMethods.RegionList(
    function OnSuccess(result) {
        if (result.length > 0) {
            loadDropdown(ddlName, result, true, value !== undefined ? value : result[0].value);
        }
        loadingScreen(true, 0);
    },
    function OnError(err) {
        alert(err.get_message());
    });

}

function populateDDLBranch(ddlName, value) {
//    loadingScreen(false, 0);
    //var selectedRegion = secureStorage.getItem("selectedRegion");

    if (selectedRegion === selectedRegionTemp) {
        var branches = secureStorage.getItem("branches");
        loadDropdown(ddlName, branches, true, value !== undefined ? value : branches[0].value);
        loadingScreen(true, 0);
    } else {
        PageMethods.BranchList(selectedRegionTemp,
        function OnSuccess(result) {
            if (result.length > 0) {
                loadDropdown(ddlName, result, true, value !== undefined ? value : result[0].value);
                secureStorage.setItem("branches", result);
                selectedRegion = selectedRegionTemp;
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            alert(err.get_message());
        });
    }
}

//function populateDDLPosition(ddlName) {
//    $('#' + ddlName).append($('<option>', {
//        value: 'BM',
//        text: 'Branch Managers'
//    }));

//    $('#' + ddlName).append($('<option>', {
//        value: 'SO',
//        text: 'Sales Officers'
//    }));
//    enableControl(ddlName, true);
//}

function populateDDLClientType(ddlName, value) {
//    loadingScreen(false, 0);

    PageMethods.ClientTypesList(
    function OnSuccess(result) {
        if (result.length > 0) {
            loadDropdown(ddlName, result, true, value !== undefined ? value : result[0].value);
        }
        loadingScreen(true, 0);
    },
    function OnError(err) {
        alert(err.get_message());
    });

}

function populateDLLDefault(ddlName) {
    clearDropdown(ddlName);
    $('#' + ddlName).append($('<option>', {
        value: '',
        text: 'Please Select'
    }));
    enableControl(ddlName, false);
    loadingScreen(true, 0);
}

function ddlSetFilter(selected, ddlname, enabled, disabled) {
    var disabledOptions = [];
    clearDropdown(ddlname);

    if (selected === 'RegionCode') {
        loadingScreen(false, 0);
        populateDDLRegion(ddlname);
        $('#' + ddlname).addClass("regionDDL");
        disableControls(enabled, false);
    } else if (selected === 'BrCode') {
        loadingScreen(false, 0);
        populateDDLBranch(ddlname);
        disableControls(enabled, false);
    } else if (selected === 'Position') {
        loadingScreen(false, 0);
        populateDDLPosition(ddlname);
        disableControls(enabled, false);
    } else if (selected === 'ClientType') {
        loadingScreen(false, 0);
        populateDDLClientType(ddlname);
        disableControls(enabled, false);
    } else {
        loadingScreen(false, 0);
        populateDLLDefault(ddlname);
        disableControls(disabled, true);
        selectedRegionTemp = "";
        $('#' + ddlname).removeClass('regionDDL');
        for (var x = 0; x < disabled.length; x++) {
            if ($('#' + disabled[x]).hasClass('filter')) {
                setDropdownDefaultValue(disabled[x], '');
            }
        }
    }

    var ctr = 0;
    var ctr2 = 0;
    $('.filter').each(function () {
        if ($(this).find(':selected').val() === "BrCode") {
            ctr++;
        } else if ($(this).find(':selected').val() === "RegionCode") {
            ctr2++;
        }
    });

    if (ctr > 0) {
        disabledOptions.push('RegionCode');
    } else if (ctr2 > 1) {
        disabledOptions.push('BrCode');
    } else {
        disabledOptions.splice(0, 1);
    }

    disableSelectOptions('ddlFilter', disabledOptions);
    disableSelectOptions('ddlFilter1', disabledOptions);
    disableSelectOptions('ddlFilter2', disabledOptions);
    disableSelectOptions('ddlFilter3', disabledOptions);

}

function changedSearchCategory(element) {
    var value = element.value;
    if (value !== "") {
        disableControls(['txtName'], false);
    } else {
        disableControls(['txtName'], true);
        setTextboxValue('txtName', '');
    }

}

function getSelectedRegion(element) {
    var value = element.value;
    var id = element.id;

    if ($('#' + id).hasClass('regionDDL')) {
        //secureStorage.setItem("selectedRegion", getDropdownValue(id));
        selectedRegionTemp = getDropdownValue(id);
        refreshBranch();
    }

}

function refreshBranch() {

    $('.filter').each(function () {
        if ($(this).find(':selected').val() === "BrCode") {
            //setDropdownDefaultValue($(this).attr('id'), '');
            setDropdownDefaultValue($(this).attr('id'), 'BrCode');
        }
    });
}

function setParams(callback) {
    initDDLFilterEvents(false);
    var obj = new Object();
    obj = secureStorage.getItem("searchParams");


    if (((obj.FilterText1 !== null)) || ((obj.Order1 !== null)) || ((obj.SearchText !== null))) {
        $("#btnAddFilter").click();
    }

    setDropdownDefaultValue('ddlSearchCategory', obj.SearchCol);
    setTextboxValue('txtName', obj.SearchText);

    setDropdownDefaultValue('ddlWhere1', obj.Where2 !== null ? obj.Where2 : 'AND');
    setDropdownDefaultValue('ddlWhere2', obj.Where3 !== null ? obj.Where3 : 'AND');
    setDropdownDefaultValue('ddlWhere3', obj.Where4 !== null ? obj.Where4 : 'AND');

    setDropdownDefaultValue('ddlFilter', obj.FilterCol1);
    setDropdownDefaultValue('ddlFilter1', obj.FilterCol2);
    setDropdownDefaultValue('ddlFilter2', obj.FilterCol3);
    setDropdownDefaultValue('ddlFilter3', obj.FilterCol4);

    showDiv(['lbl1', 'divSort', 'divAssDes'], true);
    ddlSetFilter_Back(obj.FilterCol1, 'ddlFilterColumn1', ['ddlFilter1'], ['ddlFilter1', 'ddlFilter2', 'ddlFilter3'], obj.FilterText1);

    showDiv(['lbl2', 'divAssDes1', 'btnAddFilter2', 'divSort1'], true);
    ddlSetFilter_Back(obj.FilterCol2, 'ddlFilterColumn2', ['ddlWhere1', 'ddlFilter2'], ['ddlWhere1', 'ddlFilter2', 'ddlFilter3'], obj.FilterText2);

    showDiv(['lbl3', 'divSort2', 'divAssDes2'], true);
    ddlSetFilter_Back(obj.FilterCol3, 'ddlFilterColumn3', ['ddlWhere2', 'ddlFilter3'], ['ddlWhere2', 'ddlFilter3'], obj.FilterText3);

    showDiv(['lbl3', 'divSort2', 'divAssDes2'], true);
    ddlSetFilter_Back(obj.FilterCol4, 'ddlFilterColumn4', ['ddlWhere3'], ['ddlWhere3'], obj.FilterText4);

    setDropdownDefaultValue('ddlSort', obj.Order1);
    setDropdownDefaultValue('ddlSort1', obj.Order2);
    setDropdownDefaultValue('ddlSort2', obj.Order3);
    setDropdownDefaultValue('ddlSort3', obj.Order4);

    setDropdownDefaultValue('ddlAssDes', obj.SortBy1 !== null ? obj.SortBy1 : 'ASC');
    setDropdownDefaultValue('ddlAssDes1', obj.SortBy2 !== null ? obj.SortBy2 : 'ASC');
    setDropdownDefaultValue('ddlAssDes2', obj.SortBy3 !== null ? obj.SortBy3 : 'ASC');
    setDropdownDefaultValue('ddlAssDes3', obj.SortBy4 !== null ? obj.SortBy4 : 'ASC');

    enableControl('ddlAssDes', obj.Order1 !== null ? true : false);

    enableControl('ddlSort1', obj.Order2 !== null ? true : false);
    enableControl('ddlAssDes1', obj.Order2 !== null ? true : false);

    enableControl('ddlSort2', obj.Order3 !== null ? true : false);
    enableControl('ddlAssDes2', obj.Order3 !== null ? true : false);

    enableControl('ddlSort3', obj.Order4 !== null ? true : false);
    enableControl('ddlAssDes3', obj.Order4 !== null ? true : false);

    searchClient(null, obj);
    loadYearDropdown_filter(obj.Year, obj.month, obj.WeekNum);
}

function initDDLFilterEvents(enabled) {
    toggleEventForElement("#ddlMonth", "change", ddlMonthOnChanged, enabled, null);
    
    toggleEventForElement("#ddlFilter", "change", ddlFilterOnChanged, enabled, null);
    toggleEventForElement("#ddlFilter1", "change", ddlFilterOnChanged1, enabled, null);
    toggleEventForElement("#ddlFilter2", "change", ddlFilterOnChanged2, enabled, null);
    toggleEventForElement("#ddlFilter3", "change", ddlFilterOnChanged3, enabled, null);

    toggleEventForElement("#ddlSort", "change", changeddlSort, enabled, null);
    toggleEventForElement("#ddlSort1", "change", changeddlSort1, enabled, null);
    toggleEventForElement("#ddlSort2", "change", changeddlSort2, enabled, null);
    toggleEventForElement("#ddlSort3", "change", changeddlSort3, enabled, null);

    toggleEventForElement("#ddlFilterColumn1", "change", getSelectedRegion, enabled, null);
    toggleEventForElement("#ddlFilterColumn2", "change", getSelectedRegion, enabled, null);
    toggleEventForElement("#ddlFilterColumn3", "change", getSelectedRegion, enabled, null);
    toggleEventForElement("#ddlFilterColumn4", "change", getSelectedRegion, enabled, null);
}

function ddlSetFilter_Back(selected, ddlname, enabled, disabled, value) {
    var disabledOptions = [];
    clearDropdown(ddlname);

    if (selected === 'RegionCode') {
        populateDDLRegion(ddlname, value);
        $('#' + ddlname).addClass("regionDDL");
        disableControls(enabled, false);
    } else if (selected === 'BrCode') {
        populateDDLBranch(ddlname, value);
        disableControls(enabled, false);
        //    } else if (selected === 'Position') {
        //        populateDDLPosition(ddlname, value);
        //        disableControls(enabled, false);
    } else if (selected === 'ClientType') {
        populateDDLClientType(ddlname, value);
        disableControls(enabled, false);
    } else {
        populateDLLDefault(ddlname);
        disableControls(disabled, true);
        for (var x = 0; x < disabled.length; x++) {
            if ($('#' + disabled[x]).hasClass('filter')) {
                setDropdownDefaultValue(disabled[x], '');
            }
        }
    }

    var ctr = 0;
    var ctr2 = 0;
    $('.filter').each(function () {
        if ($(this).find(':selected').val() === "BrCode") {
            ctr++;
        } else if ($(this).find(':selected').val() === "RegionCode") {
            ctr2++;
        }
    });

    if (ctr > 0) {
        disabledOptions.push('RegionCode');
    } else if (ctr2 > 1) {
        disabledOptions.push('BrCode');
    } else {
        disabledOptions.splice(0, 1);
    }

    disableSelectOptions('ddlFilter', disabledOptions);
    disableSelectOptions('ddlFilter1', disabledOptions);
    disableSelectOptions('ddlFilter2', disabledOptions);
    disableSelectOptions('ddlFilter3', disabledOptions);

}

function loadYearDropdown_filter(valueYear, valueMonth, valueWeek) {
    loadingScreen(false, 0)
    PageMethods.YearList(
    function OnSuccess(result) {
        if (result.length > 0) {
            loadDropdown('ddlYear', result, true);
            setDropdownDefaultText('ddlYear', valueYear);
            loadMonthDropdown_filter(valueYear, valueMonth, valueWeek);
        }
        loadingScreen(true, 0);
    },
    function OnError(err) {
        alert(err.get_message());
    });
}


function loadMonthDropdown_filter(valueYear, valueMonth, valueWeek) {
    var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    var objDate = new Date();
    var objCurrentDay, objCurrentMonth, objCurrentYear;
    setDropdownDefaultText('ddlMonth', valueMonth);

    if (valueMonth !== '') {
        loadWeekDropdown_filter(valueYear, valueMonth, valueWeek);
    } else {
        clearDropdown('ddlWeek');
        $('#ddlWeek').append($('<option>', {
            value: '',
            text: 'None'
        }));
        enableControl('ddlWeek', false);
    }
}


function loadWeekDropdown_filter(objCurrentYear, objCurrentMonth, value) {
    var obj = new Object();
    obj.Year = objCurrentYear;
    obj.month = objCurrentMonth;

    PageMethods.MinMaxWeekList(obj,
    function OnSuccess(result) {
        if (result.length > 0) {
            loadDropdown('ddlWeek', result, true, value);
        }
        loadingScreen(true, 0);
    },
    function OnError(err) {
        alert(err.get_message());
    });
}