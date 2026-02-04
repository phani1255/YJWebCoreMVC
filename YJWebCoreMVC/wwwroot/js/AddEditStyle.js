/*
* Created by Phanindra 27-Nov-2025
* Phanindra 12/15/2025 Added more functionality to Edit Style form.
* Phanindra 12/26/2025 Added more functionality to Edit Style form.
* Phanindra 01/05/2026 Worked on fixes in AddEdit Style form.
* Phanindra 01/09/2026 Worked on fixes in AddEdit Style form.
* Phanindra 01/13/2026 Worked on fixes in AddEdit Style form.
* Phanindra 01/15/2026 Worked on fixes in AddEdit Style form.
* Phanindra 01/20/2026 Worked on fixes in AddEdit Style form.
* Chakri    01/27/2026 Changes in btnSaveStyle function.
* Phanindra 01/27/2026 Worked on issues in Add Edit style form
*/

//const { selectors } = require("sizzle");


var is_edit = $('#is_edit').val();
var styleCases = $("#StyleCaseJson").val();
let caseText = '';

if (styleCases.length > 1) {
    caseText = 'MULTI';
} else if (styleCases.length === 1) {
    caseText = styleCases[0].Case;
}

$("#caseNo").val(caseText);

if (is_edit == false) {
    $('#cost_multi').val($('#multi').val());

    var nextRoundOff = $('#nextRoundOff').val();
    $('#opt_' + nextRoundOff).prop('checked', true);

    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    $("#txtEstDate").val(today);

    $('#glclass').val('ASSET');
    $("#qty_stock").val('0.00');
    $("#txtAlloc").val('0.00');
}

$(".commonCostTotalCls").prop('readonly', true);

$(document).on('change', '#chkOverWriteCost', function () {
    if ($(this).is(':checked')) {
        $(".commonCostTotalCls").prop('readonly', false);
    } else {
        $(".commonCostTotalCls").prop('readonly', true);
    }
});

var isRFID = $('#is_RFID').val();
if (isRFID)
    $('#rfid').prop('checked', true);

$(document).on('change', '#wtsDwt', function () {
    if ($(this).is(':checked')) {
        $('.WtsUnit').text('DWT');
    } else {
        $('.WtsUnit').text('Gr.');
    }
});

$(document).on('input', '#tagsToPrint', function () {
    // Remove everything except digits
    this.value = this.value.replace(/\D/g, '');
});

const COST_TYPES = ["CASTING", "FINDING", "LABOR", "SETTING", "STONE"];

function buildCostTypeDropdown(selectedValue = "") {

    let html = `<select class="form-control CostType">`;
    html += `<option value=""></option>`;

    COST_TYPES.forEach(type => {
        let selected = (type === selectedValue) ? "selected" : "";
        html += `<option value="${type}" ${selected}>${type}</option>`;
    });

    html += `</select>`;
    return html;
}

var VenderTypes = JSON.parse($("#VendorTypesForAutofill").html());
$(document).on("input", ".ddlVendorCls", function () {
    $(".vendor-autocomplete-items").show();

    var $curInput = $(this); // use local variable
    var query = $curInput.val().toLowerCase();
    var $list = $curInput.siblings(".vendor-autocomplete-items");
    $list.empty();

    if (!query) return;

    var results = VenderTypes.filter(function (item) {
        return item.Text.toLowerCase().indexOf(query) === 0;
    });

    results.forEach(function (item) {
        var $itemElement = $("<div>")
            .addClass("vendor-autocomplete-item")
            .text(item.Text)
            .on("mousedown", function () {
                $curInput.val(item.Text); // set value correctly
                $('#txtVendorName').val(item.Value);
                $list.empty();              // clear suggestion list
            });
        $list.append($itemElement);
    });
});

$(document).on("focusout", ".ddlVendorCls", function () {

    const $input = $(this);
    const typed = $input.val().trim().toLowerCase();
    const $list = $input.siblings(".vendor-autocomplete-items");

    // delay so mousedown can complete
    setTimeout(() => {

        if (!typed) {
            $("#txtVendorName").val("");
            $list.hide();
            return;
        }

        const match = VenderTypes.find(v =>
            v.Text.toLowerCase() === typed
        );

        if (match) {
            $input.val(match.Text);
            $("#txtVendorName").val(match.Value);
        } else {
            // invalid entry
            $("#txtVendorName").val("");
            // optional:
            // $input.val("");
        }

        $list.hide();

    }, 150);
});

$('.num2decimal').each(function () {
    let val = $(this).val();
    if (val === '' || isNaN(val)) {
        $(this).val('0.00');
    } else {
        $(this).val(parseFloat(val).toFixed(2));
    }
});

$(document).on('blur', '.num2decimal', function () {
    let val = $(this).val().trim();

    if (val === '' || isNaN(val)) {
        $(this).val('0.00');
    } else {
        $(this).val(parseFloat(val).toFixed(2));
    }
});

$(document).on('input', '.num2decimal', function () {
    let value = this.value;

    // Remove invalid characters
    value = value.replace(/[^0-9.]/g, '');

    // Allow only one decimal point
    const parts = value.split('.');
    if (parts.length > 2) {
        value = parts[0] + '.' + parts[1];
    }

    // Limit to 2 decimal places
    if (parts.length === 2) {
        parts[1] = parts[1].substring(0, 2);
        value = parts[0] + '.' + parts[1];
    }

    this.value = value;
});

let storeDropdownHtml = '';
if ($("#storeDropdownTemplate").length > 0)
    storeDropdownHtml = $("#storeDropdownTemplate").html();

let bomClasses = [];
let bomShapes = [];

bomClasses = $('#bomData').data('classes') || [];
bomShapes = $('#bomData').data('shapes') || [];

$(function () {
    let selectedStore = $('#StoreCodeInUse').val();
    if (selectedStore) {
        $('#storeNo').val(selectedStore);
    }
});

$(document).on('click', '.btnSaveStyle', function (e) {


    // Basic field values
    let styleNo = $('#styleNo').val();
    let glclass = $('#glclass').val();
    let itemType = $('#itemType').val().trim();
    let saveForm = $(this).val();

    if (!styleNo) {
        alert('Style # is required');
        return;
    }

    var costDetails = [];

    $(".CostDetailsBody tr").each(function () {

        var $row = $(this);

        var rowData = {
            material: $.trim($row.find(".costMeterial").val()),
            lot_no: $.trim($row.find(".CostLotNo").val()),
            quality: $.trim($row.find(".CostQuality").val()),
            Size: $.trim($row.find(".CostSize").val()),
            PCS: parseFloat($row.find(".CostPcs").val()) || 0,
            TotalWt: parseFloat($row.find(".CostTotalWt").val()) || 0,
            by_pc: $row.find(".CostByPC").prop("checked"),
            Price: parseFloat($row.find(".CostUnitCost").val()) || 0,
            Total: parseFloat($row.find(".CostTotal").val()) || 0,
            DoNotDeduct: $row.find(".CostDontDeduct").prop("checked"),
            Store_No: $row.find(".CostStoreNo").val() || "",
            Note: $.trim($row.find(".CostNote").val()),
            Unit_Price: parseFloat($row.find(".CostUnitPrice").val()) || 0,
            header_type: $.trim($row.find(".CostType").val()),
            Original_Stock: parseFloat($row.find(".CostPcs").val()) || 0,
            Original_Wt: parseFloat($row.find(".CostTotalWt").val()) || 0,
            ToDelete: false
        };
        costDetails.push(rowData);
    });


    var StoneMaterialXml = jsonToXml("STONESMATERIAL", costDetails);

    StoneMaterialXml =
        '<?xml version="1.0" encoding="utf-16"?>' +
        '<DocumentElement>' +
        StoneMaterialXml +
        '</DocumentElement>';

    let caseJson = $("#StyleCaseJson").val();
    let caseXml = styleCaseJsonToXml(caseJson);

    var StoneData = [];

    $(".BomDetailsBody tr").each(function () {

        var $row = $(this);

        var rowData = {
            material: $.trim($row.find(".bomClass").val()),
            lot_no: $.trim($row.find(".bomShape").val()),
            quality: parseFloat($row.find(".CostQuality").val()) || 0,
            Size: parseFloat($row.find(".bomSize").val()) || 0,
            PCS: $.trim($row.find(".bomShade").val()),
            TotalWt: parseFloat($row.find(".bomGrade").val()) || 0,
            TotalWt: parseFloat($row.find(".bomPriceLvel").val()) || 0,
            TotalWt: parseFloat($row.find(".bomPcs").val()) || 0,
            TotalWt: parseFloat($row.find(".bomWtEach").val()) || 0,
            TotalWt: parseFloat($row.find(".bomTotalWt").val()) || 0,
            Price: parseFloat($row.find(".bomPriceCt").val()) || 0,
            Total: parseFloat($row.find(".bomEach").val()) || 0,
            Unit_Price: parseFloat($row.find(".bomTotal").val()) || 0,
            DoNotDeduct: $row.find(".bomCtrStn").prop("checked"),
            Store_No: $row.find(".bomNote").val() || "",
            Note: $.trim($row.find(".bomStoneType").val()),
        };
        StoneData.push(rowData);
    });

    var dtStoneDataBom = jsonToXml("STONEDATABOM", StoneData);

    dtStoneDataBom =
        '<?xml version="1.0" encoding="utf-16"?>' +
        '<DocumentElement>' +
        dtStoneDataBom +
        '</DocumentElement>';

    var payload = $("#AddEditStyleMainForm").serializeArray();

    //payload.forEach(x => console.log(x.name, x.value));

    payload.push({
        name: "dtStyleCase",
        value: caseXml
    });

    payload.push({
        name: "StonesMaterialXml",
        value: StoneMaterialXml
    });

    payload.push({
        name: "dtStoneDataBom",
        value: dtStoneDataBom
    });

    $.ajax({
        url: '/Styles/SaveStyle',
        type: 'POST',
        data: payload,
        traditional: true, // important for arrays like att13_Selected
        success: function (resp) {
            if (!resp) {
                alert('No response from server');
                return;
            }
            if (!resp.success) {
                alert(resp.message || 'Error saving style.');
                return;
            }


            var isFromScrap = $('#FromScrap').val() === "true";

            if (isFromScrap) {

                var styleNo = $('#styleNo').val();

                if (window.top.currentStyleTargetRow) {
                    window.top.currentStyleTargetRow
                        .find('td:eq(10)')
                        .text(styleNo);
                }

                window.top.$('#StyleModal').modal('hide');

                return;
            }

            alert('Style saved successfully.');

            if (saveForm == 'save_form') {
                window.location.href = "../Styles/AddStyle";
            }


            // TODO: refresh your list/grid if needed
            // reloadStylesGrid();
        },
        error: function (xhr) {
            alert('Error saving style: ' + xhr.responseText);
        }
    });
});



$(document).on('click', '.gotoHome', function () {
    if (confirm("Ok to Discard the changes and Close This Form ?")) {
        window.location.href = "../Home/Index";
    }
})

$(document).on('click', '#btnAddStockNumbers', function () {
    $('.addStockForm').show();
})

