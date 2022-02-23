using HPCL_Web.Models.ViewModel.Officers;
using System.Collections.Generic;

namespace HPCL_Web.Models.ViewModel.ValidateNewCards
{
    public class ValidateNewCardsModel
    {
        public ValidateNewCardsModel()
        {
            NewCardsLists = new List<NewCardsList>();
            VehicleNumberCardIdentifierLists = new List<VehicleNumberCardIdentifierList>();
            OfficerTypeMdl = new List<OfficerTypeModel>();
            OfficerTypeMdl.Add(new OfficerTypeModel
            {
                OfficerTypeID = 0,
                OfficerTypeName = "--Select--",
                OfficerTypeShortName = "--Select--"
            });
        }
        public string FormNumber { get; set; }
        public string Date { get; set; }
        public string CreatedBy { get; set; }
        public string Comments { get; set; }
        //public virtual List<VehicleDetailsModel> VehicleDetailsMdl { get; set; }
        public virtual List<NewCardsList> NewCardsLists { get; set; }
        public virtual List<VehicleNumberCardIdentifierList> VehicleNumberCardIdentifierLists { get; set; }
        public virtual List<OfficerTypeModel> OfficerTypeMdl { get; set; }
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
    }
    public class VehicleNumberCardIdentifierList
    {
        public string VehicleNumberCardIdentifier { get; set; }
        public string VehicleOwnerName { get; set; }
    }
}
