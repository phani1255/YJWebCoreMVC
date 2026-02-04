/*Hemanth   08/26/2024 edited exportToExcel and exporttopdf for display multiple data tables in single form
* Hemanth   08/27/2024 changed pdf size in common function
* Hemanth   08/28/2024 code added to enable date fields when click last month and last week dates
* Neetha    08/28/2024 Removed alert when selecting customer
* Phanindra 08/29/2024 added refresh buttons change globally.
* Neetha    09/04/2024 added top 20 selections in customer dropdown.
* Phanindra 09/06/2024 added addNumericColumnsClasstoDT function to make price and qty columns right justify.
* Phanindra 09/09/2024 Added condition to hide search results when select customer popup is closed.
* Phanindra 09/10/2024 Added script for common reset button.
* Hemanth   09/11/2024 Added script for change download filename as formname.
* Phanindra 09/12/2024 Added function for getting style images 
* Phanindra 09/13/2024 Added formatStyleNumber function for removing special characters in style
* Phanindra 09/20/2024 Changed printDiv function for print and preview options
* Phanindra 09/22/2024 Worked on Changes for default filters and numberWithCommas function
* Phanindra 09/25/2024 Worked on print and preview option styles
* Neetha    10/03/2024 Added new function to hide required columns in print and preview - hideColumnsClasstoDT().
* Neetha    10/04/2024 Changes in  hideColumnsClasstoDT().
* Phanindra 10/04/2024 added functionality to show footer only in last page in printdiv function.
* Hemanth   10/08/2024 fix for reset datatable pagination and non empty numaric rows for print page.
* Neetha    10/11/2024 Preview functionality inside popups for Sales COG profit Report.
* Hemanth   10/13/2024 Changes added for showing preview and print with multiple datatables in form ex:SalesSummaryReport
* Neetha    10/14/2024 Fixed multiple downloads on clicking excel button.
* Hemanth   10/15/2024 SeachInvoice code changes added
* Hemanth   10/24/2024 preview issue for hide table
* Phanindra 11/04/2024 Added jsonToXml method to convert datatable object to xml format.
* Hemanth   11/04/2024 Added hide and show report tables code print and preview.
* Phanindra 11/20/2024 Added getCustomerNamebyCode , getPotCustomerNamebyCode methods and added dropdown funcationality for potential customers. and added code to format phone number
* Phanindra 11/28/2024 Added methods for preview, print, excel and pdf download for normal div table
* Phanindra 12/04/2024 Modified print functionality to show in landscape mode when there are more than 8 columns in table.
* Hemanth   12/09/2024 Changes for print option with multple tables code added
* Phanindra 12/09/2024 Fixed isue with duplicate content in Print and Preview functions
* Phanindra 12/23/2024 Commented auto show option for menu items on page load.
* Phanindra 12/25/2024 Worked on fixing print related issues and fixed issue if the table is not datatable
* Phanindra 01/07/2025 Worked on adding callback functionality in selectUer function.
* Phanindra 01/20/2025 Removed default font size for pdf.
* Phanindra 03/04/2025 Fixed issue with heading in pdf report.
* Phanindra 03/12/2025 Fixed issue with heading in print report.
* Phanindra 03/17/2025 Worked on print related issue for portrait and landscape
* Hemanth   04/24/2025 selectInvoice method overloaded
* Phanindra 04/24/2025 Modified selectUser method.
* Manoj     05/16/2025 Changes Added for Print,Preview,PDF, Excel Reports for Adding Sub Heading To reports
* Phanindra 05/19/2025 Added method to adjust submenu items position
* Manoj     05/21/2025 fixed adding PDF Reports Sub Headings 
* Manoj     05/23/2025 Fixed Preview Report Multiple Headings Issue 
* Phanindra 05/27/2025 Fixed issue with third level menu alignment. 
* Phanindra 06/09/2025 Fixed issue with third level menu alignment.
* Phanindra 06/12/2025 Fixed menu issues for small screen.
* Hemanth   06/18/2025 selectRepair and clos modal function added
* Hemanth   06/20/2025 dates functionality added for rpair order
* Hemanth   06/23/2025 changs in selectRepair function
* Hmanth    06/25/2025 Added proforma invoice related functions.
* Hmanth    07/18/2025 Added selectPo searchPO related functions.
* Manoj     07/28/2025 Added changes in printNormalHtmlTable 
* Phanindra 08/14/2025 Updated all the click events to work for dynamically loaded html elements
* Phanindra 10/16/2025 removed modal-open class on body when closing model popup
* Hemanth   10/21/2025 Added BCustomerCodesForAutofill.
*/


function numberWithCommas(x) {
    if (x == "" || x == null || x == '0')
        return '0.00';
    else {
        //console.log(x);
        return parseFloat(x).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }

}


$(document).on('click', '.btnGetLastMonthDates', function () {
    $("#cbFillFromToDates").prop("checked", false);
    var date1 = new Date();
    date1.setDate(0); // date1 now contains the last day of the previous month.
    var lastmonth = date1.getMonth() + 1;
    lastmonth = ('0' + lastmonth).slice(-2);
    var fromDate = date1.getFullYear() + "-" + lastmonth + "-01";
    var toDate = date1.getFullYear() + "-" + lastmonth + "-" + date1.getDate();

    if ($('#txtFromDate').length > 0) {
        $('#txtFromDate').val(fromDate).prop('disabled', false);
        $('#txtToDate').val(toDate).prop('disabled', false);
    }
    for (i = 1; i <= 5; i++) {
        $('#txtFromDate' + i).val(fromDate).prop('disabled', false);
        $('#txtToDate' + i).val(toDate).prop('disabled', false);
    }
    buttonStyleClassChange('btnFormResultsSearch');
});

$(document).on('click', '.btnGetLastWeekDates', function () {
    $("#cbFillFromToDates").prop("checked", false);
    // lastweek
    var today = new Date();
    var curr1 = new Date(today.getFullYear(), today.getMonth(), today.getDate() - 7);
    var firstday1 = new Date(curr1.setDate(curr1.getDate() - curr1.getDay()));
    var lastday1 = new Date(curr1.setDate(curr1.getDate() - curr1.getDay() + 6));

    var lstwk_frstdatemonth = ("00" + (firstday1.getMonth() + 1).toString()).slice(-2);
    var lstwk_lstdatemonth = ("00" + (lastday1.getMonth() + 1).toString()).slice(-2);
    var lstwk_frstdateday = ("00" + firstday1.getDate().toString()).slice(-2);
    var lstwk_lstdateday = ("00" + lastday1.getDate().toString()).slice(-2);

    //console.log('New last week first day Date :' + (firstday1.getFullYear() + "-" + lstwk_frstdatemonth + "-" + lstwk_frstdateday));
    //console.log('New last week last day Date :' + (lastday1.getFullYear() + "-" + lstwk_lstdatemonth + "-" + lstwk_lstdateday));
    var fromDate = firstday1.getFullYear() + "-" + lstwk_frstdatemonth + "-" + lstwk_frstdateday;
    var toDate = lastday1.getFullYear() + "-" + lstwk_lstdatemonth + "-" + lstwk_lstdateday;
    if ($('#txtFromDate').length > 0) {
        $('#txtFromDate').val(fromDate).prop('disabled', false);
        $('#txtToDate').val(toDate).prop('disabled', false);
    }
    for (i = 1; i <= 5; i++) {
        $('#txtFromDate' + i).val(fromDate).prop('disabled', false);
        $('#txtToDate' + i).val(toDate).prop('disabled', false);
    }
    //existAutoFilledData();
    buttonStyleClassChange('btnFormResultsSearch');
});

