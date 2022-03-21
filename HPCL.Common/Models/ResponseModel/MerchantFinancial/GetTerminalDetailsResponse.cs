using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.MerchantFinancial
{
    public class GetTerminalDetailsResponse : ResponseMsg
    {
        public GetTerminalDetailsResponse()
        {
            objTerminalDeploymentDetail = new List<ObjTerminalDeploymentDetail>();
            objTerminalDetails = new List<ObjTerminalDetail>();
        }
        public virtual List<ObjTerminalDeploymentDetail> objTerminalDeploymentDetail { get; set; }
        public virtual List<ObjTerminalDetail> objTerminalDetails { get; set; }
    }

    public class ObjTerminalDetail
    {
        public string TerminalId { get; set; }
        public string MerchantId { get; set; }
        public string MerchantCity { get; set; }
        public string ApprovalDate { get; set; }
        public string FirstTransactionDate { get; set; }
        public string LastTransactionDate { get; set; }
        public string DeploymentStatus { get; set; }
    }

    public class ObjTerminalDeploymentDetail
    {
        public string Events { get; set; }
        public string Date { get; set; }
        public string Comments { get; set; }
        public string DeliveredTo { get; set; }
    }
}
