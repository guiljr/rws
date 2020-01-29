using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Library.ExtensionsCore
{
    public static class BearerAuthenticationExtension
    {
        private const string KeyType = "KeyType";
        private const string KeyTypeKeyFile = "KeyFile";
        private const string KeyTypeKeyStore = "KeyStore";
        private const string KeyTypeTemporary = "Temporary";
        private const string KeyFilePath = "KeyFilePath";
        private const string KeyFilePassword = "KeyFilePassword";
        private const string KeyStoreIssuer = "KeyStoreIssuer";
        private const string KeyThumbprint = "KeyThumbprint";

        public static IIdentityServerBuilder AddSigninCredentialFromConfig(
          this IIdentityServerBuilder builder, IConfigurationSection options, ILogger logger)
        {

            try
            {
                var secret = "test".Sha256();
                string keyType = options.GetValue<string>(KeyType);
                logger.Information($"SigninCredentialExtension keyType is {keyType}");

                switch (keyType)
                {
                    case KeyTypeTemporary:
                        logger.Information($"SigninCredentialExtension adding Temporary Signing Credential");
                        builder.AddDeveloperSigningCredential();
                        break;

                    case KeyTypeKeyFile:
                        AddCertificateFromFile(builder, options, logger);
                        break;

                    case KeyTypeKeyStore:
                        AddCertificateFromStore(builder, options, logger);
                        break;
                }

                return builder;
            }
            catch
            {
                throw;
            }
        }

        private static void AddCertificateFromStore(IIdentityServerBuilder builder,
          IConfigurationSection options, ILogger logger)
        {
            var keyIssuer = options.GetValue<string>(KeyStoreIssuer);
            var keyThumbprint = options.GetValue<string>(KeyThumbprint);

            logger.Information($"SigninCredentialExtension adding key from store by {keyThumbprint}");

            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            //var certificates = store.Certificates.Find(X509FindType.FindByIssuerName, keyIssuer, true);
            var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, keyThumbprint, true);

            if (certificates.Count > 0)
                builder.AddSigningCredential(certificates[0]);
            else
                logger.Fatal("A matching key couldn't be found in the store");
        }

        private static void AddCertificateFromFile(IIdentityServerBuilder builder,
          IConfigurationSection options, ILogger logger)
        {
            var keyFilePath = options.GetValue<string>(KeyFilePath);
            var keyFilePassword = options.GetValue<string>(KeyFilePassword);

            if (File.Exists(keyFilePath))
            {
                logger.Information($"SigninCredentialExtension adding key from file {keyFilePath}");
                builder.AddSigningCredential(new X509Certificate2(keyFilePath, keyFilePassword));
            }
            else
            {
                logger.Error($"SigninCredentialExtension cannot find key file {keyFilePath}");
            }
        }
    }
}