$(document).on('click', '#btnAddStockOk', function () {
    let storeNo = $("#storeNo").val();
    let numItems = $("#txtNumberOfItems").val();
    let styleNo = $("#styleNo").val();

    if (!storeNo) {
        alert("Select the store.");
        return;
    }

    if (!numItems || !/^\d+$/.test(numItems)) {
        alert("Please enter a valid number of items (digits only).");
        return;
    }

    if (parseInt(numItems) < 1) {
        alert("The number of items must be 1 or greater.");
        return;
    }

    $.ajax({
        url: "/Styles/AddStockNumbers",
        type: "POST",
        data: {
            styleNo: styleNo,
            storeNo: storeNo,
            count: parseInt(numItems)
        },
        success: function (resp) {
            if (!resp.success) {
                alert(resp.message);
                return;
            }

            // Append the new stock numbers to the table
            resp.stockNumbers.forEach(function (num) {
                $("#stockGrid tbody").append(`
                    <tr>
                        <td>${num}</td>
                        <td>1.00</td>
                        <td>${storeNo}</td>
                        <td><input type="checkbox"></td>
                        <td></td>
                    </tr>
                `);
            });

            // Update total quantity
            $("#txtQtyStock").val(resp.qtyStock);

            // Reset & hide
            $("#txtAddItems").val("");
            $("#AddStockModal").hide();
        },
        error: function (xhr) {
            alert("Error: " + xhr.responseText);
        }
    });
});

function populateStyle(json, partialLoading) {

    if (!json) return;

    fillFirstTabStyleFormFields(json);
    fillSecondTabStyleFormFields(json);
    fillThirdTabStyleFormFields(json);
    if (partialLoading === 0) {
        fillCostDetailsTabFields(json);
        //fillStoneDetailsGrid(json);
        LoadBOMTab(json);
        LoadBOMTOTALTab(json);
        LoadStockNumbers();
    }
    getcastmetal();
}

function loadStyleInfo(style, partialLoading = 0) {

    if (!style || style.trim() === "") {
        console.log("ADD mode: skipping loadStyleInfo");
        return;
    }

    $.ajax({
        url: '../Styles/GetStyleData',
        type: 'post',
        data: { Style: style },
        success: function (response) {

            if (!response) {
                console.log("No response (ADD mode)");
                return;
            }

            let parsed;
            try {
                parsed = typeof response === "string" ? JSON.parse(response) : response;
            } catch (e) {
                console.error("Invalid JSON", response);
                return;
            }

            if (!parsed || !parsed.length) {
                console.log("No style data found (ADD mode)");
                return;
            }

            let json = parsed[0];

            populateStyle(json, partialLoading);
        }
    });
}


function toNumber(val) {
    let num = parseFloat(val);
    return isNaN(num) ? 0 : num;
}

function format2(val) {
    return parseFloat(val).toFixed(2);
}

function clampDuty(val) {
    if (val < 0) return 0;
    if (val > 99.99) return 99.99;
    return val;
}

function recalcCosts() {

    let purchasePrice = toNumber($('#purchase_price').val());

    /* =========================
       1️⃣ Duties %
    ========================== */
    let dutyPercent = clampDuty(toNumber($('#duties').val()));
    $('#duties').val(format2(dutyPercent));

    let dutyAmount = purchasePrice * (dutyPercent / 100);
    $('#duties_amount').val(format2(dutyAmount)); // NOT included in cost-field

    /* =========================
       2️⃣ Total Cost
       (sum ONLY raw cost inputs)
    ========================== */
    let totalCost =
        purchasePrice +
        dutyAmount +
        toNumber($('#gems_gen_cost').val()) +
        toNumber($('#labor_cost').val()) +
        toNumber($('#fieldval1').val()) +
        toNumber($('#fieldval2').val()) +
        toNumber($('#fieldval3').val());

    $('#totalCost').val(format2(totalCost));

    /* =========================
       3️⃣ Retail Price
    ========================== */
    let multiplier = toNumber($('#cost_multi').val());
    $('#price').val(format2(totalCost * multiplier));

    /* =========================
       4️⃣ Replacement Cost
    ========================== */
    if (!$('#differentReplacement').is(':checked')) {
        $('#txtReplacementCost')
            .val(format2(totalCost))
            .prop('readonly', true);
    }
}

// 🔔 Recalculate when any cost-related input changes
$(document).on(
    'blur change',
    '#purchase_price, #duties, #gems_gen_cost, #labor_cost, #fieldval1, #fieldval2, #fieldval3',
    recalcCosts
);

$('#differentReplacement').on('change', function () {
    if ($(this).is(':checked')) {
        $('#txtReplacementCost')
            .val('0.00')
            .prop('readonly', false)
            .focus();
    } else {
        $('#txtReplacementCost').prop('readonly', true);
        recalcCosts();
    }
});

$('#totalCost, #qty_stock, #txtAlloc, #price').prop('readonly', true);

recalcCosts();


function sval(v) {
    return (v !== undefined && v !== null) ? v : '';
}

function nval(v, decimals = 2) {
    return (v !== undefined && v !== null && !isNaN(v))
        ? Number(v).toFixed(decimals)
        : Number(0).toFixed(decimals);
}

function setRoundNearest(val) {

    //let val = json.NextRoundOff;

    // normalize "none"
    if (val === null || val === undefined || val === 0 || val === "0") {
        val = "none";
    }

    $(`input[name="roundNearest"][value="${val}"]`)
        .prop("checked", true);
}


