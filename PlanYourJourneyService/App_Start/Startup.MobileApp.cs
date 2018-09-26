using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using PlanYourJourneyService.DataObjects;
using PlanYourJourneyService.Models;
using Owin;

namespace PlanYourJourneyService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new PlanYourJourneyInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer<PlanYourJourneyContext>(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
        }
    }

    public class PlanYourJourneyInitializer : CreateDatabaseIfNotExists<PlanYourJourneyContext>
    {
        protected override void Seed(PlanYourJourneyContext context)
        {
            List<Arrangement> arrangements = new List<Arrangement>
            {
                new Arrangement
                {
                    Id = Guid.NewGuid().ToString(), Title = "First item", Date = new DateTime(2000,2,2),
                    Location = "Russia", ImageResourcePath = null, Contents = "Notes", Favorite = 0,
                    Author = "Pushkin"
                },
                new Arrangement { Id = Guid.NewGuid().ToString(), Title = "Second item",
                    Date = new DateTime(2000,2,2),
                    Location = "Russia", ImageResourcePath = null, Contents = "Notes", Favorite = 0,
                    Author = "Pushkin" },
            };

            foreach (Arrangement arrangement in arrangements)
            {
                context.Set<Arrangement>().Add(arrangement);
            }

            base.Seed(context);
        }
    }
}

