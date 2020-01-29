using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Library.ExtensionsCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Models = Engine.Model;
using Engine.Contracts.Interfaces;

namespace DynAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class HostController : ControllerBase
    {
        private IHostBS _bsvc;
        private readonly IConfiguration _config;
        public HostController(IConfiguration config, IHostBS bsvc)
        {
            _config = config;
            _bsvc = bsvc;
        }


        [HttpGet]
        public string Index()
        {
            return "Hello";
        }

        [HttpPost("~/api/getexchangerate")]
        public async Task<Models.ResponseModelBase<Models.ExhangeRateModel>> GetExchange([FromBody] Models.ExchangeRateRequestModel model)
        {
            try
            {
                var response = await _bsvc.GetExchangeRate(model);
                response.TargetValue = (Convert.ToDecimal(response.Value) * Convert.ToDecimal(model.SourceValue)).ToString();

                return await Task.Factory.StartNew(() =>
                {
                    return new Models.ResponseModelBase<Models.ExhangeRateModel>()
                    {
                        Success = true,
                        Data = response
                    };

                });
            }
            catch (Exception ex)
            {
                return await Task.Factory.StartNew(() =>
                {
                    return new Models.ResponseModelBase<Models.ExhangeRateModel>()
                    {
                        Success = false,
                        Message = ex.Message
                    };
                });
            }
        }

    }
}
