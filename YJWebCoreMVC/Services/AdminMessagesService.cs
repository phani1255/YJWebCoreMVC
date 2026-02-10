// Neetha    02/06/2025 Created new file. 

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class AdminMessagesService
    {
        private readonly ConnectionProvider _connectionProvider;
     
        public AdminMessagesService(ConnectionProvider connectionProvider)
        {           
            _connectionProvider = connectionProvider;            
        }

        public bool SaveInvoiceMemoNotes(AdminMessagesModel model)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("InvoiceMemoNoteDetails", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@invoicenote", model.InvoiceNote ?? "");
                dbCommand.Parameters.AddWithValue("@memonote", model.MemoNote ?? "");
                dbCommand.Parameters.AddWithValue("@repairnote", model.RepairNote ?? "");
                dbCommand.Parameters.AddWithValue("@repairinvoicenote", model.RepairInvoiceNote ?? "");
                dbCommand.Parameters.AddWithValue("@repairdisclaimer", model.RepairDisclaimer ?? "");
                dbCommand.Parameters.AddWithValue("@Warrentynote", model.WarrentyNote ?? "");
                dbCommand.Parameters.AddWithValue("@PrintLayawayNote", model.PrintLayawayNote ?? "");
                dbCommand.Parameters.AddWithValue("@ReturnPolicy", model.ReturnPolicy ?? "");
                dbCommand.Parameters.AddWithValue("@EspNote", model.EspNote ?? "");
                connection.Open();
                return dbCommand.ExecuteNonQuery() > 0;
            }
        }

    }
}
