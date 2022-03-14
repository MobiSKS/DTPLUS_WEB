using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.TerminalManagementResponse
{
  public  class ManageTerminalResponse
    {

        public string Merchantid { get; set; }

        public string Terminalid { get; set; }

        public string ApprovalDate { get; set; }

        public string DeploymentStatus { get; set; }

        public string TerminalIssuanceType { get; set; }

        public string MappedMerchantId { get; set; }

        public string ServiceChargeInRsPerLtr { get; set; }

        public string RouteId { get; set; }

        public string EffectiveDate { get; set; }

    }
}
