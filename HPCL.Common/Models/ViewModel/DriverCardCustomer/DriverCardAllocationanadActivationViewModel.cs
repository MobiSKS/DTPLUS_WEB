﻿using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.DriverCardCustomer
{
    public class DriverCardAllocationanadActivationViewModel : BaseEntity
    {
        public DriverCardAllocationanadActivationViewModel()
        {
            ZoneMdl = new List<ZonalOfficeResponseModal>();
            RegionMdl = new List<RegionalOfficeResponseModal>();
            DriverCardAllocationandActivationDetails = new List<DriverCardAllocationandActivationDetails>();
            ZoneMdl.Add(new ZonalOfficeResponseModal
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "--All--"
            });
            RegionMdl.Add(new RegionalOfficeResponseModal
            {
                RegionalOfficeID = 0,
                RegionalOfficeName = "--All--"
            });
        }

        public List<DriverCardAllocationandActivationDetails> DriverCardAllocationandActivationDetails { get; set; }
        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CustomerId { get; set; }
        public virtual List<RegionalOfficeResponseModal> RegionMdl { get; set; }
        public virtual List<ZonalOfficeResponseModal> ZoneMdl { get; set; }
    }
    public class DriverCardAllocationandActivationDetails
    {

        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string FormNumber { get; set; }
        public string CardNo { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MappingDate { get; set; }
        public string AllocatedMID { get; set; }
        public string ZO { get; set; }
        public string RO { get; set; }
        public string RetailOutletName { get; set; }
        public string AllocationDate { get; set; }
        public string FirstTransactionDate { get; set; }
        public string IsDTPCustomer { get; set; }
    }
}