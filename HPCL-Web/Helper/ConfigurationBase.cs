using System;
using Microsoft.Extensions.Configuration;

namespace HPCL_Web.Helper
{
    public abstract class ConfigurationBase
    {
        protected IConfigurationRoot GetAPIBaseUrl()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        protected void RaiseValueNotFoundException(string configurationKey)
        {
            throw new Exception($"appsettings key ({configurationKey}) could not be found.");
        }
    }
}