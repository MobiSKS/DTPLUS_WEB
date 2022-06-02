using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ResponseModel.CommonResponse;
using HPCL.Common.Models.ViewModel.Officers;
using System;
using System.Collections.Generic;

namespace HPCL.Common.Models.ViewModel.ValidateNewCards
{
    public class ValidateNewCardsModel
    {
        public ValidateNewCardsModel()
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

    public class NewCardsList
    {
        public string FormNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string TotalCards { get; set; }
        public string CreatedRoleId { get; set; }
        public string CreatedRoleName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string StatusId { get; set; }
        public string StatusName { get; set; }
        public string KYCStatus { get; set; }
        public string CustomerID { get; set; }
        public string NoofVechileforAllCards { get; set; }
        public string Action { get; set; }
        public string CustomerType { get; set; }


    }
    public class VehicleNumberCardIdentifierList
    {
        public string VehicleNumberCardIdentifier { get; set; }
        public string VehicleOwnerName { get; set; }
    }
}
