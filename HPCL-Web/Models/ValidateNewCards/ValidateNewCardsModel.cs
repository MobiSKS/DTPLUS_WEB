using HPCL_Web.Models.Officer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models.ValidateNewCards
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
        public string RBEId { get; set; }
        public string RBEName { get; set; }
        public string CardPreference { get; set; }
        public string FeePaymentDetails { get; set; }
        public string CRNo { get; set; }
        public string CRName { get; set; }
        public string NumberOfCards { get; set; }
        public string Comments { get; set; }
        public virtual List<NewCardsList> NewCardsLists { get; set; }
        public virtual List<VehicleNumberCardIdentifierList> VehicleNumberCardIdentifierLists { get; set; }
        public virtual List<OfficerTypeModel> OfficerTypeMdl { get; set; }
    }

    public class NewCardsList
    {
        public string CustomerRefNo { get; set; }
        public string FormNo { get; set; }
        public string CustomerName { get; set; }
        public string RBE_RSMName { get; set; }
        public string MobileNo { get; set; }
    }
    public class VehicleNumberCardIdentifierList
    {
        public string VehicleNumberCardIdentifier { get; set; }
        public string VehicleOwnerName { get; set; }
    }
}
