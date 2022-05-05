using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.TMS
{
    public class ManageEnrollmentsModel : BaseEntity
    {
        public string CustomerID { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public int StatusCode { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Reason { get; set; }
        public virtual List<UpdateEnrollmentCustomer> tmsUpdateEnrollmentCustomerList { get; set; }
        public ManageEnrollmentsModel()
        {
            tmsUpdateEnrollmentCustomerList = new List<UpdateEnrollmentCustomer>();
        }
    }
    public class UpdateEnrollmentCustomer
    {
        public string CustomerID { get; set; }
        public string TMSUserId { get; set; }
        public string TMSStatusID { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
        public string CreatedBy { get; set; }
    }
}