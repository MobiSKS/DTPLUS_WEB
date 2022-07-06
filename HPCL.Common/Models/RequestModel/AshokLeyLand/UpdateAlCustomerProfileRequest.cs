using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.AshokLeyLand
{
    public class UpdateAlCustomerProfileRequest: BaseEntity
    {
        public string CustomerID { get; set; }
        public string ZonalOffice { get; set; }
        public string RegionalOffice { get; set; }
        public string formNumber { get; set; }
        public string DateOfApplication { get; set; }
        public string salesArea { get; set; }
        public string IndividualOrgNameTitle { get; set; }
        public string IndividualOrgName { get; set; }
        public string NameOnCard { get; set; }
        public string TypeOfBusinessEntity { get; set; }
        public string ResidenceStatus { get; set; }
        public string IncomeTaxPan { get; set; }
        public string CommunicationAddress1 { get; set; }
        public string CommunicationAddress2 { get; set; }
        public string CommunicationAddress3 { get; set; }
        public string CommunicationLocation { get; set; }
        public string CommunicationCityName { get; set; }
        public string CommunicationPincode { get; set; }
        public string CommunicationStateId { get; set; }
        public string CommunicationDistrictId { get; set; }
        public string CommunicationPhoneNo { get; set; }
        public string CommunicationFax { get; set; }
        public string CommunicationMobileNo { get; set; }
        public string CommunicationEmailid { get; set; }
        public string SignedOnDate { get; set; }

    }
}