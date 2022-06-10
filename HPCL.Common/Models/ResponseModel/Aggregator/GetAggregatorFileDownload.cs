using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Aggregator
{
    public class GetAggregatorFileDownload:CommonResponseBase
    {
        public List<GetAggregatorFileDetails> Data { get; set; }
    }
    public class GetAggregatorFileDetails
    {
        public string FormNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string AddressProofTypeId { get; set; }
        public string AddressProofTypeName { get; set; }
        public string CustomerAddressProof { get; set; }
        public string IdProofTypeId { get; set; }
        public string IdProofTypeName { get; set; }
        public string IDProofofOwnerPartner { get; set; }
        public string PanCardTypeId { get; set; }
        public string PanCardTypeName { get; set; }
        public string PANCarddetails { get; set; }
        public string VehicleDetailsTypeId { get; set; }
        public string VehicleDetailsTypeName { get; set; }
        public string VehicleDetails { get; set; }
        public string CustomerFormTypeId { get; set; }
        public string CustomerFormTypeName { get; set; }
        public string SignedCustomerForm { get; set; }
    }
    public class FileModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
