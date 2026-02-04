namespace YJWebCoreMVC.Models
{
    public class EmailSettingsModel
    {

        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string SmtpServer { get; set; }
        public bool UseSsl { get; set; }
        public bool UseOAuth2 { get; set; }
        public string EmailSignature { get; set; }
        public string UserName { get; set; }

        public bool UsePictureSignature { get; set; }
        public byte[] SignatureImage { get; set; }
        public byte[] SignatureImageBytes { get; set; }

    }
}
