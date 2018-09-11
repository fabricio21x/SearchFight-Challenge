using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchFight.Model.Interfaces
{
    public interface ISearchParser
    {        
        RegexOptions Options { get; set; }       
        int GroupIndex { get; set; }        
        string Pattern { get; set; }

        string Parse(string response);
    }
}
