using HPCL_Web.Models.CommonEntity;

namespace HPCL_Web.Models.ViewModel.Security
{
    public class BindGrid : BaseEntity
    {
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string StatusId { get; set; }
        public string Comments { get; set; }
    }

    public class ViewRbeDetails : BaseEntity
    {
        public string UserName { get; set; }
    }

    public class ViewRbeDetailsResponse
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


    public class ApproveRejectRbeUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Comments { get; set; }
        public int Approvalstatus { get; set; }
        public string ApprovedBy { get; set; }
    }

    public class ApproveRejectRbeUserResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
