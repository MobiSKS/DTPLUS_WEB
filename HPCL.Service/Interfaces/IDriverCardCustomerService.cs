﻿using HPCL.Common.Models;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.DriverCardCustomer;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IDriverCardCustomerService
    {
        Task<RequestForDriverCardModel> RequestForDriverCard();
        Task<RequestForDriverCardModel> RequestForDriverCard(RequestForDriverCardModel requestForDriverCardModel);
        Task<DriverCardCustomerModel> CreateDriverCardCustomer();
        Task<DriverCardAllocationToMerchantModel> DriverCardAllocation();
        Task<OTCUnAllocatedCardsResponse> GetAllUnAllocatedDriverCards(string RegionalId);
        Task<CommonResponseData> SaveDriverCardsAllocation([FromBody] LinkCardsToMerchantModel linkCardsToMerchantModel);
        Task<List<CardDetails>> GetAvailableDriverCardByRegionalId(string RegionalId, string MerchantID);
        Task<DriverCardCustomerModel> CreateDriverCardCustomer(DriverCardCustomerModel customerModel);

    }
}
