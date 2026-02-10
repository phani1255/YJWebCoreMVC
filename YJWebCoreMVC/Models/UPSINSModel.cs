using YJWebCoreMVC.Services;

namespace YJWebCoreMVC.Models
{
    public class UPSINSModel
    {

        public decimal? INSURANCE { get; set; }
        public decimal? COD { get; set; }
        public decimal? RESIDENT { get; set; }
        public decimal? SATURDAY { get; set; }
        public decimal? MIN_INS { get; set; }
        public string BKUP_FLD { get; set; }
        public string RAP_USR { get; set; }
        public string RAP_PW { get; set; }
        public string QB_PATH { get; set; }
        public int? STYLE_LEN { get; set; }
        public DateTime? lastsync { get; set; }
        public int? dbversion { get; set; }
        public string COMPANYNAME { get; set; }
        public string COMPANY_ADDR1 { get; set; }
        public string COMPANY_ADDR2 { get; set; }
        public string COMPANY_TEL { get; set; }
        public byte[] COMPANY_LOGO { get; set; }
        public string Attrib1 { get; set; }
        public string Attrib2 { get; set; }
        public string Attrib3 { get; set; }
        public string Attrib4 { get; set; }
        public string Attrib5 { get; set; }
        public string Attrib6 { get; set; }
        public string Attrib7 { get; set; }
        public string Attrib8 { get; set; }
        public string Attrib9 { get; set; }
        public string Attrib10 { get; set; }
        public string Attrib11 { get; set; }
        public string Attrib12 { get; set; }
        public string Attrib13 { get; set; }
        public string Attrib14 { get; set; }
        public string Attrib15 { get; set; }
        public string Attrib16 { get; set; }
        public string Attrib17 { get; set; }
        public string Attrib18 { get; set; }
        public string CustAttr1 { get; set; }
        public string CustAttr2 { get; set; }
        public string CustAttr3 { get; set; }
        public string CustAttr4 { get; set; }
        public string CustAttr5 { get; set; }
        public string CustAttr6 { get; set; }
        public string CustAttr7 { get; set; }
        public string CustAttr8 { get; set; }
        public string CustMultiAttr1 { get; set; }
        public string CustMultiAttr2 { get; set; }
        public string CustMultiAttr3 { get; set; }
        public string CustCheck1 { get; set; }
        public string CustCheck2 { get; set; }
        public string CustCheck3 { get; set; }
        public string CustCheck4 { get; set; }
        public string CustCheck5 { get; set; }
        public string CustCheck6 { get; set; }
        public string CustCheck7 { get; set; }
        public string CustCheck8 { get; set; }
        public decimal? LEFT_MARGIN { get; set; }
        public decimal? RIGHT_MARGIN { get; set; }
        public decimal? CINC { get; set; }
        public decimal? TOP_MARGIN { get; set; }
        public string PRINTERPORT { get; set; }
        public bool? MICR { get; set; }
        public string SCANPATH { get; set; }
        public string pwd { get; set; }
        public DateTime? QBVALIDATEDON { get; set; }
        public string cert_path { get; set; }
        public bool? NEGATIVEINV { get; set; }
        public bool? AUTOSANDH { get; set; }
        public string INVOICENOTE { get; set; }
        public string MEMONOTE { get; set; }
        public string print_mode { get; set; }
        public bool? diamond_dealer { get; set; }
        public string StyleField1 { get; set; }
        public string StyleField2 { get; set; }
        public string appversion { get; set; }
        public decimal? Tag_multiplier { get; set; }
        public string ImagesPath { get; set; }
        public decimal CUST_BOUNCE_FEE { get; set; }
        public decimal BANK_BOUNCE_FEE { get; set; }
        public string FedexLabelPath { get; set; }
        public string FedexKey { get; set; }
        public string FedexPwd { get; set; }
        public string FedexAccount { get; set; }
        public string FedexMeter { get; set; }
        public string FedexPayorAccount { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyZip { get; set; }
        public string CompanyState { get; set; }
        public string Attrib19 { get; set; }
        public string Attrib20 { get; set; }
        public string Attrib21 { get; set; }
        public string StyleCheck1 { get; set; }
        public string StyleCheck2 { get; set; }
        public string StyleCheck3 { get; set; }
        public string StyleCheck4 { get; set; }
        public string StyleCheck5 { get; set; }
        public string StyleCheck6 { get; set; }
        public string RFID_IP { get; set; }
        public string PRINTERPORT2 { get; set; }
        public string PRINTERPORT3 { get; set; }
        public string UpsAccessKey { get; set; }
        public string UpsUsername { get; set; }
        public string UpsPassword { get; set; }
        public string UpsShipperNo { get; set; }
        public string UpsLabelType { get; set; }
        public string UpsLabelPath { get; set; }
        public decimal multi_lnm { get; set; }
        public decimal gold_sur { get; set; }
        public string tag_left1 { get; set; }
        public string tag_left2 { get; set; }
        public string tag_left3 { get; set; }
        public string tag_left4 { get; set; }
        public string Rfid_Port { get; set; }
        public decimal RfidPrintLeft { get; set; }
        public decimal RfidPrintRight { get; set; }
        public decimal RfidPrintTop { get; set; }
        public decimal RfidPrintCinc { get; set; }
        public string company_email { get; set; }
        public string INVHISTPATH { get; set; }
        public string Rfid_printer_port { get; set; }
        public string Cost_Code { get; set; }
        public string StyleField3 { get; set; }
        public string StyleField4 { get; set; }
        public string StyleField5 { get; set; }
        public bool PrintAging { get; set; }
        public bool PCS_DECIMAL { get; set; }
        public string gl_sales { get; set; }
        public string gl_snh { get; set; }
        public string gl_ar { get; set; }
        public string gl_memoprice { get; set; }
        public string gl_memocost { get; set; }
        public string gl_cogs { get; set; }
        public string gl_undep_funds { get; set; }
        public string gl_cashinbank { get; set; }
        public string gl_slsinvnt { get; set; }
        public string gl_comision { get; set; }
        public string gl_ap { get; set; }
        public string gl_inventory { get; set; }
        public string gl_cons { get; set; }
        public string tag_left1A { get; set; }
        public string tag_left2A { get; set; }
        public string tag_left3A { get; set; }
        public string tag_left4A { get; set; }
        public string no_stores { get; set; }
        public string store { get; set; }
        public string store2 { get; set; }
        public string store3 { get; set; }
        public string store4 { get; set; }
        public bool No_Memo { get; set; }
        public string ASSET_GL { get; set; }
        public string COGS_GL { get; set; }
        public string CLEAR_GL { get; set; }
        public string SALES_GL { get; set; }
        public string TAG_LEFT5 { get; set; }
        public string TAG_LEFT5A { get; set; }
        public bool? USE_GLCODE { get; set; }
        public string desc1 { get; set; }
        public string desc2 { get; set; }
        public string desc3 { get; set; }
        public string desc4 { get; set; }
        public string desc5 { get; set; }
        public string desc6 { get; set; }
        public string desc7 { get; set; }
        public string desc8 { get; set; }
        public string desc9 { get; set; }
        public string desc10 { get; set; }
        public byte[] PrinterSettings { get; set; }
        public string statmentofvaluenotes { get; set; }
        public string rfid_ip2 { get; set; }
        public string rfid_ip3 { get; set; }
        public string rfid_port2 { get; set; }
        public string rfid_port3 { get; set; }
        public int? invoicecopies { get; set; }
        public string TAG_LEFT1B { get; set; }
        public string TAG_LEFT2B { get; set; }
        public string TAG_LEFT3B { get; set; }
        public string TAG_LEFT4B { get; set; }
        public string TAG_LEFT5B { get; set; }
        public decimal? LEFT_MARGIN2 { get; set; }
        public decimal? RIGHT_MARGIN2 { get; set; }
        public decimal? TOP_MARGIN2 { get; set; }
        public decimal? CINC2 { get; set; }
        public decimal? LEFT_MARGIN3 { get; set; }
        public decimal? RIGHT_MARGIN3 { get; set; }
        public decimal? TOP_MARGIN3 { get; set; }
        public decimal? CINC3 { get; set; }
        public string gl_inventory_transit { get; set; }
        public string DISC_GL { get; set; }
        public int? Warranty { get; set; }
        public string Repair_note { get; set; }
        public string Repair_invoice_note { get; set; }
        public int min_barcode { get; set; }
        public int max_barcode { get; set; }
        public string gl_Memo_Liability { get; set; }
        public string cc_mid { get; set; }
        public string cc_hsn { get; set; }
        public string cc_username { get; set; }
        public string cc_authorization { get; set; }
        public byte[] cc_pwd { get; set; }
        public string REPAIR_DISCLAIMER { get; set; }
        public string gl_trade_in { get; set; }
        public string gl_warranty_cogs { get; set; }
        public string gl_warranty_sales { get; set; }
        public int printer_font { get; set; }
        public string gl_inv_spl_order { get; set; }
        public int lastversion { get; set; }
        public string TAG_PLACE { get; set; }
        public string TAG_TEXT { get; set; }
        public decimal GP_PERCENT { get; set; }
        public decimal KARAT_REDUCTION { get; set; }
        public decimal PAYOUT_PERCENT { get; set; }
        public decimal WT_REDUCTION { get; set; }
        public string SCRAP_DISCLAIMER { get; set; }
        public string gl_vendor_credit { get; set; }
        public string pversion { get; set; }
        public string gl_financed { get; set; }
        public string gl_giftcard { get; set; }
        public bool? DPRIORITY { get; set; }
        public bool signature { get; set; }
        public string store5 { get; set; }
        public string gl_scrap { get; set; }
        public string backup_folder { get; set; }
        public int GIP { get; set; }
        public string GL_MATERIAL { get; set; }
        public string GL_OVER_HEAD { get; set; }
        public string GL_REPAIR_SALES { get; set; }
        public string JomaUser { get; set; }
        public string JomaPwd { get; set; }
        public string WFAPIToken { get; set; }
        public string STYLE { get; set; }
        public string QtyInStock { get; set; }
        public string sq_loc_code { get; set; }
        public string sq_authcode { get; set; }
        public string clover_authcode { get; set; }
        public int refreshinterval { get; set; }
        public string email_signature { get; set; }
        public string sq_appid { get; set; }
        public string sq_clientsecret { get; set; }
        public DateTime? sq_tokenexpiredate { get; set; }
        public string sq_locationcode { get; set; }
        public string sq_merchantid { get; set; }
        public string sq_deviceserial { get; set; }
        public byte[] sq_accesstoken { get; set; }
        public byte[] sq_refreshtoken { get; set; }
        public int Left_Top { get; set; }
        public int Left_Top2 { get; set; }
        public int Left_Top3 { get; set; }
        public bool Not_SideBySide { get; set; }
        public bool Not_SideBySide2 { get; set; }
        public bool Not_SideBySide3 { get; set; }
        public string servername { get; set; }
        public string Customer_acc { get; set; }
        public string vendor_acc { get; set; }
        public string gl_inv_adjust { get; set; }
        public string AddressLabel_printer { get; set; }
        public string Jobbag_printer { get; set; }
        public decimal Daily_SaleTarget { get; set; }
        public decimal Monthly_SaleTarget { get; set; }
        public decimal Yearly_SaleTarget { get; set; }
        public string CloverPairingToken { get; set; }
        public string CloverSecrureURL { get; set; }
        public string CloverRemoteId { get; set; }
        public string CloverDeviceSerial { get; set; }
        public string Statement_MSG { get; set; }
        public string GIA_KEY { get; set; }
        public int ltimer { get; set; }
        public int DecimalsInPieces { get; set; }
        public string APPRAISER_NAME { get; set; }
        public string PORTTEMPLATE1 { get; set; }
        public string PORTTEMPLATE2 { get; set; }
        public string PORTTEMPLATE3 { get; set; }
        public bool Address_Landscape { get; set; }
        public string INVOICESMSTEXT { get; set; }
        public string REPAIRSMSTEXT { get; set; }
        public string text_from { get; set; }
        public string whatsapp_from { get; set; }
        public int ESPInsp_DueMonths { get; set; }
        public int LayawayPay_DueDays { get; set; }
        public string LABEL_LEFT_MARGIN { get; set; }
        public string LABEL_RIGHT_MARGIN { get; set; }
        public string LABEL_LINES_MARGIN { get; set; }
        public string LABEL_PRINTERNAME { get; set; }
        public decimal repair_value { get; set; }
        public string picture_printer { get; set; }
        public decimal email_inv_amount { get; set; }
        public string email_inv_addr { get; set; }
        public decimal pricerange1 { get; set; }
        public decimal pricerange2 { get; set; }
        public decimal pricerange3 { get; set; }
        public decimal pricerange4 { get; set; }
        public decimal pricerange5 { get; set; }
        public decimal pricerange6 { get; set; }
        public decimal pricerange7 { get; set; }
        public decimal pricerange8 { get; set; }
        public string ShopifyAPI { get; set; }
        public string ShopifyPwd { get; set; }
        public string ShopifySecret { get; set; }
        public string ShopifyURL { get; set; }
        public int CASHDRAWPORT { get; set; }
        public string gl_sales_tax { get; set; }
        public string jm_store { get; set; }
        public string receipt_printer { get; set; }
        public string TAG_LINE1 { get; set; }
        public string TAG_LINE2 { get; set; }
        public string TAG_LINE3 { get; set; }
        public string TAG_LINE4 { get; set; }
        public string TAG_LINE5 { get; set; }
        public string TAG_LINE1A { get; set; }
        public string TAG_LINE2A { get; set; }
        public string TAG_LINE3A { get; set; }
        public string TAG_LINE4A { get; set; }
        public string TAG_LINE5A { get; set; }
        public string TAG_LINE1B { get; set; }
        public string TAG_LINE2B { get; set; }
        public string TAG_LINE3B { get; set; }
        public string TAG_LINE4B { get; set; }
        public string TAG_LINE5B { get; set; }
        public string TAG_LINE1C { get; set; }
        public string TAG_LINE2C { get; set; }
        public string TAG_LINE3C { get; set; }
        public string TAG_LINE4C { get; set; }
        public string TAG_LINE5C { get; set; }
        public string TAG_LINE1D { get; set; }
        public string TAG_LINE2D { get; set; }
        public string TAG_LINE3D { get; set; }
        public string TAG_LINE4D { get; set; }
        public string TAG_LINE5D { get; set; }
        public string TAG_LINE1E { get; set; }
        public string TAG_LINE2E { get; set; }
        public string TAG_LINE3E { get; set; }
        public string TAG_LINE4E { get; set; }
        public string TAG_LINE5E { get; set; }
        public string DIAMONDLABELPRINTER { get; set; }
        public string LastPictureFolderpath { get; set; }
        public byte[] AppraisalSignature { get; set; }
        public string desc11 { get; set; }
        public string desc12 { get; set; }
        public string desc13 { get; set; }
        public string desc14 { get; set; }
        public string desc15 { get; set; }
        public string desc16 { get; set; }
        public string desc17 { get; set; }
        public string desc18 { get; set; }
        public string desc19 { get; set; }
        public string desc20 { get; set; }
        public string WEDI_ID { get; set; }
        public int Package_Noofitems { get; set; }
        public string COSTCODE_DEFAULTCHAR { get; set; }
        public string TAG_RIGHT4 { get; set; }
        public string TAG_RIGHT4A { get; set; }
        public string TAG_RIGHT4B { get; set; }
        public string TAG_RIGHT5 { get; set; }
        public string TAG_RIGHT5A { get; set; }
        public string TAG_RIGHT5B { get; set; }
        public string MinPricePassword { get; set; }
        public string gl_repair_inventory { get; set; }
        public string gl_ovh_adj { get; set; }
        public string gl_repair_labor { get; set; }
        public string gl_rpr_lbr_adj { get; set; }
        public string Warranty_note { get; set; }
        public string gl_storecredit { get; set; }
        public string WtInStock { get; set; }
        public string BrinksLabelPath { get; set; }
        public string BrinksUsername { get; set; }
        public string BrinksPwd { get; set; }
        public string Cert_printer_name { get; set; }
        public string text_for_the_back_of_thecert { get; set; }
        public string WEDI_IMG_PATH { get; set; }
        public string JM_API_KEY { get; set; }
        public string JM_API_PWD { get; set; }
        public bool no_tagprice { get; set; }
        public string po_shipdate { get; set; }
        public string PO_CANDATE { get; set; }
        public bool? MOVE_BARCODE { get; set; }
        public string TAG_RIGHT1 { get; set; }
        public string TAG_RIGHT2 { get; set; }
        public string TAG_RIGHT3 { get; set; }
        public string TAG_RIGHT2A { get; set; }
        public string TAG_RIGHT2B { get; set; }
        public Store Store { get; private set; }

        //public string StoreCode
        //{
        //    set
        //    {
        //        // Fetch and initialize the Store object based on the provided StoreCode
        //        Store = FetchStoreFromDatabase(value);
        //    }
        //}

        //private Store FetchStoreFromDatabase(string storeCode)
        //{
        //    // Initialize a new store object
        //    Store store = null;

        //    using (SqlConnection conn = _connectionProvider.GetConnection())
        //    {
        //        try
        //        {
        //            // Open the connection
        //            conn.Open();

        //            // SQL query to fetch the store details
        //            string query = "SELECT * FROM dbo.stores WHERE code = @StoreCode";

        //            using (SqlCommand cmd = new SqlCommand(query, conn))
        //            {
        //                // Add the parameter to the SQL query
        //                cmd.Parameters.AddWithValue("@StoreCode", storeCode);

        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        // Read the data from the database and populate the store object
        //                        while (reader.Read())
        //                        {
        //                            store = new Store
        //                            {
        //                                Code = reader["code"] as string,
        //                                Name = reader["name"] as string,
        //                                Addr1 = reader["addr1"] as string,
        //                                Addr2 = reader["addr2"] as string,
        //                                Addr3 = reader["addr3"] as string,
        //                                Addr4 = reader["addr4"] as string,
        //                                Tel = reader["tel"] as string,
        //                                SalesTax = reader["sales_tax"] as decimal?,
        //                                Inactive = reader["inactive"] as bool?,
        //                                Dept = reader["DEPT"] as string,
        //                                Ccmid = reader["CCMID"] as string,
        //                                Cchsn = reader["CCHSN"] as string,
        //                                Ccusername = reader["CCUSERNAME"] as string,
        //                                Ccpasswd = reader["CCpasswd"] as string,
        //                                BankAcc = reader["bank_acc"] as string,
        //                                SqLocation = reader["sq_location"] as string,
        //                                SqDeviceid = reader["sq_deviceid"] as string,
        //                                FeedbackLink = reader["feedback_link"] as string,
        //                                StoreLogo = reader["STORE_LOGO"] as byte[],
        //                                InvoiceNote = reader["invoicenote"] as string,
        //                                MemoNote = reader["memonote"] as string,
        //                                InvoiceSmstext = reader["INVOICESMSTEXT"] as string,
        //                                RepairSmstext = reader["REPAIRSMSTEXT"] as string,
        //                                ScrapBank = reader["scrap_bank"] as string,
        //                                DepositBank = reader["deposit_bank"] as string,
        //                                Notext = reader["notext"] as bool?,
        //                                Airport = reader["airport"] as string,
        //                                LogoStore = reader["LOGO_STORE"] as string,
        //                                City = reader["city"] as string,
        //                                State = reader["state"] as string,
        //                                Zip = reader["zip"] as string,
        //                                LogoId = reader["Logo_ID"] as int?
        //                            };
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle any exceptions (logging, etc.)
        //            Console.WriteLine($"Error fetching data: {ex.Message}");
        //        }
        //    }

        //    return store;
        //}


    }
}
