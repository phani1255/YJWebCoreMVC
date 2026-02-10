
#region
    
  // venkat 02/05/2026 Service created
 
#endregion


using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Text;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
  
    public class RapnetService 
    {       
        
        private readonly HelperService _helperService;

        public RapnetService(HelperService helperService)
        {
            _helperService = helperService;
        }
        public DataTable GetRapnetData()
        {
            return _helperService.HelperCommon.GetSqlData(@"
            select shape as s,
                   concat(wt1,'-',wt2) as e,
                   color as dd,
                   clarity as d,
                   price
            from rapnet
            where clarity in ('IF','VVS1','VVS2','VS1','VS2','SI1','SI2','SI3','I1','I2','I3')
            order by shape, color, wt1");
        }

        public void UpdateRapnetCredentials(string username, string password)
        {
            _helperService.HelperCommon.GetSqlData(
                "UPDATE ups_ins SET rap_usr = @username, rap_pw = @password",
                "@username", username,
                "@password", password
            );
        }

        public string UpdateRapnetPrices()
        {
            DataTable dt = _helperService.HelperCommon.GetSqlData("SELECT * FROM rapnet");

            if (!_helperService.HelperCommon.DataTableOK(dt))
            {
                return "No data found.";
            }

            _helperService.HelperCommon.GetStoreProc("UpdateStyleRapPrice");

            return "Diamond Prices Updated Successfully.";
        }

        public (bool Success, string Message) DownloadrapnetPrices()
        {
            try
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                string folderpath = Path.Combine(Path.GetTempPath(), "Rapnet");

                DataRow rapnetuserdetails = _helperService.HelperVenkat.getrapnetuserdetails();
                if (rapnetuserdetails == null)
                    return (false, "Rapnet Login Details Missing.");

                string username = rapnetuserdetails["rap_usr"].ToString();
                string password = rapnetuserdetails["rap_pw"].ToString();

                if (!Directory.Exists(folderpath))
                    Directory.CreateDirectory(folderpath);

                values.Add("Username", username);
                values.Add("Password", password);

                string authticket = HttpPostRequest("https://technet.rapaport.com/HTTP/Authenticate.aspx", values);
                values.Clear();

                if (string.IsNullOrEmpty(authticket))
                    return (false, "Invalid Rapnet Credentials.");

                values.Add("ticket", authticket);
                string roundcsv = HttpPostRequest("http://technet.rapaport.com/HTTP/Prices/CSV2_Round.aspx", values);
                values.Clear();

                CreateCsv(roundcsv, "roundcsv.csv", folderpath);

                values.Add("ticket", authticket);
                string pearcsv = HttpPostRequest("http://technet.rapaport.com/HTTP/Prices/CSV2_Pear.aspx", values);

                CreateCsv(pearcsv, "pearcsv.csv", folderpath);

                ReadCsvAndUpdateDb(folderpath);

                _helperService.HelperCommon.GetStoreProc("UpdateStyleRapPrice");

                return (true, "Diamond Prices Updated Successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        private string HttpPostRequest(string url, Dictionary<string, string> postParameters)
        {
            string postData = "";

            foreach (string key in postParameters.Keys)
                postData += Uri.EscapeUriString(key) + "=" + Uri.EscapeUriString(postParameters[key]) + "&";

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            myHttpWebRequest.Method = "POST";

            byte[] data = Encoding.ASCII.GetBytes(postData);

            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = data.Length;

            Stream requestStream = myHttpWebRequest.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();
            try
            {
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Stream responseStream = myHttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);
                string pageContent = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                responseStream.Close();
                myHttpWebResponse.Close();

                return pageContent;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void ReadCsvAndUpdateDb(string folderPath)
        {
            var rapnetdata = new DataTable();
            rapnetdata.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn("shape", typeof(string)),
                new DataColumn("clarity", typeof(string)),
                new DataColumn("color", typeof(string)),
                new DataColumn("wt1", typeof(string)),
                new DataColumn("wt2", typeof(string)),
                new DataColumn("price", typeof(string)),
                new DataColumn("date", typeof(string))
            });

            var roundPath = Path.Combine(folderPath, "roundcsv.csv");
            var pearPath = Path.Combine(folderPath, "pearcsv.csv");

            ReadSingleCsvIntoTable(roundPath, rapnetdata);
            ReadSingleCsvIntoTable(pearPath, rapnetdata);

            if (rapnetdata.Rows.Count > 0)
            {
                _helperService.HelperVenkat.UpdateRapnetTable(rapnetdata);
            }
        }

        private void ReadSingleCsvIntoTable(string filePath, DataTable table)
        {
            if (!File.Exists(filePath))
                return;

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var splits = line.Split(',');

                    if (splits.Length < 7)
                        continue;

                    string wt2 = splits[4];

                    if (!string.IsNullOrWhiteSpace(splits[3]) &&
                        decimal.TryParse(splits[3], out decimal wt) &&
                        wt == 5)
                    {
                        wt2 = "99.99";
                    }

                    table.Rows.Add(
                        splits[0],
                        splits[1],
                        splits[2],
                        splits[3],
                        wt2,
                        splits[5],
                        splits[6]
                    );
                }
            }
        }


        private void CreateCsv(string csvString, string fileName, string folderPath)
        {
            if (string.IsNullOrWhiteSpace(csvString))
                return;

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);
            var lines = csvString.Split('\n');

            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var item in lines)
                {
                    if (string.IsNullOrWhiteSpace(item))
                        continue;

                    var results = item.Split(',');

                    // Ensure expected columns exist
                    if (results.Length < 7 || string.IsNullOrWhiteSpace(results[0]))
                        continue;

                    var shape = results[0];
                    var clarity = results[1];
                    var color = results[2];
                    var wt1 = results[3];
                    var wt2 = results[4];
                    var price = results[5];
                    var date = results[6].Replace("\r", "");

                    var line = $"{shape},{clarity},{color},{wt1},{wt2},{price},{date}";
                    writer.WriteLine(line);
                }
            }
        }


        //public DataRow GetRapnetUserDetails()
        //{
        //    return __helperCommonService.GetRapnetUserDetails();
        //}

        //public void UpdateRapnetTable(DataTable table)
        //{
        //    UpdateRapnetTable(table);
        //}

        //public void ExecuteUpdateStyleRapPrice()
        //{
        //    __helperCommonService.ExecuteStoreProc("UpdateStyleRapPrice");
        //}
    }
}
