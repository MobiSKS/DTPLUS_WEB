using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.DtpSupport
{
    public class GeneralUpdatesModel : BaseEntity
    {
        public GeneralUpdatesModel()
        {
            EntityMdl = new List<GetEntityModel>();
            EntityMdl.Add(new GetEntityModel
            {
                EntityId = 0,
                EntityName = "--Select--"
            });

            EntityFieldMdl = new List<GetEntityFieldModel>();
            SalesAreaMdl = new List<SalesAreaModel>();

            SalesAreaMdl.Add(new SalesAreaModel
            {
                SalesAreaID = 0,
                SalesAreaName = "Select"
            });
        }
        public virtual List<GetEntityModel> EntityMdl { get; set; }
        public virtual List<GetEntityFieldModel> EntityFieldMdl { get; set; }
        public virtual List<SalesAreaModel> SalesAreaMdl { get; set; }
        public string Message { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityFieldId { get; set; }
        public string CustomerIdOrCardOrMerchantId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public int StatusCode { get; set; }
        public int Status { get; set; }
        public string UpdateStatus { get; set; }
        public int CustomerSalesAreaID { get; set; }
    }
}