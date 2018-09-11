using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchFight.Model.Interfaces
{
    public interface ISearchEngine
    {        
        string Address { get; set; }
        
        string Name { get; set; }
        
        ISearchParser Parser { get; set; }

        double ProcessQuery(IQuery query);
    }
}
