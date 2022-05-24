using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.CustomerRequest
{
    public class UpdateCardRenwalRequestPost : BaseEntity
    {
        public string CustomerId { get; set; }
        public TypeCardRenewalRequest[] TypeCardRenewalRequest { get; set; }
    }

    public class TypeCardRenewalRequest
    {
        public string CardNo { get; set; }
        public string VechileNo { get; set; }
    }
}
