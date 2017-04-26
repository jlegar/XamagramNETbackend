using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using XamagramNETbackend.DataObjects;
using XamagramNETbackend.Models;

namespace XamagramNETbackend.Controllers
{
    public class CityController : TableController<City>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<City>(context, Request);
        }

        // GET tables/City
        public IQueryable<City> GetAllCity()
        {
            return Query(); 
        }

        // GET tables/City/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<City> GetCity(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/City/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<City> PatchCity(string id, Delta<City> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/City
        public async Task<IHttpActionResult> PostCity(City item)
        {
            City current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/City/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCity(string id)
        {
             return DeleteAsync(id);
        }
    }
}
