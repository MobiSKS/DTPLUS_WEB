using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.TatkalCardCustomer
{
    public class TatkalViewRequestModel : BaseEntity
    {


        public TatkalViewRequestModel()
        {
            ZonalOffices = new List<ZonalOfficeResponseModal>();
            ZonalOffices.Add(new ZonalOfficeResponseModal
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "--All--"
            });

            RegionMdl = new List<CustomerRegionModel>();
            RegionMdl.Add(new CustomerRegionModel
            {
                RegionalOfficeID = 0,
                RegionalOfficeName = "--All--"

            });

            Reasons = new List<TerminalManagementCloseReasonModel>();
            Reasons.Add(new TerminalManagementCloseReasonModel
            {
                ReasonId = 0,
                ReasonName = "--Select--"
            });
            TatkalViewRequest = new List<TatkalViewRequestDetailsModel>();

            StatusName = new List<Status>();
            StatusName.Add(new Status
            {
                StatusId = 0,
                StatusName = "--All--"

            });
            StatusName.Add(new Status
            {
                StatusId = 1,
                StatusName = "Pending"

            });
            StatusName.Add(new Status
            {
                StatusId = 2 ,
                StatusName = "Mapped"

            });

            SBUTypes = new List<SbuTypeResponseModal>();
        }
        public virtual List<SbuTypeResponseModal> SBUTypes { get; set; }
        public string SBUTypeId { get; set; }

        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
        public string RegionalOfcIdVal { get; set; }
        public string CloseRequestStatus { get; set; }
        public int CustomerRegionID { get; set; }


        public string Status { get; set; }
        public int StatusId { get; set; }

        public int StatusFlag { get; set; }
        public string Reason { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public virtual List<TerminalManagementCloseReasonModel> Reasons { get; set; }
        public virtual List<ZonalOfficeResponseModal> ZonalOffices { get; set; }
        public virtual List<CustomerRegionModel> RegionMdl { get; set; }
        public virtual List<Status> StatusName { get; set; }


        public virtual List<TatkalViewRequestDetailsModel> TatkalViewRequest { get; set; }

    }
    public class Status
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
