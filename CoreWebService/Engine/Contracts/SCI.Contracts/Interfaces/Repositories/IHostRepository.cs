using Engine.Base.Contracts.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities = Engine.Contracts.Entities;
using Models = Engine.Model;

namespace Engine.Contracts.Interfaces
{
    public interface IHostRepository
    {

        Task<Models.ExhangeRateModel> GetExchangeRate(Models.ExchangeRateRequestModel model);


    }
}