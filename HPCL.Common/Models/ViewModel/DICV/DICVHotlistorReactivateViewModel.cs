using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.DICV
{
    public class DICVHotlistorReactivateViewModel : BaseEntity
    {
        public DICVHotlistorReactivateViewModel()
        {
            HotlistEntity = new List<HotlistEntity>();
            HotlistEntity.Add(new HotlistEntity
            {
                EntityId = 0,
                EntityName = "--Select--"
            });
            HotlistReason = new List<DICVHotlistReason>();
            HotlistReason.Add(new DICVHotlistReason
            {
                ReasonId = 0,
                ReasonName = "--Select--"
            });
            HotlistStatus = new List<HotlistStatus>();
            HotlistStatus.Add(new HotlistStatus
            {
                StatusId = 0,
                StatusName = "--Select--"
            });
            FromDate = DateTime.Now.ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        public string HotlistDate { get; set; }
        public string EntityType { get; set; }
        public string EntityIdVal { get; set; }
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string Remarks { get; set; }
        public string EntityTypeId { get; set; }
        public string ActionId { get; set; }
        public string Action { get; set; }
        public string ReasonId { get; set; }
        public string ReasonDetails { get; set; }
        public virtual List<HotlistEntity> HotlistEntity { get; set; }
        public virtual List<DICVHotlistReason> HotlistReason { get; set; }
        public virtual List<HotlistStatus> HotlistStatus { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}