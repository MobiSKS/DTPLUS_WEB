using System;
using System.Collections.Generic;
using System.Text;
using HPCL.Common.Models.CommonEntity;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer
{
    public class RequestForOTCCardModel
    {
        public RequestForOTCCardModel()
        {
            RegionMdl = new List<RegionModel>();
            RegionMdl.Add(new RegionModel
            {
                RegionID = 0,
                RegionName = "Select Region"

            });

        }
        public virtual List<RegionModel> RegionMdl { get; set; }

        [Required(ErrorMessage = "No of My HP (OTC) Cards is Required")]
        public string NoofCards { get; set; }

        [Required(ErrorMessage = "Region Name is Required")]
        public int CustomerRegionID { get; set; }
        public string Remarks { get; set; }
        public int Internel_Status_Code { get; set; }
    }
}
