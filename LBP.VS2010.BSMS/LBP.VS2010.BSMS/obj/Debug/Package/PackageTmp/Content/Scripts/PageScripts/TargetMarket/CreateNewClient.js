$(document).ready(function () {
    initLoad();
});

function initLoad(){
    hideError();
    loadingScreen(false, 0);

    var deferred = $.Deferred();

    PageMethods.OnLoadValues(
        function OnSuccess(result) {
            deferred.resolve(result);
        },
        function OnError(err) {
            deferred.reject(err.get_message());
    });


        $.when(deferred).done(async function (data) {
        try {
            var currentUser = data.currentUser;
            var ClientTypeList = data.ClientTypeList;
            var LeadSourceList = data.LeadSourceList;
            var IndustryTypeList = data.IndustryTypeList;
            var ReasonList = data.ReasonList;
            var CPANames = data.CPANames;
            var CASAProductList = data.CASAProductList;
            var LoanProductList = data.LoanProductList;
            var OtherProductList = data.OtherProductList;
            var LoanProductListDDL = data.LoanProductListDDL;
            var DaysBefore = data.DaysBefore;

            //var isAdmn = ['3', '4', '5'].includes(currentUser.RoleID);
            //var isBM = ['8'].includes(currentUser.RoleID);
            //var isGH = ['6', '7'].includes(currentUser.RoleID);

            toggleEventForElement('#ddlClientType', 'change', getClientType, true);
            toggleEventForElement('#chkLostCustomer', 'click', getLost, true);
            toggleEventForElement('#btnAccountInfo', 'click', AJAXWrapPageMethodCall, true, 'initModal');
            toggleEventForElement('#btnAdd', 'click', AJAXWrapPageMethodCall, true, 'validateClient');
            toggleEventForElement('#btnCancel', 'click', AJAXWrapPageMethodCall, true, 'pageBack');
            toggleEventForElement('#txtLead', 'change', changeStatusTextBox, true);
            toggleEventForElement('#txtSuspect', 'change', changeStatusTextBox, true);
            toggleEventForElement('#txtProspect', 'change', changeStatusTextBox, true);
            toggleEventForElement('#txtCustomer', 'change', changeStatusTextBox, true);
            toggleEventForElement('#chkLead', 'change', changeStatusCheckBox, true);
            toggleEventForElement('#chkSuspect', 'change', changeStatusCheckBox, true);
            toggleEventForElement('#chkProspect', 'change', changeStatusCheckBox, true);
            toggleEventForElement('#chkCustomer', 'change', changeStatusCheckBox, true);
            toggleEventForElement('#ddlReason', 'change', setDDLReason, true);

            loadDropdown('ddlClientType', ClientTypeList, true, 'Individual');
            loadDropdown('ddlSource', LeadSourceList, true, '1');
            loadDropdown('ddlIndustryType', IndustryTypeList, true, 'Acctg., Auditing & Bookkeeping Services');
            loadDropdown('ddlReason', ReasonList, true);
            setDaysBefore(DaysBefore);

            enableControl('ddlReason', false);
            $("#divAccountInfo").hide();

            appendCheckbox(CASAProductList, "divCASAProducts", "CASA");
            appendCheckbox(LoanProductList, "divLoanProducts","Loan");
            appendTableCheckbox(OtherProductList, "tBodyProducts", "Other", "other", "")
            appendCheckbox(CASAProductList, "modalCASAProducts","ModalCASA");
            loadDropdown('ddlModalLoanProducts', LoanProductListDDL, true);
            appendTableCheckbox(OtherProductList, "tBodyModalProducts", "ModalOther", "modalOther", "Modal");

            setTextboxValue('txtAmount', '0');
            setTextboxValue('txtLoanReported', '0');
            setTextboxValue('txtADB', '0');
            setTextboxValue('txtFeedback', '');
            createTextAreaCounter();

            secureStorage.setItem("CPANames", CPANames);

        } catch (e) {
            showErrorModal(e);
        }

        loadingScreen(true, 0);
    }).fail(function (e) {
        showErrorModal(e);
        loadingScreen(true, 0);
    });
}

function appendCheckbox(result, divName, chkboxName){
    for (var x = 0; x < result.length; x++) {
        $("#"+divName).append("<div class='col-sm-3 form-check form-check-inline'> <input type='checkbox' class='form-check-input' name='chk"+chkboxName+"' value='"+result[x].CASACodes +"' id='"+chkboxName+ result[x].CASACodes +"'/>" + 
            " <label class='form-check-label label-font-standard' for='"+chkboxName + result[x].CASACodes +"'> "+ result[x].CASAShortName +" </label> </div>");
    }
}

