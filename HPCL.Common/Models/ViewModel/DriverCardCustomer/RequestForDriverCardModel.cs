﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HPCL.Common.Models.CommonEntity;


namespace HPCL.Common.Models.ViewModel.DriverCardCustomer
{
    public class RequestForDriverCardModel : BaseEntity
    {
        public RequestForDriverCardModel()
        {
            RegionMdl = new List<RegionModel>();
            RegionMdl.Add(new RegionModel
            {
                RegionID = 0,
                RegionName = "Select Region"

            });

        }
        public virtual List<RegionModel> RegionMdl { get; set; }

        [Required(ErrorMessage = "No of Driver Cards is Required")]
        public string NoofCards { get; set; }

        [Required(ErrorMessage = "Region Name is Required")]
        public int CustomerRegionID { get; set; }
        public string Remarks { get; set; }
        public int Internel_Status_Code { get; set; }
    }

    
}