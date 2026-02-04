/*
 * Converted by Phanindra on 03-Feb-2026
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using YJWebCoreMVC.Services;

namespace YJWebCoreMVC.Controllers
{
    public class ImagesController: Controller
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;
        private readonly HelperPhanindraService _helperPhanindraService;
        public ImagesController(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, HelperPhanindraService helperPhanindraService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
            _helperPhanindraService = helperPhanindraService;
        }
        public ImagesController()
        {

        }

        public ActionResult GetImage(string fileName)
        {
            var WebImagesPath = HttpContext.Session.GetString("WebImagesUrl");
            //string imagePath = Path.Combine(@"\\ISHAL5-VM\yjewelimages", fileName);
            string imagePath = Path.Combine(WebImagesPath, fileName);
            if (!System.IO.File.Exists(imagePath))
            {
                //return HttpNotFound();
                imagePath = Path.Combine(WebImagesPath, "img_unavailable.jpg");
            }
            string contentType = "image/jpeg"; // Adjust based on file type if needed
            return File(imagePath, contentType);

        }

        // GET: /Image/GetImages?style=Style123
        public JsonResult GetImages(string style, bool emptyImagePathRequired = true, bool getAllImages = false)
        {
            try
            {
                style = GetStyle(style);
                List<string> images = new List<string>();
                //string baseUrl = "https://wjewel.dnsalias.org/styleimages/";
                string baseUrl = HttpContext.Session.GetString("WebImagesUrl");

                using (SqlConnection con = _connectionProvider.GetConnection())
                {
                    string query = "SELECT [Description] AS ImageName FROM styl_images WHERE style = @style ORDER BY ORDER_BY";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@style", style);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<string> imageUrls = new List<string>();

                            while (reader.Read())
                            {
                                if (reader["ImageName"] != DBNull.Value)
                                {
                                    string imageUrl = baseUrl + reader["ImageName"].ToString();
                                    imageUrls.Add(imageUrl);
                                    if (getAllImages == false)
                                    {
                                        break;
                                    }
                                }
                            }

                            // Check images synchronously in parallel
                            //images = CheckImagesExist(imageUrls);
                            if (imageUrls.Count == 0 && emptyImagePathRequired == true)
                            {
                                imageUrls.Add(baseUrl + "img_unavailable.jpg");
                            }
                            images = imageUrls;
                        }
                    }
                }

                return Json(images);
            }
            catch (Exception)
            {
                return Json(new List<string>());
            }
        }

        // Synchronous method to check multiple images in parallel
        private List<string> CheckImagesExist(List<string> imageUrls)
        {
            List<string> validImages = new List<string>();
            object lockObj = new object(); // Lock for thread safety

            Parallel.ForEach(imageUrls, new ParallelOptions { MaxDegreeOfParallelism = 10 }, imageUrl =>
            {
                if (ImageExists(imageUrl))
                {
                    lock (lockObj) // Ensure thread safety when adding to the list
                    {
                        validImages.Add(imageUrl);
                    }
                }
            });

            return validImages;
        }

        // Synchronous HTTP HEAD request to check if image exists
        private bool ImageExists(string imageUrl)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageUrl);
                request.Method = "HEAD";
                request.Timeout = 2000; // Set a small timeout to avoid delays
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch
            {
                return false; // If request fails (404, timeout, etc.), assume image doesn't exist
            }
        }

        [HttpPost]
        public JsonResult GetImagesAll()
        {
            try
            {
                var imageMap = new Dictionary<string, List<string>>();
                string baseUrl = HttpContext.Session.GetString("WebImagesUrl") ?? "https://wjewel.dnsalias.org/styleimages/";

                // Read and log the request bodyRequest.Body.Seek(0, SeekOrigin.Begin);

                using var reader = new StreamReader(Request.Body);
                string stylesJson = reader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine("Received styles JSON: " + stylesJson);
                var styleArray = JsonConvert.DeserializeObject<string[]>(stylesJson) ?? new string[0];

                if (styleArray.Length == 0)
                {
                    System.Diagnostics.Debug.WriteLine("No styles received or parsing failed.");
                    return Json(imageMap);
                }

                using (SqlConnection con = _connectionProvider.GetConnection())
                {
                    string query = "SELECT style, [Description] AS ImageName FROM styl_images WHERE style IN ({0})";
                    var parameters = styleArray.Select((s, i) => $"@style{i}").ToArray();
                    query = string.Format(query, string.Join(",", parameters));

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        for (int i = 0; i < styleArray.Length; i++)
                        {
                            cmd.Parameters.AddWithValue($"@style{i}", styleArray[i]);
                            System.Diagnostics.Debug.WriteLine($"Added parameter @style{i}: {styleArray[i]}");
                        }

                        con.Open();
                        using (SqlDataReader reader1 = cmd.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                string style = reader1["style"].ToString();
                                if (reader1["ImageName"] != DBNull.Value)
                                {
                                    string imageUrl = baseUrl + reader1["ImageName"].ToString();
                                    if (!imageMap.ContainsKey(style))
                                    {
                                        imageMap[style] = new List<string>();
                                    }
                                    imageMap[style].Add(imageUrl);
                                    System.Diagnostics.Debug.WriteLine($"Mapped {style} to {imageUrl}");
                                }
                            }
                        }
                    }

                    bool emptyImagePathRequired = true;
                    foreach (var style in styleArray)
                    {
                        if (!imageMap.ContainsKey(style) && emptyImagePathRequired)
                        {
                            imageMap[style] = new List<string> { baseUrl + "img_unavailable.jpg" };
                            System.Diagnostics.Debug.WriteLine($"Added default image for {style}");
                        }
                    }
                }

                return Json(imageMap);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in GetImagesAll: " + ex.ToString());
                return Json(new Dictionary<string, List<string>>());
            }
        }

        public string InvStyle(string p_style)
        {
            if (string.IsNullOrEmpty(p_style))
                return "";
            return p_style.Split('_')[0];
        }
        public string GetStyle(string style)
        {
            if (_helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.SN_Images) || _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.ModelImages))
            {
                style = _helperCommonService.GetBeforeUnderScore(style);
                string beforehyphenstyle = _helperCommonService.GetBeforeHyphen(style).Length >= 2 ? _helperCommonService.GetBeforeHyphen(style) : style;
                if (_helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.SN_Images))
                {
                    string afterhypenstyle = style.Replace(beforehyphenstyle + "-", "");
                    int afterPart;
                    if (Int32.TryParse(afterhypenstyle, out afterPart))
                        return InvStyle(beforehyphenstyle);
                    return InvStyle(style);
                }
                return beforehyphenstyle;
            }
            return style;
        }


        [HttpGet]
        public JsonResult GetImagesNew(string styleName, bool emptyImagePathRequired = true)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(styleName))
                    return Json(new List<string>());

                styleName = styleName.Replace("/", "").Trim();

                byte[] imageData = null;
                using (var con = _connectionProvider.GetConnection())
                using (var cmd = new SqlCommand("GetImage", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@stylename", styleName);
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            imageData = reader[0] as byte[];
                        }
                    }
                }

                var images = new List<string>();
                if (imageData != null)
                {
                    string base64Image = "data:image/png;base64," + Convert.ToBase64String(imageData);
                    images.Add(base64Image);
                }

                return Json(images);
            }
            catch (Exception)
            {
                return Json(new List<string>());
            }
        }

        public ActionResult GetImageBytesBasedOnStyle(string StyleCode)
        {
            var imageData = _helperCommonService.GetImage(StyleCode);
            if (string.IsNullOrEmpty(imageData))
            {
                return File(HttpContext.Session.GetString("WebImagesUrl") + "/img_unavailable.jpg", "image/jpg"); // fallback
            }

            var bytes = Convert.FromBase64String(imageData);
            return File(bytes, "image/png");
        }
        public string GetImageBasedOnStyle(string StyleCode)
        {
            string sampleImageUrl = HttpContext.Session.GetString("WebImagesUrl") + "img_unavailable.jpg";
            string imageurl = _helperCommonService.GetImage(StyleCode.ToString());
            string styleImage = "";
            if (imageurl == null || imageurl == "" || imageurl.Contains("img_unavailable"))
            {
                styleImage = "<p class='reportImg'><img src='" + (sampleImageUrl) + "' ></p>";
            }
            else
            {
                styleImage = "<p class='reportImg'><img src='data:image/png;base64," + (imageurl) + "' ></p>";
            }
            return styleImage;
        }
        public string GetStyleImages(string StyleCode, bool isByModel = false)
        {
            if (isByModel)
            {
                StyleCode = GetSummaryImageStyle(StyleCode);
            }
            DataTable dt = _helperPhanindraService.GetStyleImages(StyleCode);
            return JsonConvert.SerializeObject(dt);
        }

        private string GetSummaryImageStyle(string style)
        {
            DataTable dtsummImage = _helperCommonService.GetSqlData("select top 1 style from styl_images where style = @style or dbo.INVSTYLEWITHHYPHEN(style) = @style order by IS_DEFAULT desc", "@style", style);
            if (_helperCommonService.DataTableOK(dtsummImage))
                style = _helperCommonService.CheckForDBNull(dtsummImage.Rows[0]["style"]);
            return style;
        }

        public JsonResult GetImagesAccordion(string styleName)
        {
            if (string.IsNullOrWhiteSpace(styleName))
                return Json(new List<object>());

            styleName = styleName.Replace("/", "").Trim();

            var images = new List<object>();

            using (var con = _connectionProvider.GetConnection())
            using (var cmd = new SqlCommand("GetStyleImages", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@style", styleName);
                cmd.Parameters.AddWithValue("@norows", 10);
                cmd.Parameters.AddWithValue("@iSFromAccordion", true);

                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        byte[] imageData = (byte[])reader["Context"];

                        images.Add(new
                        {
                            imageId = Convert.ToInt32(reader["Pkid"]),
                            orderBy = Convert.ToInt32(reader["ORDER_BY"]),
                            isDefault = reader["DefaultImage"].ToString() == "Default",
                            src = "data:image/png;base64," +
                                  Convert.ToBase64String(imageData)
                        });
                    }
                }
            }
            return Json(images);
        }

        [HttpPost]
        public JsonResult RemoveStyleImage(int imageId)
        {
            using (var con = _connectionProvider.GetConnection())
            using (var cmd = new SqlCommand("DELETE FROM styl_images WHERE PKID = @pkid", con))
            {
                cmd.Parameters.AddWithValue("@pkid", imageId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return Json(new { success = true });
        }


        [HttpPost]
        public IActionResult AddStyleImages(string style, IEnumerable<IFormFile> files)
        {
            if (string.IsNullOrWhiteSpace(style) || files == null)
                return BadRequest(new { success = false });

            style = style.Replace("/", "").Trim();

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                    continue;

                byte[] imageData;

                using (var stream = file.OpenReadStream())
                using (var img = Image.FromStream(stream))
                {
                    int w = img.Width;
                    int h = img.Height;

                    if (w > 600)
                    {
                        decimal ratio = 600m / w;
                        w = 600;
                        h = (int)(h * ratio);

                        imageData = ResizeImage(img, w, h);
                    }
                    else
                    {
                        using var ms = new MemoryStream();
                        img.Save(ms, ImageFormat.Jpeg);
                        imageData = ms.ToArray();
                    }
                }

                using var con = _connectionProvider.GetConnection();
                using var cmd = new SqlCommand("AddStyleImage", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@fData", SqlDbType.Image, imageData.Length).Value = imageData;
                cmd.Parameters.Add("@style", SqlDbType.NVarChar).Value = style;
                cmd.Parameters.Add("@desc", SqlDbType.NVarChar).Value = Path.GetFileName(file.FileName);
                cmd.Parameters.Add("@orig_name", SqlDbType.NVarChar).Value = Path.GetFileName(file.FileName);
                cmd.Parameters.Add("@lastfolderpath", SqlDbType.NVarChar).Value = string.Empty;

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Json(new { success = true });
        }


        private byte[] ResizeImage(Image img, int width, int height)
        {
            using (var bmp = new Bitmap(width, height))
            using (var g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
                using (var ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateImageOrder(List<ImageOrderDto> list)
        {
            using (SqlConnection con = _connectionProvider.GetConnection())
            {
                con.Open();
                foreach (var item in list)
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE styl_images SET order_by = @o WHERE pkid = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@o", item.OrderBy);
                        cmd.Parameters.AddWithValue("@id", item.ImageId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            return Json(true);
        }
        public class ImageOrderDto
        {
            public int ImageId { get; set; }
            public int OrderBy { get; set; }
        }
        [HttpPost]
        public ActionResult SetDefaultImage(string style, int imageId)
        {
            using (SqlConnection con = _connectionProvider.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = @"UPDATE styl_images SET IS_DEFAULT = CASE WHEN PKID = @pkid THEN 1 ELSE 0 END WHERE STYLE = @style";
                    cmd.Parameters.AddWithValue("@pkid", imageId);
                    cmd.Parameters.AddWithValue("@style", style);
                    cmd.ExecuteNonQuery();
                }
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult RotateImage(string style, string imageSrc, string direction)
        {
            if (string.IsNullOrEmpty(imageSrc))
                return Json(new { success = false });

            var base64 = imageSrc.Substring(imageSrc.IndexOf(",") + 1);
            byte[] imgBytes = Convert.FromBase64String(base64);
            using (var ms = new MemoryStream(imgBytes))
            using (var img = Image.FromStream(ms))
            {
                if (direction == "left")
                    img.RotateFlip(RotateFlipType.Rotate90FlipX);
                else
                    img.RotateFlip(RotateFlipType.Rotate90FlipY);

                byte[] rotatedBytes;
                using (var outStream = new MemoryStream())
                {
                    img.Save(outStream, ImageFormat.Gif);
                    rotatedBytes = outStream.ToArray();
                }

                using (var con = _connectionProvider.GetConnection())
                using (var cmd = new SqlCommand("AddStyleImage", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@fData", SqlDbType.Image).Value = rotatedBytes;
                    cmd.Parameters.Add("@style", SqlDbType.NVarChar).Value = style;
                    cmd.Parameters.Add("@desc", SqlDbType.NVarChar).Value = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".gif";
                    cmd.Parameters.Add("@orig_name", SqlDbType.NVarChar).Value = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".gif";

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return Json(new { success = true });
        }
        public ActionResult CaptureFromWebcam(string style, string imageBase64)
        {
            if (string.IsNullOrWhiteSpace(style) || string.IsNullOrWhiteSpace(imageBase64))
                return BadRequest("Invalid input");

            try
            {
                string base64 = imageBase64.Contains(",") ? imageBase64.Split(',')[1] : imageBase64;
                byte[] rawBytes = Convert.FromBase64String(base64);

                Image finalImage;

                using (var ms = new MemoryStream(rawBytes))
                using (var img = Image.FromStream(ms))
                {
                    FixImageOrientation(img);
                    finalImage = ResizeImageToMaxWidth(img, 600);
                }

                byte[] finalBytes;

                using (var outStream = new MemoryStream())
                {
                    using (var safeImage = new Bitmap(finalImage.Width, finalImage.Height, PixelFormat.Format24bppRgb))
                    using (var g = Graphics.FromImage(safeImage))
                    {
                        safeImage.SetResolution(finalImage.HorizontalResolution, finalImage.VerticalResolution);
                        g.DrawImage(finalImage, 0, 0);

                        safeImage.Save(outStream, ImageFormat.Jpeg);
                    }

                    finalBytes = outStream.ToArray();
                }

                string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";

                using (var con = _connectionProvider.GetConnection())
                using (var cmd = new SqlCommand("AddStyleImage", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@fData", SqlDbType.Image).Value = finalBytes;
                    cmd.Parameters.Add("@style", SqlDbType.NVarChar).Value = style;
                    cmd.Parameters.Add("@desc", SqlDbType.NVarChar).Value = filename;
                    cmd.Parameters.Add("@orig_name", SqlDbType.NVarChar).Value = filename;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                //return new HttpStatusCodeResult(500, ex.Message);
                return BadRequest("Invalid input");
            }
        }
        public Bitmap ResizeImageToMaxWidth(Image img, int maxWidth)
        {
            int width = img.Width;
            int height = img.Height;

            if (width <= maxWidth)
            {
                return new Bitmap(img);
            }

            float ratio = (float)height / width;
            int newWidth = maxWidth;
            int newHeight = (int)(newWidth * ratio);

            var bmp = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (var g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.DrawImage(img, 0, 0, newWidth, newHeight);
            }

            return bmp; // fully detached safe image
        }

        private void FixImageOrientation(Image img)
        {
            const int OrientationKey = 0x0112;
            if (!img.PropertyIdList.Contains(OrientationKey))
                return;
            try
            {
                var prop = img.GetPropertyItem(OrientationKey);
                int orientation = prop.Value[0];
                switch (orientation)
                {
                    case 2: img.RotateFlip(RotateFlipType.RotateNoneFlipX); break;
                    case 3: img.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                    case 4: img.RotateFlip(RotateFlipType.Rotate180FlipX); break;
                    case 5: img.RotateFlip(RotateFlipType.Rotate90FlipX); break;
                    case 6: img.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                    case 7: img.RotateFlip(RotateFlipType.Rotate270FlipX); break;
                    case 8: img.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
                }
                img.RemovePropertyItem(OrientationKey);
            }
            catch { }
        }
    }
}
