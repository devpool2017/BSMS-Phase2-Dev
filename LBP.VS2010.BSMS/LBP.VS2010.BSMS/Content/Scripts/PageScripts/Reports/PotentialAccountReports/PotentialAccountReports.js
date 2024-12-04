$(async function () {
    loadingScreen(true, 0);
    hideSpecificError('lblPotentialAccountReportErrorMessage')
    toggleEventForElement('#btnGenerate', 'click', AJAXWrapPageMethodCall, true, 'generateReport');

    showDiv(['divReport'], false);
        $('iframe[name="ifrmReportViewer"]').on('load', function () {
        resizeIframe(this);
        loadingScreen(true, 0);
    });

});

function generateReport() {
    hideSpecificError('lblPotentialAccountReportErrorMessage')
    loadingScreen(false, 1000);

    var obj = {
        SelectReport: getDropdownValue('ddl-s')
    };

    PageMethods.GenerateReport(obj,
        function OnSuccess(result) {
            if (result == '') {
             $('iframe[name="ifrmReportViewer"]').attr("src", '../Reports/ReportViewer.aspx');
             showDiv(['divReport'], true);
             loadingScreen(true, 0);
            }
            else {
                showDiv(['divReport'], false);
                showSpecificError(result, 'lblPotentialAccountReportErrorMessage', 'divPotentialAccountReportError');
            }
            loadingScreen(true, 0);

        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });

}

function resizeIframe(obj) {
    obj.style.height = obj.contentWindow.document.documentElement.scrollHeight + 'px';
}