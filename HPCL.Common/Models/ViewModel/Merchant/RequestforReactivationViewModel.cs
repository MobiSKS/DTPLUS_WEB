using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Merchant
{
    public class RequestforReactivationViewModel : CommonResponseBase
    {
        public RequestforReactivationViewModel()
        {
            ZonalOffices = new List<ZonalOfficeResponseModal>();
            ZonalOffices.Add(new ZonalOfficeResponseModal
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "--All--"
            });

            HotlistedDates = new List<HotlistedDates>();
            HotlistedDates.Add(new HotlistedDates
            {
                HotlistDateId = 0,
                HotlistDate= "--All--"
            });

            SBUTypes = new List<SbuTypeResponseModal>();
            StatusList = new List<MerchantReactivationStatus>();
            HotlistedDates = new List<HotlistedDates>();
            Data = new List<RequestForReactivationMerchantDetails>();
        }
        public virtual List<SbuTypeResponseModal> SBUTypes { get; set; }
        public string SBUTypeId { get; set; }
        public string MerchantId { get; set; }

        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
        public string RegionalOfcIdVal { get; set; }
        public string HotlistDate { get; set; }
        public string StatusId { get; set; }

        public virtual List<MerchantReactivationStatus> StatusList { get; set; }
        public virtual List<HotlistedDates> HotlistedDates { get; set; }
        public virtual List<ZonalOfficeResponseModal> ZonalOffices { get; set; }
        public List<RequestForReactivationMerchantDetails> Data { get; set; }

    }
    public class MerchantReactivationStatus
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
    public class HotlistedDates
    {
        public int HotlistDateId { get; set; }
        public string HotlistDate { get; set; }
    }
    public class RequestForReactivationMerchantDetails
    {
        public string MerchantId { get; set; }
        public string RetailOutletName { get; set; }
        public string HotlistReasonId { get; set; }
        public string HotlistedDate { get; set; }
        public string RequestedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public string Reason{ get; set; }
    }

}
