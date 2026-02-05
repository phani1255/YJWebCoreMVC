// Created by Dharani 06/26/2025
// Dharani 06/27/2025 Added UpdateTagMultiplier method.
// Dharani 07/01/2025 Added LabelprinterSetup method.
// chakri  07/01/2025 Added  GetPrinterItems and PrintTags methods.
// Dharani 07/03/2025 Added TagprinterSetup, deletePrinter methods.
// Dharani 07/11/2025 Added GetUPSTagFields, UpdateorInsertTagtemplateFields, UpdateUPSTagFields methods
// Dharani 07/22/2025 Added TagPrinterSetupModel, DiamondModel, TagPrinterSetupModel.
// Dharani 07/23/2025 Added GetUPSDiamondTagFields, UpdateDIAMONDLABEL_TEMPLATE, UpdateUPSDiamondTagFields methods.
// Dharani 07/25/2025 Added AutoDescriptionTemplateModel, AutoDescModel, GetUPSInsDescFieldsForAutoDescription methods
// Dharani 07/28/2025 Added UpdateUPSInsForAutoDescription method.
// Chakri  09/03/2025 Changes in PrintTags method.
// Lokesh  09/25/2025 Added BreakPieceModel and BreakItem.
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class TaggingService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public TaggingService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public bool UpdateTagMargins(int dLeftMargin, int dRightMargin, int intTopMargin, int intCINC,
            int intLeftMargin_Rfid, int intRightMargin_Rfid, int intTopMargin_Rfid, int intCINC_Rfid, int dLeftMargin2,
            int dRightMargin2, int intTopMargin2, int intCINC2, int dLeftMargin3, int dRightMargin3, int intTopMargin3,
            int intCINC3, int printerfont, int intTopLeft1 = 0, int intTopLeft2 = 0, int intTopLeft3 = 0,
            int IsNotSideBySide1 = 0, int IsNotSideBySide2 = 0, int IsNotSideBySide3 = 0)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {

                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = @"Update ups_ins set LEFT_MARGIN=@LeftMargin,RIGHT_MARGIN=@RightMargin,TOP_MARGIN=@TopMargin,CINC=@CINC,
                                         LEFT_MARGIN2=@LeftMargin2,RIGHT_MARGIN2=@RightMargin2,TOP_MARGIN2=@TopMargin2,CINC2=@CINC2,
                                         LEFT_MARGIN3=@LeftMargin3,RIGHT_MARGIN3=@RightMargin3,TOP_MARGIN3=@TopMargin3,CINC3=@CINC3,
                                        RfidPrintLeft=@LeftMargin_Rfid,RfidPrintRight=@RightMargin_Rfid,RfidPrintTop=@TopMargin_Rfid,RfidPrintCinc=@CINC_Rfid,printer_font = @printer_font,
                                        Left_top = @LeftTop1, Left_Top2 = @LeftTop2, Left_Top3 = @LeftTop3, not_sidebyside = @IsNotSideBySide1, not_sidebyside2 = @IsNotSideBySide2, not_sidebyside3 = @IsNotSideBySide3";

                dbCommand.Parameters.AddWithValue("@LeftMargin", dLeftMargin);
                dbCommand.Parameters.AddWithValue("@RightMargin", dRightMargin);
                dbCommand.Parameters.AddWithValue("@TopMargin", intTopMargin);
                dbCommand.Parameters.AddWithValue("@CINC", intCINC);
                dbCommand.Parameters.AddWithValue("@LeftMargin2", dLeftMargin2);
                dbCommand.Parameters.AddWithValue("@RightMargin2", dRightMargin2);
                dbCommand.Parameters.AddWithValue("@TopMargin2", intTopMargin2);
                dbCommand.Parameters.AddWithValue("@CINC2", intCINC2);
                dbCommand.Parameters.AddWithValue("@LeftMargin3", dLeftMargin3);
                dbCommand.Parameters.AddWithValue("@RightMargin3", dRightMargin3);
                dbCommand.Parameters.AddWithValue("@TopMargin3", intTopMargin3);
                dbCommand.Parameters.AddWithValue("@CINC3", intCINC3);
                dbCommand.Parameters.AddWithValue("@LeftMargin_Rfid", intLeftMargin_Rfid);
                dbCommand.Parameters.AddWithValue("@RightMargin_Rfid", intRightMargin_Rfid);
                dbCommand.Parameters.AddWithValue("@TopMargin_Rfid", intTopMargin_Rfid);
                dbCommand.Parameters.AddWithValue("@CINC_Rfid", intCINC_Rfid);
                dbCommand.Parameters.AddWithValue("@printer_font", printerfont);

                dbCommand.Parameters.AddWithValue("@LeftTop1", intTopLeft1);
                dbCommand.Parameters.AddWithValue("@LeftTop2", intTopLeft2);
                dbCommand.Parameters.AddWithValue("@LeftTop3", intTopLeft3);

                dbCommand.Parameters.AddWithValue("@IsNotSideBySide1", IsNotSideBySide1);
                dbCommand.Parameters.AddWithValue("@IsNotSideBySide2", IsNotSideBySide2);
                dbCommand.Parameters.AddWithValue("@IsNotSideBySide3", IsNotSideBySide3);
                dbCommand.CommandTimeout = 5000;
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public bool UpdateTagMultiplier(double tagMultiplier)
        {
            using (var Connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("UPDATE ups_ins SET tag_multiplier = @TagMultiplier", Connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@TagMultiplier", SqlDbType.Float) { Value = tagMultiplier });

                Connection.Open();
                var rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public bool LabelprinterSetup(string port, int left, int right, int distance, bool tsc, bool citoh, bool zebra, bool godex)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {

                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = @"DELETE tag_printer where isLabel = 1;insert into TAG_PRINTER(NAME,port,TAG_LEFT,TAG_RIGHT,TAG_TOP,TAG_DISTANCE,TEMPLATENAME,NOT_SIDEBYSIDE,TOP_LEFT,FONTNAME,TSC,CITOH,ZEBRA,GODEX,ISLABEL) values
                (@NAME,@PORT,@LEFT,@RIGHT,@TOP,@DISTANCE,@TEMPLATENAME,@NOTSIDE,@TOPLEFT,@FONTNAME,@TSC,@CITOH,@ZEBRA,@GODEX,@ISLABEL)";

                dbCommand.Parameters.AddWithValue("@NAME", "");
                dbCommand.Parameters.AddWithValue("@PORT", port);
                dbCommand.Parameters.AddWithValue("@LEFT", left);
                dbCommand.Parameters.AddWithValue("@RIGHT", right);
                dbCommand.Parameters.AddWithValue("@TOP", 0);
                dbCommand.Parameters.AddWithValue("@DISTANCE", distance);
                dbCommand.Parameters.AddWithValue("@NOTSIDE", 0);
                dbCommand.Parameters.AddWithValue("@TOPLEFT", 0);
                dbCommand.Parameters.AddWithValue("@FONTNAME", 0);
                dbCommand.Parameters.AddWithValue("@TEMPLATENAME", "");
                dbCommand.Parameters.AddWithValue("@TSC", tsc);
                dbCommand.Parameters.AddWithValue("@CITOH", citoh);
                dbCommand.Parameters.AddWithValue("@ZEBRA", zebra);
                dbCommand.Parameters.AddWithValue("@GODEX", godex);
                dbCommand.Parameters.AddWithValue("@ISLABEL", 1);

                dbCommand.CommandTimeout = 5000;
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
        public List<string> GetPrinterItems()
        {
            List<string> printers = new List<string>();
            DataTable dtPrinters = _helperCommonService.GetSqlData("SELECT NAME FROM TAG_PRINTER with (nolock) WHERE ISLABEL <> 1 ORDER BY NAME");

            if (_helperCommonService.DataTableOK(dtPrinters))
            {
                foreach (DataRow row in dtPrinters.Rows)
                    printers.Add(row["NAME"].ToString());
            }
            return printers;
        }
        public bool TagprinterSetup(string printerName, string oldPrinterName, string port, int left, int right, int top, int distance, int topLeft, int font, string templateName, bool isNotSideByside, bool tsc, bool citoh, bool zebra, bool godex, bool isEdit, int TagEvenOffset_Y = 0, int TagEvenOffset_X = 0, int taglegthgodexs = 0)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 5000;

                command.CommandText = isEdit
                                        ? @"UPDATE TAG_PRINTER 
                                            SET PORT = @PORT, NAME = @NAME, TAG_LEFT = @LEFT, TAG_RIGHT = @RIGHT, TAG_TOP = @TOP, TAG_DISTANCE = @DISTANCE, 
                                                TEMPLATENAME = @TEMPLATENAME, NOT_SIDEBYSIDE = @NOTSIDE, TOP_LEFT = @TOPLEFT, FONTNAME = @FONTNAME,
                                                TSC = @TSC, CITOH = @CITOH, ZEBRA = @ZEBRA, GODEX = @GODEX, TagEvenOffset_Y = @TagEvenOffset_Y, 
                                                TagEvenOffset_X = @TagEvenOffset_X, tag_length = @taglegthgodexs
                                            WHERE NAME = @oldPrinterName"
                                        : @"INSERT INTO TAG_PRINTER 
                                            (NAME, PORT, TAG_LEFT, TAG_RIGHT, TAG_TOP, TAG_DISTANCE, TEMPLATENAME, NOT_SIDEBYSIDE, TOP_LEFT, FONTNAME, 
                                             TSC, CITOH, ZEBRA, GODEX, TagEvenOffset_Y, TagEvenOffset_X, tag_length)
                                           VALUES 
                                            (@NAME, @PORT, @LEFT, @RIGHT, @TOP, @DISTANCE, @TEMPLATENAME, @NOTSIDE, @TOPLEFT, @FONTNAME, 
                                             @TSC, @CITOH, @ZEBRA, @GODEX, @TagEvenOffset_Y, @TagEvenOffset_X, @taglegthgodexs)";


                // Add parameters
                command.Parameters.AddWithValue("@NAME", printerName);
                command.Parameters.AddWithValue("@PORT", port);
                command.Parameters.AddWithValue("@LEFT", left);
                command.Parameters.AddWithValue("@RIGHT", right);
                command.Parameters.AddWithValue("@TOP", top);
                command.Parameters.AddWithValue("@DISTANCE", distance);
                command.Parameters.AddWithValue("@NOTSIDE", isNotSideByside);
                command.Parameters.AddWithValue("@TOPLEFT", topLeft);
                command.Parameters.AddWithValue("@FONTNAME", font);
                command.Parameters.AddWithValue("@TEMPLATENAME", templateName ?? "");
                command.Parameters.AddWithValue("@TSC", tsc);
                command.Parameters.AddWithValue("@CITOH", citoh);
                command.Parameters.AddWithValue("@ZEBRA", zebra);
                command.Parameters.AddWithValue("@GODEX", godex);
                command.Parameters.AddWithValue("@oldPrinterName", oldPrinterName);
                command.Parameters.AddWithValue("@TagEvenOffset_Y", TagEvenOffset_Y);
                command.Parameters.AddWithValue("@TagEvenOffset_X", TagEvenOffset_X);
                command.Parameters.AddWithValue("@taglegthgodexs", taglegthgodexs);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public void deletePrinter(string name)
        {
            _helperCommonService.GetSqlData(@"DELETE TAG_PRINTER where name = @NAME", "@name", name);
        }

        public DataTable GetUPSTagFields()
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                // Assign the SQL to the command object
                //SqlDataAdapter.SelectCommand.CommandText = @"select isnull(tag_left1,'') tag_left1, isnull(tag_left2,'') tag_left2, isnull(tag_left3,'')tag_left3 , isnull(tag_left4,'') tag_left4, isnull(tag_left5,'') tag_left5,isnull(tag_left1A,'') tag_left1A, isnull(tag_left2A,'') tag_left2A, isnull(tag_left3A,'')tag_left3A , isnull(tag_left4A,'') tag_left4A, isnull(tag_left5A,'') tag_left5A, isnull(tag_left5,'') tag_left5,isnull(tag_left1B,'') tag_left1B, isnull(tag_left2B,'') tag_left2B, isnull(tag_left3B,'')tag_left3B , isnull(tag_left4B,'') tag_left4B, isnull(tag_left5B,'') tag_left5B,isnull(TAG_PLACE,'')tag_place,isnull(tag_text,'')tag_text,* from ups_ins";
                SqlDataAdapter.SelectCommand.CommandText = @" select 
                isnull(tag_left1,'')   tag_left1,   isnull(tag_left2,'')   tag_left2,  isnull(tag_left3,'')   tag_left3,
	            isnull(tag_left4,'')   tag_left4,   isnull(tag_left5,'')   tag_left5,  isnull(tag_left6,'')   tag_left6,    isnull(tag_left7,'')   tag_left7,
	            isnull(tag_left1A,'')  tag_left1A,  isnull(tag_left2A,'')  tag_left2A, isnull(tag_left3A,'')  tag_left3A, 
	            isnull(tag_left4A,'')  tag_left4A,  isnull(tag_left5A,'')  tag_left5A, isnull(tag_left6A,'')  tag_left6A,   isnull(tag_left7A,'')  tag_left7A, 
	            isnull(tag_left1B,'')  tag_left1B,  isnull(tag_left2B,'')  tag_left2B, isnull(tag_left3B,'')  tag_left3B,   isnull(tag_left4B,'')  tag_left4B, 
	            isnull(tag_left5B,'')  tag_left5B,  isnull(tag_left6B,'')  tag_left6B, isnull(tag_left7B,'')  tag_left7B,  
	            isnull(TAG_RIGHT4,'')  TAG_RIGHT4,  isnull(TAG_RIGHT5,'')  TAG_RIGHT5, isnull(TAG_RIGHT6,'')  TAG_RIGHT6,   isnull(TAG_RIGHT7,'')  TAG_RIGHT7,
	            isnull(TAG_RIGHT1A,'') TAG_RIGHT1A, isnull(TAG_RIGHT3A,'') TAG_RIGHT3A, isnull(TAG_RIGHT4A,'') TAG_RIGHT4A, isnull(TAG_RIGHT5A,'') TAG_RIGHT5A, isnull(TAG_RIGHT6A,'') TAG_RIGHT6A, isnull(TAG_RIGHT7A,'') TAG_RIGHT7A,
	            isnull(TAG_RIGHT1B,'') TAG_RIGHT1B, isnull(TAG_RIGHT3B,'') TAG_RIGHT3B,isnull(TAG_RIGHT4B,'')  TAG_RIGHT4B, isnull(TAG_RIGHT5B,'') TAG_RIGHT5B, isnull(TAG_RIGHT6B,'') TAG_RIGHT6B, isnull(TAG_RIGHT7B,'') TAG_RIGHT7B,
	            isnull(TAG_PLACE,'') tag_place, isnull(tag_text,'') tag_text,isnull(TAG_RIGHT1,'') AS TAG_RIGHT1,ISNULL(TAG_RIGHT2,'')AS TAG_RIGHT2,ISNULL(TAG_RIGHT3,'') AS TAG_RIGHT3
                ,ISNULL(TAG_RIGHT2A,'') AS TAG_RIGHT2A,ISNULL(TAG_RIGHT2B,'') AS TAG_RIGHT2B,ISNULL(MOVE_BARCODE,0) AS MOVE_BARCODE,ISNULL(no_tagprice,0) as no_tagprice,isnull(IgnoreDollerforprice,0) as IgnoreDollerforprice,isnull(ignoredecimals,0) as ignoredecimals
                ,isnull(TAG_PLACE2,'') tag_place2, isnull(tag_text2,'') tag_text2,isnull(TAG_PLACE3,'') tag_place3, isnull(tag_text3,'') tag_text3
                ,isnull(TAG_PLACE4,'') tag_place4, isnull(tag_text4,'') tag_text4,
                isnull(tag_left1C,'')  tag_left1C,  isnull(tag_left1D,'') tag_left1D, ISNULL(tag_left1E,'') AS tag_left1E,
                isnull(tag_left2C,'')  tag_left2C,  isnull(tag_left2D,'') tag_left2D, ISNULL(tag_left2E,'') AS tag_left2E,
                isnull(tag_left3C,'')  tag_left3C,  isnull(tag_left3D,'') tag_left3D, ISNULL(tag_left3E,'') AS tag_left3E,
                isnull(tag_left4C,'')  tag_left4C,  isnull(tag_left4D,'') tag_left4D, ISNULL(tag_left4E,'') AS tag_left4E,
                isnull(tag_left5C,'')  tag_left5C,  isnull(tag_left5D,'') tag_left5D, ISNULL(tag_left5E,'') AS tag_left5E,
                isnull(tag_left6C,'')  tag_left6C,  isnull(tag_left6D,'') tag_left6D, ISNULL(tag_left6E,'') AS tag_left6E,
                isnull(tag_left7C,'')  tag_left7C,  isnull(tag_left7D,'') tag_left7D, ISNULL(tag_left7E,'') AS tag_left7E,
                 isnull(TAG_RIGHT1C,'') TAG_RIGHT1C, isnull(TAG_RIGHT1D,'') TAG_RIGHT1D, isnull(TAG_RIGHT1E,'') TAG_RIGHT1E,
                isnull(TAG_RIGHT2C,'') TAG_RIGHT2C, isnull(TAG_RIGHT2D,'') TAG_RIGHT2D, isnull(TAG_RIGHT2E,'') TAG_RIGHT2E,
                isnull(TAG_RIGHT3C,'') TAG_RIGHT3C, isnull(TAG_RIGHT3D,'') TAG_RIGHT3D, isnull(TAG_RIGHT3E,'') TAG_RIGHT3E,
                isnull(TAG_RIGHT4C,'') TAG_RIGHT4C, isnull(TAG_RIGHT4D,'') TAG_RIGHT4D, isnull(TAG_RIGHT4E,'') TAG_RIGHT4E,
                isnull(TAG_RIGHT5C,'') TAG_RIGHT5C, isnull(TAG_RIGHT5D,'') TAG_RIGHT5D, isnull(TAG_RIGHT5E,'') TAG_RIGHT5E,
                isnull(TAG_RIGHT6C,'') TAG_RIGHT6C, isnull(TAG_RIGHT6D,'') TAG_RIGHT6D, isnull(TAG_RIGHT6E,'') TAG_RIGHT6E,
                isnull(TAG_RIGHT7C,'') TAG_RIGHT7C, isnull(TAG_RIGHT7D,'') TAG_RIGHT7D, isnull(TAG_RIGHT7E,'') TAG_RIGHT7E
                from ups_ins, ups_ins1";
                // Fill the table from adapter //    ignoredecimals,IgnoreDollerforprice
                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public bool UpdateorInsertTagtemplateFields(TagModel tagmodel, bool isUpdate, string TemplateName, string Ctag_place, string Ctag_text, string printPort, bool Noprice = false, bool DntShowDoller = false, bool DntShowDecimalvalue = false)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                if (isUpdate)
                    dbCommand.CommandText = @"Update tag_template set 
                                            TAG_LEFT1   = @tag_left1,   TAG_LEFT2   = @tag_left2,   TAG_LEFT3   = @tag_left3,   TAG_LEFT4   = @tag_left4,   TAG_LEFT5   = @tag_left5,   TAG_LEFT6   = @tag_left6,  TAG_LEFT7  = @tag_left7,
                                            TAG_LEFT1A  = @tag_left1A,  TAG_LEFT2A  = @tag_left2A,  TAG_LEFT3A  = @tag_left3A,  TAG_LEFT4A  = @tag_left4A,  TAG_LEFT5A  = @tag_left5A,  TAG_LEFT6A  = @tag_left6A, TAG_LEFT7A = @tag_left7A,
                                            TAG_LEFT1B  = @tag_left1B,  TAG_LEFT2B  = @tag_left2B,  TAG_LEFT3B  = @tag_left3B,  TAG_LEFT4B  = @tag_left4B,  TAG_LEFT5B  = @tag_left5B,  TAG_LEFT6B  = @tag_left6B, TAG_LEFT7B = @tag_left7B,
                                            TAG_RIGHT4  = @tag_right4,  TAG_RIGHT5  = @tag_right5,  TAG_RIGHT6  = @tag_right6,  TAG_RIGHT7  = @tag_right7,
                                            TAG_RIGHT1A = @tag_right1A, TAG_RIGHT3A = @tag_right3A, TAG_RIGHT4A = @tag_right4A, TAG_RIGHT5A = @tag_right5A, TAG_RIGHT6A = @tag_right6A, TAG_RIGHT7A = @tag_right7A,                                           
                                            TAG_RIGHT1B = @tag_right1B, TAG_RIGHT3B = @tag_right3B, TAG_RIGHT4B = @tag_right4B, TAG_RIGHT5B = @tag_right5B, TAG_RIGHT6B = @tag_right6B, TAG_RIGHT7B = @tag_right7B,
                                            NO_PRICE    = @no_price,    TAG_PLACE   = @tag_place,   TAG_TEXT    = @tag_text, TAG_PLACE2   = @tag_place2,   TAG_TEXT2    = @tag_text2, 
                                            TAG_PLACE3   = @tag_place3,   TAG_TEXT3    = @tag_text3, TAG_PLACE4   = @tag_place4,   TAG_TEXT4    = @tag_text4, 
                                            PRINTERPORT = @printerPort ,MOVE_BARCODE=@MOVE_BARCODE,
                                            TAG_RIGHT1=@TAG_RIGHT1,TAG_RIGHT2=@TAG_RIGHT2 ,TAG_RIGHT3=@TAG_RIGHT3 ,TAG_RIGHT2A=@TAG_RIGHT2A,TAG_RIGHT2B=@TAG_RIGHT2B,
                                            IgnoreDollerforprice =@IgnoreDollerforprice, ignoredecimals=@ignoredecimals
                                            ,tag_left1C=@tag_left1C,tag_left1D=@tag_left1D,tag_left1E=@tag_left1E,
                                            tag_left2C=@tag_left2C,tag_left2D=@tag_left2D,tag_left2E=@tag_left2E,tag_left3C=@tag_left3C,tag_left3D=@tag_left3D,tag_left3E=@tag_left3E,
                                            tag_left4C=@tag_left4C,tag_left4D=@tag_left4D,tag_left4E=@tag_left4E,tag_left5C=@tag_left5C,tag_left5D=@tag_left5D,tag_left5E=@tag_left5E, 
                                            tag_left6C=@tag_left6C,tag_left6D=@tag_left6D,tag_left6E=@tag_left6E, tag_left7C=@tag_left7C,tag_left7D=@tag_left7D,tag_left7E=@tag_left7E
                                            , TAG_RIGHT1C = @tag_right1C, TAG_RIGHT1D = @tag_right1D, TAG_RIGHT1E = @tag_right1E,TAG_RIGHT2C = @tag_right2C, TAG_RIGHT2D = @tag_right2D, TAG_RIGHT2E = @tag_right2E
                                            , TAG_RIGHT3C = @tag_right3C, TAG_RIGHT3D = @tag_right3D, TAG_RIGHT3E = @tag_right3E,TAG_RIGHT4C = @tag_right4C, TAG_RIGHT4D = @tag_right4D, TAG_RIGHT4E = @tag_right4E                                           
                                             , TAG_RIGHT5C = @tag_right5C, TAG_RIGHT5D = @tag_right5D, TAG_RIGHT5E = @tag_right5E,TAG_RIGHT6C = @tag_right6C, TAG_RIGHT6D = @tag_right6D, TAG_RIGHT6E = @tag_right6E
                                           , TAG_RIGHT7C = @tag_right7C, TAG_RIGHT7D = @tag_right7D, TAG_RIGHT7E = @tag_right7E
                                            Where TEMPLATENAME = @TemplateName";
                else
                    //dbCommand.CommandText = @"insert into tag_template (TEMPLATENAME,tag_left1,tag_left2,tag_left3,tag_left4,tag_left5,tag_left6,tag_left7,tag_right4,tag_right5,tag_right6,tag_right7,tag_left1A,tag_left2A,tag_left3A,tag_left4A,tag_left5A,tag_left6A,tag_left7A,tag_right4A,tag_right5A,tag_right6A,tag_right7A,TAG_LEFT1B,TAG_LEFT2B,TAG_LEFT3B,TAG_LEFT4B,TAG_LEFT5B,TAG_LEFT6B,TAG_LEFT7B,tag_right4B,tag_right5B,tag_right6B,tag_right7B,tag_place,tag_text,PRINTERPORT,no_price)VALUES(@TemplateName,@tag_left1,@tag_left2,@tag_left3,@tag_left4,@tag_left5,@tag_left6,@tag_left7,@tag_right4,@tag_right5,@tag_right6,@tag_right7,@tag_left1A,@tag_left2A,@tag_left3A,@tag_left4A,@tag_left5A,@tag_left6A,@tag_left7A,@tag_right4A,@tag_right5A,@tag_right6A,@tag_right7A,@tag_left1B,@tag_left2B,@tag_left3B,@tag_left4B,@tag_left5B,@tag_right4B,@tag_right5B,@tag_right6B,@tag_right7B,@tag_place,@tag_text,@printerPort,@no_price)";
                    dbCommand.CommandText = @"insert into tag_template (TEMPLATENAME, TAG_LEFT1, TAG_LEFT2, TAG_LEFT3, TAG_LEFT4, TAG_LEFT5, TAG_LEFT6, TAG_LEFT7, TAG_LEFT1A, TAG_LEFT2A, TAG_LEFT3A, TAG_LEFT4A, TAG_LEFT5A, TAG_LEFT6A, TAG_LEFT7A, TAG_LEFT1B, TAG_LEFT2B, TAG_LEFT3B, TAG_LEFT4B, TAG_LEFT5B, TAG_LEFT6B, TAG_LEFT7B,   TAG_RIGHT4, TAG_RIGHT5, TAG_RIGHT6, TAG_RIGHT7, TAG_RIGHT1A, TAG_RIGHT3A, TAG_RIGHT4A, TAG_RIGHT5A, TAG_RIGHT6A, TAG_RIGHT7A, TAG_RIGHT1B, TAG_RIGHT3B, TAG_RIGHT4B, TAG_RIGHT5B, TAG_RIGHT6B, TAG_RIGHT7B, TAG_PLACE, TAG_TEXT, PRINTERPORT, NO_PRICE,MOVE_BARCODE,TAG_RIGHT1,TAG_RIGHT2,TAG_RIGHT3,TAG_RIGHT2A,TAG_RIGHT2B,tag_place2,tag_place3,tag_place4,tag_text2,tag_text3,tag_text4,tag_left1C,tag_left1D,tag_left1E,tag_left2C,tag_left2D,tag_left2E,tag_left3C,tag_left3D,tag_left3E,tag_left4C,tag_left4D,tag_left4E,tag_left5C,tag_left5D,tag_left5E,tag_left6C,tag_left6D,tag_left6E,tag_left7C,tag_left7D,tag_left7E,TAG_RIGHT1C,TAG_RIGHT1D,TAG_RIGHT1E,TAG_RIGHT2C,TAG_RIGHT2D,TAG_RIGHT2E,TAG_RIGHT3C,TAG_RIGHT3D,TAG_RIGHT3E,TAG_RIGHT4C,TAG_RIGHT4D,TAG_RIGHT4E,TAG_RIGHT5C,TAG_RIGHT5D,TAG_RIGHT5E,TAG_RIGHT6C,TAG_RIGHT6D,TAG_RIGHT6E,TAG_RIGHT7C,TAG_RIGHT7D,TAG_RIGHT7E)
                                                                  VALUES(@TemplateName,@tag_left1,@tag_left2,@tag_left3,@tag_left4,@tag_left5,@tag_left6,@tag_left7,@tag_left1A,@tag_left2A,@tag_left3A,@tag_left4A,@tag_left5A,@tag_left6A,@tag_left7A,@tag_left1B,@tag_left2B,@tag_left3B,@tag_left4B,@tag_left5B,@tag_right6B,@tag_right7B,@tag_right4,@tag_right5,@tag_right6,@tag_right7,@tag_right1A,@tag_right3A,@tag_right4A,@tag_right5A,@tag_right6A,@tag_right7A,@tag_right1B,@tag_right3B,@tag_right4B,@tag_right5B,@tag_right6B,@tag_right7B,@tag_place,@tag_text,@printerPort,@no_price,@MOVE_BARCODE,@TAG_RIGHT1,@TAG_RIGHT2,@TAG_RIGHT3,@TAG_RIGHT2A,@TAG_RIGHT2B,@tag_place2,@tag_place3,@tag_place4,@tag_text2,@tag_text3,@tag_text4,@tag_left1C,@tag_left1D,@tag_left1E,@tag_left2C,@tag_left2D,@tag_left2E,@tag_left3C,@tag_left3D,@tag_left3E,@tag_left4C,@tag_left4D,@tag_left4E,@tag_left5C,@tag_left5D,@tag_left5E,@tag_left6C,@tag_left6D,@tag_left6E,@tag_left7C,@tag_left7D,@tag_left7E,@TAG_RIGHT1C,@TAG_RIGHT1D,@TAG_RIGHT1E,@TAG_RIGHT2C,@TAG_RIGHT2D,@TAG_RIGHT2E,@TAG_RIGHT3C,@TAG_RIGHT3D,@TAG_RIGHT3E,@TAG_RIGHT4C,@TAG_RIGHT4D,@TAG_RIGHT4E,@TAG_RIGHT5C,@TAG_RIGHT5D,@TAG_RIGHT5E,@TAG_RIGHT6C,@TAG_RIGHT6D,@TAG_RIGHT6E,@TAG_RIGHT7C,@TAG_RIGHT7D,@TAG_RIGHT7E)"; //tag_place2,tag_text2,tag_place3,tag_text3,tag_place4,tag_text4

                dbCommand.Parameters.AddWithValue("@tag_left1", string.IsNullOrEmpty(tagmodel.tag_left1) ? "" : tagmodel.tag_left1);
                dbCommand.Parameters.AddWithValue("@tag_left2", string.IsNullOrEmpty(tagmodel.tag_left2) ? "" : tagmodel.tag_left2);
                dbCommand.Parameters.AddWithValue("@tag_left3", string.IsNullOrEmpty(tagmodel.tag_left3) ? "" : tagmodel.tag_left3);
                dbCommand.Parameters.AddWithValue("@tag_left4", string.IsNullOrEmpty(tagmodel.tag_left4) ? "" : tagmodel.tag_left4);
                dbCommand.Parameters.AddWithValue("@tag_left5", string.IsNullOrEmpty(tagmodel.tag_left5) ? "" : tagmodel.tag_left5);
                dbCommand.Parameters.AddWithValue("@tag_left6", string.IsNullOrEmpty(tagmodel.tag_left6) ? "" : tagmodel.tag_left6);
                dbCommand.Parameters.AddWithValue("@tag_left7", string.IsNullOrEmpty(tagmodel.tag_left7) ? "" : tagmodel.tag_left7);

                dbCommand.Parameters.AddWithValue("@tag_right4", string.IsNullOrEmpty(tagmodel.tag_right4) ? "" : tagmodel.tag_right4);
                dbCommand.Parameters.AddWithValue("@tag_right5", string.IsNullOrEmpty(tagmodel.tag_right5) ? "" : tagmodel.tag_right5);
                dbCommand.Parameters.AddWithValue("@tag_right6", string.IsNullOrEmpty(tagmodel.tag_right6) ? "" : tagmodel.tag_right6);
                dbCommand.Parameters.AddWithValue("@tag_right7", string.IsNullOrEmpty(tagmodel.tag_right7) ? "" : tagmodel.tag_right7);

                dbCommand.Parameters.AddWithValue("@tag_left1A", string.IsNullOrEmpty(tagmodel.tag_left1A) ? "" : tagmodel.tag_left1A);
                dbCommand.Parameters.AddWithValue("@tag_left2A", string.IsNullOrEmpty(tagmodel.tag_left2A) ? "" : tagmodel.tag_left2A);
                dbCommand.Parameters.AddWithValue("@tag_left3A", string.IsNullOrEmpty(tagmodel.tag_left3A) ? "" : tagmodel.tag_left3A);
                dbCommand.Parameters.AddWithValue("@tag_left4A", string.IsNullOrEmpty(tagmodel.tag_left4A) ? "" : tagmodel.tag_left4A);
                dbCommand.Parameters.AddWithValue("@tag_left5A", string.IsNullOrEmpty(tagmodel.tag_left5A) ? "" : tagmodel.tag_left5A);
                dbCommand.Parameters.AddWithValue("@tag_left6A", string.IsNullOrEmpty(tagmodel.tag_left6A) ? "" : tagmodel.tag_left6A);
                dbCommand.Parameters.AddWithValue("@tag_left7A", string.IsNullOrEmpty(tagmodel.tag_left7A) ? "" : tagmodel.tag_left7A);

                dbCommand.Parameters.AddWithValue("@tag_right1A", string.IsNullOrEmpty(tagmodel.tag_right1A) ? "" : tagmodel.tag_right1A);
                dbCommand.Parameters.AddWithValue("@tag_right3A", string.IsNullOrEmpty(tagmodel.tag_right3A) ? "" : tagmodel.tag_right3A);
                dbCommand.Parameters.AddWithValue("@tag_right4A", string.IsNullOrEmpty(tagmodel.tag_right4A) ? "" : tagmodel.tag_right4A);
                dbCommand.Parameters.AddWithValue("@tag_right5A", string.IsNullOrEmpty(tagmodel.tag_right5A) ? "" : tagmodel.tag_right5A);
                dbCommand.Parameters.AddWithValue("@tag_right6A", string.IsNullOrEmpty(tagmodel.tag_right6A) ? "" : tagmodel.tag_right6A);
                dbCommand.Parameters.AddWithValue("@tag_right7A", string.IsNullOrEmpty(tagmodel.tag_right7A) ? "" : tagmodel.tag_right7A);

                dbCommand.Parameters.AddWithValue("@tag_left1B", string.IsNullOrEmpty(tagmodel.tag_left1B) ? "" : tagmodel.tag_left1B);
                dbCommand.Parameters.AddWithValue("@tag_left2B", string.IsNullOrEmpty(tagmodel.tag_left2B) ? "" : tagmodel.tag_left2B);
                dbCommand.Parameters.AddWithValue("@tag_left3B", string.IsNullOrEmpty(tagmodel.tag_left3B) ? "" : tagmodel.tag_left3B);
                dbCommand.Parameters.AddWithValue("@tag_left4B", string.IsNullOrEmpty(tagmodel.tag_left4B) ? "" : tagmodel.tag_left4B);
                dbCommand.Parameters.AddWithValue("@tag_left5B", string.IsNullOrEmpty(tagmodel.tag_left5B) ? "" : tagmodel.tag_left5B);
                dbCommand.Parameters.AddWithValue("@tag_left6B", string.IsNullOrEmpty(tagmodel.tag_left6B) ? "" : tagmodel.tag_left6B);
                dbCommand.Parameters.AddWithValue("@tag_left7B", string.IsNullOrEmpty(tagmodel.tag_left7B) ? "" : tagmodel.tag_left7B);

                dbCommand.Parameters.AddWithValue("@tag_right1B", string.IsNullOrEmpty(tagmodel.tag_right1B) ? "" : tagmodel.tag_right1B);
                dbCommand.Parameters.AddWithValue("@tag_right3B", string.IsNullOrEmpty(tagmodel.tag_right3B) ? "" : tagmodel.tag_right3B);
                dbCommand.Parameters.AddWithValue("@tag_right4B", string.IsNullOrEmpty(tagmodel.tag_right4B) ? "" : tagmodel.tag_right4B);
                dbCommand.Parameters.AddWithValue("@tag_right5B", string.IsNullOrEmpty(tagmodel.tag_right5B) ? "" : tagmodel.tag_right5B);
                dbCommand.Parameters.AddWithValue("@tag_right6B", string.IsNullOrEmpty(tagmodel.tag_right6B) ? "" : tagmodel.tag_right6B);
                dbCommand.Parameters.AddWithValue("@tag_right7B", string.IsNullOrEmpty(tagmodel.tag_right7B) ? "" : tagmodel.tag_right7B);
                dbCommand.Parameters.AddWithValue("@TAG_RIGHT1", string.IsNullOrEmpty(tagmodel.tag_right1) ? "" : tagmodel.tag_right1);
                dbCommand.Parameters.AddWithValue("@TAG_RIGHT2", string.IsNullOrEmpty(tagmodel.tag_right2) ? "" : tagmodel.tag_right2);
                dbCommand.Parameters.AddWithValue("@TAG_RIGHT3", string.IsNullOrEmpty(tagmodel.tag_right3) ? "" : tagmodel.tag_right3);
                dbCommand.Parameters.AddWithValue("@TAG_RIGHT2A", string.IsNullOrEmpty(tagmodel.tag_right2A) ? "" : tagmodel.tag_right2A);
                dbCommand.Parameters.AddWithValue("@TAG_RIGHT2B", string.IsNullOrEmpty(tagmodel.tag_right2B) ? "" : tagmodel.tag_right2B);

                dbCommand.Parameters.AddWithValue("@MOVE_BARCODE", tagmodel.Move_barcode);
                dbCommand.Parameters.AddWithValue("@TemplateName", string.IsNullOrEmpty(TemplateName) ? "" : TemplateName);
                dbCommand.Parameters.AddWithValue("@tag_place", string.IsNullOrEmpty(Ctag_place) ? "" : Ctag_place);
                dbCommand.Parameters.AddWithValue("@tag_text", string.IsNullOrEmpty(Ctag_text) ? "" : Ctag_text);
                dbCommand.Parameters.AddWithValue("@printerPort", string.IsNullOrEmpty(printPort) ? "" : printPort);
                dbCommand.Parameters.AddWithValue("@no_price", Noprice);
                dbCommand.Parameters.AddWithValue("@IgnoreDollerforprice", DntShowDoller);
                dbCommand.Parameters.AddWithValue("@ignoredecimals", DntShowDecimalvalue);

                dbCommand.Parameters.AddWithValue("@tag_place2", string.IsNullOrEmpty(tagmodel.CTag_Place2) ? "" : tagmodel.CTag_Place2);
                dbCommand.Parameters.AddWithValue("@tag_text2", string.IsNullOrEmpty(tagmodel.CTag_Text2) ? "" : tagmodel.CTag_Text2);
                dbCommand.Parameters.AddWithValue("@tag_place3", string.IsNullOrEmpty(tagmodel.CTag_Place3) ? "" : tagmodel.CTag_Place3);
                dbCommand.Parameters.AddWithValue("@tag_text3", string.IsNullOrEmpty(tagmodel.CTag_Text3) ? "" : tagmodel.CTag_Text3);
                dbCommand.Parameters.AddWithValue("@tag_place4", string.IsNullOrEmpty(tagmodel.CTag_Place4) ? "" : tagmodel.CTag_Place4);
                dbCommand.Parameters.AddWithValue("@tag_text4", string.IsNullOrEmpty(tagmodel.CTag_Text4) ? "" : tagmodel.CTag_Text4);
                //tag_left1C
                dbCommand.Parameters.AddWithValue("@tag_left1C", string.IsNullOrEmpty(tagmodel.tag_left1C) ? "" : tagmodel.tag_left1C);
                dbCommand.Parameters.AddWithValue("@tag_left2C", string.IsNullOrEmpty(tagmodel.tag_left2C) ? "" : tagmodel.tag_left2C);
                dbCommand.Parameters.AddWithValue("@tag_left3C", string.IsNullOrEmpty(tagmodel.tag_left3C) ? "" : tagmodel.tag_left3C);
                dbCommand.Parameters.AddWithValue("@tag_left4C", string.IsNullOrEmpty(tagmodel.tag_left4C) ? "" : tagmodel.tag_left4C);
                dbCommand.Parameters.AddWithValue("@tag_left5C", string.IsNullOrEmpty(tagmodel.tag_left5C) ? "" : tagmodel.tag_left5C);
                dbCommand.Parameters.AddWithValue("@tag_left6C", string.IsNullOrEmpty(tagmodel.tag_left6C) ? "" : tagmodel.tag_left6C);
                dbCommand.Parameters.AddWithValue("@tag_left7C", string.IsNullOrEmpty(tagmodel.tag_left7C) ? "" : tagmodel.tag_left7C);

                dbCommand.Parameters.AddWithValue("@tag_right1C", string.IsNullOrEmpty(tagmodel.tag_right1C) ? "" : tagmodel.tag_right1C);
                dbCommand.Parameters.AddWithValue("@tag_right2C", string.IsNullOrEmpty(tagmodel.tag_right2C) ? "" : tagmodel.tag_right2C);
                dbCommand.Parameters.AddWithValue("@tag_right3C", string.IsNullOrEmpty(tagmodel.tag_right3C) ? "" : tagmodel.tag_right3C);
                dbCommand.Parameters.AddWithValue("@tag_right4C", string.IsNullOrEmpty(tagmodel.tag_right4C) ? "" : tagmodel.tag_right4C);
                dbCommand.Parameters.AddWithValue("@tag_right5C", string.IsNullOrEmpty(tagmodel.tag_right5C) ? "" : tagmodel.tag_right5C);
                dbCommand.Parameters.AddWithValue("@tag_right6C", string.IsNullOrEmpty(tagmodel.tag_right6C) ? "" : tagmodel.tag_right6C);
                dbCommand.Parameters.AddWithValue("@tag_right7C", string.IsNullOrEmpty(tagmodel.tag_right7C) ? "" : tagmodel.tag_right7C);
                //tag_left1D
                dbCommand.Parameters.AddWithValue("@tag_left1D", string.IsNullOrEmpty(tagmodel.tag_left1D) ? "" : tagmodel.tag_left1D);
                dbCommand.Parameters.AddWithValue("@tag_left2D", string.IsNullOrEmpty(tagmodel.tag_left2D) ? "" : tagmodel.tag_left2D);
                dbCommand.Parameters.AddWithValue("@tag_left3D", string.IsNullOrEmpty(tagmodel.tag_left3D) ? "" : tagmodel.tag_left3D);
                dbCommand.Parameters.AddWithValue("@tag_left4D", string.IsNullOrEmpty(tagmodel.tag_left4D) ? "" : tagmodel.tag_left4D);
                dbCommand.Parameters.AddWithValue("@tag_left5D", string.IsNullOrEmpty(tagmodel.tag_left5D) ? "" : tagmodel.tag_left5D);
                dbCommand.Parameters.AddWithValue("@tag_left6D", string.IsNullOrEmpty(tagmodel.tag_left6D) ? "" : tagmodel.tag_left6D);
                dbCommand.Parameters.AddWithValue("@tag_left7D", string.IsNullOrEmpty(tagmodel.tag_left7D) ? "" : tagmodel.tag_left7D);

                dbCommand.Parameters.AddWithValue("@tag_right1D", string.IsNullOrEmpty(tagmodel.tag_right1D) ? "" : tagmodel.tag_right1D);
                dbCommand.Parameters.AddWithValue("@tag_right2D", string.IsNullOrEmpty(tagmodel.tag_right2D) ? "" : tagmodel.tag_right2D);
                dbCommand.Parameters.AddWithValue("@tag_right3D", string.IsNullOrEmpty(tagmodel.tag_right3D) ? "" : tagmodel.tag_right3D);
                dbCommand.Parameters.AddWithValue("@tag_right4D", string.IsNullOrEmpty(tagmodel.tag_right4D) ? "" : tagmodel.tag_right4D);
                dbCommand.Parameters.AddWithValue("@tag_right5D", string.IsNullOrEmpty(tagmodel.tag_right5D) ? "" : tagmodel.tag_right5D);
                dbCommand.Parameters.AddWithValue("@tag_right6D", string.IsNullOrEmpty(tagmodel.tag_right6D) ? "" : tagmodel.tag_right6D);
                dbCommand.Parameters.AddWithValue("@tag_right7D", string.IsNullOrEmpty(tagmodel.tag_right7D) ? "" : tagmodel.tag_right7D);
                //tag_left1E
                dbCommand.Parameters.AddWithValue("@tag_left1E", string.IsNullOrEmpty(tagmodel.tag_left1E) ? "" : tagmodel.tag_left1E);
                dbCommand.Parameters.AddWithValue("@tag_left2E", string.IsNullOrEmpty(tagmodel.tag_left2E) ? "" : tagmodel.tag_left2E);
                dbCommand.Parameters.AddWithValue("@tag_left3E", string.IsNullOrEmpty(tagmodel.tag_left3E) ? "" : tagmodel.tag_left3E);
                dbCommand.Parameters.AddWithValue("@tag_left4E", string.IsNullOrEmpty(tagmodel.tag_left4E) ? "" : tagmodel.tag_left4E);
                dbCommand.Parameters.AddWithValue("@tag_left5E", string.IsNullOrEmpty(tagmodel.tag_left5E) ? "" : tagmodel.tag_left5E);
                dbCommand.Parameters.AddWithValue("@tag_left6E", string.IsNullOrEmpty(tagmodel.tag_left6E) ? "" : tagmodel.tag_left6E);
                dbCommand.Parameters.AddWithValue("@tag_left7E", string.IsNullOrEmpty(tagmodel.tag_left7E) ? "" : tagmodel.tag_left7E);

                dbCommand.Parameters.AddWithValue("@tag_right1E", string.IsNullOrEmpty(tagmodel.tag_right1E) ? "" : tagmodel.tag_right1E);
                dbCommand.Parameters.AddWithValue("@tag_right2E", string.IsNullOrEmpty(tagmodel.tag_right2E) ? "" : tagmodel.tag_right2E);
                dbCommand.Parameters.AddWithValue("@tag_right3E", string.IsNullOrEmpty(tagmodel.tag_right3E) ? "" : tagmodel.tag_right3E);
                dbCommand.Parameters.AddWithValue("@tag_right4E", string.IsNullOrEmpty(tagmodel.tag_right4E) ? "" : tagmodel.tag_right4E);
                dbCommand.Parameters.AddWithValue("@tag_right5E", string.IsNullOrEmpty(tagmodel.tag_right5E) ? "" : tagmodel.tag_right5E);
                dbCommand.Parameters.AddWithValue("@tag_right6E", string.IsNullOrEmpty(tagmodel.tag_right6E) ? "" : tagmodel.tag_right6E);
                dbCommand.Parameters.AddWithValue("@tag_right7E", string.IsNullOrEmpty(tagmodel.tag_right7E) ? "" : tagmodel.tag_right7E);

                dbCommand.CommandTimeout = 5000;
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
        public bool UpdateUPSTagFields(TagModel tagmodel, string tag_place, string tag_text, bool NoTagPrice = false, bool IgnoreDollerforprice = false, bool ignoredecimals = false)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {

                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = @"Update ups_ins set 
                                        tag_left1   = @tag_left1,  tag_left2   = @tag_left2, tag_left3   = @tag_left3, tag_left4   = @tag_left4, tag_left5   = @tag_left5,
                                        tag_left1A  = @tag_left1A, tag_left2A  = @tag_left2A, tag_left3A = @tag_left3A, tag_left4A = @tag_left4A, tag_left5A = @tag_left5A,
                                        tag_left1B  = @tag_left1B, tag_left2B  = @tag_left2B, tag_left3B = @tag_left3B, tag_left4B = @tag_left4B, tag_left5B = @tag_left5B,
                                        tag_right4  = @tag_right4, tag_right5  = @tag_right5,
                                        tag_right4A = @tag_right4A,tag_right5A = @tag_right5A,                                       
                                        tag_right4B = @tag_right4B,tag_right5B = @tag_right5B
                                       ,MOVE_BARCODE=@MOVE_BARCODE, TAG_RIGHT1=@TAG_RIGHT1,TAG_RIGHT2=@TAG_RIGHT2 ,TAG_RIGHT3=@TAG_RIGHT3 ,TAG_RIGHT2A=@TAG_RIGHT2A,TAG_RIGHT2B=@TAG_RIGHT2B
                                        ,tag_place = @tag_place,tag_text = @tag_text,no_tagprice= @NoTagPrice; 
                                        update ups_ins1 set
                                        tag_left6 = @tag_left6, TAG_LEFT7 = @tag_left7, tag_left6A = @tag_left6A, TAG_LEFT7A = @tag_left7A, TAG_LEFT6B = @tag_left6B, TAG_LEFT7B = @tag_left7B, 
                                        tag_right1A = @tag_right1A, tag_right3A = @tag_right3A, tag_right1B = @tag_right1B, tag_right3B = @tag_right3B, tag_right6 = @tag_right6, tag_right7 = @tag_right7, tag_right6A = @tag_right6A, tag_right7A = @tag_right7A, tag_right6B = @tag_right6B, tag_right7B = @tag_right7B 
                                        ,tag_place2 = @tag_place2,tag_text2 = @tag_text2,tag_place3 = @tag_place3,tag_text3 = @tag_text3,tag_place4 = @tag_place4,tag_text4 = @tag_text4
                                        ,IgnoreDollerforprice =@IgnoreDollerforprice, ignoredecimals=@ignoredecimals ,tag_left1C=@tag_left1C,tag_left1D=@tag_left1D,tag_left1E=@tag_left1E,
                                        tag_left2C=@tag_left2C,tag_left2D=@tag_left2D,tag_left2E=@tag_left2E,tag_left3C=@tag_left3C,tag_left3D=@tag_left3D,tag_left3E=@tag_left3E,
                                        tag_left4C=@tag_left4C,tag_left4D=@tag_left4D,tag_left4E=@tag_left4E,tag_left5C=@tag_left5C,tag_left5D=@tag_left5D,tag_left5E=@tag_left5E, 
                                        tag_left6C=@tag_left6C,tag_left6D=@tag_left6D,tag_left6E=@tag_left6E, tag_left7C=@tag_left7C,tag_left7D=@tag_left7D,tag_left7E=@tag_left7E 
                                        
                                        , tag_right2C=@tag_right2C,tag_right2D=@tag_right2D,tag_right2E=@tag_right2E,tag_right3C=@tag_right3C,tag_right3D=@tag_right3D,tag_right3E=@tag_right3E,
                                        tag_right4C=@tag_right4C,tag_right4D=@tag_right4D,tag_right4E=@tag_right4E,tag_right5C=@tag_right5C,tag_right5D=@tag_right5D,tag_right5E=@tag_right5E, 
                                        tag_right6C=@tag_right6C,tag_right6D=@tag_right6D,tag_right6E=@tag_right6E,tag_right7C=@tag_right7C,tag_right7D=@tag_right7D,tag_right7E=@tag_right7E 
                                        ";//tag_place2,tag_text2,tag_place3,tag_text3,tag_place4,tag_text4

                dbCommand.Parameters.AddWithValue("@tag_left1", string.IsNullOrEmpty(tagmodel.tag_left1) ? "" : tagmodel.tag_left1);
                dbCommand.Parameters.AddWithValue("@tag_left2", string.IsNullOrEmpty(tagmodel.tag_left2) ? "" : tagmodel.tag_left2);
                dbCommand.Parameters.AddWithValue("@tag_left3", string.IsNullOrEmpty(tagmodel.tag_left3) ? "" : tagmodel.tag_left3);
                dbCommand.Parameters.AddWithValue("@tag_left4", string.IsNullOrEmpty(tagmodel.tag_left4) ? "" : tagmodel.tag_left4);
                dbCommand.Parameters.AddWithValue("@tag_left5", string.IsNullOrEmpty(tagmodel.tag_left5) ? "" : tagmodel.tag_left5);
                dbCommand.Parameters.AddWithValue("@tag_left6", string.IsNullOrEmpty(tagmodel.tag_left6) ? "" : tagmodel.tag_left6);
                dbCommand.Parameters.AddWithValue("@tag_left7", string.IsNullOrEmpty(tagmodel.tag_left7) ? "" : tagmodel.tag_left7);

                dbCommand.Parameters.AddWithValue("@tag_right4", string.IsNullOrEmpty(tagmodel.tag_right4) ? "" : tagmodel.tag_right4);
                dbCommand.Parameters.AddWithValue("@tag_right5", string.IsNullOrEmpty(tagmodel.tag_right5) ? "" : tagmodel.tag_right5);
                dbCommand.Parameters.AddWithValue("@tag_right6", string.IsNullOrEmpty(tagmodel.tag_right6) ? "" : tagmodel.tag_right6);
                dbCommand.Parameters.AddWithValue("@tag_right7", string.IsNullOrEmpty(tagmodel.tag_right7) ? "" : tagmodel.tag_right7);

                dbCommand.Parameters.AddWithValue("@tag_left1A", string.IsNullOrEmpty(tagmodel.tag_left1A) ? "" : tagmodel.tag_left1A);
                dbCommand.Parameters.AddWithValue("@tag_left2A", string.IsNullOrEmpty(tagmodel.tag_left2A) ? "" : tagmodel.tag_left2A);
                dbCommand.Parameters.AddWithValue("@tag_left3A", string.IsNullOrEmpty(tagmodel.tag_left3A) ? "" : tagmodel.tag_left3A);
                dbCommand.Parameters.AddWithValue("@tag_left4A", string.IsNullOrEmpty(tagmodel.tag_left4A) ? "" : tagmodel.tag_left4A);
                dbCommand.Parameters.AddWithValue("@tag_left5A", string.IsNullOrEmpty(tagmodel.tag_left5A) ? "" : tagmodel.tag_left5A);
                dbCommand.Parameters.AddWithValue("@tag_left6A", string.IsNullOrEmpty(tagmodel.tag_left6A) ? "" : tagmodel.tag_left6A);
                dbCommand.Parameters.AddWithValue("@tag_left7A", string.IsNullOrEmpty(tagmodel.tag_left7A) ? "" : tagmodel.tag_left7A);

                dbCommand.Parameters.AddWithValue("@tag_right1A", string.IsNullOrEmpty(tagmodel.tag_right1A) ? "" : tagmodel.tag_right1A);
                dbCommand.Parameters.AddWithValue("@tag_right3A", string.IsNullOrEmpty(tagmodel.tag_right3A) ? "" : tagmodel.tag_right3A);
                dbCommand.Parameters.AddWithValue("@tag_right4A", string.IsNullOrEmpty(tagmodel.tag_right4A) ? "" : tagmodel.tag_right4A);
                dbCommand.Parameters.AddWithValue("@tag_right5A", string.IsNullOrEmpty(tagmodel.tag_right5A) ? "" : tagmodel.tag_right5A);
                dbCommand.Parameters.AddWithValue("@tag_right6A", string.IsNullOrEmpty(tagmodel.tag_right6A) ? "" : tagmodel.tag_right6A);
                dbCommand.Parameters.AddWithValue("@tag_right7A", string.IsNullOrEmpty(tagmodel.tag_right7A) ? "" : tagmodel.tag_right7A);

                dbCommand.Parameters.AddWithValue("@tag_left1B", string.IsNullOrEmpty(tagmodel.tag_left1B) ? "" : tagmodel.tag_left1B);
                dbCommand.Parameters.AddWithValue("@tag_left2B", string.IsNullOrEmpty(tagmodel.tag_left2B) ? "" : tagmodel.tag_left2B);
                dbCommand.Parameters.AddWithValue("@tag_left3B", string.IsNullOrEmpty(tagmodel.tag_left3B) ? "" : tagmodel.tag_left3B);
                dbCommand.Parameters.AddWithValue("@tag_left4B", string.IsNullOrEmpty(tagmodel.tag_left4B) ? "" : tagmodel.tag_left4B);
                dbCommand.Parameters.AddWithValue("@tag_left5B", string.IsNullOrEmpty(tagmodel.tag_left5B) ? "" : tagmodel.tag_left5B);
                dbCommand.Parameters.AddWithValue("@tag_left6B", string.IsNullOrEmpty(tagmodel.tag_left6B) ? "" : tagmodel.tag_left6B);
                dbCommand.Parameters.AddWithValue("@tag_left7B", string.IsNullOrEmpty(tagmodel.tag_left7B) ? "" : tagmodel.tag_left7B);

                dbCommand.Parameters.AddWithValue("@tag_right1B", string.IsNullOrEmpty(tagmodel.tag_right1B) ? "" : tagmodel.tag_right1B);
                dbCommand.Parameters.AddWithValue("@tag_right3B", string.IsNullOrEmpty(tagmodel.tag_right3B) ? "" : tagmodel.tag_right3B);
                dbCommand.Parameters.AddWithValue("@tag_right4B", string.IsNullOrEmpty(tagmodel.tag_right4B) ? "" : tagmodel.tag_right4B);
                dbCommand.Parameters.AddWithValue("@tag_right5B", string.IsNullOrEmpty(tagmodel.tag_right5B) ? "" : tagmodel.tag_right5B);
                dbCommand.Parameters.AddWithValue("@tag_right6B", string.IsNullOrEmpty(tagmodel.tag_right6B) ? "" : tagmodel.tag_right6B);
                dbCommand.Parameters.AddWithValue("@tag_right7B", string.IsNullOrEmpty(tagmodel.tag_right7B) ? "" : tagmodel.tag_right7B);

                dbCommand.Parameters.AddWithValue("@TAG_RIGHT1", string.IsNullOrEmpty(tagmodel.tag_right1) ? "" : tagmodel.tag_right1);
                dbCommand.Parameters.AddWithValue("@TAG_RIGHT2", string.IsNullOrEmpty(tagmodel.tag_right2) ? "" : tagmodel.tag_right2);
                dbCommand.Parameters.AddWithValue("@TAG_RIGHT3", string.IsNullOrEmpty(tagmodel.tag_right3) ? "" : tagmodel.tag_right3);
                dbCommand.Parameters.AddWithValue("@TAG_RIGHT2A", string.IsNullOrEmpty(tagmodel.tag_right2A) ? "" : tagmodel.tag_right2A);
                dbCommand.Parameters.AddWithValue("@TAG_RIGHT2B", string.IsNullOrEmpty(tagmodel.tag_right2B) ? "" : tagmodel.tag_right2B);

                dbCommand.Parameters.AddWithValue("@MOVE_BARCODE", tagmodel.Move_barcode);

                dbCommand.Parameters.AddWithValue("@tag_text", string.IsNullOrEmpty(tag_text) ? "" : tag_text);
                dbCommand.Parameters.AddWithValue("@tag_place", string.IsNullOrEmpty(tag_place) ? "" : tag_place);
                dbCommand.Parameters.AddWithValue("@NoTagPrice", NoTagPrice);
                dbCommand.Parameters.AddWithValue("@IgnoreDollerforprice", IgnoreDollerforprice);
                dbCommand.Parameters.AddWithValue("@ignoredecimals", ignoredecimals);

                dbCommand.Parameters.AddWithValue("@tag_text2", string.IsNullOrEmpty(tagmodel.CTag_Text2) ? "" : tagmodel.CTag_Text2);
                dbCommand.Parameters.AddWithValue("@tag_place2", string.IsNullOrEmpty(tagmodel.CTag_Place2) ? "" : tagmodel.CTag_Place2);
                dbCommand.Parameters.AddWithValue("@tag_text3", string.IsNullOrEmpty(tagmodel.CTag_Text3) ? "" : tagmodel.CTag_Text3);
                dbCommand.Parameters.AddWithValue("@tag_place3", string.IsNullOrEmpty(tagmodel.CTag_Place3) ? "" : tagmodel.CTag_Place3);
                dbCommand.Parameters.AddWithValue("@tag_text4", string.IsNullOrEmpty(tagmodel.CTag_Text4) ? "" : tagmodel.CTag_Text4);
                dbCommand.Parameters.AddWithValue("@tag_place4", string.IsNullOrEmpty(tagmodel.CTag_Place4) ? "" : tagmodel.CTag_Place4);
                //tag_left1C
                dbCommand.Parameters.AddWithValue("@tag_left1C", string.IsNullOrEmpty(tagmodel.tag_left1C) ? "" : tagmodel.tag_left1C);
                dbCommand.Parameters.AddWithValue("@tag_left2C", string.IsNullOrEmpty(tagmodel.tag_left2C) ? "" : tagmodel.tag_left2C);
                dbCommand.Parameters.AddWithValue("@tag_left3C", string.IsNullOrEmpty(tagmodel.tag_left3C) ? "" : tagmodel.tag_left3C);
                dbCommand.Parameters.AddWithValue("@tag_left4C", string.IsNullOrEmpty(tagmodel.tag_left4C) ? "" : tagmodel.tag_left4C);
                dbCommand.Parameters.AddWithValue("@tag_left5C", string.IsNullOrEmpty(tagmodel.tag_left5C) ? "" : tagmodel.tag_left5C);
                dbCommand.Parameters.AddWithValue("@tag_left6C", string.IsNullOrEmpty(tagmodel.tag_left6C) ? "" : tagmodel.tag_left6C);
                dbCommand.Parameters.AddWithValue("@tag_left7C", string.IsNullOrEmpty(tagmodel.tag_left7C) ? "" : tagmodel.tag_left7C);

                dbCommand.Parameters.AddWithValue("@tag_right1C", string.IsNullOrEmpty(tagmodel.tag_right1C) ? "" : tagmodel.tag_right1C);
                dbCommand.Parameters.AddWithValue("@tag_right2C", string.IsNullOrEmpty(tagmodel.tag_right2C) ? "" : tagmodel.tag_right2C);
                dbCommand.Parameters.AddWithValue("@tag_right3C", string.IsNullOrEmpty(tagmodel.tag_right3C) ? "" : tagmodel.tag_right3C);
                dbCommand.Parameters.AddWithValue("@tag_right4C", string.IsNullOrEmpty(tagmodel.tag_right4C) ? "" : tagmodel.tag_right4C);
                dbCommand.Parameters.AddWithValue("@tag_right5C", string.IsNullOrEmpty(tagmodel.tag_right5C) ? "" : tagmodel.tag_right5C);
                dbCommand.Parameters.AddWithValue("@tag_right6C", string.IsNullOrEmpty(tagmodel.tag_right6C) ? "" : tagmodel.tag_right6C);
                dbCommand.Parameters.AddWithValue("@tag_right7C", string.IsNullOrEmpty(tagmodel.tag_right7C) ? "" : tagmodel.tag_right7C);
                //tag_left1D
                dbCommand.Parameters.AddWithValue("@tag_left1D", string.IsNullOrEmpty(tagmodel.tag_left1D) ? "" : tagmodel.tag_left1D);
                dbCommand.Parameters.AddWithValue("@tag_left2D", string.IsNullOrEmpty(tagmodel.tag_left2D) ? "" : tagmodel.tag_left2D);
                dbCommand.Parameters.AddWithValue("@tag_left3D", string.IsNullOrEmpty(tagmodel.tag_left3D) ? "" : tagmodel.tag_left3D);
                dbCommand.Parameters.AddWithValue("@tag_left4D", string.IsNullOrEmpty(tagmodel.tag_left4D) ? "" : tagmodel.tag_left4D);
                dbCommand.Parameters.AddWithValue("@tag_left5D", string.IsNullOrEmpty(tagmodel.tag_left5D) ? "" : tagmodel.tag_left5D);
                dbCommand.Parameters.AddWithValue("@tag_left6D", string.IsNullOrEmpty(tagmodel.tag_left6D) ? "" : tagmodel.tag_left6D);
                dbCommand.Parameters.AddWithValue("@tag_left7D", string.IsNullOrEmpty(tagmodel.tag_left7D) ? "" : tagmodel.tag_left7D);

                dbCommand.Parameters.AddWithValue("@tag_right1D", string.IsNullOrEmpty(tagmodel.tag_right1D) ? "" : tagmodel.tag_right1D);
                dbCommand.Parameters.AddWithValue("@tag_right2D", string.IsNullOrEmpty(tagmodel.tag_right2D) ? "" : tagmodel.tag_right2D);
                dbCommand.Parameters.AddWithValue("@tag_right3D", string.IsNullOrEmpty(tagmodel.tag_right3D) ? "" : tagmodel.tag_right3D);
                dbCommand.Parameters.AddWithValue("@tag_right4D", string.IsNullOrEmpty(tagmodel.tag_right4D) ? "" : tagmodel.tag_right4D);
                dbCommand.Parameters.AddWithValue("@tag_right5D", string.IsNullOrEmpty(tagmodel.tag_right5D) ? "" : tagmodel.tag_right5D);
                dbCommand.Parameters.AddWithValue("@tag_right6D", string.IsNullOrEmpty(tagmodel.tag_right6D) ? "" : tagmodel.tag_right6D);
                dbCommand.Parameters.AddWithValue("@tag_right7D", string.IsNullOrEmpty(tagmodel.tag_right7D) ? "" : tagmodel.tag_right7D);
                //tag_left1E
                dbCommand.Parameters.AddWithValue("@tag_left1E", string.IsNullOrEmpty(tagmodel.tag_left1E) ? "" : tagmodel.tag_left1E);
                dbCommand.Parameters.AddWithValue("@tag_left2E", string.IsNullOrEmpty(tagmodel.tag_left2E) ? "" : tagmodel.tag_left2E);
                dbCommand.Parameters.AddWithValue("@tag_left3E", string.IsNullOrEmpty(tagmodel.tag_left3E) ? "" : tagmodel.tag_left3E);
                dbCommand.Parameters.AddWithValue("@tag_left4E", string.IsNullOrEmpty(tagmodel.tag_left4E) ? "" : tagmodel.tag_left4E);
                dbCommand.Parameters.AddWithValue("@tag_left5E", string.IsNullOrEmpty(tagmodel.tag_left5E) ? "" : tagmodel.tag_left5E);
                dbCommand.Parameters.AddWithValue("@tag_left6E", string.IsNullOrEmpty(tagmodel.tag_left6E) ? "" : tagmodel.tag_left6E);
                dbCommand.Parameters.AddWithValue("@tag_left7E", string.IsNullOrEmpty(tagmodel.tag_left7E) ? "" : tagmodel.tag_left7E);

                dbCommand.Parameters.AddWithValue("@tag_right1E", string.IsNullOrEmpty(tagmodel.tag_right1E) ? "" : tagmodel.tag_right1E);
                dbCommand.Parameters.AddWithValue("@tag_right2E", string.IsNullOrEmpty(tagmodel.tag_right2E) ? "" : tagmodel.tag_right2E);
                dbCommand.Parameters.AddWithValue("@tag_right3E", string.IsNullOrEmpty(tagmodel.tag_right3E) ? "" : tagmodel.tag_right3E);
                dbCommand.Parameters.AddWithValue("@tag_right4E", string.IsNullOrEmpty(tagmodel.tag_right4E) ? "" : tagmodel.tag_right4E);
                dbCommand.Parameters.AddWithValue("@tag_right5E", string.IsNullOrEmpty(tagmodel.tag_right5E) ? "" : tagmodel.tag_right5E);
                dbCommand.Parameters.AddWithValue("@tag_right6E", string.IsNullOrEmpty(tagmodel.tag_right6E) ? "" : tagmodel.tag_right6E);
                dbCommand.Parameters.AddWithValue("@tag_right7E", string.IsNullOrEmpty(tagmodel.tag_right7E) ? "" : tagmodel.tag_right7E);

                dbCommand.CommandTimeout = 5000;
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public DataTable GetUPSDiamondTagFields()
        {
            string tagline = "isnull(tag_line1, '') tag_line1, isnull(tag_line2, '') tag_line2, isnull(tag_line3, '') tag_line3 , isnull(tag_line4, '') tag_line4, isnull(tag_line5, '') tag_line5, ";
            string taglineA = "isnull(tag_line1A, '') tag_line1A, isnull(tag_line2A, '') tag_line2A, isnull(tag_line3A, '')tag_line3A, isnull(tag_line4A, '') tag_line4A, isnull(tag_line5A, '') tag_line5A, ";
            string taglineB = "isnull(tag_line1B, '') tag_line1B, isnull(tag_line2B, '') tag_line2B, isnull(tag_line3B, '')tag_line3B, isnull(tag_line4B, '') tag_line4B, isnull(tag_line5B, '') tag_line5B, ";
            string taglineC = "isnull(tag_line1C, '') tag_line1C, isnull(tag_line2C, '') tag_line2C, isnull(tag_line3C, '')tag_line3C, isnull(tag_line4C, '') tag_line4C, isnull(tag_line5C, '') tag_line5C, ";
            string taglineD = "isnull(tag_line1D, '') tag_line1D, isnull(tag_line2D, '') tag_line2D, isnull(tag_line3D, '')tag_line3D, isnull(tag_line4D, '') tag_line4D, isnull(tag_line5D, '') tag_line5D, ";
            string taglineE = "isnull(tag_line1E, '') tag_line1E, isnull(tag_line2E, '') tag_line2E, isnull(tag_line3E, '')tag_line3E, isnull(tag_line4E, '') tag_line4E, isnull(tag_line5E, '') tag_line5E ";
            string strTagline = @"SELECT " + tagline + taglineA + taglineB + taglineC + taglineD + taglineE + " from ups_ins"; //+ FieldName + FieldValues;
            return _helperCommonService.GetSqlData(strTagline);
        }

        public bool UpdateDIAMONDLABEL_TEMPLATE(string templateName, bool isUpdate, DiamondModel diamondModel, string fieldVal1, string fieldVal2, string fieldVal3, string fieldVal4, string fieldVal5)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.CommandType = CommandType.Text;
                dbCommand.Connection = _connectionProvider.GetConnection();
                if (isUpdate)
                    dbCommand.CommandText = @"UPDATE DIAMONDLABEL_TEMPLATE SET tag_line1 = @tag_line1, tag_line2 = @tag_line2, tag_line3 = @tag_line3, tag_line4 = @tag_line4, tag_line5 = @tag_line5, tag_line1A = @tag_line1A, tag_line2A = @tag_line2A, tag_line3A = @tag_line3A, tag_line4A = @tag_line4A, tag_line5A = @tag_line5A, tag_line1B = @tag_line1B, tag_line2B = @tag_line2B, tag_line3B = @tag_line3B, tag_line4B = @tag_line4B, tag_line5B = @tag_line5B, tag_line1C = @tag_line1C, tag_line2C = @tag_line2C, tag_line3C = @tag_line3C, tag_line4C = @tag_line4C, tag_line5C = @tag_line5C, tag_line1D = @tag_line1D, tag_line2D = @tag_line2D, tag_line3D = @tag_line3D, tag_line4D = @tag_line4D, tag_line5D = @tag_line5D, tag_line1E = @tag_line1E, tag_line2E = @tag_line2E, tag_line3E = @tag_line3E, tag_line4E = @tag_line4E, tag_line5E = @tag_line5E where template_name = @templateName";
                else
                    dbCommand.CommandText = @" INSERT iNTO DIAMONDLABEL_TEMPLATE (template_name,tag_line1,tag_line2,tag_line3,tag_line4,tag_line5,tag_line1A,tag_line2A,tag_line3A,tag_line4A,tag_line5A,tag_line1B,tag_line2B,tag_line3B,tag_line4B,tag_line5B,tag_line1C,tag_line2C,tag_line3C,tag_line4C,tag_line5C,tag_line1D,tag_line2D,tag_line3D,tag_line4D,tag_line5D,tag_line1E,tag_line2E,tag_line3E,tag_line4E,tag_line5E) VALUES (@templateName,@tag_line1, @tag_line2, @tag_line3, @tag_line4, @tag_line5, @tag_line1A, @tag_line2A, @tag_line3A, @tag_line4A, @tag_line5A, @tag_line1B, @tag_line2B, @tag_line3B, @tag_line4B, @tag_line5B, @tag_line1C, @tag_line2C, @tag_line3C, @tag_line4C, @tag_line5C, @tag_line1D, @tag_line2D, @tag_line3D, @tag_line4D, @tag_line5D, @tag_line1E, @tag_line2E, @tag_line3E, @tag_line4E, @tag_line5E)";

                dbCommand.Parameters.AddWithValue("@templateName", templateName);
                dbCommand.Parameters.AddWithValue("@tag_line1", string.IsNullOrEmpty(diamondModel.tag_line1) ? "" : diamondModel.tag_line1);
                dbCommand.Parameters.AddWithValue("@tag_line2", string.IsNullOrEmpty(diamondModel.tag_line2) ? "" : diamondModel.tag_line2);
                dbCommand.Parameters.AddWithValue("@tag_line3", string.IsNullOrEmpty(diamondModel.tag_line3) ? "" : diamondModel.tag_line3);
                dbCommand.Parameters.AddWithValue("@tag_line4", string.IsNullOrEmpty(diamondModel.tag_line4) ? "" : diamondModel.tag_line4);
                dbCommand.Parameters.AddWithValue("@tag_line5", string.IsNullOrEmpty(diamondModel.tag_line5) ? "" : diamondModel.tag_line5);

                dbCommand.Parameters.AddWithValue("@tag_line1A", string.IsNullOrEmpty(diamondModel.tag_line1A) ? "" : diamondModel.tag_line1A);
                dbCommand.Parameters.AddWithValue("@tag_line2A", string.IsNullOrEmpty(diamondModel.tag_line2A) ? "" : diamondModel.tag_line2A);
                dbCommand.Parameters.AddWithValue("@tag_line3A", string.IsNullOrEmpty(diamondModel.tag_line3A) ? "" : diamondModel.tag_line3A);
                dbCommand.Parameters.AddWithValue("@tag_line4A", string.IsNullOrEmpty(diamondModel.tag_line4A) ? "" : diamondModel.tag_line4A);
                dbCommand.Parameters.AddWithValue("@tag_line5A", string.IsNullOrEmpty(diamondModel.tag_line5A) ? "" : diamondModel.tag_line5A);

                dbCommand.Parameters.AddWithValue("@tag_line1B", string.IsNullOrEmpty(diamondModel.tag_line1B) ? "" : diamondModel.tag_line1B);
                dbCommand.Parameters.AddWithValue("@tag_line2B", string.IsNullOrEmpty(diamondModel.tag_line2B) ? "" : diamondModel.tag_line2B);
                dbCommand.Parameters.AddWithValue("@tag_line3B", string.IsNullOrEmpty(diamondModel.tag_line3B) ? "" : diamondModel.tag_line3B);
                dbCommand.Parameters.AddWithValue("@tag_line4B", string.IsNullOrEmpty(diamondModel.tag_line4B) ? "" : diamondModel.tag_line4B);
                dbCommand.Parameters.AddWithValue("@tag_line5B", string.IsNullOrEmpty(diamondModel.tag_line5B) ? "" : diamondModel.tag_line5B);

                dbCommand.Parameters.AddWithValue("@tag_line1C", string.IsNullOrEmpty(diamondModel.tag_line1C) ? "" : diamondModel.tag_line1C);
                dbCommand.Parameters.AddWithValue("@tag_line2C", string.IsNullOrEmpty(diamondModel.tag_line2C) ? "" : diamondModel.tag_line2C);
                dbCommand.Parameters.AddWithValue("@tag_line3C", string.IsNullOrEmpty(diamondModel.tag_line3C) ? "" : diamondModel.tag_line3C);
                dbCommand.Parameters.AddWithValue("@tag_line4C", string.IsNullOrEmpty(diamondModel.tag_line4C) ? "" : diamondModel.tag_line4C);
                dbCommand.Parameters.AddWithValue("@tag_line5C", string.IsNullOrEmpty(diamondModel.tag_line5C) ? "" : diamondModel.tag_line5C);

                dbCommand.Parameters.AddWithValue("@tag_line1D", string.IsNullOrEmpty(diamondModel.tag_line1D) ? "" : diamondModel.tag_line1D);
                dbCommand.Parameters.AddWithValue("@tag_line2D", string.IsNullOrEmpty(diamondModel.tag_line2D) ? "" : diamondModel.tag_line2D);
                dbCommand.Parameters.AddWithValue("@tag_line3D", string.IsNullOrEmpty(diamondModel.tag_line3D) ? "" : diamondModel.tag_line3D);
                dbCommand.Parameters.AddWithValue("@tag_line4D", string.IsNullOrEmpty(diamondModel.tag_line4D) ? "" : diamondModel.tag_line4D);
                dbCommand.Parameters.AddWithValue("@tag_line5D", string.IsNullOrEmpty(diamondModel.tag_line5D) ? "" : diamondModel.tag_line5D);

                dbCommand.Parameters.AddWithValue("@tag_line1E", string.IsNullOrEmpty(diamondModel.tag_line1E) ? "" : diamondModel.tag_line1E);
                dbCommand.Parameters.AddWithValue("@tag_line2E", string.IsNullOrEmpty(diamondModel.tag_line2E) ? "" : diamondModel.tag_line2E);
                dbCommand.Parameters.AddWithValue("@tag_line3E", string.IsNullOrEmpty(diamondModel.tag_line3E) ? "" : diamondModel.tag_line3E);
                dbCommand.Parameters.AddWithValue("@tag_line4E", string.IsNullOrEmpty(diamondModel.tag_line4E) ? "" : diamondModel.tag_line4E);
                dbCommand.Parameters.AddWithValue("@tag_line5E", string.IsNullOrEmpty(diamondModel.tag_line5E) ? "" : diamondModel.tag_line5E);

                dbCommand.CommandTimeout = 5000;
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public bool UpdateUPSDiamondTagFields(DiamondModel diamondModel, string fieldVal1, string fieldVal2, string fieldVal3, string fieldVal4, string fieldVal5)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.CommandType = CommandType.Text;
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandText = @"UPDATE ups_ins SET tag_line1 = '', tag_line2 = '', tag_line3 = '', tag_line4 = '', tag_line5 = '', tag_line1A = '', tag_line2A = '', tag_line3A = '', tag_line4A = '', tag_line5A = '', tag_line1B = '', tag_line2B = '', tag_line3B = '', tag_line4B = '', tag_line5B = '', tag_line1C = '', tag_line2C = '', tag_line3C = '', tag_line4C = '', tag_line5C = '', tag_line1D = '', tag_line2D = '', tag_line3D = '', tag_line4D = '', tag_line5D = '', tag_line1E = '', tag_line2E = '', tag_line3E = '', tag_line4E = '', tag_line5E = ''";
                dbCommand.CommandTimeout = 5000;
                dbCommand.Connection.Open();
                var rowAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                dbCommand.CommandType = CommandType.Text;
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandText = @"UPDATE ups_ins SET tag_line1 = @tag_line1, tag_line2 = @tag_line2, tag_line3 = @tag_line3, tag_line4 = @tag_line4, tag_line5 = @tag_line5, tag_line1A = @tag_line1A, tag_line2A = @tag_line2A, tag_line3A = @tag_line3A, tag_line4A = @tag_line4A, tag_line5A = @tag_line5A, tag_line1B = @tag_line1B, tag_line2B = @tag_line2B, tag_line3B = @tag_line3B, tag_line4B = @tag_line4B, tag_line5B = @tag_line5B, tag_line1C = @tag_line1C, tag_line2C = @tag_line2C, tag_line3C = @tag_line3C, tag_line4C = @tag_line4C, tag_line5C = @tag_line5C, tag_line1D = @tag_line1D, tag_line2D = @tag_line2D, tag_line3D = @tag_line3D, tag_line4D = @tag_line4D, tag_line5D = @tag_line5D, tag_line1E = @tag_line1E, tag_line2E = @tag_line2E, tag_line3E = @tag_line3E, tag_line4E = @tag_line4E, tag_line5E = @tag_line5E";
                //dbCommand.CommandText = @"UPDATE ups_ins SET tag_line1 = @tag_line1, tag_line2 = @tag_line2, tag_line3 = @tag_line3, tag_line4 = @tag_line4, tag_line5 = @tag_line5, tag_line1A = @tag_line1A, tag_line2A = @tag_line2A, tag_line3A = @tag_line3A, tag_line4A = @tag_line4A, tag_line5A = @tag_line5A, tag_line1B = @tag_line1B, tag_line2B = @tag_line2B, tag_line3B = @tag_line3B, tag_line4B = @tag_line4B, tag_line5B = @tag_line5B, tag_line1C = @tag_line1C, tag_line2C = @tag_line2C, tag_line3C = @tag_line3C, tag_line4C = @tag_line4C, tag_line5C = @tag_line5C, tag_line1D = @tag_line1D, tag_line2D = @tag_line2D, tag_line3D = @tag_line3D, tag_line4D = @tag_line4D, tag_line5D = @tag_line5D, tag_line1E = @tag_line1E, tag_line2E = @tag_line2E, tag_line3E = @tag_line3E, tag_line4E = @tag_line4E, tag_line5E = @tag_line5E";

                dbCommand.Parameters.AddWithValue("@tag_line1", string.IsNullOrEmpty(diamondModel.tag_line1) ? "" : diamondModel.tag_line1);
                dbCommand.Parameters.AddWithValue("@tag_line2", string.IsNullOrEmpty(diamondModel.tag_line2) ? "" : diamondModel.tag_line2);
                dbCommand.Parameters.AddWithValue("@tag_line3", string.IsNullOrEmpty(diamondModel.tag_line3) ? "" : diamondModel.tag_line3);
                dbCommand.Parameters.AddWithValue("@tag_line4", string.IsNullOrEmpty(diamondModel.tag_line4) ? "" : diamondModel.tag_line4);
                dbCommand.Parameters.AddWithValue("@tag_line5", string.IsNullOrEmpty(diamondModel.tag_line5) ? "" : diamondModel.tag_line5);

                dbCommand.Parameters.AddWithValue("@tag_line1A", string.IsNullOrEmpty(diamondModel.tag_line1A) ? "" : diamondModel.tag_line1A);
                dbCommand.Parameters.AddWithValue("@tag_line2A", string.IsNullOrEmpty(diamondModel.tag_line2A) ? "" : diamondModel.tag_line2A);
                dbCommand.Parameters.AddWithValue("@tag_line3A", string.IsNullOrEmpty(diamondModel.tag_line3A) ? "" : diamondModel.tag_line3A);
                dbCommand.Parameters.AddWithValue("@tag_line4A", string.IsNullOrEmpty(diamondModel.tag_line4A) ? "" : diamondModel.tag_line4A);
                dbCommand.Parameters.AddWithValue("@tag_line5A", string.IsNullOrEmpty(diamondModel.tag_line5A) ? "" : diamondModel.tag_line5A);

                dbCommand.Parameters.AddWithValue("@tag_line1B", string.IsNullOrEmpty(diamondModel.tag_line1B) ? "" : diamondModel.tag_line1B);
                dbCommand.Parameters.AddWithValue("@tag_line2B", string.IsNullOrEmpty(diamondModel.tag_line2B) ? "" : diamondModel.tag_line2B);
                dbCommand.Parameters.AddWithValue("@tag_line3B", string.IsNullOrEmpty(diamondModel.tag_line3B) ? "" : diamondModel.tag_line3B);
                dbCommand.Parameters.AddWithValue("@tag_line4B", string.IsNullOrEmpty(diamondModel.tag_line4B) ? "" : diamondModel.tag_line4B);
                dbCommand.Parameters.AddWithValue("@tag_line5B", string.IsNullOrEmpty(diamondModel.tag_line5B) ? "" : diamondModel.tag_line5B);

                dbCommand.Parameters.AddWithValue("@tag_line1C", string.IsNullOrEmpty(diamondModel.tag_line1C) ? "" : diamondModel.tag_line1C);
                dbCommand.Parameters.AddWithValue("@tag_line2C", string.IsNullOrEmpty(diamondModel.tag_line2C) ? "" : diamondModel.tag_line2C);
                dbCommand.Parameters.AddWithValue("@tag_line3C", string.IsNullOrEmpty(diamondModel.tag_line3C) ? "" : diamondModel.tag_line3C);
                dbCommand.Parameters.AddWithValue("@tag_line4C", string.IsNullOrEmpty(diamondModel.tag_line4C) ? "" : diamondModel.tag_line4C);
                dbCommand.Parameters.AddWithValue("@tag_line5C", string.IsNullOrEmpty(diamondModel.tag_line5C) ? "" : diamondModel.tag_line5C);

                dbCommand.Parameters.AddWithValue("@tag_line1D", string.IsNullOrEmpty(diamondModel.tag_line1D) ? "" : diamondModel.tag_line1D);
                dbCommand.Parameters.AddWithValue("@tag_line2D", string.IsNullOrEmpty(diamondModel.tag_line2D) ? "" : diamondModel.tag_line2D);
                dbCommand.Parameters.AddWithValue("@tag_line3D", string.IsNullOrEmpty(diamondModel.tag_line3D) ? "" : diamondModel.tag_line3D);
                dbCommand.Parameters.AddWithValue("@tag_line4D", string.IsNullOrEmpty(diamondModel.tag_line4D) ? "" : diamondModel.tag_line4D);
                dbCommand.Parameters.AddWithValue("@tag_line5D", string.IsNullOrEmpty(diamondModel.tag_line5D) ? "" : diamondModel.tag_line5D);

                dbCommand.Parameters.AddWithValue("@tag_line1E", string.IsNullOrEmpty(diamondModel.tag_line1E) ? "" : diamondModel.tag_line1E);
                dbCommand.Parameters.AddWithValue("@tag_line2E", string.IsNullOrEmpty(diamondModel.tag_line2E) ? "" : diamondModel.tag_line2E);
                dbCommand.Parameters.AddWithValue("@tag_line3E", string.IsNullOrEmpty(diamondModel.tag_line3E) ? "" : diamondModel.tag_line3E);
                dbCommand.Parameters.AddWithValue("@tag_line4E", string.IsNullOrEmpty(diamondModel.tag_line4E) ? "" : diamondModel.tag_line4E);
                dbCommand.Parameters.AddWithValue("@tag_line5E", string.IsNullOrEmpty(diamondModel.tag_line5E) ? "" : diamondModel.tag_line5E);


                dbCommand.Parameters.AddWithValue("@fieldVal1", fieldVal1);
                dbCommand.Parameters.AddWithValue("@fieldVal2", fieldVal2);
                dbCommand.Parameters.AddWithValue("@fieldVal3", fieldVal3);
                dbCommand.Parameters.AddWithValue("@fieldVal4", fieldVal4);
                dbCommand.Parameters.AddWithValue("@fieldVal5", fieldVal5);


                //dbCommand.CommandText = @"UPDATE ups_ins SET tag_line1 = '', tag_line2 = '', tag_line3 = '', tag_line4 = '', tag_line5 = '', tag_line1A = '', tag_line2A = '', tag_line3A = '', tag_line4A = '', tag_line5A = '', tag_line1B = '', tag_line2B = '', tag_line3B = '', tag_line4B = '', tag_line5B = '', tag_line1C = '', tag_line2C = '', tag_line3C = '', tag_line4C = '', tag_line5C = '', tag_line1D = '', tag_line2D = '', tag_line3D = '', tag_line4D = '', tag_line5D = '', tag_line1E = '', tag_line2E = '', tag_line3E = '', tag_line4E = '', tag_line5E = ''";
                dbCommand.CommandTimeout = 5000;
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public DataTable GetUPSInsDescFieldsForAutoDescription(string isFromTemplate = null)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                SqlDataAdapter.SelectCommand.CommandText = !string.IsNullOrEmpty(isFromTemplate) ? @"SELECT isnull(desc1,'') AUTO_DESC1, isnull(desc2,'') AUTO_DESC2, isnull(desc3,'') AUTO_DESC3 , isnull(desc4,'') AUTO_DESC4, isnull(desc5,'') AUTO_DESC5, isnull(desc6,'') AUTO_DESC6, isnull(desc7,'') AUTO_DESC7, isnull(desc8,'') AUTO_DESC8, isnull(desc9,'') AUTO_DESC9, isnull(desc10,'') AUTO_DESC10, isnull(desc11,'') AUTO_DESC11, isnull(desc12,'') AUTO_DESC12, isnull(desc13,'') AUTO_DESC13 , isnull(desc14,'') AUTO_DESC14, isnull(desc15,'') AUTO_DESC15, isnull(desc16,'') AUTO_DESC16, isnull(desc17,'') AUTO_DESC17, isnull(desc18,'') AUTO_DESC18 , isnull(desc19,'') AUTO_DESC19, isnull(desc20,'') AUTO_DESC20,isnull(desc21,'') AUTO_DESC21,isnull(desc22,'') AUTO_DESC22,isnull(desc23,'') AUTO_DESC23,isnull(desc24,'') AUTO_DESC24,isnull(desc25,'') AUTO_DESC25,isnull(desc26,'') AUTO_DESC26,isnull(desc27,'') AUTO_DESC27,isnull(desc28,'') AUTO_DESC28,isnull(desc29,'') AUTO_DESC29,isnull(desc30,'') AUTO_DESC30,isnull(desc31,'') AUTO_DESC31,isnull(desc32,'') AUTO_DESC32,isnull(desc33,'') AUTO_DESC33,isnull(desc34,'') AUTO_DESC34,isnull(desc35,'') AUTO_DESC35,isnull(desc36,'') AUTO_DESC36,isnull(desc37,'') AUTO_DESC37,isnull(desc38,'') AUTO_DESC38,isnull(desc39,'') AUTO_DESC39,isnull(desc40,'') AUTO_DESC40, isnull(fixedvalue1, '') AS Fixeddesc1, isnull(fixedvalue2, '') AS Fixeddesc2, isnull(fixedvalue3, '') AS Fixeddesc3, isnull(fixedvalue4, '') AS Fixeddesc4, isnull(fixedvalue5, '') AS Fixeddesc5, isnull(fixedvalue6, '') AS Fixeddesc6, isnull(fixedvalue7, '') AS Fixeddesc7, isnull(fixedvalue8, '') AS Fixeddesc8, isnull(fixedvalue9, '') AS Fixeddesc9, isnull(fixedvalue10, '') AS Fixeddesc10, isnull(fixedvalue11, '') AS Fixeddesc11, isnull(fixedvalue12, '') AS Fixeddesc12, isnull(fixedvalue13, '') AS Fixeddesc13, isnull(fixedvalue14, '') AS Fixeddesc14, isnull(fixedvalue15, '') AS Fixeddesc15, isnull(fixedvalue16, '') AS Fixeddesc16, isnull(Line_Break, '') AS desc_lineBreak FROM autodesc_template where TEMPLATENAME = @TemplateName"
                : @"SELECT isnull(desc1,'') AUTO_DESC1, isnull(desc2,'') AUTO_DESC2, isnull(desc3,'') AUTO_DESC3 , isnull(desc4,'') AUTO_DESC4, isnull(desc5,'') AUTO_DESC5, isnull(desc6,'') AUTO_DESC6, isnull(desc7,'') AUTO_DESC7, isnull(desc8,'') AUTO_DESC8, isnull(desc9,'') AUTO_DESC9, isnull(desc10,'') AUTO_DESC10, isnull(desc11,'') AUTO_DESC11, isnull(desc12,'') AUTO_DESC12, isnull(desc13,'') AUTO_DESC13 , isnull(desc14,'') AUTO_DESC14, isnull(desc15,'') AUTO_DESC15, isnull(desc16,'') AUTO_DESC16, isnull(desc17,'') AUTO_DESC17, isnull(desc18,'') AUTO_DESC18 , isnull(desc19,'') AUTO_DESC19, isnull(desc20,'') AUTO_DESC20, ISNULL(i.desc21, '') AS AUTO_DESC21, ISNULL(i.desc22, '') AS AUTO_DESC22, ISNULL(i.desc23, '') AS AUTO_DESC23, ISNULL(i.desc24, '') AS AUTO_DESC24, ISNULL(i.desc25, '') AS AUTO_DESC25, ISNULL(i.desc26, '') AS AUTO_DESC26, ISNULL(i.desc27, '') AS AUTO_DESC27,ISNULL(i.desc28, '') AS AUTO_DESC28,ISNULL(i.desc29, '') AS AUTO_DESC29,ISNULL(i.desc30, '') AS AUTO_DESC30,ISNULL(i.desc31, '') AS AUTO_DESC31,ISNULL(i.desc32, '') AS AUTO_DESC32,ISNULL(i.desc33, '') AS AUTO_DESC33,ISNULL(i.desc34, '') AS AUTO_DESC34, ISNULL(i.desc35, '') AS AUTO_DESC35,ISNULL(i.desc36, '') AS AUTO_DESC36,ISNULL(i.desc37, '') AS AUTO_DESC37,ISNULL(i.desc38, '') AS AUTO_DESC38,ISNULL(i.desc39, '') AS AUTO_DESC39,ISNULL(i.desc40, '') AS AUTO_DESC40, isnull(i.fixedvalue1, '') AS Fixeddesc1, isnull(i.fixedvalue2, '') AS Fixeddesc2, isnull(i.fixedvalue3, '') AS Fixeddesc3, isnull(i.fixedvalue4, '') AS Fixeddesc4, isnull(i.fixedvalue5, '') AS Fixeddesc5, isnull(i.fixedvalue6, '') AS Fixeddesc6, isnull(i.fixedvalue7, '') AS Fixeddesc7, isnull(i.fixedvalue8, '') AS Fixeddesc8, isnull(i.fixedvalue9, '') AS Fixeddesc9, isnull(i.fixedvalue10, '') AS Fixeddesc10, isnull(i.fixedvalue11, '') AS Fixeddesc11, isnull(i.fixedvalue12, '') AS Fixeddesc12, isnull(i.fixedvalue13, '') AS Fixeddesc13, isnull(i.fixedvalue14, '') AS Fixeddesc14, isnull(i.fixedvalue15, '') AS Fixeddesc15, isnull(i.fixedvalue16, '') AS Fixeddesc16, isnull(i.desc_lineBreak, '') AS desc_lineBreak FROM ups_ins u, ups_ins1 i";

                if (!string.IsNullOrEmpty(isFromTemplate))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TemplateName", isFromTemplate);
                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public bool UpdateUPSInsForAutoDescription(AutoDescModel autoDescModel, bool isFromTemplate = false)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                if (isFromTemplate)
                    dbCommand.CommandText = @"
                        IF EXISTS (SELECT 1 FROM autodesc_template WHERE TEMPLATENAME = @TemplateName)
                        BEGIN
                            UPDATE autodesc_template SET desc1 = @autoDesc1, desc2 = @autoDesc2, desc3 = @autoDesc3,desc4 = @autoDesc4, desc5 = @autoDesc5, desc6 = @autoDesc6,desc7 = @autoDesc7, desc8 = @autoDesc8, desc9 = @autoDesc9,desc10 = @autoDesc10, desc11 = @autoDesc11, desc12 = @autoDesc12, 
                                desc13 = @autoDesc13, desc14 = @autoDesc14, desc15 = @autoDesc15,desc16 = @autoDesc16, desc17 = @autoDesc17, desc18 = @autoDesc18, desc19 = @autoDesc19, desc20 = @autoDesc20,desc21 = @autoDesc21,desc22 = @autoDesc22,desc23 = @autoDesc23,desc24 = @autoDesc24,desc25 = @autoDesc25,desc26 = @autoDesc26, desc27 = @autoDesc27,desc28 = @autoDesc28,desc29 = @autoDesc29,desc30 = @autoDesc30,desc31 = @autoDesc31,desc32 = @autoDesc32,desc33 = @autoDesc33, desc34 = @autoDesc34,desc35 = @autoDesc35,desc36 = @autoDesc36,desc37 = @autoDesc37,desc38 = @autoDesc38,desc39 = @autoDesc39,desc40 = @autoDesc40,
                                FixedValue1 = @fixedDescription1, FixedValue2 = @fixedDescription2, FixedValue3 = @fixedDescription3, FixedValue4 = @fixedDescription4, FixedValue5 = @fixedDescription5, FixedValue6 = @fixedDescription6, FixedValue7 = @fixedDescription7, FixedValue8 = @fixedDescription8, FixedValue9 = @fixedDescription9, FixedValue10 = @fixedDescription10, FixedValue11 = @fixedDescription11, FixedValue12 = @fixedDescription12, FixedValue13 = @fixedDescription13, FixedValue14 = @fixedDescription14, FixedValue15 = @fixedDescription15, FixedValue16 = @fixedDescription16, Line_Break = @LineBreak WHERE TEMPLATENAME = @TemplateName;
                        END
                        ELSE
                        BEGIN
                            INSERT INTO autodesc_template (TEMPLATENAME, desc1, desc2, desc3, desc4, desc5, desc6, desc7, desc8, desc9, desc10, desc11, desc12, desc13, desc14, desc15, desc16, desc17, desc18, desc19, desc20,FixedValue1, FixedValue2, FixedValue3, FixedValue4,FixedValue5, FixedValue6, FixedValue7, FixedValue8,desc21,desc22,desc23,desc24,desc25,desc26,desc27,desc28,desc29,desc30,desc31,desc32,desc33,desc34,desc35,desc36,desc37,desc38,desc39,desc40,FixedValue9,FixedValue10,FixedValue11,FixedValue12,FixedValue13,FixedValue14,FixedValue15,FixedValue16,Line_Break)
                            VALUES (@TemplateName, @autoDesc1, @autoDesc2, @autoDesc3, @autoDesc4, @autoDesc5, @autoDesc6,@autoDesc7, @autoDesc8, @autoDesc9, @autoDesc10, @autoDesc11, @autoDesc12, @autoDesc13, @autoDesc14, @autoDesc15, @autoDesc16, @autoDesc17, @autoDesc18, @autoDesc19, @autoDesc20,@fixedDescription1, @fixedDescription2, @fixedDescription3, @fixedDescription4, @fixedDescription5, @fixedDescription6, @fixedDescription7, @fixedDescription8,@autoDesc21,@autoDesc22,@autoDesc23,@autoDesc24,@autoDesc25,@autoDesc26,@autoDesc27,@autoDesc28,@autoDesc29,@autoDesc30,@autoDesc31,@autoDesc32,@autoDesc33,@autoDesc34,@autoDesc35,@autoDesc36,@autoDesc37,@autoDesc38,@autoDesc39,@autoDesc40,@fixedDescription9,@fixedDescription10,@fixedDescription11,@fixedDescription12,@fixedDescription13,@fixedDescription14,@fixedDescription15,@fixedDescription16,@LineBreak);
                        END";
                else
                {
                    dbCommand.CommandText = @"UPDATE ups_ins SET desc1 = @autoDesc1, desc2 = @autoDesc2, desc3 = @autoDesc3, desc4 = @autoDesc4, desc5 = @autoDesc5, desc6 = @autoDesc6, desc7 = @autoDesc7, desc8 = @autoDesc8, desc9 = @autoDesc9, desc10 = @autoDesc10, desc11 = @autoDesc11, desc12 = @autoDesc12, desc13 = @autoDesc13, desc14 = @autoDesc14, desc15 = @autoDesc15, desc16 = @autoDesc16, desc17 = @autoDesc17, desc18 = @autoDesc18, desc19 = @autoDesc19, desc20 = @autoDesc20;UPDATE UPS_INS1 SET FixedValue1 = @fixedDescription1,FixedValue2 = @fixedDescription2,FixedValue3 = @fixedDescription3,FixedValue4 = @fixedDescription4,FixedValue5 = @fixedDescription5,FixedValue6 = @fixedDescription6,FixedValue7 = @fixedDescription7,FixedValue8 = @fixedDescription8,
                                             FixedValue9 = @fixedDescription9,FixedValue10 = @fixedDescription10,FixedValue11 = @fixedDescription11,FixedValue12 = @fixedDescription12,FixedValue13 = @fixedDescription13,FixedValue14 = @fixedDescription14,FixedValue15 = @fixedDescription15,FixedValue16 = @fixedDescription16, desc21 = @autoDesc21,desc22 = @autoDesc22,desc23 = @autoDesc23,desc24 = @autoDesc24,desc25 = @autoDesc25,desc26 = @autoDesc26, desc27 = @autoDesc27,desc28 = @autoDesc28,desc29 = @autoDesc29,desc30 = @autoDesc30,desc31 = @autoDesc31,desc32 = @autoDesc32,desc33 = @autoDesc33, desc34 = @autoDesc34,desc35 = @autoDesc35,desc36 = @autoDesc36,desc37 = @autoDesc37,desc38 = @autoDesc38,desc39 = @autoDesc39,desc40 = @autoDesc40, desc_lineBreak = @LineBreak;";

                }

                dbCommand.Parameters.AddWithValue("@autoDesc1", string.IsNullOrEmpty(autoDescModel.AUTO_DESC1) ? "" : autoDescModel.AUTO_DESC1);
                dbCommand.Parameters.AddWithValue("@autoDesc2", string.IsNullOrEmpty(autoDescModel.AUTO_DESC2) ? "" : autoDescModel.AUTO_DESC2);
                dbCommand.Parameters.AddWithValue("@autoDesc3", string.IsNullOrEmpty(autoDescModel.AUTO_DESC3) ? "" : autoDescModel.AUTO_DESC3);
                dbCommand.Parameters.AddWithValue("@autoDesc4", string.IsNullOrEmpty(autoDescModel.AUTO_DESC4) ? "" : autoDescModel.AUTO_DESC4);
                dbCommand.Parameters.AddWithValue("@autoDesc5", string.IsNullOrEmpty(autoDescModel.AUTO_DESC5) ? "" : autoDescModel.AUTO_DESC5);
                dbCommand.Parameters.AddWithValue("@autoDesc6", string.IsNullOrEmpty(autoDescModel.AUTO_DESC6) ? "" : autoDescModel.AUTO_DESC6);
                dbCommand.Parameters.AddWithValue("@autoDesc7", string.IsNullOrEmpty(autoDescModel.AUTO_DESC7) ? "" : autoDescModel.AUTO_DESC7);
                dbCommand.Parameters.AddWithValue("@autoDesc8", string.IsNullOrEmpty(autoDescModel.AUTO_DESC8) ? "" : autoDescModel.AUTO_DESC8);
                dbCommand.Parameters.AddWithValue("@autoDesc9", string.IsNullOrEmpty(autoDescModel.AUTO_DESC9) ? "" : autoDescModel.AUTO_DESC9);
                dbCommand.Parameters.AddWithValue("@autoDesc10", string.IsNullOrEmpty(autoDescModel.AUTO_DESC10) ? "" : autoDescModel.AUTO_DESC10);
                dbCommand.Parameters.AddWithValue("@autoDesc11", string.IsNullOrEmpty(autoDescModel.AUTO_DESC11) ? "" : autoDescModel.AUTO_DESC11);
                dbCommand.Parameters.AddWithValue("@autoDesc12", string.IsNullOrEmpty(autoDescModel.AUTO_DESC12) ? "" : autoDescModel.AUTO_DESC12);
                dbCommand.Parameters.AddWithValue("@autoDesc13", string.IsNullOrEmpty(autoDescModel.AUTO_DESC13) ? "" : autoDescModel.AUTO_DESC13);
                dbCommand.Parameters.AddWithValue("@autoDesc14", string.IsNullOrEmpty(autoDescModel.AUTO_DESC14) ? "" : autoDescModel.AUTO_DESC14);
                dbCommand.Parameters.AddWithValue("@autoDesc15", string.IsNullOrEmpty(autoDescModel.AUTO_DESC15) ? "" : autoDescModel.AUTO_DESC15);
                dbCommand.Parameters.AddWithValue("@autoDesc16", string.IsNullOrEmpty(autoDescModel.AUTO_DESC16) ? "" : autoDescModel.AUTO_DESC16);
                dbCommand.Parameters.AddWithValue("@autoDesc17", string.IsNullOrEmpty(autoDescModel.AUTO_DESC17) ? "" : autoDescModel.AUTO_DESC17);
                dbCommand.Parameters.AddWithValue("@autoDesc18", string.IsNullOrEmpty(autoDescModel.AUTO_DESC18) ? "" : autoDescModel.AUTO_DESC18);
                dbCommand.Parameters.AddWithValue("@autoDesc19", string.IsNullOrEmpty(autoDescModel.AUTO_DESC19) ? "" : autoDescModel.AUTO_DESC19);
                dbCommand.Parameters.AddWithValue("@autoDesc20", string.IsNullOrEmpty(autoDescModel.AUTO_DESC20) ? "" : autoDescModel.AUTO_DESC20);
                dbCommand.Parameters.AddWithValue("@autoDesc21", string.IsNullOrEmpty(autoDescModel.AUTO_DESC21) ? "" : autoDescModel.AUTO_DESC21);
                dbCommand.Parameters.AddWithValue("@autoDesc22", string.IsNullOrEmpty(autoDescModel.AUTO_DESC22) ? "" : autoDescModel.AUTO_DESC22);
                dbCommand.Parameters.AddWithValue("@autoDesc23", string.IsNullOrEmpty(autoDescModel.AUTO_DESC23) ? "" : autoDescModel.AUTO_DESC23);
                dbCommand.Parameters.AddWithValue("@autoDesc24", string.IsNullOrEmpty(autoDescModel.AUTO_DESC24) ? "" : autoDescModel.AUTO_DESC24);
                dbCommand.Parameters.AddWithValue("@autoDesc25", string.IsNullOrEmpty(autoDescModel.AUTO_DESC25) ? "" : autoDescModel.AUTO_DESC25);
                dbCommand.Parameters.AddWithValue("@autoDesc26", string.IsNullOrEmpty(autoDescModel.AUTO_DESC26) ? "" : autoDescModel.AUTO_DESC26);
                dbCommand.Parameters.AddWithValue("@autoDesc27", string.IsNullOrEmpty(autoDescModel.AUTO_DESC27) ? "" : autoDescModel.AUTO_DESC27);
                dbCommand.Parameters.AddWithValue("@autoDesc28", string.IsNullOrEmpty(autoDescModel.AUTO_DESC28) ? "" : autoDescModel.AUTO_DESC28);
                dbCommand.Parameters.AddWithValue("@autoDesc29", string.IsNullOrEmpty(autoDescModel.AUTO_DESC29) ? "" : autoDescModel.AUTO_DESC29);
                dbCommand.Parameters.AddWithValue("@autoDesc30", string.IsNullOrEmpty(autoDescModel.AUTO_DESC30) ? "" : autoDescModel.AUTO_DESC30);
                dbCommand.Parameters.AddWithValue("@autoDesc31", string.IsNullOrEmpty(autoDescModel.AUTO_DESC31) ? "" : autoDescModel.AUTO_DESC31);
                dbCommand.Parameters.AddWithValue("@autoDesc32", string.IsNullOrEmpty(autoDescModel.AUTO_DESC32) ? "" : autoDescModel.AUTO_DESC32);
                dbCommand.Parameters.AddWithValue("@autoDesc33", string.IsNullOrEmpty(autoDescModel.AUTO_DESC33) ? "" : autoDescModel.AUTO_DESC33);
                dbCommand.Parameters.AddWithValue("@autoDesc34", string.IsNullOrEmpty(autoDescModel.AUTO_DESC34) ? "" : autoDescModel.AUTO_DESC34);
                dbCommand.Parameters.AddWithValue("@autoDesc35", string.IsNullOrEmpty(autoDescModel.AUTO_DESC35) ? "" : autoDescModel.AUTO_DESC35);
                dbCommand.Parameters.AddWithValue("@autoDesc36", string.IsNullOrEmpty(autoDescModel.AUTO_DESC36) ? "" : autoDescModel.AUTO_DESC36);
                dbCommand.Parameters.AddWithValue("@autoDesc37", string.IsNullOrEmpty(autoDescModel.AUTO_DESC37) ? "" : autoDescModel.AUTO_DESC37);
                dbCommand.Parameters.AddWithValue("@autoDesc38", string.IsNullOrEmpty(autoDescModel.AUTO_DESC38) ? "" : autoDescModel.AUTO_DESC38);
                dbCommand.Parameters.AddWithValue("@autoDesc39", string.IsNullOrEmpty(autoDescModel.AUTO_DESC39) ? "" : autoDescModel.AUTO_DESC39);
                dbCommand.Parameters.AddWithValue("@autoDesc40", string.IsNullOrEmpty(autoDescModel.AUTO_DESC40) ? "" : autoDescModel.AUTO_DESC40);

                dbCommand.Parameters.AddWithValue("@fixedDescription1", string.IsNullOrEmpty(autoDescModel.Fixeddesc1) ? "" : autoDescModel.Fixeddesc1);
                dbCommand.Parameters.AddWithValue("@fixedDescription2", string.IsNullOrEmpty(autoDescModel.Fixeddesc2) ? "" : autoDescModel.Fixeddesc2);
                dbCommand.Parameters.AddWithValue("@fixedDescription3", string.IsNullOrEmpty(autoDescModel.Fixeddesc3) ? "" : autoDescModel.Fixeddesc3);
                dbCommand.Parameters.AddWithValue("@fixedDescription4", string.IsNullOrEmpty(autoDescModel.Fixeddesc4) ? "" : autoDescModel.Fixeddesc4);
                dbCommand.Parameters.AddWithValue("@fixedDescription5", string.IsNullOrEmpty(autoDescModel.Fixeddesc5) ? "" : autoDescModel.Fixeddesc5);
                dbCommand.Parameters.AddWithValue("@fixedDescription6", string.IsNullOrEmpty(autoDescModel.Fixeddesc6) ? "" : autoDescModel.Fixeddesc6);
                dbCommand.Parameters.AddWithValue("@fixedDescription7", string.IsNullOrEmpty(autoDescModel.Fixeddesc7) ? "" : autoDescModel.Fixeddesc7);
                dbCommand.Parameters.AddWithValue("@fixedDescription8", string.IsNullOrEmpty(autoDescModel.Fixeddesc8) ? "" : autoDescModel.Fixeddesc8);
                dbCommand.Parameters.AddWithValue("@fixedDescription9", string.IsNullOrEmpty(autoDescModel.Fixeddesc9) ? "" : autoDescModel.Fixeddesc9);
                dbCommand.Parameters.AddWithValue("@fixedDescription10", string.IsNullOrEmpty(autoDescModel.Fixeddesc10) ? "" : autoDescModel.Fixeddesc10);
                dbCommand.Parameters.AddWithValue("@fixedDescription11", string.IsNullOrEmpty(autoDescModel.Fixeddesc11) ? "" : autoDescModel.Fixeddesc11);
                dbCommand.Parameters.AddWithValue("@fixedDescription12", string.IsNullOrEmpty(autoDescModel.Fixeddesc12) ? "" : autoDescModel.Fixeddesc12);
                dbCommand.Parameters.AddWithValue("@fixedDescription13", string.IsNullOrEmpty(autoDescModel.Fixeddesc13) ? "" : autoDescModel.Fixeddesc13);
                dbCommand.Parameters.AddWithValue("@fixedDescription14", string.IsNullOrEmpty(autoDescModel.Fixeddesc14) ? "" : autoDescModel.Fixeddesc14);
                dbCommand.Parameters.AddWithValue("@fixedDescription15", string.IsNullOrEmpty(autoDescModel.Fixeddesc15) ? "" : autoDescModel.Fixeddesc15);
                dbCommand.Parameters.AddWithValue("@fixedDescription16", string.IsNullOrEmpty(autoDescModel.Fixeddesc16) ? "" : autoDescModel.Fixeddesc16);
                dbCommand.Parameters.AddWithValue("@TemplateName", string.IsNullOrEmpty(autoDescModel.TemplateName) ? "" : autoDescModel.TemplateName);
                dbCommand.Parameters.AddWithValue("@LineBreak", string.IsNullOrEmpty(autoDescModel.desc_lineBreak) ? "" : autoDescModel.desc_lineBreak);

                dbCommand.CommandTimeout = 5000;
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
    }

    public class TagPrinter
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public TagPrinter(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public string PrintTags(string tag1, string tag2, string tag3, string tag4,
                                       string tag5, string tag6, int printDollar = 0,
                                       int noOfTags = 1, string tag7 = "", string tag8 = "")
        {
            StringBuilder printTag = new StringBuilder();
            //_helperCommonService.GetDefaultValues();

            decimal tagPrinterLeft, tagPrinterRight, tagPrinterTop, tagPrinterCinc, tagPrinterLength;
            decimal dummyTopR;

            _helperCommonService.CheckZebraCitohTscGodex();

            _helperCommonService.SetPrinterMargins(out tagPrinterLeft, out tagPrinterRight, out tagPrinterTop,
                                      out tagPrinterCinc, out dummyTopR, false, out tagPrinterLength);

            _helperCommonService.Tag_Start(printTag);

            _helperCommonService.add_tag(printTag, tagPrinterLeft, tagPrinterTop, tag1);
            _helperCommonService.add_tag(printTag, tagPrinterLeft, tagPrinterTop + tagPrinterCinc, tag3);
            _helperCommonService.add_tag(printTag, tagPrinterLeft, tagPrinterTop + tagPrinterCinc * 2, tag5);
            _helperCommonService.add_tag(printTag, tagPrinterLeft, tagPrinterTop + tagPrinterCinc * 3, tag7);

            _helperCommonService.add_tag(printTag, tagPrinterRight, tagPrinterTop, tag2);
            _helperCommonService.add_tag(printTag, tagPrinterRight, tagPrinterTop + tagPrinterCinc, tag4);
            _helperCommonService.add_tag(printTag, tagPrinterRight, tagPrinterTop + tagPrinterCinc * 2, tag6);
            _helperCommonService.add_tag(printTag, tagPrinterRight, tagPrinterTop + tagPrinterCinc * 3, tag8);


            _helperCommonService.Tag_End(printTag);
            return _helperCommonService.WriteTag(noOfTags, printTag);
        }
    }
}
