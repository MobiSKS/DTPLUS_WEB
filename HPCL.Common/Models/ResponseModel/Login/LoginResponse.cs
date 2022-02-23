namespace HPCL.Common.Models.ResponseModel.Login
{
    public class LoginResponse
    {
        public string LoginType { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string UserRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StatusId { get; set; }
        public string StatusName { get; set; }
        public string MobileNo { get; set; }
        public string RegionalOfficeID { get; set; }
        public string RegionalOfficeName { get; set; }
        public string ZonalOfficeID { get; set; }
        public string ZonalOfficeName { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
