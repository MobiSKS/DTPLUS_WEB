namespace HPCL_Web.Models.Login
{
    public class LoginResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string LoginType { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
