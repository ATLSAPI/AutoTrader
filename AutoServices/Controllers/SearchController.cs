using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoServices.Models;
using System.Web.Http.Cors;

namespace AutoServices.Controllers
{
    public class SearchController : ApiController
    {
       static readonly ISearchRepository repository = new SearchRepository();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Search> GetAllProducts()
       {
           return repository.GetAll();
       }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public Search GetProduct(int id)
       {
           Search item = repository.Get(id);
           if (item == null)
           {
               throw new HttpResponseException(HttpStatusCode.NotFound);
           }
           return item;
       }

       public IEnumerable<Search> GetProductsByCategory(string category)
       {
           return repository.GetAll().Where(
               p => string.Equals(p.Make, category, StringComparison.OrdinalIgnoreCase));
       }

       public Search PostProduct(Search item)
       {
           item = repository.Add(item);
           return item;
       }

       public HttpResponseMessage PostSearch(Search item)
       {
           item = repository.Add(item);
           var response = Request.CreateResponse<Search>(HttpStatusCode.Created, item);

           string uri = Url.Link("DefaultApi", new { id = item.Id });
           response.Headers.Location = new Uri(uri);
           return response;
       }

       public void PutSearch(int id, Search car)
       {
           car.Id = id;
           if (!repository.Update(car))
           {
               throw new HttpResponseException(HttpStatusCode.NotFound);
           }
       }

       public void DeleteSearch(int id)
       {
           Search item = repository.Get(id);
           if (item == null)
           {
               throw new HttpResponseException(HttpStatusCode.NotFound);
           }

           repository.Remove(id);
       }

    }

}
