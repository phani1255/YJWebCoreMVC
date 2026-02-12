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

        public List<string> getFollowUpTypes()
        {
            DataTable dt = _helperCommonService.GetStoreProc("GetFTypes");
            if (dt == null || dt.Rows.Count == 0)
            {
                return new List<string>();
            }
            List<string> followUpTypes = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                if (row["ftype"] != DBNull.Value)
                {
                    followUpTypes.Add(row["ftype"].ToString());
                }
            }
            return followUpTypes;
        }

        List<string> userList { get; set; } = new List<string>();
        public DataTable loadall(string acc = "", bool blnIsPotentialCust = false)
        {
            DataTable dtUsers = new DataTable();
            DataTable dtNotes = new DataTable();
            string accname = acc;

            if (blnIsPotentialCust)
            {
                dtNotes = ShowPotentialCustomerNotes(acc);
            }
            else
            {
                dtNotes = ShowCustomerNotes(acc);
            }

            dtUsers = _helperCommonService.GetSqlData("SELECT DISTINCT NAME FROM PASSFILE");
            if (_helperCommonService.DataTableOK(dtUsers))
            {
                foreach (DataRow drUsers in dtUsers.Rows)
                    userList.Add(drUsers["Name"].ToString());
            }

            return dtNotes;
        }

        public DataTable ShowPotentialCustomerNotes(string acc)
        {
            return _helperCommonService.GetSqlData(@"select WHO as [User] ,DTIME as Date,[TYPE] as Type ,NOTE as Note ,followup as FollowUp,completed as Completed from POTENTIALCUSTNOTE where acc= trim(@acc) order by DTIME", "@acc", acc);
        }

        public DataTable ShowCustomerNotes(string acc)
        {
            return _helperCommonService.GetSqlData(@"select ID,WHO as [User] ,DTIME as Date,[TYPE] as Type ,NOTE as Note ,followup as FollowUp, completed as Completed, time as FollowUp_Time,reminder as Reminder from CUSTNOTE where acc= trim(@acc) order by DTIME", "@acc", acc);
        }
    }

}
