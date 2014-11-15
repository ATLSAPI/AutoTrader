using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models
{
    public interface ISearchRepository
    {

        IEnumerable<Search> GetAll();
        Search Get(int id);
        Search Add(Search item);
        void Remove(int id);
        bool Update(Search item);

    }
  
}
