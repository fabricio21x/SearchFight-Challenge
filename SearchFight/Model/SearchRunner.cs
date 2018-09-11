using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

/// <summary>
/// This class models a search engine with the methods needed to retrieve the 
/// result count from the queries entered by the user
/// </summary>
namespace SearchFight.Model
{
    public class SearchRunner : RunnerSerializer
    {
        [XmlAttribute]
        public string Address { get; set; }        
       
        public SearchClient Client { get; set; }
        public SearchResultParser Parser { get; set; }

        public SearchRunner()
        {
            Client = new SearchClient();
        }

        private Uri GetUri(string query)
        {
            string uri = Address + HttpUtility.UrlEncode(query);
            return new Uri(uri);
        }

        public long ProcessQuery(IQuery query)
        {
            var uri = GetUri(query.QueryText);
            string response = Client.GetResultString(uri);
            string result = Parser.Parse(response);

            return long.Parse(result.Replace(",", "").Replace(".", ""));
        }


    }
}
