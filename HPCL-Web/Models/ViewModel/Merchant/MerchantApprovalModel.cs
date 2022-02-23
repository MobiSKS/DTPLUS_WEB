﻿using System.Collections.Generic;

namespace HPCL_Web.Models.ViewModel.Merchant
{
    public class MerchantApprovalModel
    {
        public MerchantApprovalModel()
        {
            Categories = new List<CategoriesModel>();
            MerchantApprovalTableDetails = new List<MerchantApprovalTableModel>();
            Categories.Add(new CategoriesModel
            {
                CategoryID = 1,
                CategoryType = "New Merchants"
            });
            Categories.Add(new CategoriesModel
            {
                CategoryID = 2,
                CategoryType = "Updated Merchants"
            });
            Categories.Add(new CategoriesModel
            {
                CategoryID = 3,
                CategoryType = "Clone Merchant"
            });
        }
        public string CategoryID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ShowTable { get; set; }
        public virtual List<CategoriesModel> Categories { get; set; }
        public virtual List<MerchantApprovalTableModel> MerchantApprovalTableDetails { get; set; }
    }
    public class CategoriesModel
    {
        public int CategoryID { get; set; }
        public string CategoryType { get; set; }
    }
}