function fillFirstTabStyleFormFields(json) {
    if ($("#isEdit").val() == "") {
        $('#glclass').val(json.CLASS_GL ? sval(json.CLASS_GL) : 'ASSET');
    } else {
        $('#glclass').val(sval(json.CLASS_GL));
    }

    $('#attr1_sel').val(sval(json.Attrib1));
    $('#attr2_sel').val(sval(json.Attrib2));
    $('#attr3_sel').val(sval(json.Attrib3));
    $('#attr4_sel').val(sval(json.Attrib4));
    $('#attr5_sel').val(sval(json.Attrib5));
    $('#attr6_sel').val(sval(json.Attrib6));
    $('#attr7_sel').val(sval(json.Attrib7));
    $('#attr8_sel').val(sval(json.Attrib8));
    $('#attr9_sel').val(sval(json.Attrib9));
    $('#attr10_sel').val(sval(json.Attrib10));
    $('#attr11_sel').val(sval(json.Attrib11));
    $('#attr12_sel').val(sval(json.Attrib12));
    $('#attr19_sel').val(sval(json.Attrib19));
    $('#attr20_sel').val(sval(json.Attrib20));
    $('#attr21_sel').val(sval(json.Attrib21));

    $("#descr").val(json.DESC);
    $("#description").val(json.DESC);
    $("#longDescription").val(json.LONGDESC);


    $("#price").val(json.PRICE ? json.PRICE.toFixed(2) : '0.00');
    $("#brands").val(sval(json.brand));
    $("#subbrand").val(sval(json.SUBBRAND));

    $("#min_price").val(json.MIN_PRICE ? json.MIN_PRICE.toFixed(2) : '0.00');
    $("#web_price").val(json.WEB_PRICE ? json.WEB_PRICE.toFixed(2) : '0.00');
    //if (Helper.is_Catherine)
    $("#itemType").val(sval(json.ITEM_TYPE));
    //else
    //    $("#itemType").val(sval(json.ITEM_TYPE).ToString() == string.Empty ? "Jewelry" : json.ITEM_TYPE);
    $("#case_no").val(sval(json.case_no));
    //case_no.$("#IsReadOnly").val(sval(case_no.Text).ToLower().Contains("multi"));
    //Group.SelectedIndexChanged -= Group_SelectedIndexChanged;
    //cate_gory.SelectedIndexChanged -= cate_gory_SelectedIndexChanged;
    $("#Group").val(sval(json.group));
    $("#cate_gory").val(sval(json.CATEGORY));
    //Group.SelectedIndexChanged += Group_SelectedIndexChanged;
    //cate_gory.SelectedIndexChanged += cate_gory_SelectedIndexChanged;

    setRoundNearest(json.NextRoundOff);

    $("#sub_category").val(sval(json.SUBCAT));
    $("#stylesize").val(sval(json.fingersize));
    $("#year").val(sval(json.year));
    $("#size").val(sval(json.bullionsize));
    $("#metal").val(sval(json.METAL));
    //if (Helper.CompanyName.ToUpper().Contains("JADE") && string.IsNullOrWhiteSpace(metal.Text))
    //    $("#metal").val("14K");
    $("#ddlmetalcolor").val(sval(json.MetalColor));
    $("#cast_no").val(sval(json.VND_STYLE));
    $("#cast_code").val(sval(json.CAST_CODE));
    $("#comboRoyalPerson").val(sval(json.ROYAL_PERSON));
    $("#textBoxRoyalty").val(sval(json.ROYALTY));
    $("#centerst_type").val(sval(json.center_type));
    $("#centerstSub_type").val(sval(json.center_stype));
    $("#centerst_shape").val(sval(json.SHAPE));
    $("#centerst_size").val(sval(json.CT_WEIGHT));
    $("#cbxCenter_size").val(sval(json.center_size));
    //radCheckBox1.$("#Checked").val(json.IS_DWT);
    //checkBox1.$("#Checked").val(json.IS_DWT_POT);
    //checkBox2.$("#Checked").val(json.IS_CWT_POT);
    $("#gold_wt").val(sval(json.GOLD_WT));
    $("#popupnote").val(sval(json.POPUPNOTE));
    $("#txtVideoLink").val(sval(json.videolink));
    //if (json.sharedinv) {
    //    $("#txtSharedInevntory").val(json.sharedinv);
    //}
    //$("#txtSharedInevntory").val(stylerow.Table.Columns.Contains("sharedinv") ? json.sharedinv);
    //if (iSCopyOld)
    //    $("#qty_stock").val(Helper.is_Auto_stock ? 1 : 0);
    //else
    //    $("#qty_stock").val(mode == "insert" ? 0 : Helper.Is_Symphony ? GetStoreInStock() : Helper.DecimalCheckForDBNull(json.IN_STOCK));
    $("#qty_stock").val(sval(json.IN_STOCK));
    $("#wt_stock").val(sval(json.WT_STOCK));
    if ($("#isEdit").val() == "") {
        let today = new Date().toISOString().split('T')[0];
        $("#txtEstDate").val(today);
    } else {
        $("#txtEstDate").val(json.DATE.split('T')[0]);
    }
    //$("#txtEstDate").val((json.DATE == DBNull.Value || mode == "insert") ? DateTime.Now : Convert.ToDateTime(json.DATE));
    $("#silver_wt").val(sval(json.SLVR_WT));
    $("#plat_wt").val(sval(json.Plat_wt));
    $("#gems_gen_cost").val(sval(json.GEM_COST));

    $("#note1").val(sval(json.NOTE));
    $("#diamond_wt").val(sval(json.STONE_WT));
    $("#diamond_type").val(sval(json.DQLTY));
    //if (Helper.CompanyName.ToUpper().Contains("JADE") && string.IsNullOrWhiteSpace(diamond_type.Text))
    //    $("#diamond_type").val("14K");
    $("#color_wt").val(sval(json.COLOR_WT));
    $("#color_type").val(sval(json.CQLTY));
    $("#tagInfo1").val(sval(json.tag_info1));
    $("#tagInfo2").val(sval(json.tag_info2));
    $("#tagInfo3").val(sval(json.tag_info3));
    $("#tagInfo4").val(sval(json.tag_info4));

    $("#labor_cost").val(sval(json.MISC));
    $("#field1").val(sval(json.fieldtext1));
    $("#field2").val(sval(json.fieldtext2));
    $("#field3").val(sval(json.fieldtext3));

    $("#fieldval1").val(json.fieldvalue1 !== "" ? json.fieldvalue1 : 0);
    $("#fieldval2").val(json.fieldvalue2 !== "" ? json.fieldvalue2 : 0);
    $("#fieldval3").val(json.fieldvalue3 !== "" ? json.fieldvalue3 : 0);
    $("#fieldval4").val(sval(json.fieldvalue4));
    $("#fieldval5").val(sval(json.fieldvalue5));
    $("#fieldval6").val(sval(json.FIELDVALUE6));
    $("#fieldval7").val(sval(json.FIELDVALUE7));
    $("#fieldval8").val(sval(json.FIELDVALUE8));

    $("#fieldVal20").val(sval(json.FieldVal20));
    $("#fieldVal21").val(sval(json.FieldVal21));
    $("#fieldVal22").val(sval(json.FieldVal22));
    $("#fieldVal23").val(sval(json.FieldVal23));
    $("#fieldVal24").val(sval(json.FieldVal24));
    $("#fieldVal25").val(sval(json.FieldVal25));
    $("#fieldVal26").val(sval(json.FieldVal26));
    $("#fieldVal27").val(sval(json.FieldVal27));
    $("#fieldVal28").val(sval(json.FieldVal28));
    $("#fieldVal29").val(sval(json.FieldVal29));

    $("#watch_brand").val(sval(json.brand));
    $("#watch_year").val(sval(json.year));
    $("#watch_desc").val(sval(json.DESC));
    $("#watch_metal").val(sval(json.METAL));
    $("#watch_longdesc").val(sval(json.LONGDESC));
    $("#html_desc").val(sval(json.html_desc));
    $("#watch_instock").val(sval(json.IN_STOCK));
    $("#sku").val(sval(json.STYLE));
    $("#Joma_SKU").val(sval(json.joma_sku));
    $("#watch_model").val(sval(json.model));
    $("#reference_number").val(sval(json.reference_number));
    $("#serial_number").val(sval(json.serial_number));
    $("#upc").val(sval(json.UPC));
    $("#country").val(sval(json.country));
    $("#dispatchtimemax").val(sval(json.dispatchtimemax));
    $("#style_type").val(sval(json.style_type));
    $("#gender").val(sval(json.gender));
    $("#Condition").val(sval(json.Condition));
    $("#html_desc").val(sval(json.html_desc));
    $("#Calendar").val(sval(json.Calendar));
    $("#movement").val(sval(json.Movement));
    $("#Power_Reserve").val(sval(json.Power_Reserve));
    $("#Luminance").val(sval(json.Luminance));
    $("#W_R_Depth").val(sval(json.W_R_Depth));

    $("#W_R_Unit").val(sval(json.W_R_Unit));
    $("#B_Type").val(sval(json.B_Type));
    $("#B_Material").val(sval(json.B_Material));
    $("#B_Color").val(sval(json.B_Color));
    $("#B_Length").val(sval(json.B_Length));
    $("#B_Length_Unit").val(sval(json.B_Length_Unit));
    $("#B_Width").val(sval(json.B_Width));
    $("#B_Width_Unit").val(sval(json.B_Width_Unit));
    $("#B_Number_of_links").val(sval(json.B_Number_of_links));
    $("#C_Material").val(sval(json.C_Material));
    $("#C_Color").val(sval(json.C_Color));
    $("#C_Shape").val(sval(json.C_Shape));
    $("#C_Finish").val(sval(json.C_Finish));
    $("#C_Back").val(sval(json.C_Back));
    $("#C_Crown").val(sval(json.C_Crown));
    $("#C_Diameter").val(sval(json.C_Diameter));
    $("#C_Diameter_Unit").val(sval(json.C_Diameter_Unit));


    $("#C_Thickness").val(sval(json.C_Thickness));
    $("#C_Thickness_Unit").val(sval(json.C_Thickness_Unit));
    $("#I_Type").val(sval(json.I_Type));
    $("#I_Material").val(sval(json.I_Material));
    $("#I_Function").val(sval(json.I_Function));
    $("#T_Type").val(sval(json.T_Type));
    $("#T_Material").val(sval(json.T_Material));
    $("#T_Code").val(sval(json.T_Code));
    $("#D_Type").val(sval(json.D_Type));
    $("#D_Material").val(sval(json.D_Material));
    $("#D_Color").val(sval(json.D_Color));
    $("#D_Crystal").val(sval(json.D_Crystal));
    $("#D_Features").val(sval(json.D_Features));
    $("#D_Functions").val(sval(json.D_Functions));
    $("#Offline_price").val(sval(json.Offline_price));
    $("#TheCollective").val(sval(json.TheCollective));
    $("#watchduration").val(sval(json.Duration));
    $("#BestOfferEnabled").val(sval(json.BestOfferEnabled));
    $("#Amazon_Price").val(sval(json.Amazon_Price));
    $("#Amazon_CA_Price").val(sval(json.Amazon_CA_Price));
    $("#ebay_price").val(sval(json.Ebay_price));
    $("#Main_Image_URL").val(sval(json.Main_Image_URL));
    $("#Back_Image_URL").val(sval(json.Back_Image_URL));
    $("#Side_Image_URL").val(sval(json.Side_Image_URL));
    $("#Serial_Image_URL").val(sval(json.Serial_Image_URL));
    $("#Other_Image_URL_1").val(sval(json.Other_Image_URL_1));

    $("#Other_Image_URL_2").val(sval(json.Other_Image_URL_2));
    $("#Other_Image_URL_3").val(sval(json.Other_Image_URL_3));
    $("#Other_Image_URL_4").val(sval(json.Other_Image_URL_4));
    $("#Authenticity").val(sval(json.Authenticity));
    $("#Box_Papers").val(sval(json.Box_Papers));
    $("#External_Wear").val(sval(json.External_Wear));
    $("#functionality").val(sval(json.Functionality));
    $("#Water_Testing").val(sval(json.Water_Testing));
    $("#Inspection_comment").val(sval(json.Inspection_comment));
    $("#Format").val(sval(json.Format));
    $("#Ebay_Minimum_Price").val(sval(json.Ebay_Minimum_Price));
    $("#Shipping").val(sval(json.Shipping));
    $("#ShippingService_1_Option").val(sval(json.ShippingService_1_Option));
    $("#ShippingService_1_Cost").val(sval(json.ShippingService_1_Cost));
    $("#Returns").val(sval(json.Returns));
    $("#ReturnsWithinOption").val(sval(json.ReturnsWithinOption));
    $("#RefundOption").val(sval(json.RefundOption));
    $("#ShippingCostPaidByOption").val(sval(json.ShippingCostPaidByOption));
    $("#RestockingFeeValueOption").val(sval(json.RestockingFeeValueOption));
    $("#IntlShippingService_1_Option").val(sval(json.IntlShippingService_1_Option));
    $("#IntlShippingService_1_Cost").val(sval(json.IntlShippingService_1_Cost));
    $("#IntlShippingService_1_Locations").val(sval(json.IntlShippingService_1_Location));
    $("#MPN").val(sval(json.MPN));
    $("#StoreCategory").val(sval(json.StoreCategory));
    $("#StoreCategory2").val(sval(json.StoreCategory2));

    $("#UseTaxTable").val(sval(json.UseTaxTable));
    $("#Shipping_Template").val(sval(json.Shipping_Template));
    //checkJomashop.$("#Checked").val(json.IS_JOMASHOP);
    //checkWatchfacts.$("#Checked").val(json.IS_WATCHFACTS);
    //$("#lOnWatchFact").val(json.on_watchfact);
    //$("#lblOnWatchFact").val(json.on_watchfact ? "On W.F." : "Not on W.F.");
    //$("#lOnJomashop").val(json.on_jomashop);
    //$("#lblOnJomashop").val(lOnJomashop ? "On JomaShop" : "Not on JomaShop");

    //chkUseCostDetailsFrmHere.$("#Checked").val((stylerow != null && !Helper.CheckModuleEnabled(Helper.Modules.PriceBasedOnGold))) ? !json.PCOMPLETE;
    //chktakepricefromhere.$("#Checked").val(stylerow != null ? json.IsPriceTaken);
    //chkOverWriteCost.$("#Checked").val(stylerow != null ? (json.OVER_WT != "FALSE") : false);
    $("#purchase_price").val(sval(json.COST));

    $("#txtsuppliercost").val(sval(json.Supplier_Cost));
    $("#txtVatAmount").val(sval(json.VAT_Amount));
    $("#txtUsdprice").val(sval(json.COST));
    $("#txtUsdConvRate").val(sval(json.Conversion_Rate));
    $("#txtMargins").val(sval(json.Margin));
    $("#txtsalesTaxRate").val(sval(json.SalesTax_Rate));
    $("#ourPart").val(sval(json.part_no));
    $("#subPart").val(sval(json.subpart));
    $("#dutiestxtcustoms").val(sval(json.DUTIES));
    $("#total_cost").val(sval(json.T_COST));
    $("#txtPcsInSet").val(sval(json.pcs_set));
    $("#txtDiamondWt").val(sval(json.diamond_wt));
    $("#txtUnCutDiamondWt").val(sval(json.uncut_diam_wt));
    //ChkDifferentReplaceCost.$("#Checked").val(json.DIFF_REPLACE);
    $("#txtReplacementCost").val(sval(json.REPLACE_COST));
    $("#replaceCost").val(sval(json.REPLACE_COST));
    $("#cost_multi").val(sval(json.MULTI));

    $("#color").val(sval(json.COLOR));
    $("#quality").val(sval(json.QUALITY));
    //$("#mounted").val(json.MOUNTED);

    //if (string.IsNullOrWhiteSpace(json.CLASS_GL))
    //    $("#glclass").val("ASSET");
    //autodesc.$("#Checked").val(json.AUTO_DESC);
    if (json.auto_descTemplate == "") {
        $("#autoDescTemplate").val("Default");
    } else {
        $("#autoDescTemplate").val(sval(json.auto_descTemplate));
    }
    //$("#autoDescTemplate").val(string.IsNullOrEmpty(Convert.ToString(json.auto_descTemplate)) ? "Default" : json.auto_descTemplate);
    //chkSoldByPiece.$("#Checked").val(json.by_pc);
    //chkMetalByGross.$("#Checked").val(json.Metal_gross);
    //chkLaborByGross.$("#Checked").val(json.Labor_gross);
    //radCheckBox2.$("#Checked").val(json.discontinued);
    //chkNonInventory.$("#Checked").val(json.NOT_STOCK);
    //chkNoSalesTax.$("#Checked").val(json.no_tax);
    //chkNoWarranty.$("#Checked").val(json.no_warranty);
    //diamond_wt.$("#ReadOnly").val(color_wt.$("#ReadOnly").val(false);

    $("#txtspclorderno").val(sval(json.Special_Order_No));
    $("#D_Marker").val(sval(json.D_MARKER));
    $("#txtespprice").val(sval(json.ESP));

    if (json.NO_COMSION !== false) {
        $("#chkNoCommission").prop('checked', true);
    }
    if (json.no_stkno !== false) {
        $("#chkNoStockNos").prop('checked', true);
    }
    if (json.no_discount !== false) {
        $("#chkNoDiscount").prop('checked', true);
    }
    $("#color").val(sval(json.COLOR));
    $("#quality").val(sval(json.QUALITY));
    //if (json.mfg_det) {
    //    $("#txtMfgDetails").val(json.mfg_det);
    //}
    //calc_markup();
    //$("#dtStyleCase").val(Helper.GetStylesCase(styleno.Text.Trim()));
    //ddlDisclaimer.$("#SelectedIndex").val(ddlDisclaimer.FindStringExact(json.Disclaimer));
    //if (Helper.is_DiamondDealer)
    //    chkPriceByWt.$("#Checked").val(Convert.ToBoolean(json.PRICEBYWT);
    //itemType_SelectedIndexChanged(null, null);

    //if (Helper.CheckModuleEnabled(Helper.Modules.GroupBasedOnType)) {
    //    $("#Group").val(json.group);
    //    $("#cate_gory").val(json.CATEGORY);
    //    $("#sub_category").val(json.SUBCAT);
    //    $("#metal").val(json.METAL);
    //}
    //$("#mounted_stock").val(Helper.GetMountedStock(styleno.Text));

    //if (json.Tag_Template);
    $("#ddlTempName").val(sval(json.Tag_Template));
    $("#oldCatForSinger").val(sval(json.CATEGORY));
    $("#oldMetalForSinger").val(sval(json.METAL));
    $("#notStock").val(sval(json.NOT_STOCK));

    //-- getting Vendor Nameby Code.
    $.ajax({
        url: '../Styles/GetVendorNameByCode',
        data: { acc: json.CAST_CODE },
        type: 'post',
        success: function (result) {
            $("#txtVendorName").val(sval(result));
        }
    })
}

