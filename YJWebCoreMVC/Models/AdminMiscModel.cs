// Neetha    02/06/2025 Migrated to MVC core. 

namespace YJWebCoreMVC.Models
{
    public class AdminMiscModel
    {
        public string Discount { get; set; }

        public decimal percentage { get; set; }

    }

    public class CharityModel
    {
        public string charity { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
    }
}