$(document).on('click', "#cbFillFromToDates", function () {
    fillFromToDates();
})

function fillFromToDates() {
    if ($("#cbFillFromToDates").prop('checked')) {
        if ($('#txtFromDate').length > 0) {
            $('#txtFromDate').val('1900-01-01').prop('disabled', 'true');
            $('#txtToDate').val('2098-12-31').prop('disabled', 'true');
        }
        for (i = 1; i <= 5; i++) {
            $('#txtFromDate' + i).val('1900-01-01').prop('disabled', 'true');
            $('#txtToDate' + i).val('2098-12-31').prop('disabled', 'true');
        }

    } else {
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);

        var today = now.getFullYear() + "-" + (month) + "-" + (day);
        console.log(today)
        if ($('#txtFromDate').length > 0) {
            $('#txtFromDate').val(today).removeAttr('disabled');
            $('#txtToDate').val(today).removeAttr('disabled');
        }
        for (i = 1; i <= 5; i++) {
            $('#txtFromDate' + i).val(today).removeAttr('disabled');
            $('#txtToDate' + i).val(today).removeAttr('disabled');
        }
    }
    if ($("#btnFormResultsSearch").length > 0) {
        buttonStyleClassChange('btnFormResultsSearch');
    }
}

$(document).on('click', "#cbFillFromToDates_repair", function () {
    fillFromToDatesRepair();
})

function fillFromToDatesRepair() {
    if ($("#cbFillFromToDates_repair").prop('checked')) {
        if ($('#txtFromDate_repair').length > 0) {
            $('#txtFromDate_repair').val('1900-01-01').prop('disabled', 'true');
            $('#txtToDate_repair').val('2098-12-31').prop('disabled', 'true');
        }
        for (i = 1; i <= 5; i++) {
            $('#txtFromDate_repair' + i).val('1900-01-01').prop('disabled', 'true');
            $('#txtToDate_repair' + i).val('2098-12-31').prop('disabled', 'true');
        }

    } else {
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);

        var today = now.getFullYear() + "-" + (month) + "-" + (day);
        console.log(today)
        if ($('#txtFromDate_repair').length > 0) {
            $('#txtFromDate_repair').val(today).removeAttr('disabled');
            $('#txtToDate_repair').val(today).removeAttr('disabled');
        }
        for (i = 1; i <= 5; i++) {
            $('#txtFromDate_repair' + i).val(today).removeAttr('disabled');
            $('#txtToDate_repair' + i).val(today).removeAttr('disabled');
        }
    }
    if ($("#btnFormResultsSearch").length > 0) {
        buttonStyleClassChange('btnFormResultsSearch');
    }
}

$(document).on('click', '.btnGetLastWeekDates_repair', function () {
    $("#cbFillFromToDates_repair").prop("checked", false);
    // lastweek
    var today = new Date();
    var curr1 = new Date(today.getFullYear(), today.getMonth(), today.getDate() - 7);
    var firstday1 = new Date(curr1.setDate(curr1.getDate() - curr1.getDay()));
    var lastday1 = new Date(curr1.setDate(curr1.getDate() - curr1.getDay() + 6));

    var lstwk_frstdatemonth = ("00" + (firstday1.getMonth() + 1).toString()).slice(-2);
    var lstwk_lstdatemonth = ("00" + (lastday1.getMonth() + 1).toString()).slice(-2);
    var lstwk_frstdateday = ("00" + firstday1.getDate().toString()).slice(-2);
    var lstwk_lstdateday = ("00" + lastday1.getDate().toString()).slice(-2);

    //console.log('New last week first day Date :' + (firstday1.getFullYear() + "-" + lstwk_frstdatemonth + "-" + lstwk_frstdateday));
    //console.log('New last week last day Date :' + (lastday1.getFullYear() + "-" + lstwk_lstdatemonth + "-" + lstwk_lstdateday));
    var fromDate = firstday1.getFullYear() + "-" + lstwk_frstdatemonth + "-" + lstwk_frstdateday;
    var toDate = lastday1.getFullYear() + "-" + lstwk_lstdatemonth + "-" + lstwk_lstdateday;
    if ($('#txtFromDate_repair').length > 0) {
        $('#txtFromDate_repair').val(fromDate).prop('disabled', false);
        $('#txtToDate_repair').val(toDate).prop('disabled', false);
    }
    for (i = 1; i <= 5; i++) {
        $('#txtFromDate_repair' + i).val(fromDate).prop('disabled', false);
        $('#txtToDate_repair' + i).val(toDate).prop('disabled', false);
    }
    //existAutoFilledData();
    buttonStyleClassChange('btnFormResultsSearch');
});

$(document).on('click', '.btnGetLastMonthDates_repair', function () {
    $("#cbFillFromToDates_repair").prop("checked", false);
    var date1 = new Date();
    date1.setDate(0); // date1 now contains the last day of the previous month.
    var lastmonth = date1.getMonth() + 1;
    lastmonth = ('0' + lastmonth).slice(-2);
    var fromDate = date1.getFullYear() + "-" + lastmonth + "-01";
    var toDate = date1.getFullYear() + "-" + lastmonth + "-" + date1.getDate();

    if ($('#txtFromDate_repair').length > 0) {
        $('#txtFromDate_repair').val(fromDate).prop('disabled', false);
        $('#txtToDate_repair').val(toDate).prop('disabled', false);
    }
    for (i = 1; i <= 5; i++) {
        $('#txtFromDate_repair' + i).val(fromDate).prop('disabled', false);
        $('#txtToDate_repair' + i).val(toDate).prop('disabled', false);
    }
    buttonStyleClassChange('btnFormResultsSearch');
});

$(document).on('click', "#cbFillFromToDates_proforma", function () {
    fillFromToDatesProforma();
})

function fillFromToDatesProforma() {
    if ($("#cbFillFromToDates_proforma").prop('checked')) {
        if ($('#txtFromDate_proforma').length > 0) {
            $('#txtFromDate_proforma').val('1900-01-01').prop('disabled', 'true');
            $('#txtToDate_proforma').val('2098-12-31').prop('disabled', 'true');
        }
        for (i = 1; i <= 5; i++) {
            $('#txtFromDate_proforma' + i).val('1900-01-01').prop('disabled', 'true');
            $('#txtToDate_proforma' + i).val('2098-12-31').prop('disabled', 'true');
        }

    } else {
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);

        var today = now.getFullYear() + "-" + (month) + "-" + (day);
        console.log(today)
        if ($('#txtFromDate_proforma').length > 0) {
            $('#txtFromDate_proforma').val(today).removeAttr('disabled');
            $('#txtToDate_proforma').val(today).removeAttr('disabled');
        }
        for (i = 1; i <= 5; i++) {
            $('#txtFromDate_proforma' + i).val(today).removeAttr('disabled');
            $('#txtToDate_proforma' + i).val(today).removeAttr('disabled');
        }
    }
    if ($("#btnFormResultsSearch").length > 0) {
        buttonStyleClassChange('btnFormResultsSearch');
    }
}