function fillSecondTabStyleFormFields(json) {
    $("#rap_shape").val(sval(json.SHAPE));
    $("#rap_shape2").val(sval(json.SHAPE));
    $("#rap_color").val(sval(json.COLOR));
    $("#rap_clarity").val(sval(json.QUALITY));
    $("#color_to").val(sval(json.COLOR_TO));
    $("#clarity_to").val(sval(json.QUALITY_TO));
    $("#rap_weight").val(sval(json.ct_weight));
    //$("#styleLastUpdate").val(json._LastUpdate);
    $("#centerst_size").val(sval(json.ct_weight));

    $("#certtype").val(sval(json.CERT_TYPE));
    $("#certno").val(sval(json.CERT_NO));
    $("#rap_culet").val(sval(json.CT_CUT));
    $("#rap_depth").val(sval(json.CT_DEPTH));
    $("#rap_table").val(sval(json.CT_TABLE));
    $("#rap_gridle").val(sval(json.CT_GIRDLE));
    $("#rap_polish").val(sval(json.CT_POLISH));
    $("#rap_symmetry").val(sval(json.SYMMETRY));
    $("#rap_cutgrade").val(sval(json.GRADE));
    $("#rap_flour").val(sval(json.FLOUR));
    $("#rap_comment").val(sval(json.CT_NOTE));

    $("#fancycolor").val(sval(json.FAN_CLR));
    $("#fcolor_intensity").val(sval(json.FAN_INT));
    $("#fcolor_overtone").val(sval(json.FAN_OVER));

    $("#gemexCert").val(sval(json.GEMEX_CERT));
    $("#gemexFire").val(sval(json.Gemex_Fire));
    $("#gemexSparkle").val(sval(json.Gemex_Sparkle));
    $("#gemexBrilliance").val(sval(json.Gemex_Brilliance));

    if (json.ON_RAP != '') {
        $("#upload_rap").prop('checked', true);
    }

    $("#rap_listprice").val(sval(json.RAPPRICE));
    $("#rap_disc").val(sval(json.RAP_DISC));
    $("#rap_targetprice").val(sval(json.RAPPRICE));
    //Update_Target();

    $("#crwn_angl").val(sval(json.crwn_angl));
    $("#crwn_hght").val(sval(json.crwn_hght));
    $("#pvln_angl").val(sval(json.pvln_angl));
    $("#pvln_dpth").val(sval(json.pvln_dpth));
    $("#star_len").val(sval(json.star_len));
    $("#lowr_hlf").val(sval(json.lowr_hlf));
    $("#flour_color").val(sval(json.flour_c));

    $("#certtype2").val(sval(json.CERT_TYPE2));
    $("#certno2").val(sval(json.CERT_NO2));
    $("#rap_culet2").val(sval(json.CT_CUT2));
    $("#rap_measure").val(sval(json.size));
    $("#rap_measure2").val(sval(json.size2));
    $("#rap_weight2").val(sval(json.ct_weight2));
    $("#rap_depth2").val(sval(json.CT_DEPTH2));
    $("#rap_table2").val(sval(json.CT_TABLE2));
    $("#rap_gridle2").val(sval(json.CT_GIRDLE2));
    $("#rap_polish2").val(sval(json.CT_POLISH2));
    $("#rap_symmetry2").val(sval(json.SYMMETRY2));
    $("#rap_cutgrade2").val(sval(json.CUT_RATE2));
    $("#rap_clarity2").val(sval(json.QUALITY2));
    $("#rap_color2").val(sval(json.COLOR2));
    $("#crwn_angl2").val(sval(json.crwn_angl2));
    $("#crwn_hght2").val(sval(json.crwn_hght2));
    $("#pvln_angl2").val(sval(json.pvln_angl2));
    $("#pvln_dpth2").val(sval(json.pvln_dpth2));
    $("#star_len2").val(sval(json.star_len2));
    $("#lowr_hlf2").val(sval(json.lowr_hlf2));
    $("#rap_flour2").val(sval(json.FLOUR2));
    $("#flour_color2").val(sval(json.FLOUR_C2));
    $("#rap_comment2").val(sval(json.CT_NOTE2));
    $("#depth_gem").val(sval(json.DEPTH));
    $("#depth_gem2").val(sval(json.DEPTH2));
    $("#fancycolor2").val(sval(json.FANCY_CLR2));
    $("#fcolor_intensity2").val(sval(json.FAN_INT2));
    $("#chkDefaultCert2").val(sval(json.USE_CERT2));
    $("#chkLabGrown").val(sval(json.LabGrown));

}

function fillThirdTabStyleFormFields(json) {

    $("#attr_checkBox1").val(sval(json.Attr_Check1));
    $("#attr_checkBox2").val(sval(json.Attr_Check2));
    $("#attr_checkBox3").val(sval(json.Attr_Check3));
    $("#attr_checkBox4").val(sval(json.Attr_Check4));
    $("#attr_checkBox5").val(sval(json.Attr_Check5));
    $("#attr_checkBox6").val(sval(json.Attr_Check6));
    $("#partner1").val(sval(json.partner1));
    $("#partner2").val(sval(json.partner2));
    $("#partner3").val(sval(json.partner3));
    $("#partner4").val(sval(json.partner4));
    $("#part_share1").val(sval(json.part_share1));
    $("#part_share2").val(sval(json.part_share2));
    $("#part_share3").val(sval(json.part_share3));
    $("#part_share4").val(sval(json.part_share4));
    $("#chkSoldByPartner").val(sval(json.SLDPRTNR));
    $("#chkPartner1Profit").val(sval(json.PRTNR1PRFT));
    $("#chkPartner2Profit").val(sval(json.PRTNR2PRFT));
    $("#chkPartner3Profit").val(sval(json.PRTNR3PRFT));
    $("#chkPartner4Profit").val(sval(json.PRTNR4PRFT));
    $("#txtDateSold").val(sval(json.DATE_SOLD));
    $("#txtPriceSold").val(sval(json.PRC_SOLD));
    //txteBayListingId.Text = eBaylistingId;
}

