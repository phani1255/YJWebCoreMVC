namespace YJWebCoreMVC.Models
{
    public class StoreModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Addr3 { get; set; }
        public string Addr4 { get; set; }
        public string Tel { get; set; }
        public decimal? SalesTax { get; set; }
        public bool? Inactive { get; set; }
        public string Dept { get; set; }
        public string Ccmid { get; set; }
        public string Cchsn { get; set; }
        public string Ccusername { get; set; }
        public string Ccpasswd { get; set; }
        public string BankAcc { get; set; }
        public string SqLocation { get; set; }
        public string SqDeviceid { get; set; }
        public string FeedbackLink { get; set; }
        public byte[] StoreLogo { get; set; }
        public string InvoiceNote { get; set; }
        public string MemoNote { get; set; }
        public string InvoiceSmstext { get; set; }
        public string RepairSmstext { get; set; }
        public string ScrapBank { get; set; }
        public string DepositBank { get; set; }
        public bool? Notext { get; set; }
        public string Airport { get; set; }
        public string LogoStore { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int? LogoId { get; set; }
    }
}