$(document).on('click', '.btnGetLastWeekDates_proforma', function () {
    $("#cbFillFromToDates_proforma").prop("checked", false);
    // lastweek
    var today = new Date();
    var curr1 = new Date(today.getFullYear(), today.getMonth(), today.getDate() - 7);
    var firstday1 = new Date(curr1.setDate(curr1.getDate() - curr1.getDay()));
    var lastday1 = new Date(curr1.setDate(curr1.getDate() - curr1.getDay() + 6));

    var lstwk_frstdatemonth = ("00" + (firstday1.getMonth() + 1).toString()).slice(-2);
    var lstwk_lstdatemonth = ("00" + (lastday1.getMonth() + 1).toString()).slice(-2);
    var lstwk_frstdateday = ("00" + firstday1.getDate().toString()).slice(-2);
    var lstwk_lstdateday = ("00" + lastday1.getDate().toString()).slice(-2);

    //console.log('New last week first day Date :' + (firstday1.getFullYear() + "-" + lstwk_frstdatemonth + "-" + lstwk_frstdateday));
    //console.log('New last week last day Date :' + (lastday1.getFullYear() + "-" + lstwk_lstdatemonth + "-" + lstwk_lstdateday));
    var fromDate = firstday1.getFullYear() + "-" + lstwk_frstdatemonth + "-" + lstwk_frstdateday;
    var toDate = lastday1.getFullYear() + "-" + lstwk_lstdatemonth + "-" + lstwk_lstdateday;
    if ($('#txtFromDate_proforma').length > 0) {
        $('#txtFromDate_proforma').val(fromDate).prop('disabled', false);
        $('#txtToDate_proforma').val(toDate).prop('disabled', false);
    }
    for (i = 1; i <= 5; i++) {
        $('#txtFromDate_proforma' + i).val(fromDate).prop('disabled', false);
        $('#txtToDate_proforma' + i).val(toDate).prop('disabled', false);
    }
    //existAutoFilledData();
    buttonStyleClassChange('btnFormResultsSearch');
});

$(document).on('click', '.btnGetLastMonthDates_proforma', function () {
    $("#cbFillFromToDates_proforma").prop("checked", false);
    var date1 = new Date();
    date1.setDate(0); // date1 now contains the last day of the previous month.
    var lastmonth = date1.getMonth() + 1;
    lastmonth = ('0' + lastmonth).slice(-2);
    var fromDate = date1.getFullYear() + "-" + lastmonth + "-01";
    var toDate = date1.getFullYear() + "-" + lastmonth + "-" + date1.getDate();

    if ($('#txtFromDate_proforma').length > 0) {
        $('#txtFromDate_proforma').val(fromDate).prop('disabled', false);
        $('#txtToDate_proforma').val(toDate).prop('disabled', false);
    }
    for (i = 1; i <= 5; i++) {
        $('#txtFromDate_proforma' + i).val(fromDate).prop('disabled', false);
        $('#txtToDate_proforma' + i).val(toDate).prop('disabled', false);
    }
    buttonStyleClassChange('btnFormResultsSearch');
});

function closeAllLists(elmnt) {
    var items = $(".autocomplete-items");
    items.each(function () {
        if (elmnt !== this && elmnt !== $("#ddlVendor")[0]) {
            $(this).empty();
        }
    });
}

$(document).ready(function () {

    if ($("#cbFillFromToDates").length > 0) {
        fillFromToDates();
    }


    // ---- Vendor Types dropdon code. -- start
    if ($("#VendorTypesForAutofill").length > 0) {
        var VenderTypes = JSON.parse($("#VendorTypesForAutofill").html());

        $("#ddlVendor").on("input", function () {
            var query = $(this).val().toLowerCase();
            var list = $("#autocomplete-list");
            list.empty();

            if (!query) return;

            var results = VenderTypes.filter(function (item) {
                return item.Text.toLowerCase().indexOf(query) === 0;
            });

            results.forEach(function (item) {
                var itemElement = $("<div>")
                    .addClass("autocomplete-item")
                    .text(item.Text)
                    .on("click", function () {
                        $("#ddlVendor").val(item.Text);
                        list.empty();
                    });

                list.append(itemElement);
            });
        });
    }
    // ---- Vendor Types dropdon code. -- end

    $(document).on("click", function (e) {
        closeAllLists(e.target);
    });

    // ---- Customer code dropdown start
    if ($("#CustomerCodesForAutofill").length > 0) {
        var CustomerCodes = JSON.parse($("#CustomerCodesForAutofill").html());
        $("#txtCustomerCode").on("input", function () {
            var capval = 0;
            var query = $(this).val().toLowerCase();
            var list = $("#autocomplete-list1");
            list.empty();

            if (!query) return;

            var results = CustomerCodes.filter(function (item) {
                return item.Text.toLowerCase().indexOf(query) === 0;
            });

            results.forEach(function (item) {
                capval++;
                if (capval <= 20) {
                    var itemElement = $("<div>")
                        .addClass("autocomplete-item")
                        .text(item.Text)
                        .on("click", function () {
                            $("#txtCustomerCode").val(item.Text);
                            list.empty();
                        });

                    list.append(itemElement);
                }
            });
        });
    }

    // ---- Customer Codes dropdown end.
	
	if ($("#BCustomerCodesForAutofill").length > 0) {
		var CustomerCodes = JSON.parse($("#BCustomerCodesForAutofill").html());
		$("#txtBCustomerCode").on("input", function () {
			var capval = 0;
			var query = $(this).val().toLowerCase();
			var list = $("#autocomplete-list2");
			list.empty();

			if (!query) return;

			var results = CustomerCodes.filter(function (item) {
				return item.Text.toLowerCase().indexOf(query) === 0;
			});

			results.forEach(function (item) {
				capval++;
				if (capval <= 20) {
					var itemElement = $("<div>")
						.addClass("autocomplete-item")
						.text(item.Text)
						.on("click", function () {
							$("#txtBCustomerCode").val(item.Text);
							list.empty();
						});

					list.append(itemElement);
				}
			});
		});
	}

    // ---- Potential Customer code dropdown start
    if ($("#PotCustomerCodesForAutofill").length > 0) {
        var PotCustomerCodes = JSON.parse($("#PotCustomerCodesForAutofill").html());
        $("#txtPotCustomerCode").on("input", function () {
            var capval = 0;
            var query = $(this).val().toLowerCase();
            var list = $("#potCustomer-list1");
            list.empty();

            if (!query) return;

            var results = PotCustomerCodes.filter(function (item) {
                return item.Text.toLowerCase().indexOf(query) === 0;
            });

            results.forEach(function (item) {
                capval++;
                if (capval <= 20) {
                    var itemElement = $("<div>")
                        .addClass("potCustomer-item")
                        .text(item.Text)
                        .on("click", function () {
                            $("#txtPotCustomerCode").val(item.Text);
                            list.empty();
                        });

                    list.append(itemElement);
                }
            });
        });
    }
    // ---- Potential Customer Codes dropdown end.

    //alert(window.location.pathname);
    //$("a[href='" + window.location.pathname + "']").addClass('active');
    //$("a[href='" + window.location.pathname + "']").parent('.third_li').parent('.third_ul').show();
    //$("a[href='" + window.location.pathname + "']").parent('.third_li').parent('.third_ul').parent('.second_li').parent('.first_ul').show();

    window.numericcolumnlist = [];
})
//---- document.ready function end ---



