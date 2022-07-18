using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Security
{
    public  class UserCreationRequestModel:CommonResponseBase
    {
        public UserCreationRequestModel()
        {
            getUserRolesandregions = new List<GetUserRolesAndRegions>();
        }
        public string Comments { get; set; }
        public string Email { get; set; }
        public string FirstName { get;set; }
        public string LastName { get;set; }
        public string MiddleName { get; set; }  
        public virtual List<GetUserRolesAndRegions> getUserRolesandregions { get; set; }
    }
}
