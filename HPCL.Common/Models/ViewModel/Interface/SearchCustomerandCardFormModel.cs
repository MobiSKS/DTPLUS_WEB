using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HPCL.Common.Models.ViewModel.Interface
{
    public class SearchCustomerandCardFormModel:BaseEntity
    {
        public SearchCustomerandCardFormModel()
        {

            RecordTypeList = new List<RecordTypeModel>();
            RecordTypeList.Add(new RecordTypeModel
            {
                entityId = "",
                entityName = "--Select--"
            });
            States = new List<StateResponseModal>();
            States.Add(new StateResponseModal
            {
                StateID = 0,
                StateName = "--All--"
            });
           

        }
        [Required(ErrorMessage = "Select Record Type")]
        public string RecordTypeId { get; set; }
        public string EntityId { get; set; }
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidFormNo, ErrorMessage = FieldValidation.ValidFormNoErrMsg)]
        public string FormNumber { get; set; }
        public string StateID { get; set; }
        public string CityId{ get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public virtual List<RecordTypeModel> RecordTypeList { get; set; }
        public virtual List<StateResponseModal> States { get; set; }
        //public virtual List<GetCityResponse> Cities { get; set; }
    }
}