$(document).on('click', ".showbsmodal", function () {
    id = $(this).attr("data-modal-id");
    $("#" + id).show();
    $("#" + id).addClass("show");
})
$(document).on('click', ".closeModal", function () {
    $('#showHideReport').css('display', 'none');
    id = $(this).attr("data-modal-id");
    if (id === undefined) {
        id = $(this).attr("data-model");
    }
    $("#" + id).hide();
    $("#" + id).removeClass("show");
    if (id == "selectCustomerAccModal") {
        $("#userSearchForm input[type='text']").val('');
        $("#searchResultsForCustomerCodes").html('');
    }
    if (id == "previewContentModal") {
        $(".main-content").show();
        $('.printableContent').html('');

        //dataTable = $('#' + id).DataTable();
        dataTable = $('.dataTable').DataTable();
        //var currentPage = dataTable.page();
        //var currentPageLength = dataTable.page.len();
        var currentPageLength = 25;
        // Restore the original pagination state
        dataTable.page.len(currentPageLength).draw();
        //dataTable.page(currentPage).draw(false);
    }
    $("body").removeClass("modal-open");
})


$(document).on('click', '#btnDataTablePreview', function () {
    tableid = $(this).attr("table-id");
    tablesCount = $(this).attr("table-count");
    tablesSubHeadings = $(this).attr("table-sub-headings");
    previewDiv(tableid, tablesCount, tablesSubHeadings);
})
$(document).on('click', '#btnDataTablePrint', function () {
    tableid = $(this).attr("table-id");
    tablesCount = $(this).attr("table-count");
    tablesSubHeadings = $(this).attr("table-sub-headings");
    reportFormat = "";
    if ($(this).attr('report-format') !== undefined) {
        reportFormat = $(this).attr('report-format');
    }
    printDiv(tableid, tablesCount, tablesSubHeadings, "", reportFormat);
});

$(document).on('click', '.btnDataTableDownloadExcel', function () {
    tableid = $(this).attr("table-id");
    tablesCount = $(this).attr("table-count");
    exportToExcel(tableid, tablesCount);
});

$(document).on('click', '.btnDataTableDownloadPDF', function () {
    tableid = $(this).attr("table-id");
    tablesCount = $(this).attr("table-count");
    exportTableToPDF(tableid, tablesCount);
});

$(document).on('click', ".activateFormRefreshBtn", function () {
    buttonStyleClassChange('btnFormResultsSearch');
})

$(document).on('click', '#btnFormResultsSearch', function () {
    existAutoFilledData();
    $('#btnFormResultsSearch').removeClass('needToRefreshBtn').addClass('refreshBtnDefault');
    $('#btnFormResultsSearch').val("Refresh");
});


function buttonStyleClassChange(btnId) {
    $('#' + btnId).removeClass('refreshBtnDefault').addClass('needToRefreshBtn');
    $('#' + btnId).val("Refresh *");
}


function selectUser(acc) {
    if ($("#methodName").length == 1) {
        if ($("#methodName").val() == "AddAdjReceivable") {
            GetReceiptData(acc);
            getCustomerNameByCode(acc)
        }
        if ($("#methodName").val() == "AddEditCredit") {
            setTimeout(function () {
                loadGridData();
                getCustomerNameByCode(acc);
            }, 1000);
        }
    }
    //if (confirm("Are you sure to select " + acc)) {
	if ($("#userAccount").val() == "bacc")
		$('#txtBCustomerCode').val(acc);
	else
    $('#txtCustomerCode').val(acc);
    //$('#userSearchPopup').hide();
    $(".closeModal").trigger('click');
    //}
}

function searchUsers() {
    var fname = $('#fname').val();
    var lname = $('#lname').val();
    var state = $('#state').val();
    var city = $('#city').val();
    var zip = $('#zip').val();
    var phone = $('#phone').val();
    var email = $('#email').val();

    $.ajax({
        url: '../Home/getCustomers',
        type: 'GET',
        data: {
            fname: fname,
            lname: lname,
            state: state,
            city: city,
            zip: zip,
            phone: phone,
            email: email
        },
        success: function (result) {
            $('#searchResultsForCustomerCodes').html(result);
        }
    });
}

// For SaleProfitReport
$(document).on('click', '.btnDataTablePreview1', function () {
    tableid = $(this).attr("table-id");
    tablesCount = $(this).attr("table-count");
    tablesSubHeadings = $(this).attr("table-sub-headings");
    previewDiv(tableid, tablesCount, tablesSubHeadings);
});
$(document).on('click', '.btnDataTablePrint1', function () {
    tableid = $(this).attr("table-id");
    tablesCount = $(this).attr("table-count");
    tablesSubHeadings = $(this).attr("table-sub-headings");
    reportFormat = "";
    if ($(this).attr('report-format') !== undefined) {
        reportFormat = $(this).attr('report-format');
    }
    printDiv(tableid, tablesCount, tablesSubHeadings, "", reportFormat);
});
//ends

//Preview for COG profit Report start
$(document).on('click', '.previewCloseButton', function () {
    $("#previewContentModal").hide();
    $("#previewContentModal").addClass("hide");
    $("#searchContentModal").show();
    $("#searchContentModal").addClass("show");
});

$(document).on('click', '.searchCloseButton', function () {
    //$("#previewContentModal").show();
    //$("#previewContentModal").addClass("show");
    $("#searchContentModal").hide();
    $("#searchContentModal").addClass("hide");
});

$(document).on('click', '.btnDataTablePreviewP', function () {
    tableid = $(this).attr("table-id");
    $("#previewContentModal").show();
    $("#previewContentModal").addClass("show");
    $("#searchContentModal").hide();
    $("#searchContentModal").addClass("hide");
    tablesCount = $(this).attr("table-count");
    tablesSubHeadings = $(this).attr("table-sub-headings");

    previewDiv(tableid, tablesCount, tablesSubHeadings);
});

$(document).on('click', '.btnDataTablePrintP', function () {
    tableid = $(this).attr("table-id");
    tablesCount = $(this).attr("table-count");
    tablesSubHeadings = $(this).attr("table-sub-headings");
    reportFormat = "";
    if ($(this).attr('report-format') !== undefined) {
        reportFormat = $(this).attr('report-format');
    }
    printDiv(tableid, tablesCount, tablesSubHeadings, "", reportFormat);
});
//Preview for COG profit Report End


function hideColumnsClasstoDT(tableid, hidecolumnlist) {
    window.hidecolumnlist = hidecolumnlist;
    $('#' + tableid + ' tr').each(function () {
        for (var i = 0; i < hidecolumnlist.length; i++) {
            $(this).children('td').eq(hidecolumnlist[i]).addClass('hideColumns');
            // $('#tfooter').find('th').eq(hidecolumnlist[i]).addClass('hideColumns');
            //$(this).children('th').eq(hidecolumnlist[i]).addClass('hideColumns');
        }
    })
    /*setTimeout(function () {
        // Restore the original pagination state
        dataTable.page.len(currentPageLength).draw();
        dataTable.page(currentPage).draw(false);
    }, 500);*/
}

