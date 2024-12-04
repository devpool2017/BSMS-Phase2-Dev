//// textarea

//(function ($) {
//    var observers = [];

//    $.event.special.domNodeInserted = {

//        setup: function setup(data, namespaces) {
//            var observer = new MutationObserver(checkObservers);

//            observers.push([this, observer, []]);
//        },

//        teardown: function teardown(namespaces) {
//            var obs = getObserverData(this);

//            obs[1].disconnect();

//            observers = $.grep(observers, function (item) {
//                return item !== obs;
//            });
//        },

//        remove: function remove(handleObj) {
//            var obs = getObserverData(this);

//            obs[2] = obs[2].filter(function (event) {
//                return event[0] !== handleObj.selector && event[1] !== handleObj.handler;
//            });
//        },

//        add: function add(handleObj) {
//            var obs = getObserverData(this);

//            var opts = $.extend({}, {
//                childList: true,
//                subtree: true
//            }, handleObj.data);

//            obs[1].observe(this, opts);

//            obs[2].push([handleObj.selector, handleObj.handler]);
//        }
//    };

//    function getObserverData(element) {
//        var $el = $(element);

//        return $.grep(observers, function (item) {
//            return $el.is(item[0]);
//        })[0];
//    }

//    function checkObservers(records, observer) {
//        var obs = $.grep(observers, function (item) {
//            return item[1] === observer;
//        })[0];

//        var triggers = obs[2];

//        var changes = [];

//        records.forEach(function (record) {
//            if (record.type === 'attributes') {
//                if (changes.indexOf(record.target) === -1) {
//                    changes.push(record.target);
//                }

//                return;
//            }

//            $(record.addedNodes).toArray().forEach(function (el) {
//                if (changes.indexOf(el) === -1) {
//                    changes.push(el);
//                }
//            })
//        });

//        triggers.forEach(function checkTrigger(item) {
//            changes.forEach(function (el) {
//                var $el = $(el);

//                if ($el.is(item[0])) {
//                    $el.trigger('domNodeInserted');
//                }
//            });
//        });
//    }

//})(jQuery);

//$(document).on('DOMNodeInserted', {
//	attributes: false,
//	subtree: true
//},
//function (e) {
//	if ($(e.target).hasClass('noForwardDate')) {
//		$(".noForwardDate").datepicker({
//			showAnim: "",
//			changeMonth: true,
//			changeYear: true,
//			yearRange: '1901:+0',
//			dateFormat: "mm/dd/yy",
//			maxDate: '0',
//			minDate: new Date(1900, 1 - 1, 1),
//			onChangeMonthYear: function (year, month, day) {
//				yearRange: '1901:+0',
//                $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
//			}
//		});
//}
//    if ($(e.target).hasClass('noBackDate')) {
//        $(".noBackDate").datepicker({
//            showAnim: "",
//            changeMonth: true,
//            changeYear: true,
//            minDate: new Date(),
//            yearRange: '1901:+75',
//            dateFormat: "mm/dd/yy",
//            onChangeMonthYear: function (year, month, day) {
//                yearRange: '1901:+0',
//                    $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
//            }
//        });
//    }
//	if ($(e.target).hasClass('shortdate')) {
//		$(".shortdate").datepicker({
//			showAnim: "",
//			changeMonth: true,
//			changeYear: true,
//			minDate: new Date(1900, 1, 1),
//			yearRange: '1901:+75',
//			dateFormat: "mm/dd/yy",
//			onChangeMonthYear: function (year, month, day) {
//				yearRange: '1901:+0',
//                $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
//			}
//		});
//	}
//	if ($(e.target).hasClass('noCurrentForwardDate')) {
//		$(".noCurrentForwardDate").datepicker({
//			showAnim: "",
//			changeMonth: true,
//			changeYear: true,
//			yearRange: '1901:+0',
//			dateFormat: "mm/dd/yy",
//			maxDate: '-1',
//			minDate: new Date(1900, 1 - 1, 1),
//			onChangeMonthYear: function (year, month, day) {
//				yearRange: '1901:+0',
//                $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
//			}
//		});
//	}
//	if ($(e.target).hasClass('defaultToday')) {
//		$(function () {
//			$(".defaultToday").datepicker('setDate', new Date());
//			$(".defaultToday").change();
//		});
//	}

