//PAGER START
function pagePerList(list, pageNumber, itemsPerPage, allItems, noItemsInGridview) {
    var newList = [];
    var pageItemsCounter = list.length;
    var pageItemsCounter = itemsPerPage;
    if (noItemsInGridview != null) {
        pageItemsCounter = noItemsInGridview;
    }
    var counter = 0;

    var newPage = Math.ceil(allItems / pageItemsCounter)
    if (pageNumber > newPage) {
        pageNumber = newPage;
    }

    for (var index = (pageNumber * pageItemsCounter) - pageItemsCounter; index < list.length && counter < pageItemsCounter; index++) {
        var singleObj = {};
        singleObj = list[index];
        singleObj['RowCount'] = (index + 1).toString();

        newList.push(singleObj);
        counter++;
    }
    return newList;
}

function pagingCounter(list, itemsPerPage, noItemsInGridview) {

    var pageItemsCounter = itemsPerPage;
    if (noItemsInGridview != null) {
        pageItemsCounter = noItemsInGridview;
    }

    return Math.ceil((list.length / pageItemsCounter));
}

function loadItemsPerPageDropdown(ddl, result, defaultItems) {
    clearDropdown(ddl);

    $.each(result, function () {
        document.getElementById(ddl).options.add(new Option(this.description, this.value));
    });

    if (defaultItems != null) {
        setPagingDropdownDefaultValue(document.getElementById(ddl), defaultItems);
    }
}


function loadPagingDropdown(ddl, result, defaultValue) {
    clearDropdown(ddl);

    $.each(result, function () {
        document.getElementById(ddl).options.add(new Option(this.description, this.value));
    });

    if (defaultValue != null) {
        setPagingDropdownDefaultValue(document.getElementById(ddl), defaultValue);
    }
}

function setPagingDropdownDefaultValue(ddl, defaultValue, timeoutValue) {

    if (timeoutValue != null) {
        setTimeout(function () { $(ddl).val(checkDropdownValue(ddl, defaultValue)).change() }, timeoutValue);
    }
    else {
        $(ddl).val(checkDropdownValue(ddl, defaultValue));
    }

}

function createPagingList(total) {
    var list = [];
    for (var counter = 1; counter <= total; counter++) {
        var singleObj = {}

        singleObj['description'] = counter.toString();
        singleObj['value'] = counter.toString();

        list.push(singleObj);
    }
    return list;
}

function createItemsPerList(total) {

    var list = [];
    if (total > 50) {
        total = 50;
    }
    else if (total < 10) {
        total = 10;
    }
    else {

        total = (Math.ceil(total / 10)) * 10;
    }



    for (var counter = 10; counter <= total; counter += 10) {
        var singleObj = {}

        singleObj['description'] = counter.toString();
        singleObj['value'] = counter.toString();

        list.push(singleObj);
    }
    return list;
}

function listOfColumnName(thheadid) {
    var ThHead = document.getElementById(thheadid);
    var ListOfTHead = ThHead.getElementsByTagName("th");

    var refObj = [];

    var ctr = 0;
    $.each(ListOfTHead, function () {
        //  refObj.push(this.getAttribute('value').toString().replace(" ", "").replace(" ", "").replace(" ", ""));
        refObj.push(this.getAttribute('value').toString().split(" ").join(""));
        ctr++;
    });
    return refObj;
}


// ITDEVS

