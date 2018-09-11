using System.IO;

namespace SearchFight.Test.Mocks
{
    public class SearchClientMock
    {
        private readonly string _response;

        public string GetResultString()
        {            
            return _response;
        }

        public SearchClientMock()
        {
            var stream = File.OpenRead("responseString.txt");
            var reader = new StreamReader(stream);
            _response = reader.ReadToEnd();
        }
    }
}
