using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Security
{
    public class ViewRbeDetailsResponse : ResponseMsg
    {
       public List<ViewRbeDetailsResponseData> data { get; set; }
    }

    public class ViewRbeDetailsResponseData
    {
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string Location { get; set; }
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
        public string RBEPhoto { get; set; }
    }
}