function appendTableCheckbox(result, tbody, chkboxName, trID, ID){

    for (var x = 0; x < result.length; x++) {

        if(result[x].ProductGroupNo == "1"){
            appendTableEmptyTR(tbody, trID+"_"+result[x].CODE);
            appendTableEmptyTD(trID+"_"+result[x].CODE, "a"+ID+ result[x].CODE);
            appendTableEmptyTD(trID+"_"+result[x].CODE, "b"+ID+ result[x].CODE);
            appendTableEmptyTD(trID+"_"+result[x].CODE, "c"+ID+ result[x].CODE);
            appendTableEmptyTD(trID+"_"+result[x].CODE, "d"+ID+ result[x].CODE);
            appendTableCheckboxTD(result, "a"+ID+ result[x].CODE, chkboxName, ID, x);

        }else if(result[x].ProductGroupNo == "2"){

            appendTableRow(result, tbody, trID, ID, x);
            appendTableCheckboxTD(result, "b"+ID+ result[x].CODE, chkboxName, ID, x);

        }else if(result[x].ProductGroupNo == "3"){

            appendTableRow(result, tbody, trID, ID, x);
            appendTableCheckboxTD(result, "c"+ID+ result[x].CODE, chkboxName, ID, x);

        }else if(result[x].ProductGroupNo == "4"){

            appendTableRow(result, tbody, trID, ID, x);
            appendTableCheckboxTD(result, "d"+ID+ result[x].CODE, chkboxName, ID, x);
        }
    }
}

function appendTableRow(result, tbody, trID, ID, x){

    if(parseInt(result[x].CODE) > $('#'+tbody).children('tr').length ){
        appendTableEmptyTR(tbody, trID+"_"+result[x].CODE);
        appendTableEmptyTD(trID+"_"+result[x].CODE, "a"+ID+ result[x].CODE);
        appendTableEmptyTD(trID+"_"+result[x].CODE, "b"+ID+ result[x].CODE);
        appendTableEmptyTD(trID+"_"+result[x].CODE, "c"+ID+ result[x].CODE);
        appendTableEmptyTD(trID+"_"+result[x].CODE, "d"+ID+ result[x].CODE);
    }
}

function appendTableCheckboxTD(result, tdID, chkboxName, ID, x){

    $("#"+tdID).append("<input type='checkbox' class='form-check-input' name='chk"+chkboxName+"' value='"+ result[x].OtherProductCode +"' id='chk"+ID+ result[x].OtherProductCode +"' style='margin-top:0.3%'/>" + 
            " <label class='form-check-label label-font-standard' for='chk" +ID+ result[x].OtherProductCode +"'> "+ result[x].OtherShortName +" </label>");
}

function appendTableEmptyTR(tbody, trID){
    $("#"+tbody).append("<tr id='"+trID+"'></tr>");
}

function appendTableEmptyTD(trID, tdID){
    $("#"+trID).append("<td id='"+ tdID +"' style='text-align: center; width: 15%;'></td>");
}

function getChkBoxModal(element){
    var value = element.value;
    var checked = element.checked;
    disableDivControl(value, !checked);

    if(!checked){

        if(value==="divModalLoanSelect"){
            clear_form_elements_input("divModalLoan");
            clear_form_elements_currency("divModalLoan");
            enableControl('txtModalLoanAmt', false);
            enableControl('txtModalPN', false);
            disableDivControl('divModalLoan', true);
            var loans = getTableTR_IDs('tbodyModalLoanPN');
		    for(var x = 0; x < loans.length; x++){
			    deletePN(loans[x])
            }
		
        }else{
            clear_form_elements_modal(value);
            clear_form_elements_input(value);
            clear_form_elements_currency(value);
        }
        
    }

}


function setCheckBox(id, checked){
    $('#' + id).prop('checked', checked);
}

function setCurrentDate(id){
    var curDate = new Date().toLocaleDateString('en-US', {
        month: '2-digit',day: '2-digit',year: 'numeric'});
    setTextboxValue(id, curDate);
}

function changeStatusTextBox(element){
    var oldID = element.id;
    var newID = element.id.replace('txt', 'chk');
    var chkbox = document.getElementById(newID)

    if(getTextboxValue(oldID) !== '' ){
        changeStatusCheckBox(chkbox, true);
    }
}

