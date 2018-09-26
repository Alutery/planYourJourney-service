using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PlanYourJourneyService.Startup))]

namespace PlanYourJourneyService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}