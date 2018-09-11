using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using SearchFight.Model.Interfaces;

/// <summary>
/// Class used to load the search engines that are going to be 
/// used in the project
/// </summary>
namespace SearchFight.Model
{
    public class ProgramConfiguration
    {
        [XmlElement(ElementName = "SearchEngine")]
        public List<SerializableSearchEngine> SearchEngines { get; set; }


        public List<ISearchEngine> GetConfiguration()
        {
            using (var stream = File.OpenRead("config.xml"))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(ProgramConfiguration));
                    var searchEngines = (serializer.Deserialize(stream) as ProgramConfiguration).SearchEngines.ToList();
                    var engines = new List<ISearchEngine>();
                    foreach (var searchEngine in searchEngines)
                    {
                        var parser = new SearchResultParser(searchEngine.Parser.Pattern, searchEngine.Parser.Options, searchEngine.Parser.GroupIndex);
                        var temp = new SearchEngine(parser, searchEngine.Name, searchEngine.Address);
                        engines.Add(temp);
                    }

                    return engines;
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException("Configuration file invalid or corrupted. " + ex.Message, ex);
                }
            }
        }
    }
}
