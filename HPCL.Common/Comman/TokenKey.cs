using Microsoft.Extensions.Configuration;
using System;

namespace HPCL.Common
{
    public class TokenManager
    {
        private readonly IConfiguration _configuration;
        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string StrStoreCode
        {
            get
            {
                return _configuration.GetSection("TokenSettings:StoreCode").Value.ToString();
            }
        }
        public string ApiBaseurl
        {
            get
            {
                return _configuration.GetSection("TokenSettings:apiBaseurl").Value.ToString();
            }
        }
        public string StrSecretKey
        {
            get
            {

                return _configuration.GetSection("TokenSettings:SecretKey").Value.ToString();
            }
        }

        public string StrAPI_Key
        {
            get
            {
                return _configuration.GetSection("TokenSettings:API_Key").Value.ToString();
            }
        }
        public string UserAgent
        {
            get
            {
                return _configuration.GetSection("TokenSettings:useragent").Value.ToString();
            }
        }
        public string Userip
        {
            get
            {
                return _configuration.GetSection("TokenSettings:userip").Value.ToString();
            }
        }
        public string Userid
        {
            get
            {
                return _configuration.GetSection("TokenSettings:userid").Value.ToString();
            }
        }

    }
}
