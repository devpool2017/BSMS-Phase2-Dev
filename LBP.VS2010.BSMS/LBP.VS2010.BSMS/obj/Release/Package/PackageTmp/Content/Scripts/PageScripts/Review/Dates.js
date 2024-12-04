$(document).ready(function () {
    loadingScreen(false, 0);
    showDiv(['divModalError'], false);
    var roleid = sessionStorage.getItem('Role');
                     if(roleid == '1')
                     {
                         showDiv(['divButtons'], true);
                     }
                     else{
                     showDiv(['divButtons'], false);
                     }
        var deferreds = {
            yearList: $.Deferred(),
            DateList: $.Deferred(),
        };
            PageMethods.DatesList(
            function OnSuccess(result) {
                deferreds.yearList.resolve(result);
            },
            function OnError(err) {
                deferreds.yearList.reject(err.get_message());
            });
              PageMethods.AllDatesList(
            function OnSuccess(result) {
                deferreds.DateList.resolve(result);
            },
            function OnError(err) {
                deferreds.DateList.reject(err.get_message());
            });

          $.when(deferreds.yearList, deferreds.DateList).done(async function (yearList,DateList) {
                   try {
                     toggleEventForElement("#ddlSearchBy", "change", changeAllDate, true, null);
                     toggleEventForElement("#btnAddDate", "click", addDate, true, null);
                     toggleEventForElement("#ddlMonth", "change", changeMonth, true, null);
                     toggleEventForElement("#ddlWeek", "change", changeWeek, true, null);
                     toggleEventForElement("#ddlYear", "change", changeDate, true, null);
                     toggleEventForElement("#btnAddSave", "click", ValidateAddDate, true, null);
                     toggleEventForElement("#btnAddCancel", "click", ClosedModal, true, null);
                     toggleEventForElement("#btnAddReset", "click", ModalReset, true, null);
                     loadDropdown('ddlSearchBy', yearList, true, "", "All");
                     loadAllDates(DateList);
                      enableControl('ddlMonth', false);
                     } catch (e) {
                       showErrorModal(e);
                   }
                   loadingScreen(true, 0);
               }).fail(function (e) {
                   showErrorModal(e);
                   loadingScreen(true, 0);
               });

});


function addDate() {
     $('#divAddDateModal').modal('show');
      loadYearList();
      
//      setDropdownDefaultValue('ddlWeek',"Please Select");
//    loadModalAllDateGridview("", "", "");
}
function ClosedModal()
{
 $('#divAddDateModal').modal('hide');
}

function ModalReset()
{
   setDropdownDefaultValue('ddlYear',"Please Select");
   setDropdownDefaultValue('ddlMonth',"Please Select");
   setDropdownDefaultValue('ddlWeek',"Please Select");
   setTextboxValue('txtFromDate','');
   setTextboxValue('txtToDate','');
    enableControl('ddlMonth', false);
            enableControl('ddlWeek', false);
       loadModalAllDateGridview('', '', '')
}
function changeAllDate()
{
  loadingScreen(false, 0);
 var obj = Object();
 obj.Year = getDropdownValue('ddlSearchBy');
   PageMethods.AllDatesListByYear(obj,
    function OnSuccess(result) {
        if (result != null) {
            var index = 1;
            var itemsPerPage = 10;
            var list = [];
            $("#tBodyDatesList").empty();
            for (var r = 0; r < result.length; r++) {
                list.push(result[r]);
            }
            secureStorage.setItem('list', JSON.stringify(list));
            allItems = result.length;
            createTable("divDatesLists", "tblDatesLists", "tBodyDatesList", list, index, itemsPerPage, allItems, "list", "DatesList");
        }
        loadingScreen(true, 0);
    },
    function OnError() {
        alert(err.get_message());
    });
}

function loadAllDates(result) {
         var index = 1;
           var itemsPerPage = 10;
           var list = [];
           $("#tBodyDatesList").empty();
           for (var r = 0; r < result.length; r++) {
               list.push(result[r]);
           }
           secureStorage.setItem('list', JSON.stringify(list));
           allItems = result.length;
           if (result.length > 0) {
               createTable("divDatesLists", "tblDatesLists", "tBodyDatesList", list, index, itemsPerPage, allItems, "list", "DatesList");
           }
}

function loadYearList() {
    loadingScreen(false, 0);
    var obj;
    PageMethods.YearList(
    function OnSuccess(result) {
        if (result.length > 0) {
            loadDropdown('ddlYear', result, true,);
            loadModalAllDateGridview("", "", "");
            setDropdownDefaultValue('ddlMonth',"Please Select");
              setDropdownDefaultValue('ddlWeek',"Please Select");
            enableControl('ddlMonth', false);
            enableControl('ddlWeek', false);
        }
        loadingScreen(true, 0);
    },
    function OnError() {
        alert(err.get_message());
    });
}