function genericPaging(divPagerID, tableName, tableBodyID, list, currentPage, itemsPerPage, allItems, sessionListName, functionName, hideAllWhenEmpty) {

    var noItemsInGridview;
    var genericList = getSessionList(sessionListName);

    var hasNoPager = $("#" + divPagerID).hasClass('noPagerDiv');

    if (genericList.length == 0) {
        showDiv(["divPaging" + divPagerID, divPagerID], false);
        showDiv(["divNoRecordFound" + divPagerID], !hideAllWhenEmpty);
    }
    else if (genericList.length != 0 && hasNoPager) {
        showDiv(["divPaging" + divPagerID, , divPagerID], true);
        showDiv(["divNoRecordFound" + divPagerID], false);
    }
    else {
        showDiv(["divPaging" + divPagerID, , divPagerID], true);
        showDiv(["divNoRecordFound" + divPagerID], false);

        var btnFirstID = "btnFirst" + divPagerID;
        var btnPreviousID = "btnPrev" + divPagerID;
        var btnNextID = "btnNext" + divPagerID;
        var btnLastID = "btnLast" + divPagerID;
        var ddlPagingID = "ddlPaging" + divPagerID;
        var ddlItemsPerPageID = "ddlItemsPerPage" + divPagerID
        var spanPagingID = "spanPaging" + divPagerID
        var spanItemsPerPageID = "spanItemsPerPage" + divPagerID

        var nextIndex = currentPage + 1;
        var prevIndex = currentPage - 1;
        var firstIndex = 1;

        var totalPageCounter = pagingCounter(list, itemsPerPage, noItemsInGridview);
        if (currentPage > totalPageCounter) {
            currentPage = totalPageCounter;
        }

        var onClickEventBtnFirst = "genericPager(" + currentPage + ",\'" + divPagerID + "\',\'" + tableBodyID + "\'," + firstIndex + "\," + itemsPerPage + "\," + allItems + ",\'" + sessionListName + "\',\'" + tableName + "\',\'" + functionName + "\',\'" + hideAllWhenEmpty + "\')";
        var onClickEventBtnPrev = "genericPager(" + currentPage + ",\'" + divPagerID + "\',\'" + tableBodyID + "\'," + prevIndex + "\," + itemsPerPage + "\," + allItems + ",\'" + sessionListName + "\',\'" + tableName + "\',\'" + functionName + "\',\'" + hideAllWhenEmpty + "\')";
        var onClickEventBtnNext = "genericPager(" + currentPage + ",\'" + divPagerID + "\',\'" + tableBodyID + "\'," + nextIndex + "\," + itemsPerPage + "\," + allItems + ",\'" + sessionListName + "\',\'" + tableName + "\',\'" + functionName + "\',\'" + hideAllWhenEmpty + "\')";
        var onClickEventBtnLast = "genericPager(" + currentPage + ",\'" + divPagerID + "\',\'" + tableBodyID + "\'," + totalPageCounter + "\," + itemsPerPage + "\," + allItems + ",\'" + sessionListName + "\',\'" + tableName + "\',\'" + functionName + "\',\'" + hideAllWhenEmpty + "\')";
        var onClickEventDdl = "genericPager(" + currentPage + ",\'" + divPagerID + "\',\'" + tableBodyID + "\',null," + itemsPerPage + "\," + allItems + ",\'" + sessionListName + "\',\'" + tableName + "\',\'" + functionName + "\',\'" + hideAllWhenEmpty + "\')";
        var onClickEventDdlItems = "genericPager(" + currentPage + ",\'" + divPagerID + "\',\'" + tableBodyID + "\',null," + itemsPerPage + "\," + allItems + ",\'" + sessionListName + "\',\'" + tableName + "\',\'" + functionName + "\',\'" + hideAllWhenEmpty + "\')";

        var btnFirst = document.getElementById(btnFirstID);
        var btnPrevious = document.getElementById(btnPreviousID);
        var btnNext = document.getElementById(btnNextID);
        var btnLast = document.getElementById(btnLastID);
        var ddlPaging = document.getElementById(ddlPagingID);
        var spanPaging = document.getElementById(spanPagingID);
        var ddlItemsPerPage = document.getElementById(ddlItemsPerPageID);
        var spanItemsPerPage = document.getElementById(spanItemsPerPageID);

        loadItemsPerPageDropdown(ddlItemsPerPageID, createItemsPerList(allItems), itemsPerPage);

        $(ddlItemsPerPage).attr("onChange", onClickEventDdlItems + ";return false;");

        loadPagingDropdown(ddlPagingID, createPagingList(totalPageCounter), currentPage);
        $(ddlPaging).attr("onChange", onClickEventDdl + ";return false;");

        //spanPaging
        $(spanPaging).empty();
        $(spanPaging).append(totalPageCounter.toString());

        //btnFirst
        if (currentPage > 1) {
            $(btnFirst).attr("onClick", onClickEventBtnFirst + ";return false;");
            $(btnFirst).prop("disabled", false);
        }
        else {
            $(btnFirst).attr("onClick", ";return false;");
            $(btnFirst).prop("disabled", true);
        }

        //btnPrevious
        if (currentPage > 1) {
            $(btnPrevious).attr("onClick", onClickEventBtnPrev + ";return false;");
            $(btnPrevious).prop("disabled", false);
        }
        else {
            $(btnPrevious).attr("onClick", ";return false;");
            $(btnPrevious).prop("disabled", true);
        }

        //btnNext
        if (currentPage < totalPageCounter) {
            $(btnNext).attr("onClick", onClickEventBtnNext + ";return false;");
            $(btnNext).prop("disabled", false);
        }
        else {
            $(btnNext).attr("onClick", ";return false;");
            $(btnNext).prop("disabled", true);
        }

        //btnLast
        if (currentPage < totalPageCounter) {
            $(btnLast).attr("onClick", onClickEventBtnLast + ";return false;");
            $(btnLast).prop("disabled", false);
        }
        else {
            $(btnLast).attr("onClick", ";return false;");
            $(btnLast).prop("disabled", true);
        }
    }


}

