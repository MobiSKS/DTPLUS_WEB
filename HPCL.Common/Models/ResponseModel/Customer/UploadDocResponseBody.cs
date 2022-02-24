using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class UploadDocResponseBody
    {
        public string FormNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string IdProofTypeId { get; set; }
        public string IdProofTypeName { get; set; }
        public string IdProofDocumentNo { get; set; }
        public string IdProofFront { get; set; }
        public string IdProofBack { get; set; }
        public string AddressProofTypeId { get; set; }
        public string AddressProofTypeName { get; set; }
        public string AddressProofDocumentNo { get; set; }
        public string AddressProofFront { get; set; }
        public string AddressProofBack { get; set; }
    }
}
