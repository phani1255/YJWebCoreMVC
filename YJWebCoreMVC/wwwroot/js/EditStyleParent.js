/*
* Created by Phanindra 15-Jan-2026
* Phanindra 01/20/2026 Added more functionality to Edit Style Parent form.
* Phanindra 01/23/2026 Added more functionality to Edit Style Parent form.
* Phanindra 01/27/2026 fixed id issue for long desc
*/

function checkCostLimit(value, max, label, isChecked) {
    let num = parseFloat(value || 0);

    if (isChecked && num > max) {
        alert(label + " must be less than or equal to " + max);
        return false;
    }
    return true;
}

function validateBeforeSave() {

    if (!checkCostLimit($('#gold_wt').val(), 9999.99, "Gold Wt", $('#chkgold_wt').is(':checked'))) { return false; }
    if (!checkCostLimit($('#silver_wt').val(), 9999.99, "Silver Weight", $('#chksilver_wt').is(':checked'))) { return false; }
    if (!checkCostLimit($('#plat_wt').val(), 9999.99, "Platinum Weight", $('#chkplat_wt').is(':checked'))) { return false; }
    if (!checkCostLimit($('#textBoxRoyalty').val(), 9999.99, "Royalty Weight", $('#chktextBoxRoyalty').is(':checked'))) { return false; }
    if (!checkCostLimit($('#centerst_size').val(), 9999.99, "Center Size Weight", $('#chkcenterst_size').is(':checked'))) { return false; }
    if (!checkCostLimit($('#diamond_wt').val(), 9999.99, "Diamond Wt", $('#chkdiamond_wt').is(':checked'))) { return false; }
    if (!checkCostLimit($('#color_wt').val(), 9999.99, "Color Wt", $('#chkcolor_wt').is(':checked'))) { return false; }
    if (!checkCostLimit($('#purchase_price').val(), 9999999.99, "Purchase Price", $('#chkpurchase_price').is(':checked'))) { return false; }
    if (!checkCostLimit($('#duties').val(), 9999.99, "Duties", $('#chkduties').is(':checked'))) { return false; }
    if (!checkCostLimit($('#gems_gen_cost').val(), 9999999.99, "Stone Cost", $('#chkgems_gen_cost').is(':checked'))) { return false; }
    if (!checkCostLimit($('#labor_cost').val(), 9999.99, "Labor Cost", $('#chklabor_cost').is(':checked'))) { return false; }
    if (!checkCostLimit($('#fieldval1').val(), 9999999.99, "Field Value 1", $('#chkfieldval1').is(':checked'))) { return false; }
    if (!checkCostLimit($('#fieldval2').val(), 9999999.99, "Field Value 2", $('#chkfieldval2').is(':checked'))) { return false; }
    if (!checkCostLimit($('#fieldval3').val(), 9999999.99, "Field Value 3", $('#chkfieldval3').is(':checked'))) { return false; }
    if (!checkCostLimit($('#total_cost').val(), 99999999.99, "Total Cost", $('#chkToTalCost').is(':checked'))) { return false; }
    if (!checkCostLimit($('#cost_multi').val(), 9999.99, "Cost Multi", $('#chkcost_multi').is(':checked'))) { return false; }
    if (!checkCostLimit($('#price').val(), 9999999.99, "Base Price", $('#chkprice').is(':checked'))) { return false; }
    if (!checkCostLimit($('#min_price').val(), 9999999.99, "Mini Price", $('#chkmin_price').is(':checked'))) { return false; }
    if (!checkCostLimit($('#lmprice').val(), 999999.99, "% Markup", $('#chkMarkup').is(":checked"))) { return false; }
    if (!checkCostLimit($('#rap_weight').val(), 9999.99, "Rap weight", $('#chkrap_weight').is(':checked'))) { return false; }
    if (!checkCostLimit($('#rap_depth').val(), 9999.99, "Rap depth", $('#chkrap_depth').is(':checked'))) { return false; }
    if (!checkCostLimit($('#rap_table').val(), 9999.99, "Rap table", $('#chkrap_table').is(':checked'))) { return false; }
    if (!checkCostLimit($('#crwn_angl').val(), 9999.99, "Crown Angle", $('#chkcrwn_angl').is(':checked'))) { return false; }
    if (!checkCostLimit($('#pvln_angl').val(), 9999.99, "Pavilion angle", $('#chkpvln_angl').is(':checked'))) { return false; }
    if (!checkCostLimit($('#star_len').val(), 9999.99, "Star length", $('#chkstar_len').is(':checked'))) { return false; }
    if (!checkCostLimit($('#crwn_hght').val(), 9999.99, "Crown Height", $('#chkcrwn_hght').is(':checked'))) { return false; }
    if (!checkCostLimit($('#lowr_hlf').val(), 9999.99, "Lower half", $('#chklowr_hlf').is(':checked'))) { return false; }
    if (!checkCostLimit($('#pvln_dpth').val(), 9999.99, "Pavilion depth %", $('#chkpvln_dpth').is(':checked'))) { return false; }
    if (!checkCostLimit($('#rap_listprice').val(), 9999.99, "Rap List Price", $('#chkrap_listprice').is(':checked'))) { return false; }
    if (!checkCostLimit($('#rap_disc').val(), 99.99, "Rap.Discount", $('#chkrap_disc').is(':checked'))) { return false; }
    if (!checkCostLimit($('#rap_targetprice').val(), 99999.99, "Target Price", $('#chkrap_targetprice').is(':checked'))) { return false; }

    return true;
}

$(document).on('click', '.gotoHome', function () {
    window.location.href = "../Home/Index";
})

function styleCaseJsonToXml(jsonStr) {

    if (!jsonStr) return "<?xml version='1.0' encoding='utf - 16'?><DocumentElement><DocumentElement />";

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

$(document).on('click', '.btnSaveStyle', function (e) {
    
    // Basic field values
    let styleNo = $('#styleno').val();
    let glclass = $('#glclass').val();
    let itemType = $('#itemType').val().trim();
    let saveForm = $(this).val();

    if (!styleNo) {
        alert('Style # is required');
        return;
    }

    if (!validateBeforeSave()) {
        return;
    }

    StoneMaterialXml = "";

    let caseJson = $("#StyleCaseJson").val();
    let caseXml = styleCaseJsonToXml(caseJson);

    var StoneData = [];

    dtStoneDataBom = "";

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
        url: '/Styles/SaveStyleParent',
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

            alert('Style saved successfully.');

            if (saveForm == 'save_form') {
                window.location.href = "../Styles/StyleParent?partNo=false";
            }

            // TODO: refresh your list/grid if needed
            // reloadStylesGrid();
        },
        error: function (xhr) {
            alert('Error saving style: ' + xhr.responseText);
        }
    });
});


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

function populateStyle(json, partialLoading) {
    if (!json) return;
    fillFirstTabStyleFormFields(json);
    fillSecondTabStyleFormFields(json);
    fillThirdTabStyleFormFields(json);
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

    $("#descr").val(sval(json.DESC));
    $("#description").val(sval(json.DESC));
    $("#longDescription").val(sval(json.LONGDESC));


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
        //$("#txtEstDate").val(today);
    } else {
        $("#radDateTimePicker1").val(json.DATE.split('T')[0]);
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
