using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using XamagramNETbackend.DataObjects;
using XamagramNETbackend.Models;
using Owin;

namespace XamagramNETbackend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MobileServiceInitializer());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    // This middleware is intended to be used locally for debugging. By default, HostName will
                    // only have a value when running in an App Service application.
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);
        }
    }

    public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            List<City> cities = new List<City>
            {
                new City { Id = Guid.NewGuid().ToString(), Name = "New York", Description = "La ciudad de Nueva York" },
                new City { Id = Guid.NewGuid().ToString(), Name = "Seattle", Description = "La ciudad de Seattle" },
                new City { Id = Guid.NewGuid().ToString(), Name = "Sevilla", Description = "La ciudad de Sevilla" },
                new City { Id = Guid.NewGuid().ToString(), Name = "Madrid", Description = "La ciudad de Madrid" },
            };

            foreach (City city in cities)
            {
                context.Set<City>().Add(city);
            }

            base.Seed(context);
        }
    }
}

