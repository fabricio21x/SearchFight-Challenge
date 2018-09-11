using System;
using System.Text.RegularExpressions;
using SearchFight.Model.Interfaces;

namespace SearchFight.Test.Mocks
{
    public class SearchResultParserMock : ISearchParser
    {
        public int GroupIndex
        {
            get { return 1; }
            set { ; }
        }

        public RegexOptions Options
        {
            get { return RegexOptions.None; }
            set { ; }
        }

        public string Pattern
        {
            get
            {
                return "\\<div[^\\>]+id=\"resultStats\"[^\\>]*\\>About ([\\d\\,\\.]+) results";
            }
            set { ; }
        }


        public string Parse(string response)
        {
            var re = new Regex(Pattern);
            var match =  re.Match(response);

            return match.Groups[GroupIndex].Value;
        }
    }
}
