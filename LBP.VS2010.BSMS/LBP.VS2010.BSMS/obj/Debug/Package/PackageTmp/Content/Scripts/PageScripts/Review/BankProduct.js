$(document).ready(function () {
    loadingScreen(true, 0);
    //toggleEventForElement("#btnSearch", "click", validateSearchProductType, true, null);
    toggleEventForElement("#ddlSearchBy", "change", findBankProduct, true, null);
    loadBankProductList();
    //findBankProduct();
});

function loadBankProductList() {
    loadingScreen(false, 0);
    PageMethods.ProductCategoryList(
    function OnSuccess(result) {
        if (result.length > 0) {
            result[0].description = "All";
            result[0].value = "";
            loadDropdown('ddlSearchBy', result, true, 'All', 'All');
            setDropdownValue('ddlSearchBy', 'All');
            findBankProduct();
        }
        loadingScreen(true, 0);
    },
    function OnError() {
        alert(err.get_message());
    });
}

function validateSearchProductType() {
    loadingScreen(false, 0);
    var searchObj = new Object;
    searchObj.ProductType = getDropdownValue('ddlSearchBy');
    PageMethods.ValidateSearch(searchObj,
        function (result) {
            if (result == "") {/// <reference path="../../../../PageUtils/" />

                findBankProduct();
            }
            else {
                showError(result);
            }
            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
        }
    );
}

function findBankProduct() {
    loadingScreen(false, 0);
    var obj = new Object;
    obj.ProductType = getDropdownValue('ddlSearchBy');
    PageMethods.LoadBankProducts(obj,
            function OnSuccess(result) {
                if (result != null) {
                    var index = 1;
                    var itemsPerPage = 10;
                    var list = [];
                    $("#tBodyBankProduct").empty();
                    for (var r = 0; r < result.length; r++) {
                        list.push(result[r]);
                    }
                    secureStorage.setItem('list', JSON.stringify(list));
                    allItems = result.length;
                    if (result.length > 0) {
                        createTable("divBankProduct", "tblBankProduct", "tBodyBankProduct", list, index, itemsPerPage, allItems, "list", "Products");
                    }
                }
                loadingScreen(true, 0);
            },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        }
 );
}

 