function fillCostDetailsTabFields(json) {

    $("#txtStoneCost").val(json.stone_cost ? json.stone_cost.toFixed(2) : '0.00');
    $("#txtLaborCost").val(json.labor_cost ? json.labor_cost.toFixed(2) : '0.00');

    $("#txtCastingCost").val(json.CASTING ? json.CASTING.toFixed(2) : '0.00');
    $("#txtSettingCost").val(json.SETTING ? json.SETTING.toFixed(2) : '0.00');
    $("#txtFindingCost").val(json.FIND_COST ? json.FIND_COST.toFixed(2) : '0.00');

    $("#txtRhodiumCost").val(json.ROD_CHRG ? json.ROD_CHRG.toFixed(2) : '0.00');
    $("#txtPolishCost").val(json.POLISH ? json.POLISH.toFixed(2) : '0.00');
    $("#txtCertCharges").val(json.LASER ? json.LASER.toFixed(2) : '0.00');

    $("#txtFieldValue1").val(nval(json.fieldval1_cost));
    $("#txtFieldValue2").val(nval(json.fieldval2_cost));
    $("#txtFieldValue3").val(nval(json.fieldval3_cost));
    $("#txtMiscCost").val(nval(json.MISC1));

    $("#txtField1").val(sval(json.field1txt_cost));
    $("#txtField2").val(sval(json.field2txt_cost));
    $("#txtField3").val(sval(json.field3txt_cost));

    $("#txtAssemblyCost").val(json.ASSEMBLY_COST ? json.ASSEMBLY_COST.toFixed(2) : '0.00');

    // lefk
    //if (Helper.is_Lefk) {
    $("#LefkstylesCost1").val(json.cost1 ? json.cost1.toFixed(2) : '0.00');
    $("#LefkstylesCost2").val(json.cost2 ? json.cost2.toFixed(2) : '0.00');
    $("#LefkstylesCost3").val(json.cost3 ? json.cost3.toFixed(2) : '0.00');
    $("#LefkstylesCost4").val(json.cost4 ? json.cost4.toFixed(2) : '0.00');
    $("#LefkstylesCost5").val(json.cost5 ? json.cost5.toFixed(2) : '0.00');
    $("#LefkstylesCost6").val(json.cost6 ? json.cost6.toFixed(2) : '0.00');
    $("#LefkstylesCost7").val(json.cost7 ? json.cost7.toFixed(2) : '0.00');
    $("#LefkstylesCost8").val(json.cost8 ? json.cost8.toFixed(2) : '0.00');
    $("#LefkstylesCost9").val(json.cost9 ? json.cost9.toFixed(2) : '0.00');
    $("#LefkstylesCost10").val(json.cost10 ? json.cost10.toFixed(2) : '0.00');
    $("#LefkstylesCost11").val(json.cost11 ? json.cost11.toFixed(2) : '0.00');
    $("#LefkstylesCost12").val(json.cost12 ? json.cost12.toFixed(2) : '0.00');

    $("#Lefkups_StyleCost13").val(sval(json.FIELDTEXT1));
    $("#Lefkups_StyleCost14").val(sval(json.FIELDTEXT2));
    $("#Lefkups_StyleCost15").val(sval(json.FIELDTEXT3));

    $("#LefkstylesCost13").val(sval(json.FIELDVALUE1));
    $("#LefkstylesCost14").val(sval(json.FIELDVALUE2));
    $("#LefkstylesCost15").val(sval(json.FIELDVALUE3));

    $("#lefkcmb1").val(sval(json.Attrib_Diamond));
    $("#lefkcmb2").val(sval(json.Attrib_colorgem));
    $("#lefkcmb3").val(sval(json.Attrib_casting));
    $("#lefkcmb4").val(sval(json.Attrib_jeweler));
    $("#lefkcmb5").val(sval(json.Attrib_setting));
    $("#lefkcmb6").val(sval(json.Attrib_model));
    //}
    //else {
    $("#stylesCost1").val(json.cost1 ? json.cost1.toFixed(2) : '0.00');
    $("#stylesCost2").val(json.cost2 ? json.cost2.toFixed(2) : '0.00');
    $("#stylesCost3").val(json.cost3 ? json.cost3.toFixed(2) : '0.00');
    $("#stylesCost4").val(json.cost4 ? json.cost4.toFixed(2) : '0.00');
    $("#stylesCost5").val(json.cost5 ? json.cost5.toFixed(2) : '0.00');
    $("#stylesCost6").val(json.cost6 ? json.cost6.toFixed(2) : '0.00');
    $("#stylesCost7").val(json.cost7 ? json.cost7.toFixed(2) : '0.00');
    $("#stylesCost8").val(json.cost8 ? json.cost8.toFixed(2) : '0.00');
    $("#stylesCost9").val(json.cost9 ? json.cost9.toFixed(2) : '0.00');
    $("#stylesCost10").val(json.cost10 ? json.cost10.toFixed(2) : '0.00');
    //}
    //CostValidating(); // has to be implemented

    $.ajax({
        url: '../Styles/GetStyleCostDetailsTabGridData',
        type: 'post',
        data: { style: json.STYLE },
        success: function (result) {
            resultjson = $.parseJSON(result);
            dtSDM = resultjson.dtStoneDataMaterial;
            copyRadGridViewMaterial = resultjson.copyRadGridViewMaterial;

            repairRow = "";
            autonumforcostdetailstab = $("#autonumforcostdetailstab").val();
            var newIndex = parseInt(autonumforcostdetailstab) + 1;

            for (i = 0; i < dtSDM.length; i++) {
                let $storeDropdown = $("#storeDropdownTemplate select").clone();

                if (dtSDM[i].Store_No) {
                    $storeDropdown.val(dtSDM[i].Store_No);
                }

                let $row = $(`
                    <tr class="CostDetailsRow rowIndex${newIndex}" data-row-id="${newIndex}">
                        <td class="selectCostDetails"><span>&rarr;</span></td>
                        <td><input type="text" class="form-control costMeterial" value="${dtSDM[i].material || ''}"></td>
                        <td>
                            <input type="text" class="CostLotList CostLotNo form-control"
                                    value="${dtSDM[i].lot_no || ''}">
                            <div class="costNo-autocomplete-items"></div>
                        </td>
                        <td><input type="text" class="form-control CostQuality" value="${dtSDM[i].quality || ''}"></td>
                        <td><input type="text" class="form-control CostSize" value="${dtSDM[i].Size || ''}"></td>
                        <td><input type="text" class="form-control CostPcs text-right" value="${format2(dtSDM[i].PCS)}"></td>
                        <td><input type="text" class="form-control CostTotalWt text-right" value="${format2(dtSDM[i].TotalWt)}"></td>
                        <td class="text-center"><input type="checkbox" class="CostByPC" ${dtSDM[i].by_pc === "true" ? "checked" : ""}></td>
                        <td><input type="text" class="form-control CostUnitCost text-right" value="${format2(dtSDM[i].Price)}" disabled></td>
                        <td><input type="text" class="form-control CostTotal text-right" value="${format2(dtSDM[i].Total)}"></td>
                        <td class="text-center"><input type="checkbox" class="CostDontDeduct" ${dtSDM[i].DoNotDeduct === "true" ? "checked" : ""}></td>
                        <td class="storeCell"></td>
                        <td><input type="text" class="form-control CostNote" value="${dtSDM[i].Note || ''}"></td>
                        <td><input type="text" class="form-control CostUnitPrice text-right" value="${format2(dtSDM[i].Unit_Price)}"></td>
                        <td>
                            ${buildCostTypeDropdown(dtSDM[i].header_type)}
                        </td>
                        <td>
                            <button class="btn btn-danger btn-sm delete_a_line">
                                <i class="fas fa-trash"></i>
                            </button>
                        </td>
                    </tr>
                `);

                $row.find(".storeCell").append($storeDropdown);

                $(".CostDetailsBody").append($row);

                $("#autonumforcostdetailstab").val(newIndex);
                newIndex++;
            }
            $(".CostDetailsBody").append(repairRow);

            stoneDetailsRow = "";
            autonumforstonedetailstab = $("#autonumforstonedetailstab").val();
            var newIndex = parseInt(autonumforstonedetailstab) + 1;

            for (i = 0; i < dtSDM.length; i++) {
                stoneDetailsRow += "<tr class='StoneDetailsRow rowIndex" + newIndex + "' data-row-id='" + newIndex + "'>";
                stoneDetailsRow += "<td class='selectStoneDetails'><span>&rarr;</span></td>";
                stoneDetailsRow += "<td><input type='text' class='form-control CostType' value='" + dtSDM[i].header_type + "'></td>";
                stoneDetailsRow += "<td class='stone_wt_column'><input type='text' class='form-control StoneDetailsTotalWt text-right pull-right' value='" + (dtSDM[i].TotalWt ? dtSDM[i].TotalWt.toFixed(2) : '0.00') + "' ></td>";
                stoneDetailsRow += "<td><button class='btn btn-danger btn-sm delete_a_line' data-index-number='" + newIndex + "'><i class='fas fa-trash'></i ></button></td > ";
                stoneDetailsRow += "</tr>";
                $("#autonumforstonedetailstab").val(newIndex);
                newIndex++;
            }
            $(".StoneDetailsBody").append(stoneDetailsRow);
        }
    })
}



$(document).on('click', '.addRowCostDetailsRow', function () {

    autonumforcostdetailstab = $("#autonumforcostdetailstab").val();
    var newIndex = parseInt(autonumforcostdetailstab) + 1;
    var storeCodeInuse = $('#storeCodeInuse').val();

    let $storeDropdown = $("#storeDropdownTemplate select").clone();

    if (storeCodeInuse) {
        $storeDropdown.val(storeCodeInuse);
    }

    let $row = $(`
        <tr class="CostDetailsRow rowIndex${newIndex}" data-row-id="${newIndex}">
            <td class="selectCostDetails"><span>&rarr;</span></td>
            <td><input type="text" class="form-control costMeterial"></td>
            <td>
                <input type="text" class="CostLotList CostLotNo form-control"
                       id="CostLotNo_${newIndex}"
                       data-index="${newIndex}" data-alerted="false">
                <div class="costNo-autocomplete-items"></div>
            </td>
            <td><input type="text" class="form-control CostQuality"></td>
            <td><input type="text" class="form-control CostSize"></td>
            <td><input type="text" class="form-control CostPcs text-right"></td>
            <td><input type="text" class="form-control CostTotalWt text-right" value="0"></td>
            <td class="text-center"><input type="checkbox" class="CostByPC"></td>
            <td><input type="text" class="form-control CostUnitCost text-right" disabled></td>
            <td><input type="text" class="form-control CostTotal text-right"></td>
            <td class="text-center"><input type="checkbox" class="CostDontDeduct"></td>
            <td class="storeCell"></td>
            <td><input type="text" class="form-control CostNote" disabled></td>
            <td><input type="text" class="form-control CostUnitPrice text-right"></td>
            <td>${buildCostTypeDropdown("")}</td>
            <td>
                <button class="btn btn-danger btn-sm delete_a_line" data-index-number="${newIndex}">
                    <i class="fas fa-trash"></i>
                </button>
            </td>
        </tr>
    `);

    $row.find(".storeCell").append($storeDropdown);

    $(".CostDetailsBody").append($row);
    $("#autonumforcostdetailstab").val(newIndex);

})


if ($("#StylesForAutofill").length > 0) {
    var AllStyles = JSON.parse($("#StylesForAutofill").html());
}




$(document).on("input", ".CostLotList, .bomLotNo", function () {

    const $curInput = $(this);
    const query = $curInput.val().toLowerCase();

    // Find the correct autocomplete container
    const $list = $curInput.closest('td').find(".costNo-autocomplete-items");

    $list.empty().hide();

    if (!query) return;

    let allStyles;
    try {
        allStyles = JSON.parse($("#StylesForAutofill").html());
    } catch (e) {
        console.error("Invalid JSON in #StylesForAutofill");
        return;
    }

    const results = allStyles.filter(item =>
        item.STYLE &&
        item.STYLE.toLowerCase().startsWith(query)
    );

    if (!results.length) return;

    results.forEach(item => {
        $("<div>")
            .addClass("costNo-autocomplete-item")
            .text(item.STYLE)
            .appendTo($list)
            .on("mousedown", function () {
                // mousedown instead of click (prevents blur issue)
                $curInput.val(item.STYLE);
                $list.empty().hide();
            });
    });

    $list.show();
});

$(document).on("blur", ".CostLotList, .bomLotNo", function () {
    const $curInput = $(this);
    const $list = $curInput.closest('td').find(".costNo-autocomplete-items");

    // Delay is important so mousedown selection works
    setTimeout(function () {
        $list.empty().hide();
    }, 150);
});

function toDec(v) {
    let n = parseFloat(v);
    return isNaN(n) ? 0 : n;
}

function showError(msg) {
    alert(msg);
}

let lotAjaxMap = new Map(); // track ajax per input

$(document).on("focusout", ".CostLotNo", function () {

    const $input = $(this);
    const $row = $input.closest("tr");
    const lotNo = $input.val().trim();

    if (!lotNo) return;

    // Abort previous request for this input
    if (lotAjaxMap.has(this)) {
        lotAjaxMap.get(this).abort();
    }

    const req = $.ajax({
        url: "/Styles/ValidateLot",
        type: "POST",
        data: { lotNo: lotNo },
        success: function (resp) {

            if (!resp || resp.success !== true) {
                showError("Invalid Lot#");
                $input.val("").focus();
                return;
            }

            // Fill Unit Cost
            $row.find(".CostUnitCost").val(format2(resp.cost));

            // Fill Unit Price
            $row.find(".CostUnitPrice").val(format2(resp.price));

            // Set Type ONLY if empty
            const $type = $row.find(".CostType");
            if (!$type.val().trim()) {

                const item = (resp.itemType || "").toUpperCase().trim();

                if (item === "LABOR")
                    $type.val("LABOR");
                else if (item === "FINDING")
                    $type.val("FINDING");
                else if (["DIAMOND", "DIAMOND(SIDE)", "COLOR STONE(SIDE)"].includes(item))
                    $type.val("STONE");
                else if (item === "METAL")
                    $type.val("CASTING");
            }

            recalcRow($row);
        },
        complete: function () {
            lotAjaxMap.delete($input[0]);
        }
    });

    lotAjaxMap.set(this, req);
});

