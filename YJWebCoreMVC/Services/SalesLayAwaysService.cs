//-- Neetha    05/28/2025 Createed New Model.
//-- Manoj     06/11/2025 Added GetMessages method to fetch messages from the database.
//-- Manoj     06/17/2025 Added CustomerCodes property.
//-- Manoj     06/19/2025 Added GettimeSaver Method
//
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class SalesLayAwaysService
    {
        private readonly HelperCommonService _helperCommonService;

        public SalesLayAwaysService(HelperCommonService helperCommonService)
        {
            _helperCommonService = helperCommonService;
        }

        public DataTable GetMessages()
        {
            return _helperCommonService.GetSqlData("Select NAME,Message from messages");
        }

        public DataTable GettimeSaver()
        {
            return _helperCommonService.GetSqlData("SELECT ltimer,* FROM UPS_INS");
        }
    }

}
