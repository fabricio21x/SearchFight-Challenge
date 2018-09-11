using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using SearchFight.Model.Interfaces;

/// <summary>
/// Class used to extract, if exists, the result from the response text
/// given a regex pattern
/// </summary>
namespace SearchFight.Model
{
    public class SearchResultParser : ISearchParser
    {
        public string Pattern { get; set; }

        public RegexOptions Options { get; set; }

        public int GroupIndex { get; set; }

        private Match FindPattern(string responseString)
        {
            try
            {
                Regex re = new Regex(Pattern, Options);
                return re.Match(responseString);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("The regex is not valid. ", ex);
            }
        }

        public string Parse(string responseString)
        {
            try
            {
                var match = FindPattern(responseString);

                if (!match.Success)
                    throw new Exception("Could not find a matching string. ");

                if (match.Groups.Count <= GroupIndex)
                    throw new Exception("The given GroupIndex is out of range. ");

                return match.Groups[GroupIndex].Value;
            }
            catch (RegexMatchTimeoutException ex)
            {
                throw new RegexMatchTimeoutException(ex.Message);
            }
        }

        public SearchResultParser(string pattern, RegexOptions options, int groupIndex)
        {
            Pattern = pattern;
            Options = options;
            GroupIndex = groupIndex;
        }
    }
}
