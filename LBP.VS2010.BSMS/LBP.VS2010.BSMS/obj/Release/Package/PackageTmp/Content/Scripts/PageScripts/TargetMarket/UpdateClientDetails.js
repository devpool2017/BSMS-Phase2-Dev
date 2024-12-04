var deletedAccNum  = [], updatedAccNum  = [], addedAccNum = [];
var deletedPN = [], updatedPN = [], addedPN = [];
var visitIncrement = 0;
var firstRunModal = 'Y';

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
            var CASAProductList = data.CASAProductList;
            var LoanProductList = data.LoanProductList;
            var OtherProductList = data.OtherProductList;
            var LoanProductListDDL = data.LoanProductListDDL;
            var ClientDetails = data.ClientDetails;
            var AccountNumberList = data.AccountNumberList;
            var LoansAvailedList = data.LoansAvailedList;
            var OtherParameters = data.OtherParameters;
            var canView = data.CanView;
            var canUpdate = data.CanUpdate;
            var isBM = data.isBM;

            createTextAreaCounter();

            toggleEventForElement('#ddlClientType', 'change', getClientType, true);
            toggleEventForElement('#chkLostCustomer', 'change', getLost, true);
            toggleEventForElement('#txtLead', 'change', changeStatusTextBox, true);
            toggleEventForElement('#txtSuspect', 'change', changeStatusTextBox, true);
            toggleEventForElement('#txtProspect', 'change', changeStatusTextBox, true);
            toggleEventForElement('#txtCustomer', 'change', changeStatusTextBox, true);
            toggleEventForElement('#chkLead', 'change', changeStatusCheckBox, true);
            toggleEventForElement('#chkSuspect', 'change', changeStatusCheckBox, true);
            toggleEventForElement('#chkProspect', 'change', changeStatusCheckBox, true);
            toggleEventForElement('#chkCustomer', 'change', changeStatusCheckBox, true);
            toggleEventForElement('#ddlReason', 'change', setDDLReason, true);
            toggleEventForElement('#chkVisited', 'change', setVisited, true);

            loadDropdown('ddlClientType', ClientTypeList, true, 'Individual');
            loadDropdown('ddlSource', LeadSourceList, true, '1');
            if (IndustryTypeList.length > 0) IndustryTypeList.splice(0, 0, { description: '', value: '' });
            loadDropdown('ddlIndustryType', IndustryTypeList, true);
            loadDropdown('ddlReason', ReasonList, true);
            setDaysBefore(OtherParameters.DaysBefore);

            enableControl('ddlReason', false);
            $("#divAccountInfo").hide();

            appendCheckbox(CASAProductList, "divCASAProducts", "CASA");
            appendCheckbox(LoanProductList, "divLoanProducts","Loan");
            appendTableCheckbox(OtherProductList, "tBodyProducts", "Other", "other", "")
            appendCheckbox(CASAProductList, "modalCASAProducts","ModalCASA");
            loadDropdown('ddlModalLoanProducts', LoanProductListDDL, true);
            appendTableCheckbox(OtherProductList, "tBodyModalProducts", "ModalOther", "modalOther", "Modal");

            secureStorage.setItem("clientDetails", ClientDetails);
            secureStorage.setItem("accountNumberList", AccountNumberList);
            secureStorage.setItem("loansAvailedList", LoansAvailedList);
            secureStorage.setItem("customerDaysBefore", setCustomerDaysBefore(OtherParameters.CustomerDaysBefore, ClientDetails.Customer));
            visitIncrement = OtherParameters.EditVisitTag === 'Y' ? 1 : 0;
            setClientDetails(ClientDetails, AccountNumberList, LoansAvailedList);
            initControlsUpdate(isBM, canView, canUpdate);

            var curDate = getCurrentDate();
            if(curDate === ClientDetails.Prospect){
                disableControls(['chkVisited', 'txtVisits'], true);
            }
            
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
    setRequired_AccInfo(value, checked);

    if(!checked){
        removeRequiredModal();

        if(value==="divModalCASA"){
            var acc = getTableTR_IDs('tbodyModalCASAAccounts');
		    for(var x = 0; x < acc.length; x++){
			    deleteAccNo(acc[x])
            }
            clear_form_elements_modal(value);
            clear_form_elements_input(value);
            clear_form_elements_currency(value);

        }else if(value==="divModalLoanSelect"){
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
        addRequiredModal();
    }

}