//	var targetID = "#" + e.target.id;
//	if ($('select').hasClass('form-control')) {
//		$('select').removeClass("form-control");
////    	$('select').addClass("form-control-ddl");
//    }

//	if ($($.escapeSelector(targetID)).is('textarea')) {
//		$(e.target).after(function () {
//			var length = $(this).attr('maxlength');
//			var lblTextAreaHTML = '<div class="form-group form-row">';
//			lblTextAreaHTML += '<div class="col-12" align="right">';
//			lblTextAreaHTML += '<label class="label-font-em" id="lblDescriptionCount' + this.id + '">';
//			lblTextAreaHTML += '' + length + ' </label> <label class="label-font-em"> chars remaining</label>';
//			lblTextAreaHTML += '</div>';
//			lblTextAreaHTML += '</div>';
//			return lblTextAreaHTML;
//		});
//	}

//	// primary buttons

//	if ($(e.target).hasClass('addButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-primary");
//		$(e.target).removeClass("addButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-plus-square"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");

//	}

//	if ($(e.target).hasClass('saveButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-success");
//		$(e.target).removeClass("saveButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-save"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");

//	}


//	if ($(e.target).hasClass('viewButton')) {
//		$(e.target).addClass("btn btn-lbp-gray");
//		//$(e.target).addClass("btn btn-info");
//		$(e.target).removeClass("viewButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-eye"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}


//	if ($(e.target).hasClass('editButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-warning ");        
//		$(e.target).removeClass("editButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-pen-square"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}

//	if ($(e.target).hasClass('deleteButton')) {
//		//$(e.target).addClass("btn btn-lbp-green");
//		$(e.target).addClass("btn btn-danger");
//		$(e.target).removeClass("deleteButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-trash-alt"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}

//	// approval buttons

//	if ($(e.target).hasClass('approveButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-success");
//		$(e.target).removeClass("approveButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-thumbs-up"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}

//	if ($(e.target).hasClass('returnButton')) {
//		//$(e.target).addClass("btn btn-lbp-green");
//		$(e.target).addClass("btn btn-danger");
//		$(e.target).removeClass("returnButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-thumbs-down"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}

//	if ($(e.target).hasClass('rejectButton')) {
//		//$(e.target).addClass("btn btn-lbp-green");
//		$(e.target).addClass("btn btn-danger");
//		$(e.target).removeClass("rejectButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-thumbs-down"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}

//	// misc buttons

//	if ($(e.target).hasClass('okButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-primary");
//		$(e.target).removeClass("okButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-check-circle"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}


//	if ($(e.target).hasClass('backButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-secondary");
//		$(e.target).removeClass("backButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-caret-square-left"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}

//	if ($(e.target).hasClass('cancelButton')) {
//		//$(e.target).addClass("btn btn-lbp-warm-gray");
//		$(e.target).addClass("btn btn-danger");
//		$(e.target).removeClass("cancelButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-ban"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}


//	if ($(e.target).hasClass('searchButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-info");
//		$(e.target).removeClass("searchButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-search"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}


//	if ($(e.target).hasClass('validateButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-info");
//		$(e.target).removeClass("validateButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-question-circle"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}


//	if ($(e.target).hasClass('clearButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-warning");
//		$(e.target).removeClass("clearButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-sync-alt"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}

//	if ($(e.target).hasClass('uploadButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-warning");
//		$(e.target).removeClass("uploadButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-file-upload"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}

//	if ($(e.target).hasClass('downloadButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-warning");
//		$(e.target).removeClass("downloadButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-file-download"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}

//	if ($(e.target).hasClass('printButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-warning");
//		$(e.target).removeClass("printButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-print"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}

//	if ($(e.target).hasClass('unlockButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-warning");
//		$(e.target).removeClass("unlockButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-lock"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}

