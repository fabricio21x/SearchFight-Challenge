using SearchFight.Model.Interfaces;

namespace SearchFight.Test.Mocks
{
    public class SearchEngineMock : ISearchEngine
    {
        public string Address
        {
            get { return "https://www.google.com"; }
            set { ; }
        } 
        public string Name { get {return "Google"; } set { ; } }               

        private readonly SearchClientMock _client;
        public ISearchParser Parser { get; set; }

        public double ProcessQuery(IQuery query)
        {
            var response = _client.GetResultString();
            var result = Parser.Parse(response);

            return long.Parse(result.Replace(",", "").Replace(".", ""));
        }

        public SearchEngineMock(ISearchParser parser, SearchClientMock client)
        {
            Parser = parser;
            _client = client;
        }
    }
}
