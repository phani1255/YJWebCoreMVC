/*
 *  Created By Phanindra on 01-May-2025
 *  Phanindra 07/15/2025 Added properties, GetRepairItems, GetDepositPayments, GetRepairItem, CheckValidOrderRepair, PaymentForRepair, 
 *  Phanindra 07/27/2025 Added GetAllRepairTableDataForInvoice method
 *  Phanindra 08/01/2025 Added ListOfRepairOrdersByAcc, creatdatagridbasedonrepid
 *  Hemanth   08/05/2025 Added CheckInvoiceNumberBasedOnInvoiceNumber
 *  Phanidra  08/19/2025 Added UpdateRepairOrderInvoice, DeleteOrderInvoiceDataIntoInSpItTable, AddEditPaymentForRepair
 *  Hemanth   08/25/2025 Added Send2Shop
 *  Hemanth   08/26/2025 Added RepRcvInShop
 *  Phanindra 08/26/2025 Modified AddEditPaymentForRepair method
 *  Manoj     11/03/2025 Added CheckRprJob Method
 *  Dharani   01/19/2026 Added RepairOrderEnvolapes and GetOrderRepairData methods.
 */
using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class OrderRepairService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public OrderRepairService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable GetRepairItems(string ordnumber)
        {
            return _helperCommonService.GetSqlData(@"SELECT ITEM,iSNULL(STYLE,'') STYLE, STAT AS DESCRIPTION,VENDOR AS REFNO,BARCODE,SHIPED,NOTE,SIZE,QTY,PRICE,Disc_Per_Line,ISNULL(QTY*(PRICE * (100 - iSNULL(Disc_Per_Line,0)) / 100),0) AS G_TOT,0 as CRow, is_tax FROM REP_ITEM WHERE trim(REPAIR_NO) = trim(@REPAIR_NO)",
                "@REPAIR_NO", ordnumber);
        }

        public DataTable GetDepositPayments(string inv_no, bool showlayaway, bool is_return)
        {
            var dataTable = new DataTable();
            // Use a "using" statement to ensure the connection and command are disposed properly
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetREPDepositDet", connection))
            using (var dataAdapter = new SqlDataAdapter(command))
            {
                // Set command properties
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters with explicit types to avoid potential issues with implicit conversion
                command.Parameters.Add("@inv_no", SqlDbType.VarChar, 10).Value = inv_no;
                command.Parameters.Add("@showlayaway", SqlDbType.Bit).Value = showlayaway;

                // Open connection, fill the DataTable, and return it
                connection.Open();
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataRow GetRepairItem(string ordnumber, bool isErepir = false)
        {
            ordnumber = "(" + string.Format("'{0}'", (ordnumber.Trim())).Replace(",", "','") + ")";

            if (isErepir)
                return _helperCommonService.GetSqlRow(@"SELECT ITEM,REP_ITEM.SIZE,QTY, STAT AS DESCRIPTION,VENDOR AS REFNO,BARCODE,SHIPED,INV_NO,repair.repStatus FROM REP_ITEM left join repair on REP_ITEM.repair_no = repair.repair_no WHERE Repair.Inv_no in " + ordnumber);
            return _helperCommonService.GetSqlRow(@"SELECT ITEM,REP_ITEM.SIZE,QTY, STAT AS DESCRIPTION,VENDOR AS REFNO,BARCODE,SHIPED,INV_NO,repair.repStatus FROM REP_ITEM left join repair on REP_ITEM.repair_no = repair.repair_no WHERE REP_ITEM.REPAIR_NO  in" + ordnumber);
        }
        public DataTable CheckValidOrderRepair(string Rpno)
        {
            Rpno = Rpno.Trim();
            return _helperCommonService.GetSqlData("select * from REPAIR where trim(repair_no) = @Rpno", "@Rpno", Rpno);
        }

        public bool PaymentForRepair(string invno, string acc, string pcname, string grtotal, string paymentItems, string UserGCNo, string StoreCode, string Cash_Register, out string out_inv_no, bool ispayment = false, bool is_update = false, bool is_return = false, string storecodeinuse = "", string xmlDiscount = "")
        {
            bool result = false;
            out_inv_no = string.Empty;

            using (var connection = _connectionProvider.GetConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                // If not a return, update invoice first
                if (!is_return)
                {
                    using (var updateCommand = new SqlCommand("UPDATE_INVOICE", connection, transaction))
                    {
                        updateCommand.CommandType = CommandType.StoredProcedure;
                        updateCommand.Parameters.AddWithValue("@INV_NO", invno);
                        updateCommand.Parameters.AddWithValue("@From_Repair", true);
                        updateCommand.CommandTimeout = 5000;
                        updateCommand.ExecuteNonQuery();
                    }
                }

                // Process payment for repair
                using (var paymentCommand = new SqlCommand("PaymentForRepair", connection, transaction))
                {
                    paymentCommand.CommandType = CommandType.StoredProcedure;
                    paymentCommand.CommandTimeout = 5000;

                    // Add parameters
                    paymentCommand.Parameters.AddWithValue("@INV_NO", invno);
                    paymentCommand.Parameters.AddWithValue("@ACC", acc);
                    paymentCommand.Parameters.AddWithValue("@PCNAME", pcname);
                    paymentCommand.Parameters.AddWithValue("@DATE", DateTime.Now);
                    paymentCommand.Parameters.AddWithValue("@GR_TOTAL", grtotal);
                    paymentCommand.Parameters.AddWithValue("@ISPAYMENT", ispayment ? 1 : 0);
                    paymentCommand.Parameters.AddWithValue("@IS_RETURN", is_return ? 1 : 0);
                    paymentCommand.Parameters.AddWithValue("@IS_UPDATE", is_update ? 1 : 0);
                    paymentCommand.Parameters.Add("@OUT_INV_NO", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                    paymentCommand.Parameters.AddWithValue("@UserGCNO", UserGCNo);
                    paymentCommand.Parameters.AddWithValue("@StoreCode", StoreCode);
                    paymentCommand.Parameters.AddWithValue("@CASH_REG_CODE", Cash_Register);
                    paymentCommand.Parameters.AddWithValue("@storecodeinuse", storecodeinuse);

                    // Payment items and discount items as XML
                    paymentCommand.Parameters.Add("@TBLPAYMENTITEMS", SqlDbType.Xml).Value = paymentItems;
                    paymentCommand.Parameters.Add("@TBLDISCOUNTITEMS", SqlDbType.Xml).Value = xmlDiscount;

                    // Execute payment command
                    var rowsAffected = paymentCommand.ExecuteNonQuery();
                    out_inv_no = Convert.ToString(paymentCommand.Parameters["@OUT_INV_NO"].Value);

                    if (rowsAffected > 0)
                    {
                        result = true;
                        transaction.Commit();  // Commit the transaction if everything is successful
                    }
                }
            }
            return result;
        }

        public DataTable GetAllRepairTableDataForInvoice(string ordnumber, bool isErepair = false)
        {
            ordnumber = "(" + string.Format("'{0}'", (ordnumber.Trim())).Replace(",", "','") + ")";
            if (isErepair)
                return _helperCommonService.GetSqlData(@"SELECT iSNULL(RI.ITEM,'') ITEM,RI.STYLE,RI.SIZE,RI.NOTE AS DESCRIPTION,RI.QTY AS RPRQTY,(RI.QTY - RI.SHIPED) as [OPEN],(RI.QTY - RI.SHIPED) AS RSRVD,convert(int, isnull(RI.QTY,0)) QTY ,(iSNULL(RI.PRICE,0) * (100 - iSNULL(RI.Disc_Per_Line,0)) / 100) AS CHARGE,RI.VENDOR AS REFNO,RI.BARCODE,RI.LINE, CAST(0 AS DECIMAL(12,2)) AS G_TOT,convert(int, RI.QTY) as INVQTY1,NULL AS INVOICEQTY, IS_TAX,R.SNH FROM REP_ITEM RI LEFT JOIN REPAIR R ON RI.REPAIR_NO = R.REPAIR_NO  WHERE R.Inv_no in" + ordnumber);// AND (RI.QTY - RI.SHIPED) != 0";
            return _helperCommonService.GetSqlData(@"SELECT RI.Repair_No,iSNULL(RI.ITEM,'') ITEM,RI.STYLE,RI.SIZE,RI.NOTE AS DESCRIPTION,RI.QTY AS RPRQTY,(RI.QTY - RI.SHIPED) as [OPEN],(RI.QTY - RI.SHIPED) AS RSRVD,convert(int, isnull(RI.QTY,0)) QTY ,(iSNULL(RI.PRICE,0) * (100 - iSNULL(RI.Disc_Per_Line,0)) / 100) AS CHARGE,RI.VENDOR AS REFNO,RI.BARCODE,RI.LINE, CAST(0 AS DECIMAL(12,2)) AS G_TOT,convert(int, RI.QTY) as INVQTY1,NULL AS INVOICEQTY, IS_TAX, R.SNH FROM REP_ITEM RI LEFT JOIN REPAIR R ON RI.REPAIR_NO = R.REPAIR_NO  WHERE RI.REPAIR_NO in" + ordnumber);// AND (RI.QTY - RI.SHIPED) != 0";
        }

        public DataTable ListOfRepairOrdersByAcc(string CACC, bool openonly)
        {
            return _helperCommonService.GetSqlData(@"select max(R.REPAIR_NO) as REPAIR_NO, max(R.ACC) AS ACC, max(date) as DATE, 
                sum(ri.qty) as QTY, SUM(isnull(ri.qty,0) - isnull(ri.[shiped],0)) as [OPEN] 
                FROM REPAIR R inner join  REP_ITEM RI on  R.REPAIR_NO = RI.REPAIR_NO 
                WHERE isnumeric(R.REPAIR_no)=1 and R.ACC=@CACC  " + (openonly ? " and ri.qty>ri.shiped " : "") +
                " group by ri.repair_no  ORDER BY RI.REPAIR_NO DESC", "@CACC", CACC);
        }

        public DataTable creatdatagridbasedonrepid(string currentrepno, bool isplit = false, bool iSOnJobbag = false)
        {
            string[] parts = Array.ConvertAll(currentrepno.Split(','), p => p.Trim());
            for (var i = 0; i < parts.Length; i++)
                parts[i] = parts[i].PadLeft(7, '0');
            string repNos = string.Join(",", parts);
            repNos = "(" + string.Format("'{0}'", (repNos.Trim().PadLeft(7, '0'))).Replace(",", "','") + ")";

            string splitrepNos = string.Join(",", parts);
            splitrepNos = "(" + string.Format("'{0}'", (splitrepNos.Trim().PadLeft(7, '0').Substring(0, 7))).Replace(",", "','") + ")";

            String cmd = $@"SELECT REPAIR.REPAIR_NO,REP_ITEM.LINE, iif(iSNULL(REP_ITEM.ITEM,'')='',REP_ITEM.STYLE,REP_ITEM.ITEM) ITEM, IIF(@isplit='True' , iSNULL((SELECT SUM(QTY) QTY FROM LBL_BAR WHERE BARCODE in {repNos} and style=REP_ITEM.ITEM   GROUP BY BARCODE),0), REP_ITEM.QTY) QTY, CAST(IIF(ISNULL(REPAIR.INV_NO,'')  = '',(REP_ITEM.PRICE * (100 - REP_ITEM.Disc_Per_Line) / 100),(REP_ITEM.PRICE * (100 - REP_ITEM.Disc_Per_Line) / 100)) AS DECIMAL(15,2)) as PRICE, REP_ITEM.NOTE,REP_ITEM.SHIPED,REP_ITEM.STAT,REP_ITEM.VENDOR,replicate('0', 6 - len(REP_ITEM.BARCODE)) + cast (REP_ITEM.BARCODE as varchar) AS BARCODE,REP_ITEM.SIZE,repair.NAME,cast(repair.DATE as date) as DATE,repair.ADDR1,repair.COUNTRY,repair.MESSAGE,repair.ACC,repair.ISSUE_CRDT,repair.is_cod,repair.cod_type,repair.early,repair.ADDR2,repair.CITY,repair.STATE,repair.ZIP,cast(repair.CAN_DATE as date) as CAN_DATE ,cast(repair.RCV_DATE as date) as RCV_DATE,cast(repair.DATE as date) as DATE,repair.CUS_REP_NO,repair.CUS_DEB_NO,(REP_ITEM.QTY - REP_ITEM.SHIPED) as [OPEN] ,c.NAME2 as name2, c.addr2 as addr2, c.addr22 as addr22, c.city2 as city2, c.state2 as state2, c.zip2 as zip2,c.Tel,c.Email,repair.shiptype,repair.resident, repair.estimate, repair.taxable, repair.sales_tax,repair.Message1,repair.salesman1,II.REPAIR_NOTE,repair.inv_no,repair.Jeweler_note,repair.deduction,repair.store,repair.no_taxresion,repair.salesman2,repair.comish1,repair.comish2,repair.comishamount1,repair.comishamount2,repair.Sales_Fee_Amount,repair.Sales_Fee_Rate,repair.repStatus,rep_size,rep_metal,REPAIR.setter, iSNULL(REP_ITEM.Disc_Per_Line,0) Disc_Per_Line,
                repair.warranty_inv_no,repair.style,repair.ship_by,repair.weight,repair.insured,repair.snh,repair.surprise,repair.Estimateready
                FROM REPAIR 
                left join REP_ITEM on REPAIR.REPAIR_NO = REP_ITEM.REPAIR_NO 
                inner join customer c on c.ACC = repair.ACC  LEFT join IN_ITEMS II on REPAIR.INV_NO = II.INV_NO  AND II.STYLE=REP_ITEM.ITEM   
                where right('0000000'+repair.REPAIR_NO,7) in " + (isplit ? splitrepNos : repNos);
            if (iSOnJobbag)
            {
                cmd += " union " + $@"select  R.REPAIR_NO,PH.LINE, ph.CODE ITEM, 
                                    cast(PH.CHANGE as decimal(11,2)) QTY, CAST(0 AS DECIMAL(15,2)) as PRICE, 
                                    PH.NOTE,cast(0 as decimal(11,2)) SHIPED,'' STAT,'' VENDOR,PH.JOB_BAG AS BARCODE,
                                    '' SIZE,R.NAME,cast(R.DATE as date) as DATE,R.ADDR1,R.COUNTRY,R.MESSAGE,R.ACC,R.ISSUE_CRDT,R.is_cod,R.cod_type,R.early,R.ADDR2,R.CITY,R.STATE,R.ZIP,cast(R.CAN_DATE as date) as CAN_DATE ,cast(R.RCV_DATE as date) as RCV_DATE,cast(R.DATE as date) as DATE,R.CUS_REP_NO,R.CUS_DEB_NO,(0 - 0) as [OPEN] ,c.NAME2 as name2, c.addr2 as addr2, c.addr22 as addr22, c.city2 as city2, c.state2 as state2, c.zip2 as zip2,c.Tel,c.Email,R.shiptype,R.resident, R.estimate, R.taxable, R.sales_tax,R.Message1,R.salesman1,'' REPAIR_NOTE,R.inv_no,R.Jeweler_note,R.deduction,R.store,R.no_taxresion,R.salesman2,R.comish1,R.comish2,R.comishamount1,R.comishamount2,R.Sales_Fee_Amount,R.Sales_Fee_Rate,R.repStatus,rep_size,rep_metal,R.setter, 0 Disc_Per_Line,
                                    R.warranty_inv_no,R.style,R.ship_by,R.weight,R.insured,R.snh,R.surprise,R.Estimateready
                                    from PARTS_HIST PH
                                    JOIN REPAIR R on [dbo].[getbarcode](R.REPAIR_NO)=[dbo].[getbarcode](PH.JOB_BAG)
                                    join CUSTOMER C on C.ACC=R.ACC
                                    where right('0000000'+PH.JOB_BAG,7) in {(isplit ? splitrepNos : repNos)}  and code not in(select concat('CODE ',ITEM) from REP_ITEM where  right('0000000'+REPAIR_NO,7) in {(isplit ? splitrepNos : repNos)} )
                                    and ISNULL(PH.ON_JOBBAG,0)=1";
            }
            return _helperCommonService.GetSqlData($@"{cmd}", "@REPAIR_NO", repNos, "@isplit", isplit.ToString());//iSNULL(REP_ITEM.STYLE,'')='' AND
        }

        public DataTable CheckInvoiceNumberBasedOnInvoiceNumber(string Inv_no)
        {
            return _helperCommonService.GetSqlData(string.Format("SELECT PON,GR_TOTAL FROM INVOICE WHERE INV_NO = '{0}' and pon != '' and  pon != 'NONE'", Inv_no.PadLeft(6)));
        }

        public DataTable GetInvoiceHeaderInformatioBasedOnInvoiceNumber(string Inv_no)
        {
            return _helperCommonService.GetSqlData(@"SELECT * FROM INVOICE with (nolock) WHERE INV_NO = @INV_NO AND v_ctl_no = 'REPAIR'",
                "@INV_NO", Inv_no.PadLeft(6));
        }

        public DataTable GetInvoiceInformationForUpdateInvoice(string ordnumber, string inv_no, int isrtnEdit = 1)
        {
            DataTable dataTable = new DataTable();

            string query = @"SELECT distinct
                rp.Repair_no AS Repair_No,
                rp.ITEM AS ITEM,
                rp.STYLE,
                rp.SIZE,
                TRIM(SUBSTRING(isi.[desc], 11, 200)) AS DESCRIPTION,
                rp.QTY AS RPRQTY,
                CONVERT(INT, (rp.QTY - rp.SHIPED) + ri.qty) AS [OPEN],
                CONVERT(INT, (rp.QTY - rp.SHIPED) + ri.qty) AS RSRVD,
                CONVERT(INT, ri.qty * @isrtnEdit) AS INVOICEQTY,
                CAST(isi.price AS DECIMAL(12, 2)) AS CHARGE,
                rp.vendor AS REFN,
                rp.BARCODE,
                rp.line,
                CAST(isi.price AS DECIMAL(12, 2)) AS G_TOT,
                CONVERT(INT, ri.qty * @isrtnEdit) AS INVQTY1,
                NULL AS INVOICEQTY,
                CAST(ISNULL(isi.IS_TAX, 0) AS BIT) AS IS_TAX
            FROM rep_item rp with (nolock)
            LEFT JOIN rep_inv ri with (nolock) ON rp.repair_no = ri.rep_no AND rp.line = ri.line
            LEFT JOIN in_sp_it isi with (nolock) ON ri.inv_no = isi.inv_no
                AND TRIM(isi.line) = TRIM(ri.line)
                AND isi.repair_no = rp.repair_no
                AND REPLACE(rp.ITEM + rp.note, ' ', '') = REPLACE(isi.[DESC], ' ', '')
            WHERE trim(ri.rep_no) IN (SELECT trim(value) FROM STRING_SPLIT(@ordnumber, ',') WHERE trim(value) <> '') AND ri.INV_NO = @INV_NO";

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand(query, _connectionProvider.GetConnection());
                dataAdapter.SelectCommand.CommandType = CommandType.Text;

                // Use parameterized queries to avoid SQL injection risk
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ordnumber", ordnumber); // Safely format the ordnumber list as a parameter
                dataAdapter.SelectCommand.Parameters.AddWithValue("@INV_NO", inv_no.PadLeft(6)); // Ensures INV_NO is properly formatted
                dataAdapter.SelectCommand.Parameters.AddWithValue("@isrtnEdit", isrtnEdit);

                // Open connection and fill the DataTable
                dataAdapter.SelectCommand.Connection.Open();
                dataAdapter.Fill(dataTable);
                dataAdapter.SelectCommand.Connection.Close();
            }

            return dataTable;
        }

        public DataTable GetInvoicePayments(string invNo, bool showLayaway = true, bool isReturn = false,
            bool isFromReturn = false, bool iSRefund = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetInvoicePayments", connection))
            using (var dataAdapter = new SqlDataAdapter(command))
            {
                // Set command properties
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 6000;

                // Add parameters with strongly-typed values
                command.Parameters.Add(new SqlParameter("@inv_no", SqlDbType.VarChar) { Value = invNo });
                command.Parameters.Add(new SqlParameter("@showlayaway", SqlDbType.Bit) { Value = showLayaway });
                command.Parameters.Add(new SqlParameter("@is_return", SqlDbType.Bit) { Value = isReturn });
                command.Parameters.Add(new SqlParameter("@iSFromReturn", SqlDbType.Bit) { Value = isFromReturn });
                command.Parameters.Add(new SqlParameter("@iSRefund", SqlDbType.Bit) { Value = iSRefund });

                // Fill DataTable
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }

        public DataTable GetInvoiceDiscount(string inv_no, bool iSRepair = false)
        {
            return _helperCommonService.GetSqlData(iSRepair ? @"select Inv_no,trim(Discount) Discount,Amount,cast(1 as bit) iSOld, cast(1 as bit) iSAsk from Invoice_Discounts with (nolock) where inv_no = trim(@inv_no)" : @"select Inv_no,trim(Discount) Discount,Amount,cast(1 as bit) iSOld, cast(1 as bit) iSAsk from Invoice_Discounts with (nolock) where RIGHT('     '+ CONVERT(VARCHAR,Trimmed_inv_no),6) =RIGHT('     '+ CONVERT(VARCHAR,trim(@inv_no)),6)",
                "@inv_no", inv_no);
        }

        public string UpdateRepairOrderInvoice(RepairorderModel repairorder)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = @"UPDATE INVOICE  SET ACC = @ACC,BACC = @BACC,ADD_COST = @ADD_COST,NAME = @NAME,SNH = @SNH,DATE = @DATE,PON = @PON,
                V_CTL_NO = @V_CTL_NO,MESSAGE = @MESSAGE,GR_TOTAL = @GR_TOTAL,ADDR1 = @ADDR1,ADDR2 = @ADDR2,CITY = @CITY,STATE = @STATE,ZIP = @ZIP,COUNTRY = @COUNTRY,
                VIA_UPS = @VIA_UPS,IS_COD = @IS_COD,WEIGHT = @WEIGHT,TERM1 = @TERM1,TERM_PCT1 = @TERM_PCT1,TERM2 = @TERM2,TERM_PCT2 = @TERM_PCT2,TERM3 = @TERM3,TERM_PCT3 = @TERM_PCT3,TERM4 = @TERM4,
                TERM_PCT4 = @TERM_PCT4,INSURED = @INSURED,EARLY = @EARLY,[PERCENT] = @PERCENT,RESIDENT = @RESIDENT,SHIPTYPE=@SHIPTYPE,TAXABLE=@TAXABLE,SALES_TAX=@SALES_TAX,SALES_TAX1=@SALES_TAX, Deduction=@Deduction,
                notax_reasson=@NO_TAXRESION,SALESMAN1=@SALESMAN1,SALESMAN2=@SALESMAN2,COMISH1=@COMISH1,COMISH2=@COMISH2,COMISHAMOUNT1=@COMISHAMOUNT1,COMISHAMOUNT2=@COMISHAMOUNT2,WARRANTY_REPAIR=@WARRANTY_REPAIR,Sales_Fee_Rate=@SALESRATE,Sales_Fee_Amount=@SEALESAMOUNT,
                PAYLATER=iSNULL(@PAYMELATER,0), sales_tax_rate=iSNULL(@SalesTaxRate,0)
                WHERE INV_NO = @INV_NO";//store_no=@storecode,

                dbCommand.Parameters.AddWithValue("@INV_NO", repairorder.INV_NO.PadLeft(6));
                dbCommand.Parameters.AddWithValue("@ACC", repairorder.ACC);
                dbCommand.Parameters.AddWithValue("@BACC", repairorder.BACC);
                dbCommand.Parameters.AddWithValue("@ADD_COST", repairorder.ADD_COST);
                dbCommand.Parameters.AddWithValue("@NAME", repairorder.NAME);
                dbCommand.Parameters.AddWithValue("@SNH", repairorder.SNH);
                dbCommand.Parameters.AddWithValue("@DATE", repairorder.DATE);
                dbCommand.Parameters.AddWithValue("@PON", repairorder.PON);
                dbCommand.Parameters.AddWithValue("@V_CTL_NO", repairorder.V_CTL_NO);
                dbCommand.Parameters.AddWithValue("@MESSAGE", repairorder.MESSAGE);
                dbCommand.Parameters.AddWithValue("@GR_TOTAL", repairorder.GR_TOTAL);
                dbCommand.Parameters.AddWithValue("@ADDR1", repairorder.ADDR1);
                dbCommand.Parameters.AddWithValue("@ADDR2", repairorder.ADDR2);
                dbCommand.Parameters.AddWithValue("@CITY", repairorder.CITY);
                dbCommand.Parameters.AddWithValue("@STATE", repairorder.STATE);
                dbCommand.Parameters.AddWithValue("@ZIP", repairorder.ZIP);
                dbCommand.Parameters.AddWithValue("@COUNTRY", repairorder.COUNTRY);
                dbCommand.Parameters.AddWithValue("@VIA_UPS", repairorder.VIA_UPS);
                dbCommand.Parameters.AddWithValue("@IS_COD", repairorder.IS_COD);
                dbCommand.Parameters.AddWithValue("@WEIGHT", repairorder.WEIGHT);
                dbCommand.Parameters.AddWithValue("@TERM1", repairorder.TERM1);
                dbCommand.Parameters.AddWithValue("@TERM_PCT1", repairorder.TERM_PCT1);
                dbCommand.Parameters.AddWithValue("@TERM2", repairorder.TERM2);
                dbCommand.Parameters.AddWithValue("@TERM_PCT2", repairorder.TERM_PCT2);
                dbCommand.Parameters.AddWithValue("@TERM3", repairorder.TERM3);
                dbCommand.Parameters.AddWithValue("@TERM_PCT3", repairorder.TERM_PCT3);
                dbCommand.Parameters.AddWithValue("@TERM4", repairorder.TERM4);
                dbCommand.Parameters.AddWithValue("@TERM_PCT4", repairorder.TERM_PCT4);
                dbCommand.Parameters.AddWithValue("@INSURED", repairorder.INSURED);
                dbCommand.Parameters.AddWithValue("@EARLY", repairorder.EARLY);
                dbCommand.Parameters.AddWithValue("@PERCENT", repairorder.PERCENT);
                dbCommand.Parameters.AddWithValue("@RESIDENT", repairorder.RESIDENT);
                dbCommand.Parameters.AddWithValue("@SHIPTYPE", repairorder.SHIP_TYPE);
                dbCommand.Parameters.AddWithValue("@TAXABLE", repairorder.TAXABLE);
                dbCommand.Parameters.AddWithValue("@SALES_TAX", repairorder.SALES_TAX);
                dbCommand.Parameters.AddWithValue("@Deduction", repairorder.Deduction);
                dbCommand.Parameters.AddWithValue("@NO_TAXRESION", repairorder.TaxReason);

                dbCommand.Parameters.AddWithValue("@WARRANTY_REPAIR", repairorder.iSFromWarranty);
                dbCommand.Parameters.AddWithValue("@SALESMAN1", repairorder.SALESMAN1);
                dbCommand.Parameters.AddWithValue("@SALESMAN2", repairorder.SALESMAN2);
                dbCommand.Parameters.AddWithValue("@COMISH1", repairorder.COMISH1);
                dbCommand.Parameters.AddWithValue("@COMISH2", repairorder.COMISH2);
                dbCommand.Parameters.AddWithValue("@COMISHAMOUNT1", repairorder.COMISHAMOUNT1);
                dbCommand.Parameters.AddWithValue("@COMISHAMOUNT2", repairorder.COMISHAMOUNT2);
                dbCommand.Parameters.AddWithValue("@SEALESAMOUNT", repairorder.SalesFeeAmount);
                dbCommand.Parameters.AddWithValue("@SALESRATE", repairorder.SalesFeeRate);
                dbCommand.Parameters.AddWithValue("@PAYMELATER", repairorder.paymelater);
                dbCommand.Parameters.AddWithValue("@SalesTaxRate", repairorder.SalesTaxRate);
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return (rowsAffected > 0 ? "1" : "0");
            }
        }

        public string DeleteOrderInvoiceDataIntoInSpItTable(string invoicenumber, string repairno = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("DelFromInSpIt", connection))
            {
                // Set command properties
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@INV_NO", invoicenumber);
                dbCommand.Parameters.AddWithValue("@REPAIRNO", repairno);

                // Add output parameter for result
                var outDelRepStatus = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                dbCommand.Parameters.Add(outDelRepStatus);

                // Set command timeout
                dbCommand.CommandTimeout = 3000;

                // Open connection and execute the command
                connection.Open();
                dbCommand.ExecuteNonQuery();

                // Return output parameter value
                return outDelRepStatus.Value?.ToString() ?? string.Empty;
            }
        }

        public string UpdateOrderInvoiceDataIntoInSpItTable(RepairorderModel repairorder)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = @"INSERT INTO IN_SP_IT (INV_NO,[DESC],PRICE,QTY,LINE,RET_INV_NO, IS_TAX,REPAIR_NO,iSInventoryItem) VALUES (@INV_NO,@DESC,@PRICE,@QTY,@LINE,@RET_INV_NO,@IS_TAX,@REPAIR_NO,iif(iSNULL(@STYLE,'')<>'',1,0))";

                dbCommand.Parameters.AddWithValue("@INV_NO", repairorder.INV_NO.PadLeft(6));
                dbCommand.Parameters.AddWithValue("@DESC", repairorder.DESC);
                dbCommand.Parameters.AddWithValue("@PRICE", repairorder.PRICE);
                dbCommand.Parameters.AddWithValue("@QTY", repairorder.QTY);
                dbCommand.Parameters.AddWithValue("@LINE", repairorder.LINE);
                dbCommand.Parameters.AddWithValue("@RET_INV_NO", repairorder.Rtn_INV_NO);
                dbCommand.Parameters.AddWithValue("@IS_TAX", repairorder.IS_TAX);
                dbCommand.Parameters.AddWithValue("@REPAIR_NO", repairorder.PON);
                dbCommand.Parameters.AddWithValue("@STYLE", repairorder.STYLE);
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return (rowsAffected > 0 ? "1" : "0");
            }
        }

        public string SaveOrderInvoiceDataIntoInSpItTable(RepairorderModel repairorder)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = @"INSERT INTO IN_SP_IT (INV_NO,[DESC],PRICE,QTY,LINE,RET_INV_NO, IS_TAX,REPAIR_NO,iSInventoryItem) VALUES (@INV_NO, CAST(ISNULL(@DESC,'') AS NVARCHAR(400)),ISNULL(@PRICE,0),ISNULL(@QTY,0),ISNULL(@LINE,''),ISNULL(@RET_INV_NO,''), ISNULL(@IS_TAX,0),ISNULL(@REPAIR_NO,''),iif(isnull(@STYLE,'')<>'',1,0))";

                dbCommand.Parameters.AddWithValue("@INV_NO", repairorder.INV_NO.PadLeft(6));
                dbCommand.Parameters.AddWithValue("@DESC", repairorder.DESC);
                dbCommand.Parameters.AddWithValue("@PRICE", repairorder.PRICE);
                dbCommand.Parameters.AddWithValue("@QTY", repairorder.QTY);
                dbCommand.Parameters.AddWithValue("@LINE", repairorder.LINE);
                dbCommand.Parameters.AddWithValue("@RET_INV_NO", repairorder.Rtn_INV_NO);
                dbCommand.Parameters.AddWithValue("@IS_TAX", repairorder.IS_TAX);
                dbCommand.Parameters.AddWithValue("@REPAIR_NO", repairorder.PON);
                dbCommand.Parameters.AddWithValue("@STYLE", Convert.ToString(repairorder.STYLE));
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return (rowsAffected > 0 ? "1" : "0");
            }
        }

        public bool AddEditPaymentForRepair(string invno, string repordno, string acc, string pcname, string grtotal, string Deposit, string UserGCNo, string paymentItems, string StoreCode, string Cash_Register, out string out_inv_no, bool ispayment = false, bool is_update = false, bool is_return = false, string storecodeinuse = "", bool isMultiRepair = false, int Counter = 0, String xmlDiscount = "", String orderno = "", decimal invitems = 0, DateTime? invdate = null)
        {
            using (SqlCommand dbCommand1 = new SqlCommand())
            {
                dbCommand1.Connection = _connectionProvider.GetConnection();
                dbCommand1.CommandType = CommandType.StoredProcedure;
                dbCommand1.CommandText = "UPDATE_INVOICE";
                dbCommand1.Parameters.AddWithValue("@INV_NO", invno);
                dbCommand1.Parameters.AddWithValue("@From_Repair", true);
                dbCommand1.CommandTimeout = 5000;
                dbCommand1.Connection.Open();
                var rowAffected = dbCommand1.ExecuteNonQuery();
                dbCommand1.Connection.Close();
            }
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "AddEditPaymentForRepair";
                dbCommand.CommandTimeout = 5000;
                Object invoicedate;
                invoicedate = invdate == null ? DateTime.Now : invdate;

                dbCommand.Parameters.AddWithValue("@INV_NO", invno);
                dbCommand.Parameters.AddWithValue("@REPAIRNO", repordno);
                dbCommand.Parameters.AddWithValue("@ACC", acc);
                dbCommand.Parameters.AddWithValue("@PCNAME", pcname);
                dbCommand.Parameters.AddWithValue("@DATE", invoicedate);
                dbCommand.Parameters.AddWithValue("@GR_TOTAL", grtotal);
                dbCommand.Parameters.AddWithValue("@DEPOSIT", Deposit);
                dbCommand.Parameters.AddWithValue("@ISPAYMENT", ispayment == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IS_RETURN", is_return == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IS_UPDATE", is_update == true ? 1 : 0);
                dbCommand.Parameters.Add("@OUT_INV_NO", SqlDbType.VarChar, 6).Direction = ParameterDirection.Output;
                dbCommand.Parameters.AddWithValue("@UserGCNO", UserGCNo);
                dbCommand.Parameters.AddWithValue("@StoreCode", StoreCode);
                dbCommand.Parameters.AddWithValue("@CASH_REG_CODE", Cash_Register);
                dbCommand.Parameters.AddWithValue("@storecodeinuse", storecodeinuse);
                dbCommand.Parameters.AddWithValue("@isMultiRepair", isMultiRepair == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@Counter", Counter);
                dbCommand.Parameters.AddWithValue("@Orderno", orderno);
                dbCommand.Parameters.AddWithValue("@CHECK_NO", "");
                dbCommand.Parameters.AddWithValue("@BANK_NAME", "");

                dbCommand.Parameters.Add("@Invitems", SqlDbType.Decimal).Value = invitems;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@TBLPAYMENTITEMS";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = paymentItems;
                dbCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@TBLDISCOUNTITEMS";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = xmlDiscount;
                dbCommand.Parameters.Add(parameter);

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();

                var rowsAffected = dbCommand.ExecuteNonQuery();
                out_inv_no = Convert.ToString(dbCommand.Parameters["@OUT_INV_NO"].Value);

                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public DataTable GetCustomerInformationBasedOnAcc(string Acc)
        {
            return _helperCommonService.GetSqlData(@"SELECT * FROM CUSTOMER WHERE ACC=@ACC", "@ACC", Acc);
        }

        public string SaveRepairOrderInvoice(RepairorderModel repairorder)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = @"INSERT INTO INVOICE(INV_NO,ACC,BACC,ADD_COST,NAME,SNH,DATE,PON,V_CTL_NO,MESSAGE,GR_TOTAL,ADDR1,ADDR2,CITY,STATE,ZIP,COUNTRY,VIA_UPS,IS_COD,WEIGHT,TERM1,TERM_PCT1,TERM2,TERM_PCT2,TERM3,TERM_PCT3,TERM4,TERM_PCT4,INSURED,EARLY,[PERCENT],RESIDENT,IS_DEB,SHIPTYPE,TAXABLE,SALES_TAX,SALES_TAX1,SALESMAN1,SALESMAN2,Deduction,store_no,notax_reasson,PickUpDate,PICKED,CreateDate,WARRANTY_REPAIR,Sales_Fee_Amount,Sales_Fee_Rate,COMISH1,COMISH2,PAYLATER,sales_tax_rate)VALUES (@INV_NO,@ACC,@BACC,@ADD_COST,@NAME,@SNH,@DATE,@PON,@V_CTL_NO,@MESSAGE,@GR_TOTAL,@ADDR1,@ADDR2,@CITY,@STATE,@ZIP,@COUNTRY,@VIA_UPS,@IS_COD,@WEIGHT,@TERM1,@TERM_PCT1,@TERM2,@TERM_PCT2,@TERM3,@TERM_PCT3,@TERM4,@TERM_PCT4,@INSURED,@EARLY,@PERCENT,@RESIDENT,1,@SHIPTYPE,@TAXABLE,@SALES_TAX,@SALES_TAX,@SALESMAN1,@SALESMAN2,@Deduction,@storecode,@NO_TAXRESION,@DATE,1,GETDATE(),@WARRANTY_REPAIR,@SALESAMOUNT,@SALESRATE,@COMISH1,@COMISH2,@PAYLATER,@SalesTaxRate)";

                dbCommand.Parameters.AddWithValue("@INV_NO", repairorder.INV_NO.PadLeft(6));
                dbCommand.Parameters.AddWithValue("@ACC", repairorder.ACC);
                dbCommand.Parameters.AddWithValue("@BACC", repairorder.BACC);
                dbCommand.Parameters.AddWithValue("@ADD_COST", repairorder.ADD_COST);
                dbCommand.Parameters.AddWithValue("@NAME", repairorder.NAME);
                dbCommand.Parameters.AddWithValue("@SNH", repairorder.SNH.ToString());
                dbCommand.Parameters.AddWithValue("@DATE", repairorder.DATE);
                dbCommand.Parameters.AddWithValue("@PON", repairorder.PON);
                dbCommand.Parameters.AddWithValue("@V_CTL_NO", repairorder.V_CTL_NO);
                dbCommand.Parameters.AddWithValue("@MESSAGE", repairorder.MESSAGE);
                dbCommand.Parameters.AddWithValue("@GR_TOTAL", repairorder.GR_TOTAL);
                dbCommand.Parameters.AddWithValue("@ADDR1", repairorder.ADDR1);
                dbCommand.Parameters.AddWithValue("@ADDR2", repairorder.ADDR2);
                dbCommand.Parameters.AddWithValue("@CITY", repairorder.CITY);
                dbCommand.Parameters.AddWithValue("@STATE", repairorder.STATE);
                dbCommand.Parameters.AddWithValue("@ZIP", repairorder.ZIP);
                dbCommand.Parameters.AddWithValue("@COUNTRY", repairorder.COUNTRY);
                dbCommand.Parameters.AddWithValue("@VIA_UPS", repairorder.VIA_UPS);
                dbCommand.Parameters.AddWithValue("@IS_COD", repairorder.IS_COD);
                dbCommand.Parameters.AddWithValue("@WEIGHT", repairorder.WEIGHT);
                dbCommand.Parameters.AddWithValue("@TERM1", repairorder.TERM1);
                dbCommand.Parameters.AddWithValue("@TERM_PCT1", repairorder.TERM_PCT1);
                dbCommand.Parameters.AddWithValue("@TERM2", repairorder.TERM2);
                dbCommand.Parameters.AddWithValue("@TERM_PCT2", repairorder.TERM_PCT2);
                dbCommand.Parameters.AddWithValue("@TERM3", repairorder.TERM3);
                dbCommand.Parameters.AddWithValue("@TERM_PCT3", repairorder.TERM_PCT3);
                dbCommand.Parameters.AddWithValue("@TERM4", repairorder.TERM4);
                dbCommand.Parameters.AddWithValue("@TERM_PCT4", repairorder.TERM_PCT4);
                dbCommand.Parameters.AddWithValue("@INSURED", repairorder.INSURED);
                dbCommand.Parameters.AddWithValue("@EARLY", repairorder.EARLY);
                dbCommand.Parameters.AddWithValue("@PERCENT", repairorder.PERCENT);
                dbCommand.Parameters.AddWithValue("@RESIDENT", repairorder.RESIDENT);
                dbCommand.Parameters.AddWithValue("@SHIPTYPE", repairorder.SHIP_TYPE);
                dbCommand.Parameters.AddWithValue("@TAXABLE", repairorder.TAXABLE);
                dbCommand.Parameters.AddWithValue("@SALES_TAX", repairorder.SALES_TAX);
                dbCommand.Parameters.AddWithValue("@SALESMAN1", repairorder.SALESMAN1);
                dbCommand.Parameters.AddWithValue("@SALESMAN2", repairorder.SALESMAN2);
                dbCommand.Parameters.AddWithValue("@Deduction", repairorder.Deduction);
                dbCommand.Parameters.AddWithValue("@storecode", repairorder.STORE);
                dbCommand.Parameters.AddWithValue("@NO_TAXRESION", repairorder.TaxReason);
                dbCommand.Parameters.AddWithValue("@COMISH1", repairorder.COMISH1);
                dbCommand.Parameters.AddWithValue("@COMISH2", repairorder.COMISH2);
                dbCommand.Parameters.AddWithValue("@COMISHAMOUNT1", repairorder.COMISHAMOUNT1);
                dbCommand.Parameters.AddWithValue("@COMISHAMOUNT2", repairorder.COMISHAMOUNT2);
                dbCommand.Parameters.AddWithValue("@WARRANTY_REPAIR", repairorder.iSFromWarranty);
                dbCommand.Parameters.AddWithValue("@SALESAMOUNT", repairorder.SalesFeeAmount);
                dbCommand.Parameters.AddWithValue("@SALESRATE", repairorder.SalesFeeRate);
                dbCommand.Parameters.AddWithValue("@PAYLATER", repairorder.paymelater);
                dbCommand.Parameters.AddWithValue("@SalesTaxRate", repairorder.SalesTaxRate);

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return (rowsAffected > 0 ? "1" : "0");
            }
        }

        public DataTable Send2Shop(string data1, string frmStore, string toShop, decimal qty, string cDat, string username, bool lSentBack = false)
        {
            var dataTable = new DataTable();

            // Use 'using' statements to ensure the connection and command are disposed correctly
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("REPAIRTOSHOP", connection))
            {
                // Configure command
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters with explicit data types for better performance
                command.Parameters.Add("@str1XmlData", SqlDbType.NVarChar).Value = data1;
                command.Parameters.Add("@cFrmStore", SqlDbType.NVarChar).Value = frmStore;
                command.Parameters.Add("@cToStore", SqlDbType.NVarChar).Value = toShop;
                command.Parameters.Add("@qty", SqlDbType.Decimal).Value = qty;
                command.Parameters.Add("@cDate", SqlDbType.NVarChar).Value = cDat;
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                command.Parameters.Add("@lBack", SqlDbType.Bit).Value = lSentBack;

                // Open connection just before executing the query
                connection.Open();

                // Use a DataAdapter to fill the DataTable
                using (var dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

        public DataTable RepRcvInShop(string data1, string toShop, string username, bool lSentBack = false)
        {
            DataTable dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("RCVREPAIRINSHOP", connection))
            using (var dbAdapter = new SqlDataAdapter(dbCommand))
            {
                // Configure the command
                dbCommand.CommandType = CommandType.StoredProcedure;

                // Add parameters with explicit types
                dbCommand.Parameters.Add("@str1XmlData", SqlDbType.NVarChar).Value = data1;
                dbCommand.Parameters.Add("@cToStore", SqlDbType.NVarChar).Value = toShop;
                dbCommand.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                dbCommand.Parameters.Add("@lBack", SqlDbType.Bit).Value = lSentBack;

                // Open the connection and fill the DataTable
                connection.Open();
                dbAdapter.Fill(dataTable);
            }
            return dataTable;
        }


        public string CheckRprJob(string cJobbag, string frmShop, string toStore)
        {
            DataTable dataTable = _helperCommonService.GetStoreProc("CHKJOBINSHOP", "@JOBBAG", cJobbag,
                "@cShop", frmShop, "@cStore", toStore);

            return _helperCommonService.DataTableOK(dataTable) ? dataTable.Rows[0]["name"].ToString() : string.Empty;
        }
        public DataTable RepairOrderEnvolapes(string ordnumber, string style, string size)
        {
            DataTable dataTable = new DataTable();
            using (var connection = _connectionProvider.GetConnection())
            using (var dataAdapter = new SqlDataAdapter())
            {
                var command = new SqlCommand("PrintJobBag", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@REPAIR_NO", ordnumber);
                command.Parameters.AddWithValue("@STYLE", style);
                command.Parameters.AddWithValue("@JOBBAGNO", string.Empty);  // Empty string is better than "" for clarity
                command.Parameters.AddWithValue("@ismfg", false);
                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        public DataTable GetOrderRepairData(string currentrepno)
        {
            return _helperCommonService.GetStoreProc("GetOrderRepairData", "@REPAIR_NO", currentrepno);
        }
    }
}
