using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.Terminal
{
    public class TerminalDeinstallationRequestViewModel : BaseEntity
    {
        public TerminalDeinstallationRequestViewModel()
        {
            ZonalOffices = new List<ZonalOfficeResponseModal>();
            ZonalOffices.Add(new ZonalOfficeResponseModal
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "--All--"
            });

            Reasons = new List<TerminalManagementCloseReasonModel>();
            Reasons.Add(new TerminalManagementCloseReasonModel
            {
                ReasonId = 0,
                ReasonName = "--Select--"
            });
             ObjMerchantDeinstallationDetail = new List<MerchantDeinstallationDetail>();
            ObjTerminalDeinstallationDetail= new List<TerminalDeinstallationDetail>();
            TerminalDeinstallationRequestDetails = new List<TerminalDeinstallationRequestDetailsViewModel>();
            DeinstallationTypes = new List<DeinstallationTypeModel>();
            DeinstallationTypes.Add(new DeinstallationTypeModel
            {
                DeinstallationTypeID = 0,
                DeinstallationType = "--Select--"
            });
            DeinstallationTypes.Add(new DeinstallationTypeModel
            {
                DeinstallationTypeID = 1,
                DeinstallationType = "All Terminal"
            });
            DeinstallationTypes.Add(new DeinstallationTypeModel
            {
                DeinstallationTypeID = 2,
                DeinstallationType = "Individual Terminal"
            });
        }

        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
        public string RegionalOfcIdVal { get; set; }
        public string CloseRequestStatus { get; set; }
        public string Reason { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DeinstallationTypeID { get; set; }
        public virtual List<TerminalManagementCloseReasonModel> Reasons { get; set; }
        public virtual List<ZonalOfficeResponseModal> ZonalOffices { get; set; }

        public virtual List<MerchantDeinstallationDetail> ObjMerchantDeinstallationDetail { get; set; }
        public virtual List<TerminalDeinstallationDetail> ObjTerminalDeinstallationDetail { get; set; }
        public virtual List<DeinstallationTypeModel> DeinstallationTypes { get; set; }
        public virtual List<TerminalDeinstallationRequestDetailsViewModel> TerminalDeinstallationRequestDetails { get; set; }
    }
    public class MerchantDeinstallationDetail
    {
        public string MerchantName { get; set; }
        public string RetailOutletName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
    }
    public class TerminalDeinstallationDetail
    {
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string TerminalId { get; set; }
        public string CreatedTime { get; set; }
        
    }
    public class DeinstallationTypeModel
    {
        public int DeinstallationTypeID { get; set; }
        public string DeinstallationType { get; set; }
    }
}