$(document).on("input change", ".CostPcs, .CostTotalWt, .CostUnitCost, .CostUnitPrice, .CostByPC", function () {
    recalcRow($(this).closest("tr"));
});

function recalcRow($row) {

    let pcs = toDec($row.find(".CostPcs").val());
    let twt = toDec($row.find(".CostTotalWt").val());
    let unitCost = toDec($row.find(".CostUnitCost").val());
    let byPC = $row.find(".CostByPC").is(":checked");

    let total = 0;

    if (byPC)
        total = pcs * unitCost;
    else
        total = twt * unitCost;

    // Set row total
    $row.find(".CostTotal").val(total.toFixed(2));

    sumCostDetails();
    calculateCostTotalsByType();
}

$(document).on('click', '.delete_a_line', function () {

    var index = $(this).data('index-number');
    $(".rowIndex" + index).remove();
    sumCostDetails();
    calculateCostTotalsByType();
})

$(document).on('change', '.CostType', function () {
    calculateCostTotalsByType();
});

function sumCostDetails() {
    let sum = 0;

    $(".CostTotal").each(function () {
        sum += toDec($(this).val());
    });

    $("#CostGrandTotal").val(sum.toFixed(2)); // if you show total on screen
}

function calculateCostTotalsByType() {

    let totals = {
        CASTING: 0,
        FINDING: 0,
        LABOR: 0,
        SETTING: 0,
        STONE: 0
    };

    $(".CostDetailsBody tr").each(function () {

        let type = ($(this).find(".CostType").val() || "")
            .toUpperCase()
            .trim();

        let rowTotal = toDec(
            $(this).find(".CostTotal").val()
        );

        if (totals.hasOwnProperty(type)) {
            totals[type] += rowTotal;
        }
    });

    $(".castingCostTotal").val(totals.CASTING.toFixed(2));
    $(".findingCostTotal").val(totals.FINDING.toFixed(2));
    $(".laborCostTotal").val(totals.LABOR.toFixed(2));
    $(".settingCostTotal").val(totals.SETTING.toFixed(2));
    $(".stoneCostTotal").val(totals.STONE.toFixed(2));
}

$(document).on("blur", ".CostPcs", function () {

    let val = toDec($(this).val());

    if (val < 0 || val > 9999.99) {
        showError("Invalid No. of PCs");
        $(this).val("");
        return;
    }
});

$(document).on("blur", ".CostTotalWt", function () {

    let val = toDec($(this).val());

    if (val > 9999.99) {
        showError("Please enter valid WT");
        $(this).val("");
        return;
    }
});

$(document).on("blur", ".CostUnitPrice", function () {

    let val = toDec($(this).val());

    if (val > 99999999999.99) {
        showError("Please enter valid Price");
        $(this).val("");
    }
});

function LoadBOMTab(json) {
    style = json.STYLE;

    $("#finds1").val(json.FIND1);
    $("#finds2").val(json.FIND2);
    $("#finds3").val(json.FIND3);
    $("#finds4").val(json.FIND4);
    $("#find_qty1").val(json.FIND_QTY1);
    $("#find_qty2").val(json.FIND_QTY2);
    $("#find_qty3").val(json.FIND_QTY3);
    $("#find_qty4").val(json.FIND_QTY4);
    $("#setting1").val(json.SET_TYPE1);
    $("#setting2").val(json.SET_TYPE2);
    $("#setting3").val(json.SET_TYPE3);
    $("#setting4").val(json.SET_TYPE4);
    $("#setting5").val(json.SET_TYPE5);
    $("#setting6").val(json.SET_TYPE6);
    $("#setting7").val(json.SET_TYPE7);
    $("#setting8").val(json.SET_TYPE8);
    $("#set1").val(json.SET1);
    $("#set2").val(json.SET2);
    $("#set3").val(json.SET3);
    $("#set4").val(json.SET4);
    $("#set5").val(json.SET5);
    $("#set6").val(json.SET6);
    $("#set7").val(json.SET7);
    $("#set8").val(json.SET8);
    $("#set_cost1").val(json.SET_COST1 ? json.SET_COST1.toFixed(2) : '0.00');
    $("#set_cost2").val(json.SET_COST2 ? json.SET_COST2.toFixed(2) : '0.00');
    $("#set_cost3").val(json.SET_COST3 ? json.SET_COST3.toFixed(2) : '0.00');
    $("#set_cost4").val(json.SET_COST4 ? json.SET_COST4.toFixed(2) : '0.00');
    $("#set_cost5").val(json.SET_COST5 ? json.SET_COST5.toFixed(2) : '0.00');
    $("#set_cost6").val(json.SET_COST6 ? json.SET_COST6.toFixed(2) : '0.00');
    $("#set_cost7").val(json.SET_COST7 ? json.SET_COST7.toFixed(2) : '0.00');
    $("#set_cost8").val(json.SET_COST8 ? json.SET_COST8.toFixed(2) : '0.00');
    $("#labor1").val(json.LABOR_TYPE1);
    $("#labor2").val(json.LABOR_TYPE2);
    $("#labor3").val(json.LABOR_TYPE3);
    $("#labor4").val(json.LABOR_TYPE4);
    $("#labor_cost1").val(json.LABOR_COST1 ? json.LABOR_COST1.toFixed(2) : '0.00');
    $("#labor_cost2").val(json.LABOR_COST2 ? json.LABOR_COST2.toFixed(2) : '0.00');
    $("#labor_cost3").val(json.LABOR_COST3 ? json.LABOR_COST3.toFixed(2) : '0.00');
    $("#labor_cost4").val(json.LABOR_COST4 ? json.LABOR_COST4.toFixed(2) : '0.00');
    $("#labor_qty1").val(json.LABOR_QTY1);
    $("#labor_qty2").val(json.LABOR_QTY2);
    $("#labor_qty3").val(json.LABOR_QTY3);
    $("#labor_qty4").val(json.LABOR_QTY4);

    $.ajax({
        url: "../Styles/GetStoneGridData",
        type: 'post',
        data: { style: style },
        success: function (response) {
            bomjson = $.parseJSON(response);

            bomDataRow = "";
            autonumforbomtab = $("#autonumforbomtab").val();
            var newIndex = parseInt(autonumforbomtab);

            for (i = 0; i < bomjson.length; i++) {
                var newIndex = parseInt(newIndex) + 1;
                bomDataRow += "<tr class='CostDetailsRow rowIndex" + newIndex + "' data-row-id='" + newIndex + "'>";
                bomDataRow += "<td class='selectBomDetails'><span>&rarr;</span></td>";
                bomDataRow += "<td class=''><input type='text' class='form-control bomClass ' value='" + bomjson.class + "' ></td>";
                bomDataRow += "<td class=''><input type='text' class='bomShape form-control ' value='" + bomjson.Shape + "' id='BomLotNo_" + newIndex + "' data-index='" + newIndex + "' data-alerted='false' value=''><div id='' class='CostNo-autocomplete-items'></div></td>";
                bomDataRow += "<td class=''><input type='text' class='form-control bomSize ' value='" + bomjson.Size + "' ></td>";
                bomDataRow += "<td class=''><input type='text' class='form-control bomShade' value='" + bomjson.Shade + "' ></td>";
                bomDataRow += "<td class=''><input type='text' class='form-control bomGrade ' value='" + bomjson.Grade + "' ></td>";
                bomDataRow += "<td class=''><input type='text' class='form-control bomPriceLvel ' value='" + bomjson.Price_Level + "' ></td>";
                bomDataRow += "<td class=''><input type='text' class='form-control bomPcs text-right ' " + bomjson.PCS + " ></td>";
                bomDataRow += "<td class=''><input type='text' class='form-control bomWtEach text-right ' value='" + (bomjson.Wt_each ? bomjson.Wt_each.toFixed(2) : '0.00') + "' disabled ></td>";
                bomDataRow += "<td class=''><input type='text' class='form-control bomTotalWt text-right' value='" + (bomjson.TotalWt ? bomjson.TotalWt.toFixed(2) : '0.00') + "'></td>";
                bomDataRow += "<td ><input type='text' class='form-control bomPriceCt text-right ' value='" + (bomjson.Price_Ct ? bomjson.Price_Ct.toFixed(2) : '0.00') + "' ></td>";
                bomDataRow += "<td class=''><input type='text' class='form-control bomEach text-right ' value='" + (bomjson.Each ? bomjson.Each.toFixed(2) : '0.00') + "' ></td>";
                bomDataRow += "<td class=''><input type='text' class='form-control bomTotal text-right ' value='" + (bomjson.Total ? bomjson.Total.toFixed(2) : '0.00') + "' disabled ></td>";
                bomDataRow += "<td class='pl-10px'><input type='checkbox' class='bomCtrStn' value='" + ((bomjson.Ctr_stn == "true") ? "checked" : "") + "'></td>";
                bomDataRow += "<td><input type='text' class='form-control bomLotNo' value='" + bomjson.lot_no + "'></td>";
                bomDataRow += "<td><input type='text' class='form-control bomNote' value='" + bomjson.Note + "'></td>";
                bomDataRow += "<td><input type='text' class='form-control bomStoneType' value='" + bomjson.stone_type + "'></td>";
                bomDataRow += "<td><button class='btn btn-danger btn-sm delete_a_line' data-index-number='" + newIndex + "'><i class='fas fa-trash'></i ></button></td > ";
                bomDataRow += "</tr>";
                $("#autonumforbomtab").val(newIndex);
            }
            $(".BomDetailsBody").append(bomDataRow);
        }
    })
}

