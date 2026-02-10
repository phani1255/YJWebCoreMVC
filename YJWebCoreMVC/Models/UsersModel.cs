/*
 * Phanindra 09/13/2024 Added GetUpsIns1 function to get WebImagesUrl to show style images
 * Phanindra 10/02/2024 Added Companyname to the session to show or hide menu items in getupsins function
 * Phanindra 11/06/2024 Added GetUserStoreDetails for login page
 * Phanindra 01/10/2025 Added MICR column for report related condition
 * Phanindra 09/01/2025 Added getPassfile method.
 * Phanindra 09/17/2025 replaced WebImagesUrl with Web_Images_Path in getUpsIns1 method
 */

using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{


    public class UsersModel
    {


        public string NAME { get; set; }
        public Nullable<decimal> LEVEL { get; set; }
        public string CODE { get; set; }
        public string PASSWD { get; set; }
        public byte[] PREVILAGE { get; set; }
        public string favorites { get; set; }
        public Nullable<decimal> TAGPRINTERNO { get; set; }
        public Nullable<bool> UseSSL { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SMTPServer { get; set; }
        public Nullable<decimal> SMTPPort { get; set; }
        public bool inactive { get; set; }
        public string notes { get; set; }
        public string displayname { get; set; }
        public bool oauth2 { get; set; }
        public string email_signature { get; set; }
        public int pcversion { get; set; }
        public bool feedminimized { get; set; }
        public string ROLE { get; set; }
        public System.DateTime last_aging { get; set; }
        public string TWILIOFROMNUMBER { get; set; }
        public string tagprinter { get; set; }
        public string store_code { get; set; }
        public IEnumerable<SelectListItem> StoreCodes { get; set; }

    }
}
