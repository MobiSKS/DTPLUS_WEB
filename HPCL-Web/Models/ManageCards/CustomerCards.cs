using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL_Web.Models.ManageCards
{
    public class CustomerCards : BaseEntity
    {
        public CustomerCards()
        {
            StatusModals = new List<StatusModal>();
        }

        [Required(ErrorMessage = "CustomerId is Required")]
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public string MobileNumber { get; set; }
        public string VehicleNumber { get; set; }
        public int StatusFlag { get; set; }

        public string CardCategory { get; set; }
        public string CardIssueType { get; set; }
        public DateTime RequestDate { get; set; }
        public string FormNumber { get; set; }
        public string RbeId { get; set; }
        public string RbeName { get; set; }
        public string vehicleType { get; set; }
        public string VehicleMake { get; set; }
        public string YearOfReg { get; set; }
        public string OwnerType { get; set; }
        public string VinNumber { get; set; }
       
        public virtual List<StatusModal> StatusModals { get; set; }
    }

    public class StatusModal
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }

    public class SearchCardResult
    {
        public int SrNumber { get; set; }
        public string CardNumber { get; set; }
        public string CustomerID { get; set; }
        public string UserID { get; set; }
        public string RequestDate { get; set; }
        public int OwnedorAttachedId { get; set; }
        public string OwnedorAttached { get; set; }
        public string VechileNo { get; set; }
        public string VehicleType { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
        public int YearOfRegistration { get; set; }
        public string Manufacturer { get; set; }
        public string MobileNumber { get; set; }
        public string VINNumber { get; set; }
        public string VehicleMake { get; set; }
        public string OwnershipType { get; set; }
        public string FormNumber { get; set; }
        public string CardCategory { get; set; }
        public string CardIssueType { get; set; }
        public string CardIdentifier { get; set; }
    }

    public class ServicesResponse
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public int SelectedServices { get; set; }
    }

    public class UpdateMobileResponse
    {
        public string Reason { get; set; }
    }

    public class SearchGridResponse
    {
        public int SrNumber { get; set; }
        public string CardNumber { get; set; }
        public string CustomerID { get; set; }
        public string UserID { get; set; }
        public string RequestDate { get; set; }
        public string OwnedorAttachedId { get; set; }
        public string OwnedorAttached { get; set; }
        public string VechileNo { get; set; }
        public string VehicleType { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
        public string YearOfRegistration { get; set; }
        public string Manufacturer { get; set; }
        public string MobileNumber { get; set; }
        public string VINNumber { get; set; }
        public string VehicleMake { get; set; }
        public string OwnershipType { get; set; }
        public string FormNumber { get; set; }
        public string CardCategory { get; set; }
        public string CardIssueType { get; set; }
        public string CardIdentifier { get; set; }
    }
}