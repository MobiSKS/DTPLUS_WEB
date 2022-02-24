using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Officer
{
    public class OfficerEditUpdateRequestModal : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string StateId { get; set; }
        public string CityName { get; set; }
        public string DistrictId { get; set; }
        public string Pin { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string Fax { get; set; }
        public string ModifiedBy { get; set; }
        public int OfficerId { get; set; }
    }
}
