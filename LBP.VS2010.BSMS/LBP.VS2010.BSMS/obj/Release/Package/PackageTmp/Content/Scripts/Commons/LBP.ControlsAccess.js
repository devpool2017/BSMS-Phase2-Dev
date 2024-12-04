$(document).ready(function () {

    //Initialize all controls based on user's access matrix
    initControlsByAccess();
});

function initControlsByAccess() {
    canView();
    canInsert();
    canUpdate();
    canDelete();
    canPrint();
    canApprove();
    canValidate();
    canConsolidate();
    isAdmin();
}

function canView() {    
    $('.canView').each(function () {        
        HasAccess(this, "CanView");
    });
}

function canInsert() {
    $('.canInsert').each(function () {
        HasAccess(this, "CanInsert");
    });
}

function canUpdate() {
    $('.canUpdate').each(function () {
        HasAccess(this, "CanUpdate");
    });
}

function canDelete() {
    $('.canDelete').each(function () {
        HasAccess(this, "CanDelete");
    });
}

function canPrint() {
    $('.canPrint').each(function () {
        HasAccess(this, "CanPrint");
    });
}

function canApprove() {
    $('.canApprove').each(function () {
        HasAccess(this, "CanApprove");
    });
}

function canValidate() {
    $('.canValidate').each(function () {
        HasAccess(this, "CanValidate");
    });
}

function canConsolidate() {
    $('.canConsolidate').each(function () {
        HasAccess(this, "CanConsolidate");
    });
}

function isAdmin() {
    $('.isAdmin').each(function () {
        HasAccess(this, "IsAdmin");
    });
}


function HasAccess(ctrl, colName) {
    PageMethods.HasAccess(colName,
        function OnSuccess(result) {
            if (!result) {
                ctrl.parentNode.removeChild(ctrl);
            }
        },
        function OnError(err) {

        }
    );
}