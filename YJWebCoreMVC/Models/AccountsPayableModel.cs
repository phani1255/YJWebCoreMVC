using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace YJWebCoreMVC.Models
{
    public class AccountsPayableModel
    {

        public string BankCode { get; set; }
        public string CheckNo { get; set; }
        public IEnumerable<SelectListItem> AllBankCodes { get; set; }

        public string BillNo { get; set; }
        public string VendorCode { get; set; }

        public string store { get; set; }
        public string ref_no { get; set; }
        public string vnd_no { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        [DataType(DataType.Date)]
        public DateTime due_date { get; set; }
        public string strFormattedDate { get; set; }
        public string strFormatteddue_date { get; set; }
        public string strFormattedEnterDate { get; set; }
        public decimal inv_amount { get; set; }
        public decimal credit { get; set; }
        public decimal payment { get; set; }
        public decimal balance { get; set; }
        public string bank { get; set; }
        public string paytype { get; set; }
        public decimal bal_inv_amount { get; set; }
        public decimal Tbill { get; set; }
        public decimal Tchecks { get; set; }
        public decimal Tcredit { get; set; }
        public string CHECK_NO { get; set; }
        public string PACK_NO { get; set; }
        public string ACC { get; set; }
        public string NAME { get; set; }
        public string BANK { get; set; }
        public DateTime? CHKDT { get; set; }
        public DateTime? BILLDT { get; set; }
        public string strformattedCHKDT { get; set; }
        public string strformattedBILLDT { get; set; }
        public decimal AMOUNT { get; set; }
        public string INV_NO { get; set; }
        public string CLRD { get; set; }
        public string VND_NO { get; set; }
        public decimal DISCOUNT { get; set; }
        public decimal PaidAmt { get; set; }
        public decimal TERM { get; set; }
        public string BALANCE { get; set; }
        public string ENTER_DATE { get; set; }
        public string SFM { get; set; }
        public string ON_QB { get; set; }
        public string IS_COMISION { get; set; }
        public string Gold_Price { get; set; }
        public string gl_log { get; set; }
        public string NOTE { get; set; }
        public string pay_terms { get; set; }
        public string Shipping_Charge { get; set; }
        public string Other_Charge { get; set; }
        public string Term_Note { get; set; }
        public string Discount { get; set; }
        public string NoOfDay1 { get; set; }
        public string DiscPercent1 { get; set; }
        public string NoOfDay2 { get; set; }
        public string DiscPercent2 { get; set; }
        public string NoOfDay3 { get; set; }
        public string DiscPercent3 { get; set; }
        public string NO_OF_TERMS { get; set; }
        public string TERM_INTERVAL { get; set; }
        public string JobbagBill { get; set; }
        public string store_no { get; set; }
        public string CVSFM_NO { get; set; }
        public string AddnChargeBillno { get; set; }
        public string NotAdded { get; set; }
        public string JobbagBillReturn { get; set; }
        public string IS_SNH_VSFM { get; set; }
        public string Gold_Balance { get; set; }
        public string Gold_Wt { get; set; }
        public string IsReturnItemsForJobbag { get; set; }
        public string Insurance { get; set; }
        public string Curr_type { get; set; }
        public string Curr_rate { get; set; }
        //public string Amount_Curr { get; set; }
        //public string Balance_Curr { get; set; }
        //public string Shipping_Charge_Curr { get; set; }
        //public string Other_Charge_Curr { get; set; }
        //public string Discount_Curr { get; set; }
        //public string Insurance_Curr { get; set; }
        public string STYLE { get; set; }
        public string VND_STYLE { get; set; }
        //public string PCS { get; set; }
        // public string WEIGHT { get; set; }
        //public string COST { get; set; }
        public string BY_WT { get; set; }
        public string DESC { get; set; }
        public string JobNote { get; set; }
        public string StyleImage { get; set; }
        //public string PRICE { get; set; }
        public string Order_no { get; set; }
        public string Jobbagno { get; set; }
        public IEnumerable<SelectListItem> VenderTypes { get; set; }
        public IEnumerable<SelectListItem> AllStores { get; set; }
        public IEnumerable<AccountsPayableModel> getBestSellerCategoryReport { get; set; }
        public List<AccountsPayableModel> lstVendorStatement { get; set; } = new List<AccountsPayableModel>();

        public decimal PCS { get; set; }
        public decimal WEIGHT { get; set; }
        public decimal COST { get; set; }
        public decimal PRICE { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal Amount_Curr { get; set; }
        public decimal Balance_Curr { get; set; }
        public decimal Shipping_Charge_Curr { get; set; }
        public decimal Other_Charge_Curr { get; set; }
        public decimal Discount_Curr { get; set; }
        public decimal Insurance_Curr { get; set; }


        public decimal TotalCost { get; set; }
        public decimal ShippingCharges { get; set; }
        public decimal OtherAmount { get; set; }
        public decimal InsuranceAmount { get; set; }
        public decimal TotalGoldWeight { get; set; }
        public decimal BalanceAmount { get; set; }
        public string GLCode { get; set; }
        public IEnumerable<SelectListItem> AllGLCodes { get; set; }
        public List<PayBillsByCCViewModel> BillList { get; set; } = new List<PayBillsByCCViewModel>();
        public List<PayBillsByCCViewModel> GLTotalList { get; set; }

        public List<VendorDetails> lstVendorDetails { get; set; } = new List<VendorDetails>();
        public List<GLLogDetails> lstGLLogDetails { get; set; } = new List<GLLogDetails>();
        public List<CheckDetails> lstCheckDetails { get; set; } = new List<CheckDetails>();
        public List<CCRDDetails> lstCCRDDetails { get; set; } = new List<CCRDDetails>();
        public List<CreditDetails> lstCreditDetails { get; set; } = new List<CreditDetails>();
        public List<CreditBillDetails> lstCreditBillDetails { get; set; } = new List<CreditBillDetails>();
        public List<CreditBillNoDetails> lstCreditBillNoDetails { get; set; } = new List<CreditBillNoDetails>();


    }


    public class CreditBillDetails
    {
        public string RTV_PAY { get; set; }
        public string PAY_NO { get; set; }
        public string INV_NO { get; set; }
        public decimal AMOUNT { get; set; }
        public string NOTE { get; set; }
    }
    public class CreditBillNoDetails
    {
        public string INV_NO { get; set; }
        public decimal AMOUNT { get; set; }
        public string NOTE { get; set; }

    }

    public class VendorDetails
    {
        public string INV_NO { get; set; }
        public string ACC { get; set; }
        public decimal AMOUNT { get; set; }
        public string VND_NO { get; set; }
        public decimal TERM { get; set; }
        public string BALANCE { get; set; }
        public string SFM { get; set; }
        public string ON_QB { get; set; }
        public string IS_COMISION { get; set; }
        public string Gold_Price { get; set; }
        public string gl_log { get; set; }
        public string NOTE { get; set; }
        public string Shipping_Charge { get; set; }
        public string pay_terms { get; set; }
        public string Other_Charge { get; set; }
        public string Term_Note { get; set; }
        public string Discount { get; set; }
        public string NoOfDay1 { get; set; }
        public string DiscPercent1 { get; set; }
        public string NoOfDay2 { get; set; }
        public string DiscPercent2 { get; set; }
        public string NoOfDay3 { get; set; }
        public string DiscPercent3 { get; set; }
        public string NO_OF_TERMS { get; set; }
        public string TERM_INTERVAL { get; set; }
        public string JobbagBill { get; set; }
        public string store_no { get; set; }
        public string CVSFM_NO { get; set; }
        public string AddnChargeBillno { get; set; }
        public string NotAdded { get; set; }
        public string JobbagBillReturn { get; set; }
        public string IS_SNH_VSFM { get; set; }
        public string Gold_Balance { get; set; }
        public string Gold_Wt { get; set; }
        public string IsReturnItemsForJobbag { get; set; }
        public string Insurance { get; set; }
        public string Curr_type { get; set; }
        public string Curr_rate { get; set; }
        public decimal Amount_Curr { get; set; }
        public decimal Balance_Curr { get; set; }
        public decimal Shipping_Charge_Curr { get; set; }
        public decimal Other_Charge_Curr { get; set; }
        public decimal Discount_Curr { get; set; }
        public decimal Insurance_Curr { get; set; }
        public decimal PCS { get; set; }
        public decimal WEIGHT { get; set; }
        public decimal COST { get; set; }
        public decimal PRICE { get; set; }
        public decimal TotalAmount { get; set; }
        public string STYLE { get; set; }
        public string VND_STYLE { get; set; }
        public string BY_WT { get; set; }
        public string DESC { get; set; }
        public string JobNote { get; set; }
        public string StyleImage { get; set; }
        public string Order_no { get; set; }
        public string Jobbagno { get; set; }

        public DateTime DATE { get; set; }
        public DateTime ENTER_DATE { get; set; }
        public DateTime DUE_DATE { get; set; }
    }
    public class GLLogDetails
    {
        public string INV_NO { get; set; }
        public string GL_CODE { get; set; }
        public string DESC { get; set; }
        public string DEPT { get; set; }
        public decimal AMOUNT { get; set; }
        public string Not_Editable { get; set; }
        public string gl_log { get; set; }
    }
    public class CheckDetails
    {
        public string CHECK_NO { get; set; }
        public string BANK { get; set; }
        public decimal AMOUNT { get; set; }
        public DateTime DATE { get; set; }
        public string ACC { get; set; }
        public string PACK { get; set; }
        public string GL_CODE { get; set; }
        public string TRANSACT { get; set; }
        public string NAME { get; set; }
        public DateTime ENTER_DATE { get; set; }
        public string NOTE { get; set; }
        public string ON_QB { get; set; }
        public string GLPOST_LOG { get; set; }
        public string CLRD { get; set; }
        public string STORE_NO { get; set; }
    }
    public class CCRDDetails
    {
        public string LOG_NO { get; set; }
        public string CC_VENDOR { get; set; }
        public DateTime DATE { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal DISCOUNT { get; set; }
    }
    public class CreditDetails
    {
        public string Credit { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string PACK_NO { get; set; }

        public string Inv_No { get; set; }
        public string ACC { get; set; }
        public string name { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string city1 { get; set; }
    }
    public class PayBillsByCCViewModel
    {
        public string BillNo { get; set; }
        public string ACC { get; set; }
        public string VndRef { get; set; }
        public bool SELECT { get; set; } = false;
        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public decimal Balance { get; set; }

        public decimal Payment { get; set; }

        public decimal Amount { get; set; }
        public decimal Discount { get; set; }

        public string Type { get; set; }

        public bool PayFull { get; set; }
        public string VendorName { get; set; }

        public string GlACC { get; set; }
        public decimal GLAmount { get; set; }

    }
    public class InvoiceViewModel
    {
        public string INV_NO { get; set; }
        public string ACC { get; set; }
        public decimal Amount { get; set; }
    }
}
