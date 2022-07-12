using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.AshokLayland
{
    public class GetALUploadKycDocumentResponse : CommonResponseBase
    {
        public List<GetALUploadKycDocumentDaata> Data { get; set; }
    }
    public class GetALUploadKycDocumentDaata
    {
        public string IdProofFront { get; set; }
        public string AddressProofFront { get; set; }
        public string PanCardFront { get; set; }
        public string VehicleDetailsFront { get; set; }
        public string CustomerFormProofFront { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}