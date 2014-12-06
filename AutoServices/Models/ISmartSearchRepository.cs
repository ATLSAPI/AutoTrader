using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models
{
    public interface ISmartSearchRepository    {

       

        IEnumerable<SmartSearch> GetAll();
     //   IEnumerable<SmartSearch> LoadCached();
      //  IEnumerable<SmartSearch> Compare(string FirstCar, string SecondCar);
        SmartSearch Get(string id);
        //IEnumerable<SmartSearch> GetFrom(int id);
       // IEnumerable<SmartSearch> GetAllRequest(string param);
        SmartSearch AddAll(SmartSearch item);
        SearchAttributes Add(SearchAttributes item); 
       // bool Update(SmartSearch item);

       // void Remove(int id);
        void GetATData(string uri, string postcode);

        

    }

  
  
}
