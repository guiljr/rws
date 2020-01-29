using Engine.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Engine.Model;

namespace Engine.Business
{
    public class HostBS : IHostBS
    {
        private IHostRepository _repository;

        public HostBS(IHostRepository repository)
        {
            _repository = repository;
        }

        public async Task<Models.ExhangeRateModel> GetExchangeRate(Models.ExchangeRateRequestModel model)
        {
            return await this._repository.GetExchangeRate(model);
        }
    }
}