using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class PrinterPortsService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public PrinterPortsService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public bool AdddefaultPrinterPort(string printerPort1, string printerPort2, string printerPort3, string rfidPrinterPort,
        string rfidIP1, string rfidPort1, string rfidIP2, string rfidPort2, string rfidIP3, string rfidPort3,
        string addressLabelPrinter, string jobbagPrinter, string portTemplate1 = "", string portTemplate2 = "",
        string portTemplate3 = "", bool isLandscape = false, string picturePrinter = "", string receiptPrinterPort = "",
        string diamondLabelPrinter = "")
        {
            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var command = new SqlCommand("SavePrntrPortsDefaultValues", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var parameters = new[]
                    {
                new SqlParameter("@PRINTERPORT", printerPort1 ?? (object)DBNull.Value),
                new SqlParameter("@PRINTERPORT2", printerPort2 ?? (object)DBNull.Value),
                new SqlParameter("@PRINTERPORT3", printerPort3 ?? (object)DBNull.Value),
                new SqlParameter("@RFID_PRINTER_PORT", rfidPrinterPort ?? (object)DBNull.Value),
                new SqlParameter("@RFID_IP", rfidIP1 ?? (object)DBNull.Value),
                new SqlParameter("@RFIDPort", rfidPort1 ?? (object)DBNull.Value),
                new SqlParameter("@RFID_IP2", rfidIP2 ?? (object)DBNull.Value),
                new SqlParameter("@RFID_Port2", rfidPort2 ?? (object)DBNull.Value),
                new SqlParameter("@RFID_IP3", rfidIP3 ?? (object)DBNull.Value),
                new SqlParameter("@RFID_Port3", rfidPort3 ?? (object)DBNull.Value),
                new SqlParameter("@ADDRESSLABEL_PRINTER", addressLabelPrinter ?? (object)DBNull.Value),
                new SqlParameter("@JOBBAG_PRINTER", jobbagPrinter ?? (object)DBNull.Value),
                new SqlParameter("@PORT1TEMPLATE", portTemplate1 ?? (object)DBNull.Value),
                new SqlParameter("@PORT2TEMPLATE", portTemplate2 ?? (object)DBNull.Value),
                new SqlParameter("@PORT3TEMPLATE", portTemplate3 ?? (object)DBNull.Value),
                new SqlParameter("@LANDSCAPE", isLandscape ? 1 : 0),
                new SqlParameter("@Printer_picture", picturePrinter ?? (object)DBNull.Value),
                new SqlParameter("@ReceiptPrinterPort", receiptPrinterPort ?? (object)DBNull.Value),
                new SqlParameter("@DIAMONDLABELPRINTER", diamondLabelPrinter ?? (object)DBNull.Value)
            };

                    command.Parameters.AddRange(parameters);

                    var adapter = new SqlDataAdapter(command);
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Check for errors (e.g., if the stored procedure returns an error row)
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Contains("Error"))
                    {
                        throw new Exception(dataTable.Rows[0]["Error"].ToString());
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AdddefaultPrinterPort: " + ex.Message);
                return false;
            }
        }

        public DataRow GetPrinterPortDetails()
        {
            return _helperCommonService.GetSqlRow(@"SELECT PRINTERPORT,PRINTERPORT2, PRINTERPORT3,RFID_PRINTER_PORT,RFID_IP,RFID_PORT,RFID_IP2,RFID_PORT2,RFID_IP3,RFID_PORT3,AddressLabel_printer,Jobbag_printer,picture_printer,receipt_printer, PORTTEMPLATE1,PORTTEMPLATE2,PORTTEMPLATE3,ADDRESS_LANDSCAPE,DIAMONDLABELPRINTER FROM UPS_INS");
        }
    }
}
