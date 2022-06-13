using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.ValidateNewCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ValidateAddOnCards
{
    public class ValidateAddOnCardsModel
    {
        public ValidateAddOnCardsModel()
        {
            NewCardsLists = new List<NewCardsList>();
            VehicleNumberCardIdentifierLists = new List<VehicleNumberCardIdentifierList>();
            OfficerTypeMdl = new List<OfficerTypeResponseModal>();
            OfficerTypeMdl.Add(new OfficerTypeResponseModal
            {
                OfficerTypeID = 0,
                OfficerTypeName = "--Select--",
                OfficerTypeShortName = "--Select--"
            });
            States = new List<StateResponseModal>();
            States.Add(new StateResponseModal
            {
                CountryID = 0,
                StateID = 0,
                StateName = "--Select--"
            });
            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        public string FormNumber { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CustomerName { get; set; }
        public string StateId { get; set; }
        public string CreatedBy { get; set; }
        public string Comments { get; set; }
        public virtual List<NewCardsList> NewCardsLists { get; set; }
        public virtual List<VehicleNumberCardIdentifierList> VehicleNumberCardIdentifierLists { get; set; }
        public virtual List<OfficerTypeResponseModal> OfficerTypeMdl { get; set; }
        public virtual List<StateResponseModal> States { get; set; }
    }

}