function setRequired_AccInfo(value, required){

    if(value === 'divModalCASA'){
        toggleRequiredTable('#modalCASAProducts', required);
        toggleRequiredConditional('#txtModalAccount', required);
        setTextboxValue('txtModalDeposit', '0.00');
    }else if(value === 'divModalLoanSelect'){
        toggleRequiredConditional('#ddlModalLoanProducts', required);
        setTextboxValue('txtModalLoanAmt', '0.00');
    }else if(value === 'divModalOther'){
        toggleRequiredTable('#tblModalProducts', required);
        setTextboxValue('txtModalDepositOther', '0.00');
    }

}

function setRequired_Currency(element){
    var value = element.value;
    var id = element.id;

    if((parseInt(value) === 0) || (value.trim() === '')){
        $('#' + id).addClass("required");
        $('#' + id).addClass("requiredField");
    }else{
        $('#' + id).removeClass("required");
        $('#' + id).removeClass("requiredField");
    }
}

function checkRequiredSelected_Modal(element){
    var id = element.id;
    var checked = getCheckboxListValues(id).length;

    if(checked > 0){
        toggleRequiredTable('#' + id, false);
    }else{
        toggleRequiredTable('#' + id, true);
    }
    
}

function addRequiredModal(){
    $("#divAccInfoContainer").find(":input[name='chkModal']").each(function() {
        $(this).addClass("required");
        $(this).addClass("requiredField");
    });
}

function removeRequiredModal(){
    $("#divAccInfoContainer").find(":input[name='chkModal']").each(function() {
        $(this).removeClass("required");
        $(this).removeClass("requiredField");
    });
}

function setCheckBox(id, checked){
    $('#' + id).prop('checked', checked);
}

function getCurrentDate(){
    var curDate = new Date().toLocaleDateString('en-US', {
        month: '2-digit',day: '2-digit',year: 'numeric'});
    return curDate;
}

function setCurrentDate(id){
    var curDate = getCurrentDate();
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
            
                var minDate = new Date(getTextboxValue(id.replace('chk', 'txt')));
                $(".noForwardDate1Week").datepicker('option', { minDate: new Date(minDate)});

            }

            if(x === 2){
                if (!($("#chkVisited").is(":checked"))){
                    if(!($("#chkVisited").is(":disabled"))){
                        if(!($("#chkProspect").is(":disabled"))){
                            $('#chkVisited').prop('checked', checked);
                            setTextboxValue('txtVisits', (parseInt(getTextboxValue('txtVisits')) + visitIncrement).toString());
                        }
                    }
                }
            }else if(x === 3){
                $("#divAccountInfo").show();

                if(($("#chkCustomer").is(":checked")) && ($("#chkProspect").is(":disabled"))){
                    disableControls(['chkVisited', 'txtVisits'], true);
                }
                
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
                if ($("#chkVisited").is(":checked")){
                    var visited = parseInt(getTextboxValue('txtVisits')) - visitIncrement;
                    $('#chkVisited').prop('checked', checked);
                    if(visited<0){
                        setTextboxValue('txtVisits', '0');
                    }else{
                        setTextboxValue('txtVisits', visited.toString());
                    }
                }
            }else if(x === 3){
                $("#divAccountInfo").hide();

                var curDate = getCurrentDate();
                if(curDate !== getTextboxValue("txtProspect")){
                    if((!($("#chkCustomer").is(":checked"))) && ($("#chkProspect").is(":disabled"))){
                        disableControls(['chkVisited', 'txtVisits'], false);
                    }
                }
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

//    }
//      else if(value === "CPA"){
//        $("#lblFName").text("CPA Name");
//        $('#divName .child').remove();
//        $("#lblMiddleInitial").text("");
//        $('#divMI .child').remove();
//        //$("#divName").append("<select id='ddlCPANames' class='lbpControl form-select form-select-sm hardcodedSelect child' style='background-color:coral'></select>");
//        $("#lblLName").text("Name");

////        PageMethods.GetCPANames(
////        function OnSuccess(result) {
////            if(result !== null){
////                toggleEventForElement('#ddlCPANames', 'change', setName, true);
////                loadDropdown('ddlCPANames', result, true, result[0].value);
////                enableControl('txtLname', false);
////            }else{
////                
////            }
////        },
////        function OnError(err) {
////            //deferreds.CPANameslist.reject(err.get_message());
////        });

    }else{
        $("#lblFName").text("");
        $('#divName .child').remove();
        $("#lblMiddleInitial").text("");
        $('#divMI .child').remove();
        $("#lblLName").text("Name");
        setTextboxValue("txtLname","");
        enableControl('txtLname', true);
    }
}

