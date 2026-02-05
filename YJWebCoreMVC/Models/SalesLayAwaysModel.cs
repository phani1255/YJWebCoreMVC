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
}
