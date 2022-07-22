using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.MODashboard
{
    public class RegionInformationResponseModel
    {
        public string TotalCustomers { get; set; }
        public string FleetCustomers { get; set; }
        public string NonFleetCustomers { get; set; }
        public string CorporateCustomers { get; set; }
        public string CustomerswithAttachedVechicles { get; set; }
        public string GenericCardCustomers { get; set; }
        public string TatkalCardCustomers { get; set; }
        public string OTCCardCustomers { get; set; }
        public string DriverCardCustomers { get; set; }
        public string ActiveCustomersOfLastMonth { get; set; }
        public string NoOfCards { get; set; }
        public string ActiveCardsOfLastMonth { get; set; }
        public string T1Customer { get; set; }
        public string T2Customer { get; set; }
        public string HPRe_FuelCardCustomers { get; set; }
        public string ActiveHPRe_FuelCardCustomersoflastmonth { get; set; }
        public string NoofApprovedorActiveDealerswithminimum1installedterminal { get; set; }
        public string Noofinstalledandactiveterminals  { get; set; }
        public string ActiveDealersoflastmonth { get; set; }
        public string ActiveTerminalsoflastmonth { get; set; }
    }
}