function setName(){
    setTextboxValue("txtLname", getDropdownText("ddlCPANames"));
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
    var columnNames = ["Loan Product Type", "Date Reported", "Loan Amount Availed", "Desired Loan Amount", "P.N. Number", "Date Released", "Released Loan Amount"];
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
    $("#"+id).find(':input:text').each(function() {
        $(this).val('');
    });
}

function initModal(){
    hideSpecificError('lblModalErrorMessage');

    toggleRequiredConditional('#ddlModalLoanProducts', true);
    toggleRequiredConditional('#txtModalAccount', true);

    if(firstRunModal === 'Y'){
        enableCheckboxDiv("chkModalCASA", "divModalCASA");
        enableCheckboxDiv("chkModalLoan", "divModalLoan");
        enableCheckboxDiv("chkModalProducts", "divModalOther");
        firstRunModal = 'N';
    }

    initModalButtons(true);
    disableSelectedOtherATypes();

    if(getDropdownValue('ddlModalLoanProducts') !== ''){
        setPN(getDropdownValue('ddlModalLoanProducts'));
    }

    if(!($("#chkModalCASA").is(":checked"))){
        disableDivControl("divModalCASA", true);
    }

    if(!($("#chkModalLoan").is(":checked"))){
        disableDivControl("divModalLoan", true);
    }

    if(!($("#chkModalProducts").is(":checked"))){
        disableDivControl("divModalOther", true);
    }
}

function initModalButtons(enabled){
    toggleEventForElement('#btnAddAccNo', 'click', AJAXWrapPageMethodCall, enabled, 'validateCASA');
    toggleEventForElement('#btnAddPN', 'click', AJAXWrapPageMethodCall, enabled, 'validateLoan');
    toggleEventForElement('#btnModalCancel', 'click', AJAXWrapPageMethodCall, enabled, 'confirmCancel');
    toggleEventForElement('#btnModalClose', 'click', AJAXWrapPageMethodCall, enabled, 'confirmCancel');
    toggleEventForElement('#chkModalCASA', 'change', getChkBoxModal, enabled);
    toggleEventForElement('#chkModalLoan', 'change', getChkBoxModal, enabled);
    toggleEventForElement('#chkModalProducts', 'change', getChkBoxModal, enabled);
    toggleEventForElement('#ddlModalLoanProducts', 'change', getModalProducts, enabled);
    toggleEventForElement('#btnModalSubmit', 'click', AJAXWrapPageMethodCall, enabled, 'validateAccountInfo');
    toggleEventForElement('#txtModalDeposit', 'change', setRequired_Currency, enabled);
    toggleEventForElement('#modalCASAProducts', 'click', checkRequiredSelected_Modal, enabled);
    toggleEventForElement('#txtModalLoanAmt', 'change', setRequired_Currency, enabled);
    toggleEventForElement('#txtModalDepositOther', 'change', setRequired_Currency, enabled);
    toggleEventForElement('#tblModalProducts', 'click', checkRequiredSelected_Modal, enabled);
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

    $("#divAccInfoContainer").find(":input[name='chkModal']").each(function() {
        $(this).prop('checked', false);
    });
    

    clientDetails = secureStorage.getItem("clientDetails");
    setTextboxValue('txtAmount', clientDetails.Amount);
    setTextboxValue('txtLoanReported', clientDetails.LoanReported);
    setTextboxValue('txtADB', clientDetails.ADB);

    setTextboxValue('txtModalDeposit', clientDetails.Amount);
    setTextboxValue('txtModalDepositOther', clientDetails.AmountOthers);

    setSelectedCheckBox('tBodyModalProducts', clientDetails.OtherATypes);
    setSelectedCheckBox('modalCASAProducts', clientDetails.CASATypes); 

    if($("#modalCASAProducts input:checked").length > 0){
        setCheckBox("chkModalCASA", true);
    }
    setSelectedCheckBox('tBodyModalProducts', clientDetails.OtherATypes);
    if($("#tBodyModalProducts input:checked").length > 0){
        setCheckBox("chkModalProducts", true);
    }

    accountNumberList = secureStorage.getItem("accountNumberList");
    loansAvailedList = secureStorage.getItem("loansAvailedList");

    setAccoutNumbers(accountNumberList);
    setModalLoans(loansAvailedList);

    addRequiredModal();
    toggleRequiredTable('#modalCASAProducts', false);
    toggleRequiredTable('#tblModalProducts', false);
    firstRunModal = 'Y';
}

