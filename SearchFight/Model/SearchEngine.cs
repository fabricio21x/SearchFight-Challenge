using System;
using System.Web;
using System.Xml.Serialization;
using SearchFight.Model.Interfaces;

/// <summary>
/// This class models a search engine with the methods needed to retrieve the 
/// result count from the queries entered by the user
/// </summary>
namespace SearchFight.Model
{
    public class SearchEngine : ISearchEngine
    {
        public string Address { get; set; }

        public string Name { get; set; }

        public ISearchParser Parser { get; set; }

        private SearchClient Client { get; }        

        public SearchEngine(ISearchParser parser, string name, string address)
        {            
            Client = new SearchClient();
            Address = address;
            Name = name;
            Parser = parser;
        }

        private Uri GetUri(string query)
        {
            string uri = Address + HttpUtility.UrlEncode(query);
            return new Uri(uri);
        }

        public double ProcessQuery(IQuery query)
        {
            var uri = GetUri(query.QueryText);
            string response = Client.GetResultString(uri);
            string result = Parser.Parse(response);

            return double.Parse(result.Replace(",", "").Replace(".", ""));
        }
    }
}
