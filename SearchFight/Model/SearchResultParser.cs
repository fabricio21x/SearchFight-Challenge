using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;


/// <summary>
/// Class used to extract, if exists, the result from the response text
/// given a regex pattern
/// </summary>
namespace SearchFight.Model
{
    public class SearchResultParser
    {
        public string Pattern { get; set; }

        [XmlAttribute]
        [DefaultValue(RegexOptions.None)]
        public RegexOptions Options { get; set; }

        [XmlAttribute]
        [DefaultValue(0)]
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
    }
}