function previewDiv(divId, tablesCount, tablesSubHeadings, reportTitle = "") {

    if (typeof (tablesCount) != "undefined" && parseInt(tablesCount) > 1) {
        $('#showHideReport').css('display', 'block');
        $('#previewContentModal .previewContent').html('');
        headings = tablesSubHeadings.split(",");

        for (i = 1; i <= parseInt(tablesCount); i++) {
            var dataTable = $('#' + divId + '-' + i).DataTable();
            var currentPage = dataTable.page();
            var currentPageLength = dataTable.page.len();

            $("#previewContentModalLabel").html($(".page-title h1").html());
            // Set the page length to show all entries
            dataTable.page.len(-1).draw();

            if (reportTitle != "") {
                //Adding reportTitle Html Content
                $('.previewContent').html("<div  style='style='text - align: center; '>" + reportTitle + "</div>");
            }
            else {
                $('.previewContent').append('<div  style="padding-top:20px;padding-bottom:20px;font-size:14px;"><b>' + headings[i - 1] + '</b></div>');
            }

            $('#' + divId + '-' + i).clone().appendTo('#previewContentModal .previewContent');
            $('#previewContentModal .previewContent table').removeClass('dataTable').removeClass('table').addClass('table-bordered');
            $(".main-content").hide();
            $("#previewContentModal").show();
            $("#previewContentModal").addClass("show");
            columnlist = window.numericcolumnlist;
            hidecolumnlist = window.hidecolumnlist;

            $('#previewContentModal table tr').each(function () {
                for (var i = 0; i < columnlist.length; i++) {
                    $(this).children('td').eq(columnlist[i]).addClass('numericColumns');
                }

                if (hidecolumnlist != undefined) {
                    for (var i = 0; i < hidecolumnlist.length; i++) {
                        //$(this).children('td').eq(hidecolumnlist[i]).addClass('hideColumns');
                        //$(this).children('th').eq(hidecolumnlist[i]).addClass('hideColumns');
                        $(this).children('td').eq(hidecolumnlist[i]).hide();
                        $(this).children('th').eq(hidecolumnlist[i]).hide();
                    }
                }
            })
        }
    } else {
        if ($('#' + divId).hasClass("dataTable")) {
            var dataTable = $('#' + divId).DataTable();
            var currentPage = dataTable.page();
            var currentPageLength = dataTable.page.len();
            // Set the page length to show all entries
            dataTable.page.len(-1).draw();
        }

        if (reportTitle != "") {
            //Adding reportTitle Html Content

            $('#previewContentModalLabel').html("<div  style='style='text - align: center; '>" + reportTitle + "</div>");
        }
        else {
            $("#previewContentModalLabel").html($(".page-title h1").html());
        }


        $('#previewContentModal .previewContent').html('');
        $('#' + divId).clone().appendTo('#previewContentModal .previewContent');
        $('#previewContentModal .previewContent table').removeClass('dataTable').removeClass('table').addClass('table-bordered');
        $(".main-content").hide();
        $("#previewContentModal").show();
        $("#previewContentModal").addClass("show");
        columnlist = window.numericcolumnlist;
        hidecolumnlist = window.hidecolumnlist;

        $('#previewContentModal table tr').each(function () {
            for (var i = 0; i < columnlist.length; i++) {
                $(this).children('td').eq(columnlist[i]).addClass('numericColumns');
            }

            if (hidecolumnlist != undefined) {
                for (var i = 0; i < hidecolumnlist.length; i++) {
                    //$(this).children('td').eq(hidecolumnlist[i]).addClass('hideColumns');
                    //$(this).children('th').eq(hidecolumnlist[i]).addClass('hideColumns');
                    $(this).children('td').eq(hidecolumnlist[i]).hide();
                    $(this).children('th').eq(hidecolumnlist[i]).hide();
                }
            }
        })
    }
    $('#previewContentModal .previewContent table').removeClass('d-none');
    if ($('#' + divId).hasClass("dataTable")) {
        setTimeout(function () {
            // Restore the original pagination state
            dataTable.page.len(currentPageLength).draw();
            dataTable.page(currentPage).draw(false);
        }, 500);
    }

    return false;
}

function previewDiv_13102024_bk(divId) {
    var dataTable = $('#' + divId).DataTable();
    var currentPage = dataTable.page();
    var currentPageLength = dataTable.page.len();

    $("#previewContentModalLabel").html($(".page-title h1").html());

    // Set the page length to show all entries
    dataTable.page.len(-1).draw();
    $('#previewContentModal .previewContent').html('');
    $('#' + divId).clone().appendTo('#previewContentModal .previewContent');
    $('#previewContentModal .previewContent table').removeClass('dataTable').removeClass('table').addClass('table-bordered');
    $(".main-content").hide();
    $("#previewContentModal").show();
    $("#previewContentModal").addClass("show");
    columnlist = window.numericcolumnlist;
    hidecolumnlist = window.hidecolumnlist;

    $('#previewContentModal table tr').each(function () {
        for (var i = 0; i < columnlist.length; i++) {
            $(this).children('td').eq(columnlist[i]).addClass('numericColumns');
        }

        if (hidecolumnlist != undefined) {
            for (var i = 0; i < hidecolumnlist.length; i++) {
                //$(this).children('td').eq(hidecolumnlist[i]).addClass('hideColumns');
                //$(this).children('th').eq(hidecolumnlist[i]).addClass('hideColumns');
                $(this).children('td').eq(hidecolumnlist[i]).hide();
                $(this).children('th').eq(hidecolumnlist[i]).hide();
            }
        }
    })
    setTimeout(function () {
        // Restore the original pagination state
        dataTable.page.len(currentPageLength).draw();
        dataTable.page(currentPage).draw(false);
    }, 500);
}

