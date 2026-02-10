/*
 *  Created By Phanindra on 16-Oct-2024
 *  Phanindra 10/21/2024 Added method GetStyleHistory
 *  Phanindra 10/29/2024 Added GetChecknoWithAccinPayTm
 *  Phanindra 11/04/2024 Added GetCustomerNameByCode
 *  Phanindra 11/20/2024 Added GetPotCustomerNameByCode , GetStateCityByZip
 *  Phanindra 07/15/2025 Added GetCustomerByCode
 */
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using YJWebCoreMVC.Filters;
using YJWebCoreMVC.Services;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Controllers
{
    public class CommonController : Controller
    {

        private readonly HelperCommonService _helperCommonService;
        private readonly SalesPaymentsCreditsService _salesPaymentsCreditsService;
        private readonly GlobalSettingsService _globalSettingsService;
        private readonly CommonService _commonService;
        public CommonController(HelperCommonService helperCommonService, SalesPaymentsCreditsService salesPaymentsCreditsService, GlobalSettingsService globalSettingsService, CommonService CommonService)
        {
            _helperCommonService = helperCommonService;
            _salesPaymentsCreditsService = salesPaymentsCreditsService;
            _globalSettingsService = globalSettingsService;
            _commonService = CommonService;

        }
        public ActionResult SearchStyle()
        {
            CommonModel objModel = new CommonModel();

            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            objModel.CategoryTypes = _helperCommonService.GetAllCategories();
            objModel.SubCategoryTypes = _helperCommonService.GetAllSubCategories();
            objModel.BrandTypes = _helperCommonService.GetAllBrands();
            objModel.MetalTypes = _helperCommonService.GetAllMetals();
            objModel.VenderTypes = _helperCommonService.GetAllVendors();
            objModel.AllStores = _helperCommonService.GetAllStores();
            objModel.AllGroups = _helperCommonService.GetAllGroups();
            objModel.AllItemTypes = _helperCommonService.GetAllItemTypes();
            objModel.AllSubBrands = _helperCommonService.GetAllSubBrands();
            objModel.AllCenterStoneColor = _helperCommonService.GetAllCenterStoneColor();
            objModel.AllCenterStoneClarity = _helperCommonService.GetAllCenterStoneClarity();
            objModel.AllCenterStoneShapes = _helperCommonService.GetAllCenterStoneShapes();
            objModel.AllCenterTypes = _helperCommonService.GetAllCenterTypes();
            objModel.AllGLClasses = _helperCommonService.GetAllGLClasses();
            return View(objModel);
        }

        public string GetSearchStyleDetails(InventoryModel crmodel, StylesAttribute modStylesAttribute, string cstore, bool ignorein_Shop = false, bool separateSNFromModel = false, bool IsprintLongdesc = false)
        {
            var data = _commonService.GetStylesSearchData(crmodel, modStylesAttribute, cstore, ignorein_Shop, separateSNFromModel, IsprintLongdesc);
            return JsonConvert.SerializeObject(data);
        }

        public string GetStyleHistory(string style)
        {
            var data = _commonService.GetStyleHistoryDetails(style);
            return JsonConvert.SerializeObject(data);
        }

        //-- Phanindra : 29-Oct-2024
        public string GetChecknoWithAccinPayTm(string reciptno = "", string Acc = "")
        {
            var data = _helperCommonService.GetChecknoWithAccinPayTm(reciptno, Acc);
            return JsonConvert.SerializeObject(data);
        }

        public bool CheckReceiptExistOrNot(string RecNo, string RTV_PAY)
        {
            var data = _helperCommonService.CheckReceiptExistOrNot(RecNo, RTV_PAY);
            return data;
        }

        public string GetCustomerNameByCode(string acc)
        {
            string name = _commonService.GetCustomerNameByCode(acc);

            return name;
        }

        public string GetPotCustomerNameByCode(string acc)
        {
            string name = _commonService.GetPotCustomerNameByCode(acc);

            return name;
        }

        public string GetStateCityByZip(string zip)
        {
            var data = _helperCommonService.GetStateCityByZip(zip);
            return JsonConvert.SerializeObject(data);
        }

        public string GetCustomerByCode(string custCode)
        {
            DataTable dtcustData = _helperCommonService.getCustomerData(custCode);
            return JsonConvert.SerializeObject(dtcustData);
        }

    }
}