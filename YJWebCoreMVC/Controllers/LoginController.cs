using Microsoft.AspNetCore.Mvc;
using YJWebCoreMVC.Services;
using YJWebCoreMVC.Models;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static YJWebCoreMVC.Services.HelperCommonService;
using ISession = Microsoft.AspNetCore.Http.ISession;

namespace YJWebCoreMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly UsersService _usersService;
        private readonly HelperService _helperService;
        private readonly GlobalSettingsService _globalSettingsService;

        public LoginController(ConnectionProvider connectionProvider, UsersService usersService, HelperService helperService, GlobalSettingsService globalSettingsService)
        {
            _connectionProvider = connectionProvider;
            _usersService = usersService;
            _helperService = helperService;
            _globalSettingsService = globalSettingsService;
        }
        public IActionResult Index()
        {
            UsersModel objModel = new UsersModel();
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "-- Select Store --", Value = "" });
            objModel.StoreCodes = salesmanList;
            return View(objModel);
        }


        [HttpPost]
        public ActionResult Authorize(YJWebCoreMVC.Models.UsersModel userModel)
        {
            //UsersModel objModel = new UsersModel();
            DataTable dtCheckUser = _usersService.CheckUserCredential(userModel.CODE, userModel.PASSWD);
            //var globalSettings = _globalSettingsService.GetGlobalSettings();
            if (dtCheckUser == null)
            {
                return View("Index", userModel);
            }
            else
            {
                HttpContext.Session.SetString("UserId", userModel.CODE.ToString());
                HttpContext.Session.SetString("STORE_CODE", dtCheckUser.Rows[0]["STORE_CODE"].ToString());
                GetDefaultValues();
                //globalSettings.StoreCode = dtCheckUser.Rows[0]["STORE_CODE"].ToString();
                DataTable dtGetUpsIns = _usersService.GetUpsIns();
                foreach (DataRow row in dtGetUpsIns.Rows)
                {
                    HttpContext.Session.SetString("DecimalsInPrices", row["PCS_DECIMAL"].ToString());
                    HttpContext.Session.SetString("COMPANYNAME", row["COMPANYNAME"].ToString());
                    HttpContext.Session.SetString("MICR", row["MICR"].ToString());
                }

                DataTable dtGetUpsIns1 = _usersService.GetUpsIns1();
                foreach (DataRow row in dtGetUpsIns1.Rows)
                {
                    HttpContext.Session.SetString("WebImagesUrl", row["Web_Images_Path"].ToString());
                }

                HttpContext.Session.SetString("StoreCodeInUse", userModel.store_code);

                DataTable dtPassFileInfo = _usersService.GetPassfile(userModel.CODE);

                foreach (DataRow passfile in dtPassFileInfo.Rows)
                {
                    HttpContext.Session.SetString("DefaultSaleman", passfile["CODE"].ToString());
                }
                //DecimalsInPieces
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout(YJWebCoreMVC.Models.UsersModel userModel)
        {
            HttpContext.Session.SetString("UserId", "");
            HttpContext.Session.SetString("DecimalsInPrices", "");
            return RedirectToAction("Index", "Login");

        }

        public string GetUserStoreDetails(string username)
        {
            var data = _usersService.GetUserStoreDetails(username);
            return JsonConvert.SerializeObject(data);
        }

        public void GetDefaultValues()
        {

            try
            {
                _helperService.HelperCommon.isNoTaxRepair = _helperService.HelperCommon.CheckModuleEnabled(Modules.NoTaxRepair);
                _helperService.HelperCommon.isJMcare = _helperService.HelperCommon.CheckModuleEnabled(Modules.JMcare);
                DataTable dataTable = _helperService.HelperCommon.GetSqlData("select * from ups_ins");
                DataTable dataTable1 = _helperService.HelperCommon.GetSqlData("select * from ups_ins1 with (nolock)");
                if (_helperService.HelperCommon.DataTableOK(dataTable))
                {
                    string CompanyName = _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "COMPANYNAME");
                    HttpContext.Session.SetString("PrintMode", _helperService.HelperCommon.PrintMode = _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "print_mode"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CompanyName", Convert.ToString(_helperService.HelperCommon.CheckForDBNullUPS(dataTable, "COMPANYNAME")));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CompanyAddr1", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "COMPANY_ADDR1").ToString());
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CompanyAddr2", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "COMPANY_ADDR2"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CompanyTel", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "COMPANY_TEL"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CompanyCity", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CompanyCity"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CompanyZip", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CompanyZip"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CompanyState", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CompanyState"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CompanyEmail", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "Company_email"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "ImagesPath", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "ImagesPath"));
                    //Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "SignBelow", _helperService.HelperCommon.DataTableOK(dataTable1) ? _helperService.HelperCommon.DecimalCheckForDBNull(dataTable1.Rows[0]["sign_below"]) : 0m);

                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustAttr1", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustAttr1"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustAttr2", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustAttr2"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustAttr3", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustAttr3"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustAttr4", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustAttr4"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustAttr5", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustAttr5"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustAttr6", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustAttr6"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustAttr7", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustAttr7"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustAttr8", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustAttr8"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustMultiAttr1", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustMultiAttr1"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustMultiAttr2", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustMultiAttr2"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustMultiAttr3", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustMultiAttr3"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustCheckAttr1", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustCheck1"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustCheckAttr2", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustCheck2"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustCheckAttr3", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustCheck3"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustCheckAttr4", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustCheck4"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustCheckAttr5", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustCheck5"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustCheckAttr6", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustCheck6"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustCheckAttr7", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustCheck7"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "CustCheckAttr8", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "CustCheck8"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "stk_prefix", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "stk_prefix"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleCheck1", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "StyleCheck1"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleCheck2", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "StyleCheck2"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleCheck3", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "StyleCheck3"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleCheck4", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "StyleCheck4"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleCheck5", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "StyleCheck5"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleCheck6", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "StyleCheck6"));

                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField20", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField20"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField21", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField21"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField22", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField22"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField23", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField23"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField24", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField24"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField25", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField25"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField26", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField26"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField27", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField27"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField28", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField28"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField29", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField29"));

                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField1", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField1"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField2", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField2"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField3", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField3"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField4", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField4"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField5", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField5"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField6", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField6"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField7", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField7"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "StyleField8", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "StyleField8"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "InvoiceNote", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "InvoiceNote"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "MemoNote", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "MemoNote"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "RepairNote", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "Repair_Note"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "RepairInvoiceNote", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "Repair_Invoice_Note"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "RepairDisclaimer", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "REPAIR_DISCLAIMER"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "warrentynote", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "Warranty_note"));

                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "GL_AP_SHIPPING", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "gl_ap_shipping"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "GL_VENDORDISCOUNT", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "gl_vendor_credit"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "GL_AP_Insurance", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "gl_ap_insurance"));
                    Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "GL_ASSET", _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "ASSET_GL"));
                }

                Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "ShopifyAPI", _helperService.HelperCommon.ShopifyAPI = _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "ShopifyAPI"));
                Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "ShopifyPwd", _helperService.HelperCommon.ShopifyPwd = _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "ShopifyPwd"));
                Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "ShopifySecret", _helperService.HelperCommon.ShopifySecret = _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "ShopifySecret"));
                Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "ShopifyURL", _helperService.HelperCommon.ShopifyURL = _helperService.HelperCommon.CheckForDBNullUPS(dataTable, "ShopifyURL"));

                Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "NegativeInv", _helperService.HelperCommon.CheckForDBNull(dataTable.Rows[0]["negativeinv"], typeof(bool).FullName));
                Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "NoChangeBefore", _helperService.HelperCommon.CheckForDBNullUPS(dataTable1, "NoChangeBefore"));
                //Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "iS_emCity", _helperService.HelperCommon.iS_emCity = _helperService.HelperCommon.CheckForDBNull(CompanyName).ToUpper().Contains("EMERALD"));
                //Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "is_Glenn", _helperService.HelperCommon.!string.IsNullOrEmpty(CompanyName) && CompanyName.IndexOf("glenn", StringComparison.OrdinalIgnoreCase) >= 0);

                //Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "is_RK", _helperService.HelperCommon.CheckForDBNull(CompanyName).ToUpper().Contains("RK -B JEW") || _helperService.HelperCommon.CheckForDBNull(CompanyName).ToUpper().Contains("RK JEW"));
                //Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "is_mara", _helperService.HelperCommon.!string.IsNullOrEmpty(CompanyName) && CompanyName.IndexOf("mara", StringComparison.OrdinalIgnoreCase) >= 0);
                //Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "iS_Shiva", _helperService.HelperCommon.CheckForDBNull(CompanyName).ToUpper().Contains("SHIVA"));
                //Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "is_RFID", _helperService.HelperCommon.CheckModuleEnabled(HelperCommonService.Modules.RFID));
                //Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "PriceBasedOnGold", _helperService.HelperCommon.CheckModuleEnabled(Modules.PriceBasedOnGold));
                //Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "is_StyleItem", _helperService.HelperCommon.CheckModuleEnabled(HelperCommonService.Modules.Style_Item).ToString());
                //Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "styleWeight", _helperService.HelperCommon.CheckModuleEnabled(Modules.StylesWeight));
                //Microsoft.AspNetCore.Http.SessionExtensions.SetString(HttpContext.Session, "is_Lefk", _helperService.HelperCommon.CheckForDBNull(CompanyName).ToUpper().Contains("LEFKO"));


            }
            catch { }
        }

    }
}
