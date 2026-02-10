// Chakri  12/31/2025  Created New Model.
// Chakri   02/05/2026 The code has been separated into the Employee model and service layers.

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
