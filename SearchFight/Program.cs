using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web.ModelBinding;
using System.Xml;
using System.Xml.Serialization;
using SearchFight.Controller;
using SearchFight.Model;
using SearchFight.Model.Interfaces;

namespace SearchFight
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("You have to enter at least one argument");
                return;
            }

            var config = new ProgramConfiguration();
            var searchEngines = config.GetConfiguration();    
                   
            var processor = new SearchProcessor(searchEngines);
            var validator = new InputValidator {Pattern = @"^[a-zA-Z0-9 .#+!]*$"}; //We can modify this filter according of what we want to search for

            for (var i = 0; i < args.Count(); i++)
            {
                var word = args[i];
                if (!validator.Validate(word))
                {
                    Console.WriteLine("One or more arguments are invalid (admitted: words, numbers and symbols # . ! +).");
                    return;
                }

                var query = new Query();

                var tokens = word.Split(' ');
            
                foreach (var token in tokens)
                    query.SetContent(token);                                                   

                processor.AddQuery(query);
            }
            processor.ProcessQueries();
            PrintResults(processor, searchEngines);

            Console.WriteLine("Press Enter to finish");
            Console.ReadLine();

        }


        private static void PrintResults(SearchProcessor processor, List<ISearchEngine> runners)
        {
            for (var i = 0; i < processor.Queries.Count; i++)
            {
                Console.Write("{0}) {1}: ", i+1, processor.Queries[i]);
                for (var j = 0; j < runners.Count; j++)
                {
                    Console.Write("| {0} > {1} | ", runners[j].Name, processor.Results[i, j]);
                }
                Console.WriteLine();
            }

            foreach (var winner in processor.PartialWinners)
            {
                Console.WriteLine("*) {0} winner: {1}", winner.Key, winner.Value);
            }

            Console.WriteLine("> Total winner: {0}", processor.TotalWinner);
        }


        private static List<ISearchEngine> GetConfiguration()
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
                        var parser = new SearchResultParser(searchEngine.Parser.Pattern,  searchEngine.Parser.Options, searchEngine.Parser.GroupIndex);                        
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
