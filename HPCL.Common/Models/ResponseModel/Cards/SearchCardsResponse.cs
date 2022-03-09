using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class SearchCardsResponse : ResponseMsg
    {
        public List<CardSearch> data { get; set; }
    }

    public class CardSearch
    {
        public int SrNumber { get; set; }
        public string CardNumber { get; set; }
        public string VechileNo { get; set; }
        public string MobileNumber { get; set; }
        public int CardStatus { get; set; }
        public string Status { get; set; }
    }
}
