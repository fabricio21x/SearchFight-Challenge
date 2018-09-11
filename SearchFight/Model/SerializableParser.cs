using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchFight.Model
{
    public class SerializableParser
    {
        [XmlAttribute]
        [DefaultValue(RegexOptions.None)]
        public RegexOptions Options { get; set; }

        [XmlAttribute("GroupIndex")]
        [DefaultValue(0)]
        public int GroupIndex { get; set; }

        [XmlElement (ElementName = "Pattern")]
        public string Pattern { get; set; }

        public SerializableParser() {}
    }
}