function genericPager(currentPage, divPagerID, tableBodyID, index, itemsPerPage, allItems, sessionListName, tableName, functionName, noItemsInGridview) {
    loadingScreen(false, 500);


    if (index == null) {
        var ddlPagingID = "ddlPaging" + divPagerID;
        index = parseInt(getDropdownValue(ddlPagingID));

    }

    var ddlItemsPerPageID = "ddlItemsPerPage" + divPagerID;
    itemsPerPage = parseInt(getDropdownValue(ddlItemsPerPageID));

    if (itemsPerPage > allItems) {
        itemsPerPage = Math.floor((allItems / 10) * 10);
    }
    else {
        itemsPerPage = parseInt(getDropdownValue(ddlItemsPerPageID));
    }

    var genericList = getSessionList(sessionListName);


    $('#' + tableBodyID).empty();
    //$.each(pagePerList(genericList, index, itemsPerPage, allItems), function () {
    createTable(divPagerID, tableName, tableBodyID, genericList, index, itemsPerPage, allItems, sessionListName, functionName);
    //});

    //genericPaging(divPagerID, tableName, tableBodyID, genericList, index, itemsPerPage, allItems, sessionListName, functionName);

    loadingScreen(true, 0);
}

function createPager() {
    $(".pagerDiv").after(function () {
        var btnFirstID = "btnFirst" + this.id;
        var btnPrevID = "btnPrev" + this.id;
        var btnNextID = "btnNext" + this.id;
        var btnLastID = "btnLast" + this.id;
        var ddlPagingID = "ddlPaging" + this.id;
        var ddlItemsPerPageID = "ddlItemsPerPage" + this.id
        var spanPagingID = "spanPaging" + this.id
        var spanItemsPerPageID = "spanItemsPerPage" + this.id

        var spanItemsPerPageHTML = '<div id="divPaging' + this.id + '" class="form-group form-row" hidden>';
        spanItemsPerPageHTML += '<label class="col-form-label label-font-standard" for=\"' + ddlItemsPerPageID + '\">';
        spanItemsPerPageHTML += 'Items per page: ';
        spanItemsPerPageHTML += '</label>';


        var ddlItemsPerPageHTML = '<div class="col-1" style="width: auto">';
        ddlItemsPerPageHTML += '<select id=\"' + ddlItemsPerPageID + '\" class="form-select form-select-sm hardcodedSelect">';
        ddlItemsPerPageHTML += '</select>';
        ddlItemsPerPageHTML += '</div><div class="col-2"></div>';

        var btnFirstHTML = '<div class="col-1" >';
        btnFirstHTML += '<button id=\"' + btnFirstID + '\" onclick="return false;" class="btn btn-lbp-green btnControl " >';
        //btnFirstHTML += '<button id=\"' + btnFirstID + '\" onclick="return false;" class="btn btn-primary btnControl " >';
        btnFirstHTML += '<span class="fa fa-angle-double-left"></span> FIRST';
        btnFirstHTML += '</button>';
        btnFirstHTML += '</div>';

        var btnPrevHTML = '<div class="col-1">';
        btnPrevHTML += '<button id=\"' + btnPrevID + '\" onclick="return false;" class="btn btn-lbp-green btnControl ">';
        //btnPrevHTML += '<button id=\"' + btnPrevID + '\" onclick="return false;" class="btn btn-primary btnControl ">';
        btnPrevHTML += '<span class="fa fa-angle-left"></span> PREV';
        btnPrevHTML += '</button>';
        btnPrevHTML += '</div>';

        var btnNextHTML = '<div class="col-1" >';
        btnNextHTML += '<button id=\"' + btnNextID + '\" onclick="return false;" class="btn btn-lbp-green btnControl">';
        //btnNextHTML += '<button id=\"' + btnNextID + '\" onclick="return false;" class="btn btn-primary btnControl">';
        btnNextHTML += 'NEXT <span class="fa fa-angle-right"></span>';
        btnNextHTML += '</button>';
        btnNextHTML += '</div>';

        var btnLastHTML = '<div class="col-1" >';
        btnLastHTML += '<button id=\"' + btnLastID + '\" onclick="return false;" class="btn btn-lbp-green btnControl ">';
        //btnLastHTML += '<button id=\"' + btnLastID + '\" onclick="return false;" class="btn btn-primary btnControl ">';
        btnLastHTML += 'LAST <span class="fa fa-angle-double-right"></span>';
        btnLastHTML += '</button>';
        //btnLastHTML += '</div></div>';
        btnLastHTML += '</div>';

        var ddlPagingHTML = '<div class="col-2"></div><div class="col-1" style="width:auto;">';
        //ddlPagingHTML += '<label class="col-form-label label-font-standard" > Page </label>';
        ddlPagingHTML += '<select id=\"' + ddlPagingID + '\" class="form-select form-select-sm hardcodedSelect">';
        ddlPagingHTML += '</select>';
        ddlPagingHTML += '</div>';

        var spanPagingHTML = '<label class="col-form-label label-font-standard" style="width:auto;" for=\"' + ddlPagingID + '\">';
        spanPagingHTML += 'of <span id=\"' + spanPagingID + '\"></span>';
        spanPagingHTML += '</label></div> ';

        var divNoRecord = '<div id="divNoRecordFound' + this.id + '" class="form-group form-row" hidden>';
        divNoRecord += '<span runat="server" class="col-12" style="border-color: Black; border-style: solid; text-align: center">';
        divNoRecord += '<strong>No record found </strong></span>';
        divNoRecord += '</div>';

        //return spanItemsPerPageHTML + ddlItemsPerPageHTML + btnFirstHTML + btnPrevHTML + ddlPagingHTML + spanPagingHTML + btnNextHTML + btnLastHTML + divNoRecord;
        return spanItemsPerPageHTML + ddlItemsPerPageHTML + btnFirstHTML + btnPrevHTML + btnNextHTML + btnLastHTML + ddlPagingHTML + spanPagingHTML + divNoRecord;
    });

    $(".noPagerDiv").after(function () {
        var divNoRecord = '<div id="divNoRecordFound' + this.id + '" class="form-group form-row" hidden>';
        divNoRecord += '<span runat="server" class="col-12" style="border-color: Black; border-style: solid; text-align: center">';
        divNoRecord += '<strong>No record found </strong></span>';
        divNoRecord += '</div>';

        return divNoRecord
    });
}

