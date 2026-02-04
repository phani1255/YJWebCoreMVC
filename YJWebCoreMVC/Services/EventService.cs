/*
 *  Created By Dharani on 12-September-2025
 *  Dharani 09/12/2025 Added GetAllEvents, InsertEvent, DeleteEvent methods.
 */

using System.Data;

namespace YJWebCoreMVC.Services
{
    public class EventService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public EventService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }
        public DataTable GetAllEvents()
        {
            return _helperCommonService.GetSqlData(@"SELECT MAINEVENT,SUBEVENT FROM EVENTS ORDER BY MAINEVENT ASC");
        }
        public DataTable InsertEvent(string event_type)
        {
            return _helperCommonService.GetStoreProc(@"INSERT_DELETE_EVENTS", "@MAINEVENT", event_type);
        }
        public DataTable DeleteEvent(string MAINEVENT, string SUBEVENT)
        {
            return _helperCommonService.GetSqlData(@"DELETE FROM [DBO].[EVENTS] WHERE MAINEVENT = @MAINEVENT AND SUBEVENT = @SUBEVENT", "@MAINEVENT", MAINEVENT, "@SUBEVENT", SUBEVENT);

        }
    }
}
