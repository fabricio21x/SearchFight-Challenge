using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

/// <summary>
/// Class used to load the search engines that are going to be 
/// used in the project
/// </summary>
namespace SearchFight.Model
{
    public class ProgramConfiguration
    {
        [XmlArrayItem("SearchEngine")]
        public List<RunnerSerializer> SearchRunners { get; set; }
    }
}
