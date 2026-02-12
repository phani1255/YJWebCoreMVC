using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace YJWebCoreMVC.Models
{
    public class SalesLayAwaysModel
    {
        public List<Dictionary<string, object>> DataList { get; set; }
        public DataRow InvoiceRow { get; set; }
        public DataRow CustomerRow { get; set; }
        public DataRow UpsInsRow { get; set; }
        public DataTable Dtrepcostdata { get; set; }
        public DataTable DtStores { get; set; }
        public DataTable DtSubReport2 { get; set; }
        public DataTable DrMasterDetail2 { get; set; }
        public DataRow DrMasterDetail { get; set; }
        public IEnumerable<SelectListItem> CustomerCodes { get; set; }
    }

    public class NoteModel
    {
        public int? ID { get; set; }
        public string ACC { get; set; }
        public string WHO { get; set; }
        public DateTime DTIME { get; set; }
        public string NOTE { get; set; }
        public string TYPE { get; set; }
        public DateTime? followup { get; set; }
        public bool completed { get; set; }
        public string time { get; set; }
        public bool reminder { get; set; }
    }
}