function createRowNumber(index, itemsPerPage, allItems) {
    while (index > (parseInt(allItems / itemsPerPage) + ((allItems % itemsPerPage != 0) ? 1 : 0))) {
        index -= 1;
    }
    return (index - 1) * itemsPerPage;
}


function HighlightEvent(tableName) {
    $("#" + tableName + " tr").each(function () {
        $(this).addClass("highlightClass");
    });
}

function createTable(divName, tableName, tableBody, list, currentPage, itemsPerPage, allItems, sessionListName, functionName, hideAllWhenEmpty, callback) {
    if (list.length > 0) {
        $.when.apply($, pagePerList(list, currentPage, itemsPerPage, allItems).map(function (item) {
            createTableRows(item, divName, tableName, tableBody, list, currentPage, itemsPerPage, allItems, sessionListName, functionName);
        })).then(function () {
            loadingScreen(true, 0);
            genericPaging(divName, tableName, tableBody, list, currentPage, itemsPerPage, allItems, sessionListName, functionName, hideAllWhenEmpty);
            // all ajax calls done now
            if (callback) {
                callback();
            }
        });
    } else {
        genericPaging(divName, tableName, tableBody, list, currentPage, itemsPerPage, allItems, sessionListName, functionName, hideAllWhenEmpty);
    }
}

