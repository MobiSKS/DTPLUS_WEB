﻿using HPCL.Common.Models.ResponseModel.CustomerDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel
{
    public class CustomerDashboardModel
    {
        public CustomerDashboardModel()
        {
            accountSummaryResponseModels = new List<AccountSummaryResponseModel>();
            verifyYourDetailsResponseModels = new List<VerifyYourDetailsResponseModel>();
            reminderResponseModels = new List<ReminderResponseModel>();
        }
        public List<AccountSummaryResponseModel> accountSummaryResponseModels { get; set; }
        public List<VerifyYourDetailsResponseModel> verifyYourDetailsResponseModels { get; set; }
        public List<ReminderResponseModel> reminderResponseModels { get; set; }
    }
}
