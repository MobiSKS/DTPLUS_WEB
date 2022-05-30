using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.CommonEntity
{
    public class SessionDataModelDetails
    {
        public string UserID { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string LoginType { get; set; }
        public string RegionalId { get; set; }
        public string ZonalId { get; set; }
        public string MerchantID { get; set; }
        public string UserId { get; set; }
        public string Today { get; set; }
        public string LocalStorage { get; set; }
        public string UserRole { get; set; }
        public string BreadCrumbsController { get; set; }
        public string BreadCrumbsAction { get; set; }
        public string CurrentAction { get; set; }
    }
}