function getModalProducts(){
    var value = getDropdownValue("ddlModalLoanProducts");

    if((value==="") || (value===undefined)){
        disableDivControl("divModalLoan", true);
        disableDivControl("divModalLoanSelect", false);
        clear_form_elements_input("divModalLoan");
        clear_form_elements_currency("divModalLoan");
        //toggleRequiredConditional("#txtModalLoanAmt", false);
    }else{
        setCurrentDate("txtModalDateReported");
        disableDivControl("divModalLoan", false);
        //toggleRequiredConditional("#txtModalLoanAmt", true);

//        if(value==="L7" || value==="L8" || value==="L9"){
//            enableControl('txtModalOrigAmt', true);
//        }else{
//            enableControl('txtModalOrigAmt', false);
//        }
    }
            enableControl('txtModalOrigAmt', false);
            enableControl('txtModalDateReported', false);
}

function getLost(element){
    var checked = element.checked;

    var clientDetails = secureStorage.getItem("clientDetails");

    if(checked){
        disableDivControl("divLostFields", false);
        //disableControls(['chkCustomer', 'txtCustomer'], true);
        $("#collapseOne").find(":input[name='chkStatus']").each(function() {
            //if (!$(this).is(":checked")) {

                changeStatusCheckBox(document.getElementById('chkLead'), false);
                disableControls([this.id, this.id.replace('chk', 'txt')], true);

                setTextboxValue('txtLead', clientDetails.Lead);
                setTextboxValue('txtSuspect', clientDetails.Suspect);
                setTextboxValue('txtProspect', clientDetails.Prospect);
                //setTextboxValue('txtCustomer', clientDetails.Customer);
                setTextboxValue('txtVisits', clientDetails.Visits);

            //}
        });
        setCurrentDate('txtDateLost');
        $("#txtReason").addClass("required");
        $("#txtReason").addClass("requiredField");
        disableControls(['chkVisited', 'txtVisits'], true);
    }else{
        disableDivControl("divLostFields", true);
        //disableControls(['chkCustomer', 'txtCustomer'], false);
        $("#collapseOne").find(":input[name='chkStatus']").each(function() {
            //if (!$(this).is(":checked")) {
            if(getTextboxValue(this.id.replace('chk', 'txt')) === ''){
                disableControls([this.id, this.id.replace('chk', 'txt')], false);
            }
            //}
        });
        clear_form_elements("divLostFields");

        var curDate = getCurrentDate();
        if(curDate === clientDetails.Prospect){
            disableControls(['chkVisited', 'txtVisits'], true);
        }else{
            disableControls(['chkVisited', 'txtVisits'], false);
        }
    }

}


function IfLost_Init(){

    if($("#chkLostCustomer").is(":checked")){
        $("#collapseOne").find(":input[name='chkStatus']").each(function() {
            disableControls([this.id, this.id.replace('chk', 'txt')], true);
            disableControls(['chkVisited', 'txtVisits', 'chkLostCustomer'], true);
        });
    }
}