function createTableRows(result, divName, tableName, tableBody, list, currentPage, itemsPerPage, allItems, sessionListName, functionName) {
    hideError();
    var table = document.getElementById(tableBody);
    var row = table.insertRow(table.rows.length);
    $(row).data('obj', result);
    $('#' + tableName + ' thead tr th').each(function () {
        //data-name
        var name = $(this).data('name');
        var className;
        if (name != undefined) {
            var nameArray = name.split(".");
            name = nameArray[0];
            className = nameArray[1];
        }
        //

        var hasButton = $(this).hasClass('hasButtons');
        var padDate = $(this).hasClass('formatDate');
        var forAdmin = $(this).hasClass('forAdmin');
        var isCurrency = $(this).hasClass('formatCurrency');
        var hasChkBox = $(this).hasClass('chkAll');
        var alignment = $(this).data('alignment');
        var sortable = $(this).hasClass('sortable');
        var columnName = $(this).data('columnname');
        var width = $(this).attr('width');
        var isLink = $(this).hasClass('isLink');
        var orderClass = '';

        //data-pass
        var passParam = $(this).data('pass');
        var passLabel = '';

        //modified to handle multiple pass paramaters, comma-delimited
        if (passParam != undefined) {
            $.each(passParam.split(","), function () {

                if (passLabel != 'undefined' && passLabel != undefined && passLabel != '') {
                    passLabel += ', ';
                }

                passLabel += '\'' + getParameters(result, this) + '\'';
            });
        }
        //

        //data-access
        var accessParam = $(this).data('access');
        var accessValues = [];
        //modified to handle multiple module paramaters, comma-delimited
        if (accessParam != undefined) {
            $.each(accessParam.split(","), function () {
                accessValues.push(getParameters(result, this));
            });
        }
        //

        //data-admin
        var adminParam = $(this).data('admin');
        var isAdmin;
        if (adminParam != undefined) {
            isAdmin = getParameters(result, adminParam);
        }


        //Hide column header if not admin
        if (forAdmin == true) {
            if (isAdmin != 'undefined' && isAdmin != true && isAdmin != 'Y') {
                $(this).remove();
            }

            //$(this).attr("style", "display:" + (isAdmin == true ? "block" : "none"));
        }

        // class-pass
        var classPassParam = $(this).data('class-pass');
        

        if ($(this).children(":first").hasClass("ASC")) {
            orderClass = "ASC";
        } else if ($(this).children(":first").hasClass("DESC")) {
            orderClass = "DESC";
        }
        if (name == "") {
            objRow = row.insertCell();
            if (hasButton == true) {
                var buttons = $(this).data('functions');
                if (buttons != undefined) {
                    var btnIndex = 0;   //To determine which access the button corresponds
                    $.each(buttons.split(','), function (i, item) {
                        var func = item.toString().trim();
                        var accessValue = accessValues[btnIndex];

                        if (accessValues.length == 0 || accessValue == 'Y' || accessValue == true) {
                            //objRow.innerHTML += '<a onclick="' + func + functionName + '(' + passLabel + ')" class="lbpControl gridViewButton ' + func + 'Button ' +
                            //    '"> ' + func[0].toUpperCase() + func.substring(1) + ' </a>';

                            var con = document.createElement('a');
                            con.classList.add('lbpControl', 'gridViewButton', func + 'Button');
                            con.textContent = func[0].toUpperCase() + func.substring(1);
                            con.addEventListener('click', function () {
                                var str = passLabel;
                                var arr = str.split(', ');

                                arr = arr.map(item => item.replace(/^'|'$/g, ''));

                                var functionToCall = func + functionName;
                                window[functionToCall].apply(null, arr);
                                //window[functionToCall](...arr);
                            });

                            objRow.append(con);

                        }

                        btnIndex++;
                    });
                }
            } else if (hasChkBox) {
                var buttons = $(this).data('functions');
                if (buttons != undefined) {
                    $.each(buttons.split(','), function (i, item) {
                        var func = item.toString().trim();
                        objRow.innerHTML += '<input type="checkbox" onclick="' + func + functionName + '(this)" class="center chkAll"/>';
                    });
                }
            } else {
                objRow.innerHTML = "";
            }
            
            if((tableName == 'tblUpdateClientList' || tableName == 'tblSearchClientList') && result.ClientType == 'Potential Account'){
                objRow.setAttribute("style", "background-color: #FF9966; width:" + width + "; text-align: " + alignment + "; ");
            }else{
                objRow.setAttribute("style", "text-align:" + alignment + "; width:" + width + ";");
            }

            if (classPassParam) {
                objRow.setAttribute("class", classPassParam);
            }

        } else {
            objRow = row.insertCell();
            if (hasButton == true) {
                var buttons = $(this).data('functions');
                if (buttons != undefined) {
                    $.each(buttons.split(','), function (i, item) {
                        var func = item.toString().trim();
                        var label;
                        if (className == 'undefined' || className == undefined || className == '') {
                            label = result[name];
                        } else {
                            label = result[className][name];
                        }

                        objRow.innerHTML += '<a class="link-label gridviewhyperlink-font-standard " onclick="' + func + functionName + '(this); return false;"> ' + label + ' </a>';
                        //objRow.innerHTML += '<a onclick="' + func + functionName + '(this); return false;"> ' + label + ' </a>';
                    });
                }
            } else if (hasChkBox) {
                var buttons = $(this).data('functions');
                if (buttons != undefined) {
                    $.each(buttons.split(','), function (i, item) {
                        var func = item.toString().trim();
                        var chkValue = (className == undefined || className == 'undefined' || className == '') ? result[name] : result[className][name];
                        var isChecked = (chkValue == 'Y' ? 'checked' : '')
                        objRow.innerHTML += '<input type="checkbox" ' +
                            'id="chk' + name + '_' + passLabel + '" ' +
                            isChecked +
                            ' onclick="' + func + functionName + '(this)" class="center chkAll"/>';
                    });
                }
            }
            else if (isLink == true) {
                var buttons = $(this).data('functions');
                if (buttons != undefined) {
                    $.each(buttons.split(','), function (i, item) {
                        var func = item.toString().trim();
                        var label;
                        if (className == 'undefined' || className == undefined || className == '') {
                            label = result[name];
                        } else {
                            label = result[className][name];
                        }

                        objRow.innerHTML += '<a class="link-label gridviewhyperlink-font-standard " onclick="' + func + functionName + '(' +passLabel +'); return false;"> ' + label + ' </a>';
                    });
                }
            }

            else {
                objRow.innerHTML = (className == undefined || className == 'undefined' || className == '') ? result[name] : result[className][name];
            }

            if (isCurrency) {
                //Get the currency value if isCurrency
                var currencyVal = Currencify(objRow.innerHTML);
                //Asign previous value if not currency
                objRow.innerHTML = (currencyVal == "" ? objRow.innerHTML : currencyVal);
            }

            if (padDate) {
                //Format date only
                objRow.innerHTML = PadDate(objRow.innerHTML);
            }

            if (sortable) {
                $(this).html(function () {
                    var btn = '<button id="btnTH' + name + '" onclick="sortField(this, \'' + divName + '\',\'' + tableName + '\',\'' + tableBody + '\',\'' + currentPage + '\',\'' + itemsPerPage + '\',\'' + allItems + '\',\'' + sessionListName + '\',\'' + functionName + '\',\'' + className + '\'); return false;" type="submit" value="' + name + '" class="clickableHeader ' + orderClass + '">';
                    btn += '' + columnName + ' <i class="fa fa-sort" style="cursor: pointer;" ></i>';
                    btn += '</button>';
                    return btn;
                });
            }
            objRow.setAttribute("style", "text-align: " + alignment + "; width:" + width + ";");

            

            if (classPassParam) {
                objRow.setAttribute("class", classPassParam);
            }

            //Hide column cell if not admin
            if (forAdmin == true) {
                if (isAdmin != 'undefined' && isAdmin != true && isAdmin != 'Y') {
                    objRow.setAttribute("style", "display:" + (isAdmin == true ? "block" : "none"));
                }
            }
            
            if((tableName == 'tblUpdateClientList' || tableName == 'tblSearchClientList') && result.ClientType == 'Potential Account'){
                      objRow.setAttribute("style", "background-color: #FF9966; width:" + width + "; text-align: " + alignment + "; ");
            }

            if((tableName == 'tblPotentialAccounts' || tableName=='tblNewLeads') && result.ClientType == 'Potential Account'){
                      objRow.setAttribute("style", "background-color: #FF9966; width:" + width + "; text-align: " + alignment + "; ");
            }
            
             if(tableName == 'tbl-AnnuallyReport'){
             var color = result.MonthCode % 2 === 0 ? '#fff' : '#f2f2f2';
              var isBold = result.Month == 'Sub-Totals' ? 'font-weight: bold;' : '';
                      objRow.setAttribute("style", "background-color: " +color + "; width:" + width + "; text-align: " + alignment + "; " +isBold);
            }
             else if (tableName == 'tbl-WeeklyActivity') {
                 var color = result.MonthCode % 2 === 0 ? '#fff' : result.MonthCode != 'Sub-Totals' ? '#f2f2f2': prevColor;
                 var isBold = result.Month == 'Sub-Totals' ? 'font-weight: bold;' : '';
                 objRow.setAttribute("style", "background-color: " + color + "; width:" + width + "; text-align: " + alignment + "; " + isBold);
                 prevColor = color;
             }

              if((tableName == 'tblSummaryBMAnnual') && result.Lead.split(" ").includes("Y") && result.Prospect.split(" ").includes("Y") && result.Customer.split(" ").includes("Y") ){
                      objRow.setAttribute("style", "color: orange; width:" + width + "; text-align: " + alignment + "; ");
            }else if((tableName == 'tblSummaryBMAnnual') && result.Lead.split(" ").includes("Y") && result.Prospect.split(" ").includes("Y") && result.Customer.split(" ").includes("N") ){
                 objRow.setAttribute("style", "color: #1ca500; width:" + width + "; text-align: " + alignment + "; ");
             }
        }

    });
}

