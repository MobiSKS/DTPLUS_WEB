namespace HPCL_Web.Models.Customer
{
    public class CustomerType : BaseEntity
    {
        public int CTFlag { get; set; }
    }

    public class CustomerTypeResponse
    {
        public int CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
    }
}
