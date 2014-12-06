using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoServices.Models;
using System.Web.Http.Cors;
using System.Web;

namespace AutoServices.Controllers
{
    public class SmartSearchController : ApiController
    {
       
        bool chkinternet = CheckForInternetConnection();
   


         [EnableCors(origins: "*", headers: "*", methods: "*")]
         public IEnumerable<SmartSearch> GetSmasrtSearch_Param(string budget_type)
         {
          
       string request ;
       string mainrequest = Request.RequestUri.ParseQueryString().ToString();
             if (chkinternet == false) { return null; }
             else
             {
                 request = CreateSearchParam.ReturnQuery(mainrequest);

                // string param = budgettype(budget_type, req) + "" + request_string(req);
                 if (mainrequest.ToLower().Contains("&count"))
                 {

                     ISmartSearchRepository repository_Smart = new SmartSearchRepository(request+"&count", request);
                     return repository_Smart.GetAll();
                 }
                 else
                 {
                     string postcode = DistanceFromCar.GetPostCode4rmUri(mainrequest);
                     ISmartSearchRepository repository_Smart = new SmartSearchRepository(request, postcode);                   
                     return repository_Smart.GetAll();
                 }
             }
         }

       [EnableCors(origins: "*", headers: "*", methods: "*")]
       public SmartSearch GetSmartSearchID(string id)
       {
           ISmartSearchRepository repository_Smart = new SmartSearchRepository("", "");
           SmartSearch item = repository_Smart.Get(id);
           if (item == null)
           {
               throw new HttpResponseException(HttpStatusCode.NotFound);
           }
           return item;
       }
      
       [EnableCors(origins: "*", headers: "*", methods: "*")]
       public IEnumerable<SmartSearch> GetCompare_Param(string compare)
       {
           // List<SmartSearch> item = new List<SmartSearch>();
           string req = Request.RequestUri.ParseQueryString().ToString();
           if (chkinternet == false) { return null; }
           else
           {
              
                   ISmartSearchRepository repository_Smart = new SmartSearchRepository(req, "");
                   
                   return repository_Smart.GetAll();
               
           }
       }

        
      
     

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.goal.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

          

    }

}
