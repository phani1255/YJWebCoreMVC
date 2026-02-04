using Microsoft.AspNetCore.Mvc;
using System.Data;
using YJWebCoreMVC.Services;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Controllers
{
    public class StylesController : Controller
    {

        private readonly StylesService _stylesService;
        private readonly HelperService _helperService;

        public StylesController(StylesService stylesService, HelperService helperService)
        {
            _stylesService = stylesService;
            _helperService = helperService;
        }

        private bool wristFormat = false, doSeparateSNFromModel = false, NisFrmInv = false, PrintCost, IsQuickSearch = false;
        bool calledFromBillForm = false;
        bool basedOnPartNo = false;

        public DataTable dtMoldHistory;


        public IActionResult PrintStoreInventory(bool DisableLayout = false)
        {
            StylesModel objModel = new StylesModel();
            objModel.CustomerCodes = _helperService.HelperCommon.GetAllCustomerCodes();
            objModel.CategoryTypes = _helperService.HelperCommon.GetAllCategories();
            objModel.SubCategoryTypes = _helperService.HelperCommon.GetAllSubCategories();
            objModel.BrandTypes = _helperService.HelperCommon.GetAllBrands();
            objModel.MetalTypes = _helperService.HelperCommon.GetAllMetals();
            objModel.VendorTypes = _helperService.HelperCommon.GetAllVendors();
            objModel.AllStores = _helperService.HelperCommon.GetAllStores();
            objModel.AllGroups = _helperService.HelperCommon.GetAllGroups();
            objModel.AllItemTypes = _helperService.HelperCommon.GetAllItemTypes();
            objModel.AllSubBrands = _helperService.HelperCommon.GetAllSubBrands();
            objModel.AllCenterStoneColor = _helperService.HelperCommon.GetAllCenterStoneColor();
            objModel.AllCenterStoneClarity = _helperService.HelperCommon.GetAllCenterStoneClarity();
            objModel.AllCenterStoneShapes = _helperService.HelperCommon.GetAllCenterStoneShapes();
            objModel.AllCenterTypes = _helperService.HelperCommon.GetAllCenterTypes();
            objModel.AllGLClasses = _helperService.HelperCommon.GetAllGLClasses();
            objModel.AllCenterStypes = _helperService.HelperCommon.GetAllCenterStypes();

            ViewBag.is_DiamondDealer = _helperService.HelperCommon.is_DiamondDealer;
            ViewBag.SingleStore = _helperService.HelperCommon.CheckModuleEnabled(HelperCommonService.Modules.SingleStore);
            ViewBag.NoMemo = _helperService.HelperCommon.CheckModuleEnabled(HelperCommonService.Modules.NoMemo);
            ViewBag.is_Moi = _helperService.HelperCommon.is_Moi;
            ViewBag.is_Glenn = _helperService.HelperCommon.is_Glenn;
            ViewBag.iSFischer = _helperService.HelperCommon.iSFischer;
            ViewBag.iS_Anar = _helperService.HelperCommon.iS_Anar;
            ViewBag.is_AlexH = _helperService.HelperCommon.is_AlexH;
            ViewBag.is_WatchKing = _helperService.HelperCommon.is_WatchKing;
            ViewBag.is_WatchDealer = _helperService.HelperCommon.is_WatchDealer;
            ViewBag.DisableLayout = DisableLayout;

            return View(objModel);
        }

    }
}
