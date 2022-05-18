﻿using HPCL.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using HPCL.Service.Services;
namespace HPCL.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceDependency(this IServiceCollection services)
        {
            services.AddTransient<IRequestService, RequestService>();
            services.AddTransient<ICardServices, CardServices>();
            services.AddTransient<ICustomerFeeWaiverServices, CustomerFeeWaiverServices>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IDriverCardCustomerService, DriverCardCustomerService>();
            services.AddTransient<IMyHpOTCCardCustomerService, MyHpOTCCardCustomerService>();
            services.AddTransient<ITatkalCardCustomerService, TatkalCardCustomerService>();
            services.AddTransient<IOfficerServices, OfficerServices>();
            services.AddTransient<ICommonActionService, CommonActionService>();
            services.AddTransient<IControlCardSearch, ControlCard>();
            services.AddTransient<IViewCardService, ViewCardService>();
            services.AddTransient<ICustomerManageService, CustomerManageService>();
            services.AddTransient<IMerchantServices, MerchantServices>();
            services.AddTransient<ITerminalManagement, TerminalManagementService>();
            services.AddTransient<ILocationServices, LocationServices>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IApprovalService, ApprovalService>();
            services.AddTransient<IValidateNewCardsService, ValidateNewCardsServices>();
            services.AddTransient<IAshokLeyLandService, AshokLeyLandService>();
            services.AddTransient<ICustomerFinancialService, CustomerFinancialService>();
            services.AddTransient<IMerchantFinancialService, MerchantFinancialService>();
            services.AddTransient<IHotlistingService, HotlistingService>();
            services.AddTransient<IManageRbeService, ManageRbeService>();
            services.AddTransient<IApplicationFormDataEntryService, ApplicationFormDataEntryService>();
            services.AddTransient<IInterfaceService, InterfaceService>();
            services.AddTransient<IDtpSupportService, DtpSupportService>();
            services.AddTransient<ITMSService, TMSService>();
            services.AddTransient<IAggregatorService, AggregatorService>();
            services.AddTransient<ICustomerRequestService, CustomerRequestService>();
            services.AddTransient<IFleetService, FleetService>();
            return services;
        }
    }
}
