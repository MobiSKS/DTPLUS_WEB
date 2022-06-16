namespace HPCL.Common.Models.CommonEntity
{
    public class SuccessResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
        public string ActionName { get; set; }
    }
    public class SuccessResponseTatkalCustomer
    {
        public int Status { get; set; }
        public string Reason { get; set; }
        public string ActionName { get; set; }
        public string CardNo { get; set; }
        public string Customerid { get; set; }
    }
}
