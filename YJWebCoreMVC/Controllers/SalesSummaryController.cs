using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YJWebCoreMVC.Models;
using YJWebCoreMVC.Services;

namespace YJWebCoreMVC.Controllers
{
    public class SalesSummaryController : Controller
    {

        private readonly SalesSummaryService _salesSummaryService;

        public SalesSummaryController(SalesSummaryService salesSummaryService)
        {
            _salesSummaryService = salesSummaryService;
        }
        public IActionResult Index()
        {
            //SalesSummaryModel objModel = new SalesSummaryModel();
            string DateFrom = "";
            string DateTo = "";
            bool iSPartial = false;
            bool bypickdate = false;
            int type = 1;
            var dtsales = _salesSummaryService.GetSales(DateFrom, DateTo, iSPartial, bypickdate, type);

            return View("SalesSummary", dtsales);
        }

        public string getSalesSummaryDetails(string fdate, string tdate, bool partialSpeckDate, bool bypickdate, int type)
        {
            //SalesSummaryModel objModel = new SalesSummaryModel();
            var data = _salesSummaryService.GetSales(fdate, tdate, partialSpeckDate, bypickdate, type);
            return JsonConvert.SerializeObject(data);
        }
    }
}
