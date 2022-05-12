using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.AshokLeyLand
{
    public class GetAlCustomerDetailForVerificationModel : CommonResponseBase
    {
        public GetAlCustomerDetailForVerificationModel()
        {
            CustomerStateMdl = new List<StateResponseModal>();
            CustomerStateMdl.Add(new StateResponseModal
            {
                StateID = 0,
                StateName = "--All State--"
            });

            StatusResponseMdl = new List<StatusResponseModal>();
            Data = new List<GetAlCustomerDetailForVerificationDetails>();
        }
        public virtual List<StateResponseModal> CustomerStateMdl { get; set; }
        public virtual List<StatusResponseModal> StatusResponseMdl { get; set; }
        public string FormNumber { get; set; }
        public string CustomerName { get; set; }
        public int StateID { get; set; }
        public int Status { get; set; }
        public virtual List<GetAlCustomerDetailForVerificationDetails> Data { get; set; }
    }

    public class GetAlCustomerDetailForVerificationDetails
    {
        public string FormNumber { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}