function changeDate() {
    enableControl('ddlMonth', true);
    var obj = new Object;
    obj.Year = getDropdownValue('ddlYear');
    obj.Month = getDropdownText('ddlMonth');
    obj.Week = getDropdownValue('ddlWeek');
    obj.Month = (obj.Month == "Please Select Month") ? "" : obj.Month;
    if (obj.Year == "" && (obj.Month == "Please Select Month" || obj.Month == "") && obj.Week == "") {
        loadModalAllDateGridview(obj.Year, obj.Month, obj.Week)
    } else {
        loadModalGridview(obj.Year, obj.Month, obj.Week);
    }
}

function changeMonth() {
    loadingScreen(false, 0);
    enableControl('ddlWeek', true);
    var obj = new Object;
    obj.Year = getDropdownValue('ddlYear');
    obj.Month = getDropdownText('ddlMonth');
    obj.Week = getDropdownValue('ddlWeek');
    obj.Month = (obj.Month == "Please Select Month") ? "" : obj.Month;
    if (obj.Year == "" && (obj.Month == "Please Select Month" || obj.Month == "") && obj.Week == "") {
        loadModalAllDateGridview(obj.Year, obj.Month, obj.Week)
    } else {
        loadModalGridview(obj.Year, obj.Month, obj.Week);
    }
}

function changeWeek() {
    loadingScreen(false, 0);
    var obj = new Object;
    var fromday, today;
    obj.Year = getDropdownValue('ddlYear');
    obj.Month = getDropdownText('ddlMonth');
    obj.Week = getDropdownValue('ddlWeek');
   
    obj.Month = (obj.Month == "Please Select Month" || obj.Month == "") ? "" : obj.Month;

    if (obj.Year == "" && (obj.Month == "Please Select Month" || obj.Month == "") && obj.Week == "") {
        loadModalAllDateGridview(obj.Year, obj.Month, obj.Week)
    } else {
        loadModalGridview(obj.Year, obj.Month, obj.Week);
    }
}

function daysInMonth(month, year) {
    return new Date(parseInt(year), parseInt(month), 0).getDate();
}

function loadModalAllDateGridview(Year, Month, Week) {
    PageMethods.YearMonthWeekListAll(
    function OnSuccess(result) {
        if (result != null) {
            var index = 1;
            var itemsPerPage = result.length;
            var list = [];
            $("#tBodyModalDate").empty();

            for (var r = 0; r < result.length; r++) {
                list.push(result[r]);
            }
            secureStorage.setItem('list', JSON.stringify(list));
            allItems = result.length;
            showDiv(['divModalDate'], true);
            createTable("divModalDate", "tblModalDate", "tBodyModalDate", list, index, itemsPerPage, allItems, "list", "ModalDate");

            showDiv(['divModalError', 'divPagingdivModalDate'], false);
            $('#divPagingdivModalDate').hide();
            setTextboxValue('txtFromDate', "");
            setTextboxValue('txtToDate', "");
        }
        else {
            $('#divPagingdivModalDate').hide();
            showDiv(['divModalDate'], false);
            showDiv(['divModalError'], true);
            showSpecificError(result.errorMessage, 'lblModalErrorMessage', 'divModalError');
        }
        loadingScreen(true, 0);
    },
    function OnError() {
        alert(err.get_message());
    });
}

