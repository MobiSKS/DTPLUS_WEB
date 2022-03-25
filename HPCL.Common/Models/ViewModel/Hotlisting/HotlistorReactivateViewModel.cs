using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.Hotlisting
{
    public class HotlistorReactivateViewModel : BaseEntity
    {
        public HotlistorReactivateViewModel()
        {
            HotlistEntity = new List<HotlistEntity>();
            HotlistEntity.Add(new HotlistEntity
            {
                EntityId = 0,
                EntityName = "--Select--"
            });
            HotlistReason = new List<HotlistReason>();
            HotlistReason.Add(new HotlistReason
            {
                StatusId = 0,
                StatusName = "--Select--"
            });
            HotlistStatus = new List<HotlistStatus>();
            HotlistStatus.Add(new HotlistStatus
            {
                StatusId = 0,
                StatusName = "--Select--"
            });

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
        public virtual List<HotlistReason> HotlistReason { get; set; }
        public virtual List<HotlistStatus> HotlistStatus { get; set; }
    }
    }
