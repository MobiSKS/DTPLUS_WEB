using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class GetGenericAttachedVehicleReq : BaseEntity
    {
        public string Customerid { get; set; }
        public string Cardno { get; set; }
        public string Mobileno { get; set; }
        public string Vehiclenumber { get; set; }
        public int Statusflag { get; set; }
    }
}