function getParameters(result, passParams) {
    var propName;
    var className;
    var value = '';

    var namePassArray = passParams.split(".");
    propName = namePassArray[0];
    className = namePassArray[1];

    if (className == 'undefined' || className == undefined || className == '') {
        value = result[propName];
    } else {
        value = result[className][propName];
    }

    return value
}

function sortField(ctrl, divName, tableName, tableBody, currentPage, itemsPerPage, allItems, sessionListName, functionName, className) {
    loadingScreen(false, 0);
    var order;
    if ($("#" + ctrl.id).hasClass("ASC")) {
        $("#" + ctrl.id).addClass("DESC").removeClass("ASC");
        order = "ASC";
    } else if ($("#" + ctrl.id).hasClass("DESC")) {
        order = "DESC";
        $("#" + ctrl.id).addClass("ASC").removeClass("DESC");
    } else {
        $("#" + ctrl.id).addClass("ASC").removeClass("DESC");
        order = "ASC";
    }
    sortBy = $(ctrl).val();
    var obj = new Object();
    obj = getSessionList(sessionListName);

    PageMethods.sortTable(obj, sortBy, order, className, function (result) {
        var index = 1;
        var itemsPerPage = 10;
        var list = [];
        $("#" + tableBody).empty();
        for (var r = 0; r < result.length; r++) {
            list.push(result[r]);
        }
        sessionStorage[sessionListName] = JSON.stringify(list);
        allItems = result.length;

        if (list.length > 0) {
            createTable(divName, tableName, tableBody, list, index, itemsPerPage, allItems, sessionListName, functionName)
        }
    },
    function onError(err) {
        alert(err.get_message());
        loadingScreen(true, 0);
    });
}
//PAGER END