function loadModalGridview(Year, Month, Week) {
    loadingScreen(false, 0);
    var obj = new Object;
    obj.Year = Year;
    obj.Month = Month;
    obj.Week = Week;

    PageMethods.YearMonthWeekListByMonth(obj,
    function OnSuccess(result) {
        if (result != null) {
            var index = 1;
            var itemsPerPage = result.length;
            var list = [];
            $("#tBodyModalDate").empty();
            for (var r = 0; r < result.length; r++) {
                list.push(result[r]);
            }
            secureStorage.setItem('list', JSON.stringify(list));
            allItems = result.length;
            showDiv(['divModalDate'], true);
            createTable("divModalDate", "tblModalDate", "tBodyModalDate", list, index, itemsPerPage, allItems, "list", "ModalDate");
            
            if(result.length > 0)
            {
            if (obj.Month != "" && obj.Week != "") {
                setTextboxValue('txtFromDate', (result[0].FromDate != "") ? result[0].FromDate : "" );
                setTextboxValue('txtToDate', (result[0].ToDate!= "") ? result[0].ToDate : "" );
            } else {
                setTextboxValue('txtFromDate', "");
                setTextboxValue('txtToDate', "");
            }
             showDiv(['divModalError', 'divPagingdivModalDate'], false);
             $('#divPagingdivModalDate').hide();
            }else{
             if (obj.Week == "" || obj.Week == "5") {
                setTextboxValue('txtFromDate', "");
                setTextboxValue('txtToDate', "");
            }
            else if (obj.Week == "1") {
                obj.Month = getDropdownValue('ddlMonth');
                fromday = "01"
                today = "07"
                setTextboxValue('txtFromDate', obj.Month + "/" + fromday + "/" + obj.Year);
                setTextboxValue('txtToDate', obj.Month + "/" + today + "/" + obj.Year);
            } else if (obj.Week == "2") {
                 obj.Month = getDropdownValue('ddlMonth');
                fromday = "08"
                today = "14"
                setTextboxValue('txtFromDate', obj.Month + "/" + fromday + "/" + obj.Year);
                setTextboxValue('txtToDate', obj.Month + "/" + today + "/" + obj.Year);
            } else if (obj.Week == "3") {
                obj.Month = getDropdownValue('ddlMonth');
                fromday = "15"
                today = "21"
                setTextboxValue('txtFromDate', obj.Month + "/" + fromday + "/" + obj.Year);
                setTextboxValue('txtToDate', obj.Month + "/" + today + "/" + obj.Year);
            } else if (obj.Week == "4") {
                obj.Month = getDropdownValue('ddlMonth');
                fromday = "22"
                today = daysInMonth(obj.Month, obj.Year)
                setTextboxValue('txtFromDate', obj.Month + "/" + fromday + "/" + obj.Year);
                setTextboxValue('txtToDate', obj.Month + "/" + today + "/" + obj.Year);
            }
          }
        }
        else {

            if (obj.Week == "" || obj.Week == "5") {
                setTextboxValue('txtFromDate', "");
                setTextboxValue('txtToDate', "");
            }
            else if (obj.Week == "1") {
                obj.Month = getDropdownValue('ddlMonth');
                fromday = "01"
                today = "07"
                setTextboxValue('txtFromDate', obj.Month + "/" + fromday + "/" + obj.Year);
                setTextboxValue('txtToDate', obj.Month + "/" + today + "/" + obj.Year);
            } else if (obj.Week == "2") {
                obj.Month = getDropdownValue('ddlMonth');
                fromday = "08"
                today = "14"
                setTextboxValue('txtFromDate', obj.Month + "/" + fromday + "/" + obj.Year);
                setTextboxValue('txtToDate', obj.Month + "/" + today + "/" + obj.Year);
            } else if (obj.Week == "3") {
                obj.Month = getDropdownValue('ddlMonth');
                fromday = "15"
                today = "21"
                setTextboxValue('txtFromDate', obj.Month + "/" + fromday + "/" + obj.Year);
                setTextboxValue('txtToDate', obj.Month + "/" + today + "/" + obj.Year);
            } else if (obj.Week == "4") {
                obj.Month = getDropdownValue('ddlMonth');
                fromday = "22"
                today = daysInMonth(obj.Month, obj.Year)
                setTextboxValue('txtFromDate', obj.Month + "/" + fromday + "/" + obj.Year);
                setTextboxValue('txtToDate', obj.Month + "/" + today + "/" + obj.Year);
            }
            $('#divPagingdivModalDate').hide();
            showDiv(['divModalDate'], false);
            showDiv(['divModalError'], true);
            showSpecificError(result.errorMessage, 'lblModalErrorMessage', 'divModalError');
        }
        loadingScreen(true, 0);
    },
    function OnError() {
        alert(err.get_message());
    });
}

function ValidateAddDate() {
    loadingScreen(false, 0);
    var obj = new Object;
    obj.Year = getDropdownValue('ddlYear');
    obj.Month = getDropdownText('ddlMonth');
    obj.Week = getDropdownValue('ddlWeek');
    PageMethods.Validatefields(obj,
    function OnSuccess(result) {
        if (result == null) {
            CheckIfExists();
        } else {
            showError(result.errorMessage);
        }
        loadingScreen(true, 0);
    },
    function OnError() {
        alert(err.get_message());
    });
}

function CheckIfExists() {
    loadingScreen(false, 0);
    var obj = new Object;
    obj.Year = getDropdownValue('ddlYear');
    obj.Month = getDropdownText('ddlMonth');
    obj.Week = getDropdownValue('ddlWeek');
    obj.FromDate = getTextboxValue('txtFromDate');
    obj.ToDate = getTextboxValue('txtToDate');
    showDiv(['divModalError'], false);
    PageMethods.CheckIfExistSave(obj,
        function (result) {
            if (result == "" || result == null) {
                   showSuccessModal('save', returnMainPage());
                
            }
            else {
             $("#divModalError").show();
                showSpecificError(result.errorMessage, 'lblModalErrorMessage', 'divModalError');
                showDiv(['divModalError'], true);
             
            }
            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
        }
    );
}


function returnMainPage()
{
 $('#divAddDateModal').modal('hide');
 window.location.href = "/Views/Review/Dates.aspx";
}