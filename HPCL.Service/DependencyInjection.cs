using HPCL.Service.Interfaces;
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

            return services;
        }
    }
}
