namespace YJWebCoreMVC.Services
{
    public class ImageService
    {
        private readonly HelperService _helperService;
        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;

        public ImageService(HelperService helperService, ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, HelperCommonService helperCommonService)
        {
            _helperService = helperService;
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _env = env;
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
    }
}