function printDiv(divId, tablesCount, tablesSubHeadings, reportTitle = "", reportFormat = "") {
    $('#showHideReport').css('display', 'block');

    if (reportTitle != "") {
        //Adding reportTitle Html Content

        $('.printableContent').html("<div style='text-align:center;'>" + reportTitle + "</div>");
    } else {
        $('.printableContent').append("<div style='text-align:center;'>" + $(".page-title").html() + "</div>");
    }

    if (typeof (tablesCount) != "undefined" && parseInt(tablesCount) > 1) {
        $('.printableContent').html('');
        headings = tablesSubHeadings.split(",");
        for (j = 1; j <= parseInt(tablesCount); j++) {
            var dataTable = $('#' + divId + '-' + j).DataTable();
            var currentPage = dataTable.page();
            var currentPageLength = dataTable.page.len();
            // Set the page length to show all entries
            dataTable.page.len(-1).draw();
            $('.printableContent').append('<div  style="padding-top:20px;padding-bottom:20px;"><b>' + headings[j - 1] + '</b></div>');
            $('#' + divId + '-' + j).clone().appendTo('.printableContent');
            $('.printableContent table').removeClass('dataTable').addClass("table-bordered");

            $('.printableContent #tfooter-' + j + ' tr').clone().appendTo('.printableContent #' + divId + '-' + j + ' tbody');
            $('.printableContent #tfooter-' + j).remove();

            $(".main-content").hide();

            columnlist = window.numericcolumnlist;
            hidecolumnlist = window.hidecolumnlist;
            $('.printableContent #' + divId + '-' + j + ' tr').each(function () {
                for (var i = 0; i < columnlist.length; i++) {
                    $(this).children('td').eq(columnlist[i]).addClass('numericColumns');
                }

                if (hidecolumnlist != undefined) {
                    for (var i = 0; i < hidecolumnlist.length; i++) {

                        $(this).children('td').eq(hidecolumnlist[i]).hide();
                        $(this).children('th').eq(hidecolumnlist[i]).hide();
                    }
                }
            })
        }
    } else {
        if ($('#' + divId).hasClass("dataTable")) {
            var dataTable = $('#' + divId).DataTable();
            var currentPage = dataTable.page();
            var currentPageLength = dataTable.page.len();

            // Set the page length to show all entries
            dataTable.page.len(-1).draw();
        }
        $('#' + divId).clone().appendTo('.printableContent');
        $('.printableContent table').removeClass('dataTable').addClass("table-bordered");
        $('.printableContent table tfoot tr').clone().appendTo('.printableContent table tbody');
        $('.printableContent table tfoot').remove();
        $('.printableContent table').removeClass('d-none');
        $(".main-content").hide();
        //addNumericColumnsClasstoDT(tableid, window.numericcolumnlist);
        columnlist = window.numericcolumnlist;
        hidecolumnlist = window.hidecolumnlist;

        $('.printableContent table tr').each(function () {
            for (var i = 0; i < columnlist.length; i++) {
                $(this).children('td').eq(columnlist[i]).addClass('numericColumns');
            }

            if (hidecolumnlist != undefined) {
                for (var i = 0; i < hidecolumnlist.length; i++) {
                    //$(this).children('td').eq(hidecolumnlist[i]).addClass('hideColumns');
                    //$(this).children('th').eq(hidecolumnlist[i]).addClass('hideColumns');
                    $(this).children('td').eq(hidecolumnlist[i]).hide();
                    $(this).children('th').eq(hidecolumnlist[i]).hide();
                }
            }
        })
    }

    // Delay to allow the table to re-render with all rows
    setTimeout(function () {
        const columns = $('.printableContent table tbody tr td').length;
        let style = document.createElement('style');
        style.type = 'text/css';
        if (reportFormat != "") {
            if (reportFormat == "landscape") {
                style.innerHTML = '@page { size: landscape; }';
            } else {
                style.innerHTML = '@page { size: portrait; }';
            }
        } else {
            if (columns > 8) {
                style.innerHTML = '@page { size: landscape; }';
            } else {
                style.innerHTML = '@page { size: portrait; }';
            }
        }

        document.head.appendChild(style);
        window.print();
        setTimeout(() => {
            document.head.removeChild(style);
        }, 1000);

        $(".main-content").show();
        $('.printableContent').html('');
        // Restore the original pagination state
        if ($('#' + divId).hasClass("dataTable")) {
            dataTable.page.len(currentPageLength).draw();
            dataTable.page(currentPage).draw(false);
        }
        $('.printableContent table').addClass('d-none');
        $('#showHideReport').css('display', 'none');
    }, 500);

}

function printDiv_13102024_bk(divId) {
    var dataTable = $('#' + divId).DataTable();
    var currentPage = dataTable.page();
    var currentPageLength = dataTable.page.len();

    // Set the page length to show all entries
    dataTable.page.len(-1).draw();
    $('#' + divId).clone().appendTo('.printableContent');
    $('.printableContent table').removeClass('dataTable').addClass("table-bordered");
    $('.printableContent table tfoot tr').clone().appendTo('.printableContent table tbody');
    $('.printableContent table tfoot').remove();
    $(".main-content").hide();
    //addNumericColumnsClasstoDT(tableid, window.numericcolumnlist);
    columnlist = window.numericcolumnlist;
    hidecolumnlist = window.hidecolumnlist;

    $('.printableContent table tr').each(function () {
        for (var i = 0; i < columnlist.length; i++) {
            $(this).children('td').eq(columnlist[i]).addClass('numericColumns');
        }

        if (hidecolumnlist != undefined) {
            for (var i = 0; i < hidecolumnlist.length; i++) {
                //$(this).children('td').eq(hidecolumnlist[i]).addClass('hideColumns');
                //$(this).children('th').eq(hidecolumnlist[i]).addClass('hideColumns');
                $(this).children('td').eq(hidecolumnlist[i]).hide();
                $(this).children('th').eq(hidecolumnlist[i]).hide();
            }
        }
    })
    /*$('.printableContent table').css({
        width: '100%',
        transform: 'rotate(-90deg)',
        transformOrigin: 'center center'
    });*/
    // Delay to allow the table to re-render with all rows
    setTimeout(function () {
        window.print();
        $(".main-content").show();
        $('.printableContent').html('');
        // Restore the original pagination state
        dataTable.page.len(currentPageLength).draw();
        dataTable.page(currentPage).draw(false);
    }, 500);

}

function exportToExcel(tableid, tablesCount, reportSubHeading = "") {

    if (typeof (tablesCount) != "undefined" && parseInt(tablesCount) > 1) {
        var dataTableId = tableid + '-1';
        var currentPage = $('#' + dataTableId).DataTable().page();
        var currentPageLength = $('#' + dataTableId).DataTable().page.len();

        // Set the page length to show all entries
        $('#' + dataTableId).DataTable().page.len(-1).draw();

        // Create the workbook and export it
        var wb = XLSX.utils.table_to_book(document.getElementById(dataTableId), { sheet: "Sheet1" });
        for (i = 2; i <= parseInt(tablesCount); i++) {
            var loopdataTableId = tableid + '-' + i;
            var currentPage = $('#' + loopdataTableId).DataTable().page();
            var currentLoopPageLength = $('#' + loopdataTableId).DataTable().page.len();
            var loopSheetNumber = "Sheet " + i;

            $('#' + loopdataTableId).DataTable().page.len(-1).draw();
            var ws2 = XLSX.utils.table_to_sheet(document.getElementById(loopdataTableId));

            XLSX.utils.book_append_sheet(wb, ws2, loopSheetNumber);
            $('#' + loopdataTableId).DataTable().page.len(currentLoopPageLength).draw();
            $('#' + loopdataTableId).DataTable().page(currentPage).draw(false);

        }

        let filname = ($('.form-h1').text() != "undefined" && $('.form-h1').text() != '') ? $('.form-h1').text() : 'data';

        //adding Date Range
        if (reportSubHeading != "") {
            filname += reportSubHeading;
        }

        XLSX.writeFile(wb, filname + '.xlsx');

        // Restore the original pagination state
        $('#' + dataTableId).DataTable().page.len(currentPageLength).draw();
        $('#' + dataTableId).DataTable().page(currentPage).draw(false);

    } else {
        var currentPage = $('#' + tableid).DataTable().page();
        var currentPageLength = $('#' + tableid).DataTable().page.len();

        // Set the page length to show all entries
        $('#' + tableid).DataTable().page.len(-1).draw();

        // Create the workbook and export it
        var wb = XLSX.utils.table_to_book(document.getElementById(tableid), { sheet: "Sheet1" });
        //XLSX.writeFile(wb, 'data.xlsx');

        // Restore the original pagination state
        $('#' + tableid).DataTable().page.len(currentPageLength).draw();
        $('#' + tableid).DataTable().page(currentPage).draw(false);

        let filname = ($('.form-h1').text() != "undefined" && $('.form-h1').text() != '') ? $('.form-h1').text() : 'data';

        //adding Date Range
        if (reportSubHeading.length > 0 && reportSubHeading != "" && filname != "data") {
            filname += $('#customSubHeader').text();
        }

        XLSX.writeFile(wb, filname + '.xlsx');

        // Restore the original pagination state
        $('#' + tableid).DataTable().page.len(currentPageLength).draw();
        $('#' + tableid).DataTable().page(currentPage).draw(false);
    }
}


