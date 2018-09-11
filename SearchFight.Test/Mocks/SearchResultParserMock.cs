using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchFight.Test.Mocks
{
    public class SearchResultParserMock
    {
        private string Pattern
        {
            get { return "\\<div[^\\>]+id=\"resultStats\"[^\\>]*\\>About ([\\d\\,\\.]+) results"; }
        }
       

        public string Parse(string response)
        {
            Regex re = new Regex(Pattern);
            Match match =  re.Match(response);

            return match.Groups[1].Value;
        }
    }
}
