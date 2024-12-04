
if (window.jQuery) {
    /* All onload functions */
    $(document).ready(function () {

        $("button, input[type='button']").on("click", function () {
            var isDisabled = $(this).attr('disabled');
            if (isDisabled == undefined || isDisabled == null) {
                var ctrlValue = $(this).prop('disabled');
                var refObj = this;
                var attr = $(this).attr('data-dismiss');
                var dttrg = $(this).attr('data-target');
                var attr2 = $(this).attr('data-toggle');
                if ((attr == undefined || attr == null) && 
                    (dttrg == undefined || dttrg ==null) &&
                    (attr2 == undefined || attr2==null)) {
                    $(this).prop('disabled', true);

                    setTimeout(function () {
                        $(refObj).prop('disabled', ctrlValue);
                    }, 500, refObj, ctrlValue);

                }
            }
        });
    });
}