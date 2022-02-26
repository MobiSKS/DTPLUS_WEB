namespace HPCL.Common.Models.CommonEntity
{
    public class BaseEntity
    {
        public string UserId { get; set; }
        public string UserAgent { get; set; }
        public string UserIp { get; set; }
        public string ZonalId { get; set; }
        public string RegionalId { get; set; }
        public string CreatedBy { get; set; }
    }
}
