using System.Collections.Generic;
using System.Threading.Tasks;
using Models = Engine.Model;

namespace Engine.Contracts.Interfaces
{
    public interface IHostBS
    {
      
        Task<Models.ExhangeRateModel> GetExchangeRate(Models.ExchangeRateRequestModel model);
    }

}