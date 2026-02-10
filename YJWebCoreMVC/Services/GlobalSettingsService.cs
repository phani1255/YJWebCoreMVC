using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class GlobalSettingsService
    {
        private readonly IMemoryCache _cache;
        private readonly ConnectionProvider _connectionProvider;

        private const string CacheKey = "GlobalSettings";

        public GlobalSettingsService(IMemoryCache cache, ConnectionProvider connectionProvider)
        {
            _cache = cache;
            _connectionProvider = connectionProvider;
        }

        public UPSINSModel GetGlobalSettings()
        {
            return _cache.GetOrCreate(CacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return LoadSettingsFromDatabase();
            });
        }

        private UPSINSModel LoadSettingsFromDatabase()
        {
            var settings = new UPSINSModel();

            using (var connection = _connectionProvider.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT TOP 1 * FROM UPS_INS", connection);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        // Map all columns to the UPS_INS object
                        settings.INSURANCE = reader["INSURANCE"] as decimal?;
                        settings.COD = reader["COD"] as decimal?;
                        settings.RESIDENT = reader["RESIDENT"] as decimal?;
                        settings.SATURDAY = reader["SATURDAY"] as decimal?;
                        settings.MIN_INS = reader["MIN_INS"] as decimal?;
                        settings.BKUP_FLD = reader["BKUP_FLD"] as string;
                        settings.RAP_USR = reader["RAP_USR"] as string;
                        settings.RAP_PW = reader["RAP_PW"] as string;
                        settings.QB_PATH = reader["QB_PATH"] as string;
                        settings.STYLE_LEN = reader["STYLE_LEN"] as int?;
                        settings.lastsync = reader["lastsync"] as DateTime?;
                        settings.dbversion = reader["dbversion"] as int?;
                        settings.COMPANYNAME = reader["COMPANYNAME"] as string;
                        settings.COMPANY_ADDR1 = reader["COMPANY_ADDR1"] as string;
                        settings.COMPANY_ADDR2 = reader["COMPANY_ADDR2"] as string;
                        settings.COMPANY_TEL = reader["COMPANY_TEL"] as string;
                        settings.COMPANY_LOGO = reader["COMPANY_LOGO"] as byte[];
                        settings.Attrib1 = reader["Attrib1"] as string;
                        settings.Attrib2 = reader["Attrib2"] as string;
                        settings.Attrib3 = reader["Attrib3"] as string;
                        settings.Attrib4 = reader["Attrib4"] as string;
                        settings.Attrib5 = reader["Attrib5"] as string;
                        settings.Attrib6 = reader["Attrib6"] as string;
                        settings.Attrib7 = reader["Attrib7"] as string;
                        settings.Attrib8 = reader["Attrib8"] as string;
                        settings.Attrib9 = reader["Attrib9"] as string;
                        settings.Attrib10 = reader["Attrib10"] as string;
                        settings.Attrib11 = reader["Attrib11"] as string;
                        settings.Attrib12 = reader["Attrib12"] as string;
                        settings.Attrib13 = reader["Attrib13"] as string;
                        settings.Attrib14 = reader["Attrib14"] as string;
                        settings.Attrib15 = reader["Attrib15"] as string;
                        settings.Attrib16 = reader["Attrib16"] as string;
                        settings.Attrib17 = reader["Attrib17"] as string;
                        settings.Attrib18 = reader["Attrib18"] as string;
                        settings.CustAttr1 = reader["CustAttr1"] as string;
                        settings.CustAttr2 = reader["CustAttr2"] as string;
                        settings.CustAttr3 = reader["CustAttr3"] as string;
                        settings.CustAttr4 = reader["CustAttr4"] as string;
                        settings.CustAttr5 = reader["CustAttr5"] as string;
                        settings.CustAttr6 = reader["CustAttr6"] as string;
                        settings.CustAttr7 = reader["CustAttr7"] as string;
                        settings.CustAttr8 = reader["CustAttr8"] as string;
                        settings.CustMultiAttr1 = reader["CustMultiAttr1"] as string;
                        settings.CustMultiAttr2 = reader["CustMultiAttr2"] as string;
                        settings.CustMultiAttr3 = reader["CustMultiAttr3"] as string;
                        settings.CustCheck1 = reader["CustCheck1"] as string;
                        settings.CustCheck2 = reader["CustCheck2"] as string;
                        settings.CustCheck3 = reader["CustCheck3"] as string;
                        settings.CustCheck4 = reader["CustCheck4"] as string;
                        settings.CustCheck5 = reader["CustCheck5"] as string;
                        settings.CustCheck6 = reader["CustCheck6"] as string;
                        settings.CustCheck7 = reader["CustCheck7"] as string;
                        settings.CustCheck8 = reader["CustCheck8"] as string;
                        settings.LEFT_MARGIN = reader["LEFT_MARGIN"] as decimal?;
                        settings.RIGHT_MARGIN = reader["RIGHT_MARGIN"] as decimal?;
                        settings.CINC = reader["CINC"] as decimal?;
                        settings.TOP_MARGIN = reader["TOP_MARGIN"] as decimal?;
                        settings.PRINTERPORT = reader["PRINTERPORT"] as string;
                        settings.MICR = reader["MICR"] as bool?;
                        settings.SCANPATH = reader["SCANPATH"] as string;
                        settings.pwd = reader["pwd"] as string;
                        settings.QBVALIDATEDON = reader["QBVALIDATEDON"] as DateTime?;
                        settings.cert_path = reader["cert_path"] as string;
                        settings.NEGATIVEINV = reader["NEGATIVEINV"] as bool?;
                        settings.AUTOSANDH = reader["AUTOSANDH"] as bool?;
                        settings.INVOICENOTE = reader["INVOICENOTE"] as string;
                        settings.MEMONOTE = reader["MEMONOTE"] as string;
                        settings.print_mode = reader["print_mode"] as string;
                        settings.diamond_dealer = reader["diamond_dealer"] as bool?;
                        settings.StyleField1 = reader["StyleField1"] as string;
                        settings.StyleField2 = reader["StyleField2"] as string;
                        settings.appversion = reader["appversion"] as string;
                        settings.Tag_multiplier = reader["Tag_multiplier"] as decimal?;
                        settings.ImagesPath = reader["ImagesPath"] as string;
                        settings.CUST_BOUNCE_FEE = (decimal)reader["CUST_BOUNCE_FEE"];
                        settings.BANK_BOUNCE_FEE = (decimal)reader["BANK_BOUNCE_FEE"];
                        settings.FedexLabelPath = reader["FedexLabelPath"] as string;
                        settings.FedexKey = reader["FedexKey"] as string;
                        settings.FedexPwd = reader["FedexPwd"] as string;
                        settings.FedexAccount = reader["FedexAccount"] as string;
                        settings.FedexMeter = reader["FedexMeter"] as string;
                        settings.FedexPayorAccount = reader["FedexPayorAccount"] as string;
                        settings.CompanyCity = reader["CompanyCity"] as string;
                        settings.CompanyZip = reader["CompanyZip"] as string;
                        settings.CompanyState = reader["CompanyState"] as string;
                        settings.Attrib19 = reader["Attrib19"] as string;
                        settings.Attrib20 = reader["Attrib20"] as string;
                        settings.Attrib21 = reader["Attrib21"] as string;
                        settings.StyleCheck1 = reader["StyleCheck1"] as string;
                        settings.StyleCheck2 = reader["StyleCheck2"] as string;
                        settings.StyleCheck3 = reader["StyleCheck3"] as string;
                        settings.StyleCheck4 = reader["StyleCheck4"] as string;
                        settings.StyleCheck5 = reader["StyleCheck5"] as string;
                        settings.StyleCheck6 = reader["StyleCheck6"] as string;
                        settings.RFID_IP = reader["RFID_IP"] as string;
                        settings.PRINTERPORT2 = reader["PRINTERPORT2"] as string;
                        settings.PRINTERPORT3 = reader["PRINTERPORT3"] as string;
                        settings.UpsAccessKey = reader["UpsAccessKey"] as string;
                        settings.UpsUsername = reader["UpsUsername"] as string;
                        settings.UpsPassword = reader["UpsPassword"] as string;
                        settings.UpsShipperNo = reader["UpsShipperNo"] as string;
                        settings.UpsLabelType = reader["UpsLabelType"] as string;
                        settings.UpsLabelPath = reader["UpsLabelPath"] as string;
                        settings.multi_lnm = (decimal)reader["multi_lnm"];
                        settings.gold_sur = (decimal)reader["gold_sur"];
                        settings.tag_left1 = reader["tag_left1"] as string;
                        settings.tag_left2 = reader["tag_left2"] as string;
                        settings.tag_left3 = reader["tag_left3"] as string;
                        settings.tag_left4 = reader["tag_left4"] as string;
                        settings.Rfid_Port = reader["Rfid_Port"] as string;
                        settings.RfidPrintLeft = (decimal)reader["RfidPrintLeft"];
                        settings.RfidPrintRight = (decimal)reader["RfidPrintRight"];
                        settings.RfidPrintTop = (decimal)reader["RfidPrintTop"];
                        settings.RfidPrintCinc = (decimal)reader["RfidPrintCinc"];
                        settings.company_email = reader["company_email"] as string;
                        settings.INVHISTPATH = reader["INVHISTPATH"] as string;
                        settings.Rfid_printer_port = reader["Rfid_printer_port"] as string;
                        settings.Cost_Code = reader["Cost_Code"] as string;
                        settings.StyleField3 = reader["StyleField3"] as string;
                        settings.StyleField4 = reader["StyleField4"] as string;
                        settings.StyleField5 = reader["StyleField5"] as string;
                        settings.PrintAging = (bool)reader["PrintAging"];
                        settings.PCS_DECIMAL = (bool)reader["PCS_DECIMAL"];
                        settings.gl_sales = reader["gl_sales"] as string;
                        settings.gl_snh = reader["gl_snh"] as string;
                        settings.gl_ar = reader["gl_ar"] as string;
                        settings.gl_memoprice = reader["gl_memoprice"] as string;
                        settings.gl_memocost = reader["gl_memocost"] as string;
                        settings.gl_cogs = reader["gl_cogs"] as string;
                        settings.gl_undep_funds = reader["gl_undep_funds"] as string;
                        settings.gl_cashinbank = reader["gl_cashinbank"] as string;
                        settings.gl_slsinvnt = reader["gl_slsinvnt"] as string;
                        settings.gl_comision = reader["gl_comision"] as string;
                        settings.gl_ap = reader["gl_ap"] as string;
                        settings.gl_inventory = reader["gl_inventory"] as string;
                        settings.gl_cons = reader["gl_cons"] as string;
                        settings.tag_left1A = reader["tag_left1A"] as string;
                        settings.tag_left2A = reader["tag_left2A"] as string;
                        settings.tag_left3A = reader["tag_left3A"] as string;
                        settings.tag_left4A = reader["tag_left4A"] as string;
                        settings.no_stores = reader["no_stores"] as string;
                        settings.store = reader["store"] as string;
                        settings.store2 = reader["store2"] as string;
                        settings.store3 = reader["store3"] as string;
                        settings.store4 = reader["store4"] as string;
                        settings.No_Memo = (bool)reader["No_Memo"];
                        settings.ASSET_GL = reader["ASSET_GL"] as string;
                        settings.COGS_GL = reader["COGS_GL"] as string;
                        settings.CLEAR_GL = reader["CLEAR_GL"] as string;
                        settings.SALES_GL = reader["SALES_GL"] as string;
                        settings.TAG_LEFT5 = reader["TAG_LEFT5"] as string;
                        settings.TAG_LEFT5A = reader["TAG_LEFT5A"] as string;
                        settings.USE_GLCODE = reader["USE_GLCODE"] as bool?;
                        settings.desc1 = reader["desc1"] as string;
                        settings.desc2 = reader["desc2"] as string;
                        settings.desc3 = reader["desc3"] as string;
                        settings.desc4 = reader["desc4"] as string;
                        settings.desc5 = reader["desc5"] as string;
                        settings.desc6 = reader["desc6"] as string;
                        settings.desc7 = reader["desc7"] as string;
                        settings.desc8 = reader["desc8"] as string;
                        settings.desc9 = reader["desc9"] as string;
                        settings.desc10 = reader["desc10"] as string;
                        settings.PrinterSettings = reader["PrinterSettings"] as byte[];
                        settings.statmentofvaluenotes = reader["statmentofvaluenotes"] as string;
                        settings.rfid_ip2 = reader["rfid_ip2"] as string;
                        settings.rfid_ip3 = reader["rfid_ip3"] as string;
                        settings.rfid_port2 = reader["rfid_port2"] as string;
                        settings.rfid_port3 = reader["rfid_port3"] as string;
                        settings.invoicecopies = reader["invoicecopies"] as int?;
                        settings.TAG_LEFT1B = reader["TAG_LEFT1B"] as string;
                        settings.TAG_LEFT2B = reader["TAG_LEFT2B"] as string;
                        settings.TAG_LEFT3B = reader["TAG_LEFT3B"] as string;
                        settings.TAG_LEFT4B = reader["TAG_LEFT4B"] as string;
                        settings.TAG_LEFT5B = reader["TAG_LEFT5B"] as string;
                        settings.LEFT_MARGIN2 = reader["LEFT_MARGIN2"] as decimal?;
                        settings.RIGHT_MARGIN2 = reader["RIGHT_MARGIN2"] as decimal?;
                        settings.TOP_MARGIN2 = reader["TOP_MARGIN2"] as decimal?;
                        settings.CINC2 = reader["CINC2"] as decimal?;
                        settings.LEFT_MARGIN3 = reader["LEFT_MARGIN3"] as decimal?;
                        settings.RIGHT_MARGIN3 = reader["RIGHT_MARGIN3"] as decimal?;
                        settings.TOP_MARGIN3 = reader["TOP_MARGIN3"] as decimal?;
                        settings.CINC3 = reader["CINC3"] as decimal?;
                        settings.gl_inventory_transit = reader["gl_inventory_transit"] as string;
                        settings.DISC_GL = reader["DISC_GL"] as string;
                        settings.Warranty = reader["Warranty"] as int?;
                        settings.Repair_note = reader["Repair_note"] as string;
                        settings.Repair_invoice_note = reader["Repair_invoice_note"] as string;
                        settings.min_barcode = (int)reader["min_barcode"];
                        settings.max_barcode = (int)reader["max_barcode"];
                        settings.gl_Memo_Liability = reader["gl_Memo_Liability"] as string;
                        settings.cc_mid = reader["cc_mid"] as string;
                        settings.cc_hsn = reader["cc_hsn"] as string;
                        settings.cc_username = reader["cc_username"] as string;
                        settings.cc_authorization = reader["cc_authorization"] as string;
                        settings.cc_pwd = reader["cc_pwd"] as byte[];
                        settings.REPAIR_DISCLAIMER = reader["REPAIR_DISCLAIMER"] as string;
                        settings.gl_trade_in = reader["gl_trade_in"] as string;
                        settings.gl_warranty_cogs = reader["gl_warranty_cogs"] as string;
                        settings.gl_warranty_sales = reader["gl_warranty_sales"] as string;
                        settings.printer_font = (int)reader["printer_font"];
                        settings.gl_inv_spl_order = reader["gl_inv_spl_order"] as string;
                        settings.lastversion = (int)reader["lastversion"];
                        settings.TAG_PLACE = reader["TAG_PLACE"] as string;
                        settings.TAG_TEXT = reader["TAG_TEXT"] as string;
                        settings.GP_PERCENT = (decimal)reader["GP_PERCENT"];
                        settings.KARAT_REDUCTION = (decimal)reader["KARAT_REDUCTION"];
                        settings.PAYOUT_PERCENT = (decimal)reader["PAYOUT_PERCENT"];
                        settings.WT_REDUCTION = (decimal)reader["WT_REDUCTION"];
                        settings.SCRAP_DISCLAIMER = reader["SCRAP_DISCLAIMER"] as string;
                        settings.gl_vendor_credit = reader["gl_vendor_credit"] as string;
                        settings.pversion = reader["pversion"] as string;
                        settings.gl_financed = reader["gl_financed"] as string;
                        settings.gl_giftcard = reader["gl_giftcard"] as string;
                        settings.DPRIORITY = reader["DPRIORITY"] as bool?;
                        settings.signature = (bool)reader["signature"];
                        settings.store5 = reader["store5"] as string;
                        settings.gl_scrap = reader["gl_scrap"] as string;
                        settings.backup_folder = reader["backup_folder"] as string;
                        settings.GIP = (int)reader["GIP"];
                        settings.GL_MATERIAL = reader["GL_MATERIAL"] as string;
                        settings.GL_OVER_HEAD = reader["GL_OVER_HEAD"] as string;
                        settings.GL_REPAIR_SALES = reader["GL_REPAIR_SALES"] as string;
                        settings.JomaUser = reader["JomaUser"] as string;
                        settings.JomaPwd = reader["JomaPwd"] as string;
                        settings.WFAPIToken = reader["WFAPIToken"] as string;
                        settings.STYLE = reader["STYLE"] as string;
                        settings.QtyInStock = reader["QtyInStock"] as string;
                        settings.sq_loc_code = reader["sq_loc_code"] as string;
                        settings.sq_authcode = reader["sq_authcode"] as string;
                        settings.clover_authcode = reader["clover_authcode"] as string;
                        settings.refreshinterval = (int)reader["refreshinterval"];
                        settings.email_signature = reader["email_signature"] as string;
                        settings.sq_appid = reader["sq_appid"] as string;
                        settings.sq_clientsecret = reader["sq_clientsecret"] as string;
                        settings.sq_tokenexpiredate = reader["sq_tokenexpiredate"] as DateTime?;
                        settings.sq_locationcode = reader["sq_locationcode"] as string;
                        settings.sq_merchantid = reader["sq_merchantid"] as string;
                        settings.sq_deviceserial = reader["sq_deviceserial"] as string;
                        settings.sq_accesstoken = reader["sq_accesstoken"] as byte[];
                        settings.sq_refreshtoken = reader["sq_refreshtoken"] as byte[];
                        settings.Left_Top = (int)reader["Left_Top"];
                        settings.Left_Top2 = (int)reader["Left_Top2"];
                        settings.Left_Top3 = (int)reader["Left_Top3"];
                        settings.Not_SideBySide = (bool)reader["Not_SideBySide"];
                        settings.Not_SideBySide2 = (bool)reader["Not_SideBySide2"];
                        settings.Not_SideBySide3 = (bool)reader["Not_SideBySide3"];
                        settings.servername = reader["servername"] as string;
                        settings.Customer_acc = reader["Customer_acc"] as string;
                        settings.vendor_acc = reader["vendor_acc"] as string;
                        settings.gl_inv_adjust = reader["gl_inv_adjust"] as string;
                        settings.AddressLabel_printer = reader["AddressLabel_printer"] as string;
                        settings.Jobbag_printer = reader["Jobbag_printer"] as string;
                        settings.Daily_SaleTarget = (decimal)reader["Daily_SaleTarget"];
                        settings.Monthly_SaleTarget = (decimal)reader["Monthly_SaleTarget"];
                        settings.Yearly_SaleTarget = (decimal)reader["Yearly_SaleTarget"];
                        settings.CloverPairingToken = reader["CloverPairingToken"] as string;
                        settings.CloverSecrureURL = reader["CloverSecrureURL"] as string;
                        settings.CloverRemoteId = reader["CloverRemoteId"] as string;
                        settings.CloverDeviceSerial = reader["CloverDeviceSerial"] as string;
                        settings.Statement_MSG = reader["Statement_MSG"] as string;
                        settings.GIA_KEY = reader["GIA_KEY"] as string;
                        settings.ltimer = (int)reader["ltimer"];
                        settings.DecimalsInPieces = (int)reader["DecimalsInPieces"];
                        settings.APPRAISER_NAME = reader["APPRAISER_NAME"] as string;
                        settings.PORTTEMPLATE1 = reader["PORTTEMPLATE1"] as string;
                        settings.PORTTEMPLATE2 = reader["PORTTEMPLATE2"] as string;
                        settings.PORTTEMPLATE3 = reader["PORTTEMPLATE3"] as string;
                        settings.Address_Landscape = (bool)reader["Address_Landscape"];
                        settings.INVOICESMSTEXT = reader["INVOICESMSTEXT"] as string;
                        settings.REPAIRSMSTEXT = reader["REPAIRSMSTEXT"] as string;
                        settings.text_from = reader["text_from"] as string;
                        settings.whatsapp_from = reader["whatsapp_from"] as string;
                        settings.ESPInsp_DueMonths = (int)reader["ESPInsp_DueMonths"];
                        settings.LayawayPay_DueDays = (int)reader["LayawayPay_DueDays"];
                        settings.LABEL_LEFT_MARGIN = reader["LABEL_LEFT_MARGIN"] as string;
                        settings.LABEL_RIGHT_MARGIN = reader["LABEL_RIGHT_MARGIN"] as string;
                        settings.LABEL_LINES_MARGIN = reader["LABEL_LINES_MARGIN"] as string;
                        settings.LABEL_PRINTERNAME = reader["LABEL_PRINTERNAME"] as string;
                        settings.repair_value = (decimal)reader["repair_value"];
                        settings.picture_printer = reader["picture_printer"] as string;
                        settings.email_inv_amount = (decimal)reader["email_inv_amount"];
                        settings.email_inv_addr = reader["email_inv_addr"] as string;
                        settings.pricerange1 = (decimal)reader["pricerange1"];
                        settings.pricerange2 = (decimal)reader["pricerange2"];
                        settings.pricerange3 = (decimal)reader["pricerange3"];
                        settings.pricerange4 = (decimal)reader["pricerange4"];
                        settings.pricerange5 = (decimal)reader["pricerange5"];
                        settings.pricerange6 = (decimal)reader["pricerange6"];
                        settings.pricerange7 = (decimal)reader["pricerange7"];
                        settings.pricerange8 = (decimal)reader["pricerange8"];
                        settings.ShopifyAPI = reader["ShopifyAPI"] as string;
                        settings.ShopifyPwd = reader["ShopifyPwd"] as string;
                        settings.ShopifySecret = reader["ShopifySecret"] as string;
                        settings.ShopifyURL = reader["ShopifyURL"] as string;
                        settings.CASHDRAWPORT = (int)reader["CASHDRAWPORT"];
                        settings.gl_sales_tax = reader["gl_sales_tax"] as string;
                        settings.jm_store = reader["jm_store"] as string;
                        settings.receipt_printer = reader["receipt_printer"] as string;
                        settings.TAG_LINE1 = reader["TAG_LINE1"] as string;
                        settings.TAG_LINE2 = reader["TAG_LINE2"] as string;
                        settings.TAG_LINE3 = reader["TAG_LINE3"] as string;
                        settings.TAG_LINE4 = reader["TAG_LINE4"] as string;
                        settings.TAG_LINE5 = reader["TAG_LINE5"] as string;
                        settings.TAG_LINE1A = reader["TAG_LINE1A"] as string;
                        settings.TAG_LINE2A = reader["TAG_LINE2A"] as string;
                        settings.TAG_LINE3A = reader["TAG_LINE3A"] as string;
                        settings.TAG_LINE4A = reader["TAG_LINE4A"] as string;
                        settings.TAG_LINE5A = reader["TAG_LINE5A"] as string;
                        settings.TAG_LINE1B = reader["TAG_LINE1B"] as string;
                        settings.TAG_LINE2B = reader["TAG_LINE2B"] as string;
                        settings.TAG_LINE3B = reader["TAG_LINE3B"] as string;
                        settings.TAG_LINE4B = reader["TAG_LINE4B"] as string;
                        settings.TAG_LINE5B = reader["TAG_LINE5B"] as string;
                        settings.TAG_LINE1C = reader["TAG_LINE1C"] as string;
                        settings.TAG_LINE2C = reader["TAG_LINE2C"] as string;
                        settings.TAG_LINE3C = reader["TAG_LINE3C"] as string;
                        settings.TAG_LINE4C = reader["TAG_LINE4C"] as string;
                        settings.TAG_LINE5C = reader["TAG_LINE5C"] as string;
                        settings.TAG_LINE1D = reader["TAG_LINE1D"] as string;
                        settings.TAG_LINE2D = reader["TAG_LINE2D"] as string;
                        settings.TAG_LINE3D = reader["TAG_LINE3D"] as string;
                        settings.TAG_LINE4D = reader["TAG_LINE4D"] as string;
                        settings.TAG_LINE5D = reader["TAG_LINE5D"] as string;
                        settings.TAG_LINE1E = reader["TAG_LINE1E"] as string;
                        settings.TAG_LINE2E = reader["TAG_LINE2E"] as string;
                        settings.TAG_LINE3E = reader["TAG_LINE3E"] as string;
                        settings.TAG_LINE4E = reader["TAG_LINE4E"] as string;
                        settings.TAG_LINE5E = reader["TAG_LINE5E"] as string;
                        settings.DIAMONDLABELPRINTER = reader["DIAMONDLABELPRINTER"] as string;
                        settings.LastPictureFolderpath = reader["LastPictureFolderpath"] as string;
                        settings.AppraisalSignature = reader["AppraisalSignature"] as byte[];
                        settings.desc11 = reader["desc11"] as string;
                        settings.desc12 = reader["desc12"] as string;
                        settings.desc13 = reader["desc13"] as string;
                        settings.desc14 = reader["desc14"] as string;
                        settings.desc15 = reader["desc15"] as string;
                        settings.desc16 = reader["desc16"] as string;
                        settings.desc17 = reader["desc17"] as string;
                        settings.desc18 = reader["desc18"] as string;
                        settings.desc19 = reader["desc19"] as string;
                        settings.desc20 = reader["desc20"] as string;
                        settings.WEDI_ID = reader["WEDI_ID"] as string;
                        settings.Package_Noofitems = (int)reader["Package_Noofitems"];
                        settings.COSTCODE_DEFAULTCHAR = reader["COSTCODE_DEFAULTCHAR"] as string;
                        settings.TAG_RIGHT4 = reader["TAG_RIGHT4"] as string;
                        settings.TAG_RIGHT4A = reader["TAG_RIGHT4A"] as string;
                        settings.TAG_RIGHT4B = reader["TAG_RIGHT4B"] as string;
                        settings.TAG_RIGHT5 = reader["TAG_RIGHT5"] as string;
                        settings.TAG_RIGHT5A = reader["TAG_RIGHT5A"] as string;
                        settings.TAG_RIGHT5B = reader["TAG_RIGHT5B"] as string;
                        settings.MinPricePassword = reader["MinPricePassword"] as string;
                        settings.gl_repair_inventory = reader["gl_repair_inventory"] as string;
                        settings.gl_ovh_adj = reader["gl_ovh_adj"] as string;
                        settings.gl_repair_labor = reader["gl_repair_labor"] as string;
                        settings.gl_rpr_lbr_adj = reader["gl_rpr_lbr_adj"] as string;
                        settings.Warranty_note = reader["Warranty_note"] as string;
                        settings.gl_storecredit = reader["gl_storecredit"] as string;
                        settings.WtInStock = reader["WtInStock"] as string;
                        settings.BrinksLabelPath = reader["BrinksLabelPath"] as string;
                        settings.BrinksUsername = reader["BrinksUsername"] as string;
                        settings.BrinksPwd = reader["BrinksPwd"] as string;
                        settings.Cert_printer_name = reader["Cert_printer_name"] as string;
                        settings.text_for_the_back_of_thecert = reader["text_for_the_back_of_thecert"] as string;
                        settings.WEDI_IMG_PATH = reader["WEDI_IMG_PATH"] as string;
                        settings.JM_API_KEY = reader["JM_API_KEY"] as string;
                        settings.JM_API_PWD = reader["JM_API_PWD"] as string;
                        settings.no_tagprice = (bool)reader["no_tagprice"];
                        settings.po_shipdate = reader["po_shipdate"] as string;
                        settings.PO_CANDATE = reader["PO_CANDATE"] as string;
                        settings.MOVE_BARCODE = reader["MOVE_BARCODE"] as bool?;
                        settings.TAG_RIGHT1 = reader["TAG_RIGHT1"] as string;
                        settings.TAG_RIGHT2 = reader["TAG_RIGHT2"] as string;
                        settings.TAG_RIGHT3 = reader["TAG_RIGHT3"] as string;
                        settings.TAG_RIGHT2A = reader["TAG_RIGHT2A"] as string;
                        settings.TAG_RIGHT2B = reader["TAG_RIGHT2B"] as string;
                    }
                }
            }

            return settings;
        }
    }

    public class UPS_INS
    {
        private readonly ConnectionProvider _connectionProvider;


        public UPS_INS(ConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public string StoreCode
        {
            set
            {
                // Fetch and initialize the Store object based on the provided StoreCode
                Store = FetchStoreFromDatabase(value);
            }
        }

        public Store Store { get; private set; }

        private Store FetchStoreFromDatabase(string storeCode)
        {
            // Initialize a new store object
            Store store = null;

            using (var conn = _connectionProvider.GetConnection())
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // SQL query to fetch the store details
                    string query = "SELECT * FROM dbo.stores WHERE code = @StoreCode";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the parameter to the SQL query
                        cmd.Parameters.AddWithValue("@StoreCode", storeCode);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Read the data from the database and populate the store object
                                while (reader.Read())
                                {
                                    store = new Store
                                    {
                                        Code = reader["code"] as string,
                                        Name = reader["name"] as string,
                                        Addr1 = reader["addr1"] as string,
                                        Addr2 = reader["addr2"] as string,
                                        Addr3 = reader["addr3"] as string,
                                        Addr4 = reader["addr4"] as string,
                                        Tel = reader["tel"] as string,
                                        SalesTax = reader["sales_tax"] as decimal?,
                                        Inactive = reader["inactive"] as bool?,
                                        Dept = reader["DEPT"] as string,
                                        Ccmid = reader["CCMID"] as string,
                                        Cchsn = reader["CCHSN"] as string,
                                        Ccusername = reader["CCUSERNAME"] as string,
                                        Ccpasswd = reader["CCpasswd"] as string,
                                        BankAcc = reader["bank_acc"] as string,
                                        SqLocation = reader["sq_location"] as string,
                                        SqDeviceid = reader["sq_deviceid"] as string,
                                        FeedbackLink = reader["feedback_link"] as string,
                                        StoreLogo = reader["STORE_LOGO"] as byte[],
                                        InvoiceNote = reader["invoicenote"] as string,
                                        MemoNote = reader["memonote"] as string,
                                        InvoiceSmstext = reader["INVOICESMSTEXT"] as string,
                                        RepairSmstext = reader["REPAIRSMSTEXT"] as string,
                                        ScrapBank = reader["scrap_bank"] as string,
                                        DepositBank = reader["deposit_bank"] as string,
                                        Notext = reader["notext"] as bool?,
                                        Airport = reader["airport"] as string,
                                        LogoStore = reader["LOGO_STORE"] as string,
                                        City = reader["city"] as string,
                                        State = reader["state"] as string,
                                        Zip = reader["zip"] as string,
                                        LogoId = reader["Logo_ID"] as int?
                                    };
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions (logging, etc.)
                    Console.WriteLine($"Error fetching data: {ex.Message}");
                }
            }

            return store;
        }

    }
    public class Store
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Addr3 { get; set; }
        public string Addr4 { get; set; }
        public string Tel { get; set; }
        public decimal? SalesTax { get; set; }
        public bool? Inactive { get; set; }
        public string Dept { get; set; }
        public string Ccmid { get; set; }
        public string Cchsn { get; set; }
        public string Ccusername { get; set; }
        public string Ccpasswd { get; set; }
        public string BankAcc { get; set; }
        public string SqLocation { get; set; }
        public string SqDeviceid { get; set; }
        public string FeedbackLink { get; set; }
        public byte[] StoreLogo { get; set; }
        public string InvoiceNote { get; set; }
        public string MemoNote { get; set; }
        public string InvoiceSmstext { get; set; }
        public string RepairSmstext { get; set; }
        public string ScrapBank { get; set; }
        public string DepositBank { get; set; }
        public bool? Notext { get; set; }
        public string Airport { get; set; }
        public string LogoStore { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int? LogoId { get; set; }
    }

}
