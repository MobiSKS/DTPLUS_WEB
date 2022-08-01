using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Interface
{
    public class RegenerateIACModel : BaseEntity
    {
        public RegenerateIACModel()
        {
            RegenerateIACResponseModels = new List<RegenerateIACResponseModel>();
         
        }
        public List<RegenerateIACResponseModel> RegenerateIACResponseModels { get; set; }
        
    }
}