//	if ($(e.target).hasClass('selectButton')) {
//		$(e.target).addClass("btn btn-lbp-green");
//		//$(e.target).addClass("btn btn-primary");
//		$(e.target).removeClass("selectButton");
//		$(e.target).attr('style', 'color: white');
//		$(e.target).html('<span><i class="fa fa-check-circle"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//	}

//	if ($(e.target).hasClass('gridViewButton')) {
//		$(e.target).addClass("btn-sm");
//	}

//	// file input

//	//    if ($(e.target).hasClass('file-input-name')) {
//	//        //alert($(e.target).parent());
//	//        //$(e.target).parent().find("br").remove();
//	//        $(e.target).parent().after("<br/><br/><br/>");
//	//        //$(e.target).addClass("btn btn-warning");

//	//    }
//});

//$(document).ready(function () {

//    $('.form-control').each(function () {
//        $(this).addClass('lbpControl');
//    }).promise().done(function () { $('.lbpControl').trigger('DOMNodeInserted'); });
//    createTextAreaCounter();
//    formatFileInputControl();
//    formatPasswordControl();



//});

//function createTextAreaCounter() {
//    //Interrupt the execution thread to allow input to update
//    $("textarea").after(function () {
//        var length = $(this).attr('maxlength');
//        var lblTextAreaHTML = '<div class="form-group form-row">';
//        lblTextAreaHTML += '<div class="col-12" align="right">';
//        lblTextAreaHTML += '<label class="label-font-em" id="lblDescriptionCount' + this.id + '">';
//        lblTextAreaHTML += '' + length + ' </label> <label class="label-font-em"> chars remaining</label>';
//        lblTextAreaHTML += '</div>';
//        lblTextAreaHTML += '</div>';

//        return lblTextAreaHTML;
//    });
//}

//function computeRemainingTextAreaCharacterCount(textBox) {
//    //Interrupt the execution thread to allow input to update
//    var length = $(textBox).attr('maxlength');
//    var characterEntered = textBox.value.length;
//    var characterRemaining = length - characterEntered;
//    $('#lblDescriptionCount' + textBox.id).html(characterRemaining);


//}


//function formatFileInputControl() {
//    $(".custom-file").after('<br/><br/><br/>');
//}

//function formatPasswordControl() {
//    $(".showPasswordMeter").passtrength();
//    $(".togglePassword").showHidePassword();

//}


// textarea

