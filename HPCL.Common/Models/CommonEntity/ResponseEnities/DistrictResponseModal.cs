using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.CommonEntity.ResponseEnities
{
    public class DistrictResponseModal
    {
        public string districtCode { get; set; }
        public int stateID { get; set; }
        public int districtID { get; set; }
        public string districtName { get; set; }
        public string stateName { get; set; }
    }
}
