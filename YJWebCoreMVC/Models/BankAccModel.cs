namespace YJWebCoreMVC.Models
{
    public class BankAccModel
    {

    }

    public class BankACCModel
    {
        public string CODE { get; set; }
        public string DESC { get; set; }
        public string LAST_INV { get; set; }
        public bool IS_QB { get; set; }
        public string QB_NAME { get; set; }
        public string BANK_ACC_NUM { get; set; }
        public string BANK_ROUT_NUM { get; set; }
        public DateTime? ACCT_OPEN_DATE { get; set; }
        public string BANK_NAME { get; set; }
        public string BANK_ADDR1 { get; set; }
        public string BANK_ADDR2 { get; set; }
        public string BANK_ADDR3 { get; set; }
        public string BANK_ADDR4 { get; set; }
        public string USER_NAME { get; set; }
        public string USER_ADDR1 { get; set; }
        public string USER_ADDR2 { get; set; }
        public string USER_ADDR3 { get; set; }
        public string USER_ADDR4 { get; set; }
        public string GL_CODE { get; set; }
        public string GL_DEPT { get; set; }
        public string Storename { get; set; }
        public string LAST_INV_DATE { get; set; }
    }
    public class AddDepositViewModel
    {
        public string DepositNo { get; set; }
        public string BankCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DepositDate { get; set; }
        public string StoreCode { get; set; }
        public List<DepositItem> Items { get; set; }
        public decimal CashDeposit { get; set; }
        public bool IsCreditCard { get; set; }
        public List<DepositItemForCreditCards> CreditCardItems { get; set; }
    }
    public class DepositItem
    {
        public string ReceiptNo { get; set; }
        public string Date { get; set; }
        public string CustomerCode { get; set; }
        public string StoreNo { get; set; }
        public string PaymentType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool Deposited { get; set; }
        public string RTV_Pay { get; set; }
    }
    public class AddDepositEditViewModel
    {
        public string DepositNo { get; set; }
        public string BankCode { get; set; }
        public string DepositDate { get; set; }
        public decimal CashDeposit { get; set; }
        public List<DepositItemForEdit> EditItems { get; set; }
    }
    public class DepositItemForEdit
    {
        public string ReceiptNo { get; set; }
        public string Date { get; set; }
        public string CustomerCode { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool Deposited { get; set; }
        public string RTV_PAY { get; set; }
        public decimal Amount1 { get; set; }
    }
    public class DepositItemForCreditCards
    {
        public string ReceiptNo { get; set; }
        public string Date { get; set; }
        public string CustomerCode { get; set; }
        public string StoreNo { get; set; }
        public string PaymentType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal BankFee { get; set; }
        public decimal TransFee { get; set; }
        public decimal NetAmount { get; set; }
        public bool Deposited { get; set; }
        public string RTV_Pay { get; set; }
    }
    public class bankTransactionModel
    {
        public string INV_NO { get; set; }
        public DateTime DATE { get; set; }
        public string DESC { get; set; }
        public string DEB_CRD { get; set; }
        public decimal AMOUNT { get; set; }
        public string BANK { get; set; }
        public DateTime ENTER_DATE { get; set; }
        public string CHK_NO { get; set; }
        public string GL_CODE { get; set; }
        public string CLRD { get; set; }
        public Decimal DEPNO { get; set; }
        public bool? ON_QB { get; set; }
        public string LOGGEDUSER { get; set; }
        public bool? ISEDIT { get; set; }
        public bool Isreconflag { get; set; }
        public string MODE { get; set; }
        public string GL_LOG { get; set; }
    }
}