function LoadBOMTOTALTab(json) {
    style = json.STYLE;

    $("#txtStoneCostbom").val(json.stone_cost ? json.stone_cost.toFixed(2) : '0.00');
    $("#txtLaborCostbom").val(json.labor_cost ? json.labor_cost.toFixed(2) : '0.00');
    $("#txtCastingCostbom").val(json.CASTING ? json.CASTING.toFixed(2) : '0.00');
    $("#txtSettingCostbom").val(json.SETTING ? json.SETTING.toFixed(2) : '0.00');
    $("#txtFindingCostbom").val(json.FIND_COST ? json.FIND_COST.toFixed(2) : '0.00');
    $("#txtRhodiumCostbom").val(json.ROD_CHRG ? json.ROD_CHRG.toFixed(2) : '0.00');
    $("#txtPolishCostbom").val(json.POLISH ? json.POLISH.toFixed(2) : '0.00');
    $("#txtCertChargesbom").val(json.LASER ? json.LASER.toFixed(2) : '0.00');
    $("#txtFieldValue1bom").val(json.fieldval1_cost ? json.fieldval1_cost.toFixed(2) : '0.00');
    $("#txtFieldValue2bom").val(json.fieldval2_cost ? json.fieldval2_cost.toFixed(2) : '0.00');
    $("#txtFieldValue3bom").val(json.fieldval3_cost ? json.fieldval3_cost.toFixed(2) : '0.00');
    $("#txtMiscCostbom").val(json.MISC1 ? json.MISC1.toFixed(2) : '0.00');
    $("#txtField1bom").val(json.field1txt_cost);
    $("#txtField2bom").val(json.field2txt_cost);
    $("#txtField3bom").val(json.field3txt_cost);
    $("#txtAssemblyCostbom").val(json.ASSEMBLY_COST);
    $("#gold_wtbom").val(json.GOLD_WT ? json.GOLD_WT.toFixed(2) : '0.00');
    $("#clasp_wtbom").val(json.Clasp_Wt);
    $("#txtlaborpcbom").val(json.labor);
    $("#txtlaborgrambom").val(json.labor_gr);
    $("#silver_wtbom").val(json.SLVR_WT);
    $("#plat_wtbom").val(json.Plat_wt);
    $("#gold_basebom").val(json.GOLDBASE);
    $("#clasp_ozbom").val(json.Clasp_OZ);
    $("#chain_ozbom").val(json.Chain_OZ);
    $("#txtGoldPart4_OZbom").val(json.GoldPart4_OZ);
    $("#silver_ozbom").val(json.SLVR_PRICE);
    $("#platinum_ozbom").val(json.Plat_price);
    $("#gold_srchrgbom").val(json.COSTPER);
    $("#clasp_srchrgbom").val(json.Clasp_Srchrg);
    $("#chain_srchrgbom").val(json.Chain_Srchrg);
    $("#txtGoldPart4_srchrgbom").val(json.GoldPart4_Srchrg);
    $("#silver_srchrgbom").val(json.Silvper);
    $("#plat_srchrgbom").val(json.Platper);

    $("#laborbom").val(json.LABOR_GR);
    $("#txtGoldPart2Grbom").val(json.GoldPart2_GR);
    $("#txtGoldPart3Grbom").val(json.GoldPart3_GR);
    $("#txtGoldPart4_Grbom").val(json.GoldPart4_Gr);
    $("#txtSilverGrbom").val(json.Silver_GR);
    $("#txtPlatinumGrbom").val(json.Platinum_GR);

    $("#txtGoldPart2Karatbom").val(json.GoldPart2_Karat);
    $("#txtGoldPart3Karatbom").val(json.GoldPart3_Karat);
    $("#txtGoldPart4_Karatbom").val(json.GoldPart4_Karat);

    if (json.MultiMarkup == true) {
        $("#chkmultiplemarkups").prop('checked', true);
    }
    //chkmultiplemarkups.Checked = stylerow["MultiMarkup"], typeof (bool).FullName);

}

$(document).on('click', '.addRowBomDetailsRow', function () {

    let index = parseInt($("#autonumforbomtab").val()) + 1;

    let bomClassDropdown = buildDropdown(bomClasses, "bomClass");
    let bomShapeDropdown = buildDropdown(bomShapes, "bomShape");

    let bomDataRow = `
    <tr class="bomBowIndex${index}" data-row-id="${index}">
        <td class="selectBomDetails"><span>&rarr;</span></td>

        <td>${bomClassDropdown}</td>
        <td>${bomShapeDropdown}</td>

        <td>
            <select class="form-control bomSize">
                <option value="">-- Select Size --</option>
            </select>
        </td>

        <td><input type="text" class="form-control bomShade"></td>
        <td><input type="text" class="form-control bomGrade"></td>
        <td><input type="text" class="form-control bomPriceLvel"></td>
        <td><input type="text" class="form-control bomPcs text-right"></td>
        <td><input type="text" class="form-control bomWtEach text-right"></td>
        <td><input type="text" class="form-control bomTotalWt text-right"></td>
        <td><input type="text" class="form-control bomPriceCt text-right"></td>
        <td><input type="text" class="form-control bomEach text-right"></td>
        <td><input type="text" class="form-control bomTotal text-right"></td>
        <td><input type="checkbox" class="bomCtrStn"></td>
        <td>
        <input type='text' class='bomLotNo form-control ' id='bomLotNo_" + newIndex + "' data-index='" + newIndex + "' data-alerted='false' value=''><div id='' class='costNo-autocomplete-items'></div></td>
        <td><input type="text" class="form-control bomNote"></td>
        <td><input type="text" class="form-control bomStoneType"></td>
        <td>
            <button type="button" data-index-number="${index}" class="btn btn-danger btn-sm delete_a_bom_line">
                <i class="fas fa-trash"></i>
            </button>
        </td>
    </tr>`;

    $(".BomDetailsBody").append(bomDataRow);
    $("#autonumforbomtab").val(index);
});

$(document).on('click', '.delete_a_bom_line', function () {

    var index = $(this).data('index-number');
    $(".bomBowIndex" + index).remove();
})

$(document).on('input', '.bomPcs, .bomWtEach, .bomPriceCt', function () {
    this.value = this.value.replace(/[^0-9.]/g, '');
});

function num(v) {
    let n = parseFloat(v);
    return isNaN(n) ? 0 : n;
}

function fix(v, d = 4) {
    return num(v).toFixed(d);
}

$(document).on('blur keyup', '.bomPcs, .bomWtEach, .bomPriceCt', function () {

    let $row = $(this).closest('tr');

    let pcs = num($row.find('.bomPcs').val());
    let wtEach = num($row.find('.bomWtEach').val());
    let priceCt = num($row.find('.bomPriceCt').val());

    // --- Calculations ---
    let totalWt = pcs * wtEach;
    let eachAmt = wtEach * priceCt;
    let total = totalWt * priceCt;

    // --- Update fields ---
    $row.find('.bomTotalWt').val(fix(totalWt, 4));
    $row.find('.bomEach').val(fix(eachAmt, 2));
    $row.find('.bomTotal').val(fix(total, 2));
});

$(document).on('change', '.bomClass, .bomShape', function () {

    let $row = $(this).closest('tr');
    let cls = $row.find('.bomClass').val();
    let shp = $row.find('.bomShape').val();

    if (!cls || !shp) return;

    $.get('/Styles/GetStoneSizes', { color: cls, shape: shp }, function (sizes) {

        let $size = $row.find('.bomSize');
        $size.empty().append('<option value="">-- Select Size --</option>');

        sizes.forEach(s => {
            $size.append(`<option value="${s}">${s}</option>`);
        });
    });
});

function buildDropdown(options, cssClass, selectedValue = "") {

    // clone + sort ascending by Text
    let sorted = [...options].sort((a, b) => {
        return a.Text.localeCompare(b.Text, undefined, { sensitivity: "base" });
    });

    let html = `<select class="form-control ${cssClass}">
                    <option value="">-- Select --</option>`;

    sorted.forEach(o => {
        let selected = o.Value == selectedValue ? "selected" : "";
        html += `<option value="${o.Value}" ${selected}>${o.Text}</option>`;
    });

    html += `</select>`;
    return html;
}

$(document).on('change', '#storeNo', function () {
    LoadStockNumbers();
})

function LoadStockNumbers() {
    style = $("#styleNo").val();
    store = $("#storeNo").val();
    $.ajax({
        url: "../Styles/LoadStockNumbers",
        type: 'post',
        data: { style: style, store: store },
        success: function (response) {
            stockjson = $.parseJSON(response);

            bomDataRow = "";
            autonumforbomtab = $("#autonumforstocknumtab").val();
            var newIndex = parseInt(autonumforbomtab);

            for (i = 0; i < stockjson.length; i++) {
                var newIndex = parseInt(newIndex) + 1;
                bomDataRow += "<tr class='StockNumDetailsRow StockNoRowIndex" + newIndex + "' data-row-id='" + newIndex + "'>";
                bomDataRow += "<td class=''><input type='text' class='form-control stockNoID ' readonly value='" + stockjson[i].stk_no + "' ></td>";
                bomDataRow += "<td class=''><input type='text' class='inStock form-control ' readonly value='" + stockjson[i].in_stock + "' id='BomLotNo_" + newIndex + "' data-index='" + newIndex + "' data-alerted='false' value=''><div id='' class='CostNo-autocomplete-items'></div></td>";
                bomDataRow += "<td class=''><input type='text' class='form-control stockNoStoreNo ' readonly value='" + stockjson[i].store_no + "' ></td>";
                bomDataRow += "<td class=''><input type='checkbox' class='stockNoprintTag' value='1' ></td>";
                bomDataRow += "<td class=''><input type='text' class='form-control noofTags' readonly value='' ></td>";
                bomDataRow += "<td> <button class='btn btn-danger btn-sm btnDeleteStock' type='button'> <i class='fas fa-trash'></i > </button> </td>";
                bomDataRow += "</tr>";
                $("#autonumforstocknumtab").val(newIndex);
            }
            $(".StockNumbersDetailsBody").append(bomDataRow);
        }
    })
}

$(document).on("click", ".btnDeleteStock", function () {

    let $row = $(this).closest("tr");

    let stockNumber = $row.find(".stockNoID").val();
    let stockStore = $row.find(".stockNoStoreNo").val();
    let inStock = parseFloat($row.find(".inStock").val()) || 0;

    if (!stockNumber) {
        alert("Please select row for delete.");
        return;
    }

    if (!confirm(`Are you sure you want to delete stock# - ${stockNumber}?`)) {
        return;
    }

    $.ajax({
        url: "/Styles/DeleteStockNumber",
        type: "POST",
        data: {
            stockNumber: stockNumber,
            storeNo: stockStore,
            inStock: inStock,
            styleNo: $("#styleNo").val()
        },
        success: function (resp) {

            if (!resp.success) {
                alert(resp.message);
                return;
            }

            // update qty stock
            $("#qty_stock").val(resp.qty_stock);

            // reload grid OR remove row
            $row.remove();
        },
        error: function () {
            alert("Error deleting stock number.");
        }
    });
});


$(document).on("click", "#btnAddCase", function () {
    $("#styleCaseModal").addClass("show").show();
    loadCaseGridFromJson();
});

function loadCaseGridFromJson() {

    $("#tblStyleCase tbody").empty();

    let jsonStr = $("#StyleCaseJson").val();
    if (!jsonStr) return; // Add mode � no data

    let data;
    try {
        data = JSON.parse(jsonStr);
    } catch (e) {
        console.error("Invalid JSON in StyleCaseJson", e);
        return;
    }

    data.forEach(row => {
        addCaseRow(row.Store, row.Case, row.CHK);
    });
}


function addCaseRow(store, caseNo, chk) {

    let dropdown = storeDropdownHtml;

    if (store) {
        dropdown = dropdown.replace(
            `value="${store}"`,
            `value="${store}" selected`
        );
    }

    let row = `
    <tr>
        <td>${dropdown}</td>
        <td>
            <input type="text"
                   class="form-control gridCaseNo"
                   value="${caseNo || ""}"
                   maxlength="10">
        </td>
        <td class="text-center">
            <input type="checkbox"
                   class="chkDelete"
                   ${chk ? "checked" : ""}>
        </td>
    </tr>`;

    $("#tblStyleCase tbody").append(row);
}


$(document).on("click", "#btnAddCaseRow", function () {
    addCaseRow("", "", false);
});

