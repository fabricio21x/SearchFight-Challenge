using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using SearchFight.Model.Interfaces;

namespace SearchFight.Model
{
    public class SerializableSearchEngine
    {
        [XmlAttribute("Address")]
        public string Address { get; set; }

        [XmlAttribute("SearchEngineName")]
        public string Name { get; set; }

        [XmlElement (ElementName = "Parser")]
        public SerializableParser Parser { get; set; }

        public SerializableSearchEngine() {}
    }
}
