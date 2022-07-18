using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Security
{
    public class UserCreationReqModel:BaseEntity
    {
        public string Comments { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string UserRole { get; set; }
        public List<UserLocDetails> TypeUserCreationDetails { get; set; }
        public List<UserLocDetailsWithName> UserCreationReqDetails { get; set; }
    }
    public class UserLocDetails
    {
        public string ZO { get; set; }
        public string RO { get; set; }
    }

    public class UserLocDetailsWithName
    {
        public string ZO { get; set; }
        public string RO { get; set; }
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
    }
}
