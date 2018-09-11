using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchFight.Model;

namespace SearchFight.Test.Mocks
{
    public class SearchEngineMock
    {
        public string SearchEngineName
        {
            get { return "Google"; }
        }

        public string Address
        {
            get { return "https://www.google.com"; }
        }

        public SearchEngineMock(SearchResultParserMock parser, SearchClientMock client)
        {
            _parser = parser;
            _client = client;
        }

        public SearchClientMock _client; 
        public SearchResultParserMock _parser;

        public long ProcessQuery()
        {
            string response = _client.GetResultString();
            string result = _parser.Parse(response);

            return long.Parse(result.Replace(",", "").Replace(".", ""));
        }

    }
}
