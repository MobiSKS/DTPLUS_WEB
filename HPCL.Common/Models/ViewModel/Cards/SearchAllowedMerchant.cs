using HPCL.Common.Models.CommonEntity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HPCL.Common.Models.ViewModel.Cards
{
    //[OnePropertySpecified(ErrorMessage = "")]
    public class SearchAllowedMerchant : BaseEntity
    {
        [StringLength(14)]
        //[Required(ErrorMessage = "Card Number is required")]
        public string CardNumber { get; set; }

        [StringLength(10)]
        //[Required(ErrorMessage = "Merchant Id is required")]
        public string MerchantId { get; set; }

        [StringLength(10)]
        //[Required(ErrorMessage = "Customer Id is required")]
        public string CustomerId { get; set; }
    }

    //public class OnePropertySpecifiedAttribute : ValidationAttribute
    //{
    //    public override bool IsValid(object value)
    //    {
    //        Type typeInfo = value.GetType();
    //        PropertyInfo[] propertyInfo = typeInfo.GetProperties();
    //        foreach (var property in propertyInfo)
    //        {
    //            if (null != property.GetValue(value, null))
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    //}
}
