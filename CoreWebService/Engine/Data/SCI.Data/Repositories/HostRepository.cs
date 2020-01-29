using System;
using System.Linq;
using System.Collections.Generic;
using Entities = Engine.Contracts.Entities;
using Models = Engine.Model;
using Engine.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Engine.Data;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Dynamic;
using System.ComponentModel;
using Engine.Contracts.Entities;

namespace Engine.Data
{
    public class HostRepository : GenericRepository<Entities.tblExchangeRate>, IHostRepository
    {
        private const string SCIDbConnection = "SCIDbConnection";
        private readonly IConfiguration _config;
        private SCIContext dBContext;
        public HostRepository(SCIContext context, IConfiguration config)
          : base(context)
        {
            dBContext = context;
            _config = config;
        }

        public async Task<Models.ExhangeRateModel> GetExchangeRate(Models.ExchangeRateRequestModel model)
        {
            try
            {
                using (var context = dBContext)
                {
                    var q = (from p in context.tblExchangeRates
                             where p.Source == model.Source && p.Target == model.Target
                             select new Models.ExhangeRateModel
                             {
                                 Value = p.Value,
                                 Source = p.Source,
                                 Target = p.Target
                             }
                        ).ToList().FirstOrDefault();
                    return await Task.Factory.StartNew(() => {
                        return q;
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



    }
}