
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Data;
using System.Xml;
using static YJWebCoreMVC.Services.HelperCommonService;

namespace YJWebCoreMVC.Services
{
    public class HelperLokeshService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;
        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public HelperLokeshService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            _env = env;
        }
        public string GetDataTableXMLEliminatehexadecimals(string tableName, DataTable dataTable)
        {
            foreach (DataColumn column in dataTable.Columns)
                if (column.DataType == typeof(DateTime))
                    column.DateTimeMode = DataSetDateTime.Unspecified;
            // Remove null characters from string columns
            foreach (DataRow row in dataTable.Rows)
                foreach (DataColumn column in dataTable.Columns)
                    if (column.DataType == typeof(string) && row[column] != DBNull.Value)
                    {
                        string cellValue = row[column].ToString();
                        row[column] = cellValue.Replace("\0", string.Empty);
                    }

            dataTable.AcceptChanges();
            dataTable.TableName = tableName;

            // Generate XML
            using (TextWriter writer = new UnicodeStringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(writer))
                {
                    xmlWriter.WriteStartDocument();
                    dataTable.WriteXml(xmlWriter);
                }

                // Remove unnecessary attributes and return the XML
                return writer.ToString().Replace(" xml:space=\"preserve\"", string.Empty);
            }
        }
        public byte[] ResizeImage(byte[] originalBytes, int maxWidth)
        {
            using (var ms = new MemoryStream(originalBytes))
            using (var img = Image.Load(ms))
            {
                int width = img.Width;
                int height = img.Height;

                if (width > maxWidth)
                {
                    decimal ratio = (decimal)maxWidth / width;
                    width = maxWidth;
                    height = (int)(height * ratio);
                }

                img.Mutate(x => x.Resize(width, height));

                using (var output = new MemoryStream())
                {
                    img.Save(output, new JpegEncoder
                    {
                        Quality = 90 // adjust if needed
                    });

                    return output.ToArray();
                }
            }

        }
        public DataTable getstyledataByCastCode(string castCode)
        {
            return _helperCommonService.GetSqlData("SELECT * FROM STYLES with (nolock) WHERE CAST_CODE = @castCode", "@castCode", castCode.Trim());
        }
    }
}
