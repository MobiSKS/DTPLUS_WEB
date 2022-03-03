using HPCL.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class LogController : Controller
    {
        private readonly ILogger<LogController> _logger;
        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

    }
}