function changeStatusCheckBox(element, checked){
    var value = element.value;
    var id;

    if(checked===undefined){
        checked = element.checked;
    }

    var chkbox = $("input[name='chkStatus']:checkbox");

    if(checked){
        for(var x = 0; x<value; x++){
            id = chkbox[x].id;
            $('#' + id).prop('checked', checked);

            $("#" + id.replace('chk', 'txt')).removeClass("noForwardDate1Week");

            if(getTextboxValue(id.replace('chk', 'txt')) === '' ){
                var selectedDate = chkbox[value-1].id.replace('chk', 'txt');

                if(getTextboxValue(selectedDate) !== '' ){
                    setTextboxValue(id.replace('chk', 'txt'), getTextboxValue(selectedDate));
                }else{
                    setCurrentDate(id.replace('chk', 'txt'));
                }
            }else{
            
                //var maxDate = new Date(getTextboxValue(id.replace('chk', 'txt')));
                //maxDate = maxDate.setDate(maxDate.getDate() + 7);
                var minDate = new Date(getTextboxValue(id.replace('chk', 'txt')));
                //minDate = minDate.setDate(minDate.getDate() - 7);

                $(".noForwardDate1Week").datepicker('option', { minDate: new Date(minDate)});

            }

            if(x === 2){
                setTextboxValue('txtVisits', '1');
            }else if(x === 3){
                $("#divAccountInfo").show();
            }

        }
    }else{
        for(var x = value-1; x<chkbox.length; x++){
            id = chkbox[x].id;
            $('#' + id).prop('checked', checked);

            $("#" + id.replace('chk', 'txt')).addClass("noForwardDate1Week");

            if(getTextboxValue(id.replace('chk', 'txt')) !== '' ){
                setTextboxValue(id.replace('chk', 'txt'), '');

                if(x > 0){
                    if(getTextboxValue(chkbox[x-1].id.replace('chk', 'txt'))){
                        var minDate = new Date(getTextboxValue(chkbox[x-1].id.replace('chk', 'txt')));
                        $(".noForwardDate1Week").datepicker('option', { minDate: new Date(minDate)});
                    }
                }
            }

            if(x === 2){
                setTextboxValue('txtVisits', '0');
            }else if(x === 3){
                $("#divAccountInfo").hide();
            }
        }
    }
    
    if($("input[name='chkStatus']:checkbox:checked").length === 4){
        enableControl('chkLostCustomer', false);
    }else{
        enableControl('chkLostCustomer', true);
    }
}

function getClientType(element){

    var value = element.value;

    if(value === "Individual"){
        $("#lblFName").text("First Name");
        $('#divName .child').remove();
        $('#divMI .child').remove();
        $("#divName").append("<input type='text' id='txtFname' class='lbpControl form-control form-control-sm required requiredField child name' " +
            "autocomplete='off' maxlength='30' />");
        $("#lblMiddleInitial").text("Middle Initial");
        $("#divMI").append("<input type='text' id='txtMname' class='lbpControl form-control form-control-sm child name' " +
            "autocomplete='off' maxlength='2' />");
        $("#lblLName").text("Last Name");
        setTextboxValue("txtLname","");
        enableControl('txtLname', true);
        toggleRequiredConditional("#ddlIndustryType", false);

    }else if(value === "CPA"){
        $("#lblFName").text("Potential Account");
        $('#divName .child').remove();
        $("#lblMiddleInitial").text("");
        $('#divMI .child').remove();
        $("#divName").append("<select id='ddlCPANames' class='lbpControl form-select form-select-sm hardcodedSelect child' style='background-color:coral'></select>");
        $("#lblLName").text("Name");

        var result = secureStorage.getItem("CPANames");

        toggleEventForElement('#ddlCPANames', 'change', onChangeCPA, true);
        //stoggleEventForElement('#ddlCPANames', 'change', setIndustry, true);
        loadDropdown('ddlCPANames', result, true, result[0].value);
        enableControl('txtLname', false);
        toggleRequiredConditional("#ddlIndustryType", true);

    }else{
        $("#lblFName").text("");
        $('#divName .child').remove();
        $("#lblMiddleInitial").text("");
        $('#divMI .child').remove();
        $("#lblLName").text("Name");
        setTextboxValue("txtLname","");
        enableControl('txtLname', true);
        toggleRequiredConditional("#ddlIndustryType", false);
    }
}


