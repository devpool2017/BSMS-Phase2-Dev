var selectedSort = [];
var firstRun = true;
//var defaultYear;
//var defaultMonth;
//var defaultWeek;

$(document).ready(function () {
    loadingScreen(true, 0);

    loadClientTypesDropdown(null, "ddlClientType");
    loadClientTypesDropdown(null, "ddlClientType1");
    loadClientTypesDropdown(null, "ddlClientType2");

    toggleEventForElement("#btnAddFilter", "click", addFilter, true, null);
    toggleEventForElement("#btnSubFilter", "click", hideFilter, true, null);
    toggleEventForElement("#btnSearch", "click", searchUpdateClient, true, null);

    showDiv(['divUpdateClientList'], false);

    if((secureStorage.getItem("prevURL") === null) || (secureStorage.getItem("prevURL") === undefined)){
        secureStorage.setItem("prevURL", "");
    }

    if ((secureStorage.getItem("editParams") !== null) && (secureStorage.getItem("prevURL").includes("UpdateClient.aspx"))) {
        $.when(setParams()).done(function () {
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


function loadClientTypesDropdown(element, ddlname, value) {
    PageMethods.ClientTypesList(
    function OnSuccess(result) {
        if (result.length > 0) {
            loadDropdown(ddlname, result, true, value !== undefined ? value : result[0].value);
        }
        loadingScreen(true, 0);
    },
    function OnError(err) {
        alert(err.get_message());
    });
}

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
    setTextboxValue('txtName', '');
    //clear_form_elements("divFilters");
}


function ddlFilterOnChanged() {
    var obj = getDropdownValue('ddlFilter');
    showDiv(['lbl1', 'divSort', 'divAssDes'], true);
    if (obj !== "") {
        disableControls(['ddlFilter1'], false);
        if (obj == "ClientType") {
            showDiv(['divStatus'], false);
            showDiv(['divClientType'], true);
            disableControls(['ddlClientType'], false);
            setDropdownDefaultValue('ddlClientType', 'Individual');
        }
        else if (obj == "Status") {
            showDiv(['divClientType'], false);
            showDiv(['divStatus'], true);
            disableControls(['ddlStatus'], false);
            setDropdownDefaultValue('ddlStatus', 'Lead');
        }
    } else {

        disableControls(['ddlStatus', 'ddlStatus1', 'ddlStatus2', 'ddlClientType', 'ddlClientType1', 'ddlClientType2', 'ddlFilter1', 'ddlFilter2', 'ddlWhere1', 'ddlWhere2'], true);
        setDropdownDefaultValue('ddlStatus', '');
        setDropdownDefaultValue('ddlStatus1', '');
        setDropdownDefaultValue('ddlStatus2', '');
        setDropdownDefaultValue('ddlClientType', '');
        setDropdownDefaultValue('ddlClientType1', '');
        setDropdownDefaultValue('ddlClientType2', '');
        setDropdownDefaultValue('ddlFilter1', '');
        setDropdownDefaultValue('ddlFilter2', '');
        setDropdownDefaultValue('ddlWhere1', 'AND');
        setDropdownDefaultValue('ddlWhere2', 'AND');

        showDiv(['divClientType', 'divClientType1', 'divClientType2'], false);
        showDiv(['divStatus', 'divStatus1', 'divStatus2'], true);

    }
}

function ddlFilterOnChanged1() {
    var obj = getDropdownValue('ddlFilter1');
    showDiv(['lbl2', 'divAssDes1', 'btnAddFilter2', 'divSort1'], true);
    if (obj !== "") {
        disableControls(['ddlWhere1', 'ddlFilter2'], false);
        if (obj == "ClientType") {
            showDiv(['divStatus1'], false);
            showDiv(['divClientType1'], true);
            disableControls(['ddlClientType1'], false);
            setDropdownDefaultValue('ddlClientType1', 'Individual');
        }
        else if (obj == "Status") {
            showDiv(['divClientType1'], false);
            showDiv(['divStatus1'], true);
            disableControls(['ddlStatus1'], false);
            setDropdownDefaultValue('ddlStatus1', 'Lead');
        }
    } else {
        disableControls(['ddlStatus1', 'ddlStatus2', 'ddlClientType1', 'ddlClientType2', 'ddlFilter2', 'ddlWhere1', 'ddlWhere2'], true);
        
        setDropdownDefaultValue('ddlStatus1', '');
        setDropdownDefaultValue('ddlStatus2', '');
        setDropdownDefaultValue('ddlClientType1', '');
        setDropdownDefaultValue('ddlClientType2', '');
        setDropdownDefaultValue('ddlFilter2', '');
        setDropdownDefaultValue('ddlWhere1', 'AND');
        setDropdownDefaultValue('ddlWhere2', 'AND');

        showDiv(['divClientType1', 'divClientType2'], false);
        showDiv(['divStatus1', 'divStatus2'], true);
        
    }
}

function ddlFilterOnChanged2() {
    var obj = getDropdownValue('ddlFilter2');
    showDiv(['lbl3', 'divSort2', 'divAssDes2'], true);
    if (obj !== "") {
        disableControls(['ddlWhere2'], false);
        if (obj == "ClientType") {
            showDiv(['divStatus2'], false);
            showDiv(['divClientType2'], true);
            disableControls(['ddlClientType2'], false);
            setDropdownDefaultValue('ddlClientType2', 'Individual');
        }
        else if (obj == "Status") {
            showDiv(['divClientType2'], false);
            showDiv(['divStatus2'], true);
            disableControls(['ddlStatus2'], false);
            setDropdownDefaultValue('ddlStatus2', 'Lead');
        }
    } else {

        disableControls(['ddlStatus2', 'ddlClientType2', 'ddlWhere2'], true);
        setDropdownDefaultValue('ddlStatus2', '');
        setDropdownDefaultValue('ddlClientType2', '');
        setDropdownDefaultValue('ddlWhere2', 'AND');

        showDiv(['divClientType2'], false);
        showDiv(['divStatus2'], true);
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


function searchUpdateClient(element, filter) {
    loadingScreen(false, 0);
    var obj = new Object();
    if (filter === undefined) {

        obj.Year = getDropdownValue('ddlYear');
        obj.month = getDropdownText('ddlMonth');
        obj.month = (obj.month == "None") ? "" : obj.month;
        obj.WeekNum = getDropdownValue('ddlWeek');

        obj.SearchText = getTextboxValue('txtName');

        obj.Where1 = getDropdownValue('ddlFilter') !== '' ? 'AND' : null;
        obj.Where2 = getDropdownValue('ddlFilter1') !== '' ? getDropdownValue('ddlWhere1') : null;
        obj.Where3 = getDropdownValue('ddlFilter2') !== '' ? getDropdownValue('ddlWhere2') : null;

        if (getDropdownValue('ddlFilter') === "ClientType") {
            obj.FilterText1 = getDropdownValue('ddlClientType') !== '' ? getDropdownValue('ddlClientType') : null;
        } else if (getDropdownValue('ddlFilter') === "Status") {
            obj.Status1 = getDropdownValue('ddlStatus') !== '' ? getDropdownValue('ddlStatus') : null;
        } else {
            obj.Where1 = null;
            obj.Status1 = null;
            obj.FilterText1 = null;
        }

        if (getDropdownValue('ddlFilter1') === "ClientType") {
            obj.FilterText2 = getDropdownValue('ddlClientType1') !== '' ? getDropdownValue('ddlClientType1') : null;
        } else if (getDropdownValue('ddlFilter1') === "Status") {
            obj.Status2 = getDropdownValue('ddlStatus1') !== '' ? getDropdownValue('ddlStatus1') : null;
        } else {
            obj.Where2 = null;
            obj.Status2 = null;
            obj.FilterText2 = null;
        }


        if (getDropdownValue('ddlFilter2') === "ClientType") {
            obj.FilterText3 = getDropdownValue('ddlClientType2') !== '' ? getDropdownValue('ddlClientType2') : null;
        } else if (getDropdownValue('ddlFilter2') === "Status") {
            obj.Status3 = getDropdownValue('ddlStatus2') !== '' ? getDropdownValue('ddlStatus2') : null;
        } else {
            obj.Where3 = null;
            obj.Status3 = null;
            obj.FilterText3 = null;
        }

        obj.Order1 = getDropdownValue('ddlSort') !== '' ? getDropdownValue('ddlSort') : null;
        obj.Order2 = getDropdownValue('ddlSort1') !== '' ? getDropdownValue('ddlSort1') : null;
        obj.Order3 = getDropdownValue('ddlSort2') !== '' ? getDropdownValue('ddlSort2') : null;

        obj.SortBy1 = getDropdownValue('ddlSort') !== '' ? getDropdownValue('ddlAssDes') : null;
        obj.SortBy2 = getDropdownValue('ddlSort1') !== '' ? getDropdownValue('ddlAssDes1') : null;
        obj.SortBy3 = getDropdownValue('ddlSort2') !== '' ? getDropdownValue('ddlAssDes2') : null;

    } else {
        obj = filter;
    }

    PageMethods.ListUpdateClient(obj,
    function OnSuccess(result) {
        if (result != null) {
            var index = 1;
            var itemsPerPage = 10;
            var list = [];
            $("#tBodyUpdateClientList").empty();
            for (var r = 0; r < result.length; r++) {
                list.push(result[r]);
            }
            secureStorage.setItem('list', JSON.stringify(list));
            allItems = result.length;
            createTable("divUpdateClientList", "tblUpdateClientList", "tBodyUpdateClientList", list, index, itemsPerPage, allItems, "list", "ClientList");
        }
        loadingScreen(true, 0);
        secureStorage.setItem("editParams", obj) 
    },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
    );
}

function editClientList(ClientID) {
    loadingScreen(false, 0);
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
        disableControls(['ddlAssDes','ddlSort1'], false);
        setDropdownDefaultValue('ddlAssDes', 'ASC');

    } else {
        selectedSort[0] = null;

        disableControls(['ddlAssDes', 'ddlSort1', 'ddlSort2', 'ddlAssDes1', 'ddlAssDes2'], true);
        setDropdownDefaultValue('ddlSort1', '');
        setDropdownDefaultValue('ddlSort2', '');
        setDropdownDefaultValue('ddlAssDes', 'ASC');
        setDropdownDefaultValue('ddlAssDes1', 'ASC');
        setDropdownDefaultValue('ddlAssDes2', 'ASC');
    }

    disableSelectOptions('ddlSort', selectedSort);
    disableSelectOptions('ddlSort1', selectedSort);
    disableSelectOptions('ddlSort2', selectedSort);
}

function changeddlSort1(element) {
    var value = element.value;

    if (value !== '') {
        selectedSort[1] = value;
        disableControls(['ddlSort2', 'ddlAssDes1'], false);
        setDropdownDefaultValue('ddlAssDes1', 'ASC');
    } else {
        selectedSort[1] = null;
        disableControls(['ddlAssDes1', 'ddlSort2', 'ddlAssDes2'], true);
        setDropdownDefaultValue('ddlSort2', '');
        setDropdownDefaultValue('ddlAssDes1', 'ASC');
        setDropdownDefaultValue('ddlAssDes2', 'ASC');
    }

    disableSelectOptions('ddlSort', selectedSort);
    disableSelectOptions('ddlSort1', selectedSort);
    disableSelectOptions('ddlSort2', selectedSort);
}


function changeddlSort2(element) {
    var value = element.value;

    if (value !== '') {
        selectedSort[2] = value;
        disableControls(['ddlAssDes2'], false);
        setDropdownDefaultValue('ddlAssDes2', 'ASC');
    } else {
        selectedSort[2] = null;
    }

    disableSelectOptions('ddlSort', selectedSort);
    disableSelectOptions('ddlSort1', selectedSort);
    disableSelectOptions('ddlSort2', selectedSort);
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
            var defaultYear = data[0].value;
            var defaultMonth = data[0].description;
            var defaultWeek = data[0].data;
            $.when(loadYearDropdown_filter(defaultYear, defaultMonth, defaultWeek)).done(function () {
                initDDLFilterEvents(true);
                //secureStorage.setItem("prevURL", document.referrer.toString());
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

function setParams() {
    var obj = new Object();
    obj = secureStorage.getItem("editParams");

    initDDLFilterEvents(true);

    $("#ddlClientType").attr("hidden", false);
    $("#ddlClientType1").attr("hidden", false);
    $("#ddlClientType2").attr("hidden", false);

    if (((obj.FilterText1 !== null)) || ((obj.Order1 !== null)) || ((obj.SearchText !== ''))) {
        $("#btnAddFilter").click();
    }

    setTextboxValue('txtName', obj.SearchText);

    setDropdownDefaultValue('ddlWhere1', obj.Where2 !== null ? obj.Where2 : 'AND');
    setDropdownDefaultValue('ddlWhere2', obj.Where3 !== null ? obj.Where3 : 'AND');

    if ((obj.FilterText1 !== undefined) && (obj.FilterText1 !== null)){
        setDropdownDefaultValue('ddlFilter', 'ClientType');
        loadClientTypesDropdown(null, "ddlClientType", obj.FilterText1);
    } else if ((obj.Status1 !== undefined) && (obj.Status1 !== null)){
        setDropdownDefaultValue('ddlFilter', 'Status');
        setDropdownDefaultValue('ddlStatus', obj.Status1 !== undefined ? obj.Status1 : '');
    } else {
        setDropdownDefaultValue('ddlFilter', '');
    }

    if ((obj.FilterText2 !== undefined) && (obj.FilterText2 !== null)) {
        setDropdownDefaultValue('ddlFilter1', 'ClientType');
        loadClientTypesDropdown(null, "ddlClientType1", obj.FilterText2);
    } else if ((obj.Status2 !== undefined) && (obj.Status2 !== null)) {
        setDropdownDefaultValue('ddlFilter1', 'Status');
        setDropdownDefaultValue('ddlStatus1', obj.Status2 !== undefined ? obj.Status2 : '');
    } else {
        setDropdownDefaultValue('ddlFilter1', '');
    }

    if ((obj.FilterText3 !== undefined) && (obj.FilterText3 !== null)) {
        setDropdownDefaultValue('ddlFilter2', 'ClientType');
        loadClientTypesDropdown(null, "ddlClientType2", obj.FilterText3);
    } else if ((obj.Status3 !== undefined) && (obj.Status3 !== null)) {
        setDropdownDefaultValue('ddlFilter2', 'Status');
        setDropdownDefaultValue('ddlStatus2', obj.Status3 !== undefined ? obj.Status3 : '');
    } else {
        setDropdownDefaultValue('ddlFilter2', '');
    }

    setDropdownDefaultValue('ddlSort', obj.Order1);
    setDropdownDefaultValue('ddlSort1', obj.Order2);
    setDropdownDefaultValue('ddlSort2', obj.Order3);

    setDropdownDefaultValue('ddlAssDes', obj.SortBy1 !== null ? obj.SortBy1 : 'ASC');
    setDropdownDefaultValue('ddlAssDes1', obj.SortBy2 !== null ? obj.SortBy2 : 'ASC');
    setDropdownDefaultValue('ddlAssDes2', obj.SortBy3 !== null ? obj.SortBy3 : 'ASC');

    enableControl('ddlAssDes', obj.Order1 !== null ? true : false);

    enableControl('ddlSort1', obj.Order2 !== null ? true : false);
    enableControl('ddlAssDes1', obj.Order2 !== null ? true : false);

    enableControl('ddlSort2', obj.Order3 !== null ? true : false);
    enableControl('ddlAssDes2', obj.Order3 !== null ? true : false);

    searchUpdateClient(null, obj);
    loadYearDropdown_filter(obj.Year, obj.month, obj.WeekNum);
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

function initDDLFilterEvents(enabled) {
    toggleEventForElement("#ddlMonth", "change", ddlMonthOnChanged, enabled, null);

    toggleEventForElement("#ddlFilter", "change", ddlFilterOnChanged, enabled, null);
    toggleEventForElement("#ddlFilter1", "change", ddlFilterOnChanged1, enabled, null);
    toggleEventForElement("#ddlFilter2", "change", ddlFilterOnChanged2, enabled, null);

    toggleEventForElement("#ddlSort", "change", changeddlSort, enabled, null);
    toggleEventForElement("#ddlSort1", "change", changeddlSort1, enabled, null);
    toggleEventForElement("#ddlSort2", "change", changeddlSort2, enabled, null);
}