using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using PlanYourJourneyService.DataObjects;
using PlanYourJourneyService.Models;

namespace PlanYourJourneyService.Controllers
{
    public class ArrangementController : TableController<Arrangement>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            PlanYourJourneyContext context = new PlanYourJourneyContext();
            DomainManager = new EntityDomainManager<Arrangement>(context, Request);
        }

        // GET tables/TodoDbContextItem
        public IQueryable<Arrangement> GetAllArrangements()
        {
            return Query();
        }

        // GET tables/Arrangement/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Arrangement> GetArrangement(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Arrangement/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Arrangement> PatchArrangement(string id, Delta<Arrangement> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Arrangement
        public async Task<IHttpActionResult> PostArrangement(Arrangement item)
        {
            Arrangement current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Arrangement/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteArrangement(string id)
        {
            return DeleteAsync(id);
        }
    }
}