function getCPAID(){
    var value = getDropdownValue("ddlCPANames");
    var newValue = value.substring(0,4) + '-' + value.substring(4);

    return newValue;
}

function onChangeCPA(element){
    //var value = element.value;
    var value = getCPAID();

    setTextboxValue("txtLname", getDropdownText("ddlCPANames"));

    PageMethods.GetCPADetails(value,
        function OnSuccess(result) {
            //deferred.resolve(result);
            setDropdownDefaultValue("ddlIndustryType", result.trim());
        },
        function OnError(err) {
            //deferred.reject(err.get_message());
    });
}

function appendTableAccNo(){
    var columnNames = ["Account Number", "ADB (Month to Date)"];
    var arrValues = [];
    var accNo = getTextboxValue("txtModalAccount");
    arrValues.push(accNo);
    arrValues.push("0.00");

    appendTable("divModalCASAAccounts", "tblModalCASAAccounts", "tbodyModalCASAAccounts", "theadModalCASAAccounts", accNo, columnNames, arrValues);
}

function appendTablePN(){
    var columnNames = ["Loan Product Type", "Date Reported", "Original Loan Amount", "Loan Amount Reported", "P.N. Number", "Date Released", "Released Loan Amount"];
    var arrValues = [];
    var PN = getTextboxValue("txtModalPN");
    arrValues.push(getDropdownText("ddlModalLoanProducts"));
    arrValues.push(getTextboxValue("txtModalDateReported"));
    arrValues.push(getTextboxValue("txtModalOrigAmt"));
    arrValues.push(getTextboxValue("txtModalLoanAmt"));
    arrValues.push(PN);
    arrValues.push("");
    arrValues.push("");
    
    appendTable("divModalLoanPN", "tblModalLoanPN", "tbodyModalLoanPN", "theadModalLoanPN", getDropdownValue("ddlModalLoanProducts"), columnNames, arrValues);
}

function appendTable(divName, tableName, tbodyName, thID, trID, columnNames, arrValues){
    
    if($('#'+divName+' > table').length == 0){

        $("#"+divName).append("<table id='"+tableName+"' class='table table-sm scrollTable' align='center'></table>");
        $("#"+tableName).append("<thead class='noScrollFixedHeader'><tr id='"+thID+"' valign='middle' align='center'></tr></thead>");
        $("#"+tableName).append("<tbody id='"+tbodyName+"' class='scrollContent border align-middle' style='position: relative; overflow: auto; overflow-x: hidden;'></tbody>");

        for(var x = 0; x<columnNames.length; x++){
        
            if(x===0){
                $("#"+thID).append("<th align='center hasButtons' valign='middle' width='17.5%' data-alignment='center' ></th>");
                $("#"+thID).append("<th align='center' valign='middle' width='15%' data-name='"+columnNames[x]+"' data-alignment='center' data-columnname='"+columnNames[x]+"'>"+columnNames[x]+"</th>");
            }else{
                $("#"+thID).append("<th align='center' valign='middle' width='15%' data-name='"+columnNames[x]+"' data-alignment='center' data-columnname='"+columnNames[x]+"'>"+columnNames[x]+"</th>");
            }
        }
    }

    addNewRow(tbodyName, trID, arrValues);
}

function addNewRow(tbodyName, trID ,arrValues){
    var functionName = '';
    if($('#'+trID).length == 0){

        $("#"+tbodyName).append("<tr id='"+trID+"'></tr>");
    
        for(var x = 0; x < arrValues.length; x++){

            if(x===0){

                if(tbodyName === "tbodyModalCASAAccounts"){
                    functionName = "AccNo";
                }else{
                    functionName = "PN";
                }

                $("#"+trID).append("<td style='text-align:center; width:17.5%;'><a onclick='delete"+functionName+"(this.id)' class='lbpControl gridViewButton btn btn-danger btn-sm' style='color: white;' id='"+trID+"'><span><i class='fa fa-trash-alt'></i></span><strong> Delete </strong></a></td>");
                $("#"+trID).append("<td style='text-align: center; width: 15%;'><a href='javascript:void(0)' onclick='set"+functionName+"(this.id)' id='"+trID+"'>"+arrValues[x]+"</a></td>");
            }else{
                $("#"+trID).append("<td style='text-align: center; width: 15%;'>"+arrValues[x]+"</td>");
            }
        }
    }
}

function removeRow(tableName, tbodyName, trID){
    $(document.getElementById(trID)).remove();
    if($("#"+tbodyName+" > tr").length == 0){
        $(document.getElementById(tableName)).remove();
    }
}

