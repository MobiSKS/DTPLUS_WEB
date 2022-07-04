using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public class ParentCustomerStatusReport
    {
        public ParentCustomerStatusReport()
        {
            ZonalOffices = new List<CustomerZonalOfficeModel>();
            ZonalOffices.Add(new CustomerZonalOfficeModel
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "--All--"
            });

            SBUTypes = new List<SbuTypeResponseModal>();
            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        public string SBUTypeId { get; set; }
        public virtual List<SbuTypeResponseModal> SBUTypes { get; set; }
        public string FormNumber { get; set; }
      
        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
        public string RegionalOfcIdVal { get; set; }
       
        public string FromDate { get; set; }
        public string ToDate { get; set; }
      
        public virtual List<CustomerZonalOfficeModel> ZonalOffices { get; set; }

        
    }
}
