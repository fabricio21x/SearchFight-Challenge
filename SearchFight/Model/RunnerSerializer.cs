using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchFight.Model
{
    [XmlInclude(typeof(SearchRunner))]
    public class RunnerSerializer
    {
        [XmlAttribute]
        public string SearchEngineName { get; set; }
    }
}
