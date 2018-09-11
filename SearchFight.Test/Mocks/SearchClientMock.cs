using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Test.Mocks
{
    public class SearchClientMock
    {
        private string _response;

        public string GetResultString()
        {            
            return _response;
        }

        public SearchClientMock()
        {
            var stream = File.OpenRead("responseString.txt");
            StreamReader reader = new StreamReader(stream);
            _response = reader.ReadToEnd();
        }
    }
}