function exportToExcel_old(tableid) {
    //var wb = XLSX.utils.table_to_book(document.getElementById('dataTableForBestSellerCategory'), { sheet: "Sheet1" });
    //XLSX.writeFile(wb, 'data.xlsx');
    var currentPage = $('#' + tableid).DataTable().page();
    var currentPageLength = $('#' + tableid).DataTable().page.len();

    // Set the page length to show all entries
    $('#' + tableid).DataTable().page.len(-1).draw();

    // Create the workbook and export it
    var wb = XLSX.utils.table_to_book(document.getElementById(tableid), { sheet: "Sheet1" });
    XLSX.writeFile(wb, 'data.xlsx');

    // Restore the original pagination state
    $('#' + tableid).DataTable().page.len(currentPageLength).draw();
    $('#' + tableid).DataTable().page(currentPage).draw(false);
}


async function exportTableToPDF(tableid, tablesCount, reportSubTitile = "") {

    if (typeof (tablesCount) != "undefined" && parseInt(tablesCount) > 1) {

        // Create the PDF document
        var { jsPDF } = window.jspdf;
        var doc = new jsPDF('l', 'mm', [297, 210]);
        for (dt = 1; dt <= parseInt(tablesCount); dt++) {

            var dataTableId = tableid + "-" + dt;
            var currentPageLength = $('#' + dataTableId).DataTable().page.len();
            // Store the current pagination state
            var currentPage = $('#' + dataTableId).DataTable().page();
            var currentPageLength = $('#' + dataTableId).DataTable().page.len();

            // Set the page length to show all entries
            $('#' + dataTableId).DataTable().page.len(-1).draw();

            // Get the table data
            var table = document.getElementById(dataTableId);
            var tableData = [];
            for (var i = 0, row; row = table.rows[i]; i++) {
                var rowData = [];
                for (var j = 0, col; col = row.cells[j]; j++) {
                    if (col.classList.contains("hide-on-print") || col.classList.contains("hideColumns")) {
                        continue;
                    }
                    rowData.push(col.innerText);
                }
                tableData.push(rowData);
            }

            doc.autoTable({
                head: [tableData[0]],
                body: tableData.slice(1),
            });
            // Restore the original pagination state
            $('#' + tableid).DataTable().page.len(currentPageLength).draw();
            $('#' + tableid).DataTable().page(currentPage).draw(false);
        }

        // Save the PDF
        let filname = ($('.form-h1').text() != "undefined" && $('.form-h1').text() != '') ? $('.form-h1').text() : 'data';
        doc.save(filname + '.pdf');


    } else {

        // Store the current pagination state
        var currentPage = $('#' + tableid).DataTable().page();
        var currentPageLength = $('#' + tableid).DataTable().page.len();

        // Set the page length to show all entries
        $('#' + tableid).DataTable().page.len(-1).draw();

        // Get the table data
        var table = document.getElementById(tableid);
        var tableData = [];
        for (var i = 0, row; row = table.rows[i]; i++) {
            var rowData = [];
            for (var j = 0, col; col = row.cells[j]; j++) {
                if (col.classList.contains("hide-on-print") || col.classList.contains("hideColumns")) {
                    continue;
                }
                rowData.push(col.innerText);
            }
            tableData.push(rowData);
        }


        // Create the PDF document
        var { jsPDF } = window.jspdf;
        var doc = new jsPDF();

        let headingText = ($('.form-h1').text() != "undefined" && $('.form-h1').text() != '') ? $('.form-h1').text() : 'Data Report';

        // Add heading to the PDF
        let currentY = 15;
        doc.setFontSize(16); // Set font size for heading
        doc.text(headingText, 105, currentY, { align: "center" });
        currentY += 7

        //Add Sub Heading to PDF
        if (reportSubTitile.length > 0 && reportSubTitile != "") {

            let subHeading = $('#customSubHeader').text();
            doc.setFontSize(11); // Set font size for heading
            doc.text(subHeading, 105, currentY, { align: "center" });
            currentY += 6


            if ($('#customSubHeader2').text().length > 0 && $('#customSubHeader2').text() != "") {

                let subHeading2 = $('#customSubHeader2').text();
                doc.setFontSize(11); // Set font size for heading
                doc.text(subHeading2, 105, currentY, { align: "center" });
                currentY += 4

            }

            if ($('#customSubHdrCurDate').text().length > 0 && $('#customSubHdrCurDate').text() != "") {

                let curDate = $('#customSubHdrCurDate').text();
                doc.setFontSize(11); // Set font size for heading
                doc.text(curDate, 15, currentY, { align: "left" });
                currentY += 4
            }
        }

        // Add the table to the PDF
        doc.autoTable({
            startY: currentY, // Position table below heading
            head: [tableData[0]],
            body: tableData.slice(1),
            margin: { top: 5, bottom: 5, left: 5, right: 5 },
            styles: { fontSize: 6, cellPadding: 0.5 },
        });

        // Save the PDF
        let filname = headingText;
        doc.save(filname + '.pdf');

        // Restore the original pagination state
        let dataTable = $('#' + tableid).DataTable();
        $('#' + tableid).DataTable().page.len(currentPageLength).draw();
        $('#' + tableid).DataTable().page(currentPage).draw(false);
    }
}
function exportTableToPDF_old(tableid) {
    // Store the current pagination state
    var currentPage = $('#' + tableid).DataTable().page();
    var currentPageLength = $('#' + tableid).DataTable().page.len();

    // Set the page length to show all entries
    $('#' + tableid).DataTable().page.len(-1).draw();

    // Get the table data
    var table = document.getElementById(tableid);
    var tableData = [];
    for (var i = 0, row; row = table.rows[i]; i++) {
        var rowData = [];
        for (var j = 0, col; col = row.cells[j]; j++) {
            rowData.push(col.innerText);
        }
        tableData.push(rowData);
    }

    // Create the PDF document
    var { jsPDF } = window.jspdf;
    var doc = new jsPDF();

    // Add the table to the PDF
    doc.autoTable({
        head: [tableData[0]],
        body: tableData.slice(1),
    });

    // Save the PDF
    doc.save('data.pdf');

    // Restore the original pagination state
    $('#' + tableid).DataTable().page.len(currentPageLength).draw();
    $('#' + tableid).DataTable().page(currentPage).draw(false);
}
function buttonStyleClassChangeWhileCheckBoxCheking() {
    buttonStyleClassChange('btnFormResultsSearch');
}

function addNumericColumnsClasstoDT(tableid, columnlist) {
    window.numericcolumnlist = columnlist;
    $('#' + tableid + ' tr').each(function () {
        for (var i = 0; i < columnlist.length; i++) {
            $(this).children('td').eq(columnlist[i]).addClass('numericColumns');
        }
    })
}

