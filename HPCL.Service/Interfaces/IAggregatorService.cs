using HPCL.Common.Models.ViewModel.Aggregator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IAggregatorService
    {

        Task<ManageAggregatorViewModel> ManageAggregator(string FromDate, string ToDate);
        Task<ManageAggregatorViewModel> ManageAggregator(ManageAggregatorViewModel cust);
    }
}