$(document).on("click", "#btnSaveCase", function () {

    let errors = [];
    let caseArray = [];

    $("#tblStyleCase tbody tr").each(function (i) {

        let store = $(this).find("select").val();
        let caseNo = $(this).find(".gridCaseNo").val().trim();
        let del = $(this).find(".chkDelete").is(":checked");

        if (store && !caseNo) {
            errors.push(`Row ${i + 1}: Case should not be empty`);
        }

        caseArray.push({
            Store: store || "",
            Case: caseNo || "",
            CHK: del
        });
    });

    if (errors.length) {
        alert(errors.join("\n"));
        return;
    }

    // Store JSON in hidden field
    $("#StyleCaseJson").val(JSON.stringify(caseArray));

    var styleCases = $("#StyleCaseJson").val();

    if (styleCases.length > 1) {
        caseText = 'MULTI';
    } else if (styleCases.length === 1) {
        caseText = styleCases[0].Case;
    }

    $("#case_no").val(caseText).prop("readonly", true);

    $("#styleCaseModal").removeClass("show").hide();
});


$(document).on("blur", ".gridCaseNo", function () {

    let $input = $(this);
    let caseNo = $input.val().trim();

    if (!caseNo) return; // empty handled on save

    $.ajax({
        url: "/Styles/ValidateCase",
        type: "POST",
        data: { caseNo: caseNo },
        success: function (res) {

            $input.next(".case-error").remove();

            if (!res.valid) {
                $input.val('');
                alert('Invalid Case.');

            }
        }
    });
});

$(document).on('click', ".vendorCodeSearchBtn1", function () {
    $("#SelectVendorCodeModel").show();
    $("#SelectVendorCodeModel").addClass('show').show();
})

$(document).on('click', ".styleCaseModalClose", function () {
    $("#styleCaseModal").hide();
    $("#styleCaseModal").removeClass('show').hide();
})

$(document).on('change', "#mountedDiamond", function () {

    let isChecked = $(this).is(':checked');

    if (isChecked) {
        $('#centerst_shape').val($('#rap_shape').val()).prop('disabled', true);
        $('#stoneWeight').val($('#rap_weight').val()).prop('disabled', true);
        $('#color').val($('#rap_clarity').val()).prop('disabled', true);
        $('#quality').val($('#rap_color').val()).prop('disabled', true);

    } else {

        $('#centerst_shape').prop('disabled', false);
        $('#stoneWeight').prop('disabled', false);
        $('#color').prop('disabled', false);
        $('#quality').prop('disabled', false);
    }
})

function styleCaseJsonToXml(jsonStr) {

    if (!jsonStr) return "<DocumentElement />";

    let data = JSON.parse(jsonStr);
    let xml = `<DocumentElement>`;

    data.forEach(r => {
        xml += `
        <STYLESCASE>
            <Store>${escapeXml(r.Store)}</Store>
            <Case>${escapeXml(r.Case)}</Case>
            <CHK>${r.CHK}</CHK>
        </STYLESCASE>`;
    });

    xml += `</DocumentElement>`;
    return xml;
}

function escapeXml(value) {
    return String(value || "")
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&apos;");
}

$(document).on('click', '#btnCopyStyle', function () {

    let partNo = $('#ourPart').val().trim();
    let styleNo = $('#styleno').val().trim();

    if (!partNo) {
        alert('Part# should not be empty.');
        return;
    }

    $.get('/Styles/GetStylesByPartNo', { partNo, styleNo }, function (resp) {

        if (!resp.success) {
            alert(resp.message);
            return;
        }

        let tbody = $('#tblCopyStyles tbody');
        tbody.empty();

        resp.data.forEach(row => {
            tbody.append(`
                <tr data-style-based-on-Part="${row.style}">
                    <td>${row.style}</td>
                    <td>${row.cast_code}</td>
                    <td>${row.part_no}</td>
                    <td>${row.sub_part}</td>
                    <td>${row.price}</td>
                    <td>${row.in_stock}</td>
                    <td>${row.desc}</td>
                </tr>
            `);
        });

        $('#copyStyleModal').addClass('show').show();
    });
});

$(document).on('dblclick', '#tblCopyStyles tbody tr', function () {

    let style = $(this).data('style-based-on-Part');

    loadStyleInfo(style, 1)

    $('#copyStyleModal').removeClass('show').hide();
});

$(document).on('click', '.delete_a_bom_line', function () {
    $(this).closest('tr').remove();
})

$(document).on('change', '#itemType', function () {
    var is_DiamondDealer = $('#is_DiamondDealer').val();
    var itemType = ($(this).val() || '').toLowerCase();

    if (is_DiamondDealer && itemType == 'diamond') {
        $('.chkPriceByWtDiv').show();
    }
    else {
        $('#chkPriceByWt').prop('checked', false);
        $('.chkPriceByWtDiv').hide();
    }
})

function getcastmetal() {
    metal = $("#metal").val();
    if (metal != "") {
        $.ajax({
            url: '../Styles/getcastmetal',
            type: 'post',
            data: { metal: metal },
            success: function (result) {
                $("#txtCastingCostMultibom").val(result);
                PriceCalForBomCostMulti();
            }
        })
    } else {
        PriceCalForBomCostMulti();
    }
}



function PriceCalForBomCostMulti() {
    if ($("#chkmultiplemarkups:checked").length > 0) {
        calcprice = (((parseFloat($("#txtCastingCostbom").val() || 0) * parseFloat($("#txtCastingCostMultibom").val() || 0))
            + (parseFloat($("#txtSettingCostbom").val() || 0) * parseFloat($("#txtSettingCostMultibom").val() || 0))
            + (parseFloat($("#txtFindingCostbom").val() || 0) * parseFloat($("#txtFindingCostMultibom").val() || 0))
            + (parseFloat($("#txtStoneCostbom").val() || 0) * parseFloat($("#txtStoneCostMultibom").val() || 0))
            + (parseFloat($("#txtLaborCostbom").val() || 0) * parseFloat($("#txtLaborCostMultibom").val() || 0)))
            + (parseFloat($("#txtRhodiumCostbom").val() || 0) + parseFloat($("#txtPolishCostbom").val() || 0) +
                parseFloat($("#txtMiscCostbom").val() || 0) + parseFloat($("#txtCertChargesbom").val() || 0) +
                parseFloat($("#txtAssemblyCostbom").val() || 0) + parseFloat($("#txtFieldValue1bom").val() || 0) +
                parseFloat($("#txtFieldValue2bom").val() || 0) + parseFloat($("#txtFieldValue3bom").val() || 0))) * (parseFloat($("#cost_multi").val() || 0));
        //if (calcprice > 0)
        //    price.NumericValue = Math.Round(Convert.ToDecimal(string.Format("{0:n2}", calcprice)), 2);
        if (calcprice > 0) {
            $("#price").val(calcprice.toFixed(2));
        }
    }

    //GetRoundOff();
}


function AddCostbom() {
    //cast_cost();
    tCost = 0;
    tCost += parseFloat($("#txtCastingCostbom").val() || 0) +
        parseFloat($("#txtSettingCostbom").val() || 0) +
        parseFloat($("#txtStoneCostbom").val() || 0) +
        parseFloat($("#txtFindingCostbom").val() || 0) +
        parseFloat($("#txtLaborCostbom").val() || 0) +

        parseFloat($("#txtFieldValue1bom").val() || 0) +
        parseFloat($("#txtFieldValue2bom").val() || 0) +
        parseFloat($("#txtFieldValue3bom").val() || 0) +

        parseFloat($("#txtPolishCostbom").val() || 0) +
        parseFloat($("#txtCertChargesbom").val() || 0) +
        parseFloat($("#txtMiscCostbom").val() || 0) +
        parseFloat($("#txtRhodiumCostbom").val() || 0) +
        parseFloat($("#txtAssemblyCostbom").val() || 0) +
        SumOfNumerics();

    return tCost + (Helper.is_Lefk ? (Convert.ToDecimal(purchase_price.Text) * (100 + Convert.ToDecimal(LefkstylesCost11.Text)) / 100) : 0);
}

function SumOfNumerics() {
    if (Helper_is_Lefk) {
        return parseFloat($("#LefkstylesCost1").val() || 0) +
            parseFloat($("#LefkstylesCost2").val() || 0) +
            parseFloat($("#LefkstylesCost3").val() || 0) +
            parseFloat($("#LefkstylesCost4").val() || 0) +
            parseFloat($("#LefkstylesCost5").val() || 0) +
            parseFloat($("#LefkstylesCost6").val() || 0) +
            parseFloat($("#LefkstylesCost7").val() || 0) +
            parseFloat($("#LefkstylesCost8").val() || 0) +
            parseFloat($("#LefkstylesCost9").val() || 0) +
            parseFloat($("#LefkstylesCost10").val() || 0) +
            parseFloat($("#LefkstylesCost12").val() || 0);
    }
    return parseFloat($("#stylesCost1").val() || 0) +
        parseFloat($("#stylesCost2").val() || 0) +
        parseFloat($("#stylesCost4").val() || 0) +
        parseFloat($("#stylesCost3").val() || 0) +
        parseFloat($("#stylesCost6").val() || 0) +
        parseFloat($("#stylesCost5").val() || 0) +
        parseFloat($("#stylesCost8").val() || 0) +
        parseFloat($("#stylesCost7").val() || 0) +
        parseFloat($("#stylesCost10").val() || 0) +
        parseFloat($("#stylesCost9").val() || 0);
}

$(document).on('click', '.autoFillTagInfo', function () {
    descr = $("#descr").val();
    $.ajax({
        url: '../Styles/TagInfo',
        type: 'post',
        data: { description: descr },
        success: function (response) {
            //alert(response)
            //response = $.parseJSON(result);
            $("#tagInfo1").val(response.length > 0 ? response[0] : "");
            $("#tagInfo2").val(response.length > 1 ? response[1] : "");
            $("#tagInfo3").val(response.length > 2 ? response[2] : "");
            $("#tagInfo4").val(response.length > 3 ? response[3] : "");
        }
    })
})

$(document).on('change', '#brands', function () {
    brand = $("#brands").val();
    if (brand != "") {
        $.ajax({
            url: '../Styles/GetSubBrandsByBrand',
            type: 'post',
            data: { brand: brand },
            success: function (result) {
                //alert(result)
                response = $.parseJSON(result);
                subbrands = "<option></option>";
                for (i = 0; i < response.length; i++) {
                    subbrands += "<option>" + response[i].subbrand + "</option>";
                }
                $("#subbrand").html(subbrands);
            }
        })
    }
})
$(document).on('change', '#cate_gory', function () {
    cate_gory = $("#cate_gory").val();

    if (cate_gory != "") {
        $.ajax({
            url: '../Styles/GetSubcatByCategoryGroup',
            type: 'post',
            data: { Category: cate_gory },
            success: function (result) {

                response = $.parseJSON(result);
                subcategories = "<option></option>";
                for (i = 0; i < response.length; i++) {
                    subcategories += "<option>" + response[i].subcat + "</option>";
                }
                $("#sub_category").html(subcategories);
            }
        })
    }
})

$(document).on('change', '.roundNearestRadio', function () {
    var dPrice = $('#price').val();
    var NextRndOff = $('.roundNearestRadio:checked').val();

    if (NextRndOff == 0)
        return Math.round((dPrice + Number.EPSILON) * 100) / 100;
    else
        dPrice = Math.ceil(dPrice / NextRndOff) * NextRndOff;

    $('#price').val(dPrice);
})

$(document).on('click', '#btnDeleteCaseRows', function () {
    $("#tblStyleCase .chkDelete:checked").each(function () {
        $(this).closest('tr').remove();
    })
})