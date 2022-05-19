using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Configure
{
    public class UpdateSMSAlertstoIndividualCardUsersRequest : BaseEntity
    {
        public string CustomerID { get; set; }
        public List<OBJSMSAlertsToIndividualCardUsers> TypeSMSAlertsToIndividualCardUsers { get; set; }
        public UpdateSMSAlertstoIndividualCardUsersRequest()
        {
            TypeSMSAlertsToIndividualCardUsers = new List<OBJSMSAlertsToIndividualCardUsers>();
        }
    }

    public class OBJSMSAlertsToIndividualCardUsers
    {
        public string CardNo { get; set; }
        public string VechileNo { get; set; }
        public string Mobileno { get; set; }
    }
}