$(document).on('click', ".btnResetFormValues", function () {
    id = $(this).attr("form-id");
    $("#" + id + " #cbFillFromToDates").prop("checked", true);
    if ($("#" + id + " .resetFromDate").length > 0) {
        $("#" + id + " .resetFromDate").val("1900-01-01").prop('disabled', 'true');
    }
    if ($("#" + id + " .resetToDate").length > 0) {
        $("#" + id + " .resetToDate").val("2098-12-31").prop('disabled', 'true');
    }
    if ($("#" + id + " .resetTextField").length > 0) {
        $("#" + id + " .resetTextField").val("");
    }
    if ($("#" + id + " .resetSelectField").length > 0) {
        $("#" + id + " .resetSelectField").val("");
    }
    if ($("#" + id + " .resetDefaultCheckField").length > 0) {
        $("#" + id + " .resetDefaultCheckField").prop("checked", true);
    }
    if ($("#" + id + " .resetDefaultUncheckField").length > 0) {
        $("#" + id + " .resetDefaultUncheckField").prop("checked", false);
    }

    $("#" + id + " .iSByPickupDate").prop("checked", true);
    $("#" + id + " .chkExcludeDealers").prop("checked", false);

    buttonStyleClassChange('btnFormResultsSearch');
});

function getStylesImages(tableid, styleIndex) {
    WebImagesUrl = $("#WebImagesUrl").val();
    allstyles = '';
    $('#' + tableid + ' tbody tr').each(function () {
        if (allstyles != '') {
            allstyles += ",";
        }
        allstyles += "'" + $(this).children('td').eq(styleIndex).children('img').attr('data-style') + "'";
    })

    $.ajax({
        url: '../Home/GetMultiStylesImages',
        type: "GET",
        data: {
            styles: allstyles
        },
        success: function (res) {
            result = JSON.parse(res);
            $.each(result, function (i, item) {
                formattedStyle = item.Text;
                if (item.Text != null) {
                    formattedStyle = item.Text.replace("/", "_");
                }
                $(".showImage_" + formattedStyle).attr('src', WebImagesUrl + item.Value).removeClass('d-none');
            });
        }
    });
}

function formatStyleNumber(style) {
    style = style.replace("/", "_");
    return style;
}


function selectInvoice(invNo) {
    $("#selectInvoiceModal").hide();
    $('#txtInvoiceCode').val(invNo);

}

function selectInvoice(invNo, invAcc) {
    $("#selectInvoiceModal").hide();
    $('#txtInvoiceCode').val(invNo);
    $('#cust_code').val(invAcc);
}

$(document).on('click', ".closeInvoiceModal", function () {
    $("#selectInvoiceModal").hide();
});

function selectRepair(repairNo, Acc) {
    $("#selectRepairModal").hide();
    $('#txtRepairCode').val(repairNo);
    $('#cust_code').val(Acc);
}

function selectProformaInvoice(invNo, Acc) {
    $("#selectProformaInvoiceModal").hide();
    $('#txtProformaInvoiceCode').val(invNo);
    $('#cust_code').val(Acc);
}

$(document).on('click', ".closeRepairModal", function () {
    $("#selectRepairModal").hide();
});

$(document).on('click', ".closeProformaInvoiceModal", function () {
    $("#selectProformaInvoiceModal").hide();
});

function jsonToXml(table, jsonObj) {
    var xml = '';

    $.each(jsonObj, function (key, value) {
        if (!isNaN(key)) {
            key = table;
        }
        if (typeof value === 'object' && !Array.isArray(value)) {
            // If value is an object, recursively call the function
            xml += '<' + key + '>' + jsonToXml(table, value) + '</' + key + '>';
        } else {
            // Else, it's a simple key-value pair
            xml += '<' + key + '>' + value + '</' + key + '>';
        }
    });

    return xml;
}

function getCustomerNamebyCode(ccode) {
    $("#txtCustomerName").val('');
    $.ajax({
        url: '../Common/GetCustomerNameByCode',
        type: 'post',
        data: {
            acc: ccode
        },
        success: function (res) {
            $("#txtCustomerName").val(res);
        }
    })
}

function getPotCustomerNamebyCode(ccode) {
    $("#txtCustomerName").val('');
    $.ajax({
        url: '../Common/GetPotCustomerNameByCode',
        type: 'post',
        data: {
            acc: ccode
        },
        success: function (res) {
            $("#txtCustomerName").val(res);
        }
    })
}

$(document).on('keyup', ".phoneNumberInput", function () {
    $(this).val($(this).val().replace(/^(\d{3})(\d{3})(\d)+$/, "($1)$2-$3"));
})

function printNormalHtmlTable(divclass) {
    $('.printableContent').empty();
    $('.printableContent').html($('.' + divclass).html());
    $("#firstCommonBsModal").hide();
    $("#firstCommonBsModal").addClass('show');
    $(".main-content").hide();
    $('.printableContent').show();
    window.print();
    $(".main-content").show();
    $('.printableContent').hide();
    $("#firstCommonBsModal").show();
    $("#firstCommonBsModal").removeClass('show');
}

function previewNormalHtmlTable(divclass) {
    var divToPrint = $('.' + divclass).html();
    $('#previewContentModal .previewContent').empty();
    $('#previewContentModal .previewContent').html(divToPrint);
    $("#previewContentModal").show();
    $("#previewContentModal").addClass('show');
}

function pdfNormalHtmlDiv(divclass) {
    const { jsPDF } = window.jspdf;
    const pdf = new jsPDF();

    html2canvas(document.querySelector("." + divclass)).then(canvas => {
        // Get the image data from the canvas
        const imgData = canvas.toDataURL("image/png");

        // Set image to PDF with some optional positioning
        pdf.addImage(imgData, 'PNG', 10, 10, 190, 0); // Adjust position and size as needed

        // Save the PDF with a filename
        pdf.save("YJewel-Report.pdf");
    });
}

function excelFromNormalDivTable(divclass) {
    const table = document.querySelector("." + divclass);
    const workbook = XLSX.utils.table_to_book(table, { sheet: "Sheet1" });
    XLSX.writeFile(workbook, "YJewel-Report.xlsx");
}


function selectPo(PoNo, Acc) {
    $("#searchResultsForPO").html("");
    $("#selectPOModal").hide();
    $('#txtPON').val(PoNo);
    $('#txtOldAcc').val(Acc);

}

function searchPO() {
    var custAcc = $('#cCode_pon').val();
    var fromDt = $('#txtFromDate_pon').val();
    var toDt = $('#txtToDate_pon').val();

    $.ajax({
        url: '../WholesaleOrders/GetPos',
        type: 'GET',
        data: {
            CustAcc: custAcc,
            FromDate: fromDt,
            ToDate: toDt
        },
        success: function (result) {
            $('#searchResultsForPO').html(result);
        }
    });
}

$(document).on('click', ".closePOModal", function () {
    $("#selectPOModal").hide();
});


document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.first_ul').forEach(firstUl => {
        const allSecondLis = Array.from(firstUl.querySelectorAll('.second_li'));

        //const visibleSecondLis = allSecondLis.filter(li => {
        //    return !li.classList.contains('hidden') && li.offsetParent !== null;
        //});

        allSecondLis.forEach((item, index) => {
            item.addEventListener('click', () => {
                const submenu = item.querySelector('.third_ul');
                if (submenu) {
                    submenu.style.left = '';
                    submenu.style.right = '';
                    submenu.style.transform = '';
                    //submenu.style.display = submenu.style.display === 'block' ? 'none' : 'block';
                    //submenu.slideToggle();

                    if (index <= 4) {
                        submenu.style.left = '0';
                        submenu.style.right = 'auto';
                    } else if (index >= 5 && index <= 8) {
                        submenu.style.left = '50%';
                        submenu.style.right = 'auto';
                        submenu.style.transform = 'translateX(-50%)';
                    } else {
                        submenu.style.left = 'auto';
                        submenu.style.right = '0';
                    }
                }
            });
        });
    });
});