function clear_form_elements(id) {
    $("#"+id).find(':input').each(function() {
        switch(this.type) {
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


function clear_form_elements_modal(id) {
    $("#"+id).find(':input').each(function() {
        switch(this.type) {
            case 'checkbox':
            case 'radio':
                this.checked = false;
                break;
        }
    });
}

function clear_form_elements_currency(id) {
    $("#"+id).find(':input.currency').each(function() {
        $(this).val('0.00');
    });
}

function clear_form_elements_input(id) {
    $("#"+id).find(':input').each(function() {
        $(this).val('');
    });
}


function initModal(){
    hideSpecificError('lblModalErrorMessage');

    enableCheckboxDiv("chkModalCASA", "divModalCASA");
    enableCheckboxDiv("chkModalLoan", "divModalLoan");
    enableCheckboxDiv("chkModalProducts", "divModalOther");
    initModalButtons(true);

}

function initModalButtons(enabled){
    toggleEventForElement('#btnAddAccNo', 'click', AJAXWrapPageMethodCall, enabled, 'validateCASA');
    toggleEventForElement('#btnAddPN', 'click', AJAXWrapPageMethodCall, enabled, 'validateLoan');
    toggleEventForElement('#btnModalCancel', 'click', AJAXWrapPageMethodCall, enabled, 'confirmCancel');
    toggleEventForElement('#btnModalClose', 'click', AJAXWrapPageMethodCall, enabled, 'confirmCancel');
    toggleEventForElement('#chkModalCASA', 'click', getChkBoxModal, enabled);
    toggleEventForElement('#chkModalLoan', 'click', getChkBoxModal, enabled);
    toggleEventForElement('#chkModalProducts', 'click', getChkBoxModal, enabled);
    toggleEventForElement('#ddlModalLoanProducts', 'change', getModalProducts, enabled);
    toggleEventForElement('#btnModalSubmit', 'click', AJAXWrapPageMethodCall, enabled, 'validateAccountInfo');
}

function confirmCancel() {
    hideSpecificError('lblModalErrorMessage');
    showConfirmModal("discard changes for", cancelModal);
}

function cancelModal(){
    $('#accountInfoModal').modal('hide');
    clear_form_elements("divAccInfoContainer");
    clear_form_elements_currency("divAccInfoContainer");
    $(document.getElementById("tblModalCASAAccounts")).remove();
    $(document.getElementById("tblModalLoanPN")).remove();
    initModalButtons(false);
    setTextboxValue('txtAmount', "0.00");
    setTextboxValue('txtADB', "0.00");
    setTextboxValue('txtLoanReported', "0.00");
}

function getModalProducts(){
    var value = getDropdownValue("ddlModalLoanProducts");
    

    if(value===undefined){
        disableDivControl("divModalLoan", true);
        disableDivControl("divModalLoanSelect", false);
        clear_form_elements_input("divModalLoan");
        clear_form_elements_currency("divModalLoan");
        toggleRequiredConditional("#txtModalLoanAmt", false);
    }else{
        setCurrentDate("txtModalDateReported");
        disableDivControl("divModalLoan", false);
        toggleRequiredConditional("#txtModalLoanAmt", true);

//        if(value==="L7" || value==="L8" || value==="L9"){
//            enableControl('txtModalOrigAmt', true);
//        }else{
//            enableControl('txtModalOrigAmt', false);
//        }
        enableControl('txtModalOrigAmt', false);
        enableControl('txtModalDateReported', false);
    }
}

function getLost(element){
    var checked = element.checked;
    if(checked){
        disableDivControl("divLostFields", false);
        setCurrentDate('txtDateLost');
        $("#txtReason").addClass("requiredField");
    }else{
        disableDivControl("divLostFields", true);
        clear_form_elements("divLostFields");
        $("#txtReason").addClass("requiredField");
    }

}


function validateClient() {
    loadingScreen(false, 0);

    var fName = '';
    var mName = '';
    if(getDropdownValue('ddlClientType') === 'Individual'){
        fName = getTextboxValue('txtFname');
        mName = getTextboxValue('txtMname');
    }
   
    var CPA_id = null
    if(getDropdownValue('ddlClientType') === 'CPA'){
        CPA_id = getCPAID();
    }

    var accNum = getTableTR_IDs('tbodyModalCASAAccounts');

    //var origAmt = getSumColumn(getColumnValues('tbodyModalLoanPN', 3));

    var loanReported = getColumnValues('tbodyModalLoanPN', 4);
    var PNNumber = getColumnValues('tbodyModalLoanPN', 5);

    var selectedProducts = getCheckboxListValues('divCASAProducts').length + 
                           getCheckboxListValues('tBodyProducts').length +
                           getCheckboxListValues('divLoanProducts').length


    var reason = getDropdownValue('ddlReason');
    if(reason===''){
        reason = getTextboxValue('txtReason') !== '' ? getTextboxValue('txtReason') : null;
    }


    var postData = {
        LName: getTextboxValue('txtLname'),
        MName: mName,
        FName: fName,
        ClientType: getDropdownValue('ddlClientType') !== '' ? getDropdownValue('ddlClientType') : null,
        Address: getTextboxValue('txtAddress') !== '' ? getTextboxValue('txtAddress') : null,
        ContactNo: getTextboxValue('txtContactNo') !== '' ? getTextboxValue('txtContactNo') : null,
        Lead: getTextboxValue('txtLead') !== '' ? getTextboxValue('txtLead') : null,
        Suspect: getTextboxValue('txtSuspect') !== '' ? getTextboxValue('txtSuspect') : null,
        Prospect: getTextboxValue('txtProspect') !== '' ? getTextboxValue('txtProspect') : null,
        Customer: getTextboxValue('txtCustomer') !== '' ? getTextboxValue('txtCustomer') : null,
        CASATypes: $("#chkModalCASA").is(":checked") === true ? getCheckboxListValues('modalCASAProducts').toString() : null,
        Amount: getTextboxValue('txtAmount') !== '' ? getTextboxValue('txtAmount') : '0.00',
        AmountOthers: getTextboxValue('txtModalDepositOther') !== '' ? getTextboxValue('txtModalDepositOther') : '0.00', 
        ADB: getTextboxValue('txtADB') !== '' ? getTextboxValue('txtADB') : '0.00',
        Lost: getTextboxValue('txtDateLost') !== '' ? getTextboxValue('txtDateLost') : null,
        Reason: reason,
        OtherATypes: $("#chkModalProducts").is(":checked") === true ? getCheckboxListValues('tBodyModalProducts').toString() : null, 
        Remarks: getTextboxValue('txtFeedback') !== '' ? getTextboxValue('txtFeedback') : null,
        DateEncoded: new Date().toLocaleDateString('en-US'),
        Visits: getTextboxValue('txtVisits'),
        AccountNumbers: accNum.toString() !== '' ? accNum.toString()  : null, 
        LeadSource: getDropdownValue('ddlSource') !== '' ? getDropdownValue('ddlSource') : null,
        IndustryType: getDropdownValue('ddlIndustryType') !== '' ? getDropdownValue('ddlIndustryType') : null,
        ProductsOffered: getProductsOffered() !== '' ? getProductsOffered() : null,
        LoanReported: getTextboxValue('txtLoanReported') !== '' ? getTextboxValue('txtLoanReported') : '0.00',
        LoansAvailed: getLoansAvailedModal() !== '' ? getLoansAvailedModal() : null,
        SelectedProducts: selectedProducts,
        CPAID: CPA_id,
        //LoansAvailedModal: getLoansAvailedModal(),
        LoanAmountReportedArr: loanReported.toString(),
        PNNumberArr : PNNumber.toString()
    };

    PageMethods.ValidateClient(postData,
    function (result) {
        if (result == '') {
            showConfirmModal("add", addClient, null, postData);
        }
        else{
            showErrorModal(result);
            //showSpecificError(result, 'lblUserErrorMessage', 'divUserError');
        }
        loadingScreen(true, 0);
    },
    function (err) {
        showErrorModal(err.get_message());
        loadingScreen(true, 0);
    });
}

function addClient(obj) {
    loadingScreen(false, 0);

    PageMethods.AddClient(obj,
    function (result) {
        if (result == '') {
            showSuccessModal('added', closeModal, reset);
        }
        else{
            showErrorModal(result);
            //showSpecificError(result, 'lblUserErrorMessage', 'divUserError');
        }
        loadingScreen(true, 0);
    },
    function (err) {
        showErrorModal(err.get_message());
        loadingScreen(true, 0);
    });
}

function reset(){
    loadingScreen(false, 0);
    window.location.href = "/Views/TargetMarket/CreateNewClient.aspx";
}

function getColumnValues(tbody, columnNumber){
    var columnValues = [];
    var rows = $('#'+tbody + ' > tr');
    for(var x = 0; x<rows.length ; x++){
        columnValues.push(rows[x].getElementsByTagName('td')[columnNumber].innerHTML.replace(/,/g,''));

    }

    return columnValues;
}

function setDDLReason(element){
    var value = element.value;
    if(value===''){
        toggleRequiredConditional("#txtReason", true);
        $("#txtReason").show();
    }else{
        toggleRequiredConditional("#txtReason", false);
        $("#txtReason").hide();
        setTextboxValue('txtReason', "");
    }
}

function deleteAccNo(accNo){
    removeRow("tblModalCASAAccounts", "tbodyModalCASAAccounts",  accNo);
}

function deletePN(PN){
    removeRow("tblModalLoanPN", "tbodyModalLoanPN",  PN);
}

function validateAccountInfo() {
    loadingScreen(false, 0);

    var accNum = getTableTR_IDs('tbodyModalCASAAccounts');

    //var loanProductsAvailed = getColumnValuesTR('tbodyModalLoanPN', 1);
    var loanProductsAvailed = getTableTR_IDs('tbodyModalLoanPN');

    var selectedProducts = getCheckboxListValues('modalCASAProducts').length + 
                           loanProductsAvailed.length +
                           getCheckboxListValues('divModalProducts').length


    var postData = {
        CASAType: $("#chkModalCASA").is(":checked"),
        CASATypesAvailed: getCheckboxListValues('modalCASAProducts').toString(),
        InitialCASADeposits: getTextboxValue('txtModalDeposit'),
        AccountNumber: accNum.toString(), 
        LoanType: $("#chkModalLoan").is(":checked"),
        LoanProductsAvailed: loanProductsAvailed.toString(),
        ProductsServices: $("#chkModalProducts").is(":checked"),
        ProductsServicesAvailed: getCheckboxListValues('divModalProducts').toString(),
        InitialCASADeposits2: getTextboxValue('txtModalDepositOther')!== '0.00' ? getTextboxValue('txtModalDepositOther') : '',
        SelectedProducts: selectedProducts
    };

    PageMethods.ValidateAccountInfo(postData,
    function (result) {
        if (result == '') {
            closeModal();
            var ADB = getSumColumn(getColumnValues('tbodyModalCASAAccounts', 2));
            var loanReported = getSumColumn(getColumnValues('tbodyModalLoanPN', 4));
            var amount = getTextboxValue('txtModalDeposit');

            setModalAmounts(ADB, loanReported, amount)
        }
        else{
            showErrorModal(result);
            //showSpecificError(result, 'lblUserErrorMessage', 'divUserError');
        }
        loadingScreen(true, 0);
    },
    function (err) {
        showErrorModal(err.get_message());
        loadingScreen(true, 0);
    });
}

function setModalAmounts(ADB, loanReported, amount){
    setTextboxValue('txtADB', ADB.toString());
    setTextboxValue('txtLoanReported', loanReported.toString());
    setTextboxValue('txtAmount', amount.toString());
}

function closeModal(callbackOK) {
    $('#accountInfoModal').modal('hide');

    if (callbackOK) {
        if (typeof callbackOK != "object") {
            callbackOK();
        }
    }
}

function getSumColumn(arr){
    var sum = new bigDecimal('0.00');

    for(var x = 0; x < arr.length; x++){
        //sum = sum + parseFloat(arr[x].replace(/,/g,''));
        var amount = new bigDecimal(arr[x].replace(/,/g,''))
        sum = sum.add(amount);
    }

//    if(isNaN(sum)){
//        sum.setvalue('0.00');
//    }

    return sum.getValue();
}


function validateCASA() {
    loadingScreen(false, 0);

    var postData = {
        CASAType: $("#chkModalCASA").is(":checked"),
        CASATypesAvailed: getCheckboxListValues('modalCASAProducts').toString(),
        InitialCASADeposits: getTextboxValue('txtModalDeposit'),
        AccountNumber: getTextboxValue('txtModalAccount')
    };


    PageMethods.ValidateCASA(postData,
    function (result) {
        if (result == '') {
            appendTableAccNo();

        }
        else{
            showErrorModal(result);
            //showSpecificError(result, 'lblUserErrorMessage', 'divUserError');
        }
        loadingScreen(true, 0);
    },
    function (err) {
        showErrorModal(err.get_message());
        loadingScreen(true, 0);
    });
}

function validateLoan() {
    loadingScreen(false, 0);

    var loans = getTableTR_IDs('tbodyModalLoanPN').toString();

    var postData = {
        LoanType: $("#chkModalLoan").is(":checked"),
        LoanProductsAvailed: getDropdownValue('ddlModalLoanProducts'),
        LoanProductSelected: loans,
        OrigLoanAmount: getTextboxValue('txtModalOrigAmt'),
        LoanAmountReported: getTextboxValue('txtModalLoanAmt'),
        PNNumber: getTextboxValue('txtModalPN')
    };

    PageMethods.ValidateLoan(postData,
    function (result) {
        if (result == '') {
            appendTablePN()
        }
        else{
            showErrorModal(result);
            //showSpecificError(result, 'lblUserErrorMessage', 'divUserError');
        }
        loadingScreen(true, 0);
    },
    function (err) {
        showErrorModal(err.get_message());
        loadingScreen(true, 0);
    });
}

function enableCheckboxDiv(checkbox, divName){
    if($("#"+checkbox).is(":checked")){
        disableDivControl(divName, false);
    }else{
        disableDivControl(divName, true);
    }
}

function setAccNo(AccNo){
    loadingScreen(false, 0);

    var tr = $("#"+AccNo);
    setTextboxValue('txtModalAccount', tr[0].getElementsByTagName('td')[1].innerText.trim());

    loadingScreen(true, 0);
    return false;
}


function setPN(PN){
    loadingScreen(false, 0);

    var tr = $("#"+PN);

    setDropdownDefaultValue("ddlModalLoanProducts", PN, tr[0].getElementsByTagName('td')[1].innerText.trim());

    setTextboxValue('txtModalDateReported', tr[0].getElementsByTagName('td')[2].innerHTML);
    setTextboxValue('txtModalOrigAmt', tr[0].getElementsByTagName('td')[3].innerHTML);
    setTextboxValue('txtModalLoanAmt', tr[0].getElementsByTagName('td')[4].innerHTML);
    setTextboxValue('txtModalPN', tr[0].getElementsByTagName('td')[5].innerHTML);

    loadingScreen(true, 0);

    return false;
}

//function getOtherATypes(){
//    var otherATypes = "";

//    if($("#chkModalCASA").is(":checked")){
//        otherATypes = otherATypes + getCheckboxListValues('modalCASAProducts').toString();
//    }

//    if($("#chkModalLoan").is(":checked")){
//        if(otherATypes!==''){
//            otherATypes = otherATypes + ','
//        }
//        otherATypes = otherATypes + getTableTR_IDs('tbodyModalLoanPN').toString();
//    }
//    
//    if($("#chkModalProducts").is(":checked")){
//        if(otherATypes!==''){
//            otherATypes = otherATypes + ','
//        }
//        otherATypes = otherATypes + getCheckboxListValues('tBodyModalProducts').toString();
//    }

//    return otherATypes;
//}

function getProductsOffered(){
    var productsOffered = "";

        productsOffered = productsOffered + getCheckboxListValues('divCASAProducts').toString();

        if(productsOffered!==''){
            productsOffered = productsOffered + ','
        }

        productsOffered = productsOffered + getCheckboxListValues('divLoanProducts').toString();

        if(productsOffered!==''){
            productsOffered = productsOffered + ','
        }
  
        productsOffered = productsOffered + getCheckboxListValues('tBodyProducts').toString();

    return productsOffered;
}

function getLoansAvailedModal(){
    var loansAvailedModal = "";

    if($("#chkModalLoan").is(":checked")){
        loansAvailedModal = loansAvailedModal + getTableTR_IDs('tbodyModalLoanPN').toString();
    }

    return loansAvailedModal;
}

function getTableTR_IDs(tbody){
    var loans = [];
    var tr = $("#"+tbody+" > tr");

    for(var x = 0; x < tr.length; x++){
        loans.push(tr[x].getAttribute('id'));
    }
    return loans;
}

function setDaysBefore(daysBefore){
    $(".noForwardDate1Week").datepicker({
        showAnim: "",
        changeMonth: true,
        changeYear: true,
        yearRange: '1901:+0',
        dateFormat: "mm/dd/yy",
        maxDate: '0',
        minDate:'-' + (parseInt(daysBefore) - 1) + 'd',
        onChangeMonthYear: function (year, month, day) {
            yearRange: '1901:+0',
                $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
        }
    });
}

function pageBack(){
    window.history.back();
}

