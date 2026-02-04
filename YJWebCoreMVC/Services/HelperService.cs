
namespace YJWebCoreMVC.Services
{
    public class HelperService
    {
        public HelperCommonService HelperCommon { get; }
        public HelperPhanindraService HelperPhanindra { get; }
        public HelperDharaniService HelperDharani{ get; }
        public HelperLokeshService HelperLokesh { get; }
        public HelperManojService HelperManoj { get; }
        public HelperSivaService HelperSiva { get; }
        public HelperSravanService HelperSravan { get; }
        public HelperVenkatService HelperVenkat { get; }


        public HelperService(HelperCommonService helperCommon, HelperPhanindraService helperPhanindra, HelperDharaniService helperDharani,
            HelperLokeshService helperLokesh, HelperManojService helperManoj, HelperSivaService helperSiva, HelperSravanService helperSravan, HelperVenkatService helperVenkat)
        {
            HelperCommon = helperCommon;
            HelperPhanindra = helperPhanindra;
            HelperDharani = helperDharani;
            HelperLokesh = helperLokesh;
            HelperManoj = helperManoj;
            HelperSiva = helperSiva;
            HelperSravan = helperSravan;
            HelperVenkat = helperVenkat;
        }
    }
}
