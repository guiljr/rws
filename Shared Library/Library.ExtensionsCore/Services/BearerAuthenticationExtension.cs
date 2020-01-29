using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.ExtensionsCore
{
    public static class BearerAuthentication
    {

        private const string Authority = "Authority";
        private const string Audience = "Audience";
        public static IServiceCollection BearerAuthenticationBasic(this IServiceCollection services, IConfigurationSection config, ILogger logger)
        {

            services.AddAuthentication()
              .AddJwtBearer("Bearer", options =>
              {
                  options.Authority = config.GetValue<string>(Authority);
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuer = true,
                      //ValidIssuers = new[] { },
                  };
                  options.Audience = config.GetValue<string>(Audience); //scope that client should have
                  options.RequireHttpsMetadata = false;
                  options.Events = new JwtBearerEvents()
                  {
                      OnTokenValidated = tvContext =>
                      {
                          // Add the access_token as a claim
                          var accessToken = tvContext.SecurityToken as JwtSecurityToken;
                          if (accessToken != null)
                          {
                              ClaimsIdentity identity = tvContext.Principal.Identity as ClaimsIdentity;
                              if (identity != null)
                              {
                                  identity.AddClaim(new Claim("access_tokens", accessToken.RawData));
                              }
                          }
                          return Task.CompletedTask;
                      }
                  };
              });

            return services;
        }

    }
}