var observer = new MutationObserver(function (mutationsList) {
    for (var mutation of mutationsList) {
        var addedNodes = mutation.addedNodes;

        for (var i = 0; i < addedNodes.length; i++) {
            var target = addedNodes[i];

            if (target.nodeType === Node.ELEMENT_NODE) {
                if (target.classList.contains('noForwardDate')) {
                    $(target).datepicker({
                        showAnim: "",
                        changeMonth: true,
                        changeYear: true,
                        yearRange: '1901:+0',
                        dateFormat: "mm/dd/yy",
                        maxDate: '0',
                        minDate: new Date(1900, 1 - 1, 1),
                        onChangeMonthYear: function (year, month, day) {
                            yearRange: '1901:+0',
                                $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
                        }
                    });
                }

                if (target.classList.contains('noBackDate')) {
                    $(target).datepicker({
                        showAnim: "",
                        changeMonth: true,
                        changeYear: true,
                        minDate: new Date(),
                        yearRange: '1901:+75',
                        dateFormat: "mm/dd/yy",
                        onChangeMonthYear: function (year, month, day) {
                            yearRange: '1901:+0',
                                $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
                        }
                    });
                }
                
                if (target.classList.contains('noBackDate3Months')) {
                    $(target).datepicker({
                        showAnim: "",
                        changeMonth: true,
                        changeYear: true,
                        yearRange: '-1:+75',
                        dateFormat: "mm/dd/yy",
                        maxDate: '0',
                        minDate:'-3m',
                        onChangeMonthYear: function (year, month, day) {
                            yearRange: '-1:+75',
                                $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
                        }
                    });
                } 

//				if (target.classList.contains('noForwardDate1Week')) {
//                    $(target).datepicker({
//                        showAnim: "",
//                        changeMonth: true,
//                        changeYear: true,
//                        yearRange: '1901:+0',
//                        dateFormat: "mm/dd/yy",
//                        maxDate: '0',
//                        minDate:'-7d',
//                        onChangeMonthYear: function (year, month, day) {
//                            yearRange: '1901:+0',
//                                $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
//                        }
//                    });
//                } 															  
                if (target.classList.contains('shortdate')) {
                    $(target).datepicker({
                        showAnim: "",
                        changeMonth: true,
                        changeYear: true,
                        minDate: new Date(1900, 1, 1),
                        yearRange: '1901:+75',
                        dateFormat: "mm/dd/yy",
                        onChangeMonthYear: function (year, month, day) {
                            yearRange: '1901:+0',
                                $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
                        }
                    });
                }

                if (target.classList.contains('noCurrentForwardDate')) {
                    $(target).datepicker({
                        showAnim: "",
                        changeMonth: true,
                        changeYear: true,
                        yearRange: '1901:+0',
                        dateFormat: "mm/dd/yy",
                        maxDate: '-1',
                        minDate: new Date(1900, 1 - 1, 1),
                        onChangeMonthYear: function (year, month, day) {
                            yearRange: '1901:+0',
                                $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
                        }
                    });
                }

                if (target.classList.contains('currentForwardDate')) {
                    $(target).datepicker({
                        showAnim: "",
                        changeMonth: true,
                        changeYear: true,
                        yearRange: new Date().getFullYear() + ':' + (new Date().getFullYear() + 20),
                        dateFormat: "mm/dd/yy",
                        minDate: 0,
                        onChangeMonthYear: function (year, month, inst) {
                            $(this).datepicker('setDate', new Date(year, month - 1, inst.selectedDay));
                        }
                    });
                }

                if (target.classList.contains('defaultToday')) {
                    $(function () {
                        $(target).datepicker('setDate', new Date());
                        $(target).change();
                    });
                }

                if (target.classList.contains('addButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("addButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-plus-square"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }
                if (target.classList.contains('subButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("subButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-minus-square"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('saveButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("saveButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-save"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('viewButton')) {
                    target.classList.add("btn", "btn-lbp-gray");
                    target.classList.remove("viewButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-eye"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('editButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("editButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-pen-square"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('deleteButton')) {
                    target.classList.add("btn", "btn-danger");
                    target.classList.remove("deleteButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-trash-alt"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('deactivateButton')) {
                    target.classList.add("btn", "btn-danger");
                    target.classList.remove("deactivateButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-trash-alt"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('activateButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("editButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-pen-square"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('approveButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("approveButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-thumbs-up"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('returnButton')) {
                    target.classList.add("btn", "btn-danger");
                    target.classList.remove("returnButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-thumbs-down"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('rejectButton')) {
                    target.classList.add("btn", "btn-danger");
                    target.classList.remove("rejectButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-thumbs-down"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('okButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("okButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-check-circle"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('backButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("backButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-caret-square-left"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('cancelButton')) {
                    target.classList.add("btn", "btn-danger");
                    target.classList.remove("cancelButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-ban"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('searchButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("searchButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-search"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('validateButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("validateButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-question-circle"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('clearButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("clearButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-sync-alt"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('uploadButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("uploadButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-file-upload"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('downloadButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("downloadButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-file-download"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('printButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("printButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-print"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('unlockButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("unlockButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-lock"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }


                if (target.classList.contains('selectButton')) {
                    target.classList.add("btn", "btn-lbp-green");
                    target.classList.remove("selectButton");
                    target.style.color = "white";
                    target.innerHTML = '<span><i class="fa fa-check-circle"></i></span>' + " " + "<strong>" + target.innerHTML + "</strong>";
                }

                if (target.classList.contains('gridViewButton')) {
                    target.classList.add("btn-sm");
                }
            }
        }
    }
});

// Observe changes to the entire document
observer.observe(document, {
    childList: true,
    subtree: true
});

//$(document).on('DOMNodeInserted', {
//    attributes: false,
//    subtree: true
//},
//    function (e) {
//        if ($(e.target).hasClass('noForwardDate')) {
//            $(".noForwardDate").datepicker({
//                showAnim: "",
//                changeMonth: true,
//                changeYear: true,
//                yearRange: '1901:+0',
//                dateFormat: "mm/dd/yy",
//                maxDate: '0',
//                minDate: new Date(1900, 1 - 1, 1),
//                onChangeMonthYear: function (year, month, day) {
//                    yearRange: '1901:+0',
//                        $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
//                }
//            });
//        }
//        if ($(e.target).hasClass('shortdate')) {
//            $(".shortdate").datepicker({
//                showAnim: "",
//                changeMonth: true,
//                changeYear: true,
//                minDate: new Date(1900, 1, 1),
//                yearRange: '1901:+75',
//                dateFormat: "mm/dd/yy",
//                onChangeMonthYear: function (year, month, day) {
//                    yearRange: '1901:+0',
//                        $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
//                }
//            });
//        }
//        if ($(e.target).hasClass('noCurrentForwardDate')) {
//            $(".noCurrentForwardDate").datepicker({
//                showAnim: "",
//                changeMonth: true,
//                changeYear: true,
//                yearRange: '1901:+0',
//                dateFormat: "mm/dd/yy",
//                maxDate: '-1',
//                minDate: new Date(1900, 1 - 1, 1),
//                onChangeMonthYear: function (year, month, day) {
//                    yearRange: '1901:+0',
//                        $(this).datepicker('setDate', new Date(year, month - 1, day.selectedDay));
//                }
//            });
//        }
//        if ($(e.target).hasClass('defaultToday')) {
//            $(function () {
//                $(".defaultToday").datepicker('setDate', new Date());
//                $(".defaultToday").change();
//            });
//        }

//        var targetID = "#" + e.target.id;
//        if ($('select').hasClass('form-control')) {
//            $('select').removeClass("form-control");
//            //    	$('select').addClass("form-control-ddl");
//        }

//        if ($($.escapeSelector(targetID)).is('textarea')) {
//            $(e.target).after(function () {
//                var length = $(this).attr('maxlength');
//                var lblTextAreaHTML = '<div class="form-group form-row">';
//                lblTextAreaHTML += '<div class="col-12" align="right">';
//                lblTextAreaHTML += '<label class="label-font-em" id="lblDescriptionCount' + this.id + '">';
//                lblTextAreaHTML += '' + length + ' </label> <label class="label-font-em"> chars remaining</label>';
//                lblTextAreaHTML += '</div>';
//                lblTextAreaHTML += '</div>';
//                return lblTextAreaHTML;
//            });
//        }

//        // primary buttons

//        if ($(e.target).hasClass('addButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-primary");
//            $(e.target).removeClass("addButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-plus-square"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");

//        }

//        if ($(e.target).hasClass('saveButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-success");
//            $(e.target).removeClass("saveButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-save"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");

//        }


//        if ($(e.target).hasClass('viewButton')) {
//            $(e.target).addClass("btn btn-lbp-gray");
//            //$(e.target).addClass("btn btn-info");
//            $(e.target).removeClass("viewButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-eye"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }


//        if ($(e.target).hasClass('editButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-warning ");        
//            $(e.target).removeClass("editButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-pen-square"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }

//        if ($(e.target).hasClass('deleteButton')) {
//            //$(e.target).addClass("btn btn-lbp-green");
//            $(e.target).addClass("btn btn-danger");
//            $(e.target).removeClass("deleteButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-trash-alt"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }

//        // approval buttons

//        if ($(e.target).hasClass('approveButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-success");
//            $(e.target).removeClass("approveButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-thumbs-up"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }

//        if ($(e.target).hasClass('returnButton')) {
//            //$(e.target).addClass("btn btn-lbp-green");
//            $(e.target).addClass("btn btn-danger");
//            $(e.target).removeClass("returnButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-thumbs-down"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }

//        if ($(e.target).hasClass('rejectButton')) {
//            //$(e.target).addClass("btn btn-lbp-green");
//            $(e.target).addClass("btn btn-danger");
//            $(e.target).removeClass("rejectButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-thumbs-down"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }

//        // misc buttons

//        if ($(e.target).hasClass('okButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-primary");
//            $(e.target).removeClass("okButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-check-circle"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }


//        if ($(e.target).hasClass('backButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-secondary");
//            $(e.target).removeClass("backButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-caret-square-left"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }

//        if ($(e.target).hasClass('cancelButton')) {
//            //$(e.target).addClass("btn btn-lbp-warm-gray");
//            $(e.target).addClass("btn btn-danger");
//            $(e.target).removeClass("cancelButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-ban"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }


//        if ($(e.target).hasClass('searchButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-info");
//            $(e.target).removeClass("searchButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-search"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }


//        if ($(e.target).hasClass('validateButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-info");
//            $(e.target).removeClass("validateButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-question-circle"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }


//        if ($(e.target).hasClass('clearButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-warning");
//            $(e.target).removeClass("clearButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-sync-alt"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }

//        if ($(e.target).hasClass('uploadButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-warning");
//            $(e.target).removeClass("uploadButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-file-upload"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }

//        if ($(e.target).hasClass('downloadButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-warning");
//            $(e.target).removeClass("downloadButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-file-download"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }

//        if ($(e.target).hasClass('printButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-warning");
//            $(e.target).removeClass("printButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-print"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }

//        if ($(e.target).hasClass('unlockButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-warning");
//            $(e.target).removeClass("unlockButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-lock"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }

//        if ($(e.target).hasClass('selectButton')) {
//            $(e.target).addClass("btn btn-lbp-green");
//            //$(e.target).addClass("btn btn-primary");
//            $(e.target).removeClass("selectButton");
//            $(e.target).attr('style', 'color: white');
//            $(e.target).html('<span><i class="fa fa-check-circle"></i></span>' + " " + "<strong>" + $(e.target).html() + "</strong>");
//        }

//        if ($(e.target).hasClass('gridViewButton')) {
//            $(e.target).addClass("btn-sm");
//        }

//        // file input

//        //    if ($(e.target).hasClass('file-input-name')) {
//        //        //alert($(e.target).parent());
//        //        //$(e.target).parent().find("br").remove();
//        //        $(e.target).parent().after("<br/><br/><br/>");
//        //        //$(e.target).addClass("btn btn-warning");

//        //    }
//    });

$(document).ready(function () {

    //$('.form-control').each(function () {
    //    $(this).addClass('lbpControl');
    //}).promise().done(function () { $('.lbpControl').trigger('DOMNodeInserted'); });
    //createTextAreaCounter();
    formatFileInputControl();
    formatPasswordControl();



});

function createTextAreaCounter() {
    //Interrupt the execution thread to allow input to update
    $("textarea").after(function () {
        var length = $(this).attr('maxlength');
        var lblTextAreaHTML = '<div class="form-group form-row">';
        lblTextAreaHTML += '<div class="col-12" align="right">';
        lblTextAreaHTML += '<label class="label-font-em" id="lblDescriptionCount' + this.id + '">';
        lblTextAreaHTML += '' + length + ' </label> <label class="label-font-em"> chars remaining</label>';
        lblTextAreaHTML += '</div>';
        lblTextAreaHTML += '</div>';

        return lblTextAreaHTML;
    });
}

function computeRemainingTextAreaCharacterCount(textBox) {
    //Interrupt the execution thread to allow input to update
    var length = $(textBox).attr('maxlength');
    var characterEntered = textBox.value.length;
    var characterRemaining = length - characterEntered;
    $('#lblDescriptionCount' + textBox.id).html(characterRemaining);


}


function formatFileInputControl() {
    $(".custom-file").after('<br/><br/><br/>');
}

function formatPasswordControl() {
    $(".showPasswordMeter").passtrength();
    $(".togglePassword").showHidePassword();

}
