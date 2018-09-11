using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchFight.Model;
using SearchFight.Controller;
using System.IO;
using System.Xml.Serialization;

namespace SearchFight
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Count() < 1)
            {
                Console.WriteLine("You have to enter at least one argument");
                return;
            }

            var runners = GetConfiguration().SearchRunners.ToList();
            SearchProcessor processor = new SearchProcessor(runners);
            InputValidator validator = new InputValidator();
            validator.Pattern = "^[a-zA-Z0-9 ]*$";

            for (int i = 0; i < args.Count(); i++)
            {
                string word = args[i];
                if (!validator.Validate(word))
                {
                    Console.WriteLine("One or more arguments are invalid.");
                    return;
                }       

                Query query = new Query();

                if (word.StartsWith("\""))
                {
                    query.SetContent(word.Substring(1));
                    while (true)
                    {
                        string nextWord = args[i++];
                        if (nextWord.EndsWith("\""))
                        {
                            query.SetContent(nextWord.Substring(0, nextWord.Length - 1));
                            break;
                        }
                        else
                            query.SetContent(nextWord);
                    }
                }
                else
                    query.SetContent(word);

                processor.AddQuery(query);
            }
            processor.ProcessQueries();
            PrintResults(processor, runners);

            Console.ReadLine();

        }


        private static void PrintResults(SearchProcessor processor, List<RunnerSerializer> runners)
        {
            for (int i = 0; i < processor.Queries.Count; i++)
            {
                Console.Write(string.Format("{0}) {1}: ", i, processor.Queries[i]));
                for (int j = 0; j < runners.Count; j++)
                {
                    Console.Write(string.Format("| {0} > {1} | ", runners[j].SearchEngineName, processor.Results[i, j]));
                }
                Console.WriteLine();
            }

            foreach (var winner in processor.PartialWinners)
            {
                Console.WriteLine(string.Format("*) {0} winner: {1}", winner.Key, winner.Value));
            }

            Console.WriteLine(string.Format("> Total winner: {0}", processor.TotalWinner));
        }


        private static ProgramConfiguration GetConfiguration()
        {
            using (var stream = File.OpenRead("config.xml"))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(ProgramConfiguration));
                    return (ProgramConfiguration)serializer.Deserialize(stream);
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException("Configuration file invalid or corrupted. " + ex.Message, ex);
                }
            }
        }
    }
}