function validateClient() {
    loadingScreen(false, 0);

//    var fName = null;
//    var mName = null;
//    if(getDropdownValue('ddlClientType') === 'Individual'){
//        fName = getTextboxValue('txtFname');
//        mName = getTextboxValue('txtMname');
//    }
   
//    var CPA_id = null
//    if(getDropdownValue('ddlClientType') === 'CPA'){
//        CPA_id = getDropdownValue('ddlCPANames');
//    }

    var accNum = getTableTR_IDs('tbodyModalCASAAccounts');

//    var origAmt = getSumColumn(getColumnValues('tbodyModalLoanPN', 3));

    var loanReported = getColumnValues('tbodyModalLoanPN', 4);
    var PNNumber = getColumnValues('tbodyModalLoanPN', 5);

    var selectedProducts = getCheckboxListValues('divCASAProducts').length + 
                           getCheckboxListValues('tBodyProducts').length +
                           getCheckboxListValues('divLoanProducts').length


    var reason = getDropdownValue('ddlReason');
    if(reason===''){
        reason = getTextboxValue('txtReason') !== '' ? getTextboxValue('txtReason') : null;
    }

    var clientDetails = secureStorage.getItem("clientDetails");
    var prospectDate = '';
    if (($("#chkVisited").is(":checked")) && (clientDetails.Prospect !== '')){
        prospectDate = getCurrentDate();
    }else{
        prospectDate = getTextboxValue('txtProspect') !== '' ? getTextboxValue('txtProspect') : null;
    } 

    var postData = {
        LName: 'UPDATE',
        //MName: mName,
        //FName: fName,
        //ClientType: getDropdownValue('ddlClientType'),
        Address: getTextboxValue('txtAddress') !== '' ? getTextboxValue('txtAddress') : null,
        ContactNo: getTextboxValue('txtContactNo') !== '' ? getTextboxValue('txtContactNo') : null,
        Lead: getTextboxValue('txtLead') !== '' ? getTextboxValue('txtLead') : null,
        Suspect: getTextboxValue('txtSuspect') !== '' ? getTextboxValue('txtSuspect') : null,
        Prospect: prospectDate,
        Customer: getTextboxValue('txtCustomer') !== '' ? getTextboxValue('txtCustomer') : null,
        CASATypes: $('#chkCustomer').is(":checked") === true ? getCheckboxListValues('modalCASAProducts').toString() : null,
        Amount: $('#chkCustomer').is(":checked") === true ? getTextboxValue('txtAmount') : '0.00',
        AmountOthers: $('#chkCustomer').is(":checked") === true ? getTextboxValue('txtModalDepositOther') : '0.00', 
        ADB: $('#chkCustomer').is(":checked") === true ? getTextboxValue('txtADB') : '0.00',
        Lost: getTextboxValue('txtDateLost') !== '' ? getTextboxValue('txtDateLost') : null,
        Reason: reason,
        OtherATypes: $('#chkCustomer').is(":checked") === true ? getCheckboxListValues('tBodyModalProducts').toString() : null, 
        Remarks: getTextboxValue('txtFeedback') !== '' ? getTextboxValue('txtFeedback') : null,
        DateEncoded: new Date().toLocaleDateString('en-US'),
        Visits: getTextboxValue('txtVisits'),
        AccountNumbers: $('#chkCustomer').is(":checked") === true ? accNum.toString() : null, 
        LeadSource: getDropdownValue('ddlSource') !== '' ? getDropdownValue('ddlSource') : null,
        IndustryType: getDropdownValue('ddlIndustryType') !== '' ? getDropdownValue('ddlIndustryType') : null,
        ProductsOffered: getProductsOffered() !== '' ? getProductsOffered() : null,
        LoanReported: $('#chkCustomer').is(":checked") === true ? getTextboxValue('txtLoanReported') : '0.00',
        LoansAvailed: $('#chkCustomer').is(":checked") === true ? getLoansAvailedModal() : null,
        SelectedProducts: selectedProducts,
        //CPAID: CPA_id,
        //LoansAvailedModal: $('#chkCustomer').is(":checked") === true ? getLoansAvailedModal() : '',
        LoanAmountReportedArr: $('#chkCustomer').is(":checked") === true ? loanReported.toString() : '',
        PNNumberArr : $('#chkCustomer').is(":checked") === true ? PNNumber.toString() : '',
        DeletedAccountNumber: $('#chkCustomer').is(":checked") === true ? deletedAccNum.toString() : accNum.toString(),
        DeletedLoansAvailed: $('#chkCustomer').is(":checked") === true ? deletedPN.toString() : getLoansAvailedModal(),
        AddedAccountNumber: $('#chkCustomer').is(":checked") === true ? addedAccNum.toString() : '',
        AddedLoansAvailed: $('#chkCustomer').is(":checked") === true ? addedPN.toString() : ''
    };

    PageMethods.ValidateClient(postData,
    function (result) {
        if (result == '') {
            showConfirmModal("update", updateClient, null, postData);
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

function updateClient(obj) {
    loadingScreen(false, 0);

    PageMethods.UpdateClient(obj,
    function (result) {
        if (result == '') {
            showSuccessModal('updated', closeModal, reset);
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
    secureStorage.setItem("prevURL", document.referrer.toString());
    window.location.href = "/Views/TargetMarket/UpdateClient.aspx";
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
    deletedAccNum.push(accNo);
}

function deletePN(PN){
    removeRow("tblModalLoanPN", "tbodyModalLoanPN",  PN);
    deletedPN.push(PN);
}

function validateAccountInfo() {
    loadingScreen(false, 0);

    var accNum = getTableTR_IDs('tbodyModalCASAAccounts');

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
        InitialCASADeposits: getTextboxValue('txtModalDeposit')!== '0.00' ? getTextboxValue('txtModalDeposit') : '',
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
//        sum = 0.00;
//    }

    return sum.getValue();
}


function validateCASA() {
    loadingScreen(false, 0);

    var accNum = getTableTR_IDs('tbodyModalCASAAccounts').toString();

    var postData = {
        CASAType: $("#chkModalCASA").is(":checked"),
        CASATypesAvailed: getCheckboxListValues('modalCASAProducts').toString(),
        InitialCASADeposits: getTextboxValue('txtModalDeposit')!== '0.00' ? getTextboxValue('txtModalDeposit') : '',
        AccountNumber: getTextboxValue('txtModalAccount'),
        AccountNumberAdded: accNum
    };

    PageMethods.ValidateCASA(postData,
    function (result) {
        if (result == '') {
            if(!(getTableTR_IDs('tbodyModalCASAAccounts').includes(postData.AccountNumber))){
                appendTableAccNo();
                addedAccNum.push(postData.AccountNumber);
            }else{
                showErrorModal("Account Number '" + postData.AccountNumber + "' was already added!");
            }
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
    var loansDate = getColumnValues('tbodyModalLoanPN', 2).toString();

    var postData = {
        LoanType: $("#chkModalLoan").is(":checked"),
        LoanProductsAvailed: getDropdownValue('ddlModalLoanProducts'),
        LoanProductSelected: loans,
        OrigLoanAmount: getTextboxValue('txtModalOrigAmt'),
        LoanAmountReported: getTextboxValue('txtModalLoanAmt') !== '0.00' ? getTextboxValue('txtModalLoanAmt') : '',
        PNNumber: getTextboxValue('txtModalPN'),
        LoanProductSelectedDate: loansDate,
        LoanDateReported: getTextboxValue('txtModalDateReported')
    };

    PageMethods.ValidateLoan(postData,
    function (result) {
        if (result == '') {
            if(!(getTableTR_IDs('tbodyModalLoanPN').includes(postData.LoanProductsAvailed))){
                appendTablePN()
                addedPN.push(postData.LoanProductsAvailed);
            }else{
                showErrorModal("Loan Product Availed '" + getDropdownText('ddlModalLoanProducts') + "' was already added!");
            }
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

        if((productsOffered!=='') || (productsOffered.charAt(productsOffered.length - 1) !== ',')){
            productsOffered = productsOffered + ','
        }

        productsOffered = productsOffered + getCheckboxListValues('divLoanProducts').toString();

        if((productsOffered!=='') || (productsOffered.charAt(productsOffered.length - 1) !== ',')){
            productsOffered = productsOffered + ','
        }
  
        productsOffered = productsOffered + getCheckboxListValues('tBodyProducts').toString();


        if(productsOffered===',,'){
            productsOffered = '';
        }

    return productsOffered;
}
function getTableTR_IDs(tbody){
    var ids = [];
    var tr = $("#"+tbody+" > tr");

    for(var x = 0; x < tr.length; x++){
        ids.push(tr[x].getAttribute('id'));
    }
    return ids;
}

function setClientDetails(clientDetails, accountNumberList, loansAvailedList){
    if(clientDetails.errMsg === ''){
        setDropdownDefaultValue('ddlClientType', clientDetails.ClientType);
        setTextboxValue('txtFname', clientDetails.FName);
        setTextboxValue('txtMname', clientDetails.MName);
        setTextboxValue('txtLname', clientDetails.LName);
        setDropdownDefaultValue('ddlSource', clientDetails.LeadSource);
        setTextboxValue('txtAddress', clientDetails.Address);
        setTextboxValue('txtContactNo', clientDetails.ContactNo);
        setTextboxValue('txtAmount', clientDetails.Amount);
        setTextboxValue('txtLoanReported', clientDetails.LoanReported);
        setDropdownDefaultValue('ddlSource', clientDetails.LeadSource);
        setDropdownDefaultValue('ddlIndustryType', clientDetails.IndustryType);

        setTextboxValue('txtLead', clientDetails.Lead);
        setTextboxValue('txtSuspect', clientDetails.Suspect);
        setTextboxValue('txtProspect', clientDetails.Prospect);
        setTextboxValue('txtCustomer', clientDetails.Customer);
        setTextboxValue('txtVisits', clientDetails.Visits);

        setTextboxValue('txtADB', clientDetails.ADB);

        if(clientDetails.Lost !== ''){
            setCheckBox('chkLostCustomer', true);
            setTextboxValue('txtDateLost', clientDetails.Lost);
        }


        $("#txtChat").append(clientDetails.Remarks);

        setTextboxValue('txtModalDeposit', clientDetails.Amount);
        setTextboxValue('txtModalDepositOther', clientDetails.AmountOthers);

//        setSelectedCheckBox('divCASAProducts', clientDetails.CASATypes);

//        setSelectedCheckBox('divLoanProducts', clientDetails.LoansAvailed);
//        setSelectedCheckBox('tBodyProducts', clientDetails.ProductsOffered);

        setSelectedCheckBox('divCASAProducts', clientDetails.ProductsOffered);
        setSelectedCheckBox('divLoanProducts', clientDetails.ProductsOffered);
        setSelectedCheckBox('tBodyProducts', clientDetails.ProductsOffered);

        setAccoutNumbers(accountNumberList);
        setModalLoans(loansAvailedList);

//        setSelectedCheckBox('modalCASAProducts', clientDetails.OtherATypes); 

        setSelectedCheckBox('modalCASAProducts', clientDetails.CASATypes); 
        if($("#modalCASAProducts input:checked").length > 0){
            setCheckBox("chkModalCASA", true);
            enableControl('chkModalCASA', false);
        }
//        setSelectedCheckBox('tBodyModalProducts', clientDetails.OtherATypes);

        setSelectedCheckBox('tBodyModalProducts', clientDetails.OtherATypes);
        if($("#tBodyModalProducts input:checked").length > 0){
            setCheckBox("chkModalProducts", true);
            enableControl('chkModalProducts', false);
        }

        $('#chkVisited').prop('checked', false);
    }
}

function setSelectedCheckBox(id, values){
    var arrValues = values.split(",");
    for(var x = 0; x < arrValues.length; x++){
        $("#"+ id +" :input[value='" + arrValues[x] + "']").prop('checked', true);
    }
}

function setAccoutNumbers(accountNumberList){
    for(var x = 0; x < accountNumberList.length; x++){
        appendTableAccNo_OnLoad(accountNumberList[x].AccountNumber, accountNumberList[x].ADB);
    }
}

function setModalLoans(loansAvailedList){
    for(var x = 0; x < loansAvailedList.length; x++){
        appendTablePN_OnLoad(loansAvailedList[x].LoanCodes, loansAvailedList[x].LoanDateReported, "0.00", loansAvailedList[x].LoanAmountReported, loansAvailedList[x].PNNumber);
        setCheckBox("chkModalLoan", true);
        enableControl('chkModalLoan', false);
        
    }
}

function appendTableAccNo_OnLoad(accnum, adb){
    var columnNames = ["Account Number", "ADB (Month to Date)"];
    var arrValues = [];

    arrValues.push(accnum.trim());
    arrValues.push(adb);

    appendTable("divModalCASAAccounts", "tblModalCASAAccounts", "tbodyModalCASAAccounts", "theadModalCASAAccounts", accnum.trim(), columnNames, arrValues);
 }

function appendTablePN_OnLoad(loanProduct, date, origAmt, LoanAmt, PN){
    var columnNames = ["Loan Product Type", "Date Reported", "Original Loan Amount", "Loan Amount Reported", "P.N. Number", "Date Released", "Released Loan Amount"];
    var arrValues = [];

    setDropdownDefaultValue('ddlModalLoanProducts', loanProduct);
    arrValues.push(getDropdownText("ddlModalLoanProducts"));
    arrValues.push(date);
    arrValues.push(parseFloat(origAmt).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
    arrValues.push(parseFloat(LoanAmt).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
    arrValues.push(PN);
    //arrValues.push("");
    arrValues.push("");
    arrValues.push("");
    
    appendTable("divModalLoanPN", "tblModalLoanPN", "tbodyModalLoanPN", "theadModalLoanPN", loanProduct, columnNames, arrValues);
}

function initControlsUpdate(isBM, canView, canUpdate){
    var customerDaysBefore = secureStorage.getItem("customerDaysBefore");

    if(isBM){

        enableControl('txtCustomer', customerDaysBefore);
        enableControl('chkCustomer', customerDaysBefore);
        
        toggleEventForElement('#btnAccountInfo', 'click', AJAXWrapPageMethodCall, true, 'initModal');
        toggleEventForElement('#btnUpdate', 'click', AJAXWrapPageMethodCall, true, 'validateClient');

        toggleEventForElement('#btnEditIndustryType', 'click', AJAXWrapPageMethodCall, true, 'editIndustryType');
        toggleEventForElement('#btnRevertIndustryType', 'click', AJAXWrapPageMethodCall, true, 'revertIndustryType');

        disableDivControlFields('collapseOne', true);
        enableControl('txtFeedback', true);
        enableControl('ddlSource', true);
        enableControl('txtAddress', true);
        enableControl('txtContactNo', true);

        if(getTextboxValue('txtSuspect') == ''){
            enableControl('txtSuspect', true);
            enableControl('chkSuspect', true);
        }

        if(getTextboxValue('txtProspect')== ''){
            enableControl('txtProspect', true);
            enableControl('chkProspect', true);
        }

        if(getTextboxValue('txtCustomer') == ''){
            enableControl('txtVisits', true);
            enableControl('txtCustomer', true);
            enableControl('chkCustomer', true);
            disableDivControlFields('divCASAProducts', false);
            disableDivControlFields('divLoanProducts', false);
            disableDivControlFields('tBodyProducts', false);
            disableDivControlFields('divLost', false);
            enableControl('chkVisited', true);
        }

        if(customerDaysBefore){
        enableControl('txtCustomer', customerDaysBefore);
        enableControl('chkCustomer', customerDaysBefore);
        }
        
        disableDivControlFields('ddlSource', false);

//        $("#collapseOne input:checked").each(function(){
//            $(this).prop('disabled', true);
//        });
    
    }else{

        disableDivControlFields('collapseOne', true);
        disableDivControlFields('divAccInfoContainer', true);
        hideSpecificError('lblModalErrorMessage');

        $("#divAccInfoContainer .btn").each(function(){
            $(this).remove();
        });

        $("#divAccInfoContainer a").each(function(){
            $(this).prop("onclick", null).off("click");;
        });

        $("#theadModalCASAAccounts th:first-child").remove();
        $("#tbodyModalCASAAccounts td:first-child").remove();
        $("#tblModalLoanPN th:first-child").remove();
        $("#tblModalLoanPN td:first-child").remove();
        $("#btnEditIndustryType").remove();
        $("#btnModalSubmit").remove();

        toggleEventForElement('#btnUpdate', 'click', AJAXWrapPageMethodCall, true, 'validateClient');
        toggleEventForElement('#btnModalCancel', 'click', AJAXWrapPageMethodCall, true, 'closeModal');
        toggleEventForElement('#btnModalClose', 'click', AJAXWrapPageMethodCall, true, 'closeModal');

        if(canView && canUpdate){
            enableControl('txtFeedback', true);
            enableControl('btnUpdate', true);
        }else if(canView && !canUpdate){
            enableControl('txtFeedback', false);
            $("#btnUpdate").remove();
        }else{
            pageBack();
        }
    }
    setTextboxValue('txtFeedback', '');
    toggleRequiredConditional("#txtFeedback", true);
    toggleEventForElement('#btnCancel', 'click', AJAXWrapPageMethodCall, true, 'pageBack');

    IfLost_Init();
}

function editIndustryType(){
    disableDivControlFields('divIndustryType', false);
    showDiv(['btnEditIndustryType'], false);
    showDiv(['btnRevertIndustryType'], true);
}

function revertIndustryType(){
    disableDivControl('divIndustryType', true);
    showDiv(['btnEditIndustryType'], true);
    showDiv(['btnRevertIndustryType'], false);
    clientDetails = secureStorage.getItem("clientDetails");
    setDropdownDefaultValue('ddlIndustryType', clientDetails.IndustryType);
    //if (clientDetails.IndustryType !== '') {
    //    setDropdownDefaultValue('ddlIndustryType', clientDetails.IndustryType);
    //    /*setTextboxValue('ddlIndustryType', '**-' + clientDetails.IndustryType);*/
    //}else{
    //    setTextboxValue('ddlIndustryType', '');
    //}
}

function pageBack(){
    secureStorage.setItem("prevURL", document.referrer.toString());
    window.history.back();
}

function disableSelectedOtherATypes(){
var customerDaysBefore = secureStorage.getItem("customerDaysBefore");

    $("#divAccInfoContainer input:checked").each(function(){
        $(this).prop('disabled', !customerDaysBefore);
    });

    $("#divAccInfoContainer input:not(:checked)").each(function(){
        $(this).prop('disabled', false);
    });
}

function getLoansAvailedModal(){
    var loansAvailedModal = "";

    if($("#chkModalLoan").is(":checked")){
        loansAvailedModal = loansAvailedModal + getTableTR_IDs('tbodyModalLoanPN').toString();
    }

    return loansAvailedModal;
}

function setVisited(element){
    var checked = element.checked;
    var visited = parseInt(getTextboxValue('txtVisits'));
    //var visitIncrement = secureStorage.getItem("VisitIncrement");

    if(checked){
        visited = visited + visitIncrement;
    }else{
        visited = visited - visitIncrement;
    }

    if(visited < 0){
        visited = 0;
    }

    setTextboxValue('txtVisits', visited.toString());
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

function setCustomerDaysBefore(customerDaysBefore, customerDate){
    var newCustomerDaysBefore = new Date();
    var newCustomerDate = new Date();
    var currentDate = new Date();
    var bol = false;

    if((customerDate !== "")){
        newCustomerDaysBefore = new Date(newCustomerDaysBefore.setDate(newCustomerDaysBefore.getDate() - parseInt(customerDaysBefore)));
        newCustomerDate = new Date(customerDate);
    }
    
    if((newCustomerDate >= newCustomerDaysBefore) && (newCustomerDate <= currentDate)){
        bol = true
    }

    return bol;
}