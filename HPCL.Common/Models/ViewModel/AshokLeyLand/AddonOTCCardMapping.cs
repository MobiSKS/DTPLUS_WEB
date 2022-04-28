using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.AshokLeyLand
{
    public class AddonOTCCardMapping : BaseEntity
    {
        public string CustomerId { get; set; }
        public string NoOfCards { get; set; }
        public List<AddonOTCCardDetails> ObjCardDetail { get; set; }
        public virtual List<VehicleTypeModel> VehicleTypeMdl { get; set; }
        public int Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Remarks { get; set; }
        public string Reason { get; set; }
        public string CustomerOrgName { get; set; }
        public string NameOnCard { get; set; }
        public string DealerCode { get; set; }
        public string SalesExecutiveEmployeeID { get; set; }
        public string TableStringyfiedData { get; set; }
        public bool VehicleVerifiedManually { get; set; }
        public string ExternalVehicleAPIStatus { get; set; }
        public AddonOTCCardMapping()
        {
            VehicleTypeMdl = new List<VehicleTypeModel>();
            ObjCardDetail = new List<AddonOTCCardDetails>();
            VehicleVerifiedManually = false;
        }
    }
    
    public class AddonOTCCardDetails
    {
        public string CardNo { get; set; }
        public string VechileNo { get; set; }
        public string VehicleType { get; set; }
        public string VINNumber { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public string NoOfCards { get; set; }
        public string DuplicateVehicleNo { get; set; }
        public string DuplicateMobileNo { get; set; }
        public string CustomerId { get; set; }
        public bool VehicleVerifiedManually { get; set; }
        public string CustomerOrgName { get; set; }
        public string NameOnCard { get; set; }
        public string DealerCode { get; set; }
        public string SalesExecutiveEmployeeID { get; set; }
        public string Verified { get; set; }
        public AddonOTCCardDetails()
        {
            VehicleVerifiedManually = false;
        }
    }

}